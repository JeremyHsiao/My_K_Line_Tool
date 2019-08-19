using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySerialLibrary;
using System.IO.Ports;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Timers;

namespace BlockMessageLibrary
{
    // enum
    enum FORMAT_ID
    {
        NEW = 0,
        ID1 = 1,
        ID2 = 2,
        ID3 = 3,
        ID4 = 4,
        WAIT_FOR_ZERO,
        OUT_OF_RANGE
    };

    enum MSG_A1A0_MODE
    {
        NO_ADDRESS_INFO = 0,
        CARB_MODE = 1,
        WITH_ADDRESS_INFO = 2,
        FUNCTIONAL_ADDRESSING = 3
    }

    enum MSG_STAGE_FORMAT_01
    {
        FMT = 0,
        SID = 1,
        Data = 2,
        CS = 3,
        END
    };

    enum MSG_STAGE_FORMAT_02
    {
        FMT = 0,
        TA = 1,
        SA = 2,
        SID = 3,
        Data = 4,
        CS = 5,
        END
    };

    enum MSG_STAGE_FORMAT_03
    {
        FMT = 0,
        Len = 1,
        SID = 2,
        Data = 3,
        CS = 4,
        END
    };

    enum MSG_STAGE_FORMAT_04
    {
        FMT = 0,
        TA = 1,
        SA = 2,
        Len = 3,
        SID = 4,
        Data = 5,
        CS = 6,
        END
    };

    class BlockMessage
    {
        // Packet data
        private bool With_Leading_00;
        private byte Fmt;
        private byte TA;
        private byte SA;
        private byte Len;
        private byte SID;
        private List<Byte> msg_data;
        private byte CheckSum;

        public BlockMessage()
        {
            msg_data = new List<byte>(); ClearBlockMessage();
        }

        public void ClearBlockMessage()
        {
            With_Leading_00 = false;
            Fmt = TA = SA = Len = SID = CheckSum = 0;            // set to 0 as null message
            msg_data.Clear();
        }

        public bool GetLeadingZero() { return With_Leading_00; }
        public void SetLeadingZero(bool LeadingZero) { With_Leading_00 = LeadingZero; }

        public byte GetFmt() { return Fmt; }
        public void SetFmt(byte NewFmt) { Fmt = NewFmt; }

        public byte GetTA() { return TA; }
        public void SetTA(byte NewTA) { TA = NewTA; }

        public byte GetSA() { return SA; }
        public void SetSA(byte NewSA) { SA = NewSA; }

        public byte GetLen() { return Len; }
        public void SetLen(byte NewLen) { Len = NewLen; }

        public byte GetSID() { return SID; }
        public void SetSID(byte NewSID) { SID = NewSID; }

        public byte GetCheckSum() { return CheckSum; }
        public void SetCheckSum(byte NewCheckSum) { CheckSum = NewCheckSum; }

        public List<byte> GetDataList() { return msg_data; }
        public void AddToDataList(byte NewData) { msg_data.Add(NewData); }
        public void ClearDataList() { msg_data.Clear(); }
        public int GetDataListLen() { return msg_data.Count; }

        public byte UpdateCheckSum(byte next_byte) { CheckSum += next_byte; return CheckSum; }
    }

    // This class is for generating output block message for serial output
    class BlockMessageForSerialOutput
    {
        // Internal data
        private BlockMessage BlockMessageInPreparation = new BlockMessage();
        private List<byte> SerialOutputDataList = new List<byte>();

        private const uint Max_Len_6Bit = 0x3f;
        // Please update according to ECU parameter
        private uint ECU_Dbmax = 40;

        // Additional string to store block message in string format
        private String out_msg_data_in_string;

        // Common Part after format header
        private void GenerateSIDDataChecksumString(ref List<byte> current_out_data, byte SID, List<byte> DataList)
        {
            current_out_data.Add(SID);
            BlockMessageInPreparation.UpdateCheckSum(SID);
            out_msg_data_in_string += SID.ToString("X2") + " ";
 //           out_msg_data_in_string += "SID:" + SID.ToString("X2") + " Data:";
            if (DataList.Count() > 0)
            {
                current_out_data.AddRange(DataList);
                foreach (byte element in DataList)
                {
                    BlockMessageInPreparation.UpdateCheckSum(element);
                    out_msg_data_in_string += element.ToString("X2") + " ";
                }
            }
            byte CheckSum = BlockMessageInPreparation.GetCheckSum();
            current_out_data.Add(CheckSum);
            out_msg_data_in_string += CheckSum.ToString("X2");
            //out_msg_data_in_string += "CS:" + CheckSum.ToString("X2");
        }

