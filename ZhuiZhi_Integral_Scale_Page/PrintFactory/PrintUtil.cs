﻿using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;
using Maticsoft.BLL;
using Newtonsoft.Json;
using Maticsoft.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI;
using ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.ScaleFactory;
using ZhuiZhi_Integral_Scale_UncleFruit.PrintFactory;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Common
{
    public class PrintUtil
    {
        public static bool PrintOrder(PrintDetail printdetail, bool isRefound, ref string errormsg)
        {
            try
            {
                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.托利多.ToString())
                {
                    return ToledoPrintUtil.PrintOrder(printdetail,isRefound,ref errormsg);
                }
                else if (ScaleName == ScaleType.顶尖PS1X.ToString())
                {
                    return SprtPrintUtil.PrintOrder(printdetail, isRefound, ref errormsg);
                }

                return false;
            }
            catch {
                return false;
            }
        }


        public static bool ReceiptPrint(Receiptdetail receipt, ref string errormsg)
        {
            try
            {
                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.托利多.ToString())
                {
                    return ToledoPrintUtil.ReceiptPrint(receipt, ref errormsg);
                }
                else if (ScaleName == ScaleType.顶尖PS1X.ToString())
                {
                    return SprtPrintUtil.ReceiptPrint(receipt, ref errormsg);
                }

                return false;
            }
            catch {
                return false;
            }
        }

        public static bool PrintBroken(CreateBrokenResult brokenresult, ref string errormsg)
        {
            try
            {
                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.托利多.ToString())
                {
                    return ToledoPrintUtil.PrintBroken(brokenresult, ref errormsg);
                }
                else if (ScaleName == ScaleType.顶尖PS1X.ToString())
                {
                    return SprtPrintUtil.PrintBroken(brokenresult, ref errormsg);
                }

                return false;
            }
            catch {
                return false;
            }
        }


        public static void OpenCashDrawerEx()
        {
            try
            {
                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.托利多.ToString())
                {
                    ToledoPrintUtil.OpenCashDrawerEx();
                }
                else if (ScaleName == ScaleType.顶尖PS1X.ToString())
                {
                    SprtPrintUtil.OpenCashDrawerEx();
                }
            }
            catch { }
        }


        public static bool PrintTopUp(string depositbillid)
        {
            try
            {
                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.托利多.ToString())
                {
                     ToledoPrintUtil.PrintTopUp(depositbillid);
                }
                else if (ScaleName == ScaleType.顶尖PS1X.ToString())
                {
                    SprtPrintUtil.PrintTopUp(depositbillid);
                }

                return true;
            }
            catch {
                return false;
            }
        }
       
    }
}
