using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_Line_Test
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

    enum MSG_STAGE_FORMAT_01
    {
        FMT = 0,
        SID = 1,
        Data = 2,
        CS = 3
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

        public BlockMessage()
        {
            msg_data = new List<byte>(); ClearBlockMessage();
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
                    Format_ID = FORMAT_ID.WAIT_FOR_ZERO;
                    msg_field_index++;
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
                    bRet = (current_checksum == next_byte) ? true : false;      // data available if checksum is ok
                    Format_ID = FORMAT_ID.WAIT_FOR_ZERO;
                    msg_field_index++;
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
                    Format_ID = FORMAT_ID.WAIT_FOR_ZERO;
                    msg_field_index++;
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
                    BlockMessageInProcess.SetLen(next_byte);
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
                    Format_ID = FORMAT_ID.WAIT_FOR_ZERO;
                    msg_field_index++;
                    break;
            }
            return bRet;
        }

        // Public function
        public ProcessBlockMessage() { BlockMessageInProcess = new BlockMessage(); StartNewProcess(); }

        public bool ProcessNextByte(byte next_byte)
        {
            bool bRet = false;

            switch(Format_ID)
            {
                case FORMAT_ID.WAIT_FOR_ZERO:
                    if(next_byte==0)
                    {
                        Format_ID = FORMAT_ID.NEW;
                        msg_field_index = 0;
                    }
                    break;
                case FORMAT_ID.NEW:
                    if (next_byte != 0)
                    {
                        byte mode_info = (byte)((next_byte & 0xc0) >> 6);
                        BlockMessageInProcess.ClearBlockMessage();
                        switch (mode_info)
                        {
                            case 0x00:
                                Format_ID = FORMAT_ID.ID1;
                                ProcessFormat1(next_byte);
                                break;
                            case 0x01:
                                // CARB mode - to be checked & implemented
                                Format_ID = FORMAT_ID.OUT_OF_RANGE;
                                break;
                            case 0x02:
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
                            case 0x03:
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
                    break;
            }
            return bRet;
        }

        public BlockMessage GetBlockMessage() { return BlockMessageInProcess; }
    }
}
