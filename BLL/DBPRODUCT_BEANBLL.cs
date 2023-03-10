using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Model;
namespace Maticsoft.BLL
{
	/// <summary>
	/// DBPRODUCT_BEAN
	/// </summary>
	public partial class DBPRODUCT_BEANBLL
	{
		private readonly Maticsoft.DAL.DBPRODUCT_BEANDAL dal=new Maticsoft.DAL.DBPRODUCT_BEANDAL();
		public DBPRODUCT_BEANBLL()
		{}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int _id)
        {
            return dal.Exists(_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.DBPRODUCT_BEANMODEL model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.DBPRODUCT_BEANMODEL model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int _id)
        {

            return dal.Delete(_id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string _idlist)
        {
            return dal.DeleteList(_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.DBPRODUCT_BEANMODEL GetModel(int _id)
        {

            return dal.GetModel(_id);
        }

    
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.DBPRODUCT_BEANMODEL> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.DBPRODUCT_BEANMODEL> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.DBPRODUCT_BEANMODEL> modelList = new List<Maticsoft.Model.DBPRODUCT_BEANMODEL>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.DBPRODUCT_BEANMODEL model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
		#region  ExtensionMethod

            /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsBySkuCode(string  skucode)
        {
            return dal.ExistsBySkuCode(skucode);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateBySkuCode(Maticsoft.Model.DBPRODUCT_BEANMODEL model)
        {
            return dal.UpdateBySkuCode(model);
        }

        public bool AddProduct(List<Maticsoft.Model.DBPRODUCT_BEANMODEL> lstmodel,string createurl)
        {
            return dal.AddProduct(lstmodel,createurl);

        }

        public SortedDictionary<string, string> GetDiatinctCategory(string strwhere)
        {
            return dal.GetDiatinctCategory(strwhere);
        }


        public DBPRODUCT_BEANMODEL GetModelBySkuCode(string skucode,string createurl)
        {
            return dal.GetModelBySkuCode(skucode,createurl);
        }


        public DBPRODUCT_BEANMODEL GetModelByGoodsID(string goodid,string creaturl)
        {
            return dal.GetModelByGoodsID(goodid, creaturl);
        }

        /// <summary>
        /// 执行指定的sql语句
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public bool ExecuteSql(string strSql)
        {
            return dal.ExecuteSql(strSql);
        }

		#endregion  ExtensionMethod
	}
}

