using System;
using System.Data;
using System.Text;
using System.Data.SQLite;
using Maticsoft.DBUtility;
using Maticsoft.Model;
using System.Collections.Generic;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:DBPRODUCT_BEAN
	/// </summary>
	public partial class DBPRODUCT_BEANDAL
	{
		public DBPRODUCT_BEANDAL()
		{}


        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long _id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from DBPRODUCT_BEAN");
            strSql.Append(" where _id=@_id");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@_id", DbType.Int64,4)
			};
            parameters[0].Value = _id;

            return DbHelperSQLite.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.DBPRODUCT_BEANMODEL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DBPRODUCT_BEAN(");
            strSql.Append("SKUCODE,GOODS_ID,CATEGORYID,CATEGORYNAME,ORIGINPRICE,SALEPRICE,SPECIAL_PRICE,TOTALSTOCKQTY,SALESUNIT,SHOPID,TENANTID,TITLE,SKUNAME,BARCODE,SPECDESC,GOODSTAGID,WEIGHTFLAG,NUM,SPECNUM,SPECTYPE,PRICETAG,PRICETAGID,CREATE_URL_IP,TAGFORMAT,BARCODEFORMAT,BESTDAYS,SPINFO,QRCODECONTENT,INGREDIENT,LOCATION,SPEC,STORE_TYPE,COMPANY,REMARK,SPECIALMESSAGE,FIRSTCATEGORYID,FIRSTCATEGORYNAME,SECONDCATEGORYID,SECONDCATEGORYNAME,PANELFLAG,PANELSHOWFLAG,MAINIMG,STATUS,SPECIAL_STATUS,IS_QUERY_BARCODE,CREATEDAT,SALECOUNT,INNERBARCODE,FIRST_LETTER,ALL_FIRST_LETTER,SHELFLIFE,SKUTYPE,SCALEFLAG,MEMBERPRICE,LOCALSTATUS)");
            strSql.Append(" values (");
            strSql.Append("@SKUCODE,@GOODS_ID,@CATEGORYID,@CATEGORYNAME,@ORIGINPRICE,@SALEPRICE,@SPECIAL_PRICE,@TOTALSTOCKQTY,@SALESUNIT,@SHOPID,@TENANTID,@TITLE,@SKUNAME,@BARCODE,@SPECDESC,@GOODSTAGID,@WEIGHTFLAG,@NUM,@SPECNUM,@SPECTYPE,@PRICETAG,@PRICETAGID,@CREATE_URL_IP,@TAGFORMAT,@BARCODEFORMAT,@BESTDAYS,@SPINFO,@QRCODECONTENT,@INGREDIENT,@LOCATION,@SPEC,@STORE_TYPE,@COMPANY,@REMARK,@SPECIALMESSAGE,@FIRSTCATEGORYID,@FIRSTCATEGORYNAME,@SECONDCATEGORYID,@SECONDCATEGORYNAME,@PANELFLAG,@PANELSHOWFLAG,@MAINIMG,@STATUS,@SPECIAL_STATUS,@IS_QUERY_BARCODE,@CREATEDAT,@SALECOUNT,@INNERBARCODE,@FIRST_LETTER,@ALL_FIRST_LETTER,@SHELFLIFE,@SKUTYPE,@SCALEFLAG,@MEMBERPRICE,@LOCALSTATUS)");
            strSql.Append(";select LAST_INSERT_ROWID()");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@SKUCODE", DbType.String),
					new SQLiteParameter("@GOODS_ID", DbType.String),
					new SQLiteParameter("@CATEGORYID", DbType.String),
					new SQLiteParameter("@CATEGORYNAME", DbType.String),
					new SQLiteParameter("@ORIGINPRICE", DbType.Decimal,4),
					new SQLiteParameter("@SALEPRICE", DbType.Decimal,4),
					new SQLiteParameter("@SPECIAL_PRICE", DbType.Decimal,4),
					new SQLiteParameter("@TOTALSTOCKQTY", DbType.Decimal,4),
					new SQLiteParameter("@SALESUNIT", DbType.String),
					new SQLiteParameter("@SHOPID", DbType.String),
					new SQLiteParameter("@TENANTID", DbType.String),
					new SQLiteParameter("@TITLE", DbType.String),
					new SQLiteParameter("@SKUNAME", DbType.String),
					new SQLiteParameter("@BARCODE", DbType.String),
					new SQLiteParameter("@SPECDESC", DbType.String),
					new SQLiteParameter("@GOODSTAGID", DbType.Int64,8),
					new SQLiteParameter("@WEIGHTFLAG", DbType.Int64,8),
					new SQLiteParameter("@NUM", DbType.Decimal,4),
					new SQLiteParameter("@SPECNUM", DbType.Decimal,4),
					new SQLiteParameter("@SPECTYPE", DbType.Int64,8),
					new SQLiteParameter("@PRICETAG", DbType.String),
					new SQLiteParameter("@PRICETAGID", DbType.Int64,8),
					new SQLiteParameter("@CREATE_URL_IP", DbType.String),
					new SQLiteParameter("@TAGFORMAT", DbType.String),
					new SQLiteParameter("@BARCODEFORMAT", DbType.String),
					new SQLiteParameter("@BESTDAYS", DbType.Int64,8),
					new SQLiteParameter("@SPINFO", DbType.String),
					new SQLiteParameter("@QRCODECONTENT", DbType.String),
					new SQLiteParameter("@INGREDIENT", DbType.String),
					new SQLiteParameter("@LOCATION", DbType.String),
					new SQLiteParameter("@SPEC", DbType.String),
					new SQLiteParameter("@STORE_TYPE", DbType.String),
					new SQLiteParameter("@COMPANY", DbType.String),
					new SQLiteParameter("@REMARK", DbType.String),
					new SQLiteParameter("@SPECIALMESSAGE", DbType.String),
					new SQLiteParameter("@FIRSTCATEGORYID", DbType.String),
					new SQLiteParameter("@FIRSTCATEGORYNAME", DbType.String),
					new SQLiteParameter("@SECONDCATEGORYID", DbType.String),
					new SQLiteParameter("@SECONDCATEGORYNAME", DbType.String),
					new SQLiteParameter("@PANELFLAG", DbType.String),
					new SQLiteParameter("@PANELSHOWFLAG", DbType.Int64,8),
					new SQLiteParameter("@MAINIMG", DbType.String),
					new SQLiteParameter("@STATUS", DbType.Int64,8),
					new SQLiteParameter("@SPECIAL_STATUS", DbType.Int64,8),
					new SQLiteParameter("@IS_QUERY_BARCODE", DbType.Int64,8),
					new SQLiteParameter("@CREATEDAT", DbType.Int64,8),
					new SQLiteParameter("@SALECOUNT", DbType.Int64,8),
					new SQLiteParameter("@INNERBARCODE", DbType.String),
					new SQLiteParameter("@FIRST_LETTER", DbType.String),
					new SQLiteParameter("@ALL_FIRST_LETTER", DbType.String),
					new SQLiteParameter("@SHELFLIFE", DbType.Int64,8),
					new SQLiteParameter("@SKUTYPE", DbType.Int64,8),
					new SQLiteParameter("@SCALEFLAG", DbType.Int64,8),
                    new SQLiteParameter("@LOCALSTATUS", DbType.Int64,8),
                                           new SQLiteParameter("@MEMBERPRICE", DbType.Decimal,4)
                                           };
            parameters[0].Value = model.SKUCODE;
            parameters[1].Value = model.GOODS_ID;
            parameters[2].Value = model.CATEGORYID;
            parameters[3].Value = model.CATEGORYNAME;
            parameters[4].Value = model.ORIGINPRICE;
            parameters[5].Value = model.SALEPRICE;
            parameters[6].Value = model.SPECIAL_PRICE;
            parameters[7].Value = model.TOTALSTOCKQTY;
            parameters[8].Value = model.SALESUNIT;
            parameters[9].Value = model.SHOPID;
            parameters[10].Value = model.TENANTID;
            parameters[11].Value = model.TITLE;
            parameters[12].Value = model.SKUNAME;
            parameters[13].Value = model.BARCODE;
            parameters[14].Value = model.SPECDESC;
            parameters[15].Value = model.GOODSTAGID;
            parameters[16].Value = model.WEIGHTFLAG;
            parameters[17].Value = model.NUM;
            parameters[18].Value = model.SPECNUM;
            parameters[19].Value = model.SPECTYPE;
            parameters[20].Value = model.PRICETAG;
            parameters[21].Value = model.PRICETAGID;
            parameters[22].Value = model.CREATE_URL_IP;
            parameters[23].Value = model.TAGFORMAT;
            parameters[24].Value = model.BARCODEFORMAT;
            parameters[25].Value = model.BESTDAYS;
            parameters[26].Value = model.SPINFO;
            parameters[27].Value = model.QRCODECONTENT;
            parameters[28].Value = model.INGREDIENT;
            parameters[29].Value = model.LOCATION;
            parameters[30].Value = model.SPEC;
            parameters[31].Value = model.STORE_TYPE;
            parameters[32].Value = model.COMPANY;
            parameters[33].Value = model.REMARK;
            parameters[34].Value = model.SPECIALMESSAGE;
            parameters[35].Value = model.FIRSTCATEGORYID;
            parameters[36].Value = model.FIRSTCATEGORYNAME;
            parameters[37].Value = model.SECONDCATEGORYID;
            parameters[38].Value = model.SECONDCATEGORYNAME;
            parameters[39].Value = model.PANELFLAG;
            parameters[40].Value = model.PANELSHOWFLAG;
            parameters[41].Value = model.MAINIMG;
            parameters[42].Value = model.STATUS;
            parameters[43].Value = model.SPECIAL_STATUS;
            parameters[44].Value = model.IS_QUERY_BARCODE;
            parameters[45].Value = model.CREATEDAT;
            parameters[46].Value = model.SALECOUNT;
            parameters[47].Value = model.INNERBARCODE;
            parameters[48].Value = ConvertToFirstPinYin(model.SKUNAME);
            parameters[49].Value = ConvertToPinYin(model.SKUNAME);
            parameters[50].Value = model.SHELFLIFE;
            parameters[51].Value = model.SKUTYPE;
            parameters[52].Value = model.SCALEFLAG;
            parameters[53].Value = model.LOCALSTATUS;
            parameters[54].Value = model.MEMBERPRICE;

            object obj = DbHelperSQLite.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.DBPRODUCT_BEANMODEL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DBPRODUCT_BEAN set ");
            strSql.Append("SKUCODE=@SKUCODE,");
            strSql.Append("GOODS_ID=@GOODS_ID,");
            strSql.Append("CATEGORYID=@CATEGORYID,");
            strSql.Append("CATEGORYNAME=@CATEGORYNAME,");
            strSql.Append("ORIGINPRICE=@ORIGINPRICE,");
            strSql.Append("SALEPRICE=@SALEPRICE,");
            strSql.Append("SPECIAL_PRICE=@SPECIAL_PRICE,");
            strSql.Append("TOTALSTOCKQTY=@TOTALSTOCKQTY,");
            strSql.Append("SALESUNIT=@SALESUNIT,");
            strSql.Append("SHOPID=@SHOPID,");
            strSql.Append("TENANTID=@TENANTID,");
            strSql.Append("TITLE=@TITLE,");
            strSql.Append("SKUNAME=@SKUNAME,");
            strSql.Append("BARCODE=@BARCODE,");
            strSql.Append("SPECDESC=@SPECDESC,");
            strSql.Append("GOODSTAGID=@GOODSTAGID,");
            strSql.Append("WEIGHTFLAG=@WEIGHTFLAG,");
            strSql.Append("NUM=@NUM,");
            strSql.Append("SPECNUM=@SPECNUM,");
            strSql.Append("SPECTYPE=@SPECTYPE,");
            strSql.Append("PRICETAG=@PRICETAG,");
            strSql.Append("PRICETAGID=@PRICETAGID,");
            strSql.Append("CREATE_URL_IP=@CREATE_URL_IP,");
            strSql.Append("TAGFORMAT=@TAGFORMAT,");
            strSql.Append("BARCODEFORMAT=@BARCODEFORMAT,");
            strSql.Append("BESTDAYS=@BESTDAYS,");
            strSql.Append("SPINFO=@SPINFO,");
            strSql.Append("QRCODECONTENT=@QRCODECONTENT,");
            strSql.Append("INGREDIENT=@INGREDIENT,");
            strSql.Append("LOCATION=@LOCATION,");
            strSql.Append("SPEC=@SPEC,");
            strSql.Append("STORE_TYPE=@STORE_TYPE,");
            strSql.Append("COMPANY=@COMPANY,");
            strSql.Append("REMARK=@REMARK,");
            strSql.Append("SPECIALMESSAGE=@SPECIALMESSAGE,");
            strSql.Append("FIRSTCATEGORYID=@FIRSTCATEGORYID,");
            strSql.Append("FIRSTCATEGORYNAME=@FIRSTCATEGORYNAME,");
            strSql.Append("SECONDCATEGORYID=@SECONDCATEGORYID,");
            strSql.Append("SECONDCATEGORYNAME=@SECONDCATEGORYNAME,");
            strSql.Append("PANELFLAG=@PANELFLAG,");
            strSql.Append("PANELSHOWFLAG=@PANELSHOWFLAG,");
            strSql.Append("MAINIMG=@MAINIMG,");
            strSql.Append("STATUS=@STATUS,");
            strSql.Append("SPECIAL_STATUS=@SPECIAL_STATUS,");
            strSql.Append("IS_QUERY_BARCODE=@IS_QUERY_BARCODE,");
            strSql.Append("CREATEDAT=@CREATEDAT,");
            strSql.Append("SALECOUNT=@SALECOUNT,");
            strSql.Append("INNERBARCODE=@INNERBARCODE,");
            strSql.Append("FIRST_LETTER=@FIRST_LETTER,");
            strSql.Append("ALL_FIRST_LETTER=@ALL_FIRST_LETTER,");
            strSql.Append("SHELFLIFE=@SHELFLIFE,");
            strSql.Append("SKUTYPE=@SKUTYPE,");
            strSql.Append("SCALEFLAG=@SCALEFLAG,");
            strSql.Append("LOCALSTATUS=@LOCALSTATUS,");
            strSql.Append("MEMBERPRICE=@MEMBERPRICE");
            strSql.Append(" where _id=@_id");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@SKUCODE", DbType.String),
					new SQLiteParameter("@GOODS_ID", DbType.String),
					new SQLiteParameter("@CATEGORYID", DbType.String),
					new SQLiteParameter("@CATEGORYNAME", DbType.String),
					new SQLiteParameter("@ORIGINPRICE", DbType.Decimal,4),
					new SQLiteParameter("@SALEPRICE", DbType.Decimal,4),
					new SQLiteParameter("@SPECIAL_PRICE", DbType.Decimal,4),
					new SQLiteParameter("@TOTALSTOCKQTY", DbType.Decimal,4),
					new SQLiteParameter("@SALESUNIT", DbType.String),
					new SQLiteParameter("@SHOPID", DbType.String),
					new SQLiteParameter("@TENANTID", DbType.String),
					new SQLiteParameter("@TITLE", DbType.String),
					new SQLiteParameter("@SKUNAME", DbType.String),
					new SQLiteParameter("@BARCODE", DbType.String),
					new SQLiteParameter("@SPECDESC", DbType.String),
					new SQLiteParameter("@GOODSTAGID", DbType.Int64,8),
					new SQLiteParameter("@WEIGHTFLAG", DbType.Int64,8),
					new SQLiteParameter("@NUM", DbType.Decimal,4),
					new SQLiteParameter("@SPECNUM", DbType.Decimal,4),
					new SQLiteParameter("@SPECTYPE", DbType.Int64,8),
					new SQLiteParameter("@PRICETAG", DbType.String),
					new SQLiteParameter("@PRICETAGID", DbType.Int64,8),
					new SQLiteParameter("@CREATE_URL_IP", DbType.String),
					new SQLiteParameter("@TAGFORMAT", DbType.String),
					new SQLiteParameter("@BARCODEFORMAT", DbType.String),
					new SQLiteParameter("@BESTDAYS", DbType.Int64,8),
					new SQLiteParameter("@SPINFO", DbType.String),
					new SQLiteParameter("@QRCODECONTENT", DbType.String),
					new SQLiteParameter("@INGREDIENT", DbType.String),
					new SQLiteParameter("@LOCATION", DbType.String),
					new SQLiteParameter("@SPEC", DbType.String),
					new SQLiteParameter("@STORE_TYPE", DbType.String),
					new SQLiteParameter("@COMPANY", DbType.String),
					new SQLiteParameter("@REMARK", DbType.String),
					new SQLiteParameter("@SPECIALMESSAGE", DbType.String),
					new SQLiteParameter("@FIRSTCATEGORYID", DbType.String),
					new SQLiteParameter("@FIRSTCATEGORYNAME", DbType.String),
					new SQLiteParameter("@SECONDCATEGORYID", DbType.String),
					new SQLiteParameter("@SECONDCATEGORYNAME", DbType.String),
					new SQLiteParameter("@PANELFLAG", DbType.String),
					new SQLiteParameter("@PANELSHOWFLAG", DbType.Int64,8),
					new SQLiteParameter("@MAINIMG", DbType.String),
					new SQLiteParameter("@STATUS", DbType.Int64,8),
					new SQLiteParameter("@SPECIAL_STATUS", DbType.Int64,8),
					new SQLiteParameter("@IS_QUERY_BARCODE", DbType.Int64,8),
					new SQLiteParameter("@CREATEDAT", DbType.Int64,8),
					new SQLiteParameter("@SALECOUNT", DbType.Int64,8),
					new SQLiteParameter("@INNERBARCODE", DbType.String),
					new SQLiteParameter("@FIRST_LETTER", DbType.String),
					new SQLiteParameter("@ALL_FIRST_LETTER", DbType.String),
					new SQLiteParameter("@SHELFLIFE", DbType.Int64,8),
					new SQLiteParameter("@SKUTYPE", DbType.Int64,8),
					new SQLiteParameter("@SCALEFLAG", DbType.Int64,8),
                    new SQLiteParameter("@LOCALSTATUS", DbType.Int64,8),
                    new SQLiteParameter("@MEMBERPRICE", DbType.Decimal,4),
					new SQLiteParameter("@_id", DbType.Int64,8)};
            parameters[0].Value = model.SKUCODE;
            parameters[1].Value = model.GOODS_ID;
            parameters[2].Value = model.CATEGORYID;
            parameters[3].Value = model.CATEGORYNAME;
            parameters[4].Value = model.ORIGINPRICE;
            parameters[5].Value = model.SALEPRICE;
            parameters[6].Value = model.SPECIAL_PRICE;
            parameters[7].Value = model.TOTALSTOCKQTY;
            parameters[8].Value = model.SALESUNIT;
            parameters[9].Value = model.SHOPID;
            parameters[10].Value = model.TENANTID;
            parameters[11].Value = model.TITLE;
            parameters[12].Value = model.SKUNAME;
            parameters[13].Value = model.BARCODE;
            parameters[14].Value = model.SPECDESC;
            parameters[15].Value = model.GOODSTAGID;
            parameters[16].Value = model.WEIGHTFLAG;
            parameters[17].Value = model.NUM;
            parameters[18].Value = model.SPECNUM;
            parameters[19].Value = model.SPECTYPE;
            parameters[20].Value = model.PRICETAG;
            parameters[21].Value = model.PRICETAGID;
            parameters[22].Value = model.CREATE_URL_IP;
            parameters[23].Value = model.TAGFORMAT;
            parameters[24].Value = model.BARCODEFORMAT;
            parameters[25].Value = model.BESTDAYS;
            parameters[26].Value = model.SPINFO;
            parameters[27].Value = model.QRCODECONTENT;
            parameters[28].Value = model.INGREDIENT;
            parameters[29].Value = model.LOCATION;
            parameters[30].Value = model.SPEC;
            parameters[31].Value = model.STORE_TYPE;
            parameters[32].Value = model.COMPANY;
            parameters[33].Value = model.REMARK;
            parameters[34].Value = model.SPECIALMESSAGE;
            parameters[35].Value = model.FIRSTCATEGORYID;
            parameters[36].Value = model.FIRSTCATEGORYNAME;
            parameters[37].Value = model.SECONDCATEGORYID;
            parameters[38].Value = model.SECONDCATEGORYNAME;
            parameters[39].Value = model.PANELFLAG;
            parameters[40].Value = model.PANELSHOWFLAG;
            parameters[41].Value = model.MAINIMG;
            parameters[42].Value = model.STATUS;
            parameters[43].Value = model.SPECIAL_STATUS;
            parameters[44].Value = model.IS_QUERY_BARCODE;
            parameters[45].Value = model.CREATEDAT;
            parameters[46].Value = model.SALECOUNT;
            parameters[47].Value = model.INNERBARCODE;
            parameters[48].Value = ConvertToFirstPinYin(model.SKUNAME);
            parameters[49].Value = ConvertToPinYin(model.SKUNAME);
            parameters[50].Value = model.SHELFLIFE;
            parameters[51].Value = model.SKUTYPE;
            parameters[52].Value = model.SCALEFLAG;
            parameters[53].Value = model.LOCALSTATUS;
            parameters[54].Value = model.MEMBERPRICE;
            parameters[55].Value = model._id;

            int rows = DbHelperSQLite.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int _id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from DBPRODUCT_BEAN ");
            strSql.Append(" where _id=@_id");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@_id", DbType.Int64,4)
			};
            parameters[0].Value = _id;

            int rows = DbHelperSQLite.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string _idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from DBPRODUCT_BEAN ");
            strSql.Append(" where _id in (" + _idlist + ")  ");
            int rows = DbHelperSQLite.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.DBPRODUCT_BEANMODEL GetModel(int _id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select _id,SKUCODE,GOODS_ID,CATEGORYID,CATEGORYNAME,ORIGINPRICE,SALEPRICE,SPECIAL_PRICE,TOTALSTOCKQTY,SALESUNIT,SHOPID,TENANTID,TITLE,SKUNAME,BARCODE,SPECDESC,GOODSTAGID,WEIGHTFLAG,NUM,SPECNUM,SPECTYPE,PRICETAG,PRICETAGID,CREATE_URL_IP,TAGFORMAT,BARCODEFORMAT,BESTDAYS,SPINFO,QRCODECONTENT,INGREDIENT,LOCATION,SPEC,STORE_TYPE,COMPANY,REMARK,SPECIALMESSAGE,FIRSTCATEGORYID,FIRSTCATEGORYNAME,SECONDCATEGORYID,SECONDCATEGORYNAME,PANELFLAG,PANELSHOWFLAG,MAINIMG,STATUS,SPECIAL_STATUS,IS_QUERY_BARCODE,CREATEDAT,SALECOUNT,INNERBARCODE,FIRST_LETTER,ALL_FIRST_LETTER,SHELFLIFE,SKUTYPE,SCALEFLAG,MEMBERPRICE,LOCALSTATUS, from DBPRODUCT_BEAN ");
            strSql.Append(" where _id=@_id");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@_id", DbType.Int64,4)
			};
            parameters[0].Value = _id;

            Maticsoft.Model.DBPRODUCT_BEANMODEL model = new Maticsoft.Model.DBPRODUCT_BEANMODEL();
            DataSet ds = DbHelperSQLite.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.DBPRODUCT_BEANMODEL DataRowToModel(DataRow row)
        {
            Maticsoft.Model.DBPRODUCT_BEANMODEL model = new Maticsoft.Model.DBPRODUCT_BEANMODEL();
            if (row != null)
            {
                if (row["_id"] != null && row["_id"].ToString() != "")
                {
                    model._id = Int64.Parse(row["_id"].ToString());
                }
                if (row["SKUCODE"] != null)
                {
                    model.SKUCODE = row["SKUCODE"].ToString();
                }
                if (row["GOODS_ID"] != null)
                {
                    model.GOODS_ID = row["GOODS_ID"].ToString();
                }
                if (row["CATEGORYID"] != null)
                {
                    model.CATEGORYID = row["CATEGORYID"].ToString();
                }
                if (row["CATEGORYNAME"] != null)
                {
                    model.CATEGORYNAME = row["CATEGORYNAME"].ToString();
                }
                if (row["ORIGINPRICE"] != null && row["ORIGINPRICE"].ToString() != "")
                {
                    model.ORIGINPRICE = decimal.Parse(row["ORIGINPRICE"].ToString());
                }
                if (row["SALEPRICE"] != null && row["SALEPRICE"].ToString() != "")
                {
                    model.SALEPRICE = decimal.Parse(row["SALEPRICE"].ToString());
                }
                if (row["SPECIAL_PRICE"] != null && row["SPECIAL_PRICE"].ToString() != "")
                {
                    model.SPECIAL_PRICE = decimal.Parse(row["SPECIAL_PRICE"].ToString());
                }
                if (row["TOTALSTOCKQTY"] != null && row["TOTALSTOCKQTY"].ToString() != "")
                {
                    model.TOTALSTOCKQTY = decimal.Parse(row["TOTALSTOCKQTY"].ToString());
                }
                if (row["SALESUNIT"] != null)
                {
                    model.SALESUNIT = row["SALESUNIT"].ToString();
                }
                if (row["SHOPID"] != null)
                {
                    model.SHOPID = row["SHOPID"].ToString();
                }
                if (row["TENANTID"] != null)
                {
                    model.TENANTID = row["TENANTID"].ToString();
                }
                if (row["TITLE"] != null)
                {
                    model.TITLE = row["TITLE"].ToString();
                }
                if (row["SKUNAME"] != null)
                {
                    model.SKUNAME = row["SKUNAME"].ToString();
                }
                if (row["BARCODE"] != null)
                {
                    model.BARCODE = row["BARCODE"].ToString();
                }
                if (row["SPECDESC"] != null)
                {
                    model.SPECDESC = row["SPECDESC"].ToString();
                }
                if (row["GOODSTAGID"] != null && row["GOODSTAGID"].ToString() != "")
                {
                    model.GOODSTAGID = Int64.Parse(row["GOODSTAGID"].ToString());
                }
                if (row["WEIGHTFLAG"] != null && row["WEIGHTFLAG"].ToString() != "")
                {
                    model.WEIGHTFLAG = int.Parse(row["WEIGHTFLAG"].ToString());
                }
                if (row["NUM"] != null && row["NUM"].ToString() != "")
                {
                    model.NUM = decimal.Parse(row["NUM"].ToString());
                }
                if (row["SPECNUM"] != null && row["SPECNUM"].ToString() != "")
                {
                    model.SPECNUM = decimal.Parse(row["SPECNUM"].ToString());
                }
                if (row["SPECTYPE"] != null && row["SPECTYPE"].ToString() != "")
                {
                    model.SPECTYPE = int.Parse(row["SPECTYPE"].ToString());
                }
                if (row["PRICETAG"] != null)
                {
                    model.PRICETAG = row["PRICETAG"].ToString();
                }
                if (row["PRICETAGID"] != null && row["PRICETAGID"].ToString() != "")
                {
                    model.PRICETAGID = Int64.Parse(row["PRICETAGID"].ToString());
                }
                if (row["CREATE_URL_IP"] != null)
                {
                    model.CREATE_URL_IP = row["CREATE_URL_IP"].ToString();
                }
                if (row["TAGFORMAT"] != null)
                {
                    model.TAGFORMAT = row["TAGFORMAT"].ToString();
                }
                if (row["BARCODEFORMAT"] != null)
                {
                    model.BARCODEFORMAT = row["BARCODEFORMAT"].ToString();
                }
                if (row["BESTDAYS"] != null && row["BESTDAYS"].ToString() != "")
                {
                    model.BESTDAYS = Int64.Parse(row["BESTDAYS"].ToString());
                }
                if (row["SPINFO"] != null)
                {
                    model.SPINFO = row["SPINFO"].ToString();
                }
                if (row["QRCODECONTENT"] != null)
                {
                    model.QRCODECONTENT = row["QRCODECONTENT"].ToString();
                }
                if (row["INGREDIENT"] != null)
                {
                    model.INGREDIENT = row["INGREDIENT"].ToString();
                }
                if (row["LOCATION"] != null)
                {
                    model.LOCATION = row["LOCATION"].ToString();
                }
                if (row["SPEC"] != null)
                {
                    model.SPEC = row["SPEC"].ToString();
                }
                if (row["STORE_TYPE"] != null)
                {
                    model.STORE_TYPE = row["STORE_TYPE"].ToString();
                }
                if (row["COMPANY"] != null)
                {
                    model.COMPANY = row["COMPANY"].ToString();
                }
                if (row["REMARK"] != null)
                {
                    model.REMARK = row["REMARK"].ToString();
                }
                if (row["SPECIALMESSAGE"] != null)
                {
                    model.SPECIALMESSAGE = row["SPECIALMESSAGE"].ToString();
                }
                if (row["FIRSTCATEGORYID"] != null)
                {
                    model.FIRSTCATEGORYID = row["FIRSTCATEGORYID"].ToString();
                }
                if (row["FIRSTCATEGORYNAME"] != null)
                {
                    model.FIRSTCATEGORYNAME = row["FIRSTCATEGORYNAME"].ToString();
                }
                if (row["SECONDCATEGORYID"] != null)
                {
                    model.SECONDCATEGORYID = row["SECONDCATEGORYID"].ToString();
                }
                if (row["SECONDCATEGORYNAME"] != null)
                {
                    model.SECONDCATEGORYNAME = row["SECONDCATEGORYNAME"].ToString();
                }
                if (row["PANELFLAG"] != null)
                {
                    model.PANELFLAG = row["PANELFLAG"].ToString();
                }
                if (row["PANELSHOWFLAG"] != null && row["PANELSHOWFLAG"].ToString() != "")
                {
                    model.PANELSHOWFLAG = Int64.Parse(row["PANELSHOWFLAG"].ToString());
                }
                if (row["MAINIMG"] != null)
                {
                    model.MAINIMG = row["MAINIMG"].ToString();
                }
                if (row["STATUS"] != null && row["STATUS"].ToString() != "")
                {
                    model.STATUS = Int64.Parse(row["STATUS"].ToString());
                }
                if (row["SPECIAL_STATUS"] != null && row["SPECIAL_STATUS"].ToString() != "")
                {
                    model.SPECIAL_STATUS = Int64.Parse(row["SPECIAL_STATUS"].ToString());
                }
                if (row["IS_QUERY_BARCODE"] != null && row["IS_QUERY_BARCODE"].ToString() != "")
                {
                    model.IS_QUERY_BARCODE = Int64.Parse(row["IS_QUERY_BARCODE"].ToString());
                }
                if (row["CREATEDAT"] != null && row["CREATEDAT"].ToString() != "")
                {
                    model.CREATEDAT = Int64.Parse(row["CREATEDAT"].ToString());
                }
                if (row["SALECOUNT"] != null && row["SALECOUNT"].ToString() != "")
                {
                    model.SALECOUNT = Int64.Parse(row["SALECOUNT"].ToString());
                }
                if (row["INNERBARCODE"] != null)
                {
                    model.INNERBARCODE = row["INNERBARCODE"].ToString();
                }
                if (row["FIRST_LETTER"] != null)
                {
                    model.FIRST_LETTER = row["FIRST_LETTER"].ToString();
                }
                if (row["ALL_FIRST_LETTER"] != null)
                {
                    model.ALL_FIRST_LETTER = row["ALL_FIRST_LETTER"].ToString();
                }
                if (row["SHELFLIFE"] != null && row["SHELFLIFE"].ToString() != "")
                {
                    model.SHELFLIFE = Int64.Parse(row["SHELFLIFE"].ToString());
                }
                if (row["SKUTYPE"] != null && row["SKUTYPE"].ToString() != "")
                {
                    model.SKUTYPE = Int64.Parse(row["SKUTYPE"].ToString());
                }
                if (row["SCALEFLAG"] != null && row["SCALEFLAG"].ToString() != "")
                {
                    model.SCALEFLAG = Int64.Parse(row["SCALEFLAG"].ToString());
                }
                if (row["LOCALSTATUS"] != null && row["LOCALSTATUS"].ToString() != "")
                {
                    model.LOCALSTATUS = Int64.Parse(row["LOCALSTATUS"].ToString());
                }

                if (row["MEMBERPRICE"] != null && row["MEMBERPRICE"].ToString() != "")
                {
                    model.MEMBERPRICE = decimal.Parse(row["MEMBERPRICE"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select _id,SKUCODE,GOODS_ID,CATEGORYID,CATEGORYNAME,ORIGINPRICE,SALEPRICE,SPECIAL_PRICE,TOTALSTOCKQTY,SALESUNIT,SHOPID,TENANTID,TITLE,SKUNAME,BARCODE,SPECDESC,GOODSTAGID,WEIGHTFLAG,NUM,SPECNUM,SPECTYPE,PRICETAG,PRICETAGID,CREATE_URL_IP,TAGFORMAT,BARCODEFORMAT,BESTDAYS,SPINFO,QRCODECONTENT,INGREDIENT,LOCATION,SPEC,STORE_TYPE,COMPANY,REMARK,SPECIALMESSAGE,FIRSTCATEGORYID,FIRSTCATEGORYNAME,SECONDCATEGORYID,SECONDCATEGORYNAME,PANELFLAG,PANELSHOWFLAG,MAINIMG,STATUS,SPECIAL_STATUS,IS_QUERY_BARCODE,CREATEDAT,SALECOUNT,INNERBARCODE,FIRST_LETTER,ALL_FIRST_LETTER,SHELFLIFE,SKUTYPE,SCALEFLAG,MEMBERPRICE,LOCALSTATUS ");
            strSql.Append(" FROM DBPRODUCT_BEAN ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQLite.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM DBPRODUCT_BEAN ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T._id desc");
            }
            strSql.Append(")AS Row, T.*  from DBPRODUCT_BEAN T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQLite.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@tblName", DbType.VarChar, 255),
                    new SQLiteParameter("@fldName", DbType.VarChar, 255),
                    new SQLiteParameter("@PageSize", DbType.Int64),
                    new SQLiteParameter("@PageIndex", DbType.Int64),
                    new SQLiteParameter("@IsReCount", DbType.bit),
                    new SQLiteParameter("@OrderType", DbType.bit),
                    new SQLiteParameter("@strWhere", DbType.VarChar,1000),
                    };
            parameters[0].Value = "DBPRODUCT_BEAN";
            parameters[1].Value = "_id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQLite.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod

		#region  ExtensionMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsBySkuCode(string  skucode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from DBPRODUCT_BEAN");
            strSql.Append(" where SKUCODE='"+skucode+"'");

            object obj =DbHelperSQLite.GetSingle(strSql.ToString());
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult =int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateBySkuCode(Maticsoft.Model.DBPRODUCT_BEANMODEL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DBPRODUCT_BEAN set ");
            strSql.Append("SKUCODE=@SKUCODE,");
            strSql.Append("GOODS_ID=@GOODS_ID,");
            strSql.Append("CATEGORYID=@CATEGORYID,");
            strSql.Append("CATEGORYNAME=@CATEGORYNAME,");
            strSql.Append("ORIGINPRICE=@ORIGINPRICE,");
            strSql.Append("SALEPRICE=@SALEPRICE,");
            strSql.Append("SPECIAL_PRICE=@SPECIAL_PRICE,");
            strSql.Append("TOTALSTOCKQTY=@TOTALSTOCKQTY,");
            strSql.Append("SALESUNIT=@SALESUNIT,");
            strSql.Append("SHOPID=@SHOPID,");
            strSql.Append("TENANTID=@TENANTID,");
            strSql.Append("TITLE=@TITLE,");
            strSql.Append("SKUNAME=@SKUNAME,");
            strSql.Append("BARCODE=@BARCODE,");
            strSql.Append("SPECDESC=@SPECDESC,");
            strSql.Append("GOODSTAGID=@GOODSTAGID,");
            strSql.Append("WEIGHTFLAG=@WEIGHTFLAG,");
            strSql.Append("NUM=@NUM,");
            strSql.Append("SPECNUM=@SPECNUM,");
            strSql.Append("SPECTYPE=@SPECTYPE,");
            strSql.Append("PRICETAG=@PRICETAG,");
            strSql.Append("PRICETAGID=@PRICETAGID,");
            strSql.Append("CREATE_URL_IP=@CREATE_URL_IP,");
            strSql.Append("TAGFORMAT=@TAGFORMAT,");
            strSql.Append("BARCODEFORMAT=@BARCODEFORMAT,");
            strSql.Append("BESTDAYS=@BESTDAYS,");
            strSql.Append("SPINFO=@SPINFO,");
            strSql.Append("QRCODECONTENT=@QRCODECONTENT,");
            strSql.Append("INGREDIENT=@INGREDIENT,");
            strSql.Append("LOCATION=@LOCATION,");
            strSql.Append("SPEC=@SPEC,");
            strSql.Append("STORE_TYPE=@STORE_TYPE,");
            strSql.Append("COMPANY=@COMPANY,");
            strSql.Append("REMARK=@REMARK,");
            strSql.Append("SPECIALMESSAGE=@SPECIALMESSAGE,");
            strSql.Append("FIRSTCATEGORYID=@FIRSTCATEGORYID,");
            strSql.Append("FIRSTCATEGORYNAME=@FIRSTCATEGORYNAME,");
            strSql.Append("SECONDCATEGORYID=@SECONDCATEGORYID,");
            strSql.Append("SECONDCATEGORYNAME=@SECONDCATEGORYNAME,");
            strSql.Append("PANELFLAG=@PANELFLAG,");
            strSql.Append("PANELSHOWFLAG=@PANELSHOWFLAG,");
            strSql.Append("MAINIMG=@MAINIMG,");
            strSql.Append("STATUS=@STATUS,");
            strSql.Append("SPECIAL_STATUS=@SPECIAL_STATUS,");
            strSql.Append("IS_QUERY_BARCODE=@IS_QUERY_BARCODE,");
            strSql.Append("CREATEDAT=@CREATEDAT,");
            strSql.Append("SALECOUNT=@SALECOUNT,");
            strSql.Append("INNERBARCODE=@INNERBARCODE,");
            strSql.Append("FIRST_LETTER=@FIRST_LETTER,");
            strSql.Append("ALL_FIRST_LETTER=@ALL_FIRST_LETTER,");
            strSql.Append("SHELFLIFE=@SHELFLIFE,");
            strSql.Append("SKUTYPE=@SKUTYPE,");
            strSql.Append("SCALEFLAG=@SCALEFLAG,");
            strSql.Append("LOCALSTATUS=@LOCALSTATUS,");
            strSql.Append("MEMBERPRICE=@MEMBERPRICE");
            strSql.Append(" where SKUCODE=@SKUCODE");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@SKUCODE", DbType.String),
					new SQLiteParameter("@GOODS_ID", DbType.String),
					new SQLiteParameter("@CATEGORYID", DbType.String),
					new SQLiteParameter("@CATEGORYNAME", DbType.String),
					new SQLiteParameter("@ORIGINPRICE", DbType.Decimal,4),
					new SQLiteParameter("@SALEPRICE", DbType.Decimal,4),
					new SQLiteParameter("@SPECIAL_PRICE", DbType.Decimal,4),
					new SQLiteParameter("@TOTALSTOCKQTY", DbType.Decimal,4),
					new SQLiteParameter("@SALESUNIT", DbType.String),
					new SQLiteParameter("@SHOPID", DbType.String),
					new SQLiteParameter("@TENANTID", DbType.String),
					new SQLiteParameter("@TITLE", DbType.String),
					new SQLiteParameter("@SKUNAME", DbType.String),
					new SQLiteParameter("@BARCODE", DbType.String),
					new SQLiteParameter("@SPECDESC", DbType.String),
					new SQLiteParameter("@GOODSTAGID", DbType.Int64,8),
					new SQLiteParameter("@WEIGHTFLAG", DbType.Int64,8),
					new SQLiteParameter("@NUM", DbType.Decimal,4),
					new SQLiteParameter("@SPECNUM", DbType.Decimal,4),
					new SQLiteParameter("@SPECTYPE", DbType.Int64,8),
					new SQLiteParameter("@PRICETAG", DbType.String),
					new SQLiteParameter("@PRICETAGID", DbType.Int64,8),
					new SQLiteParameter("@CREATE_URL_IP", DbType.String),
					new SQLiteParameter("@TAGFORMAT", DbType.String),
					new SQLiteParameter("@BARCODEFORMAT", DbType.String),
					new SQLiteParameter("@BESTDAYS", DbType.Int64,8),
					new SQLiteParameter("@SPINFO", DbType.String),
					new SQLiteParameter("@QRCODECONTENT", DbType.String),
					new SQLiteParameter("@INGREDIENT", DbType.String),
					new SQLiteParameter("@LOCATION", DbType.String),
					new SQLiteParameter("@SPEC", DbType.String),
					new SQLiteParameter("@STORE_TYPE", DbType.String),
					new SQLiteParameter("@COMPANY", DbType.String),
					new SQLiteParameter("@REMARK", DbType.String),
					new SQLiteParameter("@SPECIALMESSAGE", DbType.String),
					new SQLiteParameter("@FIRSTCATEGORYID", DbType.String),
					new SQLiteParameter("@FIRSTCATEGORYNAME", DbType.String),
					new SQLiteParameter("@SECONDCATEGORYID", DbType.String),
					new SQLiteParameter("@SECONDCATEGORYNAME", DbType.String),
					new SQLiteParameter("@PANELFLAG", DbType.String),
					new SQLiteParameter("@PANELSHOWFLAG", DbType.Int64,8),
					new SQLiteParameter("@MAINIMG", DbType.String),
					new SQLiteParameter("@STATUS", DbType.Int64,8),
					new SQLiteParameter("@SPECIAL_STATUS", DbType.Int64,8),
					new SQLiteParameter("@IS_QUERY_BARCODE", DbType.Int64,8),
					new SQLiteParameter("@CREATEDAT", DbType.Int64,8),
					new SQLiteParameter("@SALECOUNT", DbType.Int64,8),
					new SQLiteParameter("@INNERBARCODE", DbType.String),
					new SQLiteParameter("@FIRST_LETTER", DbType.String),
					new SQLiteParameter("@ALL_FIRST_LETTER", DbType.String),
					new SQLiteParameter("@SHELFLIFE", DbType.Int64,8),
					new SQLiteParameter("@SKUTYPE", DbType.Int64,8),
                    new SQLiteParameter("@SCALEFLAG", DbType.Int64,8),
                    new SQLiteParameter("@LOCALSTATUS", DbType.Int64,8),
                    new SQLiteParameter("@MEMBERPRICE", DbType.Decimal,4),
					new SQLiteParameter("@_id", DbType.Int64,8)};
            parameters[0].Value = model.SKUCODE;
            parameters[1].Value = model.GOODS_ID;
            parameters[2].Value = model.CATEGORYID;
            parameters[3].Value = model.CATEGORYNAME;
            parameters[4].Value = model.ORIGINPRICE;
            parameters[5].Value = model.SALEPRICE;
            parameters[6].Value = model.SPECIAL_PRICE;
            parameters[7].Value = model.TOTALSTOCKQTY;
            parameters[8].Value = model.SALESUNIT;
            parameters[9].Value = model.SHOPID;
            parameters[10].Value = model.TENANTID;
            parameters[11].Value = model.TITLE;
            parameters[12].Value = model.SKUNAME;
            parameters[13].Value = model.BARCODE;
            parameters[14].Value = model.SPECDESC;
            parameters[15].Value = model.GOODSTAGID;
            parameters[16].Value = model.WEIGHTFLAG;
            parameters[17].Value = model.NUM;
            parameters[18].Value = model.SPECNUM;
            parameters[19].Value = model.SPECTYPE;
            parameters[20].Value = model.PRICETAG;
            parameters[21].Value = model.PRICETAGID;
            parameters[22].Value = model.CREATE_URL_IP;
            parameters[23].Value = model.TAGFORMAT;
            parameters[24].Value = model.BARCODEFORMAT;
            parameters[25].Value = model.BESTDAYS;
            parameters[26].Value = model.SPINFO;
            parameters[27].Value = model.QRCODECONTENT;
            parameters[28].Value = model.INGREDIENT;
            parameters[29].Value = model.LOCATION;
            parameters[30].Value = model.SPEC;
            parameters[31].Value = model.STORE_TYPE;
            parameters[32].Value = model.COMPANY;
            parameters[33].Value = model.REMARK;
            parameters[34].Value = model.SPECIALMESSAGE;
            parameters[35].Value = model.FIRSTCATEGORYID;
            parameters[36].Value = model.FIRSTCATEGORYNAME;
            parameters[37].Value = model.SECONDCATEGORYID;
            parameters[38].Value = model.SECONDCATEGORYNAME;
            parameters[39].Value = model.PANELFLAG;
            parameters[40].Value = model.PANELSHOWFLAG;
            parameters[41].Value = model.MAINIMG;
            parameters[42].Value = model.STATUS;
            parameters[43].Value = model.SPECIAL_STATUS;
            parameters[44].Value = model.IS_QUERY_BARCODE;
            parameters[45].Value = model.CREATEDAT;
            parameters[46].Value = model.SALECOUNT;
            parameters[47].Value = model.INNERBARCODE;
            parameters[48].Value = ConvertToFirstPinYin(model.SKUNAME);
            parameters[49].Value = ConvertToPinYin(model.SKUNAME);
            parameters[50].Value = model.SHELFLIFE;
            parameters[51].Value = model.SKUTYPE;
            parameters[52].Value = model.SCALEFLAG;
            parameters[53].Value = model.LOCALSTATUS;
            parameters[54].Value = model.MEMBERPRICE;
            parameters[55].Value = model._id;

            int rows = DbHelperSQLite.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool AddProduct(System.Collections.Generic.List<Maticsoft.Model.DBPRODUCT_BEANMODEL> lstmodel,string URL)
        {

            System.Collections.ArrayList lststring = new System.Collections.ArrayList();
            //lststring.Add("delete  from DBPRODUCT_BEAN");
            foreach (Maticsoft.Model.DBPRODUCT_BEANMODEL model in lstmodel)
            {
                //根据skucode  删除后增加 ，有的话就更新 没有新增
                string strdel = "delete from DBPRODUCT_BEAN where SKUCODE='"+model.SKUCODE+"'";
                StringBuilder strSqladd = new StringBuilder();
                strSqladd.Append("insert into DBPRODUCT_BEAN(");
                strSqladd.Append("SKUCODE,GOODS_ID,CATEGORYID,CATEGORYNAME,ORIGINPRICE,SALEPRICE,SPECIAL_PRICE,TOTALSTOCKQTY,SALESUNIT,SHOPID,TENANTID,TITLE,SKUNAME,BARCODE,SPECDESC,GOODSTAGID,WEIGHTFLAG,NUM,SPECNUM,SPECTYPE,PRICETAG,PRICETAGID,CREATE_URL_IP,TAGFORMAT,BARCODEFORMAT,BESTDAYS,SPINFO,QRCODECONTENT,INGREDIENT,LOCATION,SPEC,STORE_TYPE,COMPANY,REMARK,SPECIALMESSAGE,FIRSTCATEGORYID,FIRSTCATEGORYNAME,SECONDCATEGORYID,SECONDCATEGORYNAME,PANELFLAG,PANELSHOWFLAG,MAINIMG,STATUS,SPECIAL_STATUS,IS_QUERY_BARCODE,CREATEDAT,SALECOUNT,INNERBARCODE,FIRST_LETTER,ALL_FIRST_LETTER,SHELFLIFE,SKUTYPE,SCALEFLAG,MEMBERPRICE,LOCALSTATUS)");
                strSqladd.Append(" values (");
                if (model.SKUCODE.Length >= 8)
                {
                    strSqladd.Append("'" + model.SKUCODE + "','" + model.SKUCODE.Substring(2, 5) + "','" + model.CATEGORYID + "','" + model.CATEGORYNAME + "'," + model.ORIGINPRICE + "," + model.SALEPRICE + "," + model.SPECIAL_PRICE + "," + model.TOTALSTOCKQTY + ",'" + model.SALESUNIT + "','" + model.SHOPID + "','" + model.TENANTID + "','" + model.TITLE + "','" + model.SKUNAME + "','" + model.BARCODE + "','" + model.SPECDESC + "'," + model.GOODSTAGID + "," + model.WEIGHTFLAG + "," + model.NUM + "," + model.SPECNUM + "," + model.SPECTYPE + ",'" + model.PRICETAG + "'," + model.PRICETAGID + ",'" + URL + "','" + model.TAGFORMAT + "','" + model.BARCODEFORMAT + "'," + model.BESTDAYS + ",'" + model.SPINFO + "','" + model.QRCODECONTENT + "','" + model.INGREDIENT + "','" + model.LOCATION + "','" + model.SPEC + "','" + model.STORE_TYPE + "','" + model.COMPANY + "','" + model.REMARK + "','" + model.SPECIALMESSAGE + "','" + model.FIRSTCATEGORYID + "','" + model.FIRSTCATEGORYNAME + "','" + model.SECONDCATEGORYID + "','" + model.SECONDCATEGORYNAME + "','" + model.PANELFLAG + "'," + model.PANELSHOWFLAG + ",'" + model.MAINIMG + "'," + model.STATUS + "," + model.SPECIAL_STATUS + "," + model.IS_QUERY_BARCODE + "," + model.CREATEDAT + "," + model.SALECOUNT + ",'" + model.INNERBARCODE + "','" + ConvertToFirstPinYin(model.SKUNAME) + "','" + ConvertToPinYin(model.SKUNAME) + "'," + model.SHELFLIFE + "," + model.SKUTYPE + "," + model.SCALEFLAG + "," + model.MEMBERPRICE + "," + model.LOCALSTATUS + ")");
                }
                else
                {
                    strSqladd.Append("'" + model.SKUCODE + "','" + model.GOODS_ID + "','" + model.CATEGORYID + "','" + model.CATEGORYNAME + "'," + model.ORIGINPRICE + "," + model.SALEPRICE + "," + model.SPECIAL_PRICE + "," + model.TOTALSTOCKQTY + ",'" + model.SALESUNIT + "','" + model.SHOPID + "','" + model.TENANTID + "','" + model.TITLE + "','" + model.SKUNAME + "','" + model.BARCODE + "','" + model.SPECDESC + "'," + model.GOODSTAGID + "," + model.WEIGHTFLAG + "," + model.NUM + "," + model.SPECNUM + "," + model.SPECTYPE + ",'" + model.PRICETAG + "'," + model.PRICETAGID + ",'" + URL + "','" + model.TAGFORMAT + "','" + model.BARCODEFORMAT + "'," + model.BESTDAYS + ",'" + model.SPINFO + "','" + model.QRCODECONTENT + "','" + model.INGREDIENT + "','" + model.LOCATION + "','" + model.SPEC + "','" + model.STORE_TYPE + "','" + model.COMPANY + "','" + model.REMARK + "','" + model.SPECIALMESSAGE + "','" + model.FIRSTCATEGORYID + "','" + model.FIRSTCATEGORYNAME + "','" + model.SECONDCATEGORYID + "','" + model.SECONDCATEGORYNAME + "','" + model.PANELFLAG + "'," + model.PANELSHOWFLAG + ",'" + model.MAINIMG + "'," + model.STATUS + "," + model.SPECIAL_STATUS + "," + model.IS_QUERY_BARCODE + "," + model.CREATEDAT + "," + model.SALECOUNT + ",'" + model.INNERBARCODE + "','" + ConvertToFirstPinYin(model.SKUNAME) + "','" + ConvertToPinYin(model.SKUNAME) + "'," + model.SHELFLIFE + "," + model.SKUTYPE + "," + model.SCALEFLAG + "," + model.MEMBERPRICE + "," + model.LOCALSTATUS + ")");

                }


                
                lststring.Add(strdel.ToString());
                
                lststring.Add(strSqladd.ToString());

            }
            
            DbHelperSQLite.ExecuteSqlTran(lststring);

            return true;

        }


        public SortedDictionary<string, string> GetDiatinctCategory(string strwhere)
        {
            SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
            string cmdsql = "select distinct firstcategoryid,firstcategoryname from dbproduct_bean";
            if (!string.IsNullOrEmpty(strwhere))
            {
                cmdsql += " where " + strwhere;
            }
            DataSet ds= DbHelperSQLite.Query(cmdsql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    if (!sort.ContainsKey(dr["firstcategoryid"].ToString()))
                    {
                        sort.Add(dr["firstcategoryid"].ToString(), dr["firstcategoryname"].ToString());
                    }                    
                }
            }
            return sort;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.DBPRODUCT_BEANMODEL GetModelBySkuCode(string skucode,string createurl)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from DBPRODUCT_BEAN ");
            strSql.Append(" where SKUCODE=@SKUCODE and CREATE_URL_IP=@CREATE_URL_IP");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@SKUCODE", DbType.String),
                    new SQLiteParameter("@CREATE_URL_IP", DbType.String)
			};
            parameters[0].Value = skucode;
            parameters[1].Value = createurl;

            Maticsoft.Model.DBPRODUCT_BEANMODEL model = new Maticsoft.Model.DBPRODUCT_BEANMODEL();
            DataSet ds = DbHelperSQLite.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.DBPRODUCT_BEANMODEL GetModelByGoodsID(string goodid, string createurl)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from DBPRODUCT_BEAN ");
            strSql.Append(" where GOODS_ID=@GOODS_ID and CREATE_URL_IP=@CREATE_URL_IP");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@GOODS_ID", DbType.String),
                    new SQLiteParameter("@CREATE_URL_IP", DbType.String)
			};
            parameters[0].Value = goodid;
            parameters[1].Value = createurl;

            Maticsoft.Model.DBPRODUCT_BEANMODEL model = new Maticsoft.Model.DBPRODUCT_BEANMODEL();
            DataSet ds = DbHelperSQLite.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 执行指定的sql语句
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public bool ExecuteSql(string strSql)
        {
            int rows = DbHelperSQLite.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		#endregion  ExtensionMethod

        #region other
        /// <summary>
        /// 这个办法是用来获得一个字符串的每个字的拼音首字母构成所需的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetChineseSpell(string strText)
        {
            try
            {
                int len = strText.Length;
                string myStr = "";
                for (int i = 0; i < len; i++)
                {
                    myStr += getSpell(strText.Substring(i, 1));
                }
                return myStr.ToUpper();
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        /// <summary>
        /// 用来获得一个字的拼音首字母
        /// </summary>
        /// <param name="cnChar"></param>
        /// <returns></returns>
        private static string getSpell(string cnChar)
        {
            try
            {
                if (cnChar.Length > 1)
                {
                    cnChar = cnChar.Substring(0, 1);
                }

                //将汉字转化为ASNI码,二进制序列
                byte[] arrCN = Encoding.Default.GetBytes(cnChar);
                if (arrCN.Length > 1)
                {
                    int area = (short)arrCN[0];
                    int pos = (short)arrCN[1];
                    int code = (area << 8) + pos;
                    int[] areacode = {45217,45253,45761,46318,46826,47010,47297,47614,48119,48119,49062,
                49324,49896,50371,50614,50622,50906,51387,51446,52218,52698,52698,52698,52980,53689,54481};
                    for (int i = 0; i < 26; i++)
                    {
                        int max = 55290;
                        if (i != 25)
                        {
                            max = areacode[i + 1];
                        }
                        if (areacode[i] <= code && code < max)
                        {
                            return Encoding.Default.GetString(new byte[] { (byte)(65 + i) }).ToUpper();
                        }
                    }
                    return "*";
                }
                else
                {
                    return cnChar;
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        /// <summary>
        /// 微软官方类库， 通过ascii码会出现有些字查不到
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string ConvertToPinYin(string str)
        {
            try
            {
                string PYstr = "";
                foreach (char item in str.ToCharArray())
                {
                    if (Microsoft.International.Converters.PinYinConverter.ChineseChar.IsValidChar(item))
                    {
                        Microsoft.International.Converters.PinYinConverter.ChineseChar cc = new Microsoft.International.Converters.PinYinConverter.ChineseChar(item);
                        PYstr += cc.Pinyins[0].Substring(0, 1).ToUpper();
                    }
                    else
                    {
                        PYstr += item.ToString().ToUpper();                      
                    }
                }
                return PYstr;
            }
            catch
            {
                return "";
            }
        }


        private string ConvertToFirstPinYin(string str)
        {
            try
            {
                string PYstr = "";
                foreach (char item in str.ToCharArray())
                {
                    if (Microsoft.International.Converters.PinYinConverter.ChineseChar.IsValidChar(item))
                    {
                        Microsoft.International.Converters.PinYinConverter.ChineseChar cc = new Microsoft.International.Converters.PinYinConverter.ChineseChar(item);

                        PYstr += cc.Pinyins[0].Substring(0, 1).ToUpper();
                        break;
                    }
                    else
                    {
                        PYstr += item.ToString().ToUpper();
                        break;
                    }
                }
                return PYstr;
            }
            catch
            {
                return "";
            }
        }


        #endregion
	}
}

