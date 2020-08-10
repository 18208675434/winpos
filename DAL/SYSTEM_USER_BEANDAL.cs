using System;
using System.Data;
using System.Text;
using System.Data.SQLite;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:SYSTEM_USER_BEAN
	/// </summary>
	public partial class SYSTEM_USER_BEANDAL
	{
		public SYSTEM_USER_BEANDAL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQLite.GetMaxID("_id", "SYSTEM_USER_BEAN"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int _id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SYSTEM_USER_BEAN");
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
		public int Add(Maticsoft.Model.SYSTEM_USER_BEANMODEL model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SYSTEM_USER_BEAN(");
			strSql.Append("LOGINACCOUNT,NICKNAME,CREATE_URL_IP)");
			strSql.Append(" values (");
			strSql.Append("@LOGINACCOUNT,@NICKNAME,@CREATE_URL_IP)");
			strSql.Append(";select LAST_INSERT_ROWID()");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@LOGINACCOUNT", DbType.String),
					new SQLiteParameter("@NICKNAME", DbType.String),
					new SQLiteParameter("@CREATE_URL_IP", DbType.String)};
			parameters[0].Value = model.LOGINACCOUNT;
			parameters[1].Value = model.NICKNAME;
			parameters[2].Value = model.CREATE_URL_IP;

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
		public bool Update(Maticsoft.Model.SYSTEM_USER_BEANMODEL model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SYSTEM_USER_BEAN set ");
			strSql.Append("LOGINACCOUNT=@LOGINACCOUNT,");
			strSql.Append("NICKNAME=@NICKNAME,");
			strSql.Append("CREATE_URL_IP=@CREATE_URL_IP");
			strSql.Append(" where _id=@_id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@LOGINACCOUNT", DbType.String),
					new SQLiteParameter("@NICKNAME", DbType.String),
					new SQLiteParameter("@CREATE_URL_IP", DbType.String),
					new SQLiteParameter("@_id", DbType.Int32,8)};
			parameters[0].Value = model.LOGINACCOUNT;
			parameters[1].Value = model.NICKNAME;
			parameters[2].Value = model.CREATE_URL_IP;
			parameters[3].Value = model._id;

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
			strSql.Append("delete from SYSTEM_USER_BEAN ");
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
			strSql.Append("delete from SYSTEM_USER_BEAN ");
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
		public Maticsoft.Model.SYSTEM_USER_BEANMODEL GetModel(int _id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select _id,LOGINACCOUNT,NICKNAME,CREATE_URL_IP from SYSTEM_USER_BEAN ");
			strSql.Append(" where _id=@_id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@_id", DbType.Int32,4)
			};
			parameters[0].Value = _id;

			Maticsoft.Model.SYSTEM_USER_BEANMODEL model=new Maticsoft.Model.SYSTEM_USER_BEANMODEL();
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
		public Maticsoft.Model.SYSTEM_USER_BEANMODEL DataRowToModel(DataRow row)
		{
			Maticsoft.Model.SYSTEM_USER_BEANMODEL model=new Maticsoft.Model.SYSTEM_USER_BEANMODEL();
			if (row != null)
			{
				if(row["_id"]!=null && row["_id"].ToString()!="")
				{
					model._id=int.Parse(row["_id"].ToString());
				}
				if(row["LOGINACCOUNT"]!=null)
				{
					model.LOGINACCOUNT=row["LOGINACCOUNT"].ToString();
				}
				if(row["NICKNAME"]!=null)
				{
					model.NICKNAME=row["NICKNAME"].ToString();
				}
				if(row["CREATE_URL_IP"]!=null)
				{
					model.CREATE_URL_IP=row["CREATE_URL_IP"].ToString();
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
			strSql.Append("select _id,LOGINACCOUNT,NICKNAME,CREATE_URL_IP ");
			strSql.Append(" FROM SYSTEM_USER_BEAN ");
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
			strSql.Append("select count(1) FROM SYSTEM_USER_BEAN ");
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
			strSql.Append(")AS Row, T.*  from SYSTEM_USER_BEAN T ");
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
			parameters[0].Value = "SYSTEM_USER_BEAN";
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
        /// 删除所有数据
        /// </summary>
        public bool DeleteAll()
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SYSTEM_USER_BEAN ");
            
          

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

