using WinSaasPOS_Scale.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinSaasPOS_Scale
{

    public partial class frmLoading : Form
    {


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();
        public frmLoading()
        {
            InitializeComponent();

        }

        public void UpInfo(string msg)
        {
            if (msg.Contains("|"))
            {
                string[] strs = msg.Split('|');
                lblMsg1.Text = strs[0];
                lblMsg2.Text = strs[1];
            }
            else
            {
                lblMsg1.Text = msg;
                lblMsg2.Text = "";
            }        
        }



        private void LoaderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           

        }

        private void frmLoading_SizeChanged(object sender, EventArgs e)
        {
           //asf.ControlAutoSize(this);
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            try
            {
                this.Dispose();
                this.Close();

            }catch{}
        }

    }

    class CONSTANTDEFINE
    {     

        public delegate void SetUISomeInfo();
    
    }




}
