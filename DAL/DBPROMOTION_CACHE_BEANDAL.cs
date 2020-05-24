using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Maticsoft.DAL
{
    public class DBPROMOTION_CACHE_BEANDAL
    {
        public DBPROMOTION_CACHE_BEANDAL()
		{}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int _id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from DBPROMOTION_CACHE_BEAN");
            strSql.Append(" where _id=@_id");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@_id", DbType.Int32,4)
			};
            parameters[0].Value = _id;

            return DbHelperSQLite.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.DBPROMOTION_CACHE_BEANMODEL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DBPROMOTION_CACHE_BEAN(");
            strSql.Append("CODE,RANK,TENANTSCOPE,DISTRICTSCOPE,SHOPSCOPE,PROMOTYPE,PROMOSUBTYPE,PROMOACTION,ELIGIBILITYCONDITION,PROMOCONDITIONTYPE,PROMOCONDITIONCONTEXT,PROMOACTIONCONTEXT,NAME,DESCRIPTION,TAG,CANBECOMBINED,ENABLED,SALECHANNEL,ENABLEDFROM,ENABLEDTO,ENABLEDTIMEINFO,CREATEDAT,UPDATEDAT,CREATEDBY,UPDATEDBY,FROMOUTER,OUTERCODE,COSTCENTERINFO,COSTRULECONTEXT,CANMIXCOUPON,ORDERSUBTYPE,AVAILABLECATEGORY,TENANTID,SHOPID,CREATE_URL_IP,ONLYUSEINORIGINAL,ONLYMEMBER,MEMBERTAGS,PURCHASELIMIT,MEMBERFLAG)");
            strSql.Append(" values (");
            strSql.Append("@CODE,@RANK,@TENANTSCOPE,@DISTRICTSCOPE,@SHOPSCOPE,@PROMOTYPE,@PROMOSUBTYPE,@PROMOACTION,@ELIGIBILITYCONDITION,@PROMOCONDITIONTYPE,@PROMOCONDITIONCONTEXT,@PROMOACTIONCONTEXT,@NAME,@DESCRIPTION,@TAG,@CANBECOMBINED,@ENABLED,@SALECHANNEL,@ENABLEDFROM,@ENABLEDTO,@ENABLEDTIMEINFO,@CREATEDAT,@UPDATEDAT,@CREATEDBY,@UPDATEDBY,@FROMOUTER,@OUTERCODE,@COSTCENTERINFO,@COSTRULECONTEXT,@CANMIXCOUPON,@ORDERSUBTYPE,@AVAILABLECATEGORY,@TENANTID,@SHOPID,@CREATE_URL_IP,@ONLYUSEINORIGINAL,@ONLYMEMBER,@MEMBERTAGS,@PURCHASELIMIT,@MEMBERFLAG)");
            strSql.Append(";select LAST_INSERT_ROWID()");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@CODE", DbType.String),
					new SQLiteParameter("@RANK", DbType.Int32,8),
					new SQLiteParameter("@TENANTSCOPE", DbType.String),
					new SQLiteParameter("@DISTRICTSCOPE", DbType.String),
					new SQLiteParameter("@SHOPSCOPE", DbType.String),
					new SQLiteParameter("@PROMOTYPE", DbType.String),
					new SQLiteParameter("@PROMOSUBTYPE", DbType.String),
					new SQLiteParameter("@PROMOACTION", DbType.String),
					new SQLiteParameter("@ELIGIBILITYCONDITION", DbType.String),
					new SQLiteParameter("@PROMOCONDITIONTYPE", DbType.String),
					new SQLiteParameter("@PROMOCONDITIONCONTEXT", DbType.String),
					new SQLiteParameter("@PROMOACTIONCONTEXT", DbType.String),
					new SQLiteParameter("@NAME", DbType.String),
					new SQLiteParameter("@DESCRIPTION", DbType.String),
					new SQLiteParameter("@TAG", DbType.String),
					new SQLiteParameter("@CANBECOMBINED", DbType.Int32,8),
					new SQLiteParameter("@ENABLED", DbType.Int32,8),
					new SQLiteParameter("@SALECHANNEL", DbType.Int32,8),
					new SQLiteParameter("@ENABLEDFROM", DbType.Int32,8),
					new SQLiteParameter("@ENABLEDTO", DbType.Int32,8),
					new SQLiteParameter("@ENABLEDTIMEINFO", DbType.String),
					new SQLiteParameter("@CREATEDAT", DbType.Int32,8),
					new SQLiteParameter("@UPDATEDAT", DbType.Int32,8),
					new SQLiteParameter("@CREATEDBY", DbType.String),
					new SQLiteParameter("@UPDATEDBY", DbType.String),
					new SQLiteParameter("@FROMOUTER", DbType.Int32,8),
					new SQLiteParameter("@OUTERCODE", DbType.String),
					new SQLiteParameter("@COSTCENTERINFO", DbType.String),
					new SQLiteParameter("@COSTRULECONTEXT", DbType.String),
					new SQLiteParameter("@CANMIXCOUPON", DbType.Int32,8),
					new SQLiteParameter("@ORDERSUBTYPE", DbType.Int32,8),
					new SQLiteParameter("@AVAILABLECATEGORY", DbType.String),
					new SQLiteParameter("@TENANTID", DbType.String),
					new SQLiteParameter("@SHOPID", DbType.String),
					new SQLiteParameter("@CREATE_URL_IP", DbType.String),
					new SQLiteParameter("@ONLYUSEINORIGINAL", DbType.Int32,8),
					new SQLiteParameter("@ONLYMEMBER", DbType.Int32,8),
					new SQLiteParameter("@MEMBERTAGS", DbType.String),
					new SQLiteParameter("@PURCHASELIMIT", DbType.Int32,8),
					new SQLiteParameter("@MEMBERFLAG", DbType.Int32,8)};
            parameters[0].Value = model.CODE;
            parameters[1].Value = model.RANK;
            parameters[2].Value = model.TENANTSCOPE;
            parameters[3].Value = model.DISTRICTSCOPE;
            parameters[4].Value = model.SHOPSCOPE;
            parameters[5].Value = model.PROMOTYPE;
            parameters[6].Value = model.PROMOSUBTYPE;
            parameters[7].Value = model.PROMOACTION;
            parameters[8].Value = model.ELIGIBILITYCONDITION;
            parameters[9].Value = model.PROMOCONDITIONTYPE;
            parameters[10].Value = model.PROMOCONDITIONCONTEXT;
            parameters[11].Value = model.PROMOACTIONCONTEXT;
            parameters[12].Value = model.NAME;
            parameters[13].Value = model.DESCRIPTION;
            parameters[14].Value = model.TAG;
            parameters[15].Value = model.CANBECOMBINED;
            parameters[16].Value = model.ENABLED;
            parameters[17].Value = model.SALECHANNEL;
            parameters[18].Value = model.ENABLEDFROM;
            parameters[19].Value = model.ENABLEDTO;
            parameters[20].Value = model.ENABLEDTIMEINFO;
            parameters[21].Value = model.CREATEDAT;
            parameters[22].Value = model.UPDATEDAT;
            parameters[23].Value = model.CREATEDBY;
            parameters[24].Value = model.UPDATEDBY;
            parameters[25].Value = model.FROMOUTER;
            parameters[26].Value = model.OUTERCODE;
            parameters[27].Value = model.COSTCENTERINFO;
            parameters[28].Value = model.COSTRULECONTEXT;
            parameters[29].Value = model.CANMIXCOUPON;
            parameters[30].Value = model.ORDERSUBTYPE;
            parameters[31].Value = model.AVAILABLECATEGORY;
            parameters[32].Value = model.TENANTID;
            parameters[33].Value = model.SHOPID;
            parameters[34].Value = model.CREATE_URL_IP;
            parameters[35].Value = model.ONLYUSEINORIGINAL;
            parameters[36].Value = model.ONLYMEMBER;
            parameters[37].Value = model.MEMBERTAGS;
            parameters[38].Value = model.PURCHASELIMIT;
            parameters[39].Value = model.MEMBERFLAG;

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
        public bool Update(Maticsoft.Model.DBPROMOTION_CACHE_BEANMODEL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DBPROMOTION_CACHE_BEAN set ");
            strSql.Append("CODE=@CODE,");
            strSql.Append("RANK=@RANK,");
            strSql.Append("TENANTSCOPE=@TENANTSCOPE,");
            strSql.Append("DISTRICTSCOPE=@DISTRICTSCOPE,");
            strSql.Append("SHOPSCOPE=@SHOPSCOPE,");
            strSql.Append("PROMOTYPE=@PROMOTYPE,");
            strSql.Append("PROMOSUBTYPE=@PROMOSUBTYPE,");
            strSql.Append("PROMOACTION=@PROMOACTION,");
            strSql.Append("ELIGIBILITYCONDITION=@ELIGIBILITYCONDITION,");
            strSql.Append("PROMOCONDITIONTYPE=@PROMOCONDITIONTYPE,");
            strSql.Append("PROMOCONDITIONCONTEXT=@PROMOCONDITIONCONTEXT,");
            strSql.Append("PROMOACTIONCONTEXT=@PROMOACTIONCONTEXT,");
            strSql.Append("NAME=@NAME,");
            strSql.Append("DESCRIPTION=@DESCRIPTION,");
            strSql.Append("TAG=@TAG,");
            strSql.Append("CANBECOMBINED=@CANBECOMBINED,");
            strSql.Append("ENABLED=@ENABLED,");
            strSql.Append("SALECHANNEL=@SALECHANNEL,");
            strSql.Append("ENABLEDFROM=@ENABLEDFROM,");
            strSql.Append("ENABLEDTO=@ENABLEDTO,");
            strSql.Append("ENABLEDTIMEINFO=@ENABLEDTIMEINFO,");
            strSql.Append("CREATEDAT=@CREATEDAT,");
            strSql.Append("UPDATEDAT=@UPDATEDAT,");
            strSql.Append("CREATEDBY=@CREATEDBY,");
            strSql.Append("UPDATEDBY=@UPDATEDBY,");
            strSql.Append("FROMOUTER=@FROMOUTER,");
            strSql.Append("OUTERCODE=@OUTERCODE,");
            strSql.Append("COSTCENTERINFO=@COSTCENTERINFO,");
            strSql.Append("COSTRULECONTEXT=@COSTRULECONTEXT,");
            strSql.Append("CANMIXCOUPON=@CANMIXCOUPON,");
            strSql.Append("ORDERSUBTYPE=@ORDERSUBTYPE,");
            strSql.Append("AVAILABLECATEGORY=@AVAILABLECATEGORY,");
            strSql.Append("TENANTID=@TENANTID,");
            strSql.Append("SHOPID=@SHOPID,");
            strSql.Append("CREATE_URL_IP=@CREATE_URL_IP,");
            strSql.Append("ONLYUSEINORIGINAL=@ONLYUSEINORIGINAL,");
            strSql.Append("ONLYMEMBER=@ONLYMEMBER,");
            strSql.Append("MEMBERTAGS=@MEMBERTAGS,");
            strSql.Append("PURCHASELIMIT=@PURCHASELIMIT,");
            strSql.Append("MEMBERFLAG=@MEMBERFLAG");
            strSql.Append(" where _id=@_id");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@CODE", DbType.String),
					new SQLiteParameter("@RANK", DbType.Int32,8),
					new SQLiteParameter("@TENANTSCOPE", DbType.String),
					new SQLiteParameter("@DISTRICTSCOPE", DbType.String),
					new SQLiteParameter("@SHOPSCOPE", DbType.String),
					new SQLiteParameter("@PROMOTYPE", DbType.String),
					new SQLiteParameter("@PROMOSUBTYPE", DbType.String),
					new SQLiteParameter("@PROMOACTION", DbType.String),
					new SQLiteParameter("@ELIGIBILITYCONDITION", DbType.String),
					new SQLiteParameter("@PROMOCONDITIONTYPE", DbType.String),
					new SQLiteParameter("@PROMOCONDITIONCONTEXT", DbType.String),
					new SQLiteParameter("@PROMOACTIONCONTEXT", DbType.String),
					new SQLiteParameter("@NAME", DbType.String),
					new SQLiteParameter("@DESCRIPTION", DbType.String),
					new SQLiteParameter("@TAG", DbType.String),
					new SQLiteParameter("@CANBECOMBINED", DbType.Int32,8),
					new SQLiteParameter("@ENABLED", DbType.Int32,8),
					new SQLiteParameter("@SALECHANNEL", DbType.Int32,8),
					new SQLiteParameter("@ENABLEDFROM", DbType.Int32,8),
					new SQLiteParameter("@ENABLEDTO", DbType.Int32,8),
					new SQLiteParameter("@ENABLEDTIMEINFO", DbType.String),
					new SQLiteParameter("@CREATEDAT", DbType.Int32,8),
					new SQLiteParameter("@UPDATEDAT", DbType.Int32,8),
					new SQLiteParameter("@CREATEDBY", DbType.String),
					new SQLiteParameter("@UPDATEDBY", DbType.String),
					new SQLiteParameter("@FROMOUTER", DbType.Int32,8),
					new SQLiteParameter("@OUTERCODE", DbType.String),
					new SQLiteParameter("@COSTCENTERINFO", DbType.String),
					new SQLiteParameter("@COSTRULECONTEXT", DbType.String),
					new SQLiteParameter("@CANMIXCOUPON", DbType.Int32,8),
					new SQLiteParameter("@ORDERSUBTYPE", DbType.Int32,8),
					new SQLiteParameter("@AVAILABLECATEGORY", DbType.String),
					new SQLiteParameter("@TENANTID", DbType.String),
					new SQLiteParameter("@SHOPID", DbType.String),
					new SQLiteParameter("@CREATE_URL_IP", DbType.String),
					new SQLiteParameter("@ONLYUSEINORIGINAL", DbType.Int32,8),
					new SQLiteParameter("@ONLYMEMBER", DbType.Int32,8),
					new SQLiteParameter("@MEMBERTAGS", DbType.String),
					new SQLiteParameter("@PURCHASELIMIT", DbType.Int32,8),
					new SQLiteParameter("@MEMBERFLAG", DbType.Int32,8),
					new SQLiteParameter("@_id", DbType.Int32,8)};
            parameters[0].Value = model.CODE;
            parameters[1].Value = model.RANK;
            parameters[2].Value = model.TENANTSCOPE;
            parameters[3].Value = model.DISTRICTSCOPE;
            parameters[4].Value = model.SHOPSCOPE;
            parameters[5].Value = model.PROMOTYPE;
            parameters[6].Value = model.PROMOSUBTYPE;
            parameters[7].Value = model.PROMOACTION;
            parameters[8].Value = model.ELIGIBILITYCONDITION;
            parameters[9].Value = model.PROMOCONDITIONTYPE;
            parameters[10].Value = model.PROMOCONDITIONCONTEXT;
            parameters[11].Value = model.PROMOACTIONCONTEXT;
            parameters[12].Value = model.NAME;
            parameters[13].Value = model.DESCRIPTION;
            parameters[14].Value = model.TAG;
            parameters[15].Value = model.CANBECOMBINED;
            parameters[16].Value = model.ENABLED;
            parameters[17].Value = model.SALECHANNEL;
            parameters[18].Value = model.ENABLEDFROM;
            parameters[19].Value = model.ENABLEDTO;
            parameters[20].Value = model.ENABLEDTIMEINFO;
            parameters[21].Value = model.CREATEDAT;
            parameters[22].Value = model.UPDATEDAT;
            parameters[23].Value = model.CREATEDBY;
            parameters[24].Value = model.UPDATEDBY;
            parameters[25].Value = model.FROMOUTER;
            parameters[26].Value = model.OUTERCODE;
            parameters[27].Value = model.COSTCENTERINFO;
            parameters[28].Value = model.COSTRULECONTEXT;
            parameters[29].Value = model.CANMIXCOUPON;
            parameters[30].Value = model.ORDERSUBTYPE;
            parameters[31].Value = model.AVAILABLECATEGORY;
            parameters[32].Value = model.TENANTID;
            parameters[33].Value = model.SHOPID;
            parameters[34].Value = model.CREATE_URL_IP;
            parameters[35].Value = model.ONLYUSEINORIGINAL;
            parameters[36].Value = model.ONLYMEMBER;
            parameters[37].Value = model.MEMBERTAGS;
            parameters[38].Value = model.PURCHASELIMIT;
            parameters[39].Value = model.MEMBERFLAG;
            parameters[40].Value = model._id;

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
            strSql.Append("delete from DBPROMOTION_CACHE_BEAN ");
            strSql.Append(" where _id=@_id");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@_id", DbType.Int32,4)
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
            strSql.Append("delete from DBPROMOTION_CACHE_BEAN ");
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
        public Maticsoft.Model.DBPROMOTION_CACHE_BEANMODEL GetModel(int _id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select _id,CODE,RANK,TENANTSCOPE,DISTRICTSCOPE,SHOPSCOPE,PROMOTYPE,PROMOSUBTYPE,PROMOACTION,ELIGIBILITYCONDITION,PROMOCONDITIONTYPE,PROMOCONDITIONCONTEXT,PROMOACTIONCONTEXT,NAME,DESCRIPTION,TAG,CANBECOMBINED,ENABLED,SALECHANNEL,ENABLEDFROM,ENABLEDTO,ENABLEDTIMEINFO,CREATEDAT,UPDATEDAT,CREATEDBY,UPDATEDBY,FROMOUTER,OUTERCODE,COSTCENTERINFO,COSTRULECONTEXT,CANMIXCOUPON,ORDERSUBTYPE,AVAILABLECATEGORY,TENANTID,SHOPID,CREATE_URL_IP,ONLYUSEINORIGINAL,ONLYMEMBER,MEMBERTAGS,PURCHASELIMIT,MEMBERFLAG from DBPROMOTION_CACHE_BEAN ");
            strSql.Append(" where _id=@_id");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@_id", DbType.Int32,4)
			};
            parameters[0].Value = _id;

            Maticsoft.Model.DBPROMOTION_CACHE_BEANMODEL model = new Maticsoft.Model.DBPROMOTION_CACHE_BEANMODEL();
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
        public Maticsoft.Model.DBPROMOTION_CACHE_BEANMODEL DataRowToModel(DataRow row)
        {
            Maticsoft.Model.DBPROMOTION_CACHE_BEANMODEL model = new Maticsoft.Model.DBPROMOTION_CACHE_BEANMODEL();
            if (row != null)
            {
                if (row["_id"] != null && row["_id"].ToString() != "")
                {
                    model._id = Int64.Parse(row["_id"].ToString());
                }
                if (row["CODE"] != null)
                {
                    model.CODE = row["CODE"].ToString();
                }
                if (row["RANK"] != null && row["RANK"].ToString() != "")
                {
                    model.RANK = Int64.Parse(row["RANK"].ToString());
                }
                if (row["TENANTSCOPE"] != null)
                {
                    model.TENANTSCOPE = row["TENANTSCOPE"].ToString();
                }
                if (row["DISTRICTSCOPE"] != null)
                {
                    model.DISTRICTSCOPE = row["DISTRICTSCOPE"].ToString();
                }
                if (row["SHOPSCOPE"] != null)
                {
                    model.SHOPSCOPE = row["SHOPSCOPE"].ToString();
                }
                if (row["PROMOTYPE"] != null)
                {
                    model.PROMOTYPE = row["PROMOTYPE"].ToString();
                }
                if (row["PROMOSUBTYPE"] != null)
                {
                    model.PROMOSUBTYPE = row["PROMOSUBTYPE"].ToString();
                }
                if (row["PROMOACTION"] != null)
                {
                    model.PROMOACTION = row["PROMOACTION"].ToString();
                }
                if (row["ELIGIBILITYCONDITION"] != null)
                {
                    model.ELIGIBILITYCONDITION = row["ELIGIBILITYCONDITION"].ToString();
                }
                if (row["PROMOCONDITIONTYPE"] != null)
                {
                    model.PROMOCONDITIONTYPE = row["PROMOCONDITIONTYPE"].ToString();
                }
                if (row["PROMOCONDITIONCONTEXT"] != null)
                {
                    model.PROMOCONDITIONCONTEXT = row["PROMOCONDITIONCONTEXT"].ToString();
                }
                if (row["PROMOACTIONCONTEXT"] != null)
                {
                    model.PROMOACTIONCONTEXT = row["PROMOACTIONCONTEXT"].ToString();
                }
                if (row["NAME"] != null)
                {
                    model.NAME = row["NAME"].ToString();
                }
                if (row["DESCRIPTION"] != null)
                {
                    model.DESCRIPTION = row["DESCRIPTION"].ToString();
                }
                if (row["TAG"] != null)
                {
                    model.TAG = row["TAG"].ToString();
                }
                if (row["CANBECOMBINED"] != null && row["CANBECOMBINED"].ToString() != "")
                {
                    model.CANBECOMBINED = Int64.Parse(row["CANBECOMBINED"].ToString());
                }
                if (row["ENABLED"] != null && row["ENABLED"].ToString() != "")
                {
                    model.ENABLED = Int64.Parse(row["ENABLED"].ToString());
                }
                if (row["SALECHANNEL"] != null && row["SALECHANNEL"].ToString() != "")
                {
                    model.SALECHANNEL = Int64.Parse(row["SALECHANNEL"].ToString());
                }
                if (row["ENABLEDFROM"] != null && row["ENABLEDFROM"].ToString() != "")
                {
                    model.ENABLEDFROM = Int64.Parse(row["ENABLEDFROM"].ToString());
                }
                if (row["ENABLEDTO"] != null && row["ENABLEDTO"].ToString() != "")
                {
                    model.ENABLEDTO = Int64.Parse(row["ENABLEDTO"].ToString());
                }
                if (row["ENABLEDTIMEINFO"] != null)
                {
                    model.ENABLEDTIMEINFO = row["ENABLEDTIMEINFO"].ToString();
                }
                if (row["CREATEDAT"] != null && row["CREATEDAT"].ToString() != "")
                {
                    model.CREATEDAT = Int64.Parse(row["CREATEDAT"].ToString());
                }
                if (row["UPDATEDAT"] != null && row["UPDATEDAT"].ToString() != "")
                {
                    model.UPDATEDAT = Int64.Parse(row["UPDATEDAT"].ToString());
                }
                if (row["CREATEDBY"] != null)
                {
                    model.CREATEDBY = row["CREATEDBY"].ToString();
                }
                if (row["UPDATEDBY"] != null)
                {
                    model.UPDATEDBY = row["UPDATEDBY"].ToString();
                }
                if (row["FROMOUTER"] != null && row["FROMOUTER"].ToString() != "")
                {
                    model.FROMOUTER = Int64.Parse(row["FROMOUTER"].ToString());
                }
                if (row["OUTERCODE"] != null)
                {
                    model.OUTERCODE = row["OUTERCODE"].ToString();
                }
                if (row["COSTCENTERINFO"] != null)
                {
                    model.COSTCENTERINFO = row["COSTCENTERINFO"].ToString();
                }
                if (row["COSTRULECONTEXT"] != null)
                {
                    model.COSTRULECONTEXT = row["COSTRULECONTEXT"].ToString();
                }
                if (row["CANMIXCOUPON"] != null && row["CANMIXCOUPON"].ToString() != "")
                {
                    model.CANMIXCOUPON = Int64.Parse(row["CANMIXCOUPON"].ToString());
                }
                if (row["ORDERSUBTYPE"] != null && row["ORDERSUBTYPE"].ToString() != "")
                {
                    model.ORDERSUBTYPE = Int64.Parse(row["ORDERSUBTYPE"].ToString());
                }
                if (row["AVAILABLECATEGORY"] != null)
                {
                    model.AVAILABLECATEGORY = row["AVAILABLECATEGORY"].ToString();
                }
                if (row["TENANTID"] != null)
                {
                    model.TENANTID = row["TENANTID"].ToString();
                }
                if (row["SHOPID"] != null)
                {
                    model.SHOPID = row["SHOPID"].ToString();
                }
                if (row["CREATE_URL_IP"] != null)
                {
                    model.CREATE_URL_IP = row["CREATE_URL_IP"].ToString();
                }
                if (row["ONLYUSEINORIGINAL"] != null && row["ONLYUSEINORIGINAL"].ToString() != "")
                {
                    model.ONLYUSEINORIGINAL = Int64.Parse(row["ONLYUSEINORIGINAL"].ToString());
                }
                if (row["ONLYMEMBER"] != null && row["ONLYMEMBER"].ToString() != "")
                {
                    model.ONLYMEMBER = Int64.Parse(row["ONLYMEMBER"].ToString());
                }
                if (row["MEMBERTAGS"] != null)
                {
                    model.MEMBERTAGS = row["MEMBERTAGS"].ToString();
                }
                if (row["PURCHASELIMIT"] != null && row["PURCHASELIMIT"].ToString() != "")
                {
                    model.PURCHASELIMIT = Int64.Parse(row["PURCHASELIMIT"].ToString());
                }
                if (row["MEMBERFLAG"] != null && row["MEMBERFLAG"].ToString() != "")
                {
                    model.MEMBERFLAG = Int64.Parse(row["MEMBERFLAG"].ToString());
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
            strSql.Append("select _id,CODE,RANK,TENANTSCOPE,DISTRICTSCOPE,SHOPSCOPE,PROMOTYPE,PROMOSUBTYPE,PROMOACTION,ELIGIBILITYCONDITION,PROMOCONDITIONTYPE,PROMOCONDITIONCONTEXT,PROMOACTIONCONTEXT,NAME,DESCRIPTION,TAG,CANBECOMBINED,ENABLED,SALECHANNEL,ENABLEDFROM,ENABLEDTO,ENABLEDTIMEINFO,CREATEDAT,UPDATEDAT,CREATEDBY,UPDATEDBY,FROMOUTER,OUTERCODE,COSTCENTERINFO,COSTRULECONTEXT,CANMIXCOUPON,ORDERSUBTYPE,AVAILABLECATEGORY,TENANTID,SHOPID,CREATE_URL_IP,ONLYUSEINORIGINAL,ONLYMEMBER,MEMBERTAGS,PURCHASELIMIT,MEMBERFLAG ");
            strSql.Append(" FROM DBPROMOTION_CACHE_BEAN ");
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
            strSql.Append("select count(1) FROM DBPROMOTION_CACHE_BEAN ");
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
            strSql.Append(")AS Row, T.*  from DBPROMOTION_CACHE_BEAN T ");
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
                    new SQLiteParameter("@PageSize", DbType.Int32),
                    new SQLiteParameter("@PageIndex", DbType.Int32),
                    new SQLiteParameter("@IsReCount", DbType.bit),
                    new SQLiteParameter("@OrderType", DbType.bit),
                    new SQLiteParameter("@strWhere", DbType.VarChar,1000),
                    };
            parameters[0].Value = "DBPROMOTION_CACHE_BEAN";
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
        public bool ExistsByCode(string code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from DBPROMOTION_CACHE_BEAN");
            strSql.Append(" where CODE=@CODE");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@CODE", DbType.String)
			};
            parameters[0].Value =code;

            return DbHelperSQLite.Exists(strSql.ToString(), parameters);
        }

        public bool AddPromotion(System.Collections.Generic.List<Maticsoft.Model.DBPROMOTION_CACHE_BEANMODEL> lstmodel,string tenantid,string shopid, string URL)
        {

            System.Collections.ArrayList lststring = new System.Collections.ArrayList();
            lststring.Add("delete  from DBPROMOTION_CACHE_BEAN");
            foreach (Maticsoft.Model.DBPROMOTION_CACHE_BEANMODEL m in lstmodel)
            {
                //根据skucode  删除后增加 ，有的话就更新 没有新增
                //string strdel = "delete from DBPROMOTION_CACHE_BEAN where SKUCODE='" + model.SKUCODE + "'";
                StringBuilder strSqladd = new StringBuilder();

                strSqladd.Append("insert into DBPROMOTION_CACHE_BEAN(");
                strSqladd.Append(" CODE, RANK, TENANTSCOPE, DISTRICTSCOPE, SHOPSCOPE,PROMOTYPE,PROMOSUBTYPE, PROMOACTION, ELIGIBILITYCONDITION, PROMOCONDITIONTYPE,PROMOCONDITIONCONTEXT, PROMOACTIONCONTEXT, NAME, DESCRIPTION, TAG,CANBECOMBINED, ENABLED, SALECHANNEL, ENABLEDFROM, ENABLEDTO, ENABLEDTIMEINFO, CREATEDAT,UPDATEDAT, CREATEDBY, UPDATEDBY, FROMOUTER, OUTERCODE, COSTCENTERINFO, COSTRULECONTEXT,CANMIXCOUPON, ORDERSUBTYPE, AVAILABLECATEGORY, TENANTID, SHOPID, CREATE_URL_IP,ONLYUSEINORIGINAL,ONLYMEMBER,MEMBERTAGS,PURCHASELIMIT,MEMBERFLAG )");
                strSqladd.Append(" values (");

                strSqladd.Append(" '" + m.CODE + "', '" + m.RANK + "', '" + m.TENANTSCOPE + "', '" + m.DISTRICTSCOPE + "', '" + m.SHOPSCOPE + "', '" + m.PROMOTYPE + "', '" + m.PROMOSUBTYPE + "','" + m.PROMOACTION + "', '" + m.ELIGIBILITYCONDITION + "', '" + m.PROMOCONDITIONTYPE + "', '" + m.PROMOCONDITIONCONTEXT + "','" + m.PROMOACTIONCONTEXT + "', '" + m.NAME + "', '" + m.DESCRIPTION + "', '" + m.TAG + "', '" + m.CANBECOMBINED + "', '" + m.ENABLED + "','" + m.SALECHANNEL + "', '" + m.ENABLEDFROM + "', '" + m.ENABLEDTO + "', '" + m.ENABLEDTIMEINFO + "', '" + m.CREATEDAT + "', '" + m.UPDATEDAT + "','CREATEDBY','" + m.UPDATEDBY + "', '" + m.FROMOUTER + "', '" + m.OUTERCODE + "', '" + m.COSTCENTERINFO + "', '" + m.COSTRULECONTEXT + "','" + m.CANMIXCOUPON + "', '" + m.ORDERSUBTYPE + "', '" + m.AVAILABLECATEGORY + "', '" + tenantid + "', '" + shopid + "', '" + URL + "', '" + m.ONLYUSEINORIGINAL + "', " + m.ONLYMEMBER + ", '" + m.MEMBERTAGS + "', " + m.PURCHASELIMIT + ", " + m.MEMBERFLAG + ")");
                                     


                //lststring.Add(strdel.ToString());

                lststring.Add(strSqladd.ToString());

            }

            DbHelperSQLite.ExecuteSqlTran(lststring);

            return true;

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateByCode(Maticsoft.Model.DBPROMOTION_CACHE_BEANMODEL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DBPROMOTION_CACHE_BEAN set ");
           
            strSql.Append("RANK=@RANK,");
            strSql.Append("TENANTSCOPE=@TENANTSCOPE,");
            strSql.Append("DISTRICTSCOPE=@DISTRICTSCOPE,");
            strSql.Append("SHOPSCOPE=@SHOPSCOPE,");
            strSql.Append("PROMOTYPE=@PROMOTYPE,");
            strSql.Append("PROMOSUBTYPE=@PROMOSUBTYPE,");
            strSql.Append("PROMOACTION=@PROMOACTION,");
            strSql.Append("ELIGIBILITYCONDITION=@ELIGIBILITYCONDITION,");
            strSql.Append("PROMOCONDITIONTYPE=@PROMOCONDITIONTYPE,");
            strSql.Append("PROMOCONDITIONCONTEXT=@PROMOCONDITIONCONTEXT,");
            strSql.Append("PROMOACTIONCONTEXT=@PROMOACTIONCONTEXT,");
            strSql.Append("NAME=@NAME,");
            strSql.Append("DESCRIPTION=@DESCRIPTION,");
            strSql.Append("TAG=@TAG,");
            strSql.Append("CANBECOMBINED=@CANBECOMBINED,");
            strSql.Append("ENABLED=@ENABLED,");
            strSql.Append("SALECHANNEL=@SALECHANNEL,");
            strSql.Append("ENABLEDFROM=@ENABLEDFROM,");
            strSql.Append("ENABLEDTO=@ENABLEDTO,");
            strSql.Append("ENABLEDTIMEINFO=@ENABLEDTIMEINFO,");
            strSql.Append("CREATEDAT=@CREATEDAT,");
            strSql.Append("UPDATEDAT=@UPDATEDAT,");
            strSql.Append("CREATEDBY=@CREATEDBY,");
            strSql.Append("UPDATEDBY=@UPDATEDBY,");
            strSql.Append("FROMOUTER=@FROMOUTER,");
            strSql.Append("OUTERCODE=@OUTERCODE,");
            strSql.Append("COSTCENTERINFO=@COSTCENTERINFO,");
            strSql.Append("COSTRULECONTEXT=@COSTRULECONTEXT,");
            strSql.Append("CANMIXCOUPON=@CANMIXCOUPON,");
            strSql.Append("ORDERSUBTYPE=@ORDERSUBTYPE,");
            strSql.Append("AVAILABLECATEGORY=@AVAILABLECATEGORY,");
            strSql.Append("TENANTID=@TENANTID,");
            strSql.Append("SHOPID=@SHOPID,");
            strSql.Append("CREATE_URL_IP=@CREATE_URL_IP,");
            strSql.Append("ONLYUSEINORIGINAL=@ONLYUSEINORIGINAL,");
            strSql.Append("ONLYMEMBER=@ONLYMEMBER,");
            strSql.Append("MEMBERTAGS=@MEMBERTAGS,");
            strSql.Append("PURCHASELIMIT=@PURCHASELIMIT,");
            strSql.Append("MEMBERFLAG=@MEMBERFLAG");
            strSql.Append(" where CODE=@CODE");
            SQLiteParameter[] parameters = {
					
					new SQLiteParameter("@RANK", DbType.Int64,8),
					new SQLiteParameter("@TENANTSCOPE", DbType.String),
					new SQLiteParameter("@DISTRICTSCOPE", DbType.String),
					new SQLiteParameter("@SHOPSCOPE", DbType.String),
					new SQLiteParameter("@PROMOTYPE", DbType.String),
					new SQLiteParameter("@PROMOSUBTYPE", DbType.String),
					new SQLiteParameter("@PROMOACTION", DbType.String),
					new SQLiteParameter("@ELIGIBILITYCONDITION", DbType.String),
					new SQLiteParameter("@PROMOCONDITIONTYPE", DbType.String),
					new SQLiteParameter("@PROMOCONDITIONCONTEXT", DbType.String),
					new SQLiteParameter("@PROMOACTIONCONTEXT", DbType.String),
					new SQLiteParameter("@NAME", DbType.String),
					new SQLiteParameter("@DESCRIPTION", DbType.String),
					new SQLiteParameter("@TAG", DbType.String),
					new SQLiteParameter("@CANBECOMBINED", DbType.Int64,8),
					new SQLiteParameter("@ENABLED", DbType.Int64,8),
					new SQLiteParameter("@SALECHANNEL", DbType.Int64,8),
					new SQLiteParameter("@ENABLEDFROM", DbType.Int64,8),
					new SQLiteParameter("@ENABLEDTO", DbType.Int64,8),
					new SQLiteParameter("@ENABLEDTIMEINFO", DbType.String),
					new SQLiteParameter("@CREATEDAT", DbType.Int64,8),
					new SQLiteParameter("@UPDATEDAT", DbType.Int64,8),
					new SQLiteParameter("@CREATEDBY", DbType.String),
					new SQLiteParameter("@UPDATEDBY", DbType.String),
					new SQLiteParameter("@FROMOUTER", DbType.Int64,8),
					new SQLiteParameter("@OUTERCODE", DbType.String),
					new SQLiteParameter("@COSTCENTERINFO", DbType.String),
					new SQLiteParameter("@COSTRULECONTEXT", DbType.String),
					new SQLiteParameter("@CANMIXCOUPON", DbType.Int64,8),
					new SQLiteParameter("@ORDERSUBTYPE", DbType.Int64,8),
					new SQLiteParameter("@AVAILABLECATEGORY", DbType.String),
					new SQLiteParameter("@TENANTID", DbType.String),
					new SQLiteParameter("@SHOPID", DbType.String),
					
                    new SQLiteParameter("@CREATE_URL_IP", DbType.String),
                    new SQLiteParameter("@ONLYUSEINORIGINAL", DbType.Int64,8),
                    new SQLiteParameter("@ONLYMEMBER", DbType.Int64,8),
					new SQLiteParameter("@MEMBERTAGS", DbType.String),
					new SQLiteParameter("@PURCHASELIMIT", DbType.Int64,8),
                    new SQLiteParameter("@MEMBERFLAG", DbType.Int64,8),
					new SQLiteParameter("@CODE", DbType.String)};
            parameters[0].Value = model.RANK;
            parameters[1].Value = model.TENANTSCOPE;
            parameters[2].Value = model.DISTRICTSCOPE;
            parameters[3].Value = model.SHOPSCOPE;
            parameters[4].Value = model.PROMOTYPE;
            parameters[5].Value = model.PROMOSUBTYPE;
            parameters[6].Value = model.PROMOACTION;
            parameters[7].Value = model.ELIGIBILITYCONDITION;
            parameters[8].Value = model.PROMOCONDITIONTYPE;
            parameters[9].Value = model.PROMOCONDITIONCONTEXT;
            parameters[10].Value = model.PROMOACTIONCONTEXT;
            parameters[11].Value = model.NAME;
            parameters[12].Value = model.DESCRIPTION;
            parameters[13].Value = model.TAG;
            parameters[14].Value = model.CANBECOMBINED;
            parameters[15].Value = model.ENABLED;
            parameters[16].Value = model.SALECHANNEL;
            parameters[17].Value = model.ENABLEDFROM;
            parameters[18].Value = model.ENABLEDTO;
            parameters[19].Value = model.ENABLEDTIMEINFO;
            parameters[20].Value = model.CREATEDAT;
            parameters[21].Value = model.UPDATEDAT;
            parameters[22].Value = model.CREATEDBY;
            parameters[23].Value = model.UPDATEDBY;
            parameters[24].Value = model.FROMOUTER;
            parameters[25].Value = model.OUTERCODE;
            parameters[26].Value = model.COSTCENTERINFO;
            parameters[27].Value = model.COSTRULECONTEXT;
            parameters[28].Value = model.CANMIXCOUPON;
            parameters[29].Value = model.ORDERSUBTYPE;
            parameters[30].Value = model.AVAILABLECATEGORY;
            parameters[31].Value = model.TENANTID;
            parameters[32].Value = model.SHOPID;
            parameters[33].Value = model.CREATE_URL_IP;
            parameters[34].Value = model.ONLYUSEINORIGINAL;
            parameters[35].Value = model.ONLYMEMBER;
            parameters[36].Value = model.MEMBERTAGS;
            parameters[37].Value = model.PURCHASELIMIT;
            parameters[38].Value = model.MEMBERFLAG;
            parameters[39].Value = model.CODE;

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
		#endregion  ExtensionMethod
    }
}
