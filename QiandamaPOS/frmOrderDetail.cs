using QiandamaPOS.Common;
using QiandamaPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QiandamaPOS
{
    public partial class frmOrderDetail : Form
    {


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        public frmOrderDetail(Orderpricedetail pricedetail)
        {
            InitializeComponent();
            IniForm(pricedetail);

        }
        private void IniForm(Orderpricedetail pricedetail)
    {
        lblTitle.Text = pricedetail.title;
        lblAmount.Text = pricedetail.amount;
        Application.DoEvents();
       // MessageBox.Show(this.Width.ToString());
    }

        public void frmOrderDetail_SizeChanged(object sender, EventArgs e)
        {
            asf.ControlAutoSize(this);
        }
    }
}