        public bool GenerateSerialOutput(out List<byte> out_data, bool ExtraLenByte = false)
        {
            // This for format 1 or 3 -- to be implemented later
            bool bRet = false;
            BlockMessageInPreparation.ClearBlockMessage();
            SerialOutputDataList.Clear();
            out_data = SerialOutputDataList;
            return bRet;
        }

        public bool GenerateSerialOutput(out List<byte> out_data, byte TA, byte SA, byte SID, List <byte> DataList, bool ExtraLenByte = false)
        {
            // This for format 2 or 4 -- to be implemented later
            bool bRet = false;

            BlockMessageInPreparation.ClearBlockMessage();
            SerialOutputDataList.Clear();

            // First calculate data length
            byte len = (byte)(DataList.Count()+1);      // SID is always there

            if (((ExtraLenByte==true)||(len>Max_Len_6Bit))&&(len < ECU_Dbmax))
            {
                // Format 4
                SerialOutputDataList.Add(0x00);
                byte fmt = ((byte)(MSG_A1A0_MODE.WITH_ADDRESS_INFO)) << 6;
                SerialOutputDataList.Add(fmt);
                BlockMessageInPreparation.UpdateCheckSum(fmt);
                SerialOutputDataList.Add(TA);
                BlockMessageInPreparation.UpdateCheckSum(TA);
                SerialOutputDataList.Add(SA);
                BlockMessageInPreparation.UpdateCheckSum(SA);
                SerialOutputDataList.Add(len);
                BlockMessageInPreparation.UpdateCheckSum(len);
                out_msg_data_in_string = "Out-format 4 - ";
                out_msg_data_in_string += fmt.ToString("X2") + " ";
                out_msg_data_in_string += TA.ToString("X2") + " ";
                out_msg_data_in_string += SA.ToString("X2") + " ";
                out_msg_data_in_string += len.ToString("X2") + " ";
                //out_msg_data_in_string += "Fmt:" + fmt.ToString("X2") + " ";
                //out_msg_data_in_string += "TA:" + TA.ToString("X2") + " ";
                //out_msg_data_in_string += "SA:" + SA.ToString("X2") + " ";
                //out_msg_data_in_string += "len:" + len.ToString("X2") + " ";
            }
            else if ((len < ECU_Dbmax) && (len <= Max_Len_6Bit))     // max 6-bit when there isn't extra length byte
            {
                SerialOutputDataList.Add(0x00);
                // Format 2
                byte fmt = ((byte)(MSG_A1A0_MODE.WITH_ADDRESS_INFO)) << 6;
                fmt |= len;
                SerialOutputDataList.Add(fmt);
                BlockMessageInPreparation.UpdateCheckSum(fmt);
                SerialOutputDataList.Add(TA);
                BlockMessageInPreparation.UpdateCheckSum(TA);
                SerialOutputDataList.Add(SA);
                BlockMessageInPreparation.UpdateCheckSum(SA);
                out_msg_data_in_string = "Out-format 2 - ";
                out_msg_data_in_string += fmt.ToString("X2") + " ";
                out_msg_data_in_string += TA.ToString("X2") + " ";
                out_msg_data_in_string += SA.ToString("X2") + " ";
                //out_msg_data_in_string += "Fmt:" + fmt.ToString("X2") + " ";
                //out_msg_data_in_string += "TA:" + TA.ToString("X2") + " ";
                //out_msg_data_in_string += "SA:" + SA.ToString("X2") + " ";
            }
            else
            {
                // Error in data length and need to handle it
            }

            // Common Part
            GenerateSIDDataChecksumString(ref SerialOutputDataList, SID, DataList);
            out_data = SerialOutputDataList;
            return bRet;
        }

        public String GetSerialOutputString() { return out_msg_data_in_string; }

    }

    // This class is for processing input block message from serial input
    class ProcessBlockMessage
    {
        // Internal data
        private BlockMessage BlockMessageInProcess;
        private FORMAT_ID Format_ID;
        private int ExpectedDataListLen;
        private uint msg_field_index;
        private bool LeadingWithZero = false;

