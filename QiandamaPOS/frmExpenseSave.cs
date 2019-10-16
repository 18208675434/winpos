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
    public partial class frmExpenseSave : Form
    {

        private HttpUtil httputil = new HttpUtil();

        /// <summary>
        /// 按比例缩放页面及控件
        /// </summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        public frmExpenseSave()
        {
            InitializeComponent();
        }



        private void btn_Click(object sender, EventArgs e)
        {

            Button button = (Button)sender;
            string temptext = txtNum.Text + button.Name.Replace("btn", "");


            //小数点后允许两位  现金抹零后不允许再输入零
            if (txtNum.Text.Length > 3 && txtNum.Text.Substring(txtNum.Text.Length - 3, 1) == ".")
            {
                return;
            }

            try
            {
                Convert.ToDecimal(temptext);
                txtNum.Text = temptext;
            }
            catch
            {

            }
            //txtNum.Text += button.Name.Replace("btn", "");
        }


        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtNum.Text.Length > 0)
            {
                txtNum.Text = txtNum.Text.Substring(0, txtNum.Text.Length - 1);
            }
        }

        public void frmExpenseSave_SizeChanged(object sender, EventArgs e)
        {
            asf.ControlAutoSize(this);
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNum.Text))
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(CurrentExpenseid))
                {
                    return;
                }

                string ErrorMsg = "";
                bool result = false;

                if (rdoFu.Checked)
                {
                    result = httputil.SaveExpense(CurrentExpenseid, Convert.ToDecimal("-"+txtNum.Text), MainModel.CurrentShopInfo.shopid, ref ErrorMsg);
                }
                else
                {
                    result = httputil.SaveExpense(CurrentExpenseid, Convert.ToDecimal(txtNum.Text), MainModel.CurrentShopInfo.shopid, ref ErrorMsg);
                }
                     


                if (result)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MainModel.ShowLog("增加营业外支出失败", true);
                }

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("增加营业外支出异常："+ex.Message,true);
            }
         
        }


        private void LoadExpenses()
        {
            string ErrorMsg = "";
            List<ExpenseType> lstexpensetype = httputil.GetExpenses(ref ErrorMsg);

            if (!string.IsNullOrWhiteSpace(ErrorMsg) || lstexpensetype == null)
            {
                MainModel.ShowLog(ErrorMsg, false);
            }
            else
            {
                foreach (ExpenseType dat in lstexpensetype)
                {
                    if (dat.visibleflag == "1")
                    {
                        AddTypeButton(dat);

                    }
                }
            }
            

            //if (pnlCashCoupons.Controls.Count > 0)
            //{
            //    Button btn = (Button)pnlCashCoupons.Controls[0];
            //    if (btn.Enabled == true)
            //    {
            //        btnCash_Click(btn, new EventArgs());
            //    }

            //}
        }

        private void AddTypeButton(ExpenseType datum)
        {

            int count = pnlExpenses.Controls.Count;
            Button btntemp = new Button();
            btntemp.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //btntemp.Location = new System.Drawing.Point(23, 15);
            btntemp.Name = "button1";
            btntemp.Size = new System.Drawing.Size(148, 41);
            btntemp.TabIndex = 0;
            btntemp.Text = datum.description;
            btntemp.UseVisualStyleBackColor = true;
            btntemp.Tag = datum.expensesid;

         

            btntemp.Click += new System.EventHandler(btnType_Click);

            int inty = 0;
            int intx = 0;

            if (count > 0)
            {
                inty = count / 2;
                intx = count % 2;
            }

            int left = intx * (pnlExpenses.Width / 2) + 5;
            int top = inty * (btntemp.Height + 5);

            pnlExpenses.Controls.Add(btntemp);
            btntemp.Location = new System.Drawing.Point(left, top);
            btntemp.Show();


        }


        private string CurrentExpenseid = "";
        private void btnType_Click(object sender, EventArgs e)
        {

            try
            {
               Button btn = (Button) sender;
               CurrentExpenseid = btn.Tag.ToString();

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("选择费用异常：" + ex.Message, true);
            }

        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmExpenseSave_Shown(object sender, EventArgs e)
        {
            rdoZheng.Checked = true;
            LoadExpenses();
        }

        private void btnDot_Click(object sender, EventArgs e)
        {

            if (txtNum.Text.Length > 0 && !txtNum.Text.Contains("."))
            {
                txtNum.Text += ".";
            }
        }

    }
}
