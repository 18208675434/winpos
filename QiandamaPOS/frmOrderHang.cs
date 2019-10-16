using QiandamaPOS.Common;
using QiandamaPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QiandamaPOS
{
    public partial class frmOrderHang : Form
    {
        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">未使用</param>
        /// <param name="cart">购物车对象</param>
        public delegate void DataRecHandleDelegate(int type, Cart cart);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataRecHandleDelegate DataReceiveHandle;

        /// <summary>
        /// 序列化 反序列化
        /// </summary>
        BinaryFormatter formatter = new BinaryFormatter();


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();
        public frmOrderHang()
        {
            InitializeComponent();
        }
        
        private void frmOrderHang_Shown(object sender, EventArgs e)
        {
            LoadOrderHang();
        }

        private void LoadOrderHang()
        {
            try
            {
                dgvOrderOnLine.Rows.Clear();


                DirectoryInfo di = new DirectoryInfo(MainModel.OrderPath);
                List<FileInfo> fList = di.GetFiles().ToList();
                for (int i = 0; i < fList.Count; i++)
                {
                    if (fList[i].Name.Contains(".order"))
                    {
                        //反序列化
                        using (Stream input = File.OpenRead(fList[i].FullName))
                        {
                            if (input.Length > 0)
                            {
                                Cart cart = (Cart)formatter.Deserialize(input);
                                string timestr = fList[i].Name.Replace(".order", "");
                                try {
                                    timestr = timestr.Substring(0, 4) + "-" + timestr.Substring(4, 2) + "-" + timestr.Substring(6, 2) + " " + timestr.Substring(8, 2) + ":" + timestr.Substring(10, 2) + ":" + timestr.Substring(12, 2);
                                }
                                catch { }
                                //TODO  会员手机号
                                dgvOrderOnLine.Rows.Add(i.ToString(), "", cart.title, timestr,"继续收银","删除挂单");

                               // lstCart.Add(cart);
                            }
                        }
                    }

                }               

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("反序列化挂单信息异常" + ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 清空所有挂单
        private void btnClearOrderHang_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgvOrderOnLine.Rows.Count <= 0)
                {
                    return;
                }

                frmDeleteGood frmdelete = new frmDeleteGood("是否确认清空所有挂单？", "", "");
                if (frmdelete.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                DirectoryInfo di = new DirectoryInfo(MainModel.OrderPath);
                List<FileInfo> fList = di.GetFiles().ToList();
                for (int i = 0; i < fList.Count; i++)
                {
                    File.Delete(fList[i].FullName);                   
                }

                ShowLog("挂单清空成功",false);
                LoadOrderHang();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("清空挂单信息异常" + ex.Message);
            }
        }

        //TODO  修改样式
        private void ShowLog(string msg, bool iserror)
        {
            this.Invoke(new InvokeHandler(delegate()
            {

                frmMsg frmmsf = new frmMsg(msg, iserror, 1000);
                frmmsf.ShowDialog(); LogManager.WriteLog(msg);
            }));

        }

        private void dgvOrderOnLine_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex < 0)
                    return;

                string filename = Convert.ToDateTime(dgvOrderOnLine.Rows[e.RowIndex].Cells["hangtime"].Value.ToString()).ToString("yyyyMMddHHmmss") + ".order";
                string BasePath = MainModel.OrderPath+"\\"+filename;
                if (File.Exists(BasePath))
                {
                    if (dgvOrderOnLine.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "继续收银")
                    {
                        int serialno = Convert.ToInt16(dgvOrderOnLine.Rows[e.RowIndex].Cells["serialno"].Value.ToString());

                        if (DataReceiveHandle != null)
                        {
                            using (Stream input = File.OpenRead(BasePath))
                            {
                                if (input.Length > 0)
                                {
                                    Cart cart = (Cart)formatter.Deserialize(input);
                                    this.DataReceiveHandle.BeginInvoke(1, cart, null, null);
                                   
                                }
                            }

                            Application.DoEvents();
                            File.Delete(BasePath);
                            this.Close();

                        }

                    }
                    else if (dgvOrderOnLine.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "删除挂单")
                    {

                        File.Delete(BasePath);

                        ShowLog("挂单删除成功",false);
                        LoadOrderHang();
                    }
                }
                else
                {
                    ShowLog("挂单文件未找到，可能已被删除！"+BasePath,false);
                }
               
               
            }
            catch (Exception ex)
            {
                ShowLog("挂单操作异常！" + ex.Message, true);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadOrderHang();
        }

        private void frmOrderHang_SizeChanged(object sender, EventArgs e)
        {
            asf.ControlAutoSize(this);
        }
    }
}
