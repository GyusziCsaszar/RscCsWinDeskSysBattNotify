using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RscSysBattNotify
{
    public class BatteryLevelStore
    {
        public static List<BattLevel> BatteryLevelList = new List<BattLevel>();
    }

    public class BattLevel
    {
        public int iBattPerc;
        public Color clFore;
    }
}
