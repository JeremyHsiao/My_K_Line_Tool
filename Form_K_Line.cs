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

        private void Tmr_FetchingUARTInput_Tick(object sender, EventArgs e)
        {
            // Regularly polling request message
            while (MySerialPort.KLineBlockMessageList.Count()>0)
            {
                BlockMessage message = MySerialPort.KLineBlockMessageList[0];
                MySerialPort.KLineBlockMessageList.RemoveAt(0);
                String message_in_string = MySerialPort.KLineBlockMessageInStringList[0];
                MySerialPort.KLineBlockMessageInStringList.RemoveAt(0);
                rtbKLineData.AppendText(message_in_string + "\n" );

                if(message.GetTA()==0x10)       // ABS in fmt 2 out fmt 4
                {
                    BlockMessageForSerialOutput out_str_proc = new BlockMessageForSerialOutput();
                    List<byte> output_data = new List<byte>();

                    // Force to use format 4
                    List<byte> return_data = new List<byte>();
                    return_data.Add(0xEF);
                    return_data.Add(0x8F);
                    out_str_proc.GenerateSerialOutput(out output_data, message.GetSA(), message.GetTA(), (byte)(message.GetSID()|0x40), return_data, true); // with extra length byt
                    MySerialPort.SendToSerial(output_data.ToArray());
                    message_in_string = out_str_proc.GetSerialOutputString();
                    rtbKLineData.AppendText(message_in_string + "\n");
                }
                else if (message.GetTA() == 0x28)       // OBD in fmt 2 out fmt 2
                {
                    BlockMessageForSerialOutput out_str_proc = new BlockMessageForSerialOutput();
                    List<byte> output_data = new List<byte>();

                    // Force to use format 4
                    List<byte> return_data = new List<byte>();
                    return_data.Add(0xEF);
                    return_data.Add(0x8F);
                    out_str_proc.GenerateSerialOutput(out output_data, message.GetSA(), message.GetTA(), (byte)(message.GetSID() | 0x40), return_data, false); // no extra length byte
                    MySerialPort.SendToSerial(output_data.ToArray());
                    message_in_string = out_str_proc.GetSerialOutputString();
                    rtbKLineData.AppendText(message_in_string + "\n");
                }
            }
        }
    }
}
