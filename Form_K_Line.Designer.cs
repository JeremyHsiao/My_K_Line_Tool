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
            this.SuspendLayout();
            // 
            // btnConnectionControl
            // 
            this.btnConnectionControl.Enabled = false;
            this.btnConnectionControl.Location = new System.Drawing.Point(704, 12);
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
            this.lstSerialComPort.Location = new System.Drawing.Point(640, 12);
            this.lstSerialComPort.Name = "lstSerialComPort";
            this.lstSerialComPort.Size = new System.Drawing.Size(58, 46);
            this.lstSerialComPort.TabIndex = 20;
            // 
            // btnRefreshCOMNo
            // 
            this.btnRefreshCOMNo.Location = new System.Drawing.Point(585, 12);
            this.btnRefreshCOMNo.Name = "btnRefreshCOMNo";
            this.btnRefreshCOMNo.Size = new System.Drawing.Size(49, 46);
            this.btnRefreshCOMNo.TabIndex = 19;
            this.btnRefreshCOMNo.Text = "Refresh COM";
            this.btnRefreshCOMNo.UseVisualStyleBackColor = true;
            this.btnRefreshCOMNo.Click += new System.EventHandler(this.BtnRefreshCOMNo_Click);
            // 
            // rtbKLineData
            // 
            this.rtbKLineData.Location = new System.Drawing.Point(12, 64);
            this.rtbKLineData.Name = "rtbKLineData";
            this.rtbKLineData.ReadOnly = true;
            this.rtbKLineData.Size = new System.Drawing.Size(760, 485);
            this.rtbKLineData.TabIndex = 22;
            this.rtbKLineData.Text = "";
            // 
            // tmr_FetchingUARTInput
            // 
            this.tmr_FetchingUARTInput.Interval = 45;
            this.tmr_FetchingUARTInput.Tick += new System.EventHandler(this.Tmr_FetchingUARTInput_Tick);
            // 
            // Form_K_Line
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.rtbKLineData);
            this.Controls.Add(this.btnConnectionControl);
            this.Controls.Add(this.lstSerialComPort);
            this.Controls.Add(this.btnRefreshCOMNo);
            this.Name = "Form_K_Line";
            this.Text = "K_Line_Test";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnectionControl;
        private System.Windows.Forms.ListBox lstSerialComPort;
        private System.Windows.Forms.Button btnRefreshCOMNo;
        private System.Windows.Forms.RichTextBox rtbKLineData;
        private System.Windows.Forms.Timer tmr_FetchingUARTInput;
    }
}

