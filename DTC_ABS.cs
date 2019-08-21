using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTC_ABS
{
    enum ABS_DTC_Code
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

    class CMD_E_ABS_DTC
    {
        private uint byte_index;
        private uint bit_index;
        private string failure_type;
        private string description;
        private ABS_DTC_Code dtc_code;

        public CMD_E_ABS_DTC(uint byte_idx, uint bit_idx, ABS_DTC_Code DTC_value, string type, string desc)
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
        public ABS_DTC_Code DTC
        {
            get { return dtc_code; }
        }
    }

    static class ABS_DTC_Table
    {
        static List<CMD_E_ABS_DTC> abs_dtc_table = new List<CMD_E_ABS_DTC>();

        static ABS_DTC_Table()
        {
            abs_dtc_table.Add(new CMD_E_ABS_DTC(0, 0, ABS_DTC_Code.ECU_Control_unit_failure,
                    "ECU", "Control Unit Failure"));
            abs_dtc_table.Add(new CMD_E_ABS_DTC(0, 1, ABS_DTC_Code.VR_Valve_Relay_Fault,
                    "VR", "Valve Replay Fault"));
            abs_dtc_table.Add(new CMD_E_ABS_DTC(0, 2, ABS_DTC_Code.Valves_EV_Inlet_value_Failure_F,
                    "Valves EV", "Inlet Valve Failure - Front"));
            abs_dtc_table.Add(new CMD_E_ABS_DTC(0, 3, ABS_DTC_Code.Valves_EV_Inlet_value_Failure_R,
                    "Valves EV", "Inlet Valve Failure - Rear"));
            abs_dtc_table.Add(new CMD_E_ABS_DTC(0, 4, ABS_DTC_Code.Valves_AV_Outlet_value_Failure_F,
                    "Valves AV", "Outlet Valve Failure - Front"));
            abs_dtc_table.Add(new CMD_E_ABS_DTC(0, 5, ABS_DTC_Code.Valves_AV_Outlet_value_Failure_R,
                    "Valves AV", "Outlet Valve Failure - Rear"));
            abs_dtc_table.Add(new CMD_E_ABS_DTC(0, 6, ABS_DTC_Code.UZ_Batter_Voltage_fault_Over_Voltage,
                    "UZ", "Battery Voltage Fault (Over-Voltage)"));
            abs_dtc_table.Add(new CMD_E_ABS_DTC(0, 7, ABS_DTC_Code.UZ_Batter_Voltage_fault_Under_Voltage,
                    "UZ", "Battery Voltage Fault (Under-Voltage)"));
            abs_dtc_table.Add(new CMD_E_ABS_DTC(1, 0, ABS_DTC_Code.RFP_Pump_Motor_Failure,
                    "RFP/RFP_HW", "Pump Motor Failure"));
            abs_dtc_table.Add(new CMD_E_ABS_DTC(1, 1, ABS_DTC_Code.WSS_ohmic_WSS_ohmic_failure_F,
                    "WSS_Ohmic", "WSS ohmic Failure - Front"));
            abs_dtc_table.Add(new CMD_E_ABS_DTC(1, 2, ABS_DTC_Code.WSS_ohmic_WSS_ohmic_failure_R,
                    "WSS_Ohmic", "WSS ohmic Failure - Rear"));
            abs_dtc_table.Add(new CMD_E_ABS_DTC(1, 3, ABS_DTC_Code.WSS_plausibility_failure_F,
                    "WSS_Plausibility", "WSS Plausibility Failure - Front"));
            abs_dtc_table.Add(new CMD_E_ABS_DTC(1, 4, ABS_DTC_Code.WSS_plausibility_failure_R,
                    "WSS_Plausibility", "WSS Plausibility Failure - Rear"));
            abs_dtc_table.Add(new CMD_E_ABS_DTC(1, 5, ABS_DTC_Code.WSS_generic_failure,
                    "WSS_Generic", "WSS Generic Failure"));
        }

        static public CMD_E_ABS_DTC Find_ABS_DTC(ABS_DTC_Code code)
        {
            foreach (CMD_E_ABS_DTC item in abs_dtc_table)
            {
                if (item.DTC == code)
                    return item;
            }
            return null;
        }

        static public CMD_E_ABS_DTC Find_ABS_DTC(int index)
        {
            if ((index>=0)&&(index < abs_dtc_table.Count))
                return abs_dtc_table.ElementAt(index);
            else
                return null;
        }

        static public CMD_E_ABS_DTC Find_ABS_DTC(uint byte_idx, uint bit_idx)
        {
            foreach (CMD_E_ABS_DTC item in abs_dtc_table)
            {
                if ((item.ByteIndex == byte_idx)&&(item.BitIndex == bit_idx))
                    return item;
            }
            return null;
        }
        static public int Count()
        {
            return abs_dtc_table.Count();
        }
    }
}
