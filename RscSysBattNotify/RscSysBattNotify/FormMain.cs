using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Reflection;

namespace RscSysBattNotify
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            Rectangle rect = Screen.FromControl(this).WorkingArea; // Bounds;
            this.Left = rect.Left + (rect.Width - (this.Width + 5));
            this.Top = rect.Top + (rect.Height - (this.Height + 5));

            RefreshPowerStatus();
        }

        private void RefreshPowerStatus()
        {
            lblPowerLineValue.Text = GetPowerStatusValueAsString("PowerLineStatus");
            lblBatteryChargeValue.Text = GetPowerStatusValueAsString("BatteryChargeStatus");

            lblBatteryLifeValue.Text = GetPowerStatusValueAsString("BatteryLifePercent");
            int iBattPerc = Int32.Parse(lblBatteryLifeValue.Text.Substring(0, 3).Trim());
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
            Close();
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            RefreshPowerStatus();
        }
    }
}
