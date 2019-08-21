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

        private const int min_delay_before_response = 20;

        private void Tmr_FetchingUARTInput_Tick(object sender, EventArgs e)
        {
            String current_time_str;

            // Regularly polling request message
            while (MySerialPort.KLineBlockMessageList.Count()>0)
            {
                current_time_str = DateTime.Now.ToString("[HH:mm:ss.fff] ");
                BlockMessage message = MySerialPort.KLineBlockMessageList[0];
                MySerialPort.KLineBlockMessageList.RemoveAt(0);
                String raw_data_in_string = MySerialPort.KLineRawDataInStringList[0];
                MySerialPort.KLineRawDataInStringList.RemoveAt(0);
                rtbKLineData.AppendText(current_time_str + raw_data_in_string + "\n");
                String message_in_string = MySerialPort.KLineBlockMessageInStringList[0];
                MySerialPort.KLineBlockMessageInStringList.RemoveAt(0);
                rtbKLineData.AppendText(current_time_str + message_in_string + "\n" );
                rtbKLineData.ScrollToCaret();

                BlockMessageForSerialOutput out_str_proc = new BlockMessageForSerialOutput();
                BlockMessage out_message = new BlockMessage();
                KWP_2000_Process kwp_2000_process = new KWP_2000_Process();
                List<byte> output_data = new List<byte>();

                kwp_2000_process.ProcessMessage(message, ref out_message);
                out_str_proc.GenerateSerialOutput(out output_data, out_message);

                MySerialPort.Add_ECU_Filtering_Data(output_data);
                MySerialPort.Enable_ECU_Filtering(true);
                Thread.Sleep((KWP_2000_Process.min_delay_before_response));
                current_time_str = DateTime.Now.ToString("[HH:mm:ss.fff] ");
                MySerialPort.SendToSerial(output_data.ToArray());
                message_in_string = out_str_proc.GetSerialOutputString();
                rtbKLineData.AppendText(current_time_str + message_in_string + "\n");
                rtbKLineData.ScrollToCaret();
            }
        }

        private void Tmr_Scan_ABS_DTC_Tick(object sender, EventArgs e)
        {

        }
    }
}
