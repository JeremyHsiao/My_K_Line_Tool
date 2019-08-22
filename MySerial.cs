using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;
using BlockMessageLibrary;

namespace MySerialLibrary
{
    class MySerial : IDisposable
    {
        // static member/function to shared aross all MySerial
        static protected Dictionary<string, Object> MySerialDictionary = new Dictionary<string, Object>();

        // Private member
        private SerialPort _serialPort;

        //
        // public functions
        //
        enum BAUD_RATE_LIST
        {
            BR_9600 = 9600,
            BR_K_Line = 10400,
            BR_115200 = 115200,
            BR_230400 = 230400,
        };

        enum RX_PROCESSOR
        {
            ENQUEUE = 0,
            READLINE,
            K_LINE
        }

        private RX_PROCESSOR Rx_Processor_Selection = RX_PROCESSOR.K_LINE;              // ENQUEUE

        public const int Serial_BaudRate = (int)BAUD_RATE_LIST.BR_K_Line;  // BAUD_RATE_LIST.BR_115200;
        public const Parity Serial_Parity = Parity.None;
        public const int Serial_DataBits = 8;
        public const StopBits Serial_StopBits = StopBits.One;

        public MySerial(string com_port) { _serialPort = new SerialPort(com_port, Serial_BaudRate, Serial_Parity, Serial_DataBits, Serial_StopBits); }
        public string GetPortName() { return _serialPort.PortName; }

        public MySerial()
        {
            _serialPort = new SerialPort
            {
                BaudRate = Serial_BaudRate,
                Parity = Serial_Parity,
                DataBits = Serial_DataBits,
                StopBits = Serial_StopBits
            };
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources
                _serialPort.Close();
            }
            // free native resources
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        static public List<string> FindAllSerialPort()
        {
            List<string> ListSerialPort = new List<string>();

            foreach (string comport_s in SerialPort.GetPortNames())
            {
                ListSerialPort.Add(comport_s);
            }

            return ListSerialPort;
        }


        public Boolean OpenPort()
        {
            Boolean bRet = false;
            _serialPort.Handshake = Handshake.None;
            _serialPort.Encoding = Encoding.UTF8;
            _serialPort.ReadTimeout = 1000;
            _serialPort.WriteTimeout = 1000;
            switch (Rx_Processor_Selection)
            {
                case RX_PROCESSOR.ENQUEUE:
                    _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                    break;
                case RX_PROCESSOR.K_LINE:
                    _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler_KLine);
                    break;
                case RX_PROCESSOR.READLINE:
                    // To-be-added.
                    break;
            }

            try
            {
                _serialPort.Open();
                Start_SerialReadThread();
                MySerialDictionary.Add(_serialPort.PortName, this);
                bRet = true;
            }
            catch (Exception ex232)
            {
                Console.WriteLine("MySerial_OpenPort Exception at PORT: " + _serialPort.PortName + " - " + ex232);
                bRet = false;
            }
            return bRet;
        }

        public Boolean OpenPort(string PortName)
        {
            Boolean bRet = false;
            _serialPort.PortName = PortName;
            bRet = OpenPort();
            return bRet;
        }

        public Boolean ClosePort()
        {
            Boolean bRet = false;
            MySerialDictionary.Remove(_serialPort.PortName);

            try
            {
                Stop_SerialReadThread();
                _serialPort.Close();
                bRet = true;
            }
            catch (Exception ex232)
            {
                Console.WriteLine("Serial_ClosePort Exception at PORT: " + _serialPort.PortName + " - " + ex232);
                bRet = false;
            }
            return bRet;
        }

        public Boolean IsPortOpened()
        {
            Boolean bRet = false;
            //if ((_serialPort.IsOpen == true) && (readThread.IsAlive))
            if (_serialPort.IsOpen == true)
            {
                bRet = true;
            }
            return bRet;
        }

        //
        // Start of read part
        //

        private Queue<byte> Rx_byte_buffer_QUEUE = new Queue<byte>();
        private Queue<string> UART_READ_MSG_QUEUE = new Queue<string>();
        public Queue<string> LOG_QUEUE = new Queue<string>();

        public byte GetRxByte() { byte ret_byte = Rx_byte_buffer_QUEUE.Dequeue(); return ret_byte; }
        public bool IsRxEmpty() { return (Rx_byte_buffer_QUEUE.Count <= 0) ? true : false; }

        public bool GetBreakState () { return _serialPort.BreakState; }

        private void Start_SerialReadThread()
        {
            LOG_QUEUE.Clear();
            UART_READ_MSG_QUEUE.Clear();
            Rx_byte_buffer_QUEUE.Clear();
        }

