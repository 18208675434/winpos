using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZhuiZhi_Integral_Scale_UncleFruit.ReturnWithoutOrder
{
    public partial class FormCheckSmCode : Form
    {
        public string CurrentPhone = "";
        public FormCheckSmCode(string name,string phone)
        {
            InitializeComponent();

            CurrentPhone = phone;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
