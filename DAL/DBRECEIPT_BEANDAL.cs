using System;
using System.Data;
using System.Text;
using System.Data.SQLite;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:DBRECEIPT_BEAN
	/// </summary>
	public partial class DBRECEIPT_BEANDAL
	{
		public DBRECEIPT_BEANDAL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQLite.GetMaxID("_id", "DBRECEIPT_BEAN"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int _id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from DBRECEIPT_BEAN");
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
		public int Add(Maticsoft.Model.DBRECEIPT_BEANMODEL model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into DBRECEIPT_BEAN(");
			strSql.Append("CASHIER,CASHTOTALAMT,ENDTIME,NETSALEAMT,OPERATETIMESTR,RECEIPTDETAIL,STARTTIME,TOTALPAYMENT,SYN_TIME,OFFLINE_RECEIPT_ID,CREATE_TIME,CREATE_SN,CREATE_URL_IP,VERSION_CODE,BACKUPS_SN,BACKUPS_SN_TIME)");
			strSql.Append(" values (");
			strSql.Append("@CASHIER,@CASHTOTALAMT,@ENDTIME,@NETSALEAMT,@OPERATETIMESTR,@RECEIPTDETAIL,@STARTTIME,@TOTALPAYMENT,@SYN_TIME,@OFFLINE_RECEIPT_ID,@CREATE_TIME,@CREATE_SN,@CREATE_URL_IP,@VERSION_CODE,@BACKUPS_SN,@BACKUPS_SN_TIME)");
			strSql.Append(";select LAST_INSERT_ROWID()");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@CASHIER", DbType.String),
					new SQLiteParameter("@CASHTOTALAMT", DbType.Decimal,4),
					new SQLiteParameter("@ENDTIME", DbType.Int64,8),
					new SQLiteParameter("@NETSALEAMT", DbType.Decimal,4),
					new SQLiteParameter("@OPERATETIMESTR", DbType.String),
					new SQLiteParameter("@RECEIPTDETAIL", DbType.String),
					new SQLiteParameter("@STARTTIME", DbType.Int64,8),
					new SQLiteParameter("@TOTALPAYMENT", DbType.Decimal,4),
					new SQLiteParameter("@SYN_TIME", DbType.Int64,8),
					new SQLiteParameter("@OFFLINE_RECEIPT_ID", DbType.String),
					new SQLiteParameter("@CREATE_TIME", DbType.Int64,8),
					new SQLiteParameter("@CREATE_SN", DbType.String),
					new SQLiteParameter("@CREATE_URL_IP", DbType.String),
					new SQLiteParameter("@VERSION_CODE", DbType.Int64,8),
					new SQLiteParameter("@BACKUPS_SN", DbType.String),
					new SQLiteParameter("@BACKUPS_SN_TIME", DbType.String)};
			parameters[0].Value = model.CASHIER;
			parameters[1].Value = model.CASHTOTALAMT;
			parameters[2].Value = model.ENDTIME;
			parameters[3].Value = model.NETSALEAMT;
			parameters[4].Value = model.OPERATETIMESTR;
			parameters[5].Value = model.RECEIPTDETAIL;
			parameters[6].Value = model.STARTTIME;
			parameters[7].Value = model.TOTALPAYMENT;
			parameters[8].Value = model.SYN_TIME;
			parameters[9].Value = model.OFFLINE_RECEIPT_ID;
			parameters[10].Value = model.CREATE_TIME;
			parameters[11].Value = model.CREATE_SN;
			parameters[12].Value = model.CREATE_URL_IP;
			parameters[13].Value = model.VERSION_CODE;
			parameters[14].Value = model.BACKUPS_SN;
			parameters[15].Value = model.BACKUPS_SN_TIME;

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
		public bool Update(Maticsoft.Model.DBRECEIPT_BEANMODEL model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update DBRECEIPT_BEAN set ");
			strSql.Append("CASHIER=@CASHIER,");
			strSql.Append("CASHTOTALAMT=@CASHTOTALAMT,");
			strSql.Append("ENDTIME=@ENDTIME,");
			strSql.Append("NETSALEAMT=@NETSALEAMT,");
			strSql.Append("OPERATETIMESTR=@OPERATETIMESTR,");
			strSql.Append("RECEIPTDETAIL=@RECEIPTDETAIL,");
			strSql.Append("STARTTIME=@STARTTIME,");
			strSql.Append("TOTALPAYMENT=@TOTALPAYMENT,");
			strSql.Append("SYN_TIME=@SYN_TIME,");
			strSql.Append("OFFLINE_RECEIPT_ID=@OFFLINE_RECEIPT_ID,");
			strSql.Append("CREATE_TIME=@CREATE_TIME,");
			strSql.Append("CREATE_SN=@CREATE_SN,");
			strSql.Append("CREATE_URL_IP=@CREATE_URL_IP,");
			strSql.Append("VERSION_CODE=@VERSION_CODE,");
			strSql.Append("BACKUPS_SN=@BACKUPS_SN,");
			strSql.Append("BACKUPS_SN_TIME=@BACKUPS_SN_TIME");
			strSql.Append(" where _id=@_id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@CASHIER", DbType.String),
					new SQLiteParameter("@CASHTOTALAMT", DbType.Decimal,4),
					new SQLiteParameter("@ENDTIME", DbType.Int64,8),
					new SQLiteParameter("@NETSALEAMT", DbType.Decimal,4),
					new SQLiteParameter("@OPERATETIMESTR", DbType.String),
					new SQLiteParameter("@RECEIPTDETAIL", DbType.String),
					new SQLiteParameter("@STARTTIME", DbType.Int64,8),
					new SQLiteParameter("@TOTALPAYMENT", DbType.Decimal,4),
					new SQLiteParameter("@SYN_TIME", DbType.Int64,8),
					new SQLiteParameter("@OFFLINE_RECEIPT_ID", DbType.String),
					new SQLiteParameter("@CREATE_TIME", DbType.Int64,8),
					new SQLiteParameter("@CREATE_SN", DbType.String),
					new SQLiteParameter("@CREATE_URL_IP", DbType.String),
					new SQLiteParameter("@VERSION_CODE", DbType.Int64,8),
					new SQLiteParameter("@BACKUPS_SN", DbType.String),
					new SQLiteParameter("@BACKUPS_SN_TIME", DbType.String),
					new SQLiteParameter("@_id", DbType.Int64,8)};
			parameters[0].Value = model.CASHIER;
			parameters[1].Value = model.CASHTOTALAMT;
			parameters[2].Value = model.ENDTIME;
			parameters[3].Value = model.NETSALEAMT;
			parameters[4].Value = model.OPERATETIMESTR;
			parameters[5].Value = model.RECEIPTDETAIL;
			parameters[6].Value = model.STARTTIME;
			parameters[7].Value = model.TOTALPAYMENT;
			parameters[8].Value = model.SYN_TIME;
			parameters[9].Value = model.OFFLINE_RECEIPT_ID;
			parameters[10].Value = model.CREATE_TIME;
			parameters[11].Value = model.CREATE_SN;
			parameters[12].Value = model.CREATE_URL_IP;
			parameters[13].Value = model.VERSION_CODE;
			parameters[14].Value = model.BACKUPS_SN;
			parameters[15].Value = model.BACKUPS_SN_TIME;
			parameters[16].Value = model._id;

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
			strSql.Append("delete from DBRECEIPT_BEAN ");
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
			strSql.Append("delete from DBRECEIPT_BEAN ");
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
		public Maticsoft.Model.DBRECEIPT_BEANMODEL GetModel(int _id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select _id,CASHIER,CASHTOTALAMT,ENDTIME,NETSALEAMT,OPERATETIMESTR,RECEIPTDETAIL,STARTTIME,TOTALPAYMENT,SYN_TIME,OFFLINE_RECEIPT_ID,CREATE_TIME,CREATE_SN,CREATE_URL_IP,VERSION_CODE,BACKUPS_SN,BACKUPS_SN_TIME from DBRECEIPT_BEAN ");
			strSql.Append(" where _id=@_id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@_id", DbType.Int64,4)
			};
			parameters[0].Value = _id;

			Maticsoft.Model.DBRECEIPT_BEANMODEL model=new Maticsoft.Model.DBRECEIPT_BEANMODEL();
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
		public Maticsoft.Model.DBRECEIPT_BEANMODEL DataRowToModel(DataRow row)
		{
			Maticsoft.Model.DBRECEIPT_BEANMODEL model=new Maticsoft.Model.DBRECEIPT_BEANMODEL();
			if (row != null)
			{
				if(row["_id"]!=null && row["_id"].ToString()!="")
				{
					model._id=Int64.Parse(row["_id"].ToString());
				}
				if(row["CASHIER"]!=null)
				{
					model.CASHIER=row["CASHIER"].ToString();
				}
				if(row["CASHTOTALAMT"]!=null && row["CASHTOTALAMT"].ToString()!="")
				{
					model.CASHTOTALAMT=decimal.Parse(row["CASHTOTALAMT"].ToString());
				}
				if(row["ENDTIME"]!=null && row["ENDTIME"].ToString()!="")
				{
                    model.ENDTIME = Int64.Parse(row["ENDTIME"].ToString());
				}
				if(row["NETSALEAMT"]!=null && row["NETSALEAMT"].ToString()!="")
				{
					model.NETSALEAMT=decimal.Parse(row["NETSALEAMT"].ToString());
				}
				if(row["OPERATETIMESTR"]!=null)
				{
					model.OPERATETIMESTR=row["OPERATETIMESTR"].ToString();
				}
				if(row["RECEIPTDETAIL"]!=null)
				{
					model.RECEIPTDETAIL=row["RECEIPTDETAIL"].ToString();
				}
				if(row["STARTTIME"]!=null && row["STARTTIME"].ToString()!="")
				{
                    model.STARTTIME = Int64.Parse(row["STARTTIME"].ToString());
				}
				if(row["TOTALPAYMENT"]!=null && row["TOTALPAYMENT"].ToString()!="")
				{
					model.TOTALPAYMENT=decimal.Parse(row["TOTALPAYMENT"].ToString());
				}
				if(row["SYN_TIME"]!=null && row["SYN_TIME"].ToString()!="")
				{
                    model.SYN_TIME = Int64.Parse(row["SYN_TIME"].ToString());
				}
				if(row["OFFLINE_RECEIPT_ID"]!=null)
				{
					model.OFFLINE_RECEIPT_ID=row["OFFLINE_RECEIPT_ID"].ToString();
				}
				if(row["CREATE_TIME"]!=null && row["CREATE_TIME"].ToString()!="")
				{
                    model.CREATE_TIME = Int64.Parse(row["CREATE_TIME"].ToString());
				}
				if(row["CREATE_SN"]!=null)
				{
					model.CREATE_SN=row["CREATE_SN"].ToString();
				}
				if(row["CREATE_URL_IP"]!=null)
				{
					model.CREATE_URL_IP=row["CREATE_URL_IP"].ToString();
				}
				if(row["VERSION_CODE"]!=null && row["VERSION_CODE"].ToString()!="")
				{
                    model.VERSION_CODE = Int64.Parse(row["VERSION_CODE"].ToString());
				}
				if(row["BACKUPS_SN"]!=null)
				{
					model.BACKUPS_SN=row["BACKUPS_SN"].ToString();
				}
				if(row["BACKUPS_SN_TIME"]!=null)
				{
					model.BACKUPS_SN_TIME=row["BACKUPS_SN_TIME"].ToString();
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
			strSql.Append("select _id,CASHIER,CASHTOTALAMT,ENDTIME,NETSALEAMT,OPERATETIMESTR,RECEIPTDETAIL,STARTTIME,TOTALPAYMENT,SYN_TIME,OFFLINE_RECEIPT_ID,CREATE_TIME,CREATE_SN,CREATE_URL_IP,VERSION_CODE,BACKUPS_SN,BACKUPS_SN_TIME ");
			strSql.Append(" FROM DBRECEIPT_BEAN ");
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
			strSql.Append("select count(1) FROM DBRECEIPT_BEAN ");
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
			strSql.Append(")AS Row, T.*  from DBRECEIPT_BEAN T ");
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
			parameters[0].Value = "DBRECEIPT_BEAN";
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
        /// 按条件删除
        /// </summary>
        public bool ClearHistory(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from DBRECEIPT_BEAN ");
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
		#endregion  ExtensionMethod
	}
}

