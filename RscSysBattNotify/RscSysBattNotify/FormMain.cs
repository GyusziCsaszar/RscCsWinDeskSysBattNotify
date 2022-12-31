using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Reflection;
using System.Runtime.InteropServices;

using System.Diagnostics;

using Ressive.Utils;

namespace RscSysBattNotify
{
    public partial class FormMain : Form
    {

        public const string csAPP_TITLE = "Rsc System Battery Notify v2.01";
        protected const string csAPP_NAME = "RscSysBattNotify";

        private int m_iBatteryLifePercentPrev = -1;
        private int m_iBatteryLifePercentPrevTenths = -1;

        private NotifyIcon m_notifyIcon = null;

        private FormGraph m_FormGraph = null;
        private Rectangle m_rcGraph;
        private bool m_bGraphClickStarted = false;

        private bool m_bDoLog;
        private string m_sLogPath;
        private int m_iRangeLow;
        private int m_iRangeNormal;

        // SRC: https://stackoverflow.com/questions/12026664/a-generic-error-occurred-in-gdi-when-calling-bitmap-gethicon
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);

        // SRC: https://stackoverflow.com/questions/156046/show-a-form-without-stealing-focus
        private const int SW_SHOWNOACTIVATE = 4;
        private const int HWND_TOPMOST = -1;
        private const uint SWP_NOACTIVATE = 0x0010;
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        static extern bool SetWindowPos(
             int hWnd,             // Window handle
             int hWndInsertAfter,  // Placement-order handle
             int X,                // Horizontal position
             int Y,                // Vertical position
             int cx,               // Width
             int cy,               // Height
             uint uFlags);         // Window positioning flags
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        static void ShowInactiveTopmost(Form frm)
        {
            ShowWindow(frm.Handle, SW_SHOWNOACTIVATE);
            SetWindowPos(frm.Handle.ToInt32(), HWND_TOPMOST,
            frm.Left, frm.Top, frm.Width, frm.Height,
            SWP_NOACTIVATE);
        }

        public FormMain()
        {
            InitializeComponent();

            StorageRegistry.m_sAppName  = csAPP_NAME;
            this.Text                   = csAPP_TITLE;

            m_sLogPath = StorageRegistry.Read("LogPath", "");
            m_bDoLog = (System.IO.File.Exists(m_sLogPath)) && (StorageRegistry.Read("DoLog", 0) > 0);
            m_iRangeLow = StorageRegistry.Read("RangeLow", 10);
            m_iRangeNormal = StorageRegistry.Read("RangeNormal", 90);

            MessageBoxEx.DarkMode = true;

            m_rcGraph = new Rectangle(0, 0, 0, 0);

            //Hide Caption Bar
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            PlaceWindow();

            chbAutoStart.Checked = IsAppStartWithWindowsOn();

            if (m_bDoLog)
            {
                try
                {
                    System.IO.File.AppendAllText(m_sLogPath, "0000;00;00;00;00;00;000;000;?\r\n"); //Mark App Start...
                }
                catch (Exception exc)
                {
                    MessageBoxEx.Show("Unable to write LOG file (" + m_sLogPath + ")!\r\n\r\nError: " + exc.Message, FormMain.csAPP_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error, true /*bTopMost*/);
                }
            }

            RefreshPowerStatus();

            RefreshNotifyIcon();
        }

