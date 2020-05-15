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
    public partial class frmToolMain : Form
    {

        public delegate void DataRecHandleDelegate(ToolType tooltype);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataRecHandleDelegate DataReceiveHandle;


        public frmToolMain()
        {
            InitializeComponent();
          
            
        }

        private void frmToolMain_Deactivate(object sender, EventArgs e)
        {
           
             this.Hide(); //this.Close();
        }

        private void pnlReceipt_Click(object sender, EventArgs e)
        {

            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(ToolType.Receipt, null, null);

             this.Hide(); //this.Close();
        }

        private void pnlExit_Click(object sender, EventArgs e)
        {
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(ToolType.Exit, null, null);

             this.Hide(); //this.Close();
        }

        private void pnlExpense_Click(object sender, EventArgs e)
        {
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(ToolType.Expense, null, null);

             this.Hide(); //this.Close();
        }


        private void pnlScale_Click(object sender, EventArgs e)
        {
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(ToolType.Scale, null, null);

             this.Hide(); //this.Close();
        }

        private void pnlReceiptQuery_Click(object sender, EventArgs e)
        {
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(ToolType.ReceiptQuery, null, null);

             this.Hide(); //this.Close();
        }

        private void pnlPrintSet_Click(object sender, EventArgs e)
        {
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(ToolType.PrintSet, null, null);

             this.Hide(); //this.Close();
        }


        private void pnlChangeMode_Click(object sender, EventArgs e)
        {
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(ToolType.ChangeMode, null, null);

             this.Hide(); //this.Close();
        }

        private void frmToolMain_Shown(object sender, EventArgs e)
        {
            try
            {
                pnlLine1.Height = 1;
                pnlLine2.Height = 1;
                //pnlLine3.Height = 1;
                pnlLine4.Height = 1;
                pnlLine5.Height = 1;
                pnlLine6.Height = 1;
                pnlLine7.Height = 1;

                lblDeviceSN.Text = "设备号:" + MainModel.DeviceSN;
                lblVersion.Text = "版本号:" + MainModel.Version;
            }
            catch (Exception ex)
            {

            }
        }

    }

    public enum ToolType
    {
        Exit,
        Receipt,
        Expense,
        ChangeMode,
        Scale,
        ReceiptQuery,
        PrintSet
    }
}
