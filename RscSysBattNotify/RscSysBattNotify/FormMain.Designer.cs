namespace RscSysBattNotify
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblPowerLineValue = new System.Windows.Forms.Label();
            this.lblPowerLine = new System.Windows.Forms.Label();
            this.lblBatteryChargeValue = new System.Windows.Forms.Label();
            this.lblBatteryLife = new System.Windows.Forms.Label();
            this.lblBatteryLifeValue = new System.Windows.Forms.Label();
            this.lblBatteryFullLifetime = new System.Windows.Forms.Label();
            this.lblBatteryFullLifetimeValue = new System.Windows.Forms.Label();
            this.lblBatteryLifeRemaining = new System.Windows.Forms.Label();
            this.lblBatteryLifeRemainingValue = new System.Windows.Forms.Label();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.chbAutoStart = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnHide = new System.Windows.Forms.Button();
            this.lblRefreshRate = new System.Windows.Forms.Label();
            this.btnSettings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(6, 97);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(26, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "X";
            this.toolTip1.SetToolTip(this.btnClose, "Exit application");
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblPowerLineValue
            // 
            this.lblPowerLineValue.AutoSize = true;
            this.lblPowerLineValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPowerLineValue.ForeColor = System.Drawing.SystemColors.Info;
            this.lblPowerLineValue.Location = new System.Drawing.Point(123, 15);
            this.lblPowerLineValue.Name = "lblPowerLineValue";
            this.lblPowerLineValue.Size = new System.Drawing.Size(31, 17);
            this.lblPowerLineValue.TabIndex = 2;
            this.lblPowerLineValue.Text = "N/A";
            // 
            // lblPowerLine
            // 
            this.lblPowerLine.AutoSize = true;
            this.lblPowerLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPowerLine.ForeColor = System.Drawing.Color.White;
            this.lblPowerLine.Location = new System.Drawing.Point(44, 15);
            this.lblPowerLine.Name = "lblPowerLine";
            this.lblPowerLine.Size = new System.Drawing.Size(82, 17);
            this.lblPowerLine.TabIndex = 3;
            this.lblPowerLine.Text = "Power Line:";
            // 
            // lblBatteryChargeValue
            // 
            this.lblBatteryChargeValue.AutoSize = true;
            this.lblBatteryChargeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblBatteryChargeValue.ForeColor = System.Drawing.SystemColors.Info;
            this.lblBatteryChargeValue.Location = new System.Drawing.Point(46, 41);
            this.lblBatteryChargeValue.Name = "lblBatteryChargeValue";
            this.lblBatteryChargeValue.Size = new System.Drawing.Size(31, 17);
            this.lblBatteryChargeValue.TabIndex = 5;
            this.lblBatteryChargeValue.Text = "N/A";
            // 
            // lblBatteryLife
            // 
            this.lblBatteryLife.AutoSize = true;
            this.lblBatteryLife.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblBatteryLife.ForeColor = System.Drawing.Color.White;
            this.lblBatteryLife.Location = new System.Drawing.Point(33, 69);
            this.lblBatteryLife.Name = "lblBatteryLife";
            this.lblBatteryLife.Size = new System.Drawing.Size(94, 20);
            this.lblBatteryLife.TabIndex = 6;
            this.lblBatteryLife.Text = "Battery Life:";
            // 
            // lblBatteryLifeValue
            // 
            this.lblBatteryLifeValue.AutoSize = true;
            this.lblBatteryLifeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblBatteryLifeValue.ForeColor = System.Drawing.Color.White;
            this.lblBatteryLifeValue.Location = new System.Drawing.Point(122, 69);
            this.lblBatteryLifeValue.Name = "lblBatteryLifeValue";
            this.lblBatteryLifeValue.Size = new System.Drawing.Size(38, 20);
            this.lblBatteryLifeValue.TabIndex = 7;
            this.lblBatteryLifeValue.Text = "N/A";
            // 
            // lblBatteryFullLifetime
            // 
            this.lblBatteryFullLifetime.AutoSize = true;
            this.lblBatteryFullLifetime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblBatteryFullLifetime.ForeColor = System.Drawing.Color.White;
            this.lblBatteryFullLifetime.Location = new System.Drawing.Point(28, 127);
            this.lblBatteryFullLifetime.Name = "lblBatteryFullLifetime";
            this.lblBatteryFullLifetime.Size = new System.Drawing.Size(136, 17);
            this.lblBatteryFullLifetime.TabIndex = 8;
            this.lblBatteryFullLifetime.Text = "Battery Full Lifetime:";
            // 
            // lblBatteryFullLifetimeValue
            // 
            this.lblBatteryFullLifetimeValue.AutoSize = true;
            this.lblBatteryFullLifetimeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblBatteryFullLifetimeValue.ForeColor = System.Drawing.Color.White;
            this.lblBatteryFullLifetimeValue.Location = new System.Drawing.Point(170, 127);
            this.lblBatteryFullLifetimeValue.Name = "lblBatteryFullLifetimeValue";
            this.lblBatteryFullLifetimeValue.Size = new System.Drawing.Size(31, 17);
            this.lblBatteryFullLifetimeValue.TabIndex = 9;
            this.lblBatteryFullLifetimeValue.Text = "N/A";
            // 
            // lblBatteryLifeRemaining
            // 
            this.lblBatteryLifeRemaining.AutoSize = true;
            this.lblBatteryLifeRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblBatteryLifeRemaining.ForeColor = System.Drawing.Color.White;
            this.lblBatteryLifeRemaining.Location = new System.Drawing.Point(47, 100);
            this.lblBatteryLifeRemaining.Name = "lblBatteryLifeRemaining";
            this.lblBatteryLifeRemaining.Size = new System.Drawing.Size(79, 17);
            this.lblBatteryLifeRemaining.TabIndex = 10;
            this.lblBatteryLifeRemaining.Text = "Remaining:";
            // 
            // lblBatteryLifeRemainingValue
            // 
            this.lblBatteryLifeRemainingValue.AutoSize = true;
            this.lblBatteryLifeRemainingValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblBatteryLifeRemainingValue.ForeColor = System.Drawing.SystemColors.Info;
            this.lblBatteryLifeRemainingValue.Location = new System.Drawing.Point(123, 100);
            this.lblBatteryLifeRemainingValue.Name = "lblBatteryLifeRemainingValue";
            this.lblBatteryLifeRemainingValue.Size = new System.Drawing.Size(31, 17);
            this.lblBatteryLifeRemainingValue.TabIndex = 11;
            this.lblBatteryLifeRemainingValue.Text = "N/A";
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Enabled = true;
            this.tmrRefresh.Interval = 60000;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // chbAutoStart
            // 
            this.chbAutoStart.AutoSize = true;
            this.chbAutoStart.Location = new System.Drawing.Point(12, 43);
            this.chbAutoStart.Name = "chbAutoStart";
            this.chbAutoStart.Size = new System.Drawing.Size(15, 14);
            this.chbAutoStart.TabIndex = 2;
            this.toolTip1.SetToolTip(this.chbAutoStart, "Start with Windows");
            this.chbAutoStart.UseVisualStyleBackColor = true;
            this.chbAutoStart.CheckedChanged += new System.EventHandler(this.chbAutoStart_CheckedChanged);
            // 
            // btnHide
            // 
            this.btnHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHide.ForeColor = System.Drawing.Color.White;
            this.btnHide.Location = new System.Drawing.Point(6, 6);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(26, 23);
            this.btnHide.TabIndex = 1;
            this.btnHide.Text = "V";
            this.toolTip1.SetToolTip(this.btnHide, "Hide this window");
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // lblRefreshRate
            // 
            this.lblRefreshRate.AutoSize = true;
            this.lblRefreshRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblRefreshRate.ForeColor = System.Drawing.Color.LightGray;
            this.lblRefreshRate.Location = new System.Drawing.Point(187, 109);
            this.lblRefreshRate.Name = "lblRefreshRate";
            this.lblRefreshRate.Size = new System.Drawing.Size(87, 12);
            this.lblRefreshRate.TabIndex = 12;
            this.lblRefreshRate.Text = "Refresh Rate: 1 min";
            // 
            // btnSettings
            // 
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.ForeColor = System.Drawing.Color.White;
            this.btnSettings.Location = new System.Drawing.Point(6, 66);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(26, 23);
            this.btnSettings.TabIndex = 3;
            this.btnSettings.Text = "...";
            this.toolTip1.SetToolTip(this.btnSettings, "Exit application");
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(284, 126);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.lblRefreshRate);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.chbAutoStart);
            this.Controls.Add(this.lblBatteryLifeRemainingValue);
            this.Controls.Add(this.lblBatteryLifeRemaining);
            this.Controls.Add(this.lblBatteryFullLifetimeValue);
            this.Controls.Add(this.lblBatteryFullLifetime);
            this.Controls.Add(this.lblBatteryLifeValue);
            this.Controls.Add(this.lblBatteryLife);
            this.Controls.Add(this.lblBatteryChargeValue);
            this.Controls.Add(this.lblPowerLine);
            this.Controls.Add(this.lblPowerLineValue);
            this.Controls.Add(this.btnClose);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "FormMain";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormMain_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblPowerLineValue;
        private System.Windows.Forms.Label lblPowerLine;
        private System.Windows.Forms.Label lblBatteryChargeValue;
        private System.Windows.Forms.Label lblBatteryLife;
        private System.Windows.Forms.Label lblBatteryLifeValue;
        private System.Windows.Forms.Label lblBatteryFullLifetime;
        private System.Windows.Forms.Label lblBatteryFullLifetimeValue;
        private System.Windows.Forms.Label lblBatteryLifeRemaining;
        private System.Windows.Forms.Label lblBatteryLifeRemainingValue;
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.CheckBox chbAutoStart;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Label lblRefreshRate;
        private System.Windows.Forms.Button btnSettings;
    }
}

