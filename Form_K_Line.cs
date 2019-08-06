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

        //private void UpdateRCFunctionButtonAfterConnection()
        //{
        //    if ((MyBlueRat.CheckConnection() == true))
        //    {
        //        if ((RedRatData != null) && (RedRatData.SignalDB != null) && (RedRatData.SelectedDevice != null) && (RedRatData.SelectedSignal != null))
        //        {
        //            btnSingleRCPressed.Enabled = true;
        //        }
        //        else
        //        {
        //            btnSingleRCPressed.Enabled = false;
        //        }
        //        btnCheckHeartBeat.Enabled = true;
        //        btnStopRCButton.Enabled = true;
        //        btnRepeatRC.Enabled = true;
        //    }
        //}

        //private void UpdateRCFunctionButtonAfterDisconnection()
        //{
        //    btnSingleRCPressed.Enabled = false;
        //    btnCheckHeartBeat.Enabled = false;
        //    btnStopRCButton.Enabled = false;
        //}

        //private void UndoTemoparilyDisbleAllRCFunctionButtons()
        //{
        //    if ((RedRatData != null) && (RedRatData.SignalDB != null) && (RedRatData.SelectedDevice != null) && (RedRatData.SelectedSignal != null))
        //    {
        //        btnSingleRCPressed.Enabled = true;
        //    }
        //    else
        //    {
        //        btnSingleRCPressed.Enabled = false;
        //    }
        //    btnCheckHeartBeat.Enabled = true;
        //    btnStopRCButton.Enabled = true;
        //    btnRepeatRC.Enabled = true;
        //    btnConnectionControl.Enabled = true;
        //}

        //private void TemoparilyDisbleAllRCFunctionButtons()
        //{
        //    btnSingleRCPressed.Enabled = false;
        //    btnCheckHeartBeat.Enabled = false;
        //    btnStopRCButton.Enabled = false;
        //    btnRepeatRC.Enabled = false;
        //    btnConnectionControl.Enabled = false;
        //}

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
            bool IsMessageReady = false;
            // Regularly polling request message
            while (!MySerialPort.IsRxEmpty())
            {
                byte data = (byte) MySerialPort.GetRxByte();
                Console.WriteLine(data);
                IsMessageReady = MyProcessBlockMessage.ProcessNextByte(data);
                if(IsMessageReady)
                {
                    MyBlockMessageList.Add(MyProcessBlockMessage.GetBlockMessage());
                    IsMessageReady = false;
                }
            }
        }
    }
}