        // Additional string to store block message in string format
        private String msg_data_in_string;

        // Private function
        private void StartNewProcess()
        {
            BlockMessageInProcess.ClearBlockMessage();
            msg_data_in_string = "";
            ExpectedDataListLen = 0;
            msg_field_index = 0;
            Format_ID = FORMAT_ID.WAIT_FOR_ZERO;
        }

        private bool ProcessFormat1(byte next_byte)
        {
            bool bRet = false;
            switch ((MSG_STAGE_FORMAT_01)msg_field_index)
            {
                case MSG_STAGE_FORMAT_01.FMT:
                    BlockMessageInProcess.SetFmt(next_byte);
                    ExpectedDataListLen = (next_byte & 0x3f) - 1;           // minus SID byte
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    msg_field_index++;
                    break;
                case MSG_STAGE_FORMAT_01.SID:
                    BlockMessageInProcess.SetSID(next_byte);
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    if (ExpectedDataListLen > 0)
                    {
                        msg_field_index++;
                    }
                    else
                    {
                        msg_field_index += 2;
                    }
                    break;
                case MSG_STAGE_FORMAT_01.Data:
                    BlockMessageInProcess.AddToDataList(next_byte);
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    if (BlockMessageInProcess.GetDataListLen() >= ExpectedDataListLen)
                    {
                        msg_field_index++;
                    }
                    break;
                case MSG_STAGE_FORMAT_01.CS:
                    byte current_checksum = BlockMessageInProcess.GetCheckSum();
                    bRet = (current_checksum == next_byte) ? true : false;      // data available if checksum is ok
                    Format_ID = FORMAT_ID.NEW;
                    msg_field_index = 0;
                    break;
            }
            return bRet;
        }

        private bool ProcessFormat2(byte next_byte)
        {
            bool bRet = false;
            switch ((MSG_STAGE_FORMAT_02)msg_field_index)
            {
                case MSG_STAGE_FORMAT_02.FMT:
                    BlockMessageInProcess.SetFmt(next_byte);
                    ExpectedDataListLen = (next_byte & 0x3f) - 1;           // minus SID byte
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    msg_field_index++;
                    msg_data_in_string += next_byte.ToString("X2") + " ";
//                    msg_data_in_string += "Fmt:" + next_byte.ToString("X2") + " ";
                    break;
                case MSG_STAGE_FORMAT_02.TA:
                    BlockMessageInProcess.SetTA(next_byte);
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    msg_field_index++;
                    msg_data_in_string += next_byte.ToString("X2") + " ";
//                   msg_data_in_string += "TA:" + next_byte.ToString("X2") + " ";
                    break;
                case MSG_STAGE_FORMAT_02.SA:
                    BlockMessageInProcess.SetSA(next_byte);
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    msg_field_index++;
                    msg_data_in_string += next_byte.ToString("X2") + " ";
//                    msg_data_in_string += "SA:" + next_byte.ToString("X2") + " ";
                    break;
                case MSG_STAGE_FORMAT_02.SID:
                    BlockMessageInProcess.SetSID(next_byte);
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    msg_data_in_string += next_byte.ToString("X2") + " ";
                    // msg_data_in_string += "SID:" + next_byte.ToString("X2") + " ";
                    if (ExpectedDataListLen > 0)
                    {
                        msg_field_index++;
//                        msg_data_in_string += "Data:";
                    }
                    else
                    {
                        msg_field_index += 2;
                    }
                    break;
                case MSG_STAGE_FORMAT_02.Data:
                    BlockMessageInProcess.AddToDataList(next_byte);
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    msg_data_in_string += next_byte.ToString("X2") + " ";
                    if (BlockMessageInProcess.GetDataListLen() >= ExpectedDataListLen)
                    {
                        msg_field_index++;
                        msg_data_in_string += " ";
                    }
                    break;
                case MSG_STAGE_FORMAT_02.CS:
                    byte current_checksum = BlockMessageInProcess.GetCheckSum();
                    Format_ID = FORMAT_ID.NEW;
                    msg_field_index = 0;
                    if (current_checksum == next_byte)
                    {
                        bRet = true;
                    }
                    else
                    {
                        bRet = false;
                    }
                    msg_data_in_string += next_byte.ToString("X2") + ((bRet) ? " - ok" : " - ng");
                    //                    msg_data_in_string += "CS:" + next_byte.ToString("X2") + ((bRet) ? " ok" : " ng");
                    break;
            }
            return bRet;
        }

