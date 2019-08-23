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
        private byte Fmt;
        private byte TA;
        private byte SA;
        private byte Len;
        private byte SID;
        private List<Byte> msg_data;
        private byte CheckSum;
        public const uint Max_Len_6Bit = 0x3f;

        public BlockMessage()
        {
            msg_data = new List<byte>(); ClearBlockMessage();
        }

        public BlockMessage(byte FMT_value, byte TA_value, byte SA_value, byte SID_value, List<byte> DataList, bool ExtraLenByte = false)
        {
            // For format 2/4
            msg_data = new List<byte>(); ClearBlockMessage();

            // returning_data
            foreach (byte res in DataList)
            {
                AddToDataList(res);
                UpdateCheckSum(res);
            }

            // FMT
            byte total_len_value = (byte)(GetDataListLen() + 1);
            if ((ExtraLenByte == true)||(total_len_value> Max_Len_6Bit))
            {
                Fmt = (byte)(FMT_value & ~Max_Len_6Bit);   // clear 6-bit LSB 
                UpdateCheckSum(Fmt);
                Len = total_len_value; // plus SID_byte
                UpdateCheckSum(Len);
            }
            else
            {
                Fmt = (byte)((FMT_value & ~Max_Len_6Bit) | (total_len_value& Max_Len_6Bit));   // clear 6-bit LSB then OR len_value
                UpdateCheckSum(Fmt);
                Len = 0;    // Clear for not-used
            }

            // TA
            TA = TA_value;
            UpdateCheckSum(TA_value);
            // SA
            SA = SA_value;
            UpdateCheckSum(SA_value);
            // SID
            SID = SID_value;
            UpdateCheckSum(SID_value);
        }

        public void ClearBlockMessage()
        {
            Fmt = TA = SA = Len = SID = CheckSum = 0;            // set to 0 as null message
            msg_data.Clear();
        }

        public byte GetFmt() { return Fmt; }
        public void SetFmt(byte NewFmt) { Fmt = NewFmt; }

        public byte GetTA() { return TA; }
        public void SetTA(byte NewTA) { TA = NewTA; }

        public byte GetSA() { return SA; }
        public void SetSA(byte NewSA) { SA = NewSA; }

        public byte GetLenByte() { return Len; }
        public void SetLenByte(byte NewLen) { Len = NewLen; }

        public uint GetMessageTotalLen()
        {
            uint temp_len = Fmt & Max_Len_6Bit;
            if (temp_len == 0)
            {
                temp_len = Len;
            }
            return temp_len;
        }

        public byte GetSID() { return SID; }
        public void SetSID(byte NewSID) { SID = NewSID; }

        public byte GetCheckSum() { return CheckSum; }
        public void SetCheckSum(byte NewCheckSum) { CheckSum = NewCheckSum; }

        public List<byte> GetDataList() { return msg_data; }
        public void AddToDataList(byte NewData) { msg_data.Add(NewData); }
        public void ClearDataList() { msg_data.Clear(); }
        public int GetDataListLen() { return msg_data.Count; }

        public byte UpdateCheckSum(byte next_byte) { CheckSum += next_byte; return CheckSum; }

        public bool GenerateSerialOutput(out List<byte> SerialOutputDataList)
        {
            bool bRet = false;
            byte byte_data;

            SerialOutputDataList = new List<byte>();

            // First calculate data length
            uint len = this.GetMessageTotalLen();

            if ((this.GetFmt() & ~BlockMessage.Max_Len_6Bit) == ((byte)(MSG_A1A0_MODE.WITH_ADDRESS_INFO) << 6))
            {
                // This for format 2 or 4 
                // Common portion
                byte_data = this.GetFmt();
                SerialOutputDataList.Add(byte_data);
                byte_data = this.GetTA();
                SerialOutputDataList.Add(byte_data);
                byte_data = this.GetSA();
                SerialOutputDataList.Add(byte_data);

                if ((this.GetFmt() & BlockMessage.Max_Len_6Bit) == 0x00) 
                {
                    // Format 4
                    byte_data = this.GetLenByte();
                    SerialOutputDataList.Add(byte_data);
                    bRet = true;
                }
                else if (len <= BlockMessage.Max_Len_6Bit)     // max 6-bit when there isn't extra length byte
                {
                    // Format 2
                     bRet = true;
                }
                else
                {
                    // Error in Len
                }
                // Common Part
                byte_data = this.GetSID();
                SerialOutputDataList.Add(byte_data);
                foreach (byte element in this.GetDataList())
                {
                    SerialOutputDataList.Add(element);
                }
                byte_data = this.GetCheckSum();
                SerialOutputDataList.Add(byte_data);
            }
            else
            {
                // Format 1/3 to be implemented in the future
            }
            return bRet;
        }

        public String GenerateDebugString()
        {
            String out_msg_data_in_string = "" ;
            byte byte_data;

            // First calculate data length
            uint len = this.GetMessageTotalLen();

            if ((this.GetFmt() & ~BlockMessage.Max_Len_6Bit) == ((byte)(MSG_A1A0_MODE.WITH_ADDRESS_INFO) << 6))
            {
                // This for format 2 or 4 
                // Common portion
                out_msg_data_in_string = "";
                byte_data = this.GetFmt();
                out_msg_data_in_string += byte_data.ToString("X2") + " ";
                byte_data = this.GetTA();
                out_msg_data_in_string += byte_data.ToString("X2") + " ";
                byte_data = this.GetSA();
                out_msg_data_in_string += byte_data.ToString("X2") + " ";

                if (((this.GetFmt() & BlockMessage.Max_Len_6Bit) == 0x00) || (len > BlockMessage.Max_Len_6Bit)) 
                {
                    // Format 4
                    out_msg_data_in_string = "Format 4 - " + out_msg_data_in_string;
                    byte_data = this.GetLenByte();
                    out_msg_data_in_string += byte_data.ToString("X2") + " ";
                }
                else if (len <= BlockMessage.Max_Len_6Bit)     // max 6-bit when there isn't extra length byte
                {
                    // Format 2
                    out_msg_data_in_string = "Format 2 - " + out_msg_data_in_string;
                }
                else
                {
                    out_msg_data_in_string = "Data Error - to be checked.";
                }
                // Common Part
                byte_data = this.GetSID();
                out_msg_data_in_string += byte_data.ToString("X2") + " ";
                foreach (byte element in this.GetDataList())
                {
                    out_msg_data_in_string += element.ToString("X2") + " ";
                }
                byte_data = this.GetCheckSum();
                out_msg_data_in_string += byte_data.ToString("X2") + " ";
            }
            else
            {
                // Format 1/3 to be implemented in the future
            }

            return out_msg_data_in_string;
        }
    }

    // This class is for processing input block message from serial input
    class ProcessBlockMessage
    {
        // Internal data
        private BlockMessage BlockMessageInProcess;
        private FORMAT_ID Format_ID;
        private int ExpectedDataListLen;
        private uint msg_field_index;

        // Private function
        private void StartNewProcess()
        {
            BlockMessageInProcess.ClearBlockMessage();
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
                    break;
                case MSG_STAGE_FORMAT_02.TA:
                    BlockMessageInProcess.SetTA(next_byte);
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    msg_field_index++;
                    break;
                case MSG_STAGE_FORMAT_02.SA:
                    BlockMessageInProcess.SetSA(next_byte);
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    msg_field_index++;
                    break;
                case MSG_STAGE_FORMAT_02.SID:
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
                case MSG_STAGE_FORMAT_02.Data:
                    BlockMessageInProcess.AddToDataList(next_byte);
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    if (BlockMessageInProcess.GetDataListLen() >= ExpectedDataListLen)
                    {
                        msg_field_index++;
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
//                   ExpectedDataListLen = (next_byte & 0x3f) - 1;       // minus SID byte
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    msg_field_index++;
                    break;
                case MSG_STAGE_FORMAT_03.Len:
                    BlockMessageInProcess.SetLenByte(next_byte);
                    ExpectedDataListLen = next_byte - 1;       // minus SID byte
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
//                    ExpectedDataListLen = (next_byte & 0x3f) - 1;       // minus SID byte
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    msg_field_index++;
                     break;
                case MSG_STAGE_FORMAT_04.TA:
                    BlockMessageInProcess.SetTA(next_byte);
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    msg_field_index++;
                    break;
                case MSG_STAGE_FORMAT_04.SA:
                    BlockMessageInProcess.SetSA(next_byte);
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    msg_field_index++;
                     break;
                case MSG_STAGE_FORMAT_04.Len:
                    BlockMessageInProcess.SetLenByte(next_byte);
                    ExpectedDataListLen = next_byte - 1;       // minus SID byte
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                    msg_field_index++;
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
                   break;
                case MSG_STAGE_FORMAT_04.Data:
                    BlockMessageInProcess.AddToDataList(next_byte);
                    BlockMessageInProcess.UpdateCheckSum(next_byte);
                     if (BlockMessageInProcess.GetDataListLen() >= ExpectedDataListLen)
                    {
                        msg_field_index++;
                    }
                    break;
                case MSG_STAGE_FORMAT_04.CS:
                    byte current_checksum = BlockMessageInProcess.GetCheckSum();
                    bRet = (current_checksum == next_byte) ? true : false;      // data available if checksum is ok
                    Format_ID = FORMAT_ID.NEW;
                    msg_field_index = 0;
                    break;
            }
            return bRet;
        }

        public const int P2_Time_min = 25;
        public const int P2_Time_max = 50;
        public const int P3_Time_min = 25;
        public const int P3_Time_max = 5000; // 5000ms
        private static Timer aTimer = new Timer(P3_Time_max); 
        private static bool P3_Timeout_Flag = false;

        private static void SetP3Timer()
        {
            aTimer = new System.Timers.Timer(P3_Time_max);
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
            //aTimer.Dispose();
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
                        }
                    }
                    else if (next_byte == 0)
                    {
                        msg_field_index = 0;
                    }
                    else 
                    {
                        BlockMessageInProcess.ClearBlockMessage();
                        MSG_A1A0_MODE mode_info = (MSG_A1A0_MODE)((next_byte & 0xc0) >> 6);
                        switch (mode_info)
                        {
                            case MSG_A1A0_MODE.NO_ADDRESS_INFO:      // No Address Information
                                if ((next_byte & 0x3f) == 0)
                                {
                                    Format_ID = FORMAT_ID.ID3;
                                    ProcessFormat3(next_byte);
                                }
                                else
                                {
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
                                     Format_ID = FORMAT_ID.ID4;
                                    ProcessFormat4(next_byte);
                                }
                                else
                                {
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

        public BlockMessage GetProcessedBlockMessage() { return BlockMessageInProcess; }

     }
}
