namespace K_Line_Test
{
    partial class Form_K_Line
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnConnectionControl = new System.Windows.Forms.Button();
            this.lstSerialComPort = new System.Windows.Forms.ListBox();
            this.btnRefreshCOMNo = new System.Windows.Forms.Button();
            this.rtbKLineData = new System.Windows.Forms.RichTextBox();
            this.tmr_FetchingUARTInput = new System.Windows.Forms.Timer(this.components);
            this.Group_OBD_Error_Code = new System.Windows.Forms.GroupBox();
            this.OBD_U0486 = new System.Windows.Forms.CheckBox();
            this.OBD_U0426 = new System.Windows.Forms.CheckBox();
            this.OBD_U0122 = new System.Windows.Forms.CheckBox();
            this.OBD_U0121 = new System.Windows.Forms.CheckBox();
            this.OBD_U0002 = new System.Windows.Forms.CheckBox();
            this.OBD_U0140 = new System.Windows.Forms.CheckBox();
            this.OBD_U0001 = new System.Windows.Forms.CheckBox();
            this.OBD_U0128 = new System.Windows.Forms.CheckBox();
            this.OBD_P2600 = new System.Windows.Forms.CheckBox();
            this.OBD_P2158 = new System.Windows.Forms.CheckBox();
            this.OBD_P1800 = new System.Windows.Forms.CheckBox();
            this.OBD_P1607 = new System.Windows.Forms.CheckBox();
            this.OBD_P1536 = new System.Windows.Forms.CheckBox();
            this.OBD_P1310 = new System.Windows.Forms.CheckBox();
            this.OBD_P0620_PIN31 = new System.Windows.Forms.CheckBox();
            this.OBD_P1300 = new System.Windows.Forms.CheckBox();
            this.OBD_P0620_PIN2 = new System.Windows.Forms.CheckBox();
            this.OBD_P0A0F = new System.Windows.Forms.CheckBox();
            this.OBD_P0606 = new System.Windows.Forms.CheckBox();
            this.OBD_P0655 = new System.Windows.Forms.CheckBox();
            this.OBD_P0605 = new System.Windows.Forms.CheckBox();
            this.OBD_P0650 = new System.Windows.Forms.CheckBox();
            this.OBD_P0604 = new System.Windows.Forms.CheckBox();
            this.OBD_P0601 = new System.Windows.Forms.CheckBox();
            this.OBD_P0560 = new System.Windows.Forms.CheckBox();
            this.OBD_P0512 = new System.Windows.Forms.CheckBox();
            this.OBD_P0500 = new System.Windows.Forms.CheckBox();
            this.OBD_P0480 = new System.Windows.Forms.CheckBox();
            this.OBD_P0410 = new System.Windows.Forms.CheckBox();
            this.OBD_P0505 = new System.Windows.Forms.CheckBox();
            this.OBD_P0352 = new System.Windows.Forms.CheckBox();
            this.OBD_P0501 = new System.Windows.Forms.CheckBox();
            this.OBD_P0351 = new System.Windows.Forms.CheckBox();
            this.OBD_P0336 = new System.Windows.Forms.CheckBox();
            this.OBD_P0335 = new System.Windows.Forms.CheckBox();
            this.OBD_P0230 = new System.Windows.Forms.CheckBox();
            this.OBD_P0217 = new System.Windows.Forms.CheckBox();
            this.OBD_P0202 = new System.Windows.Forms.CheckBox();
            this.OBD_P0130 = new System.Windows.Forms.CheckBox();
            this.OBD_P0201 = new System.Windows.Forms.CheckBox();
            this.OBD_P0120 = new System.Windows.Forms.CheckBox();
            this.OBD_P0155 = new System.Windows.Forms.CheckBox();
            this.OBD_P0115 = new System.Windows.Forms.CheckBox();
            this.OBD_P0150 = new System.Windows.Forms.CheckBox();
            this.OBD_P0110 = new System.Windows.Forms.CheckBox();
            this.OBD_P0135 = new System.Windows.Forms.CheckBox();
            this.OBD_P0105 = new System.Windows.Forms.CheckBox();
            this.OBD_C0085 = new System.Windows.Forms.CheckBox();
            this.OBD_C0083 = new System.Windows.Forms.CheckBox();
            this.OBD_P0503 = new System.Windows.Forms.CheckBox();
            this.Group_ABS_Error_Code = new System.Windows.Forms.GroupBox();
            this.ABS_0x5025 = new System.Windows.Forms.CheckBox();
            this.ABS_0x5044 = new System.Windows.Forms.CheckBox();
            this.ABS_0x5052 = new System.Windows.Forms.CheckBox();
            this.ABS_0x5042 = new System.Windows.Forms.CheckBox();
            this.ABS_0x5053 = new System.Windows.Forms.CheckBox();
            this.ABS_0x5045 = new System.Windows.Forms.CheckBox();
            this.ABS_0x5014 = new System.Windows.Forms.CheckBox();
            this.ABS_0x5043 = new System.Windows.Forms.CheckBox();
            this.ABS_0x5018 = new System.Windows.Forms.CheckBox();
            this.ABS_0x5035 = new System.Windows.Forms.CheckBox();
            this.ABS_0x5013 = new System.Windows.Forms.CheckBox();
            this.ABS_0x5017 = new System.Windows.Forms.CheckBox();
            this.ABS_0x5019 = new System.Windows.Forms.CheckBox();
            this.ABS_0x5055 = new System.Windows.Forms.CheckBox();
            this.Group_DTC_data_option = new System.Windows.Forms.GroupBox();
            this.DTC_option_all_in_turn = new System.Windows.Forms.RadioButton();
            this.DTC_option_first_six = new System.Windows.Forms.RadioButton();
            this.tmr_Scan_ABS_DTC = new System.Windows.Forms.Timer(this.components);
            this.Group_OBD_Error_Code.SuspendLayout();
            this.Group_ABS_Error_Code.SuspendLayout();
            this.Group_DTC_data_option.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnectionControl
            // 
            this.btnConnectionControl.Enabled = false;
            this.btnConnectionControl.Location = new System.Drawing.Point(131, 19);
            this.btnConnectionControl.Name = "btnConnectionControl";
            this.btnConnectionControl.Size = new System.Drawing.Size(68, 46);
            this.btnConnectionControl.TabIndex = 21;
            this.btnConnectionControl.Text = "Connect UART";
            this.btnConnectionControl.UseVisualStyleBackColor = true;
            this.btnConnectionControl.Click += new System.EventHandler(this.btnConnectionControl_Click);
            // 
            // lstSerialComPort
            // 
            this.lstSerialComPort.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstSerialComPort.FormattingEnabled = true;
            this.lstSerialComPort.ItemHeight = 14;
            this.lstSerialComPort.Location = new System.Drawing.Point(67, 19);
            this.lstSerialComPort.Name = "lstSerialComPort";
            this.lstSerialComPort.Size = new System.Drawing.Size(58, 46);
            this.lstSerialComPort.TabIndex = 20;
            // 
            // btnRefreshCOMNo
            // 
            this.btnRefreshCOMNo.Location = new System.Drawing.Point(12, 19);
            this.btnRefreshCOMNo.Name = "btnRefreshCOMNo";
            this.btnRefreshCOMNo.Size = new System.Drawing.Size(49, 46);
            this.btnRefreshCOMNo.TabIndex = 19;
            this.btnRefreshCOMNo.Text = "Refresh COM";
            this.btnRefreshCOMNo.UseVisualStyleBackColor = true;
            this.btnRefreshCOMNo.Click += new System.EventHandler(this.BtnRefreshCOMNo_Click);
            // 
            // rtbKLineData
            // 
            this.rtbKLineData.Location = new System.Drawing.Point(12, 385);
            this.rtbKLineData.Name = "rtbKLineData";
            this.rtbKLineData.ReadOnly = true;
            this.rtbKLineData.Size = new System.Drawing.Size(760, 172);
            this.rtbKLineData.TabIndex = 22;
            this.rtbKLineData.Text = "";
            // 
            // tmr_FetchingUARTInput
            // 
            this.tmr_FetchingUARTInput.Interval = 28;
            this.tmr_FetchingUARTInput.Tick += new System.EventHandler(this.Tmr_FetchingUARTInput_Tick);
            // 
            // Group_OBD_Error_Code
            // 
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_U0486);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_U0426);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_U0122);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_U0121);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_U0002);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_U0140);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_U0001);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_U0128);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P2600);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P2158);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P1800);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P1607);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P1536);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P1310);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0620_PIN31);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P1300);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0620_PIN2);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0A0F);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0606);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0655);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0605);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0650);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0604);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0601);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0560);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0512);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0500);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0480);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0410);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0505);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0352);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0501);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0351);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0336);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0335);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0230);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0217);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0202);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0130);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0201);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0120);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0155);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0115);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0150);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0110);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0135);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0105);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_C0085);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_C0083);
            this.Group_OBD_Error_Code.Controls.Add(this.OBD_P0503);
            this.Group_OBD_Error_Code.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Group_OBD_Error_Code.Location = new System.Drawing.Point(322, 12);
            this.Group_OBD_Error_Code.Margin = new System.Windows.Forms.Padding(2);
            this.Group_OBD_Error_Code.Name = "Group_OBD_Error_Code";
            this.Group_OBD_Error_Code.Padding = new System.Windows.Forms.Padding(2);
            this.Group_OBD_Error_Code.Size = new System.Drawing.Size(451, 368);
            this.Group_OBD_Error_Code.TabIndex = 24;
            this.Group_OBD_Error_Code.TabStop = false;
            this.Group_OBD_Error_Code.Text = "OBD Status";
            // 
            // OBD_U0486
            // 
            this.OBD_U0486.AutoSize = true;
            this.OBD_U0486.Location = new System.Drawing.Point(369, 38);
            this.OBD_U0486.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_U0486.Name = "OBD_U0486";
            this.OBD_U0486.Size = new System.Drawing.Size(70, 20);
            this.OBD_U0486.TabIndex = 52;
            this.OBD_U0486.Text = "U0486";
            this.OBD_U0486.UseVisualStyleBackColor = true;
            // 
            // OBD_U0426
            // 
            this.OBD_U0426.AutoSize = true;
            this.OBD_U0426.Location = new System.Drawing.Point(369, 18);
            this.OBD_U0426.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_U0426.Name = "OBD_U0426";
            this.OBD_U0426.Size = new System.Drawing.Size(70, 20);
            this.OBD_U0426.TabIndex = 51;
            this.OBD_U0426.Text = "U0426";
            this.OBD_U0426.UseVisualStyleBackColor = true;
            // 
            // OBD_U0122
            // 
            this.OBD_U0122.AutoSize = true;
            this.OBD_U0122.Location = new System.Drawing.Point(254, 303);
            this.OBD_U0122.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_U0122.Name = "OBD_U0122";
            this.OBD_U0122.Size = new System.Drawing.Size(70, 20);
            this.OBD_U0122.TabIndex = 48;
            this.OBD_U0122.Text = "U0122";
            this.OBD_U0122.UseVisualStyleBackColor = true;
            // 
            // OBD_U0121
            // 
            this.OBD_U0121.AutoSize = true;
            this.OBD_U0121.Location = new System.Drawing.Point(254, 283);
            this.OBD_U0121.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_U0121.Name = "OBD_U0121";
            this.OBD_U0121.Size = new System.Drawing.Size(70, 20);
            this.OBD_U0121.TabIndex = 47;
            this.OBD_U0121.Text = "U0121";
            this.OBD_U0121.UseVisualStyleBackColor = true;
            // 
            // OBD_U0002
            // 
            this.OBD_U0002.AutoSize = true;
            this.OBD_U0002.Location = new System.Drawing.Point(254, 263);
            this.OBD_U0002.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_U0002.Name = "OBD_U0002";
            this.OBD_U0002.Size = new System.Drawing.Size(70, 20);
            this.OBD_U0002.TabIndex = 46;
            this.OBD_U0002.Text = "U0002";
            this.OBD_U0002.UseVisualStyleBackColor = true;
            // 
            // OBD_U0140
            // 
            this.OBD_U0140.AutoSize = true;
            this.OBD_U0140.Location = new System.Drawing.Point(254, 343);
            this.OBD_U0140.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_U0140.Name = "OBD_U0140";
            this.OBD_U0140.Size = new System.Drawing.Size(70, 20);
            this.OBD_U0140.TabIndex = 50;
            this.OBD_U0140.Text = "U0140";
            this.OBD_U0140.UseVisualStyleBackColor = true;
            // 
            // OBD_U0001
            // 
            this.OBD_U0001.AutoSize = true;
            this.OBD_U0001.Location = new System.Drawing.Point(254, 243);
            this.OBD_U0001.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_U0001.Name = "OBD_U0001";
            this.OBD_U0001.Size = new System.Drawing.Size(70, 20);
            this.OBD_U0001.TabIndex = 45;
            this.OBD_U0001.Text = "U0001";
            this.OBD_U0001.UseVisualStyleBackColor = true;
            // 
            // OBD_U0128
            // 
            this.OBD_U0128.AutoSize = true;
            this.OBD_U0128.Location = new System.Drawing.Point(254, 323);
            this.OBD_U0128.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_U0128.Name = "OBD_U0128";
            this.OBD_U0128.Size = new System.Drawing.Size(70, 20);
            this.OBD_U0128.TabIndex = 49;
            this.OBD_U0128.Text = "U0128";
            this.OBD_U0128.UseVisualStyleBackColor = true;
            // 
            // OBD_P2600
            // 
            this.OBD_P2600.AutoSize = true;
            this.OBD_P2600.Location = new System.Drawing.Point(254, 223);
            this.OBD_P2600.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P2600.Name = "OBD_P2600";
            this.OBD_P2600.Size = new System.Drawing.Size(67, 20);
            this.OBD_P2600.TabIndex = 44;
            this.OBD_P2600.Text = "P2600";
            this.OBD_P2600.UseVisualStyleBackColor = true;
            // 
            // OBD_P2158
            // 
            this.OBD_P2158.AutoSize = true;
            this.OBD_P2158.Location = new System.Drawing.Point(254, 203);
            this.OBD_P2158.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P2158.Name = "OBD_P2158";
            this.OBD_P2158.Size = new System.Drawing.Size(67, 20);
            this.OBD_P2158.TabIndex = 43;
            this.OBD_P2158.Text = "P2158";
            this.OBD_P2158.UseVisualStyleBackColor = true;
            // 
            // OBD_P1800
            // 
            this.OBD_P1800.AutoSize = true;
            this.OBD_P1800.Location = new System.Drawing.Point(254, 159);
            this.OBD_P1800.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P1800.Name = "OBD_P1800";
            this.OBD_P1800.Size = new System.Drawing.Size(67, 20);
            this.OBD_P1800.TabIndex = 42;
            this.OBD_P1800.Text = "P1800";
            this.OBD_P1800.UseVisualStyleBackColor = true;
            // 
            // OBD_P1607
            // 
            this.OBD_P1607.AutoSize = true;
            this.OBD_P1607.Location = new System.Drawing.Point(254, 139);
            this.OBD_P1607.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P1607.Name = "OBD_P1607";
            this.OBD_P1607.Size = new System.Drawing.Size(67, 20);
            this.OBD_P1607.TabIndex = 41;
            this.OBD_P1607.Text = "P1607";
            this.OBD_P1607.UseVisualStyleBackColor = true;
            // 
            // OBD_P1536
            // 
            this.OBD_P1536.AutoSize = true;
            this.OBD_P1536.Location = new System.Drawing.Point(254, 119);
            this.OBD_P1536.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P1536.Name = "OBD_P1536";
            this.OBD_P1536.Size = new System.Drawing.Size(67, 20);
            this.OBD_P1536.TabIndex = 40;
            this.OBD_P1536.Text = "P1536";
            this.OBD_P1536.UseVisualStyleBackColor = true;
            // 
            // OBD_P1310
            // 
            this.OBD_P1310.AutoSize = true;
            this.OBD_P1310.Location = new System.Drawing.Point(254, 99);
            this.OBD_P1310.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P1310.Name = "OBD_P1310";
            this.OBD_P1310.Size = new System.Drawing.Size(67, 20);
            this.OBD_P1310.TabIndex = 39;
            this.OBD_P1310.Text = "P1310";
            this.OBD_P1310.UseVisualStyleBackColor = true;
            // 
            // OBD_P0620_PIN31
            // 
            this.OBD_P0620_PIN31.AutoSize = true;
            this.OBD_P0620_PIN31.Location = new System.Drawing.Point(124, 344);
            this.OBD_P0620_PIN31.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0620_PIN31.Name = "OBD_P0620_PIN31";
            this.OBD_P0620_PIN31.Size = new System.Drawing.Size(115, 20);
            this.OBD_P0620_PIN31.TabIndex = 34;
            this.OBD_P0620_PIN31.Text = "P0620_PIN31";
            this.OBD_P0620_PIN31.UseVisualStyleBackColor = true;
            // 
            // OBD_P1300
            // 
            this.OBD_P1300.AutoSize = true;
            this.OBD_P1300.Location = new System.Drawing.Point(254, 79);
            this.OBD_P1300.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P1300.Name = "OBD_P1300";
            this.OBD_P1300.Size = new System.Drawing.Size(67, 20);
            this.OBD_P1300.TabIndex = 38;
            this.OBD_P1300.Text = "P1300";
            this.OBD_P1300.UseVisualStyleBackColor = true;
            // 
            // OBD_P0620_PIN2
            // 
            this.OBD_P0620_PIN2.AutoSize = true;
            this.OBD_P0620_PIN2.Location = new System.Drawing.Point(124, 324);
            this.OBD_P0620_PIN2.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0620_PIN2.Name = "OBD_P0620_PIN2";
            this.OBD_P0620_PIN2.Size = new System.Drawing.Size(107, 20);
            this.OBD_P0620_PIN2.TabIndex = 33;
            this.OBD_P0620_PIN2.Text = "P0620_PIN2";
            this.OBD_P0620_PIN2.UseVisualStyleBackColor = true;
            // 
            // OBD_P0A0F
            // 
            this.OBD_P0A0F.AutoSize = true;
            this.OBD_P0A0F.Location = new System.Drawing.Point(254, 59);
            this.OBD_P0A0F.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0A0F.Name = "OBD_P0A0F";
            this.OBD_P0A0F.Size = new System.Drawing.Size(70, 20);
            this.OBD_P0A0F.TabIndex = 37;
            this.OBD_P0A0F.Text = "P0A0F";
            this.OBD_P0A0F.UseVisualStyleBackColor = true;
            // 
            // OBD_P0606
            // 
            this.OBD_P0606.AutoSize = true;
            this.OBD_P0606.Location = new System.Drawing.Point(124, 304);
            this.OBD_P0606.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0606.Name = "OBD_P0606";
            this.OBD_P0606.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0606.TabIndex = 32;
            this.OBD_P0606.Text = "P0606";
            this.OBD_P0606.UseVisualStyleBackColor = true;
            // 
            // OBD_P0655
            // 
            this.OBD_P0655.AutoSize = true;
            this.OBD_P0655.Location = new System.Drawing.Point(254, 39);
            this.OBD_P0655.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0655.Name = "OBD_P0655";
            this.OBD_P0655.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0655.TabIndex = 36;
            this.OBD_P0655.Text = "P0655";
            this.OBD_P0655.UseVisualStyleBackColor = true;
            // 
            // OBD_P0605
            // 
            this.OBD_P0605.AutoSize = true;
            this.OBD_P0605.Location = new System.Drawing.Point(124, 284);
            this.OBD_P0605.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0605.Name = "OBD_P0605";
            this.OBD_P0605.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0605.TabIndex = 31;
            this.OBD_P0605.Text = "P0605";
            this.OBD_P0605.UseVisualStyleBackColor = true;
            // 
            // OBD_P0650
            // 
            this.OBD_P0650.AutoSize = true;
            this.OBD_P0650.Location = new System.Drawing.Point(254, 19);
            this.OBD_P0650.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0650.Name = "OBD_P0650";
            this.OBD_P0650.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0650.TabIndex = 35;
            this.OBD_P0650.Text = "P0650";
            this.OBD_P0650.UseVisualStyleBackColor = true;
            // 
            // OBD_P0604
            // 
            this.OBD_P0604.AutoSize = true;
            this.OBD_P0604.Location = new System.Drawing.Point(124, 264);
            this.OBD_P0604.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0604.Name = "OBD_P0604";
            this.OBD_P0604.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0604.TabIndex = 30;
            this.OBD_P0604.Text = "P0604";
            this.OBD_P0604.UseVisualStyleBackColor = true;
            // 
            // OBD_P0601
            // 
            this.OBD_P0601.AutoSize = true;
            this.OBD_P0601.Location = new System.Drawing.Point(124, 244);
            this.OBD_P0601.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0601.Name = "OBD_P0601";
            this.OBD_P0601.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0601.TabIndex = 29;
            this.OBD_P0601.Text = "P0601";
            this.OBD_P0601.UseVisualStyleBackColor = true;
            // 
            // OBD_P0560
            // 
            this.OBD_P0560.AutoSize = true;
            this.OBD_P0560.Location = new System.Drawing.Point(124, 223);
            this.OBD_P0560.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0560.Name = "OBD_P0560";
            this.OBD_P0560.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0560.TabIndex = 28;
            this.OBD_P0560.Text = "P0560";
            this.OBD_P0560.UseVisualStyleBackColor = true;
            // 
            // OBD_P0512
            // 
            this.OBD_P0512.AutoSize = true;
            this.OBD_P0512.Location = new System.Drawing.Point(124, 203);
            this.OBD_P0512.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0512.Name = "OBD_P0512";
            this.OBD_P0512.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0512.TabIndex = 27;
            this.OBD_P0512.Text = "P0512";
            this.OBD_P0512.UseVisualStyleBackColor = true;
            // 
            // OBD_P0500
            // 
            this.OBD_P0500.AutoSize = true;
            this.OBD_P0500.Location = new System.Drawing.Point(124, 119);
            this.OBD_P0500.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0500.Name = "OBD_P0500";
            this.OBD_P0500.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0500.TabIndex = 24;
            this.OBD_P0500.Text = "P0500";
            this.OBD_P0500.UseVisualStyleBackColor = true;
            // 
            // OBD_P0480
            // 
            this.OBD_P0480.AutoSize = true;
            this.OBD_P0480.Location = new System.Drawing.Point(124, 99);
            this.OBD_P0480.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0480.Name = "OBD_P0480";
            this.OBD_P0480.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0480.TabIndex = 23;
            this.OBD_P0480.Text = "P0480";
            this.OBD_P0480.UseVisualStyleBackColor = true;
            // 
            // OBD_P0410
            // 
            this.OBD_P0410.AutoSize = true;
            this.OBD_P0410.Location = new System.Drawing.Point(124, 79);
            this.OBD_P0410.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0410.Name = "OBD_P0410";
            this.OBD_P0410.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0410.TabIndex = 22;
            this.OBD_P0410.Text = "P0410";
            this.OBD_P0410.UseVisualStyleBackColor = true;
            // 
            // OBD_P0505
            // 
            this.OBD_P0505.AutoSize = true;
            this.OBD_P0505.Location = new System.Drawing.Point(124, 159);
            this.OBD_P0505.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0505.Name = "OBD_P0505";
            this.OBD_P0505.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0505.TabIndex = 26;
            this.OBD_P0505.Text = "P0505";
            this.OBD_P0505.UseVisualStyleBackColor = true;
            // 
            // OBD_P0352
            // 
            this.OBD_P0352.AutoSize = true;
            this.OBD_P0352.Location = new System.Drawing.Point(124, 59);
            this.OBD_P0352.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0352.Name = "OBD_P0352";
            this.OBD_P0352.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0352.TabIndex = 21;
            this.OBD_P0352.Text = "P0352";
            this.OBD_P0352.UseVisualStyleBackColor = true;
            // 
            // OBD_P0501
            // 
            this.OBD_P0501.AutoSize = true;
            this.OBD_P0501.Location = new System.Drawing.Point(124, 139);
            this.OBD_P0501.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0501.Name = "OBD_P0501";
            this.OBD_P0501.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0501.TabIndex = 25;
            this.OBD_P0501.Text = "P0501";
            this.OBD_P0501.UseVisualStyleBackColor = true;
            // 
            // OBD_P0351
            // 
            this.OBD_P0351.AutoSize = true;
            this.OBD_P0351.Location = new System.Drawing.Point(124, 39);
            this.OBD_P0351.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0351.Name = "OBD_P0351";
            this.OBD_P0351.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0351.TabIndex = 20;
            this.OBD_P0351.Text = "P0351";
            this.OBD_P0351.UseVisualStyleBackColor = true;
            // 
            // OBD_P0336
            // 
            this.OBD_P0336.AutoSize = true;
            this.OBD_P0336.Location = new System.Drawing.Point(124, 19);
            this.OBD_P0336.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0336.Name = "OBD_P0336";
            this.OBD_P0336.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0336.TabIndex = 19;
            this.OBD_P0336.Text = "P0336";
            this.OBD_P0336.UseVisualStyleBackColor = true;
            // 
            // OBD_P0335
            // 
            this.OBD_P0335.AutoSize = true;
            this.OBD_P0335.Location = new System.Drawing.Point(10, 343);
            this.OBD_P0335.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0335.Name = "OBD_P0335";
            this.OBD_P0335.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0335.TabIndex = 18;
            this.OBD_P0335.Text = "P0335";
            this.OBD_P0335.UseVisualStyleBackColor = true;
            // 
            // OBD_P0230
            // 
            this.OBD_P0230.AutoSize = true;
            this.OBD_P0230.Location = new System.Drawing.Point(10, 323);
            this.OBD_P0230.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0230.Name = "OBD_P0230";
            this.OBD_P0230.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0230.TabIndex = 17;
            this.OBD_P0230.Text = "P0230";
            this.OBD_P0230.UseVisualStyleBackColor = true;
            // 
            // OBD_P0217
            // 
            this.OBD_P0217.AutoSize = true;
            this.OBD_P0217.Location = new System.Drawing.Point(10, 303);
            this.OBD_P0217.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0217.Name = "OBD_P0217";
            this.OBD_P0217.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0217.TabIndex = 16;
            this.OBD_P0217.Text = "P0217";
            this.OBD_P0217.UseVisualStyleBackColor = true;
            // 
            // OBD_P0202
            // 
            this.OBD_P0202.AutoSize = true;
            this.OBD_P0202.Location = new System.Drawing.Point(10, 283);
            this.OBD_P0202.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0202.Name = "OBD_P0202";
            this.OBD_P0202.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0202.TabIndex = 15;
            this.OBD_P0202.Text = "P0202";
            this.OBD_P0202.UseVisualStyleBackColor = true;
            // 
            // OBD_P0130
            // 
            this.OBD_P0130.AutoSize = true;
            this.OBD_P0130.Location = new System.Drawing.Point(10, 160);
            this.OBD_P0130.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0130.Name = "OBD_P0130";
            this.OBD_P0130.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0130.TabIndex = 7;
            this.OBD_P0130.Text = "P0130";
            this.OBD_P0130.UseVisualStyleBackColor = true;
            // 
            // OBD_P0201
            // 
            this.OBD_P0201.AutoSize = true;
            this.OBD_P0201.Location = new System.Drawing.Point(10, 263);
            this.OBD_P0201.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0201.Name = "OBD_P0201";
            this.OBD_P0201.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0201.TabIndex = 14;
            this.OBD_P0201.Text = "P0201";
            this.OBD_P0201.UseVisualStyleBackColor = true;
            // 
            // OBD_P0120
            // 
            this.OBD_P0120.AutoSize = true;
            this.OBD_P0120.Location = new System.Drawing.Point(10, 140);
            this.OBD_P0120.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0120.Name = "OBD_P0120";
            this.OBD_P0120.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0120.TabIndex = 6;
            this.OBD_P0120.Text = "P0120";
            this.OBD_P0120.UseVisualStyleBackColor = true;
            // 
            // OBD_P0155
            // 
            this.OBD_P0155.AutoSize = true;
            this.OBD_P0155.Location = new System.Drawing.Point(10, 243);
            this.OBD_P0155.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0155.Name = "OBD_P0155";
            this.OBD_P0155.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0155.TabIndex = 13;
            this.OBD_P0155.Text = "P0155";
            this.OBD_P0155.UseVisualStyleBackColor = true;
            // 
            // OBD_P0115
            // 
            this.OBD_P0115.AutoSize = true;
            this.OBD_P0115.Location = new System.Drawing.Point(10, 120);
            this.OBD_P0115.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0115.Name = "OBD_P0115";
            this.OBD_P0115.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0115.TabIndex = 5;
            this.OBD_P0115.Text = "P0115";
            this.OBD_P0115.UseVisualStyleBackColor = true;
            // 
            // OBD_P0150
            // 
            this.OBD_P0150.AutoSize = true;
            this.OBD_P0150.Location = new System.Drawing.Point(10, 223);
            this.OBD_P0150.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0150.Name = "OBD_P0150";
            this.OBD_P0150.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0150.TabIndex = 12;
            this.OBD_P0150.Text = "P0150";
            this.OBD_P0150.UseVisualStyleBackColor = true;
            // 
            // OBD_P0110
            // 
            this.OBD_P0110.AutoSize = true;
            this.OBD_P0110.Location = new System.Drawing.Point(10, 100);
            this.OBD_P0110.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0110.Name = "OBD_P0110";
            this.OBD_P0110.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0110.TabIndex = 4;
            this.OBD_P0110.Text = "P0110";
            this.OBD_P0110.UseVisualStyleBackColor = true;
            // 
            // OBD_P0135
            // 
            this.OBD_P0135.AutoSize = true;
            this.OBD_P0135.Location = new System.Drawing.Point(10, 203);
            this.OBD_P0135.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0135.Name = "OBD_P0135";
            this.OBD_P0135.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0135.TabIndex = 11;
            this.OBD_P0135.Text = "P0135";
            this.OBD_P0135.UseVisualStyleBackColor = true;
            // 
            // OBD_P0105
            // 
            this.OBD_P0105.AutoSize = true;
            this.OBD_P0105.Location = new System.Drawing.Point(10, 80);
            this.OBD_P0105.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0105.Name = "OBD_P0105";
            this.OBD_P0105.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0105.TabIndex = 3;
            this.OBD_P0105.Text = "P0105";
            this.OBD_P0105.UseVisualStyleBackColor = true;
            // 
            // OBD_C0085
            // 
            this.OBD_C0085.AutoSize = true;
            this.OBD_C0085.Location = new System.Drawing.Point(10, 60);
            this.OBD_C0085.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_C0085.Name = "OBD_C0085";
            this.OBD_C0085.Size = new System.Drawing.Size(69, 20);
            this.OBD_C0085.TabIndex = 2;
            this.OBD_C0085.Text = "C0085";
            this.OBD_C0085.UseVisualStyleBackColor = true;
            // 
            // OBD_C0083
            // 
            this.OBD_C0083.AutoSize = true;
            this.OBD_C0083.Location = new System.Drawing.Point(10, 39);
            this.OBD_C0083.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_C0083.Name = "OBD_C0083";
            this.OBD_C0083.Size = new System.Drawing.Size(69, 20);
            this.OBD_C0083.TabIndex = 1;
            this.OBD_C0083.Text = "C0083";
            this.OBD_C0083.UseVisualStyleBackColor = true;
            // 
            // OBD_P0503
            // 
            this.OBD_P0503.AutoSize = true;
            this.OBD_P0503.Location = new System.Drawing.Point(10, 19);
            this.OBD_P0503.Margin = new System.Windows.Forms.Padding(2);
            this.OBD_P0503.Name = "OBD_P0503";
            this.OBD_P0503.Size = new System.Drawing.Size(67, 20);
            this.OBD_P0503.TabIndex = 0;
            this.OBD_P0503.Text = "P0503";
            this.OBD_P0503.UseVisualStyleBackColor = true;
            // 
            // Group_ABS_Error_Code
            // 
            this.Group_ABS_Error_Code.Controls.Add(this.ABS_0x5025);
            this.Group_ABS_Error_Code.Controls.Add(this.ABS_0x5044);
            this.Group_ABS_Error_Code.Controls.Add(this.ABS_0x5052);
            this.Group_ABS_Error_Code.Controls.Add(this.ABS_0x5042);
            this.Group_ABS_Error_Code.Controls.Add(this.ABS_0x5053);
            this.Group_ABS_Error_Code.Controls.Add(this.ABS_0x5045);
            this.Group_ABS_Error_Code.Controls.Add(this.ABS_0x5014);
            this.Group_ABS_Error_Code.Controls.Add(this.ABS_0x5043);
            this.Group_ABS_Error_Code.Controls.Add(this.ABS_0x5018);
            this.Group_ABS_Error_Code.Controls.Add(this.ABS_0x5035);
            this.Group_ABS_Error_Code.Controls.Add(this.ABS_0x5013);
            this.Group_ABS_Error_Code.Controls.Add(this.ABS_0x5017);
            this.Group_ABS_Error_Code.Controls.Add(this.ABS_0x5019);
            this.Group_ABS_Error_Code.Controls.Add(this.ABS_0x5055);
            this.Group_ABS_Error_Code.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Group_ABS_Error_Code.Location = new System.Drawing.Point(213, 12);
            this.Group_ABS_Error_Code.Margin = new System.Windows.Forms.Padding(2);
            this.Group_ABS_Error_Code.Name = "Group_ABS_Error_Code";
            this.Group_ABS_Error_Code.Padding = new System.Windows.Forms.Padding(2);
            this.Group_ABS_Error_Code.Size = new System.Drawing.Size(94, 368);
            this.Group_ABS_Error_Code.TabIndex = 25;
            this.Group_ABS_Error_Code.TabStop = false;
            this.Group_ABS_Error_Code.Text = "ABS Status";
            // 
            // ABS_0x5025
            // 
            this.ABS_0x5025.AutoSize = true;
            this.ABS_0x5025.Location = new System.Drawing.Point(10, 303);
            this.ABS_0x5025.Margin = new System.Windows.Forms.Padding(2);
            this.ABS_0x5025.Name = "ABS_0x5025";
            this.ABS_0x5025.Size = new System.Drawing.Size(75, 20);
            this.ABS_0x5025.TabIndex = 16;
            this.ABS_0x5025.Text = "0x5025";
            this.ABS_0x5025.UseVisualStyleBackColor = true;
            // 
            // ABS_0x5044
            // 
            this.ABS_0x5044.AutoSize = true;
            this.ABS_0x5044.Location = new System.Drawing.Point(10, 283);
            this.ABS_0x5044.Margin = new System.Windows.Forms.Padding(2);
            this.ABS_0x5044.Name = "ABS_0x5044";
            this.ABS_0x5044.Size = new System.Drawing.Size(75, 20);
            this.ABS_0x5044.TabIndex = 15;
            this.ABS_0x5044.Text = "0x5044";
            this.ABS_0x5044.UseVisualStyleBackColor = true;
            // 
            // ABS_0x5052
            // 
            this.ABS_0x5052.AutoSize = true;
            this.ABS_0x5052.Location = new System.Drawing.Point(10, 160);
            this.ABS_0x5052.Margin = new System.Windows.Forms.Padding(2);
            this.ABS_0x5052.Name = "ABS_0x5052";
            this.ABS_0x5052.Size = new System.Drawing.Size(75, 20);
            this.ABS_0x5052.TabIndex = 7;
            this.ABS_0x5052.Text = "0x5052";
            this.ABS_0x5052.UseVisualStyleBackColor = true;
            // 
            // ABS_0x5042
            // 
            this.ABS_0x5042.AutoSize = true;
            this.ABS_0x5042.Location = new System.Drawing.Point(10, 263);
            this.ABS_0x5042.Margin = new System.Windows.Forms.Padding(2);
            this.ABS_0x5042.Name = "ABS_0x5042";
            this.ABS_0x5042.Size = new System.Drawing.Size(75, 20);
            this.ABS_0x5042.TabIndex = 14;
            this.ABS_0x5042.Text = "0x5042";
            this.ABS_0x5042.UseVisualStyleBackColor = true;
            // 
            // ABS_0x5053
            // 
            this.ABS_0x5053.AutoSize = true;
            this.ABS_0x5053.Location = new System.Drawing.Point(10, 140);
            this.ABS_0x5053.Margin = new System.Windows.Forms.Padding(2);
            this.ABS_0x5053.Name = "ABS_0x5053";
            this.ABS_0x5053.Size = new System.Drawing.Size(75, 20);
            this.ABS_0x5053.TabIndex = 6;
            this.ABS_0x5053.Text = "0x5053";
            this.ABS_0x5053.UseVisualStyleBackColor = true;
            // 
            // ABS_0x5045
            // 
            this.ABS_0x5045.AutoSize = true;
            this.ABS_0x5045.Location = new System.Drawing.Point(10, 243);
            this.ABS_0x5045.Margin = new System.Windows.Forms.Padding(2);
            this.ABS_0x5045.Name = "ABS_0x5045";
            this.ABS_0x5045.Size = new System.Drawing.Size(75, 20);
            this.ABS_0x5045.TabIndex = 13;
            this.ABS_0x5045.Text = "0x5045";
            this.ABS_0x5045.UseVisualStyleBackColor = true;
            // 
            // ABS_0x5014
            // 
            this.ABS_0x5014.AutoSize = true;
            this.ABS_0x5014.Location = new System.Drawing.Point(10, 120);
            this.ABS_0x5014.Margin = new System.Windows.Forms.Padding(2);
            this.ABS_0x5014.Name = "ABS_0x5014";
            this.ABS_0x5014.Size = new System.Drawing.Size(75, 20);
            this.ABS_0x5014.TabIndex = 5;
            this.ABS_0x5014.Text = "0x5014";
            this.ABS_0x5014.UseVisualStyleBackColor = true;
            // 
            // ABS_0x5043
            // 
            this.ABS_0x5043.AutoSize = true;
            this.ABS_0x5043.Location = new System.Drawing.Point(10, 223);
            this.ABS_0x5043.Margin = new System.Windows.Forms.Padding(2);
            this.ABS_0x5043.Name = "ABS_0x5043";
            this.ABS_0x5043.Size = new System.Drawing.Size(75, 20);
            this.ABS_0x5043.TabIndex = 12;
            this.ABS_0x5043.Text = "0x5043";
            this.ABS_0x5043.UseVisualStyleBackColor = true;
            // 
            // ABS_0x5018
            // 
            this.ABS_0x5018.AutoSize = true;
            this.ABS_0x5018.Location = new System.Drawing.Point(10, 100);
            this.ABS_0x5018.Margin = new System.Windows.Forms.Padding(2);
            this.ABS_0x5018.Name = "ABS_0x5018";
            this.ABS_0x5018.Size = new System.Drawing.Size(75, 20);
            this.ABS_0x5018.TabIndex = 4;
            this.ABS_0x5018.Text = "0x5018";
            this.ABS_0x5018.UseVisualStyleBackColor = true;
            // 
            // ABS_0x5035
            // 
            this.ABS_0x5035.AutoSize = true;
            this.ABS_0x5035.Location = new System.Drawing.Point(10, 203);
            this.ABS_0x5035.Margin = new System.Windows.Forms.Padding(2);
            this.ABS_0x5035.Name = "ABS_0x5035";
            this.ABS_0x5035.Size = new System.Drawing.Size(75, 20);
            this.ABS_0x5035.TabIndex = 11;
            this.ABS_0x5035.Text = "0x5035";
            this.ABS_0x5035.UseVisualStyleBackColor = true;
            // 
            // ABS_0x5013
            // 
            this.ABS_0x5013.AutoSize = true;
            this.ABS_0x5013.Location = new System.Drawing.Point(10, 80);
            this.ABS_0x5013.Margin = new System.Windows.Forms.Padding(2);
            this.ABS_0x5013.Name = "ABS_0x5013";
            this.ABS_0x5013.Size = new System.Drawing.Size(75, 20);
            this.ABS_0x5013.TabIndex = 3;
            this.ABS_0x5013.Text = "0x5013";
            this.ABS_0x5013.UseVisualStyleBackColor = true;
            // 
            // ABS_0x5017
            // 
            this.ABS_0x5017.AutoSize = true;
            this.ABS_0x5017.Location = new System.Drawing.Point(10, 60);
            this.ABS_0x5017.Margin = new System.Windows.Forms.Padding(2);
            this.ABS_0x5017.Name = "ABS_0x5017";
            this.ABS_0x5017.Size = new System.Drawing.Size(75, 20);
            this.ABS_0x5017.TabIndex = 2;
            this.ABS_0x5017.Text = "0x5017";
            this.ABS_0x5017.UseVisualStyleBackColor = true;
            // 
            // ABS_0x5019
            // 
            this.ABS_0x5019.AutoSize = true;
            this.ABS_0x5019.Location = new System.Drawing.Point(10, 39);
            this.ABS_0x5019.Margin = new System.Windows.Forms.Padding(2);
            this.ABS_0x5019.Name = "ABS_0x5019";
            this.ABS_0x5019.Size = new System.Drawing.Size(75, 20);
            this.ABS_0x5019.TabIndex = 1;
            this.ABS_0x5019.Text = "0x5019";
            this.ABS_0x5019.UseVisualStyleBackColor = true;
            // 
            // ABS_0x5055
            // 
            this.ABS_0x5055.AutoSize = true;
            this.ABS_0x5055.Location = new System.Drawing.Point(10, 19);
            this.ABS_0x5055.Margin = new System.Windows.Forms.Padding(2);
            this.ABS_0x5055.Name = "ABS_0x5055";
            this.ABS_0x5055.Size = new System.Drawing.Size(75, 20);
            this.ABS_0x5055.TabIndex = 0;
            this.ABS_0x5055.Text = "0x5055";
            this.ABS_0x5055.UseVisualStyleBackColor = true;
            // 
            // Group_DTC_data_option
            // 
            this.Group_DTC_data_option.Controls.Add(this.DTC_option_all_in_turn);
            this.Group_DTC_data_option.Controls.Add(this.DTC_option_first_six);
            this.Group_DTC_data_option.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Group_DTC_data_option.Location = new System.Drawing.Point(12, 72);
            this.Group_DTC_data_option.Margin = new System.Windows.Forms.Padding(2);
            this.Group_DTC_data_option.Name = "Group_DTC_data_option";
            this.Group_DTC_data_option.Padding = new System.Windows.Forms.Padding(2);
            this.Group_DTC_data_option.Size = new System.Drawing.Size(187, 308);
            this.Group_DTC_data_option.TabIndex = 26;
            this.Group_DTC_data_option.TabStop = false;
            this.Group_DTC_data_option.Text = "DTC data options";
            // 
            // DTC_option_all_in_turn
            // 
            this.DTC_option_all_in_turn.AutoSize = true;
            this.DTC_option_all_in_turn.Location = new System.Drawing.Point(13, 58);
            this.DTC_option_all_in_turn.Margin = new System.Windows.Forms.Padding(2);
            this.DTC_option_all_in_turn.Name = "DTC_option_all_in_turn";
            this.DTC_option_all_in_turn.Size = new System.Drawing.Size(124, 20);
            this.DTC_option_all_in_turn.TabIndex = 2;
            this.DTC_option_all_in_turn.Text = "All DTC in turn";
            this.DTC_option_all_in_turn.UseVisualStyleBackColor = true;
            // 
            // DTC_option_first_six
            // 
            this.DTC_option_first_six.AutoSize = true;
            this.DTC_option_first_six.Checked = true;
            this.DTC_option_first_six.Location = new System.Drawing.Point(13, 24);
            this.DTC_option_first_six.Margin = new System.Windows.Forms.Padding(2);
            this.DTC_option_first_six.Name = "DTC_option_first_six";
            this.DTC_option_first_six.Size = new System.Drawing.Size(100, 20);
            this.DTC_option_first_six.TabIndex = 0;
            this.DTC_option_first_six.TabStop = true;
            this.DTC_option_first_six.Text = "First-6 DTC";
            this.DTC_option_first_six.UseVisualStyleBackColor = true;
            // 
            // tmr_Scan_ABS_DTC
            // 
            this.tmr_Scan_ABS_DTC.Interval = 500;
            this.tmr_Scan_ABS_DTC.Tick += new System.EventHandler(this.Tmr_Scan_ABS_DTC_Tick);
            // 
            // Form_K_Line
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.Group_DTC_data_option);
            this.Controls.Add(this.Group_ABS_Error_Code);
            this.Controls.Add(this.Group_OBD_Error_Code);
            this.Controls.Add(this.rtbKLineData);
            this.Controls.Add(this.btnConnectionControl);
            this.Controls.Add(this.lstSerialComPort);
            this.Controls.Add(this.btnRefreshCOMNo);
            this.Name = "Form_K_Line";
            this.Text = "K_Line_Test";
            this.Group_OBD_Error_Code.ResumeLayout(false);
            this.Group_OBD_Error_Code.PerformLayout();
            this.Group_ABS_Error_Code.ResumeLayout(false);
            this.Group_ABS_Error_Code.PerformLayout();
            this.Group_DTC_data_option.ResumeLayout(false);
            this.Group_DTC_data_option.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnectionControl;
        private System.Windows.Forms.ListBox lstSerialComPort;
        private System.Windows.Forms.Button btnRefreshCOMNo;
        private System.Windows.Forms.RichTextBox rtbKLineData;
        private System.Windows.Forms.Timer tmr_FetchingUARTInput;
        private System.Windows.Forms.GroupBox Group_OBD_Error_Code;
        private System.Windows.Forms.CheckBox OBD_U0486;
        private System.Windows.Forms.CheckBox OBD_U0426;
        private System.Windows.Forms.CheckBox OBD_U0122;
        private System.Windows.Forms.CheckBox OBD_U0121;
        private System.Windows.Forms.CheckBox OBD_U0002;
        private System.Windows.Forms.CheckBox OBD_U0140;
        private System.Windows.Forms.CheckBox OBD_U0001;
        private System.Windows.Forms.CheckBox OBD_U0128;
        private System.Windows.Forms.CheckBox OBD_P2600;
        private System.Windows.Forms.CheckBox OBD_P2158;
        private System.Windows.Forms.CheckBox OBD_P1800;
        private System.Windows.Forms.CheckBox OBD_P1607;
        private System.Windows.Forms.CheckBox OBD_P1536;
        private System.Windows.Forms.CheckBox OBD_P1310;
        private System.Windows.Forms.CheckBox OBD_P0620_PIN31;
        private System.Windows.Forms.CheckBox OBD_P1300;
        private System.Windows.Forms.CheckBox OBD_P0620_PIN2;
        private System.Windows.Forms.CheckBox OBD_P0A0F;
        private System.Windows.Forms.CheckBox OBD_P0606;
        private System.Windows.Forms.CheckBox OBD_P0655;
        private System.Windows.Forms.CheckBox OBD_P0605;
        private System.Windows.Forms.CheckBox OBD_P0650;
        private System.Windows.Forms.CheckBox OBD_P0604;
        private System.Windows.Forms.CheckBox OBD_P0601;
        private System.Windows.Forms.CheckBox OBD_P0560;
        private System.Windows.Forms.CheckBox OBD_P0512;
        private System.Windows.Forms.CheckBox OBD_P0500;
        private System.Windows.Forms.CheckBox OBD_P0480;
        private System.Windows.Forms.CheckBox OBD_P0410;
        private System.Windows.Forms.CheckBox OBD_P0505;
        private System.Windows.Forms.CheckBox OBD_P0352;
        private System.Windows.Forms.CheckBox OBD_P0501;
        private System.Windows.Forms.CheckBox OBD_P0351;
        private System.Windows.Forms.CheckBox OBD_P0336;
        private System.Windows.Forms.CheckBox OBD_P0335;
        private System.Windows.Forms.CheckBox OBD_P0230;
        private System.Windows.Forms.CheckBox OBD_P0217;
        private System.Windows.Forms.CheckBox OBD_P0202;
        private System.Windows.Forms.CheckBox OBD_P0130;
        private System.Windows.Forms.CheckBox OBD_P0201;
        private System.Windows.Forms.CheckBox OBD_P0120;
        private System.Windows.Forms.CheckBox OBD_P0155;
        private System.Windows.Forms.CheckBox OBD_P0115;
        private System.Windows.Forms.CheckBox OBD_P0150;
        private System.Windows.Forms.CheckBox OBD_P0110;
        private System.Windows.Forms.CheckBox OBD_P0135;
        private System.Windows.Forms.CheckBox OBD_P0105;
        private System.Windows.Forms.CheckBox OBD_C0085;
        private System.Windows.Forms.CheckBox OBD_C0083;
        private System.Windows.Forms.CheckBox OBD_P0503;
        private System.Windows.Forms.GroupBox Group_ABS_Error_Code;
        private System.Windows.Forms.CheckBox ABS_0x5025;
        private System.Windows.Forms.CheckBox ABS_0x5044;
        private System.Windows.Forms.CheckBox ABS_0x5052;
        private System.Windows.Forms.CheckBox ABS_0x5042;
        private System.Windows.Forms.CheckBox ABS_0x5053;
        private System.Windows.Forms.CheckBox ABS_0x5045;
        private System.Windows.Forms.CheckBox ABS_0x5014;
        private System.Windows.Forms.CheckBox ABS_0x5043;
        private System.Windows.Forms.CheckBox ABS_0x5018;
        private System.Windows.Forms.CheckBox ABS_0x5035;
        private System.Windows.Forms.CheckBox ABS_0x5013;
        private System.Windows.Forms.CheckBox ABS_0x5017;
        private System.Windows.Forms.CheckBox ABS_0x5019;
        private System.Windows.Forms.CheckBox ABS_0x5055;
        private System.Windows.Forms.GroupBox Group_DTC_data_option;
        private System.Windows.Forms.RadioButton DTC_option_all_in_turn;
        private System.Windows.Forms.RadioButton DTC_option_first_six;
        private System.Windows.Forms.Timer tmr_Scan_ABS_DTC;
    }
}

