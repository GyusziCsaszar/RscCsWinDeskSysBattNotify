using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Ressive.Utils;

namespace RscSysBattNotify
{
    public partial class FormGraph : Form
    {
        public FormGraph()
        {
            InitializeComponent();

            tbLogPath.Text = StorageRegistry.Read("LogPath", "");

            chbDoLog.Checked = (StorageRegistry.Read("DoLog", 0) > 0);
        }

        private void btnLogPath_Click(object sender, EventArgs e)
        {

            SaveFileDialog dlg = new SaveFileDialog();

            dlg.Filter = "Log Files (*.log)|*.log|Text Files (*.txt)|*.txt|CSV Files (*.csv)|*.csv";
            dlg.FilterIndex = 0;
            dlg.DefaultExt = "log";

            if (DialogResult.OK == dlg.ShowDialog())
            {
                tbLogPath.Text = dlg.FileName;
            }

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            DoApply();
        }

        private bool DoApply()
        {
            if (tbLogPath.Text.Length > 0)
            {
                if (!System.IO.File.Exists(tbLogPath.Text))
                {
                    try
                    {
                        System.IO.File.AppendAllText(tbLogPath.Text, "YYYY;MM;DD;hh;mm;ss;fff;ppp;s\r\n");
                    }
                    catch (Exception exc)
                    {
                        MessageBoxEx.Show("LOG path is not accessible!\r\n\r\nError: " + exc.Message, FormMain.csAPP_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error, true /*bTopMost*/);
                        return false;
                    }
                }
            }

            StorageRegistry.Write("LogPath", tbLogPath.Text);

            StorageRegistry.Write("DoLog", chbDoLog.Checked ? 1 : 0);

            return true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (DoApply())
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void FormGraph_Paint(object sender, PaintEventArgs e)
        {

            Pen pen;

            Point ptBottomLeft = new Point();
            ptBottomLeft.X = 15;
            ptBottomLeft.Y = 112;

            Point ptTopRight = new Point();
            ptTopRight.X = ClientRectangle.Width - 15;
            ptTopRight.Y = 10;

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
    }
}
