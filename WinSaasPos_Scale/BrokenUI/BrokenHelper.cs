using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinSaasPOS_Scale.BrokenUI.Model;
using WinSaasPOS_Scale.Common;
using WinSaasPOS_Scale.Model;

namespace WinSaasPOS_Scale.BrokenUI
{
    public class BrokenHelper
    {
        //<summary>
        //按比例缩放页面及控件
        //</summary>
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();
        private static FormBroken frmbroken = null;
        public static void IniFormBroken()
        {
            try
            {
                if (frmbroken != null)
                {
                    try
                    {
                        frmbroken.Dispose();
                    }
                    catch { }

                }
                frmbroken = new FormBroken();
                asf.AutoScaleControlTest(frmbroken, 1180, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                frmbroken.Location = new System.Drawing.Point(0,0);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化报损页面异常"+ex.Message);
            }
        }

        public static void ShowFormBroken()
        {
            try
            {
                if (frmbroken == null || frmbroken.IsDisposed)
                {
                    IniFormBroken();
                }
                frmbroken.ShowDialog();
            }
            catch (Exception ex)
            {
                frmbroken = null;
                LogManager.WriteLog("显示报损页面异常" + ex.Message);
            }
        }



        private static FormBrokenCreate frmbrokencreate = null;
        public static void IniFormBrokenCreate()
        {
            try
            {
                if (frmbrokencreate != null)
                {
                    try
                    {
                        frmbrokencreate.Dispose();
                    }
                    catch { }
                }
                frmbrokencreate = new FormBrokenCreate();
                asf.AutoScaleControlTest(frmbrokencreate, 1180, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                frmbrokencreate.Location = new System.Drawing.Point(0,0);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化报损页面异常" + ex.Message);
            }
        }

        public static void ShowFormBrokenCreate()
        {
            try
            {
                if (frmbrokencreate == null || frmbrokencreate.IsDisposed)
                {
                    IniFormBrokenCreate();
                }

                frmbrokencreate.ShowDialog();
                frmbrokencreate.Dispose();
            }
            catch (Exception ex)
            {
                frmbrokencreate = null;
                LogManager.WriteLog("显示报损页面异常" + ex.Message);
            }
        }


        #region  报损数据处理

        private static HttpUtil httputil = new HttpUtil();
        public static BrokenResult GetSkuMovePrice(List<BrokenProduct> products)
        {
            BrokenResult result = new BrokenResult();
            try
            {
                List<string> lstskucodes = new List<string>();
                foreach (BrokenProduct pro in products)
                {
                    lstskucodes.Add(pro.skucode);
                }

                string errormsg = "";
                List<SkuMovePrice> lstprices = httputil.GetSkuMovePrice(lstskucodes, ref errormsg);

                if (lstprices == null || !string.IsNullOrEmpty(errormsg))
                {
                    result.WhetherSuccess = false;
                    result.message = errormsg;
                }
                else
                {
                    foreach (SkuMovePrice price in lstprices)
                    {
                        products.ForEach(r => r.deliveryprice = r.skucode == price.skucode ? price.deliveryprice : r.deliveryprice);
                    }
                    result.WhetherSuccess = true;
                }
               
                return result;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("获取报损商品价格异常"+ex.Message);
                result.WhetherSuccess = false;
                result.message = ex.Message;
                return result;
            }
        }


        public static List<BrokenProduct> LstProductToLstBrokenProduct(List<Product> lstpro)
        {
            try
            {
                if(lstpro==null)
                    return null;
                List<BrokenProduct> lstbrokenpro = new List<BrokenProduct>();
                foreach (Product pro in lstpro)
                {
                    lstbrokenpro.Add(ProductToBrokenProduct(pro));
                }
                return lstbrokenpro;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static BrokenProduct ProductToBrokenProduct(Product pro)
        {
            try
            {
                BrokenProduct bpro = new BrokenProduct();
                bpro.skucode = pro.skucode;
                bpro.skuname = pro.skuname;
                bpro.title =pro.skuname;
                bpro.weightflag = pro.goodstagid == 1;
                bpro.num = pro.num;
                bpro.specnum = pro.specnum;

                if (pro.price != null)
                {
                    bpro.saleprice = pro.price.saleprice;
                    bpro.unit = pro.price.unit;
                    bpro.specnum = pro.price.specnum;

                }

                return bpro;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Product转换BrokenProdcut异常"+ex.Message);
                return null;
            }
        }

        public static ParaCreateBroken GetParaCreateBroken(List<BrokenProduct> products){
            try{

                ParaCreateBroken para = new ParaCreateBroken();
                para.itemlist = new List<Item>();

                foreach (BrokenProduct pro in products)
                {
                    Item item = new Item();
                    item.skucode = pro.skucode;

                    item.deliveryquantity = pro.weightflag ? pro.specnum : pro.num;
                    para.itemlist.Add(item);
                }

                para.shopid = MainModel.CurrentShopInfo.shopid;


                return para;

            }catch(Exception ex){
                LogManager.WriteLog("解析报损商品转换异常"+ex.Message);
                return null;
            }
        }
        #endregion
    }

    public class BrokenResult
    {
        public  bool WhetherSuccess { get; set; }
        public  long code { get; set; }

        public  string message { get; set; }
    }
}
