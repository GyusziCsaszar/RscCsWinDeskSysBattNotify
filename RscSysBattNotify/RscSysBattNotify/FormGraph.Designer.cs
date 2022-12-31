namespace RscSysBattNotify
{
    partial class FormGraph
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
            this.lblLogPath = new System.Windows.Forms.Label();
            this.tbLogPath = new System.Windows.Forms.TextBox();
            this.btnLogPath = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chbDoLog = new System.Windows.Forms.CheckBox();
            this.lblRefreshRate = new System.Windows.Forms.Label();
            this.lblRanges = new System.Windows.Forms.Label();
            this.tbRangeEmpty = new System.Windows.Forms.TextBox();
            this.pnlLow = new System.Windows.Forms.Panel();
            this.tbRangeLow = new System.Windows.Forms.TextBox();
            this.tbRangeNormal = new System.Windows.Forms.TextBox();
            this.pnlMidLow = new System.Windows.Forms.Panel();
            this.tbRangeHigh = new System.Windows.Forms.TextBox();
            this.pnlHigh = new System.Windows.Forms.Panel();
            this.pnlMidHigh = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblLogPath
            // 
            this.lblLogPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLogPath.AutoSize = true;
            this.lblLogPath.Location = new System.Drawing.Point(13, 184);
            this.lblLogPath.Name = "lblLogPath";
            this.lblLogPath.Size = new System.Drawing.Size(72, 13);
            this.lblLogPath.TabIndex = 0;
            this.lblLogPath.Text = "LOG file path:";
            // 
            // tbLogPath
            // 
            this.tbLogPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLogPath.BackColor = System.Drawing.Color.Gainsboro;
            this.tbLogPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLogPath.ForeColor = System.Drawing.Color.Black;
            this.tbLogPath.Location = new System.Drawing.Point(91, 181);
            this.tbLogPath.Name = "tbLogPath";
            this.tbLogPath.Size = new System.Drawing.Size(301, 20);
            this.tbLogPath.TabIndex = 2;
            // 
            // btnLogPath
            // 
            this.btnLogPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogPath.Location = new System.Drawing.Point(398, 178);
            this.btnLogPath.Name = "btnLogPath";
            this.btnLogPath.Size = new System.Drawing.Size(27, 23);
            this.btnLogPath.TabIndex = 1;
            this.btnLogPath.Text = "...";
            this.btnLogPath.UseVisualStyleBackColor = true;
            this.btnLogPath.Click += new System.EventHandler(this.btnLogPath_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.Location = new System.Drawing.Point(171, 226);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(261, 226);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(352, 226);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chbDoLog
            // 
            this.chbDoLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbDoLog.AutoSize = true;
            this.chbDoLog.Location = new System.Drawing.Point(16, 216);
            this.chbDoLog.Name = "chbDoLog";
            this.chbDoLog.Size = new System.Drawing.Size(117, 17);
            this.chbDoLog.TabIndex = 3;
            this.chbDoLog.Text = "Log Battery Charge";
            this.chbDoLog.UseVisualStyleBackColor = true;
            // 
            // lblRefreshRate
            // 
            this.lblRefreshRate.AutoSize = true;
            this.lblRefreshRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblRefreshRate.ForeColor = System.Drawing.Color.LightGray;
            this.lblRefreshRate.Location = new System.Drawing.Point(179, 115);
            this.lblRefreshRate.Name = "lblRefreshRate";
            this.lblRefreshRate.Size = new System.Drawing.Size(87, 12);
            this.lblRefreshRate.TabIndex = 13;
            this.lblRefreshRate.Text = "Refresh Rate: 1 min";
            // 
            // lblRanges
            // 
            this.lblRanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRanges.AutoSize = true;
            this.lblRanges.Location = new System.Drawing.Point(13, 147);
            this.lblRanges.Name = "lblRanges";
            this.lblRanges.Size = new System.Drawing.Size(64, 13);
            this.lblRanges.TabIndex = 14;
            this.lblRanges.Text = "Ranges (%):";
            // 
            // tbRangeEmpty
            // 
            this.tbRangeEmpty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbRangeEmpty.BackColor = System.Drawing.Color.Gainsboro;
            this.tbRangeEmpty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRangeEmpty.Location = new System.Drawing.Point(88, 145);
            this.tbRangeEmpty.Name = "tbRangeEmpty";
            this.tbRangeEmpty.ReadOnly = true;
            this.tbRangeEmpty.Size = new System.Drawing.Size(37, 20);
            this.tbRangeEmpty.TabIndex = 15;
            this.tbRangeEmpty.Text = "0";
            this.tbRangeEmpty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pnlLow
            // 
            this.pnlLow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlLow.BackColor = System.Drawing.Color.Red;
            this.pnlLow.Location = new System.Drawing.Point(131, 148);
            this.pnlLow.Name = "pnlLow";
            this.pnlLow.Size = new System.Drawing.Size(25, 12);
            this.pnlLow.TabIndex = 16;
            // 
            // tbRangeLow
            // 
            this.tbRangeLow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbRangeLow.BackColor = System.Drawing.Color.Gainsboro;
            this.tbRangeLow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRangeLow.Location = new System.Drawing.Point(162, 145);
            this.tbRangeLow.Name = "tbRangeLow";
            this.tbRangeLow.Size = new System.Drawing.Size(37, 20);
            this.tbRangeLow.TabIndex = 17;
            this.tbRangeLow.Text = "10";
            this.tbRangeLow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbRangeNormal
            // 
            this.tbRangeNormal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbRangeNormal.BackColor = System.Drawing.Color.Gainsboro;
            this.tbRangeNormal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRangeNormal.Location = new System.Drawing.Point(276, 145);
            this.tbRangeNormal.Name = "tbRangeNormal";
            this.tbRangeNormal.Size = new System.Drawing.Size(37, 20);
            this.tbRangeNormal.TabIndex = 19;
            this.tbRangeNormal.Text = "90";
            this.tbRangeNormal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pnlMidLow
            // 
            this.pnlMidLow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlMidLow.BackColor = System.Drawing.Color.Orange;
            this.pnlMidLow.Location = new System.Drawing.Point(207, 148);
            this.pnlMidLow.Name = "pnlMidLow";
            this.pnlMidLow.Size = new System.Drawing.Size(25, 12);
            this.pnlMidLow.TabIndex = 18;
            // 
            // tbRangeHigh
            // 
            this.tbRangeHigh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbRangeHigh.BackColor = System.Drawing.Color.Gainsboro;
            this.tbRangeHigh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRangeHigh.Location = new System.Drawing.Point(352, 145);
            this.tbRangeHigh.Name = "tbRangeHigh";
            this.tbRangeHigh.ReadOnly = true;
            this.tbRangeHigh.Size = new System.Drawing.Size(37, 20);
            this.tbRangeHigh.TabIndex = 21;
            this.tbRangeHigh.Text = "100";
            this.tbRangeHigh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pnlHigh
            // 
            this.pnlHigh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlHigh.BackColor = System.Drawing.Color.LimeGreen;
            this.pnlHigh.Location = new System.Drawing.Point(321, 148);
            this.pnlHigh.Name = "pnlHigh";
            this.pnlHigh.Size = new System.Drawing.Size(25, 12);
            this.pnlHigh.TabIndex = 20;
            // 
            // pnlMidHigh
            // 
            this.pnlMidHigh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlMidHigh.BackColor = System.Drawing.Color.DarkKhaki;
            this.pnlMidHigh.Location = new System.Drawing.Point(241, 148);
            this.pnlMidHigh.Name = "pnlMidHigh";
            this.pnlMidHigh.Size = new System.Drawing.Size(25, 12);
            this.pnlMidHigh.TabIndex = 22;
            // 
            // FormGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(437, 261);
            this.Controls.Add(this.pnlMidHigh);
            this.Controls.Add(this.tbRangeHigh);
            this.Controls.Add(this.pnlHigh);
            this.Controls.Add(this.tbRangeNormal);
            this.Controls.Add(this.pnlMidLow);
            this.Controls.Add(this.tbRangeLow);
            this.Controls.Add(this.pnlLow);
            this.Controls.Add(this.tbRangeEmpty);
            this.Controls.Add(this.lblRanges);
            this.Controls.Add(this.lblRefreshRate);
            this.Controls.Add(this.chbDoLog);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnLogPath);
            this.Controls.Add(this.tbLogPath);
            this.Controls.Add(this.lblLogPath);
            this.ForeColor = System.Drawing.Color.White;
            this.MinimizeBox = false;
            this.Name = "FormGraph";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.TopMost = true;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormGraph_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLogPath;
        private System.Windows.Forms.TextBox tbLogPath;
        private System.Windows.Forms.Button btnLogPath;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chbDoLog;
        private System.Windows.Forms.Label lblRefreshRate;
        private System.Windows.Forms.Label lblRanges;
        private System.Windows.Forms.TextBox tbRangeEmpty;
        private System.Windows.Forms.Panel pnlLow;
        private System.Windows.Forms.TextBox tbRangeLow;
        private System.Windows.Forms.TextBox tbRangeNormal;
        private System.Windows.Forms.Panel pnlMidLow;
        private System.Windows.Forms.TextBox tbRangeHigh;
        private System.Windows.Forms.Panel pnlHigh;
        private System.Windows.Forms.Panel pnlMidHigh;
    }
}