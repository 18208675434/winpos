using WinSaasPOS.Common;
using WinSaasPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinSaasPOS
{
    public partial class frmExpenseSave : Form
    {

        private HttpUtil httputil = new HttpUtil();

        /// <summary>
        /// 按比例缩放页面及控件
        /// </summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        private string CurrentAddOrMinus = "+";

        public frmExpenseSave()
        {
            InitializeComponent();
        }


        private void btn_Click(object sender, EventArgs e)
        {
            try
            {

                Button button = (Button)sender;
                string temptext = txtNum.Text + button.Name.Replace("btn", "");


                decimal CheckDecimal = Convert.ToDecimal(txtNum.Text + button.Name.Replace("btn", ""));

                if (CheckDecimal > 100000)
                {
                    return;
                }


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
            catch (Exception ex)
            {

            }
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
                if (string.IsNullOrEmpty(txtNum.Text))
                {
                    return;
                }
                if (string.IsNullOrEmpty(CurrentExpenseid))
                {
                    return;
                }
                try
                {
                    double doublenum = Convert.ToDouble(txtNum.Text);
                    if (doublenum <= 0)
                    {
                        return;
                    }
                }
                catch
                {
                    return;
                }

                string ErrorMsg = "";
                bool result = false;

                result = httputil.SaveExpense(CurrentExpenseid, Convert.ToDecimal(CurrentAddOrMinus+txtNum.Text), MainModel.CurrentShopInfo.shopid, ref ErrorMsg);

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

            if (!string.IsNullOrEmpty(ErrorMsg) || lstexpensetype == null)
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


            if (pnlExpenses.Controls.Count > 0)
            {
                Button btn = (Button)pnlExpenses.Controls[0];
                if (btn.Enabled == true)
                {
                    btnType_Click(btn, new EventArgs());
                }

            }
        }

        private void AddTypeButton(ExpenseType datum)
        {

            int count = pnlExpenses.Controls.Count;
            Button btntemp = new Button();
            btntemp.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            btntemp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            //btntemp.Name = "button1";
            btntemp.Size = new System.Drawing.Size((pnlExpenses.Width-20)/3, 41);
            btntemp.TabIndex = 0;
            btntemp.Text = datum.description;
            btntemp.UseVisualStyleBackColor = true;
            btntemp.Tag = datum.expensesid;

         

            btntemp.Click += new System.EventHandler(btnType_Click);

            int inty = 0;
            int intx = 0;

            if (count > 0)
            {
                inty = count / 3;
                intx = count % 3;
            }

            int left = intx * (pnlExpenses.Width / 3) + 5;
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

               foreach (Control con in pnlExpenses.Controls)
               {
                   con.BackColor = Color.White;
               }

               btn.BackColor = Color.SkyBlue;

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
            btnAdd_Click(null, null);
            LoadExpenses();
        }

        private void btnDot_Click(object sender, EventArgs e)
        {

            if (txtNum.Text.Length > 0 && !txtNum.Text.Contains("."))
            {
                txtNum.Text += ".";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CurrentAddOrMinus = "+";
            btnAdd.ForeColor = Color.White;
            btnAdd.BackColor = Color.SkyBlue;
            
            btnMinus.ForeColor = Color.Black;
            btnMinus.BackColor = Color.White;
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            CurrentAddOrMinus = "-";

            btnAdd.ForeColor = Color.Black;
            btnAdd.BackColor = Color.White;

            btnMinus.ForeColor = Color.White ;
            btnMinus.BackColor = Color.SkyBlue;
        }

        private void txtNum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double doublenum = Convert.ToDouble(txtNum.Text);

                if (doublenum > 0)
                {
                    btnOK.BackColor = Color.OrangeRed;
                }
                else
                {
                    btnOK.BackColor = Color.Silver;
                }
            }
            catch
            {
                btnOK.BackColor = Color.Silver;
            }
        }

    }
}
