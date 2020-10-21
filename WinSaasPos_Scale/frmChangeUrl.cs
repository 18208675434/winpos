using WinSaasPOS_Scale.Common;
using WinSaasPOS_Scale.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinSaasPOS_Scale
{
    public partial class frmChangeUrl : Form
    {
        public frmChangeUrl()
        {
            InitializeComponent();
        }

        private void frmChangeUrl_Shown(object sender, EventArgs e)
        {
            TXTURL.Text=MainModel.URL;
            txtPrivateKey.Text = MainModel.PrivateKey;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                /// <summary>
                /// 私钥
                /// </summary>
                INIManager.SetIni("System", "PrivateKey", txtPrivateKey.Text, MainModel.IniPath);
                /// <summary>
                /// 环境
                /// </summary>
                INIManager.SetIni("System", "URL", TXTURL.Text, MainModel.IniPath);
                MainModel.URL = TXTURL.Text;
                MainModel.PrivateKey = txtPrivateKey.Text;

                MessageBox.Show("保存成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败"+ex.Message);
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //https://pos-stage.zhuizhikeji.com    kVl55eO1n3DZhWC8Z7
            if (rdoStage.Checked)
            {
                TXTURL.Text = "https://pos-stage.zhuizhikeji.com";
                txtPrivateKey.Text = "kVl55eO1n3DZhWC8Z7";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //https://pos-qa.zhuizhikeji.com    kVl55eO1n3DZhWC8Z7
            if (rdoQa.Checked)
            {
                TXTURL.Text = "https://pos-qa.zhuizhikeji.com";
                txtPrivateKey.Text = "kVl55eO1n3DZhWC8Z7";
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

            // https://pos.zhuizhikeji.com    fbNZhX5LSUUhKnCpZo6uZLUVQpmewP
            if (rdoZheng.Checked)
            {
                TXTURL.Text = "https://pos.zhuizhikeji.com";
                txtPrivateKey.Text = "fbNZhX5LSUUhKnCpZo6uZLUVQpmewP";
            }
        }
    }
}
