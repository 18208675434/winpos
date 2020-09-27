using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.ReturnWithoutOrder
{
    public partial class FormReturnCashOrBalance : Form
    {
        private Cart thisCurrentCart = null;
        private Member thisCurrentMember = null;

        private HttpUtil httputil = new HttpUtil();
        public FormReturnCashOrBalance(Cart cart, Member member)
        {
            InitializeComponent();
            thisCurrentCart = cart;
            thisCurrentMember = member;
        }

        private void FormReturnCash_Load(object sender, EventArgs e)
        {
            lblReturnCash.Text = thisCurrentCart.totalpayment.ToString("f2");
            lblProCount.Text = thisCurrentCart.productcount.ToString();
            lblRefundPoint.Text = thisCurrentCart.returnpoint.ToString();
        }

        private void FormReturnCash_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        private void btnReturnBalance_Click(object sender, EventArgs e)
        {
            if (ReturnWithoutOrderHelper.ShowFormRetunCash(thisCurrentCart, thisCurrentMember, ReturnType.balance))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void btnReturnCash_Click(object sender, EventArgs e)
        {
            if (ReturnWithoutOrderHelper.ShowFormRetunCash(thisCurrentCart, thisCurrentMember, ReturnType.cash))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }


    }
}
