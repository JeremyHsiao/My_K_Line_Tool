using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTC_OBD
{
    enum OBD_DTC_Code
    {
        ECU_Control_unit_failure = 0x5055,
        VR_Valve_Relay_Fault = 0x5019,
        Valves_EV_Inlet_value_Failure_F = 0x5017,
        Valves_EV_Inlet_value_Failure_R = 0x5013,
        Valves_AV_Outlet_value_Failure_F = 0x5018,
        Valves_AV_Outlet_value_Failure_R = 0x5014,
        UZ_Batter_Voltage_fault_Over_Voltage = 0x5053,
        UZ_Batter_Voltage_fault_Under_Voltage = 0x5052,
        RFP_Pump_Motor_Failure = 0x5035,
        WSS_ohmic_WSS_ohmic_failure_F = 0x5043,
        WSS_ohmic_WSS_ohmic_failure_R = 0x5045,
        WSS_plausibility_failure_F = 0x5042,
        WSS_plausibility_failure_R = 0x5044,
        WSS_generic_failure = 0x5025,
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
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 0, OBD_DTC_Code.ECU_Control_unit_failure,
                    "ECU", "Control Unit Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 1, OBD_DTC_Code.VR_Valve_Relay_Fault,
                    "VR", "Valve Replay Fault"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 2, OBD_DTC_Code.Valves_EV_Inlet_value_Failure_F,
                    "Valves EV", "Inlet Valve Failure - Front"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 3, OBD_DTC_Code.Valves_EV_Inlet_value_Failure_R,
                    "Valves EV", "Inlet Valve Failure - Rear"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 4, OBD_DTC_Code.Valves_AV_Outlet_value_Failure_F,
                    "Valves AV", "Outlet Valve Failure - Front"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 5, OBD_DTC_Code.Valves_AV_Outlet_value_Failure_R,
                    "Valves AV", "Outlet Valve Failure - Rear"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 6, OBD_DTC_Code.UZ_Batter_Voltage_fault_Over_Voltage,
                    "UZ", "Battery Voltage Fault (Over-Voltage)"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(0, 7, OBD_DTC_Code.UZ_Batter_Voltage_fault_Under_Voltage,
                    "UZ", "Battery Voltage Fault (Under-Voltage)"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 0, OBD_DTC_Code.RFP_Pump_Motor_Failure,
                    "RFP/RFP_HW", "Pump Motor Failure"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 1, OBD_DTC_Code.WSS_ohmic_WSS_ohmic_failure_F,
                    "WSS_Ohmic", "WSS ohmic Failure - Front"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 2, OBD_DTC_Code.WSS_ohmic_WSS_ohmic_failure_R,
                    "WSS_Ohmic", "WSS ohmic Failure - Rear"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 3, OBD_DTC_Code.WSS_plausibility_failure_F,
                    "WSS_Plausibility", "WSS Plausibility Failure - Front"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 4, OBD_DTC_Code.WSS_plausibility_failure_R,
                    "WSS_Plausibility", "WSS Plausibility Failure - Rear"));
            obd_dtc_table.Add(new CMD_F_OBD_DTC(1, 5, OBD_DTC_Code.WSS_generic_failure,
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
