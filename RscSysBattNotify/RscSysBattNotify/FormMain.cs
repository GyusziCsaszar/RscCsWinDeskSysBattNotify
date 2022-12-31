﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Reflection;
using System.Runtime.InteropServices;

using Ressive.Utils;

namespace RscSysBattNotify
{
    public partial class FormMain : Form
    {

        protected const string csAPP_TITLE = "Rsc System Battery Notify v1.05";
        protected const string csAPP_NAME = "RscSysBattNotify";

        private int m_iBatteryLifePercentPrev = -1;
        private int m_iBatteryLifePercentPrevTenths = -1;

        private NotifyIcon m_notifyIcon = null;

        // SRC: https://stackoverflow.com/questions/12026664/a-generic-error-occurred-in-gdi-when-calling-bitmap-gethicon
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);

        public FormMain()
        {
            InitializeComponent();

            this.Text = csAPP_TITLE;

            //Hide Caption Bar
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            PlaceWindow();

            chbAutoStart.Checked = IsAppStartWithWindowsOn();

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

                this.Visible = true;
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

            lblBatteryLifeValue.Text = GetPowerStatusValueAsString("BatteryLifePercent");
            int iBattPerc = 0;
            if (!Int32.TryParse(lblBatteryLifeValue.Text.Substring(0, 3).Trim(), out iBattPerc))
            {
                iBattPerc = Int32.Parse(lblBatteryLifeValue.Text.Substring(0, 2).Trim());
            }
            if (iBattPerc >= 90)
            {
                lblBatteryLifeValue.ForeColor = Color.Green;
            }
            else if (iBattPerc <= 10)
            {
                lblBatteryLifeValue.ForeColor = Color.Red;
            }
            else
            {
                lblBatteryLifeValue.ForeColor = Color.Orange;
            }
            int iBattPercTenths = iBattPerc / 10;
            if (m_iBatteryLifePercentPrevTenths != iBattPercTenths && m_iBatteryLifePercentPrevTenths > 0)
            {
                if (!Visible) Visible = true;
            }
            m_iBatteryLifePercentPrevTenths = iBattPercTenths;
            m_iBatteryLifePercentPrev = iBattPerc;

            lblBatteryFullLifetimeValue.Text = GetPowerStatusValueAsString("BatteryFullLifetime");
            lblBatteryLifeRemainingValue.Text = GetPowerStatusValueAsString("BatteryLifeRemaining");
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

                        DateTime dt = new DateTime(1, 1, 1, iHour, iMin, iSec);
                        return dt.ToShortTimeString();
                    }
                    else if (sName == "BatteryChargeStatus")
                    {
                        string sVal = oVal.ToString();
                        if (sVal == "0") return "Discharging";
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

            registryKey.Dispose();
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
    }
}