        private bool ProcessFormat3(byte next_byte)
        {
            bool bRet = false;
            switch ((MSG_STAGE_FORMAT_03)msg_field_index)
            {
                case MSG_STAGE_FORMAT_03.FMT:
                    BlockMessageInProcess.SetFmt(next_byte);
                    ExpectedDataListLen = (next_byte & 0x3f) - 1;       // minus SID byte
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    msg_field_index++;
                    break;
                case MSG_STAGE_FORMAT_03.Len:
                    BlockMessageInProcess.SetLen(next_byte);
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    msg_field_index++;
                    break;
                case MSG_STAGE_FORMAT_03.SID:
                    BlockMessageInProcess.SetSID(next_byte);
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    if (ExpectedDataListLen > 0)
                    {
                        msg_field_index++;
                    }
                    else
                    {
                        msg_field_index += 2;
                    }
                    break;
                case MSG_STAGE_FORMAT_03.Data:
                    BlockMessageInProcess.AddToDataList(next_byte);
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    if (BlockMessageInProcess.GetDataListLen() >= ExpectedDataListLen)
                    {
                        msg_field_index++;
                    }
                    break;
                case MSG_STAGE_FORMAT_03.CS:
                    byte current_checksum = BlockMessageInProcess.GetCheckSum();
                    bRet = (current_checksum == next_byte) ? true : false;      // data available if checksum is ok
                    Format_ID = FORMAT_ID.NEW;
                    msg_field_index = 0;
                    break;
            }
            return bRet;
        }

        private bool ProcessFormat4(byte next_byte)
        {
            bool bRet = false;
            switch ((MSG_STAGE_FORMAT_04)msg_field_index)
            {
                case MSG_STAGE_FORMAT_04.FMT:
                    BlockMessageInProcess.SetFmt(next_byte);
                    ExpectedDataListLen = (next_byte & 0x3f) - 1;       // minus SID byte
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    msg_field_index++;
                    msg_data_in_string += next_byte.ToString("X2") + " ";
                    break;
                case MSG_STAGE_FORMAT_04.TA:
                    BlockMessageInProcess.SetTA(next_byte);
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    msg_field_index++;
                    msg_data_in_string += next_byte.ToString("X2") + " ";
                    break;
                case MSG_STAGE_FORMAT_04.SA:
                    BlockMessageInProcess.SetSA(next_byte);
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    msg_field_index++;
                    msg_data_in_string += next_byte.ToString("X2") + " ";
                    break;
                case MSG_STAGE_FORMAT_04.Len:
                    BlockMessageInProcess.SetLen(next_byte);
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    msg_field_index++;
                    msg_data_in_string += next_byte.ToString("X2") + " ";
                    break;
                case MSG_STAGE_FORMAT_04.SID:
                    BlockMessageInProcess.SetSID(next_byte);
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    if (ExpectedDataListLen > 0)
                    {
                        msg_field_index++;
                    }
                    else
                    {
                        msg_field_index += 2;
                    }
                    msg_data_in_string += next_byte.ToString("X2") + " ";
                    break;
                case MSG_STAGE_FORMAT_04.Data:
                    BlockMessageInProcess.AddToDataList(next_byte);
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    msg_data_in_string += next_byte.ToString("X2") + " ";
                    if (BlockMessageInProcess.GetDataListLen() >= ExpectedDataListLen)
                    {
                        msg_field_index++;
                    }
                    break;
                case MSG_STAGE_FORMAT_04.CS:
                    byte current_checksum = BlockMessageInProcess.GetCheckSum();
                    bRet = (current_checksum == next_byte) ? true : false;      // data available if checksum is ok
                    msg_data_in_string += next_byte.ToString("X2") + " ";
                    Format_ID = FORMAT_ID.NEW;
                    msg_field_index = 0;
                    break;
            }
            return bRet;
        }

        private const int P3_Time = 5000; // 5000ms
        private static Timer aTimer = new Timer(P3_Time); 
        private static bool P3_Timeout_Flag = false;

