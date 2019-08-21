using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTC_OBD
{
    // P: 0x00
    // C: 0x04
    // B: 0x08
    // U: 0x0C

    enum OBD_DTC_Code
    {
        P0503 = 0x0503,
        C0083 = 0x4083,
        C0085 = 0x4085,
        P0105 = 0x0105,
        P0110 = 0x0110,
        P0115 = 0x0115,
        P0120 = 0x0120,
        P0130 = 0x0130,

        P0135 = 0x0135,
        P0150 = 0x0150,
        P0155 = 0x0155,
        P0201 = 0x0201,
        P0202 = 0x0202,
        P0217 = 0x0217,
        P0230 = 0x0230,
        P0335 = 0x0335,

        P0336 = 0x0336,
        P0351 = 0x0351,
        P0352 = 0x0352,
        P0410 = 0x0410,
        P0480 = 0x0480,
        P0500 = 0x0500,
        P0501 = 0x0501,
        P0505 = 0x0505,

        P0512 = 0x0512,
        P0560 = 0x0560,
        P0601 = 0x0601,
        P0604 = 0x0604,
        P0605 = 0x0605,
        P0606 = 0x0606,
        P0620_PIN2 = 0x0620,
        P0620_PIN31 = 0x0620,

        P0650 = 0x0650,
        P0655 = 0x0655,
        P0A0F = 0x0A0F,
        P1300 = 0x1300,
        P1310 = 0x1310,
        P1536 = 0x1536,
        P1607 = 0x1607,
        P1800 = 0x1800,

        P2158 = 0x2158,
        P2600 = 0x2600,
        U0001 = 0xC001,
        U0002 = 0xC002,
        U0121 = 0xC121,
        U0122 = 0xC122,
        U0128 = 0xC128,
        U0140 = 0xC140,

        U0426 = 0xC426,
        U0486 = 0xC486,
        END
    }

    class CMD_F_OBD_DTC
    {
        private uint byte_index;
        private uint bit_index;
        private string failure_type;
        private string description;
        private OBD_DTC_Code dtc_code;

        public CMD_F_OBD_DTC(uint byte_idx, uint bit_idx, OBD_DTC_Code DTC_value, string type, string desc)
        {
            byte_index = byte_idx;
            bit_index = bit_idx;
            failure_type = type;
            description = desc;
            dtc_code = DTC_value;
        }

        public uint ByteIndex
        {
            get { return byte_index; }
        }
        public uint BitIndex
        {
            get { return bit_index; }
        }
        public string FailureType
        {
            get { return failure_type; }
        }
        public string Description
        {
            get { return failure_type; }
        }
        public OBD_DTC_Code DTC
        {
            get { return dtc_code; }
        }
    }

    static class OBD_DTC_Table
    {
        static List<CMD_F_OBD_DTC> obd_dtc_table = new List<CMD_F_OBD_DTC>();

        static OBD_DTC_Table()
        {
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 0, OBD_DTC_Code.P0503,
                    "Powertrain", "Vehicle Speed Sensor Intermittent/Erratic/High"));
            // Failure Type and Description needs to be updated from here
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 1, OBD_DTC_Code.C0083,
                    "Chassis", "Valve Replay Fault"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 2, OBD_DTC_Code.C0085,
                    "Chassis", "Inlet Valve Failure - Front"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 3, OBD_DTC_Code.P0105,
                    "Powertrain", "Inlet Valve Failure - Rear"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 4, OBD_DTC_Code.P0110,
                    "Valves AV", "Outlet Valve Failure - Front"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 5, OBD_DTC_Code.P0115,
                    "Valves AV", "Outlet Valve Failure - Rear"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 6, OBD_DTC_Code.P0120,
                    "UZ", "Battery Voltage Fault (Over-Voltage)"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 7, OBD_DTC_Code.P0130,
                    "UZ", "Battery Voltage Fault (Under-Voltage)"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 0, OBD_DTC_Code.P0135,
                    "RFP/RFP_HW", "Pump Motor Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 1, OBD_DTC_Code.P0150,
                    "WSS_Ohmic", "WSS ohmic Failure - Front"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 2, OBD_DTC_Code.P0155,
                    "WSS_Ohmic", "WSS ohmic Failure - Rear"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 3, OBD_DTC_Code.P0201,
                    "WSS_Plausibility", "WSS Plausibility Failure - Front"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 4, OBD_DTC_Code.P0202,
                    "WSS_Plausibility", "WSS Plausibility Failure - Rear"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0217,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0230,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0335,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0336,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0351,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0352,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0410,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0480,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0500,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0501,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0505,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0512,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0560,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0601,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0604,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0605,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0606,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0620_PIN2,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0620_PIN31,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0650,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0655,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0A0F,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P1300,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P1310,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P1536,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P1607,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P1800,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P2158,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P2600,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.U0001,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.U0002,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.U0121,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.U0122,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.U0128,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.U0140,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.U0426,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.U0486,
                    "WSS_Generic", "WSS Generic Failure"));
        }

        static public CMD_F_OBD_DTC Find_ABS_DTC(OBD_DTC_Code code)
        {
            foreach (CMD_F_OBD_DTC item in obd_dtc_table)
            {
                if (item.DTC == code)
                    return item;
            }
            return null;
        }

        static public CMD_F_OBD_DTC Find_ABS_DTC(int index)
        {
            if ((index>=0)&&(index < obd_dtc_table.Count))
                return obd_dtc_table.ElementAt(index);
            else
                return null;
        }

        static public CMD_F_OBD_DTC Find_ABS_DTC(uint byte_idx, uint bit_idx)
        {
            foreach (CMD_F_OBD_DTC item in obd_dtc_table)
            {
                if ((item.ByteIndex == byte_idx)&&(item.BitIndex == bit_idx))
                    return item;
            }
            return null;
        }

        static public int Count()
        {
            return OBD_DTC_Table.Count();
        }
    }
}
