using Maticsoft.BLL;
using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using WinSaasPOS.Common;
using WinSaasPOS.Model;

namespace WinSaasPOS.ScaleUI
{
    public class Scale_Toledo
    {
       
        private string TaskFileName = "Task.xml";
        private string DeviceListFileName = "DeviceList.xml";
        private string CommandFileName = "Command.xml";
        private string DataFileName = "Data.xml";

       // private string taskid = "";  //任务ID必须唯一

        private string resultpath = AppDomain.CurrentDomain.BaseDirectory + "ToledoData" + "\\" + "TaskResult.xml";



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

        /// <summary>
        /// 当前传秤商品集合 开始就访问数据库
        /// </summary>
        private List<DBPRODUCT_BEANMODEL> CurrentLstPro = new List<DBPRODUCT_BEANMODEL>();

        private List<DBSCALE_KEY_BEANMODEL> CurrentLstScaleKey = new List<DBSCALE_KEY_BEANMODEL>();

        public bool SendToledoData(string scaleip,string port,out string msg)
        {
            try
            {
                if (!GlobalUtil.CheckIP(scaleip))
                {
                    msg = "电子秤连接失败";
                    return false;
                }

                msg = "";
                CurrentLstScaleKey = scalebll.GetModelList(" IP='" + scaleip + "' and CREATE_URL_IP ='" + MainModel.URL + "'");
                if (CurrentLstScaleKey == null || CurrentLstScaleKey.Count <= 0)
                {
                    msg = scaleip + "秤无商品信息";
                    return false;
                }
                string skucodes ="";
                foreach (DBSCALE_KEY_BEANMODEL scale in CurrentLstScaleKey)
                {
                    skucodes += scale.SKUCODE+",";
                }
                skucodes = skucodes.TrimEnd(',');

                CurrentLstPro = productbll.GetModelList(" CREATE_URL_IP='" + MainModel.URL + "' and SHOPID='" + MainModel.CurrentShopInfo.shopid + "' and SCALEFLAG=1");

                if (CurrentLstPro == null || CurrentLstPro.Count == 0)
                {
                    msg = scaleip + "未匹配到商品";
                    return false;
                }
                if (!SendLable(scaleip, port))
                {
                    msg = scaleip + "标签传递失败";
                    //return false;
                }

                if (!SendBarcode(scaleip, port))
                {
                    msg = scaleip + "条码传递失败";
                    //return false;
                }

                if (!SendPLU(scaleip, port))
                {
                    msg = scaleip + "PLU传递失败";
                    return false;
                }


                if (!SendPreset(scaleip, port))
                {
                    msg = scaleip + "快捷键传递失败";
                    return false;
                }


                if (!SendETText(scaleip, port))
                {
                    msg = scaleip + "附加文本传递失败";
                    return false;
                }



                return true;
            }
            catch (Exception ex)
            {
                WriteToledoLog("传秤异常"+ex.Message);
                msg = "传秤异常" + ex.Message;
                return false;
            }

            
        }

