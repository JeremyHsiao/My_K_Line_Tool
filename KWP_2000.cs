using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockMessageLibrary;

namespace KWP_2000
{
    class KWP_2000_Process
    {
        private const byte ADDRESS_ABS = 0x28;
        private const byte ADDRESS_OBD = 0x10;
        private const int min_delay_before_response = 20;
        private const byte RETURN_SID_OR_VALUE = 0x40;

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
                bRet = ProcessMessage_ABS(in_msg);
            }
            else if (in_msg.GetTA() == ADDRESS_OBD)      
            {
                bRet = ProcessMessage_OBD(in_msg, ref out_msg);
            }
            return bRet;
        }

        private uint ABS_Reply_StopCommunication_Length = 0x01;
        private BlockMessage PrepareResponse_StopCommunication_ABS(BlockMessage in_msg, ref BlockMessage out_msg)
        {
            byte this_byte;
            out_msg.ClearBlockMessage();
            // Format 2
            // FMT
            this_byte = (byte)((((uint)MSG_A1A0_MODE.WITH_ADDRESS_INFO) << 6) | ABS_Reply_StopCommunication_Length);
            out_msg.SetTA(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // SA
            this_byte = in_msg.GetSA();
            out_msg.SetTA(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // TA
            this_byte = in_msg.GetTA();
            out_msg.SetSA(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // SID
            this_byte = (byte)(in_msg.GetSID() | RETURN_SID_OR_VALUE);
            out_msg.SetSID(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            return out_msg;
        }

        private uint ABS_KeyByte_for_StartCommunication = 0x8FEF;
        private uint ABS_Reply_StartCommunication_Length = 0x03;

        private BlockMessage PrepareResponse_StartCommunication_ABS(BlockMessage in_msg, ref BlockMessage out_msg)
        {
            byte this_byte;
            out_msg.ClearBlockMessage();
            // Format 4
            // FMT
            this_byte = (byte)((((uint)MSG_A1A0_MODE.WITH_ADDRESS_INFO) << 6));
            out_msg.SetTA(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // SA
            this_byte = in_msg.GetSA();
            out_msg.SetTA(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // TA
            this_byte = in_msg.GetTA();
            out_msg.SetSA(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // Len
            this_byte = (byte) ABS_Reply_StartCommunication_Length;
            out_msg.SetLen(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // SID
            this_byte = (byte)(in_msg.GetSID() | RETURN_SID_OR_VALUE);
            out_msg.SetSID(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // KeyByte * 2
            this_byte = (byte)(ABS_KeyByte_for_StartCommunication & 0xff);
            out_msg.AddToDataList(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            this_byte = (byte)((ABS_KeyByte_for_StartCommunication >> 8) & 0xff);
            out_msg.AddToDataList(this_byte);
            return out_msg;
        }

        private uint ReadDiagnosticTroubleCodesByStatus_ABS_StatusOfDTC = 0;
        private uint ReadDiagnosticTroubleCodesByStatus_ABS_GroupOfDTC = 0;
        private byte[] fixed_response_data_abs = { 0x04, 0x01, 0x15, 0x61, 0x40, 0x85, 0x62, 0x02, 0x30, 0x62, 0xC4, 0x86, 0x62 };

        private BlockMessage PrepareResponse_ReadDiagnosticTroubleCodesByStatus_ABS(BlockMessage in_msg, ref BlockMessage out_msg)
        {
            byte this_byte;
            // Read Status of DTC & Group of DTC
            List<byte> in_list = in_msg.GetDataList();

            ReadDiagnosticTroubleCodesByStatus_ABS_StatusOfDTC = (uint)in_list.IndexOf(0);
            ReadDiagnosticTroubleCodesByStatus_ABS_GroupOfDTC = (uint)in_list.IndexOf(1) + ((uint)in_list.IndexOf(2)) << 8;
            //[01:52:30.915] Format 2 - 84 28 FA 18 00 40 00  FE - ok
            //[01:52:30.980] Format 4 - 80 FA 28 0E 58 04 50 43 E0 50 45 E0 50 52 A0 50 53 A0 79 
            out_msg.ClearBlockMessage();
            // Format 4
            // FMT
            this_byte = (byte)((((uint)MSG_A1A0_MODE.WITH_ADDRESS_INFO) << 6));
            out_msg.SetTA(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // SA
            this_byte = in_msg.GetSA();
            out_msg.SetTA(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // TA
            this_byte = in_msg.GetTA();
            out_msg.SetSA(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // Len
            this_byte = (byte) fixed_response_data_abs.Length;
            out_msg.SetLen(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // SID
            this_byte = (byte)(in_msg.GetSID() | RETURN_SID_OR_VALUE);
            out_msg.SetSID(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // returning_data
            foreach (byte res in fixed_response_data_abs)
            {
                out_msg.AddToDataList(res);
                out_msg.UpdateCheckSum(res);
            }
            return out_msg;
        }

        public bool ProcessMessage_ABS(BlockMessage abs_msg)
        {
            bool bRet = false;

            switch((ENUM_SID)abs_msg.GetSID())
            {
                case ENUM_SID.ReadDiagnosticTroubleCodesByStatus:
                    break;
                case ENUM_SID.StartCommunication:
                    ResponseMessage = PrepareResponse_StartCommunication_ABS(abs_msg, ref ResponseMessage);
                    bRet = true;
                    break;
                case ENUM_SID.StopCommunication:
                    ResponseMessage = PrepareResponse_StopCommunication_ABS(abs_msg, ref ResponseMessage);
                    bRet = true;
                    break;
                default:
                    break;
            }

            return bRet;
        }

        private uint OBD_Reply_StopCommunication_Length = 0x01;
        private BlockMessage PrepareResponse_StopCommunication_OBD(BlockMessage in_msg, ref BlockMessage out_msg)
        {
            byte this_byte;
            out_msg.ClearBlockMessage();
            // Format 2
            // FMT
            this_byte = (byte)((((uint)MSG_A1A0_MODE.WITH_ADDRESS_INFO) << 6) | OBD_Reply_StopCommunication_Length);
            out_msg.SetTA(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // SA
            this_byte = in_msg.GetSA();
            out_msg.SetTA(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // TA
            this_byte = in_msg.GetTA();
            out_msg.SetSA(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // SID
            this_byte = (byte)(in_msg.GetSID() | RETURN_SID_OR_VALUE);
            out_msg.SetSID(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            return out_msg;
        }

        private uint OBD_KeyByte_for_StartCommunication = 0x8FEF;
        private uint OBD_Reply_StartCommunication_Length = 0x03;
        private BlockMessage PrepareResponse_StartCommunication_OBD(BlockMessage in_msg, ref BlockMessage out_msg)
        {
            byte this_byte;
            out_msg.ClearBlockMessage();
            // Format 2
            // FMT
            this_byte = (byte)((((uint)MSG_A1A0_MODE.WITH_ADDRESS_INFO) << 6) | OBD_Reply_StartCommunication_Length);
            out_msg.SetTA(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // SA
            this_byte = in_msg.GetSA();
            out_msg.SetTA(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // TA
            this_byte = in_msg.GetTA();
            out_msg.SetSA(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // SID
            this_byte = (byte)(in_msg.GetSID() | RETURN_SID_OR_VALUE);
            out_msg.SetSID(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // KeyByte * 2
            this_byte = (byte)(OBD_KeyByte_for_StartCommunication & 0xff);
            out_msg.AddToDataList(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            this_byte = (byte)((OBD_KeyByte_for_StartCommunication >> 8) & 0xff);
            out_msg.AddToDataList(this_byte);
            return out_msg;
        }

        private uint ReadDiagnosticTroubleCodesByStatus_OBD_StatusOfDTC = 0;
        private uint ReadDiagnosticTroubleCodesByStatus_OBD_GroupOfDTC = 0;
        private byte []fixed_response_data_obd = { 0x04, 0x01, 0x15, 0x61, 0x40, 0x85, 0x62, 0x02, 0x30, 0x62, 0xC4, 0x86, 0x62 };

        private BlockMessage PrepareResponse_ReadDiagnosticTroubleCodesByStatus_OBD(BlockMessage in_msg, ref BlockMessage out_msg)
        {
            byte this_byte;
            // Read Status of DTC & Group of DTC
            List<byte> in_list = in_msg.GetDataList();

            ReadDiagnosticTroubleCodesByStatus_OBD_StatusOfDTC = (uint) in_list.IndexOf(0);
            ReadDiagnosticTroubleCodesByStatus_OBD_GroupOfDTC = (uint)in_list.IndexOf(1) + ((uint)in_list.IndexOf(2))<<8;
            //[01:51:41.891] Format 2 - 84 10 F1 18 00 FF 00  9C - ok
            //[01:51:42.012] Format 2 - 8E F1 10 58 04 01 15 61 40 85 62 02 30 62 C4 86 62  C9 - ok
            out_msg.ClearBlockMessage();
            // Format 2
            // FMT
            this_byte = (byte)((((uint)MSG_A1A0_MODE.WITH_ADDRESS_INFO) << 6) | (fixed_response_data_obd.Length) );
            out_msg.SetTA(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // SA
            this_byte = in_msg.GetSA();
            out_msg.SetTA(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // TA
            this_byte = in_msg.GetTA();
            out_msg.SetSA(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // SID
            this_byte = (byte)(in_msg.GetSID() | RETURN_SID_OR_VALUE);
            out_msg.SetSID(this_byte);
            out_msg.UpdateCheckSum(this_byte);
            // returning_data
            foreach(byte res in fixed_response_data_obd)
            {
                out_msg.AddToDataList(res);
                out_msg.UpdateCheckSum(res);
            }
            return out_msg;

        }

        public bool ProcessMessage_OBD(BlockMessage obd_msg, ref BlockMessage ResponseMessage)
        {
            bool bRet = false;
            BlockMessageForSerialOutput out_str_proc = new BlockMessageForSerialOutput();
            List<byte> output_data = new List<byte>();

            switch ((ENUM_SID)obd_msg.GetSID())
            {
                case ENUM_SID.ReadDiagnosticTroubleCodesByStatus:
                    ResponseMessage = PrepareResponse_ReadDiagnosticTroubleCodesByStatus_OBD(obd_msg, ref ResponseMessage);
                    bRet = true;
                    break;
                case ENUM_SID.StartCommunication:
                    ResponseMessage = PrepareResponse_StartCommunication_OBD(obd_msg, ref ResponseMessage);
                    bRet = true;
                    break;
                case ENUM_SID.StopCommunication:
                    ResponseMessage = PrepareResponse_StopCommunication_OBD(obd_msg, ref ResponseMessage);
                    bRet = true;
                    break;
                default:
                    break;
            }

            return bRet;
        }

    }
}