        private static void SetP3Timer()
        {
            aTimer = new System.Timers.Timer(P3_Time);
            aTimer.Elapsed += OnP3TimedEvent;
            aTimer.AutoReset = false;
            P3_Timeout_Flag = false;
            aTimer.Enabled = true;
        }

        private static void OnP3TimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            P3_Timeout_Flag = true;
            aTimer.Enabled = false;
        }

        private void ClearP3TimeoutFlag()
        {
            aTimer.Enabled = false;
            P3_Timeout_Flag = false;
            aTimer.Dispose();
        }

        private bool CheckAndClearP3Timeout()
        {
            bool bRet = P3_Timeout_Flag;
            ClearP3TimeoutFlag();
            return (bRet);
        }

        // Public function
        public ProcessBlockMessage() { BlockMessageInProcess = new BlockMessage(); StartNewProcess(); }

        public bool ProcessNextByte(byte next_byte)
        {
            bool bRet = false;

            switch (Format_ID)
            {
                case FORMAT_ID.WAIT_FOR_ZERO:
                    if (next_byte == 0)
                    {
                        Format_ID = FORMAT_ID.NEW;
                        msg_field_index = 0;
                        LeadingWithZero = true;
                    }
                    break;
                case FORMAT_ID.NEW:
                    if (CheckAndClearP3Timeout() == true)
                    {
                        // Already timeout, must be a zero for a valid data
                        if (next_byte != 0)
                        {
                            Format_ID = FORMAT_ID.WAIT_FOR_ZERO;
                        }
                        else
                        {
                            msg_field_index = 0;
                            LeadingWithZero = true;
                        }
                    }
                    else if (next_byte == 0)
                    {
                        msg_field_index = 0;
                        LeadingWithZero = true;
                    }
                    else 
                    {
                        BlockMessageInProcess.ClearBlockMessage();
                        msg_data_in_string = (LeadingWithZero) ? "Init - " : "";
                        LeadingWithZero = false;
                        MSG_A1A0_MODE mode_info = (MSG_A1A0_MODE)((next_byte & 0xc0) >> 6);
                        switch (mode_info)
                        {
                            case MSG_A1A0_MODE.NO_ADDRESS_INFO:      // No Address Information
                                if ((next_byte & 0x3f) == 0)
                                {
                                    msg_data_in_string += "Format 3 - ";
                                    Format_ID = FORMAT_ID.ID3;
                                    ProcessFormat3(next_byte);
                                }
                                else
                                {
                                    msg_data_in_string += "Format 1 - ";
                                    Format_ID = FORMAT_ID.ID1;
                                    ProcessFormat1(next_byte);
                                }
                                break;
                            case MSG_A1A0_MODE.CARB_MODE:
                                // CARB mode - to be checked & implemented
                                Format_ID = FORMAT_ID.OUT_OF_RANGE;
                                break;
                            case MSG_A1A0_MODE.WITH_ADDRESS_INFO:
                                if ((next_byte & 0x3f) == 0)
                                {
                                    msg_data_in_string += "Format 4 - ";
                                    Format_ID = FORMAT_ID.ID4;
                                    ProcessFormat4(next_byte);
                                }
                                else
                                {
                                    msg_data_in_string += "Format 2 - ";
                                    Format_ID = FORMAT_ID.ID2;
                                    ProcessFormat2(next_byte);
                                }
                                break;
                            case MSG_A1A0_MODE.FUNCTIONAL_ADDRESSING:
                                // Functional Addressing mode are not supported here
                                Format_ID = FORMAT_ID.OUT_OF_RANGE;
                                break;
                        }
                    }
                    break;
                case FORMAT_ID.ID1:
                    bRet = ProcessFormat1(next_byte);
                    break;
                case FORMAT_ID.ID2:
                    bRet = ProcessFormat2(next_byte);
                    break;
                case FORMAT_ID.ID3:
                    bRet = ProcessFormat3(next_byte);
                    break;
                case FORMAT_ID.ID4:
                    bRet = ProcessFormat4(next_byte);
                    break;
                default:
                    if (next_byte == 0)
                    {
                        Format_ID = FORMAT_ID.NEW;
                        msg_field_index = 0;
                    }
                    break;
            }
            if(bRet==true)
            {
                SetP3Timer();
            }
            return bRet;
        }

        public BlockMessage GetBlockMessage() { return BlockMessageInProcess; }

        public String GetBlockMessageString() { return msg_data_in_string; }
    }
}
