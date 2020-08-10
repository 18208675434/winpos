using Maticsoft.BLL;
using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Common
{
    public class CartUtil
    {
        private static DBPRODUCT_BEANBLL productbll = new DBPRODUCT_BEANBLL();

        public static List<Product> LoadAllProduct(bool NeesLocalPrice)
        {
            List<Product> LstAllProduct = new List<Product>();
            try
            {
                List<DBPRODUCT_BEANMODEL> lstpro = productbll.GetModelList(" STATUS =1 and CREATE_URL_IP='" + MainModel.URL + "' and SHOPID='" + MainModel.CurrentShopInfo.shopid + "' order by FIRSTCATEGORYID");
                foreach (DBPRODUCT_BEANMODEL pro in lstpro)
                {                   
                    Product product = new Product();
                    product.mainimg = pro.MAINIMG;
                    product.price = null;
                    product.pricetag = pro.PRICETAG;
                    product.pricetagid = (int)pro.PRICETAGID;
                    product.saleunit = pro.SALESUNIT;
                    product.skucode = pro.SKUCODE;
                    product.skuname = pro.SKUNAME;
                    product.title = pro.SKUNAME;
                    product.firstcategoryid = pro.FIRSTCATEGORYID;
                    product.firstcategoryname = pro.FIRSTCATEGORYNAME;
                    product.barcode = pro.SKUCODE;
                    //product.isQueryBarcode = 0;
                    product.weightflag = Convert.ToBoolean(pro.WEIGHTFLAG);
                    product.shopid = pro.SHOPID;
                    product.goodstagid = (int)pro.WEIGHTFLAG;
                    product.num = 1;

                    product.salecount = (int)pro.SALECOUNT;
                    product.createdat = pro.CREATEDAT;
                    product.AllFirstLetter = pro.ALL_FIRST_LETTER;

                    product.InnerBarcode = pro.INNERBARCODE;
                    product.ShelfLife = pro.SHELFLIFE;

                    product.memberprice = pro.MEMBERPRICE;

                    if (NeesLocalPrice)
                    {
                        Price price = new Price();
                        price.saleprice = pro.SALEPRICE;
                        price.originprice = pro.SALEPRICE;
                        price.specnum = pro.SPECNUM;
                        price.unit = pro.SALESUNIT;

                        product.price = price;
                        //product.pricetagid = "";
                        product.pricetag = "";
                        //product.isLoadPanel = true;
                        //product.panelbmp = GetItemImg(product);

                        //singlecalculate.calculate(product);
                    }
                    //product.price = new Price();
                    //过滤脏数据
                    if(!string.IsNullOrEmpty( product.firstcategoryid)){
                        LstAllProduct.Add(product);
                    }
                    
                }

                return LstAllProduct;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载商品数据异常" + ex.StackTrace);
                return LstAllProduct;
            }

        }


        public static bool checkEanCodeIsError(String barCode, int num)
        {
            if (barCode.Length != num)
            {
                return true;
            }
            try
            {
                String code = barCode.Substring(0, num - 1);
                String checkBit = barCode.Substring(num - 1);
                int c1 = 0, c2 = 0;
                for (int i = 0; i < code.Length; i += 2)
                {
                    char c = code[i];
                    int n = c - '0';
                    c1 += n;
                }
                for (int i = 1; i < code.Length; i += 2)
                {
                    char c = code[i];
                    int n = c - '0';
                    c2 += n;
                }
                String check = (10 - (c1 + c2 * 3) % 10) % 10 + "";
                if (check == checkBit)
                {
                    return false;
                }
                else
                {
                    if (checkBit.Equals(getMod10CheckDigit(code) + ""))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                return true;
            }
        }

        public static char getMod10CheckDigit(string barcodeString)
        {

            int a = 0, b = 0, c = 0;
            int d = 0;
            char[] charArray = barcodeString.ToCharArray();

            // Step 1: Sum all of the digits in the odd positions together.
            for (int i = barcodeString.Length - 1; i >= 0; i = i - 2)
            {
                if (charArray[i] > '9' || charArray[i] < '0')
                    return (char)0;
                a = a + charArray[i] - 48;
            }

            // Step 2: Mutliply the sum from Step 1 by 3.
            a = a * 3;

            // Step 3: Sum all of the digits in the even positions together.
            for (int i = barcodeString.Length - 2; i >= 0; i = i - 2)
            {
                if (charArray[i] > '9' || charArray[i] < '0')
                    return (char)0;
                b = b + charArray[i] - 48;
            }

            // Step 4: Sum together the results from Step 2 and Step 3.
            c = a + b;

            // Step 5: Subtract the sum from the next highest multiple of 10.
            d = c % 10;
            if (d != 0)
            {
                d = 10 - d;
            }
            return (char)(d + 48);

        }


        public static bool GetBarCode(Product pro)
        {
            try
            {
                string tempcode = "";
                  if (pro.goodstagid == 0) //标品
                {

                    tempcode = "26" + pro.InnerBarcode + pro.num.ToString().PadLeft(5,'0');
                    string checkeancode = GetcheckEanCode(tempcode);

                    pro.barcode = tempcode + checkeancode;
                }
                else
                {
                    tempcode = "25" + pro.InnerBarcode + (Convert.ToInt32(pro.specnum * 1000)).ToString().PadLeft(5, '0');
                    string checkeancode = GetcheckEanCode(tempcode);

                    pro.barcode = tempcode + checkeancode;
                }
                  return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("获取barcode异常"+ex.Message);
                return false;
            }
        }

        /// <summary>
        ///获取条码校验位
        /// </summary>
        /// <param name="barCode">条码字符串</param>
        /// <param name="num">条码长度</param>
        /// <returns></returns>
        public static string GetcheckEanCode(String code)
        {
           
            try
            {
                   
                int c1 = 0, c2 = 0;
                for (int i = 0; i < code.Length; i += 2)
                {
                    char c = code[i];
                    int n = c - '0';
                    c1 += n;
                }
                for (int i = 1; i < code.Length; i += 2)
                {
                    char c = code[i];
                    int n = c - '0';
                    c2 += n;
                }
                String check = (10 - (c1 + c2 * 3) % 10) % 10 + "";
                return check;
            }
            catch (Exception e)
            {
                return null ;
            }
        }



        public static Product GetNewProduct(Product pro)
        {
            try
            {

                Product newpro = new Product();
                newpro.title = pro.skuname;
                newpro.skuname = pro.skuname;
                newpro.skucode = pro.skucode;
                newpro.num = pro.num;
                newpro.specnum = pro.specnum;
                newpro.spectype = pro.spectype;
                newpro.goodstagid = pro.weightflag == true ? 1 : 0;
                newpro.barcode = pro.barcode;
                newpro.weightflag = pro.weightflag;
                newpro.mainimg = pro.mainimg;
                newpro.panelbmp = pro.panelbmp;
                newpro.saleunit = pro.saleunit;
                newpro.InnerBarcode = pro.InnerBarcode;
                newpro.AllFirstLetter = pro.AllFirstLetter;
                newpro.goodstagid = pro.goodstagid;
                newpro.weightflag = pro.weightflag;
                newpro.pricetagid = pro.pricetagid;
                newpro.pricetag = pro.pricetag;

                newpro.firstcategoryid = pro.firstcategoryid;
                newpro.secondcategoryid = pro.secondcategoryid;
                newpro.categoryid = pro.categoryid;
                    Price price = null;

                    if (pro.price != null)
                    {
                        price = new Price();

                        price.saleprice = pro.price.saleprice;
                        price.originprice = pro.price.originprice;
                        price.specnum = pro.price.specnum;
                        price.unit = pro.price.unit;
                    }

                    newpro.price = price;
                

                return newpro;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
