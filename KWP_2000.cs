using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockMessageLibrary;
using DTC_ABS;
using DTC_OBD;

namespace KWP_2000
{
    class DTC_Data
    {
        private byte dtc_high;
        private byte dtc_low;
        private byte status_of_dtc;

        public DTC_Data(byte high, byte low, byte status)
        {
            dtc_high = high;
            dtc_low = low;
            status_of_dtc = status;
        }

        public List<byte> ToByteList()
        {
            List<byte> ret_list = new List<byte>();
            ret_list.Add(dtc_high);
            ret_list.Add(dtc_low);
            ret_list.Add(status_of_dtc);
            return ret_list;
        }
    }

    class KWP_2000_Process
    {
        public const byte ADDRESS_ABS = 0x28;
        public const byte ADDRESS_OBD = 0x10;
        public const int min_delay_before_response = 20;
        private const byte RETURN_SID_OR_VALUE = 0x40;
        private const byte NEGATIVE_RESPONSE_SID = 0x7F;
        public const int ReadDiagnosticCodesByStatus_MaxNumberOfDTC = 6;

        enum ENUM_SID
        {
            ReadDiagnosticTroubleCodesByStatus = 0x18,
            StartCommunication = 0x81,
            StopCommunication = 0x82,
            MAX_SID_PLUS_1
        };


        //private BlockMessage InComingMessage = new BlockMessage();
        private BlockMessage ResponseMessage = new BlockMessage();

        public KWP_2000_Process()
        {
            //InComingMessage.ClearBlockMessage();
            ResponseMessage.ClearBlockMessage();
        }

        public bool ProcessMessage(BlockMessage in_msg, ref BlockMessage out_msg)
        {
            bool bRet = false;

            if (in_msg.GetTA() == ADDRESS_ABS)      
            {
                bRet = ProcessMessage_ABS(in_msg, ref out_msg);
            }
            else if (in_msg.GetTA() == ADDRESS_OBD)      
            {
                bRet = ProcessMessage_OBD(in_msg, ref out_msg);
            }
            return bRet;
        }

        private BlockMessage PrepareResponse_StopCommunication_ABS(BlockMessage in_msg, ref BlockMessage out_msg)
        {
            List<byte> out_list = new List<byte>();
            out_msg = new BlockMessage((byte)((((uint)MSG_A1A0_MODE.WITH_ADDRESS_INFO) << 6)), in_msg.GetSA(), in_msg.GetTA(),
                                                (byte)(in_msg.GetSID() | RETURN_SID_OR_VALUE), out_list, true); // for format_4
            return out_msg;
        }

        private uint ABS_KeyByte_for_StartCommunication = 0x8FEF;
        private BlockMessage PrepareResponse_StartCommunication_ABS(BlockMessage in_msg, ref BlockMessage out_msg)
        {
            List<byte> out_list = new List<byte>();
            out_list.Add((byte)(ABS_KeyByte_for_StartCommunication & 0xff));
            out_list.Add((byte)((ABS_KeyByte_for_StartCommunication >> 8) & 0xff));
            out_msg = new BlockMessage((byte)((((uint)MSG_A1A0_MODE.WITH_ADDRESS_INFO) << 6)), in_msg.GetSA(), in_msg.GetTA(),
                                                (byte)(in_msg.GetSID() | RETURN_SID_OR_VALUE), out_list, true); // for format_4
            return out_msg;
        }

        private uint ReadDiagnosticTroubleCodesByStatus_ABS_StatusOfDTC = 0;
        private uint ReadDiagnosticTroubleCodesByStatus_ABS_GroupOfDTC = 0;
//        private byte[] fixed_response_data_abs = { 0x04, 0x50, 0x43, 0xE0, 0x50, 0x45, 0xE0, 0x50, 0x52, 0xA0, 0x50, 0x53, 0xA0 };
        // 0x5043
        // 0x5045
        // 0x5052
        // 0x5053
        public const byte Lamp_OFF_Failure_Set   = 0x60;
        public const byte Lamp_OFF_Failure_Reset = 0x20;
        public const byte Lamp_ON_Failure_Set    = 0xE0;
        public const byte Lamp_ON_Failure_Reset  = 0xA0;
        public byte[] dtc_status_table = { Lamp_ON_Failure_Reset, Lamp_ON_Failure_Set };  // bit 7: lamp off/on (always on here); bit 6,5: 01/11 = failure is reset/set at time of request
        private byte[] dtc_status_table_lamp_may_by_off = { Lamp_OFF_Failure_Reset, Lamp_OFF_Failure_Set, Lamp_ON_Failure_Reset, Lamp_ON_Failure_Set };
        private Queue<DTC_Data> ABS_DTC_Data_Queue = new Queue<DTC_Data>();
        private Queue<DTC_Data> OBD_DTC_Data_Queue = new Queue<DTC_Data>();

