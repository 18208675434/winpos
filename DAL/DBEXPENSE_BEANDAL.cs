using System;
using System.Data;
using System.Text;
using System.Data.SQLite;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:DBEXPENSE_BEAN
	/// </summary>
	public partial class DBEXPENSE_BEANDAL
	{
		public DBEXPENSE_BEANDAL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQLite.GetMaxID("_id", "DBEXPENSE_BEAN"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int _id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from DBEXPENSE_BEAN");
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
		public int Add(Maticsoft.Model.DBEXPENSE_BEANMODEL model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into DBEXPENSE_BEAN(");
			strSql.Append("CREATEAT,CREATEURLIP,DEVICECODE,DEVICESN,EXPENSEFEE,EXPENSEID,OFFLINERECORDID,SALECLERKPHONE,CREATEBY,SHOPID,EXPENSENAME,TYPE,VERSIONCODE,SYNTIME)");
			strSql.Append(" values (");
			strSql.Append("@CREATEAT,@CREATEURLIP,@DEVICECODE,@DEVICESN,@EXPENSEFEE,@EXPENSEID,@OFFLINERECORDID,@SALECLERKPHONE,@CREATEBY,@SHOPID,@EXPENSENAME,@TYPE,@VERSIONCODE,@SYNTIME)");
			strSql.Append(";select LAST_INSERT_ROWID()");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@CREATEAT", DbType.Int64,8),
					new SQLiteParameter("@CREATEURLIP", DbType.String),
					new SQLiteParameter("@DEVICECODE", DbType.String),
					new SQLiteParameter("@DEVICESN", DbType.String),
					new SQLiteParameter("@EXPENSEFEE", DbType.Decimal,4),
					new SQLiteParameter("@EXPENSEID", DbType.String),
					new SQLiteParameter("@OFFLINERECORDID", DbType.String),
					new SQLiteParameter("@SALECLERKPHONE", DbType.String),
					new SQLiteParameter("@CREATEBY", DbType.String),
					new SQLiteParameter("@SHOPID", DbType.String),
					new SQLiteParameter("@EXPENSENAME", DbType.String),
					new SQLiteParameter("@TYPE", DbType.String),
					new SQLiteParameter("@VERSIONCODE", DbType.Int64,8),
					new SQLiteParameter("@SYNTIME", DbType.Int64,8)};
			parameters[0].Value = model.CREATEAT;
			parameters[1].Value = model.CREATEURLIP;
			parameters[2].Value = model.DEVICECODE;
			parameters[3].Value = model.DEVICESN;
			parameters[4].Value = model.EXPENSEFEE;
			parameters[5].Value = model.EXPENSEID;
			parameters[6].Value = model.OFFLINERECORDID;
			parameters[7].Value = model.SALECLERKPHONE;
			parameters[8].Value = model.CREATEBY;
			parameters[9].Value = model.SHOPID;
			parameters[10].Value = model.EXPENSENAME;
			parameters[11].Value = model.TYPE;
			parameters[12].Value = model.VERSIONCODE;
			parameters[13].Value = model.SYNTIME;

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
		public bool Update(Maticsoft.Model.DBEXPENSE_BEANMODEL model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update DBEXPENSE_BEAN set ");
			strSql.Append("CREATEAT=@CREATEAT,");
			strSql.Append("CREATEURLIP=@CREATEURLIP,");
			strSql.Append("DEVICECODE=@DEVICECODE,");
			strSql.Append("DEVICESN=@DEVICESN,");
			strSql.Append("EXPENSEFEE=@EXPENSEFEE,");
			strSql.Append("EXPENSEID=@EXPENSEID,");
			strSql.Append("OFFLINERECORDID=@OFFLINERECORDID,");
			strSql.Append("SALECLERKPHONE=@SALECLERKPHONE,");
			strSql.Append("CREATEBY=@CREATEBY,");
			strSql.Append("SHOPID=@SHOPID,");
			strSql.Append("EXPENSENAME=@EXPENSENAME,");
			strSql.Append("TYPE=@TYPE,");
			strSql.Append("VERSIONCODE=@VERSIONCODE,");
			strSql.Append("SYNTIME=@SYNTIME");
			strSql.Append(" where _id=@_id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@CREATEAT", DbType.Int64,8),
					new SQLiteParameter("@CREATEURLIP", DbType.String),
					new SQLiteParameter("@DEVICECODE", DbType.String),
					new SQLiteParameter("@DEVICESN", DbType.String),
					new SQLiteParameter("@EXPENSEFEE", DbType.Decimal,4),
					new SQLiteParameter("@EXPENSEID", DbType.String),
					new SQLiteParameter("@OFFLINERECORDID", DbType.String),
					new SQLiteParameter("@SALECLERKPHONE", DbType.String),
					new SQLiteParameter("@CREATEBY", DbType.String),
					new SQLiteParameter("@SHOPID", DbType.String),
					new SQLiteParameter("@EXPENSENAME", DbType.String),
					new SQLiteParameter("@TYPE", DbType.String),
					new SQLiteParameter("@VERSIONCODE", DbType.Int64,8),
					new SQLiteParameter("@SYNTIME", DbType.Int64,8),
					new SQLiteParameter("@_id", DbType.Int64,8)};
			parameters[0].Value = model.CREATEAT;
			parameters[1].Value = model.CREATEURLIP;
			parameters[2].Value = model.DEVICECODE;
			parameters[3].Value = model.DEVICESN;
			parameters[4].Value = model.EXPENSEFEE;
			parameters[5].Value = model.EXPENSEID;
			parameters[6].Value = model.OFFLINERECORDID;
			parameters[7].Value = model.SALECLERKPHONE;
			parameters[8].Value = model.CREATEBY;
			parameters[9].Value = model.SHOPID;
			parameters[10].Value = model.EXPENSENAME;
			parameters[11].Value = model.TYPE;
			parameters[12].Value = model.VERSIONCODE;
			parameters[13].Value = model.SYNTIME;
			parameters[14].Value = model._id;

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
			strSql.Append("delete from DBEXPENSE_BEAN ");
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
			strSql.Append("delete from DBEXPENSE_BEAN ");
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
		public Maticsoft.Model.DBEXPENSE_BEANMODEL GetModel(int _id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select _id,CREATEAT,CREATEURLIP,DEVICECODE,DEVICESN,EXPENSEFEE,EXPENSEID,OFFLINERECORDID,SALECLERKPHONE,CREATEBY,SHOPID,EXPENSENAME,TYPE,VERSIONCODE,SYNTIME from DBEXPENSE_BEAN ");
			strSql.Append(" where _id=@_id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@_id", DbType.Int64,4)
			};
			parameters[0].Value = _id;

			Maticsoft.Model.DBEXPENSE_BEANMODEL model=new Maticsoft.Model.DBEXPENSE_BEANMODEL();
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
		public Maticsoft.Model.DBEXPENSE_BEANMODEL DataRowToModel(DataRow row)
		{
			Maticsoft.Model.DBEXPENSE_BEANMODEL model=new Maticsoft.Model.DBEXPENSE_BEANMODEL();
			if (row != null)
			{
				if(row["_id"]!=null && row["_id"].ToString()!="")
				{
					model._id=Int64.Parse(row["_id"].ToString());
				}
				if(row["CREATEAT"]!=null && row["CREATEAT"].ToString()!="")
				{
					model.CREATEAT=Int64.Parse(row["CREATEAT"].ToString());
				}
				if(row["CREATEURLIP"]!=null)
				{
					model.CREATEURLIP=row["CREATEURLIP"].ToString();
				}
				if(row["DEVICECODE"]!=null)
				{
					model.DEVICECODE=row["DEVICECODE"].ToString();
				}
				if(row["DEVICESN"]!=null)
				{
					model.DEVICESN=row["DEVICESN"].ToString();
				}
				if(row["EXPENSEFEE"]!=null && row["EXPENSEFEE"].ToString()!="")
				{
					model.EXPENSEFEE=decimal.Parse(row["EXPENSEFEE"].ToString());
				}
				if(row["EXPENSEID"]!=null)
				{
					model.EXPENSEID=row["EXPENSEID"].ToString();
				}
				if(row["OFFLINERECORDID"]!=null)
				{
					model.OFFLINERECORDID=row["OFFLINERECORDID"].ToString();
				}
				if(row["SALECLERKPHONE"]!=null)
				{
					model.SALECLERKPHONE=row["SALECLERKPHONE"].ToString();
				}
				if(row["CREATEBY"]!=null)
				{
					model.CREATEBY=row["CREATEBY"].ToString();
				}
				if(row["SHOPID"]!=null)
				{
					model.SHOPID=row["SHOPID"].ToString();
				}
				if(row["EXPENSENAME"]!=null)
				{
					model.EXPENSENAME=row["EXPENSENAME"].ToString();
				}
				if(row["TYPE"]!=null)
				{
					model.TYPE=row["TYPE"].ToString();
				}
				if(row["VERSIONCODE"]!=null && row["VERSIONCODE"].ToString()!="")
				{
					model.VERSIONCODE=Int64.Parse(row["VERSIONCODE"].ToString());
				}
				if(row["SYNTIME"]!=null && row["SYNTIME"].ToString()!="")
				{
					model.SYNTIME=Int64.Parse(row["SYNTIME"].ToString());
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
			strSql.Append("select _id,CREATEAT,CREATEURLIP,DEVICECODE,DEVICESN,EXPENSEFEE,EXPENSEID,OFFLINERECORDID,SALECLERKPHONE,CREATEBY,SHOPID,EXPENSENAME,TYPE,VERSIONCODE,SYNTIME ");
			strSql.Append(" FROM DBEXPENSE_BEAN ");
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
			strSql.Append("select count(1) FROM DBEXPENSE_BEAN ");
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
			strSql.Append(")AS Row, T.*  from DBEXPENSE_BEAN T ");
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
			parameters[0].Value = "DBEXPENSE_BEAN";
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
            strSql.Append("delete from DBEXPENSE_BEAN ");
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

