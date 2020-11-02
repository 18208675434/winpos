using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinSaasPOS.HelperUI
{
    public partial class FormBackGround : Form
    {
        public FormBackGround()
        {
            InitializeComponent();
        }

        private void FormBackGround_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