        public void ABS_DTC_Queue_Clear()
        {
            ABS_DTC_Data_Queue.Clear();
        }

        public void ABS_DTC_Queue_Add(byte dtc_high, byte dtc_low, byte lamp_status)
        {
            ABS_DTC_Data_Queue.Enqueue(new DTC_Data(dtc_high, dtc_low, lamp_status));
        }

        public void ABS_DTC_Queue_Add(CMD_E_ABS_DTC new_dtc, byte lamp_status)
        {
            uint dtc = (uint)new_dtc.DTC;
            ABS_DTC_Data_Queue.Enqueue(new DTC_Data((byte)(dtc >> 8), (byte)(dtc & 0xff), lamp_status));
        }

        public void OBD_DTC_Queue_Clear()
        {
            OBD_DTC_Data_Queue.Clear();
        }

        public void OBD_DTC_Queue_Add(byte dtc_high, byte dtc_low, byte lamp_status)
        {
            OBD_DTC_Data_Queue.Enqueue(new DTC_Data(dtc_high, dtc_low, lamp_status));
        }

        public void OBD_DTC_Queue_Add(CMD_F_OBD_DTC new_dtc, byte lamp_status)
        {
            uint dtc = (uint) new_dtc.DTC;
            OBD_DTC_Data_Queue.Enqueue(new DTC_Data((byte)(dtc >> 8), (byte)(dtc & 0xff), lamp_status));
        }

        private List<byte> GenerateQueuedResponseData_ABS()
        {
            List<byte> ret_list = new List<byte>();
            List<byte> status_of_dtc_list = new List<byte>();
            Byte DTC_no = 0;

            while ((ABS_DTC_Data_Queue.Count > 0) && (DTC_no < ReadDiagnosticCodesByStatus_MaxNumberOfDTC))
            {
                DTC_Data this_abs_dtc = ABS_DTC_Data_Queue.Dequeue();
                ret_list.AddRange(this_abs_dtc.ToByteList());
                DTC_no++;
            }
            status_of_dtc_list.Add(DTC_no);
            status_of_dtc_list.AddRange(ret_list);
            return status_of_dtc_list;
        }

        private List<byte> GenerateQueuedResponseData_OBD()
        {
            List<byte> ret_list = new List<byte>();
            List<byte> status_of_dtc_list = new List<byte>();
            Byte DTC_no = 0;

            while((OBD_DTC_Data_Queue.Count>0)&&(DTC_no< ReadDiagnosticCodesByStatus_MaxNumberOfDTC))
            {
                DTC_Data this_obd_dtc = OBD_DTC_Data_Queue.Dequeue();
                ret_list.AddRange(this_obd_dtc.ToByteList());
                DTC_no++;
            }
            status_of_dtc_list.Add(DTC_no);
            status_of_dtc_list.AddRange(ret_list);
            return status_of_dtc_list;
        }

        //private List<byte> GenerateRandomResponseData_ABS()
        //{
        //    Random rs = new Random();
        //    List<byte> ret_list = new List<byte>();
        //    CMD_E_ABS_DTC random_dtc;
        //    uint dtc;

