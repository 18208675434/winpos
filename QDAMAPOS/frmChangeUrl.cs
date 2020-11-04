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
            //https://pos-qa-zhuizhikeji.com    kVI55eO1n3DZhWC8Z7
            if (radioButton1.Checked)
            {
                TXTURL.Text = "https://pos-qa-zhuizhikeji.com";
                txtPrivateKey.Text = "kVI55eO1n3DZhWC8Z7";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //https://pos.qdama.cn    riIptbBrTkNqG0q9UQ
            if (radioButton2.Checked)
            {
                TXTURL.Text = "https://pos.qdama.cn";
                txtPrivateKey.Text = "riIptbBrTkNqG0q9UQ";
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

       // https://pos-stage.qdama.cn    vnz91BcbmnGOQuFQW8NPAHYMLXFIDpr4
            if (radioButton3.Checked)
            {
                TXTURL.Text = "https://pos-stage.qdama.cn";
                txtPrivateKey.Text = "vnz91BcbmnGOQuFQW8NPAHYMLXFIDpr4";
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
            //https://pos-qa.qdama.cn    HuDo4MiNqqfdkAAOLGeOnj9srhwjBohm6
                TXTURL.Text = "https://pos-qa.qdama.cn";
                txtPrivateKey.Text = "HuDo4MiNqqfdkAOLGeOnj9srhwjBohm6";
            }
        }
    }
}
