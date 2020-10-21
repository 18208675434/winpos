using QDAMAPOS.Common;
using QDAMAPOS.Model;
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

namespace QDAMAPOS
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
        public delegate void DataRecHandleDelegate(int type, Cart cart, string phone);
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

        private Bitmap bmpContinue;
        private Bitmap bmpDelHang;
        private Bitmap bmpWhite;

        /// <summary>
        /// this.enable=false; 页面不可用页面不可控；  通过该标志控制页面是否可用
        /// </summary>
        private bool IsEnable = true;


        public string CurrentPhone ="";
        public Cart CurrentCart =new Cart();

        public frmOrderHang()
        {
            InitializeComponent();
        }


        private void frmOrderHang_Load(object sender, EventArgs e)
        {
            btnMenu.Text = MainModel.CurrentUser.nickname + "，你好  ";
            lblShopName.Text = MainModel.CurrentShopInfo.shopname;
            btnOnLineType.Left = lblShopName.Left + lblShopName.Width + 10;
            if (MainModel.IsOffLine)
            {
                btnOnLineType.BackgroundImage = Resources.ResourcePos.OffLineType; btnOnLineType.Text = "   离线";
                pnlDgvHead.Visible = false;
                pnlDgvHeadOffLine.Visible = true;
                dgvOrderOnLine.Columns["phone"].Visible = false;
            }
            else
            {
                btnOnLineType.BackgroundImage = Resources.ResourcePos.OnLineType; btnOnLineType.Text = "   在线";
                pnlDgvHead.Visible = true;
                pnlDgvHeadOffLine.Visible = false;
            }
            timerNow.Interval = 1000;
            timerNow.Enabled = true;
            LoadBmp();
            LoadOrderHang();
        }


        private void LoadBmp()
        {
            try
            {

                //int height = dgvOrderOnLine.RowTemplate.Height * 55 / 100;
                //bmpContinue = new Bitmap(Resources.ResourcePos.Continue, dgvOrderOnLine.Columns["Continue"].Width * 80 / 100, height);

                //bmpDelHang = new Bitmap(Resources.ResourcePos.DelHang, dgvOrderOnLine.Columns["DelHang"].Width * 80 / 100, height);
                bmpContinue = (Bitmap)MainModel.GetControlImage(btnContinue);
                bmpDelHang = (Bitmap)MainModel.GetControlImage(btnDelHang);
                bmpWhite = Resources.ResourcePos.White;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载页面图片异常" + ex.Message);
            }
        }

        private void LoadOrderHang()
        {
            try
            {
                dgvOrderOnLine.Rows.Clear();

                string orderpath = MainModel.OrderPath;
                if (MainModel.IsOffLine)
                {
                    orderpath = MainModel.OffLineOrderPath;
                }
                DirectoryInfo di = new DirectoryInfo(orderpath);
                List<FileInfo> fList = di.GetFiles().ToList();
                fList.Reverse(); //名称开头是时间，文件倒序  挂单也就倒序

                for (int i = 0; i < fList.Count; i++)
                {

                    if (fList[i].Name.Contains(".order"))
                    {
                        try
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

                                    try
                                    {
                                        timestr = timestr.Substring(0, 4) + "-" + timestr.Substring(4, 2) + "-" + timestr.Substring(6, 2) + " " + timestr.Substring(8, 2) + ":" + timestr.Substring(10, 2) + ":" + timestr.Substring(12, 2);
                                    }
                                    catch { }

                                    string title = cart.products[0].title + "等共" + cart.products.Count + "件商品";
                                    //TODO  会员手机号
                                    dgvOrderOnLine.Rows.Add((i + 1).ToString(), phone, title, timestr, bmpContinue, bmpDelHang);

                                    // lstCart.Add(cart);
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                            LogManager.WriteLog("挂单文件反序列化异常，删除" + fList[i].FullName);
                            //挂单反序列化异常 删除单据，防止对象修改  之前序列化的文件解析出现问题
                            File.Delete(fList[i].FullName);
                        }
                    }

                }

                if (dgvOrderOnLine.Rows.Count > 0)
                {
                    MainModel.ShowLog("刷新完成", false);
                }
                else
                {
                    // ShowLog("暂无数据", false);
                }


            }
            catch (Exception ex)
            {
                LogManager.WriteLog("反序列化挂单信息异常" + ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (!IsEnable)
            {
                return;
            }
            this.Close();
        }


        private void dgvOrderOnLine_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }
                if (e.RowIndex < 0)
                    return;


                IsEnable = false;

                string phone = dgvOrderOnLine.Rows[e.RowIndex].Cells["phone"].Value.ToString();
                string filename = Convert.ToDateTime(dgvOrderOnLine.Rows[e.RowIndex].Cells["hangtime"].Value.ToString()).ToString("yyyyMMddHHmmss") + "-" + phone + ".order";
                string BasePath = "";
                if (MainModel.IsOffLine)
                {
                    BasePath=MainModel.OffLineOrderPath + "\\" + filename;
                }
                else
                {
                    BasePath = MainModel.OrderPath + "\\" + filename;
                }
                
                if (File.Exists(BasePath))
                {
                    if (dgvOrderOnLine.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == bmpContinue)
                    {
                       // LoadingHelper.ShowLoadingScreen("加载中...");
                        int serialno = Convert.ToInt16(dgvOrderOnLine.Rows[e.RowIndex].Cells["serialno"].Value.ToString());


                            using (Stream input = File.OpenRead(BasePath))
                            {
                                if (input.Length > 0)
                                {
                                    CurrentCart = (Cart)formatter.Deserialize(input);
                                    CurrentPhone = phone;                                   
                                  
                                }
                            }
                            File.Delete(BasePath);
                            this.DialogResult = DialogResult.OK;
                            this.Dispose();                          

                           // Application.DoEvents();
                           // this.Close();

                    }
                    else if (dgvOrderOnLine.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == bmpDelHang)
                    {

                        IsEnable = false;

                        FrmConfirmBack frmconfirmback = new FrmConfirmBack("确认删除该挂单？", "", "");
                        frmconfirmback.Location = new Point(0, 0);
                        if (frmconfirmback.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }

                        File.Delete(BasePath);
                        MainModel.ShowLog("挂单删除成功", false);
                        LoadOrderHang();
                    }
                }
                else
                {
                    MainModel.ShowLog("挂单文件未找到，可能已被删除！" + BasePath, false);
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("挂单操作异常！" + ex.Message, true);
            }
            finally
            {
                IsEnable = true;
                LoadPicScreen(false);
            }
        }


        private void frmOrderHang_SizeChanged(object sender, EventArgs e)
        {
            //  asf.ControlAutoSize(this);
        }



        private void LoadPicScreen(bool isShown)
        {
            try
            {
                if (isShown)
                {
                    picScreen.BackgroundImage = MainModel.GetWinformImage(this);
                    picScreen.Size = new System.Drawing.Size(this.Width, this.Height);
                    picScreen.Visible = true;
                }
                else
                {
                    //picScreen.Size = new System.Drawing.Size(0, 0);
                    picScreen.Visible = false;
                }

                Application.DoEvents();
            }
            catch (Exception ex)
            {
                picScreen.Visible = false;
                LogManager.WriteLog("修改挂单窗体背景图异常：" + ex.Message);
            }
        }

        private void picScreen_Click(object sender, EventArgs e)
        {
            LoadPicScreen(false);
        }


        private void timerNow_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void btnWindows_Click(object sender, EventArgs e)
        {
            MainModel.ShowWindows();
        }


    }
}
