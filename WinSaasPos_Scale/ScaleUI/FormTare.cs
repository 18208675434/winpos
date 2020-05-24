using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WinSaasPOS_Scale.Common;
using WinSaasPOS_Scale.HelperUI;
using WinSaasPOS_Scale.Model;
using WinSaasPOS_Scale.ScaleFactory;

namespace WinSaasPOS_Scale.BaseUI
{
    public partial class FormTare : Form
    {
        public FormTare()
        {
            InitializeComponent();
        }


        //皮重下置零 实际为清皮
        private void lblZero_Click(object sender, EventArgs e)
        {
            try
            {

                //ToledoResult result = ToledoUtil.SendZero();
                //ToledoResult result = ToledoUtil.ClearTare();
                ScaleResult result = ScaleGlobalHelper.ClearTare();
                if (!result.WhetherSuccess)
                {
                    //MainModel.ShowLog(result.Message,false);
                }
                this.Close();
            }
            catch (Exception ex)
            {
               // MainModel.ShowLog("置零异常"+ex.Message,true);

            }
           
        }

        private void lblTare_Click(object sender, EventArgs e)
        {
            try
            {

                //ToledoResult result = ToledoUtil.SendTareByKey();
                ScaleResult result = ScaleGlobalHelper.SetTare();
                if (!result.WhetherSuccess)
                {
                    MainModel.ShowLog(result.Message, false);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("置零异常" + ex.Message, true);

            }
        }

        private void lblNumber_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                string numbervalue = NumberHelper.ShowFormNumber("数字去皮", NumberType.TareWeight);
                if (!string.IsNullOrEmpty(numbervalue))
                {

                    //ToledoUtil.ClearTare();
                    //ToledoResult result = ToledoUtil.SendPreTare(Convert.ToInt32(numbervalue));

                    ScaleGlobalHelper.ClearTare();
                    ScaleResult result = ScaleGlobalHelper.SetTareByNum(Convert.ToInt32(numbervalue));

                    if (result.WhetherSuccess)
                    {
                        MainModel.ShowLog("数字去皮完成", false);
                    }
                    else
                    {
                        MainModel.ShowLog("数字去皮失败" + result.Message, false);
                    }

                }
                else
                {

                }
                this.Close();
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("置零异常" + ex.Message, true);

            }
        }


        private void panel1_Leave(object sender, EventArgs e)
        {
            //this.Close();
        }



        private void FormTare_Deactivate(object sender, EventArgs e)
        {
        
                this.Close();
        }

     
            
    }
}
