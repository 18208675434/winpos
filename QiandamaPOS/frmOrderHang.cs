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
using System.Threading;
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
        public delegate void DataRecHandleDelegate(int type, Cart cart,string phone);
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

                                string shortfilename = fList[i].Name.Replace(".order", "");
                                string timestr = "";
                                string phone = "";
                                if (shortfilename.Contains("-"))
                                {
                                     timestr = shortfilename.Split('-')[0];
                                     phone = shortfilename.Split('-')[1];
                                }
                                else
                                {
                                    timestr = shortfilename;
                                }

                                //string timestr = fList[i].Name.Replace(".order", "");

                                try {
                                    timestr = timestr.Substring(0, 4) + "-" + timestr.Substring(4, 2) + "-" + timestr.Substring(6, 2) + " " + timestr.Substring(8, 2) + ":" + timestr.Substring(10, 2) + ":" + timestr.Substring(12, 2);
                                }
                                catch { }
                                //TODO  会员手机号
                                dgvOrderOnLine.Rows.Add((i+1).ToString(), phone, cart.title, timestr,"继续收银","删除挂单");

                               // lstCart.Add(cart);
                            }
                        }
                    }

                }

                if (dgvOrderOnLine.Rows.Count > 0)
                {
                    ShowLog("刷新完成", false);
                }
                else
                {
                    ShowLog("暂无数据", false);
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

                //if (dgvOrderOnLine.Rows.Count <= 0)
                //{
                //    return;
                //}
                DirectoryInfo di = new DirectoryInfo(MainModel.OrderPath);
                List<FileInfo> fList = di.GetFiles().ToList();

                if (fList.Count > 0)
                {
                    this.Enabled = false;
                    frmDeleteGood frmdelete = new frmDeleteGood("是否确认清空所有挂单？", "", "");
                    if (frmdelete.ShowDialog() != DialogResult.OK)
                    {
                        this.Enabled = true;
                        return;
                    }
                    this.Enabled = true;

                    for (int i = 0; i < fList.Count; i++)
                    {
                        File.Delete(fList[i].FullName);
                    }

                    ShowLog("挂单清空成功", false);
                    LoadOrderHang();

                }
               
              
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

                string phone = dgvOrderOnLine.Rows[e.RowIndex].Cells["phone"].Value.ToString();
                string filename = Convert.ToDateTime(dgvOrderOnLine.Rows[e.RowIndex].Cells["hangtime"].Value.ToString()).ToString("yyyyMMddHHmmss")+"-"+phone + ".order";
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


                                    this.DataReceiveHandle.BeginInvoke(1, cart,phone, null, null);                                   
                                }
                            }

                            Application.DoEvents();
                            File.Delete(BasePath);
                            this.Close();

                        }
                    }
                    else if (dgvOrderOnLine.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "删除挂单")
                    {

                         this.Enabled = false;
            frmDeleteGood frmdelete = new frmDeleteGood("确认删除该挂单？", "", "");
            if (frmdelete.ShowDialog() != DialogResult.OK)
            {
                this.Enabled = true;
                return;
            }
            this.Enabled = true;

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
          //  asf.ControlAutoSize(this);
        }

        private void frmOrderHang_EnabledChanged(object sender, EventArgs e)
        {
             try
            {
                if (this.Enabled)
                {
                    picScreen.Visible = false;

                }
                else
                {
                    picScreen.BackgroundImage = MainModel.GetWinformImage(this);
                    picScreen.Size = new System.Drawing.Size(this.Width, this.Height);
                    //picScreen.Location = new System.Drawing.Point(0, 0);
                    picScreen.Visible = true;
                    // this.Opacity = 0.9d;
                }
            }
            catch (Exception ex)
            {
                picScreen.Visible = false;
                LogManager.WriteLog("修改主窗体背景图异常："+ex.Message);
            }
        }


        
        //private Image GetWinformImage()
        //{
        //    //获取当前屏幕的图像
        //    Bitmap b = new Bitmap(this.Width, this.Height);
        //    this.DrawToBitmap(b, new Rectangle(0, 0, this.Width, this.Height));
        //    //b.Save(yourFileName);
        //    return b;
        //}


        //private Image TransparentImage(Image srcImage, float opacity)
        //{
        //    float[][] nArray ={ new float[] {1, 0, 0, 0, 0},
        //          new float[] {0, 1, 0, 0, 0},
        //          new float[] {0, 0, 1, 0, 0},
        //          new float[] {0, 0, 0, opacity, 0},
        //          new float[] {0, 0, 0, 0, 1}};
        //    ColorMatrix matrix = new ColorMatrix(nArray);
        //    ImageAttributes attributes = new ImageAttributes();
        //    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        //    Bitmap resultImage = new Bitmap(srcImage.Width, srcImage.Height);
        //    Graphics g = Graphics.FromImage(resultImage);
        //    g.DrawImage(srcImage, new Rectangle(0, 0, srcImage.Width, srcImage.Height), 0, 0, srcImage.Width, srcImage.Height, GraphicsUnit.Pixel, attributes);

        //    return resultImage;
        //}
        
    }
}
