using Maticsoft.BLL;
using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using WinSaasPOS.Model;

namespace WinSaasPOS.Common
{
   public class ScaleUtil
    {
        /// <summary>
        /// 电子秤表操作
        /// </summary>
       private DBSCALE_KEY_BEANBLL scalebll = new DBSCALE_KEY_BEANBLL();

        /// <summary>
        /// 电子秤更新状态表操作类
        /// </summary>
        private DBTRANSFER_SCALE_STATUS_BEANBLL scalestatusbll = new DBTRANSFER_SCALE_STATUS_BEANBLL();

        /// <summary>
        /// 产品表操作类
        /// </summary>
        private DBPRODUCT_BEANBLL productbll = new DBPRODUCT_BEANBLL();

        public bool SendScaleByScaleIp(string scaleip ,ref string errormsg)
        {
            try
            {

                List<string> lstSendPLUStr = new List<string>(); //PLU主档
                List<string> lstSendKSAStr = new List<string>(); //快捷键



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
                        
                        DBPRODUCT_BEANMODEL pro = productbll.GetModelBySkuCode(scale.SKUCODE, MainModel.URL);
                        //lstSendPro.Add(pro);
                        if (pro == null)
                        {
                            break;
                        }
                        StringBuilder sb = new StringBuilder();
                        sb.Append(pro.INNERBARCODE + ",");
                        sb.Append(pro.INNERBARCODE + ",");
                        sb.Append(pro.SKUNAME + ",");
                        sb.Append("" + ",");
                        sb.Append("" + ",");

                        sb.Append("" + ",");
                        sb.Append(pro.SALEPRICE + ",");

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
                        sb.Append(pro.BESTDAYS + ",");  //保质期
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
                        sb.Append("ITF");   //ITF  18位   EAN13位

                        lstSendPLUStr.Add(sb.ToString());

                        lstSendKSAStr.Add(pro.INNERBARCODE + "," + scale.NOPOS + "," + scale.SETTINGPAGE);
                       
                    }

                    File.WriteAllLines(MainModel.ServerPath + "plu_import.csv", lstSendPLUStr.ToArray(), Encoding.Default);
                    File.WriteAllLines(MainModel.ServerPath + "kas_import.csv", lstSendKSAStr.ToArray(), Encoding.Default);

                //Test
                  //  scaleip = "192.168.1.20";

                    //
                    string plucmd = "digicon -P -s " + scaleip + ":"+scalename + " -m plu_template.json -i plu_import.csv";

                    bool cmdPluResult = RunCmd(plucmd);

                    //
                    string kascmd = "digicon -K -s " + scaleip + ":" + scalename + " -m kas_template.json -i kas_import.csv";
                    bool cmdKasResult = RunCmd(kascmd);

                    if (cmdPluResult && cmdKasResult)
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
                LogManager.WriteLog("传秤异常" + ex.Message+ex.StackTrace);
                return false;
            }


        }

        /// <summary>
        /// 执行CMD语句
        /// </summary>
        /// <param name="cmd">要执行的CMD命令</param>
        public bool RunCmd(string cmd)
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

                if (outStr.Contains("Successfully"))
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
