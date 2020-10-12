using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.HelperUI
{
    public partial class FormNumber : Form
    {

        public string NumberValue ="";

        private NumberType CurrentNumberType=NumberType.BarCode;


        /// <summary>
        /// 当前页面唯一标识
        /// </summary>
        public string ThisGuid = Guid.NewGuid().ToString();
        public FormNumber()
        {
            InitializeComponent();
        }

        private void FormNumber_Resize(object sender, EventArgs e)
        {
            SetWindowRegion();
        }
        /// <summary>
        /// 设置窗体的Region   画半径为10的圆角
        /// </summary>
        public void SetWindowRegion()
        {
            try
            {
                GraphicsPath FormPath;
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                FormPath = GetRoundedRectPath(rect, 10);
                this.Region = new Region(FormPath);
            }
            catch (Exception ex)
            {
            }

        }
        /// <summary>
        /// 绘制圆角路径
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            try
            {
                int diameter = radius;
                Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
                GraphicsPath path = new GraphicsPath();

                // 左上角
                path.AddArc(arcRect, 180, 90);

                // 右上角
                arcRect.X = rect.Right - diameter;
                path.AddArc(arcRect, 270, 90);

                // 右下角
                arcRect.Y = rect.Bottom - diameter;
                path.AddArc(arcRect, 0, 90);

                // 左下角
                arcRect.X = rect.Left;
                path.AddArc(arcRect, 90, 90);
                path.CloseFigure();//闭合曲线
                return path;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public void UpInfo(string title, NumberType numbertype)
        {
            try
            {
                NumberValue = "";
                txtNum.Text = "";
                lblInfo.Text = title;
                CurrentNumberType = numbertype;
                switch (CurrentNumberType)
                {
                    case NumberType.BarCode: txtNum.WaterText = "输入商品条码"; lblg.Visible = false; txtNum.Width = rbtnOK.Width; txtNum.Width = txtNum.Width - (txtNum.Left - txtNum.Left) - 4; break;
                    case NumberType.MemberCode: txtNum.WaterText = "输入会员手机号"; lblg.Visible = false; txtNum.Width = rbtnOK.Width; txtNum.Width = txtNum.Width - (txtNum.Left - txtNum.Left) - 4; break;
                    case NumberType.ProWeight: txtNum.WaterText = "请输入实际重量"; lblg.Visible = true; txtNum.Width = lblg.Left - txtNum.Left - 2; txtNum.Width = txtNum.Width - (txtNum.Left - txtNum.Left) - 4; break;
                    case NumberType.TareWeight: txtNum.WaterText = "请输入皮重"; lblg.Visible = true; txtNum.Width = lblg.Left - txtNum.Left - 2; txtNum.Width = txtNum.Width - (txtNum.Left - txtNum.Left) - 4; break;
                    case NumberType.ProNum: txtNum.WaterText = "请输入实际数量"; lblg.Visible = false; txtNum.Width = rbtnOK.Width; txtNum.Width = txtNum.Width - (txtNum.Left - txtNum.Left) - 4; break;
                    case NumberType.BindingMember: txtNum.WaterText = "请输入需绑定的会员手机号"; lblg.Visible = false; txtNum.Width = rbtnOK.Width; txtNum.Width = txtNum.Width - (txtNum.Left - txtNum.Left) - 4; break;
                    case NumberType.GiftCardNo: txtNum.WaterText = "请输入礼品卡卡号"; lblg.Visible = false; txtNum.Width = rbtnOK.Width; txtNum.Width = txtNum.Width - (txtNum.Left - txtNum.Left) - 4; break;
                    case NumberType.GiftCardPwd: txtNum.WaterText = "请输入礼品卡秘钥"; lblg.Visible = false; txtNum.Width = rbtnOK.Width; txtNum.Width = txtNum.Width - (txtNum.Left - txtNum.Left) - 4; break;
                    case NumberType.BindingEntryCard: txtNum.WaterText = "请输入或扫描实体卡号"; lblg.Visible = false; txtNum.Width = rbtnOK.Width; txtNum.Width = txtNum.Width - (txtNum.Left - txtNum.Left) - 4; break;
                
                }

                rbtnOK.allbackcolor = Color.LightGray;
                rbtnOK.WhetherEnable = false;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("刷新数字窗体异常" + ex.Message);
            }
        }


        private void btnCancle_Click(object sender, EventArgs e)
        {
            Control btn = (Control)sender;
            UpdatNumberUtil(btn.Name);
            this.Close();
        }

        //下一步需要判断实收现金是否足够
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (txtNum.Text.Length == 0)
                {
                    return;
                }

                if (CurrentNumberType == NumberType.MemberCode)
                {
                    if (txtNum.Text.Length != 11)
                    {
                        MainModel.ShowLog("手机号格式不正确！",false);

                        return;
                    }

                    if (txtNum.Text.Length > 0 && txtNum.Text.Substring(0, 1) != "1")
                    {
                        MainModel.ShowLog("手机号格式不正确！", false);

                        return;
                    }
                }

                if (CurrentNumberType == NumberType.ProWeight)
                {

                    if (Convert.ToDouble(txtNum.Text) == 0)
                    {
                        return;
                    }
                }


                this.DialogResult = DialogResult.OK;

                NumberValue = txtNum.Text;
                this.Close();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("数字窗体关闭异常" + ex.StackTrace);
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                UpdatNumberUtil(btn.Name);

                Button button = (Button)sender;

                //条码控制20位
                if (CurrentNumberType == NumberType.BarCode && txtNum.Text.Length >= 20)
                {
                    return;
                }

                //会员号控制11位
                if (CurrentNumberType == NumberType.MemberCode && txtNum.Text.Length >= 11)
                {
                    return;
                }

                //商品重量控制最大6位
                if (CurrentNumberType == NumberType.ProWeight && txtNum.Text.Length >= 6)
                {
                    return;
                }

                //商品重量控制最大6位
                if (CurrentNumberType == NumberType.TareWeight && txtNum.Text.Length >= 4)
                {
                    return;
                }

                //控制重量信息第一位不为0
                if (CurrentNumberType == NumberType.ProWeight && txtNum.Text.Length >= 5 && button.Name == "btn00")
                {
                    return;
                }
                if (CurrentNumberType == NumberType.ProWeight && txtNum.Text == "" && (button.Name == "btn00" || button.Name == "btn0"))
                {
                    return;
                }


                txtNum.Text += button.Name.Replace("btn", "");
            }
            catch { }
            finally
            {
               // txtNum.Focus();
            }
        }


        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                UpdatNumberUtil(btn.Name);

                if (txtNum.Text.Length > 0)
                {
                    txtNum.Text = txtNum.Text.Substring(0, txtNum.Text.Length - 1);
                }
            }
            catch { }


        }


        //控制仅允许录入数字
        private void TextNUMBER_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox txt = sender as TextBox;
                e.Handled = true;
                char ch = e.KeyChar;

                if (ch >= '0' && ch <= '9')
                    e.Handled = false;


                if (ch == '.')
                    e.Handled = false;

                if (ch == (char)Keys.Back)
                    e.Handled = false;

            }
            catch { }
        }

        /// <summary>
        /// 防止死循环
        /// </summary>
        bool thisEnable = true;
        private void UpdatNumberUtil(string btnname)
        {


            if (thisEnable)
            {
                NumberUtil.BtnName = btnname;
                NumberUtil.FormGuid = ThisGuid;

                NumberUtil.IsUpdate = true;
            }
            else
            {
                thisEnable = true;
            }

        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            try{
                if (keyData == Keys.Enter)
                {
                    btnOK_Click(null,null);
                    return false;
                }
                return false;
            }catch{
                return false;
            }

        }

        private void txtNum_DataChanged(string data)
        {
            try
            {
                //double doublenum = Convert.ToDouble(txtNum.Text);
                decimal doublenum = 0;
                bool changesuccess = decimal.TryParse(txtNum.Text, out doublenum);
                if (changesuccess && doublenum > 0)
                {
                    rbtnOK.allbackcolor = Color.FromArgb(20, 137, 205);
                    rbtnOK.WhetherEnable = true;
                }
                else
                {
                    rbtnOK.allbackcolor = Color.LightGray;
                    rbtnOK.WhetherEnable = false;
                }
            }
            catch (Exception ex)
            {
                rbtnOK.BackColor = Color.LightGray;
                rbtnOK.WhetherEnable = true;

                LogManager.WriteLog("数字窗体控件数据变化检测异常" + ex.Message);
            }
           
        }

    }
}
