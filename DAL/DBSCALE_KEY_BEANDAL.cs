using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Maticsoft.DAL
{
    /// <summary>
    /// 数据访问类:DBSCALE_KEY_BEANDAL
    /// </summary>
    public partial class DBSCALE_KEY_BEANDAL
    {
        public DBSCALE_KEY_BEANDAL()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int _id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from DBSCALE_KEY_BEAN");
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
        public int Add(Maticsoft.Model.DBSCALE_KEY_BEANMODEL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DBSCALE_KEY_BEAN(");
            strSql.Append("TEMPID,TEMPNAME,SCALESID,IP,PORT,SHOPID,SETTINGPAGE,SCALESTYPE,XPOS,YPOS,NOPOS,SKUCODE,CREATE_URL_IP,SYN_TIME,ERROR_TIME,STATUS)");
            strSql.Append(" values (");
            strSql.Append("@TEMPID,@TEMPNAME,@SCALESID,@IP,@PORT,@SHOPID,@SETTINGPAGE,@SCALESTYPE,@XPOS,@YPOS,@NOPOS,@SKUCODE,@CREATE_URL_IP,@SYN_TIME,@ERROR_TIME,@STATUS)");
            strSql.Append(";select LAST_INSERT_ROWID()");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@TEMPID", DbType.String),
					new SQLiteParameter("@TEMPNAME", DbType.String),
					new SQLiteParameter("@SCALESID", DbType.String),
					new SQLiteParameter("@IP", DbType.String),
					new SQLiteParameter("@PORT", DbType.String),
					new SQLiteParameter("@SHOPID", DbType.String),
					new SQLiteParameter("@SETTINGPAGE", DbType.Int32,8),
					new SQLiteParameter("@SCALESTYPE", DbType.String),
					new SQLiteParameter("@XPOS", DbType.Int32,8),
					new SQLiteParameter("@YPOS", DbType.Int32,8),
					new SQLiteParameter("@NOPOS", DbType.Int32,8),
					new SQLiteParameter("@SKUCODE", DbType.String),
					new SQLiteParameter("@CREATE_URL_IP", DbType.String),
					new SQLiteParameter("@SYN_TIME", DbType.Int32,8),
					new SQLiteParameter("@ERROR_TIME", DbType.Int32,8),
					new SQLiteParameter("@STATUS", DbType.Int32,8)};
            parameters[0].Value = model.TEMPID;
            parameters[1].Value = model.TEMPNAME;
            parameters[2].Value = model.SCALESID;
            parameters[3].Value = model.IP;
            parameters[4].Value = model.PORT;
            parameters[5].Value = model.SHOPID;
            parameters[6].Value = model.SETTINGPAGE;
            parameters[7].Value = model.SCALESTYPE;
            parameters[8].Value = model.XPOS;
            parameters[9].Value = model.YPOS;
            parameters[10].Value = model.NOPOS;
            parameters[11].Value = model.SKUCODE;
            parameters[12].Value = model.CREATE_URL_IP;
            parameters[13].Value = model.SYN_TIME;
            parameters[14].Value = model.ERROR_TIME;
            parameters[15].Value = model.STATUS;

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
        public bool Update(Maticsoft.Model.DBSCALE_KEY_BEANMODEL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DBSCALE_KEY_BEAN set ");
            strSql.Append("TEMPID=@TEMPID,");
            strSql.Append("TEMPNAME=@TEMPNAME,");
            strSql.Append("SCALESID=@SCALESID,");
            strSql.Append("IP=@IP,");
            strSql.Append("PORT=@PORT,");
            strSql.Append("SHOPID=@SHOPID,");
            strSql.Append("SETTINGPAGE=@SETTINGPAGE,");
            strSql.Append("SCALESTYPE=@SCALESTYPE,");
            strSql.Append("XPOS=@XPOS,");
            strSql.Append("YPOS=@YPOS,");
            strSql.Append("NOPOS=@NOPOS,");
            strSql.Append("SKUCODE=@SKUCODE,");
            strSql.Append("CREATE_URL_IP=@CREATE_URL_IP,");
            strSql.Append("SYN_TIME=@SYN_TIME,");
            strSql.Append("ERROR_TIME=@ERROR_TIME,");
            strSql.Append("STATUS=@STATUS");
            strSql.Append(" where _id=@_id");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@TEMPID", DbType.String),
					new SQLiteParameter("@TEMPNAME", DbType.String),
					new SQLiteParameter("@SCALESID", DbType.String),
					new SQLiteParameter("@IP", DbType.String),
					new SQLiteParameter("@PORT", DbType.String),
					new SQLiteParameter("@SHOPID", DbType.String),
					new SQLiteParameter("@SETTINGPAGE", DbType.Int32,8),
					new SQLiteParameter("@SCALESTYPE", DbType.String),
					new SQLiteParameter("@XPOS", DbType.Int32,8),
					new SQLiteParameter("@YPOS", DbType.Int32,8),
					new SQLiteParameter("@NOPOS", DbType.Int32,8),
					new SQLiteParameter("@SKUCODE", DbType.String),
					new SQLiteParameter("@CREATE_URL_IP", DbType.String),
					new SQLiteParameter("@SYN_TIME", DbType.Int32,8),
					new SQLiteParameter("@ERROR_TIME", DbType.Int32,8),
					new SQLiteParameter("@STATUS", DbType.Int32,8),
					new SQLiteParameter("@_id", DbType.Int32,8)};
            parameters[0].Value = model.TEMPID;
            parameters[1].Value = model.TEMPNAME;
            parameters[2].Value = model.SCALESID;
            parameters[3].Value = model.IP;
            parameters[4].Value = model.PORT;
            parameters[5].Value = model.SHOPID;
            parameters[6].Value = model.SETTINGPAGE;
            parameters[7].Value = model.SCALESTYPE;
            parameters[8].Value = model.XPOS;
            parameters[9].Value = model.YPOS;
            parameters[10].Value = model.NOPOS;
            parameters[11].Value = model.SKUCODE;
            parameters[12].Value = model.CREATE_URL_IP;
            parameters[13].Value = model.SYN_TIME;
            parameters[14].Value = model.ERROR_TIME;
            parameters[15].Value = model.STATUS;
            parameters[16].Value = model._id;

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
            strSql.Append("delete from DBSCALE_KEY_BEAN ");
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
            strSql.Append("delete from DBSCALE_KEY_BEAN ");
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
        public Maticsoft.Model.DBSCALE_KEY_BEANMODEL GetModel(int _id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select _id,TEMPID,TEMPNAME,SCALESID,IP,PORT,SHOPID,SETTINGPAGE,SCALESTYPE,XPOS,YPOS,NOPOS,SKUCODE,CREATE_URL_IP,SYN_TIME,ERROR_TIME,STATUS from DBSCALE_KEY_BEAN ");
            strSql.Append(" where _id=@_id");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@_id", DbType.Int32,4)
			};
            parameters[0].Value = _id;

            Maticsoft.Model.DBSCALE_KEY_BEANMODEL model = new Maticsoft.Model.DBSCALE_KEY_BEANMODEL();
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
        public Maticsoft.Model.DBSCALE_KEY_BEANMODEL DataRowToModel(DataRow row)
        {
            Maticsoft.Model.DBSCALE_KEY_BEANMODEL model = new Maticsoft.Model.DBSCALE_KEY_BEANMODEL();
            if (row != null)
            {
                if (row["_id"] != null && row["_id"].ToString() != "")
                {
                    model._id = int.Parse(row["_id"].ToString());
                }
                if (row["TEMPID"] != null)
                {
                    model.TEMPID = row["TEMPID"].ToString();
                }
                if (row["TEMPNAME"] != null)
                {
                    model.TEMPNAME = row["TEMPNAME"].ToString();
                }
                if (row["SCALESID"] != null)
                {
                    model.SCALESID = row["SCALESID"].ToString();
                }
                if (row["IP"] != null)
                {
                    model.IP = row["IP"].ToString();
                }
                if (row["PORT"] != null)
                {
                    model.PORT = row["PORT"].ToString();
                }
                if (row["SHOPID"] != null)
                {
                    model.SHOPID = row["SHOPID"].ToString();
                }
                if (row["SETTINGPAGE"] != null && row["SETTINGPAGE"].ToString() != "")
                {
                    model.SETTINGPAGE = int.Parse(row["SETTINGPAGE"].ToString());
                }
                if (row["SCALESTYPE"] != null)
                {
                    model.SCALESTYPE = row["SCALESTYPE"].ToString();
                }
                if (row["XPOS"] != null && row["XPOS"].ToString() != "")
                {
                    model.XPOS = int.Parse(row["XPOS"].ToString());
                }
                if (row["YPOS"] != null && row["YPOS"].ToString() != "")
                {
                    model.YPOS = int.Parse(row["YPOS"].ToString());
                }
                if (row["NOPOS"] != null && row["NOPOS"].ToString() != "")
                {
                    model.NOPOS = int.Parse(row["NOPOS"].ToString());
                }
                if (row["SKUCODE"] != null)
                {
                    model.SKUCODE = row["SKUCODE"].ToString();
                }
                if (row["CREATE_URL_IP"] != null)
                {
                    model.CREATE_URL_IP = row["CREATE_URL_IP"].ToString();
                }
                if (row["SYN_TIME"] != null && row["SYN_TIME"].ToString() != "")
                {
                    model.SYN_TIME = int.Parse(row["SYN_TIME"].ToString());
                }
                if (row["ERROR_TIME"] != null && row["ERROR_TIME"].ToString() != "")
                {
                    model.ERROR_TIME = int.Parse(row["ERROR_TIME"].ToString());
                }
                if (row["STATUS"] != null && row["STATUS"].ToString() != "")
                {
                    model.STATUS = int.Parse(row["STATUS"].ToString());
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
            strSql.Append("select _id,TEMPID,TEMPNAME,SCALESID,IP,PORT,SHOPID,SETTINGPAGE,SCALESTYPE,XPOS,YPOS,NOPOS,SKUCODE,CREATE_URL_IP,SYN_TIME,ERROR_TIME,STATUS ");
            strSql.Append(" FROM DBSCALE_KEY_BEAN ");
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
            strSql.Append("select count(1) FROM DBSCALE_KEY_BEAN ");
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
            strSql.Append(")AS Row, T.*  from DBSCALE_KEY_BEAN T ");
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
            parameters[0].Value = "DBSCALE_KEY_BEAN";
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


        public bool AddScales(System.Collections.Generic.List<Maticsoft.Model.DBSCALE_KEY_BEANMODEL> lstmodel, string createurl,string shopid)
        {

            System.Collections.ArrayList lststring = new System.Collections.ArrayList();

            lststring.Add("delete  from DBSCALE_KEY_BEAN");
            foreach (Maticsoft.Model.DBSCALE_KEY_BEANMODEL model in lstmodel)
            {
                StringBuilder strSql = new StringBuilder();
                StringBuilder strSql1 = new StringBuilder();
                StringBuilder strSql2 = new StringBuilder();
                if (model.TEMPID != null)
                {
                    strSql1.Append("TEMPID,");
                    strSql2.Append("'" + model.TEMPID + "',");
                }
                if (model.TEMPNAME != null)
                {
                    strSql1.Append("TEMPNAME,");
                    strSql2.Append("'" + model.TEMPNAME + "',");
                }
                if (model.SCALESID != null)
                {
                    strSql1.Append("SCALESID,");
                    strSql2.Append("'" + model.SCALESID + "',");
                }
                if (model.IP != null)
                {
                    strSql1.Append("IP,");
                    strSql2.Append("'" + model.IP + "',");
                }
                if (model.PORT != null)
                {
                    strSql1.Append("PORT,");
                    strSql2.Append("'" + model.PORT + "',");
                }

                
                    strSql1.Append("SHOPID,");
                    strSql2.Append("'" + shopid + "',");
               
                //if (model.SHOPID != null)
                //{
                //    strSql1.Append("SHOPID,");
                //    strSql2.Append("'" + model.SHOPID + "',");
                //}
                if (model.SETTINGPAGE != null)
                {
                    strSql1.Append("SETTINGPAGE,");
                    strSql2.Append("" + model.SETTINGPAGE + ",");
                }
                if (model.SCALESTYPE != null)
                {
                    strSql1.Append("SCALESTYPE,");
                    strSql2.Append("'" + model.SCALESTYPE + "',");
                }
                if (model.XPOS != null)
                {
                    strSql1.Append("XPOS,");
                    strSql2.Append("" + model.XPOS + ",");
                }
                if (model.YPOS != null)
                {
                    strSql1.Append("YPOS,");
                    strSql2.Append("" + model.YPOS + ",");
                }
                if (model.NOPOS != null)
                {
                    strSql1.Append("NOPOS,");
                    strSql2.Append("" + model.NOPOS + ",");
                }
                if (model.SKUCODE != null)
                {
                    strSql1.Append("SKUCODE,");
                    strSql2.Append("'" + model.SKUCODE + "',");
                }
                if (createurl != null)
                {
                    strSql1.Append("CREATE_URL_IP,");
                    strSql2.Append("'" + createurl + "',");
                }
                if (model.SYN_TIME != null)
                {
                    strSql1.Append("SYN_TIME,");
                    strSql2.Append("" + model.SYN_TIME + ",");
                }
                if (model.ERROR_TIME != null)
                {
                    strSql1.Append("ERROR_TIME,");
                    strSql2.Append("" + model.ERROR_TIME + ",");
                }
                if (model.STATUS != null)
                {
                    strSql1.Append("STATUS,");
                    strSql2.Append("" + model.STATUS + ",");
                }
                strSql.Append("insert into DBSCALE_KEY_BEAN(");
                strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
                strSql.Append(")");
                strSql.Append(" values (");
                strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
                strSql.Append(")");
                strSql.Append(";select LAST_INSERT_ROWID()");

                lststring.Add(strSql.ToString());
            }

            DbHelperSQLite.ExecuteSqlTran(lststring);

            return true;

        }



        public List<string> GetDiatinctByScaleIP(string strwhere)
        {
            List<string> sort = new List<string>();
            string cmdsql = "select distinct IP from DBSCALE_KEY_BEAN";
            if (!string.IsNullOrEmpty(strwhere))
            {
                cmdsql += " where " + strwhere;
            }
            DataSet ds = DbHelperSQLite.Query(cmdsql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sort.Add(dr["IP"].ToString());
                }
            }
            return sort;
        }


        #endregion  ExtensionMethod
    }
}
