using System;
using System.Data;
using System.Text;
using System.Data.SQLite;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:DBTRANSFER_SCALE_STATUS_BEAN
	/// </summary>
	public partial class DBTRANSFER_SCALE_STATUS_BEANDAL
	{
		public DBTRANSFER_SCALE_STATUS_BEANDAL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQLite.GetMaxID("_id", "DBTRANSFER_SCALE_STATUS_BEAN"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Int64 _id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from DBTRANSFER_SCALE_STATUS_BEAN");
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
		public int Add(Maticsoft.Model.DBTRANSFER_SCALE_STATUS_BEANMODEL model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into DBTRANSFER_SCALE_STATUS_BEAN(");
			strSql.Append("SYS_SUCCESS_TIME,SYS_ERROR_TIME,STATUS,SCALEIP,CREATE_URL_IP)");
			strSql.Append(" values (");
			strSql.Append("@SYS_SUCCESS_TIME,@SYS_ERROR_TIME,@STATUS,@SCALEIP,@CREATE_URL_IP)");
			strSql.Append(";select LAST_INSERT_ROWID()");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@SYS_SUCCESS_TIME", DbType.Int64,8),
					new SQLiteParameter("@SYS_ERROR_TIME", DbType.Int64,8),
					new SQLiteParameter("@STATUS", DbType.Int64,8),
					new SQLiteParameter("@SCALEIP", DbType.String),
					new SQLiteParameter("@CREATE_URL_IP", DbType.String)};
			parameters[0].Value = model.SYS_SUCCESS_TIME;
			parameters[1].Value = model.SYS_ERROR_TIME;
			parameters[2].Value = model.STATUS;
			parameters[3].Value = model.SCALEIP;
			parameters[4].Value = model.CREATE_URL_IP;

			object obj = DbHelperSQLite.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt16(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.DBTRANSFER_SCALE_STATUS_BEANMODEL model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update DBTRANSFER_SCALE_STATUS_BEAN set ");
			strSql.Append("SYS_SUCCESS_TIME=@SYS_SUCCESS_TIME,");
			strSql.Append("SYS_ERROR_TIME=@SYS_ERROR_TIME,");
			strSql.Append("STATUS=@STATUS,");
			strSql.Append("SCALEIP=@SCALEIP,");
			strSql.Append("CREATE_URL_IP=@CREATE_URL_IP");
			strSql.Append(" where _id=@_id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@SYS_SUCCESS_TIME", DbType.Int64,8),
					new SQLiteParameter("@SYS_ERROR_TIME", DbType.Int64,8),
					new SQLiteParameter("@STATUS", DbType.Int64,8),
					new SQLiteParameter("@SCALEIP", DbType.String),
					new SQLiteParameter("@CREATE_URL_IP", DbType.String),
					new SQLiteParameter("@_id", DbType.Int64,8)};
			parameters[0].Value = model.SYS_SUCCESS_TIME;
			parameters[1].Value = model.SYS_ERROR_TIME;
			parameters[2].Value = model.STATUS;
			parameters[3].Value = model.SCALEIP;
			parameters[4].Value = model.CREATE_URL_IP;
			parameters[5].Value = model._id;

			Int64 rows=DbHelperSQLite.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Delete(Int64 _id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from DBTRANSFER_SCALE_STATUS_BEAN ");
			strSql.Append(" where _id=@_id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@_id", DbType.Int64,4)
			};
			parameters[0].Value = _id;

			Int64 rows=DbHelperSQLite.ExecuteSql(strSql.ToString(),parameters);
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
			strSql.Append("delete from DBTRANSFER_SCALE_STATUS_BEAN ");
			strSql.Append(" where _id in ("+_idlist + ")  ");
			Int64 rows=DbHelperSQLite.ExecuteSql(strSql.ToString());
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
		public Maticsoft.Model.DBTRANSFER_SCALE_STATUS_BEANMODEL GetModel(Int64 _id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select _id,SYS_SUCCESS_TIME,SYS_ERROR_TIME,STATUS,SCALEIP,CREATE_URL_IP from DBTRANSFER_SCALE_STATUS_BEAN ");
			strSql.Append(" where _id=@_id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@_id", DbType.Int64,4)
			};
			parameters[0].Value = _id;

			Maticsoft.Model.DBTRANSFER_SCALE_STATUS_BEANMODEL model=new Maticsoft.Model.DBTRANSFER_SCALE_STATUS_BEANMODEL();
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
		public Maticsoft.Model.DBTRANSFER_SCALE_STATUS_BEANMODEL DataRowToModel(DataRow row)
		{
			Maticsoft.Model.DBTRANSFER_SCALE_STATUS_BEANMODEL model=new Maticsoft.Model.DBTRANSFER_SCALE_STATUS_BEANMODEL();
			if (row != null)
			{
				if(row["_id"]!=null && row["_id"].ToString()!="")
				{
					model._id=Int64.Parse(row["_id"].ToString());
				}
				if(row["SYS_SUCCESS_TIME"]!=null && row["SYS_SUCCESS_TIME"].ToString()!="")
				{
					model.SYS_SUCCESS_TIME=Int64.Parse(row["SYS_SUCCESS_TIME"].ToString());
				}
				if(row["SYS_ERROR_TIME"]!=null && row["SYS_ERROR_TIME"].ToString()!="")
				{
					model.SYS_ERROR_TIME=Int64.Parse(row["SYS_ERROR_TIME"].ToString());
				}
				if(row["STATUS"]!=null && row["STATUS"].ToString()!="")
				{
					model.STATUS=Int64.Parse(row["STATUS"].ToString());
				}
				if(row["SCALEIP"]!=null)
				{
					model.SCALEIP=row["SCALEIP"].ToString();
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
			strSql.Append("select _id,SYS_SUCCESS_TIME,SYS_ERROR_TIME,STATUS,SCALEIP,CREATE_URL_IP ");
			strSql.Append(" FROM DBTRANSFER_SCALE_STATUS_BEAN ");
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
			strSql.Append("select count(1) FROM DBTRANSFER_SCALE_STATUS_BEAN ");
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
				return Convert.ToInt16(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, Int64 startIndex, Int64 endIndex)
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
			strSql.Append(")AS Row, T.*  from DBTRANSFER_SCALE_STATUS_BEAN T ");
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
		public DataSet GetList(Int64 PageSize,Int64 PageIndex,string strWhere)
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
			parameters[0].Value = "DBTRANSFER_SCALE_STATUS_BEAN";
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
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.DBTRANSFER_SCALE_STATUS_BEANMODEL GetModelByScaleIp(string scaleip)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select _id,SYS_SUCCESS_TIME,SYS_ERROR_TIME,STATUS,SCALEIP,CREATE_URL_IP from DBTRANSFER_SCALE_STATUS_BEAN ");
            strSql.Append(" where SCALEIP=@SCALEIP");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@SCALEIP", DbType.String)
			};
            parameters[0].Value = scaleip;

            Maticsoft.Model.DBTRANSFER_SCALE_STATUS_BEANMODEL model = new Maticsoft.Model.DBTRANSFER_SCALE_STATUS_BEANMODEL();
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
        /// 是否存在该记录
        /// </summary>
        public bool ExistsByScaleIp(string scaleip)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from DBTRANSFER_SCALE_STATUS_BEAN");
            strSql.Append(" where SCALEIP=@SCALEIP");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@SCALEIP", DbType.String)
			};
            parameters[0].Value = scaleip;

            return DbHelperSQLite.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateByScaleIp(Maticsoft.Model.DBTRANSFER_SCALE_STATUS_BEANMODEL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DBTRANSFER_SCALE_STATUS_BEAN set ");
            strSql.Append("SYS_SUCCESS_TIME=@SYS_SUCCESS_TIME,");
            strSql.Append("SYS_ERROR_TIME=@SYS_ERROR_TIME,");
            strSql.Append("STATUS=@STATUS,");
            strSql.Append("SCALEIP=@SCALEIP,");
            strSql.Append("CREATE_URL_IP=@CREATE_URL_IP");
            strSql.Append(" where SCALEIP=@SCALEIP");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@SYS_SUCCESS_TIME", DbType.Int64,8),
					new SQLiteParameter("@SYS_ERROR_TIME", DbType.Int64,8),
					new SQLiteParameter("@STATUS", DbType.Int64,8),
					new SQLiteParameter("@SCALEIP", DbType.String),
					new SQLiteParameter("@CREATE_URL_IP", DbType.String),
					new SQLiteParameter("@SCALEIP", DbType.String)};
            parameters[0].Value = model.SYS_SUCCESS_TIME;
            parameters[1].Value = model.SYS_ERROR_TIME;
            parameters[2].Value = model.STATUS;
            parameters[3].Value = model.SCALEIP;
            parameters[4].Value = model.CREATE_URL_IP;
            parameters[5].Value = model.SCALEIP;

            Int64 rows = DbHelperSQLite.ExecuteSql(strSql.ToString(), parameters);
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

