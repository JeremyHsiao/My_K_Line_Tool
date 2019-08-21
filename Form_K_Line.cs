using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySerialLibrary;
using BlockMessageLibrary;
using System.Threading;
using KWP_2000;
using DTC_ABS;
using DTC_OBD;

namespace K_Line_Test
{
    public partial class Form_K_Line : Form
    {

        private MySerial MySerialPort = new MySerial();
        private List<BlockMessage> MyBlockMessageList = new List<BlockMessage>();
        private ProcessBlockMessage MyProcessBlockMessage = new ProcessBlockMessage();

        public Form_K_Line()
        {
            InitializeComponent();
        }

        private void Serial_UpdatePortName()
        {
            lstSerialComPort.Items.Clear();

            foreach (string comport_s in MySerial.FindAllSerialPort())
            {
                lstSerialComPort.Items.Add(comport_s);
            }

            if (lstSerialComPort.Items.Count > 0)
            {
                lstSerialComPort.SelectedIndex = 0;     // this can be modified to preferred default
                EnableConnectButton();
                UpdateToConnectButton();
            }
            else
            {
                DisableConnectButton();
                UpdateToConnectButton();
            }
        }

        private void EnableRefreshCOMButton()
        {
            btnRefreshCOMNo.Enabled = true;
        }

        private void DisableRefreshCOMButton()
        {
            btnRefreshCOMNo.Enabled = false;
        }

        private void EnableConnectButton()
        {
            btnConnectionControl.Enabled = true;
        }

        private void DisableConnectButton()
        {
            btnConnectionControl.Enabled = false;
        }

        const string CONNECT_UART_STRING_ON_BUTTON = "Connect UART";
        const string DISCONNECT_UART_STRING_ON_BUTTON = "Disconnect UART";

        private void UpdateToConnectButton()
        {
            btnConnectionControl.Text = CONNECT_UART_STRING_ON_BUTTON;
        }

        private void UpdateToDisconnectButton()
        {
            btnConnectionControl.Text = DISCONNECT_UART_STRING_ON_BUTTON;
        }

        private void BtnRefreshCOMNo_Click(object sender, EventArgs e)
        {
            Serial_UpdatePortName();
        }

        private void btnConnectionControl_Click(object sender, EventArgs e)
        {
            if (btnConnectionControl.Text.Equals(CONNECT_UART_STRING_ON_BUTTON, StringComparison.Ordinal)) // Check if button is showing "Connect" at this moment.
            {   // User to connect
                string curItem = lstSerialComPort.SelectedItem.ToString();
                if (MySerialPort.OpenPort(curItem) == true)
                {
                    //BlueRat_UART_Exception_status = false;
                    UpdateToDisconnectButton();
                    DisableRefreshCOMButton();
                    tmr_FetchingUARTInput.Enabled = true;
                }
                else
                {
                    // Error message
                }
            }
            else
            {   // User to disconnect
                if (MySerialPort.ClosePort() == true)
                {
                    UpdateToConnectButton();
                    EnableRefreshCOMButton();
                    // if (BlueRat_UART_Exception_status) { Serial_UpdatePortName(); }
                    //BlueRat_UART_Exception_status = false;
                }
                else
                {
                    // Error message
                }
                tmr_FetchingUARTInput.Enabled = false;
            }
        }

        private void Use_Fixed_DTC_from_HQ(KWP_2000_Process kwp2000)
        {
            // byte[] fixed_response_data_abs = { 0x04, 0x50, 0x43, 0xE0, 0x50, 0x45, 0xE0, 0x50, 0x52, 0xA0, 0x50, 0x53, 0xA0 };
            kwp2000.ABS_DTC_Queue_Add(0x50, 0x43, 0xE0);
            kwp2000.ABS_DTC_Queue_Add(0x50, 0x45, 0xE0);
            kwp2000.ABS_DTC_Queue_Add(0x50, 0x52, 0xA0);
            kwp2000.ABS_DTC_Queue_Add(0x50, 0x53, 0xA0);
        }

