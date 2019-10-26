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
    public partial class frmCoupon : Form
    {
        /// <summary>
        /// 选中的券码
        /// </summary>
        public string SelectCouponCode = "";

        public string StrValue = "";

        /// <summary>
        /// 当前购物车
        /// </summary>
        private Cart CurrentCart = new Cart();
        public frmCoupon(Cart cart,string selectcoupon)
        {
            InitializeComponent();
            CurrentCart = (Cart)cart.qianClone();
            SelectCouponCode = selectcoupon;
            UpdateDgvCoupon(cart, selectcoupon);

            dgvCoupon.ClearSelection();
        }

        private void UpdateDgvCoupon(Cart cart, string selectcoupon)
        {
            try
            {
                lblTitle.Text = cart.availablecoupons.Length + "张优惠券可用";
                
                if (!string.IsNullOrEmpty(selectcoupon))
                {
                    btnCheckNone.Text = "";
                }
                dgvCoupon.Rows.Clear();
                if (cart != null && cart.availablecoupons != null && cart.availablecoupons.Length > 0)
                {
                    foreach (Availablecoupon couponsBean in cart.availablecoupons)
                    {
                        string content = "";
                        //if ("Cash" == couponsBean.catalog || "CashReduction" == couponsBean.catalog)
                        //{
                        //    //txt_flag.setVisibility(View.VISIBLE);
                        //    //txt_price.setText(String.format("%.2f", couponsBean.getAmount()));
                        //}
                        //else if ("DiscountCoupon".equals(couponsBean.getCatalog()))
                        //{
                        //    txt_flag.setVisibility(View.GONE);
                        //    txt_price.setText(couponsBean.getAmount() + "折");
                        //}
                        if ("Cash" == couponsBean.catalog)
                        {
                            content = "      现金券";
                        }
                        else if ("DiscountCoupon" == couponsBean.catalog)
                        {
                            if (couponsBean.orderminamount <= 0)
                            {
                                content = "    无门槛使用";
                            }
                            else
                            {
                                content = "    满" + couponsBean.orderminamount + "元使用";
                            }
                        }
                        else if ("CashReduction" == couponsBean.catalog)
                        {
                            if (couponsBean.orderminamount <= 0)
                            {
                                content = "    无门槛使用";
                            }
                            else
                            {
                                content = "    满" + couponsBean.orderminamount + "元使用";
                            }
                        }

                        string starttime = MainModel.GetDateTimeByStamp(couponsBean.enabledfrom.ToString()).ToString("yyyy-MM-dd");
                        string endtime = MainModel.GetDateTimeByStamp(couponsBean.enabledto.ToString()).ToString("yyyy-MM-dd");
                        string couponcode = couponsBean.couponcode;
                        string select = "";
                        if (selectcoupon == couponcode)
                        {
                            select = "√";
                        }
                        else
                        {
                            select = "□";
                        }
                        dgvCoupon.Rows.Add(couponcode, "￥" + couponsBean.amount, content + "\r\n" + starttime + "至" + endtime, select);
                    }


                    dgvCoupon.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR","加载优惠券列表异常"+ex.Message);
            }
        }

        private void frmCoupon_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void dgvCoupon_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.ColumnIndex == 2 && e.RowIndex >= 0 && e.Value != null)//要进行重绘的单元格
            {

                Graphics gpcEventArgs = e.Graphics;
                Color clrBack = e.CellStyle.BackColor;
                //Font fntText = e.CellStyle.Font;//获取单元格字体
                //先使用北京颜色重画一遍背景
                gpcEventArgs.FillRectangle(new SolidBrush(clrBack), e.CellBounds);


                if (!e.Value.ToString().Contains("\r\n"))
                {
                    return;
                }

                string tempstr = e.Value.ToString().Replace("\r\n", "*");
                string strLine1 = "";
                string strLine2 = "";

                strLine1 = tempstr.Split('*')[0];
                strLine2 = tempstr.Split('*')[1];

                string[] sts = tempstr.Split('*');
                //Size sizText = TextRenderer.MeasureText(e.Graphics, strFirstLine, fntText);
                int intX = e.CellBounds.Left + e.CellStyle.Padding.Left;
                int intY = e.CellBounds.Top + e.CellStyle.Padding.Top;
                int intWidth = e.CellBounds.Width - (e.CellStyle.Padding.Left + e.CellStyle.Padding.Right);
                //int intHeight = sizText.Height + (e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom);

                Font fnt1 = new System.Drawing.Font("微软雅黑", 13F);
                //Graphics g = this.CreateGraphics(); //this是指所有control派生出来的类，这里是个form

                SizeF size1 = this.CreateGraphics().MeasureString(strLine1, fnt1);
                //第一行
                TextRenderer.DrawText(e.Graphics, strLine1, fnt1, new Rectangle(intX, intY + 5, intWidth, (int)size1.Height),
                    Color.Black, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

                //另起一行
                Font fnt2 = new System.Drawing.Font("微软雅黑", 11F);
                SizeF size2 = this.CreateGraphics().MeasureString(strLine1, fnt1);
                intY = intY + (int)size1.Height + 10;
                TextRenderer.DrawText(e.Graphics, strLine2, fnt2, new Rectangle(intX, intY, intWidth, (int)size2.Height),
                    Color.DarkGray, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);


               

                e.Handled = true;

                dgvCoupon.ClearSelection();
            }
        }


        private void lblExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvCoupon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;                

                SelectCouponCode = dgvCoupon.Rows[e.RowIndex].Cells["couponcode"].Value.ToString();
                string amount = dgvCoupon.Rows[e.RowIndex].Cells["amount"].Value.ToString();
                int CouponAmount = Convert.ToInt16(amount.Replace("￥",""));

                if (CurrentCart.totalpayment <= CouponAmount)
                {
                    frmCouponOutcs frmcoupon = new frmCouponOutcs();
                    frmcoupon.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    frmcoupon.ShowDialog();

                    //收银完成
                    if(frmcoupon.DialogResult==DialogResult.OK)
                    {
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                    else
                    {

                    }
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

                dgvCoupon.ClearSelection();
                                
            }
            catch (Exception ex)
            {

            }
        }

        private void pnlCouponNone_Click(object sender, EventArgs e)
        {
            SelectCouponCode = "";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
