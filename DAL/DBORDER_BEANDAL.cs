using System;
using System.Data;
using System.Text;
using System.Data.SQLite;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:DBORDER_BEAN
	/// </summary>
	public partial class DBORDER_BEANDAL
	{
		public DBORDER_BEANDAL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQLite.GetMaxID("_id", "DBORDER_BEAN"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int _id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from DBORDER_BEAN");
			strSql.Append(" where _id=@_id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@_id", DbType.Int64,4)
			};
			parameters[0].Value = _id;

			return DbHelperSQLite.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.DBORDER_BEANMODEL model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into DBORDER_BEAN(");
			strSql.Append("OFFLINEORDERID,ORDER_JSON,ORDERAT,CUSTOMERPHONE,TITLE,PRICETOTAL,ORDERSTATUS,SYNSTATUS,SYN_TIME,ORDERSTATUSVALUE,REFUND_TIME,CREATE_TIME,CREATE_SN,BACKUPS_SNS,BACKUPS_TIME,BACKUPS_REFUND_SN,BACKUPS_REFUND_TIME,CREATE_URL_IP,VERSION_CODE)");
			strSql.Append(" values (");
			strSql.Append("@OFFLINEORDERID,@ORDER_JSON,@ORDERAT,@CUSTOMERPHONE,@TITLE,@PRICETOTAL,@ORDERSTATUS,@SYNSTATUS,@SYN_TIME,@ORDERSTATUSVALUE,@REFUND_TIME,@CREATE_TIME,@CREATE_SN,@BACKUPS_SNS,@BACKUPS_TIME,@BACKUPS_REFUND_SN,@BACKUPS_REFUND_TIME,@CREATE_URL_IP,@VERSION_CODE)");
			strSql.Append(";select LAST_INSERT_ROWID()");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@OFFLINEORDERID", DbType.String),
					new SQLiteParameter("@ORDER_JSON", DbType.String),
					new SQLiteParameter("@ORDERAT", DbType.Int64,8),
					new SQLiteParameter("@CUSTOMERPHONE", DbType.String),
					new SQLiteParameter("@TITLE", DbType.String),
					new SQLiteParameter("@PRICETOTAL", DbType.Decimal,4),
					new SQLiteParameter("@ORDERSTATUS", DbType.String),
					new SQLiteParameter("@SYNSTATUS", DbType.String),
					new SQLiteParameter("@SYN_TIME", DbType.Int64,8),
					new SQLiteParameter("@ORDERSTATUSVALUE", DbType.Int64,8),
					new SQLiteParameter("@REFUND_TIME", DbType.Int64,8),
					new SQLiteParameter("@CREATE_TIME", DbType.Int64,8),
					new SQLiteParameter("@CREATE_SN", DbType.String),
					new SQLiteParameter("@BACKUPS_SNS", DbType.String),
					new SQLiteParameter("@BACKUPS_TIME", DbType.String),
					new SQLiteParameter("@BACKUPS_REFUND_SN", DbType.String),
					new SQLiteParameter("@BACKUPS_REFUND_TIME", DbType.String),
					new SQLiteParameter("@CREATE_URL_IP", DbType.String),
					new SQLiteParameter("@VERSION_CODE", DbType.Int64,8)};
			parameters[0].Value = model.OFFLINEORDERID;
			parameters[1].Value = model.ORDER_JSON;
			parameters[2].Value = model.ORDERAT;
			parameters[3].Value = model.CUSTOMERPHONE;
			parameters[4].Value = model.TITLE;
			parameters[5].Value = model.PRICETOTAL;
			parameters[6].Value = model.ORDERSTATUS;
			parameters[7].Value = model.SYNSTATUS;
			parameters[8].Value = model.SYN_TIME;
			parameters[9].Value = model.ORDERSTATUSVALUE;
			parameters[10].Value = model.REFUND_TIME;
			parameters[11].Value = model.CREATE_TIME;
			parameters[12].Value = model.CREATE_SN;
			parameters[13].Value = model.BACKUPS_SNS;
			parameters[14].Value = model.BACKUPS_TIME;
			parameters[15].Value = model.BACKUPS_REFUND_SN;
			parameters[16].Value = model.BACKUPS_REFUND_TIME;
			parameters[17].Value = model.CREATE_URL_IP;
			parameters[18].Value = model.VERSION_CODE;

			object obj = DbHelperSQLite.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(Maticsoft.Model.DBORDER_BEANMODEL model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update DBORDER_BEAN set ");
			strSql.Append("OFFLINEORDERID=@OFFLINEORDERID,");
			strSql.Append("ORDER_JSON=@ORDER_JSON,");
			strSql.Append("ORDERAT=@ORDERAT,");
			strSql.Append("CUSTOMERPHONE=@CUSTOMERPHONE,");
			strSql.Append("TITLE=@TITLE,");
			strSql.Append("PRICETOTAL=@PRICETOTAL,");
			strSql.Append("ORDERSTATUS=@ORDERSTATUS,");
			strSql.Append("SYNSTATUS=@SYNSTATUS,");
			strSql.Append("SYN_TIME=@SYN_TIME,");
			strSql.Append("ORDERSTATUSVALUE=@ORDERSTATUSVALUE,");
			strSql.Append("REFUND_TIME=@REFUND_TIME,");
			strSql.Append("CREATE_TIME=@CREATE_TIME,");
			strSql.Append("CREATE_SN=@CREATE_SN,");
			strSql.Append("BACKUPS_SNS=@BACKUPS_SNS,");
			strSql.Append("BACKUPS_TIME=@BACKUPS_TIME,");
			strSql.Append("BACKUPS_REFUND_SN=@BACKUPS_REFUND_SN,");
			strSql.Append("BACKUPS_REFUND_TIME=@BACKUPS_REFUND_TIME,");
			strSql.Append("CREATE_URL_IP=@CREATE_URL_IP,");
			strSql.Append("VERSION_CODE=@VERSION_CODE");
			strSql.Append(" where _id=@_id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@OFFLINEORDERID", DbType.String),
					new SQLiteParameter("@ORDER_JSON", DbType.String),
					new SQLiteParameter("@ORDERAT", DbType.Int64,8),
					new SQLiteParameter("@CUSTOMERPHONE", DbType.String),
					new SQLiteParameter("@TITLE", DbType.String),
					new SQLiteParameter("@PRICETOTAL", DbType.Decimal,4),
					new SQLiteParameter("@ORDERSTATUS", DbType.String),
					new SQLiteParameter("@SYNSTATUS", DbType.String),
					new SQLiteParameter("@SYN_TIME", DbType.Int64,8),
					new SQLiteParameter("@ORDERSTATUSVALUE", DbType.Int64,8),
					new SQLiteParameter("@REFUND_TIME", DbType.Int64,8),
					new SQLiteParameter("@CREATE_TIME", DbType.Int64,8),
					new SQLiteParameter("@CREATE_SN", DbType.String),
					new SQLiteParameter("@BACKUPS_SNS", DbType.String),
					new SQLiteParameter("@BACKUPS_TIME", DbType.String),
					new SQLiteParameter("@BACKUPS_REFUND_SN", DbType.String),
					new SQLiteParameter("@BACKUPS_REFUND_TIME", DbType.String),
					new SQLiteParameter("@CREATE_URL_IP", DbType.String),
					new SQLiteParameter("@VERSION_CODE", DbType.Int64,8),
					new SQLiteParameter("@_id", DbType.Int64,8)};
			parameters[0].Value = model.OFFLINEORDERID;
			parameters[1].Value = model.ORDER_JSON;
			parameters[2].Value = model.ORDERAT;
			parameters[3].Value = model.CUSTOMERPHONE;
			parameters[4].Value = model.TITLE;
			parameters[5].Value = model.PRICETOTAL;
			parameters[6].Value = model.ORDERSTATUS;
			parameters[7].Value = model.SYNSTATUS;
			parameters[8].Value = model.SYN_TIME;
			parameters[9].Value = model.ORDERSTATUSVALUE;
			parameters[10].Value = model.REFUND_TIME;
			parameters[11].Value = model.CREATE_TIME;
			parameters[12].Value = model.CREATE_SN;
			parameters[13].Value = model.BACKUPS_SNS;
			parameters[14].Value = model.BACKUPS_TIME;
			parameters[15].Value = model.BACKUPS_REFUND_SN;
			parameters[16].Value = model.BACKUPS_REFUND_TIME;
			parameters[17].Value = model.CREATE_URL_IP;
			parameters[18].Value = model.VERSION_CODE;
			parameters[19].Value = model._id;

			int rows=DbHelperSQLite.ExecuteSql(strSql.ToString(),parameters);
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from DBORDER_BEAN ");
			strSql.Append(" where _id=@_id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@_id", DbType.Int64,4)
			};
			parameters[0].Value = _id;

			int rows=DbHelperSQLite.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string _idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from DBORDER_BEAN ");
			strSql.Append(" where _id in ("+_idlist + ")  ");
			int rows=DbHelperSQLite.ExecuteSql(strSql.ToString());
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
		public Maticsoft.Model.DBORDER_BEANMODEL GetModel(int _id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select _id,OFFLINEORDERID,ORDER_JSON,ORDERAT,CUSTOMERPHONE,TITLE,PRICETOTAL,ORDERSTATUS,SYNSTATUS,SYN_TIME,ORDERSTATUSVALUE,REFUND_TIME,CREATE_TIME,CREATE_SN,BACKUPS_SNS,BACKUPS_TIME,BACKUPS_REFUND_SN,BACKUPS_REFUND_TIME,CREATE_URL_IP,VERSION_CODE from DBORDER_BEAN ");
			strSql.Append(" where _id=@_id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@_id", DbType.Int64,4)
			};
			parameters[0].Value = _id;

			Maticsoft.Model.DBORDER_BEANMODEL model=new Maticsoft.Model.DBORDER_BEANMODEL();
			DataSet ds=DbHelperSQLite.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
		public Maticsoft.Model.DBORDER_BEANMODEL DataRowToModel(DataRow row)
		{
			Maticsoft.Model.DBORDER_BEANMODEL model=new Maticsoft.Model.DBORDER_BEANMODEL();
			if (row != null)
			{
				if(row["_id"]!=null && row["_id"].ToString()!="")
				{
					model._id=Int64.Parse(row["_id"].ToString());
				}
				if(row["OFFLINEORDERID"]!=null)
				{
					model.OFFLINEORDERID=row["OFFLINEORDERID"].ToString();
				}
				if(row["ORDER_JSON"]!=null)
				{
					model.ORDER_JSON=row["ORDER_JSON"].ToString();
				}
				if(row["ORDERAT"]!=null && row["ORDERAT"].ToString()!="")
				{
                    model.ORDERAT = Int64.Parse(row["ORDERAT"].ToString());
				}
				if(row["CUSTOMERPHONE"]!=null)
				{
					model.CUSTOMERPHONE=row["CUSTOMERPHONE"].ToString();
				}
				if(row["TITLE"]!=null)
				{
					model.TITLE=row["TITLE"].ToString();
				}
				if(row["PRICETOTAL"]!=null && row["PRICETOTAL"].ToString()!="")
				{
					model.PRICETOTAL=decimal.Parse(row["PRICETOTAL"].ToString());
				}
				if(row["ORDERSTATUS"]!=null)
				{
					model.ORDERSTATUS=row["ORDERSTATUS"].ToString();
				}
				if(row["SYNSTATUS"]!=null)
				{
					model.SYNSTATUS=row["SYNSTATUS"].ToString();
				}
				if(row["SYN_TIME"]!=null && row["SYN_TIME"].ToString()!="")
				{
                    model.SYN_TIME = Int64.Parse(row["SYN_TIME"].ToString());
				}
				if(row["ORDERSTATUSVALUE"]!=null && row["ORDERSTATUSVALUE"].ToString()!="")
				{
                    model.ORDERSTATUSVALUE = Int64.Parse(row["ORDERSTATUSVALUE"].ToString());
				}
				if(row["REFUND_TIME"]!=null && row["REFUND_TIME"].ToString()!="")
				{
                    model.REFUND_TIME = Int64.Parse(row["REFUND_TIME"].ToString());
				}
				if(row["CREATE_TIME"]!=null && row["CREATE_TIME"].ToString()!="")
				{
                    model.CREATE_TIME = Int64.Parse(row["CREATE_TIME"].ToString());
				}
				if(row["CREATE_SN"]!=null)
				{
					model.CREATE_SN=row["CREATE_SN"].ToString();
				}
				if(row["BACKUPS_SNS"]!=null)
				{
					model.BACKUPS_SNS=row["BACKUPS_SNS"].ToString();
				}
				if(row["BACKUPS_TIME"]!=null)
				{
					model.BACKUPS_TIME=row["BACKUPS_TIME"].ToString();
				}
				if(row["BACKUPS_REFUND_SN"]!=null)
				{
					model.BACKUPS_REFUND_SN=row["BACKUPS_REFUND_SN"].ToString();
				}
				if(row["BACKUPS_REFUND_TIME"]!=null)
				{
					model.BACKUPS_REFUND_TIME=row["BACKUPS_REFUND_TIME"].ToString();
				}
				if(row["CREATE_URL_IP"]!=null)
				{
					model.CREATE_URL_IP=row["CREATE_URL_IP"].ToString();
				}
				if(row["VERSION_CODE"]!=null && row["VERSION_CODE"].ToString()!="")
				{
                    model.VERSION_CODE = Int64.Parse(row["VERSION_CODE"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select _id,OFFLINEORDERID,ORDER_JSON,ORDERAT,CUSTOMERPHONE,TITLE,PRICETOTAL,ORDERSTATUS,SYNSTATUS,SYN_TIME,ORDERSTATUSVALUE,REFUND_TIME,CREATE_TIME,CREATE_SN,BACKUPS_SNS,BACKUPS_TIME,BACKUPS_REFUND_SN,BACKUPS_REFUND_TIME,CREATE_URL_IP,VERSION_CODE ");
			strSql.Append(" FROM DBORDER_BEAN ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQLite.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM DBORDER_BEAN ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T._id desc");
			}
			strSql.Append(")AS Row, T.*  from DBORDER_BEAN T ");
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
			parameters[0].Value = "DBORDER_BEAN";
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
        /// 删除一条数据
        /// </summary>
        public bool Delete(string offlineorderid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from DBORDER_BEAN ");
            strSql.Append(" where OFFLINEORDERID=@OFFLINEORDERID");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@OFFLINEORDERID", DbType.String )
			};
            parameters[0].Value = offlineorderid;

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
        /// 按条件删除
        /// </summary>
        public bool ClearHistory(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from DBORDER_BEAN ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

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
        public Maticsoft.Model.DBORDER_BEANMODEL GetModel(string offlineorderid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select _id,OFFLINEORDERID,ORDER_JSON,ORDERAT,CUSTOMERPHONE,TITLE,PRICETOTAL,ORDERSTATUS,SYNSTATUS,SYN_TIME,ORDERSTATUSVALUE,REFUND_TIME,CREATE_TIME,CREATE_SN,BACKUPS_SNS,BACKUPS_TIME,BACKUPS_REFUND_SN,BACKUPS_REFUND_TIME,CREATE_URL_IP,VERSION_CODE from DBORDER_BEAN ");
            strSql.Append(" where OFFLINEORDERID=@OFFLINEORDERID");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@OFFLINEORDERID", DbType.String)
			};
            parameters[0].Value = offlineorderid;

            Maticsoft.Model.DBORDER_BEANMODEL model = new Maticsoft.Model.DBORDER_BEANMODEL();
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
		#endregion  ExtensionMethod
	}
}

