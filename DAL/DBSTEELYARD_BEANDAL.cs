using System;
using System.Data;
using System.Text;
using System.Data.SQLite;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:DBSTEELYARD_BEAN
	/// </summary>
	public partial class DBSTEELYARD_BEANDAL
	{
		public DBSTEELYARD_BEANDAL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQLite.GetMaxID("_id", "DBSTEELYARD_BEAN"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int _id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from DBSTEELYARD_BEAN");
			strSql.Append(" where _id=@_id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@_id", DbType.Int32,4)
			};
			parameters[0].Value = _id;

			return DbHelperSQLite.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.DBSTEELYARD_BEANMODEL model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into DBSTEELYARD_BEAN(");
			strSql.Append("NO,NAME,IP,TYPE,SYN_TIME,USER_NAME,PASSWORD,CREATE_TIME,CREATE_SN,CREATE_URL_IP,VERSION_CODE)");
			strSql.Append(" values (");
			strSql.Append("@NO,@NAME,@IP,@TYPE,@SYN_TIME,@USER_NAME,@PASSWORD,@CREATE_TIME,@CREATE_SN,@CREATE_URL_IP,@VERSION_CODE)");
			strSql.Append(";select LAST_INSERT_ROWID()");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@NO", DbType.String),
					new SQLiteParameter("@NAME", DbType.String),
					new SQLiteParameter("@IP", DbType.String),
					new SQLiteParameter("@TYPE", DbType.String),
					new SQLiteParameter("@SYN_TIME", DbType.Int32,8),
					new SQLiteParameter("@USER_NAME", DbType.String),
					new SQLiteParameter("@PASSWORD", DbType.String),
					new SQLiteParameter("@CREATE_TIME", DbType.Int32,8),
					new SQLiteParameter("@CREATE_SN", DbType.String),
					new SQLiteParameter("@CREATE_URL_IP", DbType.String),
					new SQLiteParameter("@VERSION_CODE", DbType.Int32,8)};
			parameters[0].Value = model.NO;
			parameters[1].Value = model.NAME;
			parameters[2].Value = model.IP;
			parameters[3].Value = model.TYPE;
			parameters[4].Value = model.SYN_TIME;
			parameters[5].Value = model.USER_NAME;
			parameters[6].Value = model.PASSWORD;
			parameters[7].Value = model.CREATE_TIME;
			parameters[8].Value = model.CREATE_SN;
			parameters[9].Value = model.CREATE_URL_IP;
			parameters[10].Value = model.VERSION_CODE;

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
		public bool Update(Maticsoft.Model.DBSTEELYARD_BEANMODEL model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update DBSTEELYARD_BEAN set ");
			strSql.Append("NO=@NO,");
			strSql.Append("NAME=@NAME,");
			strSql.Append("IP=@IP,");
			strSql.Append("TYPE=@TYPE,");
			strSql.Append("SYN_TIME=@SYN_TIME,");
			strSql.Append("USER_NAME=@USER_NAME,");
			strSql.Append("PASSWORD=@PASSWORD,");
			strSql.Append("CREATE_TIME=@CREATE_TIME,");
			strSql.Append("CREATE_SN=@CREATE_SN,");
			strSql.Append("CREATE_URL_IP=@CREATE_URL_IP,");
			strSql.Append("VERSION_CODE=@VERSION_CODE");
			strSql.Append(" where _id=@_id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@NO", DbType.String),
					new SQLiteParameter("@NAME", DbType.String),
					new SQLiteParameter("@IP", DbType.String),
					new SQLiteParameter("@TYPE", DbType.String),
					new SQLiteParameter("@SYN_TIME", DbType.Int32,8),
					new SQLiteParameter("@USER_NAME", DbType.String),
					new SQLiteParameter("@PASSWORD", DbType.String),
					new SQLiteParameter("@CREATE_TIME", DbType.Int32,8),
					new SQLiteParameter("@CREATE_SN", DbType.String),
					new SQLiteParameter("@CREATE_URL_IP", DbType.String),
					new SQLiteParameter("@VERSION_CODE", DbType.Int32,8),
					new SQLiteParameter("@_id", DbType.Int32,8)};
			parameters[0].Value = model.NO;
			parameters[1].Value = model.NAME;
			parameters[2].Value = model.IP;
			parameters[3].Value = model.TYPE;
			parameters[4].Value = model.SYN_TIME;
			parameters[5].Value = model.USER_NAME;
			parameters[6].Value = model.PASSWORD;
			parameters[7].Value = model.CREATE_TIME;
			parameters[8].Value = model.CREATE_SN;
			parameters[9].Value = model.CREATE_URL_IP;
			parameters[10].Value = model.VERSION_CODE;
			parameters[11].Value = model._id;

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
			strSql.Append("delete from DBSTEELYARD_BEAN ");
			strSql.Append(" where _id=@_id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@_id", DbType.Int32,4)
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
			strSql.Append("delete from DBSTEELYARD_BEAN ");
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
		public Maticsoft.Model.DBSTEELYARD_BEANMODEL GetModel(int _id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select _id,NO,NAME,IP,TYPE,SYN_TIME,USER_NAME,PASSWORD,CREATE_TIME,CREATE_SN,CREATE_URL_IP,VERSION_CODE from DBSTEELYARD_BEAN ");
			strSql.Append(" where _id=@_id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@_id", DbType.Int32,4)
			};
			parameters[0].Value = _id;

			Maticsoft.Model.DBSTEELYARD_BEANMODEL model=new Maticsoft.Model.DBSTEELYARD_BEANMODEL();
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
		public Maticsoft.Model.DBSTEELYARD_BEANMODEL DataRowToModel(DataRow row)
		{
			Maticsoft.Model.DBSTEELYARD_BEANMODEL model=new Maticsoft.Model.DBSTEELYARD_BEANMODEL();
			if (row != null)
			{
				if(row["_id"]!=null && row["_id"].ToString()!="")
				{
					model._id=int.Parse(row["_id"].ToString());
				}
				if(row["NO"]!=null)
				{
					model.NO=row["NO"].ToString();
				}
				if(row["NAME"]!=null)
				{
					model.NAME=row["NAME"].ToString();
				}
				if(row["IP"]!=null)
				{
					model.IP=row["IP"].ToString();
				}
				if(row["TYPE"]!=null)
				{
					model.TYPE=row["TYPE"].ToString();
				}
				if(row["SYN_TIME"]!=null && row["SYN_TIME"].ToString()!="")
				{
					model.SYN_TIME=int.Parse(row["SYN_TIME"].ToString());
				}
				if(row["USER_NAME"]!=null)
				{
					model.USER_NAME=row["USER_NAME"].ToString();
				}
				if(row["PASSWORD"]!=null)
				{
					model.PASSWORD=row["PASSWORD"].ToString();
				}
				if(row["CREATE_TIME"]!=null && row["CREATE_TIME"].ToString()!="")
				{
					model.CREATE_TIME=int.Parse(row["CREATE_TIME"].ToString());
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
					model.VERSION_CODE=int.Parse(row["VERSION_CODE"].ToString());
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
			strSql.Append("select _id,NO,NAME,IP,TYPE,SYN_TIME,USER_NAME,PASSWORD,CREATE_TIME,CREATE_SN,CREATE_URL_IP,VERSION_CODE ");
			strSql.Append(" FROM DBSTEELYARD_BEAN ");
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
			strSql.Append("select count(1) FROM DBSTEELYARD_BEAN ");
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
			strSql.Append(")AS Row, T.*  from DBSTEELYARD_BEAN T ");
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
			parameters[0].Value = "DBSTEELYARD_BEAN";
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

		#endregion  ExtensionMethod
	}
}

