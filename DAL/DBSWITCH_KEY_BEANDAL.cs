using System;
using System.Data;
using System.Text;
using System.Data.SQLite;
using Maticsoft.DBUtility;
using System.Collections.Generic;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:DBSWITCH_KEY_BEAN
	/// </summary>
	public partial class DBSWITCH_KEY_BEANDAL
	{
		public DBSWITCH_KEY_BEANDAL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQLite.GetMaxID("_id", "DBSWITCH_KEY_BEAN"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int _id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from DBSWITCH_KEY_BEAN");
			strSql.Append(" where _id=+model._id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("+model._id", DbType.Int32,4)
			};
			parameters[0].Value = _id;

			return DbHelperSQLite.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.DBSWITCH_KEY_BEANMODEL model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into DBSWITCH_KEY_BEAN(");
			strSql.Append("SHOPID,SKUCODE,SKUNAME,SCALEIP,SCALETYPE,SCALETYPENAME,KEYPLANNAME,KEYNO,PNO,YNO,XNO,SYN_TIME,ERROR_TIME,STATUS,CREATE_URL_IP,CREATE_TIME)");
			strSql.Append(" values (");
			strSql.Append("+model.SHOPID,+model.SKUCODE,+model.SKUNAME,+model.SCALEIP,+model.SCALETYPE,+model.SCALETYPENAME,+model.KEYPLANNAME,+model.KEYNO,+model.PNO,+model.YNO,+model.XNO,+model.SYN_TIME,+model.ERROR_TIME,+model.STATUS,+model.CREATE_URL_IP,+model.CREATE_TIME)");
			strSql.Append(";select LAST_INSERT_ROWID()");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("+model.SHOPID", DbType.String),
					new SQLiteParameter("+model.SKUCODE", DbType.String),
					new SQLiteParameter("+model.SKUNAME", DbType.String),
					new SQLiteParameter("+model.SCALEIP", DbType.String),
					new SQLiteParameter("+model.SCALETYPE", DbType.String),
					new SQLiteParameter("+model.SCALETYPENAME", DbType.String),
					new SQLiteParameter("+model.KEYPLANNAME", DbType.String),
					new SQLiteParameter("+model.KEYNO", DbType.String),
					new SQLiteParameter("+model.PNO", DbType.String),
					new SQLiteParameter("+model.YNO", DbType.String),
					new SQLiteParameter("+model.XNO", DbType.String),
					new SQLiteParameter("+model.SYN_TIME", DbType.Int32,8),
					new SQLiteParameter("+model.ERROR_TIME", DbType.Int32,8),
					new SQLiteParameter("+model.STATUS", DbType.Int32,8),
					new SQLiteParameter("+model.CREATE_URL_IP", DbType.String),
					new SQLiteParameter("+model.CREATE_TIME", DbType.Int32,8)};
			parameters[0].Value = model.SHOPID;
			parameters[1].Value = model.SKUCODE;
			parameters[2].Value = model.SKUNAME;
			parameters[3].Value = model.SCALEIP;
			parameters[4].Value = model.SCALETYPE;
			parameters[5].Value = model.SCALETYPENAME;
			parameters[6].Value = model.KEYPLANNAME;
			parameters[7].Value = model.KEYNO;
			parameters[8].Value = model.PNO;
			parameters[9].Value = model.YNO;
			parameters[10].Value = model.XNO;
			parameters[11].Value = model.SYN_TIME;
			parameters[12].Value = model.ERROR_TIME;
			parameters[13].Value = model.STATUS;
			parameters[14].Value = model.CREATE_URL_IP;
			parameters[15].Value = model.CREATE_TIME;

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
		public bool Update(Maticsoft.Model.DBSWITCH_KEY_BEANMODEL model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update DBSWITCH_KEY_BEAN set ");
			strSql.Append("SHOPID=+model.SHOPID,");
			strSql.Append("SKUCODE=+model.SKUCODE,");
			strSql.Append("SKUNAME=+model.SKUNAME,");
			strSql.Append("SCALEIP=+model.SCALEIP,");
			strSql.Append("SCALETYPE=+model.SCALETYPE,");
			strSql.Append("SCALETYPENAME=+model.SCALETYPENAME,");
			strSql.Append("KEYPLANNAME=+model.KEYPLANNAME,");
			strSql.Append("KEYNO=+model.KEYNO,");
			strSql.Append("PNO=+model.PNO,");
			strSql.Append("YNO=+model.YNO,");
			strSql.Append("XNO=+model.XNO,");
			strSql.Append("SYN_TIME=+model.SYN_TIME,");
			strSql.Append("ERROR_TIME=+model.ERROR_TIME,");
			strSql.Append("STATUS=+model.STATUS,");
			strSql.Append("CREATE_URL_IP=+model.CREATE_URL_IP,");
			strSql.Append("CREATE_TIME=+model.CREATE_TIME");
			strSql.Append(" where _id=+model._id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("+model.SHOPID", DbType.String),
					new SQLiteParameter("+model.SKUCODE", DbType.String),
					new SQLiteParameter("+model.SKUNAME", DbType.String),
					new SQLiteParameter("+model.SCALEIP", DbType.String),
					new SQLiteParameter("+model.SCALETYPE", DbType.String),
					new SQLiteParameter("+model.SCALETYPENAME", DbType.String),
					new SQLiteParameter("+model.KEYPLANNAME", DbType.String),
					new SQLiteParameter("+model.KEYNO", DbType.String),
					new SQLiteParameter("+model.PNO", DbType.String),
					new SQLiteParameter("+model.YNO", DbType.String),
					new SQLiteParameter("+model.XNO", DbType.String),
					new SQLiteParameter("+model.SYN_TIME", DbType.Int32,8),
					new SQLiteParameter("+model.ERROR_TIME", DbType.Int32,8),
					new SQLiteParameter("+model.STATUS", DbType.Int32,8),
					new SQLiteParameter("+model.CREATE_URL_IP", DbType.String),
					new SQLiteParameter("+model.CREATE_TIME", DbType.Int32,8),
					new SQLiteParameter("+model._id", DbType.Int32,8)};
			parameters[0].Value = model.SHOPID;
			parameters[1].Value = model.SKUCODE;
			parameters[2].Value = model.SKUNAME;
			parameters[3].Value = model.SCALEIP;
			parameters[4].Value = model.SCALETYPE;
			parameters[5].Value = model.SCALETYPENAME;
			parameters[6].Value = model.KEYPLANNAME;
			parameters[7].Value = model.KEYNO;
			parameters[8].Value = model.PNO;
			parameters[9].Value = model.YNO;
			parameters[10].Value = model.XNO;
			parameters[11].Value = model.SYN_TIME;
			parameters[12].Value = model.ERROR_TIME;
			parameters[13].Value = model.STATUS;
			parameters[14].Value = model.CREATE_URL_IP;
			parameters[15].Value = model.CREATE_TIME;
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
			strSql.Append("delete from DBSWITCH_KEY_BEAN ");
			strSql.Append(" where _id=+model._id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("+model._id", DbType.Int32,4)
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
			strSql.Append("delete from DBSWITCH_KEY_BEAN ");
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
		public Maticsoft.Model.DBSWITCH_KEY_BEANMODEL GetModel(int _id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select _id,SHOPID,SKUCODE,SKUNAME,SCALEIP,SCALETYPE,SCALETYPENAME,KEYPLANNAME,KEYNO,PNO,YNO,XNO,SYN_TIME,ERROR_TIME,STATUS,CREATE_URL_IP,CREATE_TIME from DBSWITCH_KEY_BEAN ");
			strSql.Append(" where _id=+model._id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("+model._id", DbType.Int32,4)
			};
			parameters[0].Value = _id;

			Maticsoft.Model.DBSWITCH_KEY_BEANMODEL model=new Maticsoft.Model.DBSWITCH_KEY_BEANMODEL();
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
		public Maticsoft.Model.DBSWITCH_KEY_BEANMODEL DataRowToModel(DataRow row)
		{
			Maticsoft.Model.DBSWITCH_KEY_BEANMODEL model=new Maticsoft.Model.DBSWITCH_KEY_BEANMODEL();
			if (row != null)
			{
				if(row["_id"]!=null && row["_id"].ToString()!="")
				{
					model._id=Int64.Parse(row["_id"].ToString());
				}
				if(row["SHOPID"]!=null)
				{
					model.SHOPID=row["SHOPID"].ToString();
				}
				if(row["SKUCODE"]!=null)
				{
					model.SKUCODE=row["SKUCODE"].ToString();
				}
				if(row["SKUNAME"]!=null)
				{
					model.SKUNAME=row["SKUNAME"].ToString();
				}
				if(row["SCALEIP"]!=null)
				{
					model.SCALEIP=row["SCALEIP"].ToString();
				}
				if(row["SCALETYPE"]!=null)
				{
					model.SCALETYPE=row["SCALETYPE"].ToString();
				}
				if(row["SCALETYPENAME"]!=null)
				{
					model.SCALETYPENAME=row["SCALETYPENAME"].ToString();
				}
				if(row["KEYPLANNAME"]!=null)
				{
					model.KEYPLANNAME=row["KEYPLANNAME"].ToString();
				}
				if(row["KEYNO"]!=null)
				{
					model.KEYNO=row["KEYNO"].ToString();
				}
				if(row["PNO"]!=null)
				{
					model.PNO=row["PNO"].ToString();
				}
				if(row["YNO"]!=null)
				{
					model.YNO=row["YNO"].ToString();
				}
				if(row["XNO"]!=null)
				{
					model.XNO=row["XNO"].ToString();
				}
				if(row["SYN_TIME"]!=null && row["SYN_TIME"].ToString()!="")
				{
					model.SYN_TIME=int.Parse(row["SYN_TIME"].ToString());
				}
				if(row["ERROR_TIME"]!=null && row["ERROR_TIME"].ToString()!="")
				{
					model.ERROR_TIME=int.Parse(row["ERROR_TIME"].ToString());
				}
				if(row["STATUS"]!=null && row["STATUS"].ToString()!="")
				{
					model.STATUS=int.Parse(row["STATUS"].ToString());
				}
				if(row["CREATE_URL_IP"]!=null)
				{
					model.CREATE_URL_IP=row["CREATE_URL_IP"].ToString();
				}
				if(row["CREATE_TIME"]!=null && row["CREATE_TIME"].ToString()!="")
				{
					model.CREATE_TIME=int.Parse(row["CREATE_TIME"].ToString());
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
			strSql.Append("select _id,SHOPID,SKUCODE,SKUNAME,SCALEIP,SCALETYPE,SCALETYPENAME,KEYPLANNAME,KEYNO,PNO,YNO,XNO,SYN_TIME,ERROR_TIME,STATUS,CREATE_URL_IP,CREATE_TIME ");
			strSql.Append(" FROM DBSWITCH_KEY_BEAN ");
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
			strSql.Append("select count(1) FROM DBSWITCH_KEY_BEAN ");
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
			strSql.Append(")AS Row, T.*  from DBSWITCH_KEY_BEAN T ");
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
					new SQLiteParameter("+model.tblName", DbType.VarChar, 255),
					new SQLiteParameter("+model.fldName", DbType.VarChar, 255),
					new SQLiteParameter("+model.PageSize", DbType.Int32),
					new SQLiteParameter("+model.PageIndex", DbType.Int32),
					new SQLiteParameter("+model.IsReCount", DbType.bit),
					new SQLiteParameter("+model.OrderType", DbType.bit),
					new SQLiteParameter("+model.strWhere", DbType.VarChar,1000),
					};
			parameters[0].Value = "DBSWITCH_KEY_BEAN";
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


        public bool AddScales(System.Collections.Generic.List<Maticsoft.Model.DBSWITCH_KEY_BEANMODEL> lstmodel,string createurl)
        {

            System.Collections.ArrayList lststring = new System.Collections.ArrayList();

           // lststring.Add("delete from DBSWITCH_KEY_BEAN");
            foreach (Maticsoft.Model.DBSWITCH_KEY_BEANMODEL model in lstmodel)
            {
                //根据skucode  删除后增加 ，有的话就更新 没有新增
                string strdel = "delete from DBSWITCH_KEY_BEAN where SKUCODE='" + model.SKUCODE + "'";
                StringBuilder strSqladd = new StringBuilder();

                strSqladd.Append("insert into DBSWITCH_KEY_BEAN(");
                strSqladd.Append("SHOPID,SKUCODE,SKUNAME,SCALEIP,SCALETYPE,SCALETYPENAME,KEYPLANNAME,KEYNO,PNO,YNO,XNO,SYN_TIME,ERROR_TIME,STATUS,CREATE_URL_IP,CREATE_TIME)");
                strSqladd.Append(" values (");
                strSqladd.Append("'"+model.SHOPID+"','"+model.SKUCODE+"','"+model.SKUNAME+"','"+model.SCALEIP+"','"+model.SCALETYPE+"','"+model.SCALETYPENAME+"','"+model.KEYPLANNAME+"','"+model.KEYNO+"','"+model.PNO+"','"+model.YNO+"','"+model.XNO+"',"+model.SYN_TIME+","+model.ERROR_TIME+","+model.STATUS+",'"+createurl+"',"+model.CREATE_TIME+")");
                lststring.Add(strdel.ToString());
                lststring.Add(strSqladd.ToString());
            }

            DbHelperSQLite.ExecuteSqlTran(lststring);

            return true;

        }



        public List<string> GetDiatinctByScaleIP(string strwhere)
        {
            List<string> sort = new List<string>();
            string cmdsql = "select distinct SCALEIP from DBSWITCH_KEY_BEAN";
            if (!string.IsNullOrEmpty(strwhere))
            {
                cmdsql += " where " + strwhere;
            }
            DataSet ds = DbHelperSQLite.Query(cmdsql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sort.Add(dr["SCALEIP"].ToString());
                }
            }
            return sort;
        }


		#endregion  ExtensionMethod
	}
}

