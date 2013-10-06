using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReversiExe
{
    public partial class ChipControl : UserControl
    {
        public ChipControl()
        {
            InitializeComponent();
        }

        public ChipControl(int i)
        {
            InitializeComponent();
           // label1.Text = i.ToString();
        }
    }
}