        private void PlaceWindow()
        {
            Rectangle rect = Screen.FromControl(this).WorkingArea; // Bounds;
            this.Left = rect.Left + (rect.Width - (this.Width + 5));
            this.Top = rect.Top + (rect.Height - (this.Height + 5));
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.Visible = false;
            }
            else
            {
                PlaceWindow();

                RefreshPowerStatus();

                RefreshNotifyIcon();

                //this.Visible = true;
                ShowInactiveTopmost(this);
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_notifyIcon != null)
            {
                m_notifyIcon.Visible = false;

                IntPtr hIcon = IntPtr.Zero;
                if (m_notifyIcon.Icon != null)
                {
                    hIcon = m_notifyIcon.Icon.Handle;

                    m_notifyIcon.Icon = null;

                    // SRC: https://stackoverflow.com/questions/12026664/a-generic-error-occurred-in-gdi-when-calling-bitmap-gethicon
                    if (hIcon != IntPtr.Zero)
                    {
                        DestroyIcon(hIcon);
                    }
                }

                m_notifyIcon = null;
            }
        }

        private void RefreshNotifyIcon()
        {

            bool bJustCreated = false;

            if (m_notifyIcon == null)
            {
                bJustCreated = true;

                m_notifyIcon = new NotifyIcon();

                m_notifyIcon.Click += NotifyIcon_Click;

            }

            if (bJustCreated || true)
            {
                string sBattLevel;
                if (m_iBatteryLifePercentPrev > 99)
                {
                    sBattLevel = "1d";
                }
                else if (m_iBatteryLifePercentPrev < 0)
                {
                    sBattLevel = "?";
                }
                else
                {
                    sBattLevel = m_iBatteryLifePercentPrev.ToString();
                }

                string sInfo = lblBatteryLifeRemainingValue.Text.Replace(":", " hours ").Replace(" 0", " ") + " minutes" + " (" + sBattLevel + " %) remaining";
                m_notifyIcon.Text = sInfo;

                Color clrTx = Color.White;
                Color clrBk = Color.DodgerBlue;
                int iCY = 2;
                if (m_iBatteryLifePercentPrev <= 10)
                {
                    iCY = 1;
                    clrBk = Color.Red;
                }
                else if (m_iBatteryLifePercentPrev <= 20)
                {
                    iCY = 1;
                    clrBk = Color.OrangeRed;
                }
                else if (m_iBatteryLifePercentPrev >= 90)
                {
                    iCY = 1;
                    clrBk = Color.Green;
                }
                else
                {
                    iCY = 1;
                    clrTx = Color.Black;
                    clrBk = Color.Orange;
                }

                //m_NotifyIcon.Icon = SystemIcons.Exclamation;

                // SRC: https://stackoverflow.com/questions/25403169/get-application-icon-of-c-sharp-winforms-app
                //m_NotifyIcon.Icon = Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location);

                // SRC: https://stackoverflow.com/questions/34075264/i-want-to-display-numbers-on-the-system-tray-notification-icons-on-windows
                Brush brush = new SolidBrush(clrTx);
                Brush brushBk = new SolidBrush(clrBk);
                Pen penBk = new Pen(clrBk);
                // Create a bitmap and draw text on it
                Bitmap bitmap = new Bitmap(24, 24); // 32, 32); // 16, 16);
                Graphics graphics = Graphics.FromImage(bitmap);
                //graphics.DrawRectangle(new Pen(Color.Red), new Rectangle(0, 0, 23, 23));
                graphics.FillEllipse(brushBk, new Rectangle(3, 0, 23 - 4, 23 - 12));
                graphics.DrawEllipse(penBk, new Rectangle(3, 0, 23 - 4, 23 - 12));
                graphics.FillEllipse(brushBk, new Rectangle(3, 12, 23 - 4, 23 - 12));
                graphics.DrawEllipse(penBk, new Rectangle(3, 12, 23 - 4, 23 - 12));
                /*
                graphics.FillRectangle(brushBk, new Rectangle(3, 6, 23 - 5, 23 - 10));
                graphics.DrawRectangle(penBk, new Rectangle(3, 6, 23 - 5, 23 - 10));
                */
                graphics.FillRectangle(brushBk, new Rectangle(1, 6, 23 - 1, 23 - 10));
                graphics.DrawRectangle(penBk, new Rectangle(1, 6, 23 - 1, 23 - 10));
                Font font = new Font("Tahoma", 14);
                int iCX = 0;
                if (sBattLevel.Length < 2) iCX += 5;
                graphics.DrawString(sBattLevel, font, brush, iCX, iCY);
                // Convert the bitmap with text to an Icon

                IntPtr hIconOld = IntPtr.Zero;
                if (m_notifyIcon.Icon != null)
                {
                    hIconOld = m_notifyIcon.Icon.Handle;
                }

                m_notifyIcon.Icon = Icon.FromHandle(bitmap.GetHicon());

                // SRC: https://stackoverflow.com/questions/12026664/a-generic-error-occurred-in-gdi-when-calling-bitmap-gethicon
                if (hIconOld != IntPtr.Zero)
                {
                    DestroyIcon(hIconOld);
                }

                if (!m_notifyIcon.Visible)
                {
                    m_notifyIcon.Visible = true;
                }

            }
        }

        private void RefreshPowerStatus()
        {
            lblPowerLineValue.Text = GetPowerStatusValueAsString("PowerLineStatus");

            lblBatteryChargeValue.Text = GetPowerStatusValueAsString("BatteryChargeStatus");

            string sChrg = "C";
            if (lblBatteryChargeValue.Text == "Discharging" || lblBatteryChargeValue.Text == "High" || lblBatteryChargeValue.Text == "Low")
                sChrg = "D";

            lblBatteryLifeValue.Text = GetPowerStatusValueAsString("BatteryLifePercent");
            int iBattPerc = 0;
            if (!Int32.TryParse(lblBatteryLifeValue.Text.Substring(0, 3).Trim(), out iBattPerc))
            {
                iBattPerc = Int32.Parse(lblBatteryLifeValue.Text.Substring(0, 2).Trim());
            }
            if (iBattPerc >= m_iRangeNormal)
            {
                lblBatteryLifeValue.ForeColor = Color.Green;
            }
            else if (iBattPerc <= m_iRangeLow)
            {
                lblBatteryLifeValue.ForeColor = Color.Red;
            }
            else if (iBattPerc <= (m_iRangeLow + 10))
            {
                lblBatteryLifeValue.ForeColor = Color.OrangeRed;
            }
            else
            {
                lblBatteryLifeValue.ForeColor = Color.Orange;
            }

            BattLevel bl = new BattLevel();
            bl.iBattPerc = iBattPerc;
            bl.clFore = lblBatteryLifeValue.ForeColor;
            BatteryLevelStore.BatteryLevelList.Add(bl);

            int iBattPercTenths = iBattPerc / 10;
            if (m_iBatteryLifePercentPrevTenths != iBattPercTenths && m_iBatteryLifePercentPrevTenths > 0)
            {
                if (!Visible)
                {
                    //Visible = true;
                    ShowInactiveTopmost(this);
                }
            }
            m_iBatteryLifePercentPrevTenths = iBattPercTenths;
            m_iBatteryLifePercentPrev = iBattPerc;

            if (Visible) Refresh();
            if (m_FormGraph != null && m_FormGraph.Visible) m_FormGraph.Refresh();

            lblBatteryFullLifetimeValue.Text = GetPowerStatusValueAsString("BatteryFullLifetime");
            lblBatteryLifeRemainingValue.Text = GetPowerStatusValueAsString("BatteryLifeRemaining");

            if (m_bDoLog)
            {
                try
                {
                    DateTime dt = DateTime.Now;

                    string sLn = String.Format("{0:D4};{1:D2};{2:D2};{3:D2};{4:D2};{5:D2};{6:D3};{7:D3};" + sChrg + "\r\n", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond, iBattPerc);

                    System.IO.File.AppendAllText(m_sLogPath, sLn);
                }
                catch (Exception exc)
                {
                    MessageBoxEx.Show("Unable to write LOG file (" + m_sLogPath + ")!\r\n\r\nError: " + exc.Message, FormMain.csAPP_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error, true /*bTopMost*/);
                }
            }
        }

        private string GetPowerStatusValueAsString(string sName)
        {
            Type t = typeof(System.Windows.Forms.PowerStatus);
            PropertyInfo[] pi = t.GetProperties();
            for (int i = 0; i < pi.Length; i++)
            {
                if (pi[i].Name == sName)
                {
                    object oVal = pi[i].GetValue(SystemInformation.PowerStatus, null);

                    if (sName == "BatteryLifePercent")
                    {
                        float fVal = (float) oVal;
                        fVal = fVal * 100;
                        
                        // FIX
                        fVal = (float) Math.Round(fVal);

                        int iVal = (int) fVal;
                        return iVal.ToString() + " %"; 
                    }
                    else if (sName == "BatteryLifeRemaining")
                    {
                        int iVal = (int) oVal;

                        if (iVal < 0) return "N/A";

                        int iHour = iVal / 3600;
                        iVal = iVal - (iHour * 3600);

                        int iMin = iVal / 60;
                        iVal = iVal - (iMin * 60);

                        int iSec = iVal;

                        try
                        {
                            DateTime dt = new DateTime(1, 1, 1, iHour, iMin, iSec);
                            return dt.ToShortTimeString();
                        }
                        catch (Exception /*exc*/)
                        {
                            //NOP...
                        }

                        return "N/A";
                    }
                    else if (sName == "BatteryChargeStatus")
                    {
                        string sVal = oVal.ToString();
                        if (sVal == "0") return "Discharging";
                        if (sVal == "NoSystemBattery") return "No System Battery";
                        return sVal;
                    }
                    else
                    {
                        return oVal.ToString();
                    }
                }
            }

            return "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (m_notifyIcon != null && m_notifyIcon.Visible)
            {
                if (DialogResult.Yes == MessageBoxEx.Show("Notification area icon is visible for this app!\r\n\r\nDo you really want to close the application?\r\n\r\nPress No to hide instead!", csAPP_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, true /*bTopMost*/))
                {
                    Close();
                }
                else
                {
                    Visible = false;
                }
            }
            else
            {
                Close();
            }
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            tmrRefresh.Enabled = false;

            RefreshPowerStatus();

            RefreshNotifyIcon();

            tmrRefresh.Enabled = true;
        }

        private void chbAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            // SRC: https://stackoverflow.com/questions/5089601/how-to-run-a-c-sharp-application-at-windows-startup
            Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey
                        ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (chbAutoStart.Checked)
            {
                // BUG: Dll Path!!!
                //string sAppPath = Application.ExecutablePath;
                //string sAppPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                //string sAppPath = System.Reflection.Assembly.GetEntryAssembly().Location;

                // FIX
                string sAppPath = Application.ExecutablePath;
                if (sAppPath.Length > 4)
                {
                    if (sAppPath.Substring(sAppPath.Length - 4).ToLower() == ".dll")
                    {
                        sAppPath = sAppPath.Substring(0, sAppPath.Length - 4) + ".exe";
                    }
                }

                registryKey.SetValue(csAPP_NAME, sAppPath);
            }
            else
            {
                registryKey.DeleteValue(csAPP_NAME);
            }

            //registryKey.Dispose();
            ((IDisposable)registryKey).Dispose();
        }

        public bool IsAppStartWithWindowsOn()
        {
            Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey
                        ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            string sValue = (string)registryKey.GetValue(csAPP_NAME, "");

            // BUG: Dll Path!!!
            //string sAppPath = Application.ExecutablePath;
            //string sAppPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //string sAppPath = System.Reflection.Assembly.GetEntryAssembly().Location;

            // FIX
            string sAppPath = Application.ExecutablePath;
            if (sAppPath.Length > 4)
            {
                if (sAppPath.Substring(sAppPath.Length - 4).ToLower() == ".dll")
                {
                    sAppPath = sAppPath.Substring(0, sAppPath.Length - 4) + ".exe";
                }
            }

            return (sValue == sAppPath);
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            if (m_notifyIcon != null && m_notifyIcon.Visible)
            {
                Visible = false;
            }
        }

        private void FormMain_Paint(object sender, PaintEventArgs e)
        {

            Pen pen;

            Point ptBottomLeft = new Point();
            ptBottomLeft.X = lblBatteryLifeValue.Location.X + lblBatteryLifeValue.Size.Width + 10;
            //ptBottomLeft.Y = lblBatteryLifeRemainingValue.Location.Y +lblBatteryLifeRemainingValue.Size.Height;
            ptBottomLeft.Y = lblRefreshRate.Location.Y - 2;

            Point ptTopRight = new Point();
            ptTopRight.X = ClientRectangle.Left + ClientRectangle.Width - 5;
            ptTopRight.Y = ptBottomLeft.Y - 102;

            m_rcGraph = new Rectangle(ptBottomLeft.X, ptTopRight.Y, ptTopRight.X - ptBottomLeft.X, ptBottomLeft.Y - ptTopRight.Y);
            /*
            pen = new Pen(Color.Red);
            e.Graphics.DrawRectangle(pen, m_rcGraph);
            pen.Dispose();
            pen = null;
            */

            pen = new Pen(Color.Gray);
            e.Graphics.DrawLine(pen, ptBottomLeft.X, ptBottomLeft.Y, ptTopRight.X, ptBottomLeft.Y);
            e.Graphics.DrawLine(pen, ptBottomLeft.X, ptTopRight.Y, ptTopRight.X, ptTopRight.Y);
            pen.Dispose();
            pen = null;

            pen = new Pen(Color.DimGray);
            e.Graphics.DrawLine(pen, ptBottomLeft.X, ptBottomLeft.Y - 10, ptTopRight.X, ptBottomLeft.Y - 10);
            e.Graphics.DrawLine(pen, ptBottomLeft.X, ptBottomLeft.Y - 20, ptTopRight.X, ptBottomLeft.Y - 20);
            e.Graphics.DrawLine(pen, ptBottomLeft.X, ptBottomLeft.Y - 30, ptTopRight.X, ptBottomLeft.Y - 30);
            e.Graphics.DrawLine(pen, ptBottomLeft.X, ptBottomLeft.Y - 40, ptTopRight.X, ptBottomLeft.Y - 40);
            e.Graphics.DrawLine(pen, ptBottomLeft.X, ptBottomLeft.Y - 50, ptTopRight.X, ptBottomLeft.Y - 50);
            e.Graphics.DrawLine(pen, ptBottomLeft.X, ptBottomLeft.Y - 60, ptTopRight.X, ptBottomLeft.Y - 60);
            e.Graphics.DrawLine(pen, ptBottomLeft.X, ptBottomLeft.Y - 70, ptTopRight.X, ptBottomLeft.Y - 70);
            e.Graphics.DrawLine(pen, ptBottomLeft.X, ptBottomLeft.Y - 80, ptTopRight.X, ptBottomLeft.Y - 80);
            e.Graphics.DrawLine(pen, ptBottomLeft.X, ptBottomLeft.Y - 90, ptTopRight.X, ptBottomLeft.Y - 90);
            pen.Dispose();
            pen = null;

            int iMaxCnt = (ptTopRight.X - ptBottomLeft.X) + 1;

            // In FormGraph show all levels gathered...
            /*
            while (m_aBattLevels.Count > iMaxCnt)
            {
                m_aBattLevels.RemoveAt(0);
            }
            */
            int iFrom = Math.Max(0, BatteryLevelStore.BatteryLevelList.Count - iMaxCnt);

            for (int i = iFrom; i < BatteryLevelStore.BatteryLevelList.Count; i++)
            {
                if ((i == iFrom) || (BatteryLevelStore.BatteryLevelList[i - 1].clFore != BatteryLevelStore.BatteryLevelList[i].clFore))
                {
                    if (pen != null)
                    {
                        pen.Dispose();
                        pen = null;
                    }

                    pen = new Pen(BatteryLevelStore.BatteryLevelList[i].clFore);
                }

                if (BatteryLevelStore.BatteryLevelList[i].iBattPerc > 0)
                {
                    e.Graphics.DrawLine(pen, ptBottomLeft.X + (i - iFrom), ptBottomLeft.Y - 1, ptBottomLeft.X + (i - iFrom), ptBottomLeft.Y - BatteryLevelStore.BatteryLevelList[i].iBattPerc);
                }
            }

            if (pen != null)
            {
                pen.Dispose();
                pen = null;
            }
        }

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            m_bGraphClickStarted = false;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point pt = new Point(e.X, e.Y);
                if (m_rcGraph.Contains(pt))
                {
                    m_bGraphClickStarted = true;
                }
            }
        }

        private void FormMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_bGraphClickStarted)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    Point pt = new Point(e.X, e.Y);
                    if (m_rcGraph.Contains(pt))
                    {
                        m_bGraphClickStarted = false;

                        ShowSystemBatteryReport();
                    }
                }
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            try
            {
                m_FormGraph = new FormGraph();
                DialogResult dr = m_FormGraph.ShowDialog();
            }
            finally
            {
                m_FormGraph = null;
            }

            m_sLogPath = StorageRegistry.Read("LogPath", "");
            m_bDoLog = (System.IO.File.Exists(m_sLogPath)) && (StorageRegistry.Read("DoLog", 0) > 0);
            m_iRangeLow = StorageRegistry.Read("RangeLow", 10);
            m_iRangeNormal = StorageRegistry.Read("RangeNormal", 90);
        }

        private void ShowSystemBatteryReport()
        {
            ProcessStartInfo psi = new ProcessStartInfo();

            psi.UseShellExecute = true; // false;
            psi.Verb = "runas";
            psi.CreateNoWindow = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;

            psi.WorkingDirectory = Environment.SystemDirectory;

            psi.FileName = Environment.SystemDirectory + "\\powercfg.exe";
            psi.Arguments = "/batteryreport"; // /output \"C:\\TEST\\battery_report.html\"";

            try
            {
                Process p = Process.Start(psi);

                p.WaitForExit();

                if (p.ExitCode == 0)
                {
                    string sHtmlFile = Environment.SystemDirectory + "\\battery-report.html";

                    Process p2 = new Process();
                    p2.StartInfo.FileName = sHtmlFile;
                    p2.StartInfo.UseShellExecute = true;
                    p2.StartInfo.Verb = "open";
                    p2.Start();
                }
                else
                {
                    MessageBoxEx.Show("Something went wrong!\r\n\r\nProcess Exit Code: " + p.ExitCode.ToString() + "\r\n(" + Environment.SystemDirectory + "\\powercfg.exe" + ")", csAPP_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error, true /*bTopMost*/);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("Something went wrong!\r\n\r\nError: " + ex.Message, csAPP_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error, true /*bTopMost*/);
            }
        }
    }
}
