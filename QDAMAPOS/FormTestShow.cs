using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QDAMAPOS
{
    public partial class FormTestShow : Form
    {
        public FormTestShow()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, EventArgs e)
        {

        }

        private void FormTestShow_Load(object sender, EventArgs e)
        {
            frmNumber frm = new frmNumber();
            frm.Show();
            this.Close();
        }
    }
}