        private void Stop_SerialReadThread()
        {
            LOG_QUEUE.Clear();
            UART_READ_MSG_QUEUE.Clear();
            Rx_byte_buffer_QUEUE.Clear();
        }

        // This Handler is for reading all input without wating for a whole line
        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            // Find out which serial port --> which myserial
            SerialPort sp = (SerialPort)sender;
            MySerialDictionary.TryGetValue(sp.PortName, out Object myserial_serial_obj);
            MySerial myserial = (MySerial)myserial_serial_obj;
            //Rx_char_buffer_QUEUE
            int buf_len = sp.BytesToRead;
            if (buf_len > 0)
            {
                // Read in all char
                byte[] input_buf = new byte[buf_len];
                sp.Read(input_buf, 0, buf_len);
                {
                    int ch_index = 0;
                    while (ch_index < buf_len)
                    {
                        byte byte_data = input_buf[ch_index];
                        myserial.Rx_byte_buffer_QUEUE.Enqueue(byte_data);
                        ch_index++;
                    }
                }
            }
        }

        //
        // K-line portion of code
        //

        public List<BlockMessage> KLineBlockMessageList = new List<BlockMessage>();
        public List<String> KLineRawDataInStringList = new List<String>();
        private String RawDataInString = "";

        private ProcessBlockMessage KLineKWP2000Process = new ProcessBlockMessage();

        private bool ECU_filtering = false;
        private List<byte> ECU_data_to_be_filtered = new List<byte>();

        public void Enable_ECU_Filtering(bool enabled)
        {
            ECU_filtering = enabled;
        }

        public void Add_ECU_Filtering_Data(List<byte> filter_Data)
        {
            ECU_data_to_be_filtered.Clear();
            ECU_data_to_be_filtered.AddRange(filter_Data);
        }

        private static void DataReceivedHandler_KLine(object sender, SerialDataReceivedEventArgs e)
        {
            // Find out which serial port --> which myserial
            SerialPort sp = (SerialPort)sender;
            MySerialDictionary.TryGetValue(sp.PortName, out Object myserial_serial_obj);
            MySerial myserial = (MySerial)myserial_serial_obj;

            while ( sp.BytesToRead > 0 )
            {
                // Read in all char
                bool IsMessageReady = false;
                byte byte_data = (byte)sp.ReadByte();
                if (myserial.ECU_filtering == true)
                {
                    if(myserial.ECU_data_to_be_filtered.Count>0)
                    {
                        myserial.ECU_data_to_be_filtered.RemoveAt(0);
                    }
                    if(myserial.ECU_data_to_be_filtered.Count==0)
                    {
                        myserial.ECU_filtering = false;
                    }
                }
                else
                {
                    myserial.RawDataInString += byte_data.ToString("X2") + " ";
                    IsMessageReady = myserial.KLineKWP2000Process.ProcessNextByte(byte_data);
                    if (IsMessageReady)
                    {
                        BlockMessage new_message = myserial.KLineKWP2000Process.GetProcessedBlockMessage();
                        myserial.KLineBlockMessageList.Add(new_message);
                        myserial.KLineRawDataInStringList.Add(myserial.RawDataInString);
                        myserial.RawDataInString = "";
                        IsMessageReady = false;
                        //break;
                    }
                }
            }
        }

        //
        // END of K-line portion of code
        //

        //
        // End of read part
        //

        public bool SendToSerial(byte[] byte_to_sent)
        {
            bool return_value = false;

            if (_serialPort.IsOpen == true)
            {
                //Application.DoEvents();
                try
                {
                    int temp_index = 0;
                    const int fixed_length = 16;

                    while ((temp_index < byte_to_sent.Length) && (_serialPort.IsOpen == true))
                    {
                        if ((temp_index + fixed_length) < byte_to_sent.Length)
                        {
                            _serialPort.Write(byte_to_sent, temp_index, fixed_length);
                            temp_index += fixed_length;
                        }
                        else
                        {
                            _serialPort.Write(byte_to_sent, temp_index, (byte_to_sent.Length - temp_index));
                            temp_index = byte_to_sent.Length;
                        }
                    }
                    return_value = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("BlueRatSendToSerial - " + ex);
                    return_value = false;
                }
            }
            else
            {
                Console.WriteLine("COM is closed and cannot send byte data\n");
                return_value = false;
            }
            return return_value;
        }

        //
        // To process UART IO Exception
        //
        protected virtual void OnUARTException(EventArgs e)
        {
            UARTException?.Invoke(this, e);
        }

        public event EventHandler UARTException;


    }
}
