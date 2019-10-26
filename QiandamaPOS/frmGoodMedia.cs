using QiandamaPOS.Common;
using QiandamaPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace QiandamaPOS
{
    public partial class frmGoodMedia : Form
    {
        
        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();
        public frmGoodMedia(Product pro)
        {
            InitializeComponent();

            Ini(pro);
        }

        private void Ini(Product pro)
        {
            lblProName.Text = pro.title;
            lblBarCode.Text = pro.barcode;
            lblItemPrice.Text ="￥"+ pro.price.saleprice.ToString();

            //0 是标品  1是散称
            if (pro.goodstagid == 0)
            {
                lblNum.Text = pro.num.ToString();
                lblSalePrice.Text = "￥" + pro.price.total.ToString();

            }
            else
            {
                lblNum.Text = pro.price.specnum.ToString() + pro.price.unit;
                lblSalePrice.Text = "￥" + pro.price.total.ToString();

            }
            
        }

        public void frmGoodMedia_SizeChanged(object sender, EventArgs e)
        {
           asf.ControlAutoSize(this);
        }

    }
}
