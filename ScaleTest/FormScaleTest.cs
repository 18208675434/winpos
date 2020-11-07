using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ScaleTest
{
    public partial class FormScaleTest : Form
    {
        private Scale_Action scaleAction = new Scale_LF();
        public FormScaleTest()
        {
            InitializeComponent();
        }

        private void btnGetWeight_Click(object sender, EventArgs e)
        {
            ScaleResult result = scaleAction.GetScaleWeight();
            if (result.WhetherSuccess)
            {
                listResult.Items.Insert(0, string.Format("{0}->稳定{1}，净重{2}，皮重{3},总重{4}", btnGetWeight.Text, result.WhetherStable, result.NetWeight, result.TareWeight, result.TotalWeight));
            }
            else
            {
                listResult.Items.Insert(0, string.Format("{0}->{1}:{2}", btnGetWeight.Text, result.WhetherStable, result.Message));
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(scaleAction.Open(txtCom.Text, 9600).ToString());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(scaleAction.Close().ToString());
        }


        private void btnTare_Click(object sender, EventArgs e)
        {
            ScaleResult result = scaleAction.SetTare();
            listResult.Items.Insert(0, string.Format("{0}->{1}:{2}", btnTare.Text, result.WhetherSuccess, result.Message));
        }

        private void btnNumTare_Click(object sender, EventArgs e)
        {
            ScaleResult result = scaleAction.SetTareByNum((int)numTare.Value);
            listResult.Items.Insert(0, string.Format("{0}->{1}:{2}", btnNumTare.Text, result.WhetherSuccess, result.Message));
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            ScaleResult result = scaleAction.SetZero();
            listResult.Items.Insert(0, string.Format("{0}->{1}:{2}", btnZero.Text, result.WhetherSuccess, result.Message));
        }

        private void btnClearTare_Click(object sender, EventArgs e)
        {
            ScaleResult result = scaleAction.ClearTare();
            listResult.Items.Insert(0,string.Format("{0}->{1}:{2}", btnClearTare.Text, result.WhetherSuccess, result.Message));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listResult.Items.Clear();
        }
    }
}
