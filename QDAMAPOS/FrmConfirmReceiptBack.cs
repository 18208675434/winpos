using QDAMAPOS.Common;
using QDAMAPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QDAMAPOS
{
    public partial class FrmConfirmReceiptBack : Form
    {

        private Receiptdetail Parareceipt;
       
        public FrmConfirmReceiptBack()
        {
            InitializeComponent();
        }
        public FrmConfirmReceiptBack(Receiptdetail receipt)
        {
            InitializeComponent();
            Parareceipt = receipt;
        }
       
        private void FrmConfirmBack_Load(object sender, EventArgs e)
        {
            try
            {
                frmConfirmReceipt frmdelete = new frmConfirmReceipt(Parareceipt);

                frmdelete.TopMost = true;
                frmdelete.DataReceiveHandle += FormDelete_DataReceiveHandle;
                frmdelete.Show();
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载确认窗体窗体异常" + ex.Message, true);
                this.Close();
            }
        }



        private void FormDelete_DataReceiveHandle(int type)
        {
            try
            {
                if (type == 0)
                {
                    this.DialogResult = DialogResult.Cancel;
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }



            }
            catch (Exception ex)
            {
                this.Close();
                LogManager.WriteLog("ERROR", "处理确认窗体结果异常" + ex.Message);
            }
            finally
            {
                this.Close();
            }
        }
    }
}
