using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MyControl
{
    public partial class NumberTextBox : System.Windows.Forms.UserControl
    {

        public delegate void DataRecHandleDelegate(string data);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataRecHandleDelegate DataChanged;


        public NumberTextBox()
        {
            InitializeComponent();
        }

        [Category("扩展属性"), Description("水印文字")]
        public string WaterText
        {
            get { return lblShuiyin.Text; }
            set
            {
                lblShuiyin.Text = value;
                lblShuiyin.BackColor = this.BackColor;
            }
        }



        public bool lockfocus = false;
        [Category("扩展属性"), Description("是否锁定焦点")]
        public bool LockFocus
        {
            get { return lockfocus; }
            set
            {
                lockfocus = value;
            }
        }

        public bool onlynumber = false;
        [Category("扩展属性"), Description("是否控制仅数字")]
        public bool OnlyNumber
        {
            get { return onlynumber; }
            set
            {
                onlynumber = value;
            }
        }

        public int decimaldigits = 0;
        [Category("扩展属性"), Description("小数位数  为0 则代表只能是整数")]
        public int DecimalDigits
        {
            get { return decimaldigits; }
            set
            {
                decimaldigits = value;
            }
        }

        public long maxdecimal = 1000000;
        [Category("扩展属性"), Description("数字类型时最大数  0则不限制")]
        public long MaxDeciaml
        {
            get { return maxdecimal; }
            set
            {
                maxdecimal = value;
            }
        }

        public int maxlength = 32767;
        [Category("扩展属性"), Description("最大长度")]
        public int MaxLength
        {
            get { return maxlength; }
            set
            {
                maxlength = value;
            }
        }


        [Category("扩展属性"), Description("文本框内容")]
        public string Text
        {
            get { return txtData.Text; }
            set
            {
                txtData.Text = value;
            }
        }

        public bool needboard = false;
        [Category("扩展属性"), Description("是否需要键盘")]
        public bool NeedBoard
        {
            get { return needboard; }
            set
            {
                needboard = value;
            }
        }

        private void myTextBox_SizeChanged(object sender, EventArgs e)
        {
            txtData.Width = this.Width - 10;
            txtData.Location = new Point(5, (this.Height - txtData.Height) / 2);

            lblShuiyin.Location = new Point(txtData.Location.X + 5, txtData.Location.Y);
            lblShuiyin.Size = new Size(txtData.Width - 10, txtData.Height);
        }

        private void myTextBox_FontChanged(object sender, EventArgs e)
        {
            txtData.Font = this.Font;
            lblShuiyin.Font = this.Font;

            txtData.Width = this.Width - 10;
            txtData.Location = new Point(5, (this.Height - txtData.Height) / 2);

            lblShuiyin.Location = new Point(txtData.Location.X + 5, txtData.Location.Y);
            lblShuiyin.Size = new Size(txtData.Width - 10, txtData.Height);
        }

        private void lblShuiyin_Click(object sender, EventArgs e)
        {
            txtData.Focus();

            if (needboard)
            {
                ZhuiZhi_Integral_Scale_UncleFruit.Common.GlobalUtil.OpenOSK();
            }
        }

        private void myTextBox_BackColorChanged(object sender, EventArgs e)
        {
            txtData.BackColor = this.BackColor;
            lblShuiyin.BackColor = this.BackColor;
        }

        private void myTextBox_Leave(object sender, EventArgs e)
        {
            if (lockfocus)
            {
                this.Focus();
            }
        }

        bool refreshSelectionStart = false;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            try
            {
                string str = txtData.Text;
                //限制总长度
                if (str.Length > maxlength)
                {
                    txtData.Text = str.Substring(0, maxlength);
                    refreshSelectionStart = true;
                    return;
                }

                if (onlynumber)
                {
                    if (decimaldigits > 0)
                    {
                        str = Regex.Replace(str, @"[^\d.\d]", "");

                        if (maxdecimal>0 && Convert.ToDecimal(str) >= maxdecimal)
                        {
                            str = str.Substring(0, str.Length - 1);
                            refreshSelectionStart = true;
                        }
                    }
                    else
                    {
                        str = Regex.Replace(str, @"[^\d]", "");
                    }

                    if (!refreshSelectionStart)
                    {
                        refreshSelectionStart = txtData.Text != str;
                    }



                    txtData.Text = str;

                    CashInputValidate(txtData);

                    if (!refreshSelectionStart)
                    {
                        refreshSelectionStart = txtData.Text != str;
                    }

                    if (refreshSelectionStart)
                    {
                        refreshSelectionStart = false;
                        txtData.SelectionStart = txtData.Text.Length;
                    }
                }

               




                // str = Regex.Replace(str, @"[^\d.\d]", "");
            }
            catch
            {

            }
            finally
            {
                try
                {
                    if (refreshSelectionStart)
                    {
                        refreshSelectionStart = false;
                        txtData.SelectionStart = txtData.Text.Length;
                    }

                    if (txtData.SelectionStart == 0)
                        txtData.SelectionStart = txtData.TextLength;

                    lblShuiyin.Visible = string.IsNullOrEmpty(txtData.Text);

                    if (DataChanged != null)
                        this.DataChanged.BeginInvoke(txtData.Text, null, null);

                }
                catch { }
            }


        }


        /// <summary>
        /// 现金格式输入规范，小数点后只能输入两位
        /// </summary>
        /// <param name="txtPayCash"></param>
        public void CashInputValidate(TextBox txtCash)
        {
            //过滤两个小数点
            if (txtCash.Text.Contains(".."))
                txtCash.Text = txtCash.Text.Replace("..", ".");

            if (txtCash.TextLength > 0)
            {
                //第一位为"."时，自动在前面添加 0
                if (txtCash.Text.Substring(0, 1) == ".")
                    txtCash.Text = "0." + txtCash.Text.Substring(1, txtCash.TextLength - 1);

                //已输入过小数点后，不能再输入小数点
                if (txtCash.Text.Substring(0, txtCash.TextLength - 1).Contains('.'))
                    txtCash.Text = txtCash.Text.TrimEnd('.');

                //小数点后只保留固定位数
                if (txtCash.Text.Contains('.') && (txtCash.TextLength - txtCash.Text.IndexOf('.') > decimaldigits + 1))
                    txtCash.Text = txtCash.Text.Substring(0, txtCash.Text.IndexOf('.') + decimaldigits + 1);
            }
            //if (txtCash.TextLength > 1)
            //{
            //    //第一位输入0后，第二位自动加小数点
            //    if (txtCash.Text.Substring(0, 1) == "0" && txtCash.Text.Substring(0, 2) != "0.")
            //    {
            //        if (txtCash.Text.Substring(1, 1) != ".")
            //            txtCash.Text = "0." + txtCash.Text.Substring(1, txtCash.TextLength - 1);
            //    }
            //}
        }


        public void SelectAll()
        {
            txtData.SelectAll();
        }
        public void Clear()
        {
            txtData.Clear();
        }

        private void NumberTextBox_Load(object sender, EventArgs e)
        {
            txtData.Width = this.Width - 10;
            txtData.Location = new Point(5, (this.Height - txtData.Height) / 2);

            lblShuiyin.Location = new Point(txtData.Location.X + 5, txtData.Location.Y);
            lblShuiyin.Size = new Size(txtData.Width - 10, txtData.Height);
        }

    }
}
