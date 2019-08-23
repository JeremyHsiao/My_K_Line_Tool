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
                    "Engine idling control", "Vehicle Speed Sensor Intermittent/Erratic/High"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 1, OBD_DTC_Code.C0083,
                    "Chassis", "Tire Pressure Monitor Malfunction Indicator (Subfault)"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 2, OBD_DTC_Code.C0085,
                    "Chassis", "Traction Disable Indicator (Subfault)"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 3, OBD_DTC_Code.P0105,
                    "air/fuel mixture control", "Manifold Absolute Pressure/Barometric Pressure Circuit Malfunction"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 4, OBD_DTC_Code.P0110,
                    "air/fuel mixture control", "Intake Air Temperature Circuit Malfunction"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 5, OBD_DTC_Code.P0115,
                    "air/fuel mixture control", "Engine Coolant Temperature Circuit Malfunction"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 6, OBD_DTC_Code.P0120,
                    "air/fuel mixture control", "Throttle Pedal Position Sensor/Switch A Circuit Malfunction"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 7, OBD_DTC_Code.P0130,
                    "air/fuel mixture control", "O2 Sensor Circuit Malfunction (Bank 2 Sensor 1)"));

            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 0, OBD_DTC_Code.P0135,
                    "air/fuel mixture control", "O2 Sensor Heater Circuit Malfunction (Bank 2 Sensor 1)"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 1, OBD_DTC_Code.P0150,
                    "air/fuel mixture control", "O2 Sensor Circuit Malfunction (Bank 2 Sensor 1)"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 2, OBD_DTC_Code.P0155,
                    "air/fuel mixture control", "O2 Sensor Heater Circuit Malfunction (Bank 2 Sensor 1)"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 3, OBD_DTC_Code.P0201,
                    "air/fuel mixture control", "Injector Circuit Malfunction - Cylinder 1"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 4, OBD_DTC_Code.P0202,
                    "air/fuel mixture control", "Injector Circuit Malfunction - Cylinder 2"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.P0217,
                    "air/fuel mixture control", "Engine Overtemp Condition"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 6, OBD_DTC_Code.P0230,
                    "air/fuel mixture control", "Fuel Pump Primary Circuit Malfunction"));
            // Failure Type and Description needs to be updated from here
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 7, OBD_DTC_Code.P0335,
                    "WSS_Generic", "WSS Generic Failure"));

            obd_dtc_table.Add(new CMD_F_OBD_DTC(2, 0, OBD_DTC_Code.P0336,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(2, 1, OBD_DTC_Code.P0351,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(2, 2, OBD_DTC_Code.P0352,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(2, 3, OBD_DTC_Code.P0410,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(2, 4, OBD_DTC_Code.P0480,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(2, 5, OBD_DTC_Code.P0500,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(2, 6, OBD_DTC_Code.P0501,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(2, 7, OBD_DTC_Code.P0505,
                    "WSS_Generic", "WSS Generic Failure"));

            obd_dtc_table.Add(new CMD_F_OBD_DTC(3, 0, OBD_DTC_Code.P0512,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(3, 1, OBD_DTC_Code.P0560,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(3, 2, OBD_DTC_Code.P0601,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(3, 3, OBD_DTC_Code.P0604,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(3, 4, OBD_DTC_Code.P0605,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(3, 5, OBD_DTC_Code.P0606,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(3, 6, OBD_DTC_Code.P0620_PIN2,
                    "Onboard computer and ancillary outputs", "Generator Control Circuit Malfunction"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(3, 7, OBD_DTC_Code.P0620_PIN31,
                    "Onboard computer and ancillary outputs", "Generator Control Circuit Malfunction"));

            obd_dtc_table.Add(new CMD_F_OBD_DTC(4, 0, OBD_DTC_Code.P0650,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(4, 1, OBD_DTC_Code.P0655,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(4, 2, OBD_DTC_Code.P0A0F,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(4, 3, OBD_DTC_Code.P1300,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(4, 4, OBD_DTC_Code.P1310,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(4, 5, OBD_DTC_Code.P1536,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(4, 6, OBD_DTC_Code.P1607,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(4, 7, OBD_DTC_Code.P1800,
                    "WSS_Generic", "WSS Generic Failure"));

            obd_dtc_table.Add(new CMD_F_OBD_DTC(5, 0, OBD_DTC_Code.P2158,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(5, 1, OBD_DTC_Code.P2600,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(5, 2, OBD_DTC_Code.U0001,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(5, 3, OBD_DTC_Code.U0002,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(5, 4, OBD_DTC_Code.U0121,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(5, 5, OBD_DTC_Code.U0122,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(5, 6, OBD_DTC_Code.U0128,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(5, 7, OBD_DTC_Code.U0140,
                    "WSS_Generic", "WSS Generic Failure"));

            obd_dtc_table.Add(new CMD_F_OBD_DTC(6, 0, OBD_DTC_Code.U0426,
                    "WSS_Generic", "WSS Generic Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(6, 1, OBD_DTC_Code.U0486,
                    "WSS_Generic", "WSS Generic Failure"));
        }

        static public CMD_F_OBD_DTC Find_OBD_DTC(OBD_DTC_Code code)
        {
            foreach (CMD_F_OBD_DTC item in obd_dtc_table)
            {
                if (item.DTC == code)
                    return item;
            }
            return null;
        }

        static public CMD_F_OBD_DTC Find_OBD_DTC(int index)
        {
            if ((index>=0)&&(index < obd_dtc_table.Count))
                return obd_dtc_table.ElementAt(index);
            else
                return null;
        }

        static public CMD_F_OBD_DTC Find_OBD_DTC(uint byte_idx, uint bit_idx)
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

        enum DTC_Prefix
        {
            P = 0x0000,
            C = 0x4000,
            B = 0x8000,
            U = 0xC000
        }

        static public OBD_DTC_Code GetDTCValueFromString(String dtc_code_str)
        {
            uint dtc_value = 0;

            dtc_code_str.ToUpper();
            char chr = dtc_code_str.ElementAt(0);
            switch (chr)
            {
                case 'P':
                    dtc_value |= (uint)DTC_Prefix.P;
                    break;
                case 'C':
                    dtc_value |= (uint)DTC_Prefix.C;
                    break;
                case 'B':
                    dtc_value |= (uint)DTC_Prefix.B;
                    break;
                case 'U':
                    dtc_value |= (uint)DTC_Prefix.U;
                    break;
            }
            dtc_code_str = dtc_code_str.Substring(1, dtc_code_str.Length - 1);
            dtc_value |= (Convert.ToUInt32(dtc_code_str, 16)&0x3fff);

            return (OBD_DTC_Code)dtc_value;
        }

        static public String GetDTCStringFromValue(uint dtc_value)
        {
            String ret_str = "";
            dtc_value &= 0xffff;

            switch ((DTC_Prefix)(dtc_value & 0xC000))
            {
                case DTC_Prefix.P:
                    ret_str = "P";
                    break;
                case DTC_Prefix.C:
                    ret_str = "C";
                    break;
                case DTC_Prefix.B:
                    ret_str = "B";
                    break;
                case DTC_Prefix.U:
                    ret_str = "U";
                    break;
            }
            ret_str += (dtc_value & 0x3fff).ToString("X4");

            return ret_str;
        }

        static public String GetDTCStringFromValue(OBD_DTC_Code dtc_code)
        {
            return GetDTCStringFromValue((uint)dtc_code);
        }
    }
}