        private void Use_Random_DTC(KWP_2000_Process kwp2000)
        {
            Random rs = new Random();
            Byte DTC_no = (byte)(rs.Next(10));
            DTC_no = (byte)((DTC_no <= KWP_2000_Process.ReadDiagnosticCodesByStatus_MaxNumberOfDTC) ? DTC_no : 0);
            for (int no = 0; no < DTC_no; no++)
            {
                CMD_E_ABS_DTC random_dtc = ABS_DTC_Table.Find_ABS_DTC(rs.Next(ABS_DTC_Table.Count()));
                kwp2000.ABS_DTC_Queue_Add(random_dtc, (byte)kwp2000.dtc_status_table[rs.Next(kwp2000.dtc_status_table.Length)]);
            }
        }

        private void Scan_DTC_from_UI(KWP_2000_Process kwp2000)
        {
            uint byte_bit_index = 0;

            foreach (var item in abs_lut)
            {
                if (item.Checked == true)
                {
                    // If CheckBox is checked, treat it as Lamp_ON_Failure_Set
                    kwp2000.ABS_DTC_Queue_Add(ABS_DTC_Table.Find_ABS_DTC((byte_bit_index / 8), (byte_bit_index%8)), KWP_2000_Process.Lamp_ON_Failure_Set);
                }
                byte_bit_index++;
            }

            foreach (var item in obd_lut)
            {
                if (item.Checked == true)
                {
                    // If CheckBox is checked, treat it as Lamp_ON_Failure_Set
                    kwp2000.OBD_DTC_Queue_Add(OBD_DTC_Table.Find_OBD_DTC((byte_bit_index / 8), (byte_bit_index % 8)), KWP_2000_Process.Lamp_ON_Failure_Set);
                }
                byte_bit_index++;
            }

        }

        private void Tmr_FetchingUARTInput_Tick(object sender, EventArgs e)
        {
            String current_time_str;

            // Regularly polling request message
            while (MySerialPort.KLineBlockMessageList.Count() > 0)
            {
                current_time_str = DateTime.Now.ToString("[HH:mm:ss.fff] ");
                BlockMessage message = MySerialPort.KLineBlockMessageList[0];
                MySerialPort.KLineBlockMessageList.RemoveAt(0);
                String raw_data_in_string = MySerialPort.KLineRawDataInStringList[0];
                MySerialPort.KLineRawDataInStringList.RemoveAt(0);
                rtbKLineData.AppendText(current_time_str + raw_data_in_string + "\n");
                String message_in_string = MySerialPort.KLineBlockMessageInStringList[0];
                MySerialPort.KLineBlockMessageInStringList.RemoveAt(0);
                rtbKLineData.AppendText(current_time_str + message_in_string + "\n");
                rtbKLineData.ScrollToCaret();

                BlockMessageForSerialOutput out_str_proc = new BlockMessageForSerialOutput();
                BlockMessage out_message = new BlockMessage();
                KWP_2000_Process kwp_2000_process = new KWP_2000_Process();
                //Use_Random_DTC(kwp_2000_process);  // Random Test
                //Use_Fixed_DTC_from_HQ(kwp_2000_process);  // Simulate response from a ECU device
                Scan_DTC_from_UI(kwp_2000_process);  // Scan Checkbox status and add DTC into queue
                kwp_2000_process.ProcessMessage(message, ref out_message);
                List<byte> output_data = new List<byte>();
                out_str_proc.GenerateSerialOutput(out output_data, out_message);

                MySerialPort.Add_ECU_Filtering_Data(output_data);
                MySerialPort.Enable_ECU_Filtering(true);
                Thread.Sleep((KWP_2000_Process.min_delay_before_response-1));
                current_time_str = DateTime.Now.ToString("[HH:mm:ss.fff] ");
                MySerialPort.SendToSerial(output_data.ToArray());
                message_in_string = out_str_proc.GetSerialOutputString();
                rtbKLineData.AppendText(current_time_str + message_in_string + "\n");
                rtbKLineData.ScrollToCaret();
            }
        }