        //    Byte DTC_no = (byte)(rs.Next(20));
        //    DTC_no = (byte)((DTC_no <= ReadDiagnosticCodesByStatus_MaxNumberOfDTC) ? DTC_no : 0);
        //    ret_list.Add(DTC_no);
        //    for (int no = 0; no < DTC_no; no++)
        //    {
        //        random_dtc = ABS_DTC_Table.Find_ABS_DTC(rs.Next(ABS_DTC_Table.Count()));
        //        dtc = (uint)random_dtc.DTC;
        //        ret_list.Add((byte)(dtc >> 8));               // HighByte
        //        ret_list.Add((byte)(dtc & 0xff));             // LowByte
        //        ret_list.Add((byte)dtc_status_table[rs.Next(dtc_status_table.Length)]);        // Status DTC -- randon values 
        //    }
        //    return ret_list;
        //}

        //private List<byte> GenerateFixednResponseData_ABS()
        //{
        //    List<byte> ret_list = new List<byte>();
        //    ret_list.AddRange(fixed_response_data_abs);
        //    return ret_list;
        //}

        private BlockMessage PrepareResponse_ReadDiagnosticTroubleCodesByStatus_ABS(BlockMessage in_msg, ref BlockMessage out_msg)
        {
            List<byte> out_list = GenerateQueuedResponseData_ABS();
            out_msg = new BlockMessage((byte)((((uint)MSG_A1A0_MODE.WITH_ADDRESS_INFO) << 6)), in_msg.GetSA(), in_msg.GetTA(),
                                                (byte)(in_msg.GetSID() | RETURN_SID_OR_VALUE), out_list, true); // for format_4
            return out_msg;
        }

        private const byte NegativeResponse_DuringInit_ReadDiagnosticTroubleCodesByStatus_ResponseCode = 0x78; 
        private BlockMessage PrepareNegativeResponse_DuringInit_ReadDiagnosticTroubleCodesByStatus_ABS(BlockMessage in_msg, ref BlockMessage out_msg)
        {
            List<byte> out_list = new List<byte>();

            out_list.Add(in_msg.GetSID());
            out_list.Add(NegativeResponse_DuringInit_ReadDiagnosticTroubleCodesByStatus_ResponseCode);
            out_msg = new BlockMessage((byte)((((uint)MSG_A1A0_MODE.WITH_ADDRESS_INFO) << 6)), in_msg.GetSA(), in_msg.GetTA(),
                                                NEGATIVE_RESPONSE_SID, out_list, true); // for format_4
            return out_msg;
        }

        private const byte NegativeResponse_MsgError_ReadDiagnosticTroubleCodesByStatus_ResponseCode = 0x12;
        private BlockMessage PrepareNegativeResponse_MsgErrort_ReadDiagnosticTroubleCodesByStatus_ABS(BlockMessage in_msg, ref BlockMessage out_msg)
        {
            List<byte> out_list = new List<byte>();

            out_list.Add(in_msg.GetSID());
            out_list.Add(NegativeResponse_MsgError_ReadDiagnosticTroubleCodesByStatus_ResponseCode);
            out_msg = new BlockMessage((byte)((((uint)MSG_A1A0_MODE.WITH_ADDRESS_INFO) << 6)), in_msg.GetSA(), in_msg.GetTA(),
                                                NEGATIVE_RESPONSE_SID, out_list, true); // for format_4
            return out_msg;
        }


        public bool ProcessMessage_ABS(BlockMessage abs_msg, ref BlockMessage ResponseMessage)
        {
            bool bRet = false;

            switch((ENUM_SID)abs_msg.GetSID())
            {
                case ENUM_SID.ReadDiagnosticTroubleCodesByStatus:
                    // Read Status of DTC & Group of DTC
                    List<byte> in_list = abs_msg.GetDataList();
                    ReadDiagnosticTroubleCodesByStatus_ABS_StatusOfDTC = (uint)in_list.IndexOf(0);
                    ReadDiagnosticTroubleCodesByStatus_ABS_GroupOfDTC = (uint)in_list.IndexOf(1) + ((uint)in_list.IndexOf(2)) << 8;
                    PrepareResponse_ReadDiagnosticTroubleCodesByStatus_ABS(abs_msg, ref ResponseMessage);
                    bRet = true;
                    break;
                case ENUM_SID.StartCommunication:
                    PrepareResponse_StartCommunication_ABS(abs_msg, ref ResponseMessage);
                    bRet = true;
                    break;
                case ENUM_SID.StopCommunication:
                    PrepareResponse_StopCommunication_ABS(abs_msg, ref ResponseMessage);
                    bRet = true;
                    break;
                default:
                    break;
            }

            return bRet;
        }

