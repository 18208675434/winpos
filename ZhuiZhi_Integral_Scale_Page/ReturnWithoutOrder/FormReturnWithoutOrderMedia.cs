using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.ReturnWithoutOrder
{
    public partial class FormReturnWithoutOrderMedia : Form
    {
        public FormReturnWithoutOrderMedia()
        {
            InitializeComponent();

            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void FormReturnWithoutOrderMedia_Shown(object sender, EventArgs e)
        {
            lblTime.Text = MainModel.Titledata;
            lblShopName.Text = "欢迎光临 " + MainModel.CurrentShopInfo.shopname;
        }

        public void UpdateForm(Cart cart,Member member)
        {
            try
            {
                if (member == null)
                {
                    ClearMember();
                }
                else
                {
                    LoadMember(member);
                }
                if (cart == null || cart.products == null || cart.products.Count == 0)
                {
                    ClearForm();
                }
                else
                {
                    dgvGood.Rows.Clear();

                    foreach (Product pro in cart.products)
                    {
                        string title = string.IsNullOrEmpty(pro.title) ? pro.skuname : pro.title;
                        dgvGood.Rows.Add(title + "\r\n" + pro.skucode, pro.price.saleprice.ToString("f2"), pro.goodstagid == 0 ? pro.num : pro.specnum, pro.price.total.ToString("f2"));
                    }

                    dgvGood.ClearSelection();
                    lblPrice.Text = cart.totalpayment.ToString("f2");
                    int count = cart.products.Count;
                    int goodscount = 0;
                    foreach (Product pro in cart.products)
                    {
                        goodscount += pro.num;
                    }

                    lblGoodsCount.Text = "(" + goodscount + "件商品)";
                }  
            }
            catch { }
        }

        public void ClearForm()
        {
            try
            {
                dgvGood.Rows.Clear();
                lblGoodsCount.Text = "(0件商品)";
                lblPrice.Text = "0.00";

            }
            catch
            {

            }
        }


        public void LoadMember(Member member)
        {
            lblMobil.Text = member.memberheaderresponsevo.mobile;
        }

        public void ClearMember()
        {
            lblMobil.Text = "";

            //lblWechartNickName.Text = "你好！";
        }

       
    }
}