        private List<CheckBox> abs_lut = new List<CheckBox>();
        private List<CheckBox> obd_lut = new List<CheckBox>();
        private void Form_K_Line_Load(object sender, EventArgs e)
        {
            // Generate abs_lut -- need to add CheckBox in sequence of byte/bit index from (0,0)
            abs_lut.Add(ABS_0x5055); //, ABS_DTC_Code.ECU_Control_unit_failure);
            abs_lut.Add(ABS_0x5019); //, ABS_DTC_Code.VR_Valve_Relay_Fault);
            abs_lut.Add(ABS_0x5017); //, ABS_DTC_Code.Valves_EV_Inlet_value_Failure_F);
            abs_lut.Add(ABS_0x5013); //, ABS_DTC_Code.Valves_EV_Inlet_value_Failure_R);
            abs_lut.Add(ABS_0x5018); //, ABS_DTC_Code.Valves_AV_Outlet_value_Failure_F);
            abs_lut.Add(ABS_0x5014); //, ABS_DTC_Code.Valves_AV_Outlet_value_Failure_R);
            abs_lut.Add(ABS_0x5053); //, ABS_DTC_Code.UZ_Batter_Voltage_fault_Over_Voltage);
            abs_lut.Add(ABS_0x5052); //, ABS_DTC_Code.UZ_Batter_Voltage_fault_Under_Voltage);
            // Byte (1,0)
            abs_lut.Add(ABS_0x5035); //, ABS_DTC_Code.RFP_Pump_Motor_Failure);
            abs_lut.Add(ABS_0x5043); //, ABS_DTC_Code.WSS_ohmic_WSS_ohmic_failure_F);
            abs_lut.Add(ABS_0x5045); //, ABS_DTC_Code.WSS_ohmic_WSS_ohmic_failure_R);
            abs_lut.Add(ABS_0x5042); //, ABS_DTC_Code.WSS_plausibility_failure_F);
            abs_lut.Add(ABS_0x5044); //, ABS_DTC_Code.WSS_plausibility_failure_R);
            abs_lut.Add(ABS_0x5025); //, ABS_DTC_Code.WSS_generic_failure);

            // Generate obd_lut -- need to add CheckBox in sequence of byte/bit index from (0,0)
            obd_lut.Add(OBD_P0503);
            obd_lut.Add(OBD_C0083);
            obd_lut.Add(OBD_C0085);
            obd_lut.Add(OBD_P0105);
            obd_lut.Add(OBD_P0110);
            obd_lut.Add(OBD_P0115);
            obd_lut.Add(OBD_P0120);
            obd_lut.Add(OBD_P0130);
            // Byte (1,0)
            obd_lut.Add(OBD_P0135);
            obd_lut.Add(OBD_P0150);
            obd_lut.Add(OBD_P0155);
            obd_lut.Add(OBD_P0201);
            obd_lut.Add(OBD_P0202);
            obd_lut.Add(OBD_P0217);
            obd_lut.Add(OBD_P0230);
            obd_lut.Add(OBD_P0335);
            // Byte (2,0)
            obd_lut.Add(OBD_P0336);
            obd_lut.Add(OBD_P0351);
            obd_lut.Add(OBD_P0352);
            obd_lut.Add(OBD_P0410);
            obd_lut.Add(OBD_P0480);
            obd_lut.Add(OBD_P0500);
            obd_lut.Add(OBD_P0501);
            obd_lut.Add(OBD_P0505);
            // Byte (3,0)
            obd_lut.Add(OBD_P0512);
            obd_lut.Add(OBD_P0560);
            obd_lut.Add(OBD_P0601);
            obd_lut.Add(OBD_P0604);
            obd_lut.Add(OBD_P0605);
            obd_lut.Add(OBD_P0606);
            obd_lut.Add(OBD_P0620_PIN2);
            obd_lut.Add(OBD_P0620_PIN31);
            // Byte (4,0)
            obd_lut.Add(OBD_P0650);
            obd_lut.Add(OBD_P0655);
            obd_lut.Add(OBD_P0A0F);
            obd_lut.Add(OBD_P1300);
            obd_lut.Add(OBD_P1310);
            obd_lut.Add(OBD_P1536);
            obd_lut.Add(OBD_P1607);
            obd_lut.Add(OBD_P1800);
            // Byte (5,0)
            obd_lut.Add(OBD_P2158);
            obd_lut.Add(OBD_P2600);
            obd_lut.Add(OBD_U0001);
            obd_lut.Add(OBD_U0002);
            obd_lut.Add(OBD_U0121);
            obd_lut.Add(OBD_U0122);
            obd_lut.Add(OBD_U0128);
            obd_lut.Add(OBD_U0140);
            // Byte (6,0)
            obd_lut.Add(OBD_U0426);
            obd_lut.Add(OBD_U0486);
        }
    }
}
