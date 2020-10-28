using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZhuiZhi_Integral_Scale_UncleFruit
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

                if (rdoStage.Checked)
                {
                    INIManager.SetIni("MQTT", "Server", "mqtt-stage.a72hongjie.com", MainModel.IniPath);
                    INIManager.SetIni("MQTT", "UserName", "zhuizhi_android", MainModel.IniPath);
                    INIManager.SetIni("MQTT", "PassWord", "123456", MainModel.IniPath);
                }
                else if (rdoQa.Checked)
                {
                    INIManager.SetIni("MQTT", "Server", "mqtt-qa.a72hongjie.com", MainModel.IniPath);
                    INIManager.SetIni("MQTT", "UserName", "zhuizhi_android", MainModel.IniPath);
                    INIManager.SetIni("MQTT", "PassWord", "123456", MainModel.IniPath);
                }
                else if (rdoZheng.Checked)
                {
                    INIManager.SetIni("MQTT", "Server", "mqtt-online.a72hongjie.com", MainModel.IniPath);
                    INIManager.SetIni("MQTT", "UserName", "zhuizhi_winpos", MainModel.IniPath);
                    INIManager.SetIni("MQTT", "PassWord", "cYbylCBf", MainModel.IniPath);
                }

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
                TXTURL.Text = "https://pos-stage.a72hongjie.com";
                txtPrivateKey.Text = "kVl55eO1n3DZhWC8Z7";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //https://pos-qa.zhuizhikeji.com    kVl55eO1n3DZhWC8Z7
            if (rdoQa.Checked)
            {
                TXTURL.Text = "https://pos-qa.a72hongjie.com";
                txtPrivateKey.Text = "kVl55eO1n3DZhWC8Z7";
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            // https://pos.zhuizhikeji.com    fbNZhX5LSUUhKnCpZo6uZLUVQpmewP
            if (rdoZheng.Checked)
            {
                TXTURL.Text = "https://pos.a72hongjie.com";
                txtPrivateKey.Text = "fbNZhX5LSUUhKnCpZo6uZLUVQpmewP";
            }
        }
    }
}
