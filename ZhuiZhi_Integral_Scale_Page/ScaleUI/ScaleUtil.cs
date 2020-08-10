using Maticsoft.BLL;
using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Common
{
   public class ScaleUtil
    {
        /// <summary>
        /// 电子秤表操作
        /// </summary>
       private static  DBSCALE_KEY_BEANBLL scalebll = new DBSCALE_KEY_BEANBLL();

        /// <summary>
        /// 电子秤更新状态表操作类
        /// </summary>
        private static  DBTRANSFER_SCALE_STATUS_BEANBLL scalestatusbll = new DBTRANSFER_SCALE_STATUS_BEANBLL();

        /// <summary>
        /// 产品表操作类
        /// </summary>
        private static  DBPRODUCT_BEANBLL productbll = new DBPRODUCT_BEANBLL();

        public static  bool SendScaleByScaleIp(string scaleip ,ref string errormsg)
        {
            try
            {

                if (!Check110Connect(scaleip))
                {
                    errormsg = "电子秤连接失败";
                    return false;
                }

                ClearData(scaleip, "Prf");
                ClearData(scaleip, "Plu");
                ClearData(scaleip, "Kas");
                
                Send110Label(scaleip);

                SendText(scaleip);

                
                if (!SendPlu(scaleip, ref errormsg))
                {
                    return false;
                }
                if (!SendKsa(scaleip, ref errormsg))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                errormsg = "传秤异常" + ex.Message;
                LogManager.WriteLog("传秤异常" + ex.Message+ex.StackTrace);
                return false;
            }


        }

        public static bool Check110Connect(string scaleip)
        {
             try{

               string plucmd = "digicon -s "+scaleip+":sm110 --check_connection";//"digicon -P -s " + scaleip + ":" + "sm110" + " -m plu_template.json -i plu_import.csv";
               
               return RunCmd(plucmd);

           }catch(Exception ex){
               LogManager.WriteLog("传秤标签异常" + ex.Message + ex.StackTrace);
               return true; //异常不影响传数据
           }
        }


//       Plu: 商品
//Mgp: 主组
//Dep: 部门
//Kas: 预置键
//Trg: 追溯码
//Trb: 追溯二维码
//Trt: 追溯码文本
//Tbt: 二维码
//Prf: 标签格式
//Pff: 标签格式明细
//Flb: 自定义条码
//Mub: 多样条码(Code128,QR...)
//Spm: 特殊信息
//Ing: 成份
//Tex: 文本
        public static bool ClearData(string scaleip,string Type)
        {
            try
            {

                string plucmd = "digicon -s" +scaleip+":sm110 -d "+Type;

                return RunCmd(plucmd);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("清理秤异常"+Type + ex.Message + ex.StackTrace);
                return false;
            }
        }


        public static bool SendText(string scaleip)
        {
            try
            {

                List<string> lstSendText = new List<string>();

                string shopname = MainModel.CurrentShopInfo.shopname;
                if (shopname.Length > 16)
                {
                    shopname = shopname.Substring(0,16);
                }

                //秤空格站位少， 不取一半
                int spacenum = (32 - Encoding.Default.GetBytes(shopname).Length)*4/5;

                shopname = " ".PadRight(spacenum, ' ') + shopname;

                lstSendText.Add(16 + "," + shopname + "," + 25);

               // lstSendText.Add(3 + "," + "保质日期" + "," + 11);
                File.WriteAllLines(MainModel.ServerPath + "tex_import.csv", lstSendText.ToArray(), Encoding.Default);

                string plucmd = "digicon -E -s " + scaleip + ":sm110 -m tex_template.json -i tex_import.csv";

                return RunCmd(plucmd);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("传秤标签异常" + ex.Message + ex.StackTrace);
                return false;
            }
        }

       public static bool Send110Label(string scaleip){
           try{

               string plucmd = "digicon -s " + scaleip + ":sm110 --write ignore --access_file_name SM110label";//"digicon -P -s " + scaleip + ":" + "sm110" + " -m plu_template.json -i plu_import.csv";
               
               return RunCmd(plucmd);

           }catch(Exception ex){
               LogManager.WriteLog("传秤标签异常" + ex.Message + ex.StackTrace);
               return false;
           }
       }


        public static bool SendPlu(string scaleip, ref string errormsg)
        {
            try
            {

                List<string> lstSendPLUStr = new List<string>();

                List<DBSCALE_KEY_BEANMODEL> lstscale = scalebll.GetModelList(" IP='" + scaleip + "' and CREATE_URL_IP ='" + MainModel.URL + "'");
                if (lstscale == null || lstscale.Count <= 0)
                {
                    errormsg = scaleip + "秤无商品信息";
                    return false;
                }

                List<DBPRODUCT_BEANMODEL> CurrentLstPro = productbll.GetModelList(" CREATE_URL_IP='" + MainModel.URL + "' and SHOPID='" + MainModel.CurrentShopInfo.shopid + "' and SCALEFLAG =1");

                if (CurrentLstPro == null || CurrentLstPro.Count == 0)
                {
                    errormsg = scaleip + "未匹配到商品";
                    return false;
                }


                string scalename = "sm110";
                if (lstscale[0].SCALESTYPE.ToUpper().Contains("SM-120"))
                {
                    scalename = "sm120";
                }
                else if (lstscale[0].SCALESTYPE.ToUpper().Contains("SM-110"))
                {
                    scalename = "sm110";
                }
                else
                {
                    errormsg = "暂未匹配该秤" + lstscale[0].SCALESTYPE;
                    return false;
                }

                foreach (DBPRODUCT_BEANMODEL pro in CurrentLstPro)
                {

                    if (pro == null || string.IsNullOrEmpty(pro.INNERBARCODE))
                    {
                        continue;
                    }

                    string shortinnerbarcode = pro.INNERBARCODE;
                    if (shortinnerbarcode.Length > 7)
                    {
                        shortinnerbarcode = shortinnerbarcode.Substring(shortinnerbarcode.Length-7); 
                    }

                    if (pro.SKUNAME.Length > 12)
                    {
                        pro.SKUNAME = pro.SKUNAME.Substring(0, 12);
                    }

                    if (pro.SALEPRICE > 9999)
                    {
                        pro.SALEPRICE = 9999;
                    }
                    //lstSendPro.Add(pro);
                    if (pro == null)
                    {
                        break;
                    }
                    StringBuilder sb = new StringBuilder();
                    sb.Append(shortinnerbarcode + ",");
                    sb.Append(pro.INNERBARCODE + ",");
                    sb.Append(pro.SKUNAME + ",");
                    sb.Append("" + ",");
                    sb.Append("" + ",");

                    sb.Append("" + ",");
                    sb.Append(pro.SALEPRICE + ",");
                    //sb.Append(11 + ",");

                    //csv文件 称重商品0  标品为1  与数据库字段定义相反        条码头标品固定26  称重固定25
                    if (pro.WEIGHTFLAG == 1)
                    {
                        sb.Append("0" + ",");
                        sb.Append("25" + ",");  //条码头标识？
                    }
                    else
                    {
                        sb.Append("1" + ",");
                        sb.Append("26" + ",");  //条码头标识？
                    }

                    sb.Append("33" + ",");  //条码格式编号
                    sb.Append(pro.SHELFLIFE + ",");  //保质期
                    sb.Append("" + ","); //销售日期？
                    sb.Append("0" + ","); //包装日期 固定0当天
                    sb.Append(17 + ",");  //标签格式编号  ？
                    sb.Append("997" + ","); //主组号

                    if (string.IsNullOrEmpty(pro.LOCATION))
                    {
                        sb.Append("  " + ",");
                    }
                    else
                    {
                        sb.Append("产地信息:" + pro.LOCATION + ",");
                    }

                    if (string.IsNullOrEmpty(pro.SPINFO))
                    {
                        sb.Append("  " + ",");
                    }
                    else
                    {
                        sb.Append("贮存条件:" + pro.SPINFO + ",");
                    }

                    sb.Append("  " + ",");
                    sb.Append("门店:" + MainModel.CurrentShopInfo.shopname + ",");
                    sb.Append("称号:" + scaleip.Substring(scaleip.LastIndexOf(".") + 1) + ",");//秤号？


                    if (string.IsNullOrEmpty(pro.REMARK))
                    {
                        sb.Append("  " + ",");
                    }
                    else
                    {
                        sb.Append("备注:" + pro.REMARK + ",");
                    }


                    if (string.IsNullOrEmpty(pro.INGREDIENT))
                    {
                        sb.Append("  " + ",");
                    }
                    else
                    {
                        sb.Append("成分:" + pro.INGREDIENT + ",");
                    }


                    if (string.IsNullOrEmpty(pro.COMPANY))
                    {
                        sb.Append("  " + ",");
                    }
                    else
                    {
                        sb.Append("销售商:" + pro.COMPANY + ",");
                    }

                    sb.Append(" " + ",");
                    sb.Append(" " + ",");

                    sb.Append(pro.QRCODECONTENT + ",");
                    sb.Append("1" + ",");
                    sb.Append("0" + ",");
                    sb.Append("1" + ",");
                    sb.Append("tou");   //ITF  18位   EAN13位

                    lstSendPLUStr.Add(sb.ToString());


                }

                File.WriteAllLines(MainModel.ServerPath + "plu_import.csv", lstSendPLUStr.ToArray(), Encoding.Default);


                //
                string plucmd = "digicon -P -s " + scaleip + ":" + scalename + " -m plu_template.json -i plu_import.csv";

                bool cmdPluResult = RunCmd(plucmd);


                if (cmdPluResult)
                {

                    DBTRANSFER_SCALE_STATUS_BEANMODEL scalestatusmodel = new DBTRANSFER_SCALE_STATUS_BEANMODEL();
                    scalestatusmodel.SYS_SUCCESS_TIME = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));
                    scalestatusmodel.STATUS = 1;
                    scalestatusmodel.SCALEIP = scaleip;
                    scalestatusmodel.CREATE_URL_IP = MainModel.URL;

                    if (scalestatusbll.ExistsByScaleIp(scaleip))
                    {
                        scalestatusbll.UpdateByScaleIp(scalestatusmodel);
                    }
                    else
                    {
                        scalestatusbll.Add(scalestatusmodel);
                    }
                    //success
                    return true;
                }
                else
                {

                    LogManager.WriteLog(scaleip + "传秤失败");
                    frmScaleResult frmscaleresult = new frmScaleResult(false, lstscale[0].TEMPNAME, "下发失败");


                    DBTRANSFER_SCALE_STATUS_BEANMODEL scalestatusmodel = new DBTRANSFER_SCALE_STATUS_BEANMODEL();
                    scalestatusmodel.SYS_ERROR_TIME = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));
                    scalestatusmodel.STATUS = 0;
                    scalestatusmodel.SCALEIP = scaleip;
                    scalestatusmodel.CREATE_URL_IP = MainModel.URL;

                    if (scalestatusbll.ExistsByScaleIp(scaleip))
                    {
                        scalestatusbll.UpdateByScaleIp(scalestatusmodel);
                    }
                    else
                    {
                        scalestatusbll.Add(scalestatusmodel);
                    }
                    errormsg = scaleip + "传秤失败";
                    return false;
                }

            }
            catch (Exception ex)
            {
                errormsg = "传秤异常" + ex.Message;
                LogManager.WriteLog("传秤异常" + ex.Message + ex.StackTrace);
                return false;
            }


        }

        public static bool SendKsa(string scaleip, ref string errormsg)
        {
            try
            {

                List<string> lstSendKSAStr = new List<string>(); //快捷键

                List<DBPRODUCT_BEANMODEL> CurrentLstPro = productbll.GetModelList(" CREATE_URL_IP='" + MainModel.URL + "' and SHOPID='" + MainModel.CurrentShopInfo.shopid + "'");


                List<DBSCALE_KEY_BEANMODEL> lstscale = scalebll.GetModelList(" IP='" + scaleip + "' and CREATE_URL_IP ='" + MainModel.URL + "'");
                if (lstscale == null || lstscale.Count <= 0)
                {
                    errormsg = scaleip + "秤无商品信息";
                    return false;
                }


                string scalename = "sm110";
                if (lstscale[0].SCALESTYPE.ToUpper().Contains("SM-120"))
                {
                    scalename = "sm120";
                }
                else if (lstscale[0].SCALESTYPE.ToUpper().Contains("SM-110"))
                {
                    scalename = "sm110";
                }
                else
                {
                    errormsg = "暂未匹配该秤" + lstscale[0].SCALESTYPE;
                    return false;
                }

                foreach (DBSCALE_KEY_BEANMODEL scale in lstscale)
                {
                    string innerbarcode = "";
                    List<DBPRODUCT_BEANMODEL> lstpro = CurrentLstPro.Where(r => r.SKUCODE == scale.SKUCODE).ToList();

                    if (lstpro != null && lstpro.Count > 0)
                    {
                        innerbarcode = lstpro[0].INNERBARCODE;
                    }

                    if (innerbarcode.Length > 7)
                    {
                        innerbarcode = innerbarcode.Substring(innerbarcode.Length - 7);
                    }

                    lstSendKSAStr.Add(innerbarcode + "," + scale.NOPOS + "," + (scale.SETTINGPAGE-1));

                }

                File.WriteAllLines(MainModel.ServerPath + "kas_import.csv", lstSendKSAStr.ToArray(), Encoding.Default);

                //
                string kascmd = "digicon -K -s " + scaleip + ":" + scalename + " -m kas_template.json -i kas_import.csv";
                bool cmdKasResult = RunCmd(kascmd);

                if (cmdKasResult)
                {

                    DBTRANSFER_SCALE_STATUS_BEANMODEL scalestatusmodel = new DBTRANSFER_SCALE_STATUS_BEANMODEL();
                    scalestatusmodel.SYS_SUCCESS_TIME = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));
                    scalestatusmodel.STATUS = 1;
                    scalestatusmodel.SCALEIP = scaleip;
                    scalestatusmodel.CREATE_URL_IP = MainModel.URL;

                    if (scalestatusbll.ExistsByScaleIp(scaleip))
                    {
                        scalestatusbll.UpdateByScaleIp(scalestatusmodel);
                    }
                    else
                    {
                        scalestatusbll.Add(scalestatusmodel);
                    }
                    //success
                    return true;
                }
                else
                {

                    LogManager.WriteLog(scaleip + "快捷键传秤失败");
                    frmScaleResult frmscaleresult = new frmScaleResult(false, lstscale[0].TEMPNAME, "下发失败");


                    DBTRANSFER_SCALE_STATUS_BEANMODEL scalestatusmodel = new DBTRANSFER_SCALE_STATUS_BEANMODEL();
                    scalestatusmodel.SYS_ERROR_TIME = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));
                    scalestatusmodel.STATUS = 0;
                    scalestatusmodel.SCALEIP = scaleip;
                    scalestatusmodel.CREATE_URL_IP = MainModel.URL;

                    if (scalestatusbll.ExistsByScaleIp(scaleip))
                    {
                        scalestatusbll.UpdateByScaleIp(scalestatusmodel);
                    }
                    else
                    {
                        scalestatusbll.Add(scalestatusmodel);
                    }
                    errormsg = scaleip + "传秤失败";
                    return false;
                }

            }
            catch (Exception ex)
            {
                errormsg = "传秤异常" + ex.Message;
                LogManager.WriteLog("传秤异常" + ex.Message + ex.StackTrace);
                return false;
            }

        }

        /// <summary>
        /// 执行CMD语句
        /// </summary>
        /// <param name="cmd">要执行的CMD命令</param>
        public static  bool RunCmd(string cmd)
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.Start();
                proc.StandardInput.WriteLine(cmd);
                proc.StandardInput.WriteLine("exit");
                string outStr = proc.StandardOutput.ReadToEnd();
                proc.Close();

                if (outStr.Contains("Successfully") || outStr.Contains("OK"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
                //return outStr;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("发送CMD命令异常" + ex.Message, true);
                return false;
            }
        }




    }



}