        /// <summary>
        /// 传标签
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        private bool SendLable(string ipaddress,string port)
        {
            try
            {

                if (!File.Exists(GetFilePath() + "\\" + "LBL00001.xml"))
                {
                    File.Copy(AppDomain.CurrentDomain.BaseDirectory + "LBL00001.xml", GetFilePath() + "\\" + "LBL00001.xml", true);
                }

                if (!File.Exists(GetFilePath() + "\\" + "LBL00002.xml"))
                {
                    File.Copy(AppDomain.CurrentDomain.BaseDirectory + "LBL00002.xml", GetFilePath() + "\\" + "LBL00002.xml", true);
                }

                string taskid = Guid.NewGuid().ToString();
                CreateTaskFile(taskid);
                CreateDeviceListFile("1",ipaddress,port);
                CreateCommandFile("LabelFormat", false);
                CreateLabelFile("LBL00001.xml", "1");
                ExecuteTaskInFile(taskid);
                if (!CheckTaskResult())
                {
                    return false;
                }

                taskid = Guid.NewGuid().ToString();
                CreateTaskFile(taskid);
                CreateDeviceListFile("1", ipaddress, port);
                CreateCommandFile("LabelFormat", false);
                CreateLabelFile("LBL00002.xml", "2");
                ExecuteTaskInFile(taskid);
                if (!CheckTaskResult())
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private bool SendBarcode(string ipaddress,string port)
        {
            try
            {
                string taskid = Guid.NewGuid().ToString();
                CreateTaskFile(taskid);
                CreateDeviceListFile("1", ipaddress, port);
                CreateCommandFile("Barcode", false);
                CreateBarcodeFile();
                ExecuteTaskInFile(taskid);
                if (!CheckTaskResult())
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 发送PLU 有增量商品时调用 不清空数据
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool SendIncrementPLU( List<DBPRODUCT_BEANMODEL> lstpro)
        {
            try
            {
                string ipaddress = "";
                string port="";
                List<DBSCALE_KEY_BEANMODEL> lstScale = scalebll.GetModelList(" SCALESTYPE ='" + "bplus" + "' and CREATE_URL_IP ='" + MainModel.URL + "'");
                if (lstScale != null && lstScale.Count > 0)
                {
                    ipaddress = lstScale[0].IP;
                    port = lstScale[0].PORT;
                }
                else
                {
                    return false;
                }

                string taskid = Guid.NewGuid().ToString();

                CreateTaskFile(taskid);
                CreateDeviceListFile("1", ipaddress, port);
                CreateCommandFile("Item", false);

                CreateDataFile(lstpro);

                ExecuteTaskInFile(taskid);
                if (!CheckTaskResult())
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 发送PLU
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        private bool SendPLU(string ipaddress, string port)
        {
            try
            {
                string taskid = Guid.NewGuid().ToString();

                CreateTaskFile(taskid);
                CreateDeviceListFile("1", ipaddress, port);
                CreateCommandFile("Item", true);

                CreateDataFile(CurrentLstPro);

                ExecuteTaskInFile(taskid);
                if (!CheckTaskResult())
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool SendPreset(string ipaddress, string port)
        {
            try
            {
                string taskid = Guid.NewGuid().ToString();

                CreateTaskFile(taskid);
                CreateDeviceListFile("1", ipaddress, port);
                CreateCommandFile("PresetDefinition", true);
                CreatePresetFile();

                ExecuteTaskInFile(taskid);
                if (!CheckTaskResult())
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private bool SendETText(string ipaddress, string port)
        {
            try
            {

                string taskid = Guid.NewGuid().ToString();

                CreateTaskFile(taskid);
                CreateDeviceListFile("1", ipaddress, port);
                CreateCommandFile("ExtraText", false);
                CreateETFile(CurrentLstPro);

                ExecuteTaskInFile(taskid);
                if (!CheckTaskResult())
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        #region  任务基础类

        [DllImport("MTScaleAPI.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ExecuteTaskInFile(string szTaskID, string szInputFile, string szOutputFile, bool bSynch);

        private void ExecuteTaskInFile(string taskid)
        {
            bool result = ExecuteTaskInFile(taskid, GetFilePath() + "\\" + TaskFileName, GetFilePath() + "\\" + "TaskResult.xml", true);  
        }



        /// <summary>
        /// 任务文件
        /// </summary>
        /// <param name="taskid"></param>
        private void CreateTaskFile(string taskid)
        {
            try
            {

                XmlDocument xmlDoc = new XmlDocument();
                //创建类型声明节点  
                XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
                xmlDoc.AppendChild(node);
                //创建根节点  
                XmlNode xnCommands = xmlDoc.CreateElement("MTTask");
                xmlDoc.AppendChild(xnCommands);
                CreateNode(xmlDoc, xnCommands, "TaskID", taskid);
                CreateNode(xmlDoc, xnCommands, "TaskType", "0");  //任务类型  0： 下发数据  98：查询单个任务状态
                CreateNode(xmlDoc, xnCommands, "TaskAction", "123");
                CreateNode(xmlDoc, xnCommands, "DataFile", DeviceListFileName); //电子秤列表文件名

                xmlDoc.Save(GetFilePath() + "\\" + TaskFileName);

            }
            catch (Exception ex)
            {
                WriteToledoLog("创建taskxml异常" + ex.Message);
            }
        }

        /// <summary>
        /// 设备列表文件
        /// </summary>
        /// <param name="deviceid"></param>
        /// <param name="ipaddress"></param>
        private void CreateDeviceListFile(string deviceid, string ipaddress,string port)
        {

            try
            {

                XmlDocument xmlDoc = new XmlDocument();
                //创建类型声明节点  
                XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
                xmlDoc.AppendChild(node);
                //创建根节点  
                XmlNode xnCommands = xmlDoc.CreateElement("Devices");
                xmlDoc.AppendChild(xnCommands);
                XmlNode scalenode = CreateNode(xmlDoc, xnCommands, "Scale", "");
                CreateNode(xmlDoc, scalenode, "DeviceID", deviceid);  //设备号，唯一标识，
                CreateNode(xmlDoc, scalenode, "ScaleNo", "1"); //秤号
                CreateNode(xmlDoc, scalenode, "ScaleType", "bPlus"); //秤类型
                CreateNode(xmlDoc, scalenode, "ConnectType", "Network");//通讯类型  Network： 局域网   interner:广域网

                XmlNode paramsnode = CreateNode(xmlDoc, scalenode, "ConnectParams", ""); //电子秤列表文件名

                XmlNode networkparams = CreateNode(xmlDoc, paramsnode, "NetworkParams", "");
                AddAttribute(networkparams, "Address", ipaddress);
                AddAttribute(networkparams, "Port", port); //固定端口

                CreateNode(xmlDoc, scalenode, "DecimalDigits", "2");  //小数点位数 用于金额的计算 国内一般设置为2
                CreateNode(xmlDoc, scalenode, "Enabled", "true");
                CreateNode(xmlDoc, scalenode, "DataFile", CommandFileName); //存放命令字 文件名

                xmlDoc.Save(GetFilePath() + "\\" + DeviceListFileName);

            }
            catch (Exception ex)
            {
                WriteToledoLog("创建taskxml异常"+ex.Message);
            }
        }

        /// <summary>
        /// 创建命令文件
        /// </summary>
        /// <param name="commandtype"></param>
        /// <param name="cleardata"></param>
        private void CreateCommandFile(string commandtype, bool cleardata)
        {

            try
            {

                XmlDocument xmlDoc = new XmlDocument();
                //创建类型声明节点  
                XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
                xmlDoc.AppendChild(node);
                //创建根节点  
                XmlNode xnCommands = xmlDoc.CreateElement("Commands");
                xmlDoc.AppendChild(xnCommands);
                XmlNode commandnode = CreateNode(xmlDoc, xnCommands, "Command", "");
                CreateNode(xmlDoc, commandnode, "CommandID", "test");  //命令编号，在每台秤的命令列表中必须唯一
                CreateNode(xmlDoc, commandnode, "CommandText", commandtype); //命令字， 表示和秤交互的数据类型 Item 代表plu  ExtraText附加文本  Barcode条码 PresetDefinition预置键
                CreateNode(xmlDoc, commandnode, "Control", "Update"); //命令控制字：Update： 更新数据Delete： 删除指定数据DeleteAll： 删除全部数据Read： 读取当前数据ReadAll：读取所有数据
                CreateNode(xmlDoc, commandnode, "DataFile", DataFileName);//存放命令字数据文件名
                CreateNode(xmlDoc, commandnode, "ClearData", cleardata.ToString()); //下发前是否清空数据

                xmlDoc.Save(GetFilePath() + "\\" + CommandFileName);

            }
            catch (Exception ex)
            {
                WriteToledoLog("创建taskxml异常"+ ex.Message);
            }
        }

        /// <summary>
        /// 创建PLU数据文件
        /// </summary>
        /// <param name="lstpro"></param>
        private void CreateDataFile(List<DBPRODUCT_BEANMODEL> lstpro)
        {
            if (!Directory.Exists(GetFilePath()))
            {
                Directory.CreateDirectory(GetFilePath());
            }

            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点  
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);

            //创建根节点  
            XmlNode xnCommands = xmlDoc.CreateElement("Data");
            xmlDoc.AppendChild(xnCommands);


            //商品信息
            foreach (DBPRODUCT_BEANMODEL pro in lstpro)
            {
                XmlNode itemnode = CreateNode(xmlDoc, xnCommands, "Item", "");

                //名称超过12秤无法打印
                if (pro != null && pro.SKUNAME != null && pro.SKUNAME.Length > 12)
                {
                    pro.SKUNAME = pro.SKUNAME.Substring(0,12);
                }

                //名称超过12秤无法打印
                if (pro != null && pro.SALEPRICE != null && pro.SALEPRICE > 9999)
                {
                    pro.SALEPRICE = 9999;
                }
                //标签号
                XmlNode labelformatsnode = CreateNode(xmlDoc, itemnode, "LabelFormats", "");
                XmlNode labelformatidnode;
                if (pro.WEIGHTFLAG == 1)
                {
                    labelformatidnode = CreateNode(xmlDoc, labelformatsnode, "LabelFormatID", "1");
                }
                else
                {
                    labelformatidnode = CreateNode(xmlDoc, labelformatsnode, "LabelFormatID", "2");
                }

                AddAttribute(labelformatidnode, "Index", "0");

                //标签号
                XmlNode barcodesnode = CreateNode(xmlDoc, itemnode, "Barcodes", "");
                XmlNode barcodeidnode = CreateNode(xmlDoc, barcodesnode, "BarcodeID", "1");

                //PLU
                CreateNode(xmlDoc, itemnode, "PLU", pro.INNERBARCODE.Substring(pro.INNERBARCODE.Length-8));
                //商品名称
                XmlNode descriptionsnode = CreateNode(xmlDoc, itemnode, "Descriptions", "");
                XmlNode descriptionnode = CreateNode(xmlDoc, descriptionsnode, "Description", pro.SKUNAME);
                AddAttribute(descriptionnode, "ID", "0");
                AddAttribute(descriptionnode, "Type", "ItemName");//商品名称
                AddAttribute(descriptionnode, "Language", "zho");
                AddAttribute(descriptionnode, "Index", "0");
                //绑定附加文本  以plu为唯一标识
                XmlNode descriptionETnode = CreateNode(xmlDoc, descriptionsnode, "Description", pro.INNERBARCODE.Substring(pro.INNERBARCODE.Length - 8));
                AddAttribute(descriptionETnode, "ID", pro.INNERBARCODE.Substring(pro.INNERBARCODE.Length - 8));
                AddAttribute(descriptionETnode, "Type", "ExtraText");  //附加文本
                AddAttribute(descriptionETnode, "Language", "zho");
                AddAttribute(descriptionETnode, "Index", "0");


                //价格
                XmlNode itempricesnode = CreateNode(xmlDoc, itemnode, "ItemPrices", "");
                XmlNode itempricenode = CreateNode(xmlDoc, itempricesnode, "ItemPrice", pro.SALEPRICE.ToString());

                AddAttribute(itempricenode, "Index", "0");
                AddAttribute(itempricenode, "Quantity", "0");
                AddAttribute(itempricenode, "Currency", "CNY");
                AddAttribute(itempricenode, "DiscountFlag", "true"); //允许打折标志
                AddAttribute(itempricenode, "PriceOverrideFlag", "true");//允许改价标志
                AddAttribute(itempricenode, "UnitDes", pro.SALESUNIT);//单位
                if (pro.WEIGHTFLAG == 1)
                {
                    AddAttribute(itempricenode, "UnitOfMeasureCode", "KGM"); //价格单位 元/千克
                }
                else
                {
                    AddAttribute(itempricenode, "UnitOfMeasureCode", "PCS"); //价格单位 计数
                }

                //包装日期 推荐日期  保质日期   
                XmlNode datesnode = CreateNode(xmlDoc, itemnode, "Dates", "");
                //XmlNode dateoffsetnode1 = CreateNode(xmlDoc, datesnode, "DateOffset", pro.SHELFLIFE.ToString());
                //AddAttribute(dateoffsetnode1, "Type", "BestBefore");//推荐日期

                //AddAttribute(dateoffsetnode1, "UnitOfOffset", "day");
                //AddAttribute(dateoffsetnode1, "PrintFormat", "MMDD");
                //AddAttribute(dateoffsetnode1, "PrintEnabled", "true");


                XmlNode dateoffsetnode2 = CreateNode(xmlDoc, datesnode, "DateOffset", pro.SHELFLIFE.ToString());
                AddAttribute(dateoffsetnode2, "Type", "SellBy");//保质期

                AddAttribute(dateoffsetnode2, "UnitOfOffset", "day");
                AddAttribute(dateoffsetnode2, "PrintFormat", "YYDDMM");
                AddAttribute(dateoffsetnode2, "PrintEnabled", "true");

                XmlNode dateoffsetnode3 = CreateNode(xmlDoc, datesnode, "DateOffset","0");  //打印当天日期  此日期会影响保质期
                AddAttribute(dateoffsetnode3, "Type", "PackedDate");//包装日期

                AddAttribute(dateoffsetnode3, "UnitOfOffset", "day");
                AddAttribute(dateoffsetnode3, "PrintFormat", "YYMMDD");
                AddAttribute(dateoffsetnode3, "PrintEnabled", "true");

            }

            xmlDoc.Save(GetFilePath() + "\\" + DataFileName);

        }

        /// <summary>
        /// 附加文本数据文件   附加文本 7 单位   附加文本8 商户名称
        /// </summary>
        /// <param name="lstpro"></param>
        private void CreateETFile(List<DBPRODUCT_BEANMODEL> lstpro)
        {

            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点  
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);

            //创建根节点  
            XmlNode xnCommands = xmlDoc.CreateElement("Data");
            xmlDoc.AppendChild(xnCommands);


            foreach (DBPRODUCT_BEANMODEL pro in lstpro)
            {
                XmlNode etnode = CreateNode(xmlDoc, xnCommands, "ExtraText", "");
                CreateNode(xmlDoc, etnode, "ID", pro.INNERBARCODE.Substring(pro.INNERBARCODE.Length - 8));
                CreateNode(xmlDoc, etnode, "DepartmentID", "0");
                CreateNode(xmlDoc, etnode, "Language", "zho");
                XmlNode ettextsnode = CreateNode(xmlDoc, etnode, "ETTexts", "");

                for (int i = 0; i < 10; i++)
                {
                    if (i == 6)
                    {
                        XmlNode ettextnode7 = CreateNode(xmlDoc, ettextsnode, "ETText", "元/" + pro.SALESUNIT);
                        AddAttribute(ettextnode7, "Index", i.ToString());
                    }
                    else if (i == 7)
                    {
                        XmlNode ettextnode7 = CreateNode(xmlDoc, ettextsnode, "ETText", MainModel.CurrentShopInfo.tenantname);
                        AddAttribute(ettextnode7, "Index", i.ToString());
                    }
                    else
                    {
                        XmlNode ettextnode7 = CreateNode(xmlDoc, ettextsnode, "ETText", "" + pro.SALESUNIT);
                        AddAttribute(ettextnode7, "Index", i.ToString());
                    }
                  
                }
            }
            xmlDoc.Save(GetFilePath() + "\\" + DataFileName);

        }

        /// <summary>
        /// 标签数据文件
        /// </summary>
        /// <param name="datafile"></param>
        /// <param name="labelno"></param>
        private void CreateLabelFile(string datafile, string labelno)
        {

            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点  
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);

            //创建根节点  
            XmlNode xnCommands = xmlDoc.CreateElement("Data");
            xmlDoc.AppendChild(xnCommands);

            XmlNode labelformatnode = CreateNode(xmlDoc, xnCommands, "LabelFormat", "");
            CreateNode(xmlDoc, labelformatnode, "LabelNo", labelno);
            CreateNode(xmlDoc, labelformatnode, "DataFile", datafile);

            xmlDoc.Save(GetFilePath() + "\\" + DataFileName);
        }

        /// <summary>
        /// 条码数据文件
        /// </summary>
        private void CreateBarcodeFile()
        {

            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点  
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);

            //创建根节点  
            XmlNode xnCommands = xmlDoc.CreateElement("Data");
            xmlDoc.AppendChild(xnCommands);

            XmlNode barcodenode1 = CreateNode(xmlDoc, xnCommands, "Barcode", "");
            AddAttribute(barcodenode1, "Symbology", "Code128");

            CreateNode(xmlDoc, barcodenode1, "ID", "1");
            CreateNode(xmlDoc, barcodenode1, "Description", "1");
            CreateNode(xmlDoc, barcodenode1, "Definition", "2500PPPPPPPPQQQQQC");


            XmlNode barcodenode2 = CreateNode(xmlDoc, xnCommands, "Barcode", "");
            AddAttribute(barcodenode2, "Symbology", "Code128");

            CreateNode(xmlDoc, barcodenode2, "ID", "2");
            CreateNode(xmlDoc, barcodenode2, "Description", "2");
            CreateNode(xmlDoc, barcodenode2, "Definition", "2600PPPPPPPP00001C");

            xmlDoc.Save(GetFilePath() + "\\" + DataFileName);

        }

        /// <summary>
        /// 快捷键数据文件
        /// </summary>
        public void CreatePresetFile()
        {
            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点  
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);

            //创建根节点  
            XmlNode datanode = xmlDoc.CreateElement("Data");
            xmlDoc.AppendChild(datanode);

            XmlNode presetdefinitionnode = CreateNode(xmlDoc, datanode, "PresetDefinition", "");

            XmlNode presetdpagenode = CreateNode(xmlDoc, presetdefinitionnode, "PresetPage", "");
            CreateNode(xmlDoc, presetdpagenode, "ID", "1");
           // CreateNode(xmlDoc, presetdpagenode, "KeyCount", "77");

            

            foreach (DBSCALE_KEY_BEANMODEL scalekey in CurrentLstScaleKey)
            {
                XmlNode presetkeynode = CreateNode(xmlDoc, presetdefinitionnode, "PresetKey", "");

                int row = scalekey.SCALESROW - scalekey.YPOS;

                int column = scalekey.XPOS + 1;

                int keyno = (row - 1) * 7 + column;
                string innerbarcode="";
                 List<DBPRODUCT_BEANMODEL> lstpro = CurrentLstPro.Where(r => r.SKUCODE == scalekey.SKUCODE).ToList();

                 if (lstpro != null && lstpro.Count > 0)
                 {
                     innerbarcode = lstpro[0].INNERBARCODE.Substring(lstpro[0].INNERBARCODE.Length-8);
                 }

                CreateNode(xmlDoc, presetkeynode, "PageID", "1");
                CreateNode(xmlDoc, presetkeynode, "Type", "PLU");
                CreateNode(xmlDoc, presetkeynode, "KeyNo", keyno.ToString());
                CreateNode(xmlDoc, presetkeynode, "Level", scalekey.SETTINGPAGE.ToString());
                CreateNode(xmlDoc, presetkeynode, "Row", row.ToString());
                CreateNode(xmlDoc, presetkeynode, "Column", column.ToString());
                XmlNode destinationnode = CreateNode(xmlDoc, presetkeynode, "Destination", innerbarcode);  
                AddAttribute(destinationnode, "DepartmentID","1");
                
                XmlNode titlenode = CreateNode(xmlDoc, presetkeynode, "Title", "AA");
                AddAttribute(titlenode, "Language","zho");


            }

            xmlDoc.Save(GetFilePath() + "\\" + DataFileName);
        }
        #endregion
        /// <summary>    
        /// 创建节点    
        /// </summary>    
        /// <param name="xmldoc"></param>  xml文档  
        /// <param name="parentnode"></param>父节点    
        /// <param name="name"></param>  节点名  
        /// <param name="value"></param>  节点值  
        ///   
        public XmlNode CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);

            if (!string.IsNullOrEmpty(value))
            {
                node.InnerText = value;

            }
            parentNode.AppendChild(node);

            return node;
        }

        /// <summary>
        /// 添加节点属性
        /// </summary>
        /// <param name="node">节点名</param>
        /// <param name="name">属性名</param>
        /// <param name="value">属性值</param>
        private void AddAttribute(XmlNode node, string name, string value)
        {
            if (node == null || name == null || name.Length == 0)
                return;

            XmlAttribute attr = node.OwnerDocument.CreateAttribute(name);
            attr.Value = value;
            node.Attributes.Append(attr);
        }

        private string TaskResultMsg="";
        private bool CheckTaskResult()
        {
            try
            {
                XmlDocument doc = new XmlDocument();

                doc.Load(resultpath);

                XmlNode scaleposxn = doc.SelectSingleNode("MTTaskResult");

                XmlNode modelxn = scaleposxn.SelectSingleNode("ReturnCode");
                string result = modelxn.InnerText;

                switch (result)
                {
                    case ("DataFileError"): TaskResultMsg = "数据文件错误"; break;
                        case("ScaleSpaceFullError"): TaskResultMsg="电子秤空间不足";break;
                        case ("TaskTypeError"): TaskResultMsg = "任务类型错误"; break;
                        case ("TaskRepeatError"): TaskResultMsg = "任务重复"; break;
                        case ("TransferError"): TaskResultMsg = "传输错误"; break;
                        case ("ScaleDataError"): TaskResultMsg = "电子秤数据错误"; break;
                        case ("ConnectError"): TaskResultMsg = "电子秤连接错误"; break;
                        case ("ServiceError"): TaskResultMsg = "服务连接不上错误"; break;
                        default: TaskResultMsg = result; break;

                }

                return result == "OK";
            }
            catch (Exception ex)
            {
                return true;//检测结果出现异常 ，返回正确  
            }
        }

        private void WriteToledoLog(string log)
        {
            LogManager.WriteLog("ToledoLog", log);
        }

        public string GetFilePath()
        {
            try
            {
                 string FilePath = AppDomain.CurrentDomain.BaseDirectory + "ToledoData";

                 if (!Directory.Exists(FilePath))
                 {
                     Directory.CreateDirectory(FilePath);
                 }

                 return FilePath;
            }
            catch (Exception ex)
            {
                return AppDomain.CurrentDomain.BaseDirectory + "ToledoData";
            }
        }


    }
}
