using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MenuUI
{
    public partial class FormOrderHangItem : Form
    {
        private Cart CurrentCart = new Cart();
        public FormOrderHangItem(Cart cart)
        {
            InitializeComponent();
            CurrentCart = cart;
        }

        private void FormOrderHangItem_Shown(object sender, EventArgs e)
        {
            LoadDgvCart();
        }

        private void LoadDgvCart()
        {
            try
            {

                dgvCart.Rows.Clear();
                if (CurrentCart == null || CurrentCart.products == null || CurrentCart.products.Count == 0)
                {
                    return;
                }
                foreach (Product pro in CurrentCart.products)
                {
                    string name = string.IsNullOrEmpty(pro.skuname) ? pro.title : pro.skuname ;

                    if (pro.goodstagid == 0)
                    {
                        dgvCart.Rows.Add(name + "\r\n" + pro.skucode, pro.num, "￥"+pro.price.total.ToString("f2"));
                    }
                    else
                    {
                        dgvCart.Rows.Add(name + "\r\n" + pro.skucode, pro.specnum+pro.price.unit, "￥" + pro.price.total.ToString("f2"));
                    }
                    
                }

                dgvCart.ClearSelection();


            }
            catch (Exception ex)
            {
                MainModel.ShowLog("",true);
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
