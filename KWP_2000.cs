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

        private BlockMessage InComingMessage = new BlockMessage();
        private BlockMessage ResponseMessage = new BlockMessage();

        public KWP_2000_Process()
        {
            InComingMessage.ClearBlockMessage();
            ResponseMessage.ClearBlockMessage();
        }

        public bool ProcessMessage(BlockMessage in_msg)
        {
            bool bRet = false;

            if (in_msg.GetTA() == ADDRESS_ABS)      
            {
                bRet = ProcessMessage_ABS(in_msg);
            }
            else if (in_msg.GetTA() == ADDRESS_OBD)      
            {
                bRet = ProcessMessage_OBD(in_msg);
            }
            return bRet;
        }

        public bool ProcessMessage_ABS(BlockMessage abs_msg)
        {
            bool bRet = false;

            switch((ENUM_SID)abs_msg.GetSID())
            {
                case ENUM_SID.ReadDiagnosticTroubleCodesByStatus:
                    break;
                case ENUM_SID.StartCommunication:
                    ResponseMessage.ClearBlockMessage();
                    //ResponseMessage.
                    break;
                case ENUM_SID.StopCommunication:
                    break;
                default:
                    break;
            }

            return bRet;
        }

        public bool ProcessMessage_OBD(BlockMessage obd_msg)
        {
            bool bRet = false;

            return bRet;
        }

    }
}