        private BlockMessage PrepareResponse_StopCommunication_OBD(BlockMessage in_msg, ref BlockMessage out_msg)
        {
            List<byte> out_list = new List<byte>();
            out_msg = new BlockMessage((byte)((((uint)MSG_A1A0_MODE.WITH_ADDRESS_INFO) << 6)), in_msg.GetSA(), in_msg.GetTA(),
                                                (byte)(in_msg.GetSID() | RETURN_SID_OR_VALUE), out_list, false); // for format_2
            return out_msg;
        }

        private uint OBD_KeyByte_for_StartCommunication = 0x8FEF;
        private BlockMessage PrepareResponse_StartCommunication_OBD(BlockMessage in_msg, ref BlockMessage out_msg)
        {
            List<byte> out_list = new List<byte>();
            out_list.Add((byte)(OBD_KeyByte_for_StartCommunication & 0xff));
            out_list.Add((byte)((OBD_KeyByte_for_StartCommunication >> 8) & 0xff));
            out_msg = new BlockMessage((byte)((((uint)MSG_A1A0_MODE.WITH_ADDRESS_INFO) << 6)), in_msg.GetSA(), in_msg.GetTA(),
                                                (byte)(in_msg.GetSID() | RETURN_SID_OR_VALUE), out_list, false); // for format_2
            return out_msg;
        }

        private uint ReadDiagnosticTroubleCodesByStatus_OBD_StatusOfDTC = 0;
        private uint ReadDiagnosticTroubleCodesByStatus_OBD_GroupOfDTC = 0;
        private byte []fixed_response_data_obd = { 0x04, 0x01, 0x15, 0x61, 0x40, 0x85, 0x62, 0x02, 0x30, 0x62, 0xC4, 0x86, 0x62 };
        // P0115: 0x0115
        // C0085: 0x4085
        // P0230: 0x0230
        // U0486: 0xC486

        private List<byte> GenerateFixednResponseData_OBD()
        {
            List<byte> ret_list = new List<byte>();
            ret_list.AddRange(fixed_response_data_obd);
            return ret_list;
        }

        private BlockMessage PrepareResponse_ReadDiagnosticTroubleCodesByStatus_OBD(BlockMessage in_msg, ref BlockMessage out_msg)
        {
            //List<byte> out_list = GenerateFixednResponseData_OBD();
            List<byte> out_list = GenerateQueuedResponseData_OBD();
            out_msg = new BlockMessage((byte)((((uint)MSG_A1A0_MODE.WITH_ADDRESS_INFO) << 6)), in_msg.GetSA(), in_msg.GetTA(),
                                                (byte)(in_msg.GetSID() | RETURN_SID_OR_VALUE), out_list, false); // for format_2
            return out_msg;
        }

        public bool ProcessMessage_OBD(BlockMessage obd_msg, ref BlockMessage ResponseMessage)
        {
            bool bRet = false;

            switch ((ENUM_SID)obd_msg.GetSID())
            {
                case ENUM_SID.ReadDiagnosticTroubleCodesByStatus:
                    List<byte> in_list = obd_msg.GetDataList();
                    ReadDiagnosticTroubleCodesByStatus_OBD_StatusOfDTC = (uint)in_list.IndexOf(0);
                    ReadDiagnosticTroubleCodesByStatus_OBD_GroupOfDTC = (uint)in_list.IndexOf(1) + ((uint)in_list.IndexOf(2)) << 8;
                    PrepareResponse_ReadDiagnosticTroubleCodesByStatus_OBD(obd_msg, ref ResponseMessage);
                    bRet = true;
                    break;
                case ENUM_SID.StartCommunication:
                    PrepareResponse_StartCommunication_OBD(obd_msg, ref ResponseMessage);
                    bRet = true;
                    break;
                case ENUM_SID.StopCommunication:
                    PrepareResponse_StopCommunication_OBD(obd_msg, ref ResponseMessage);
                    bRet = true;
                    break;
                default:
                    break;
            }

            return bRet;
        }

    }
}
