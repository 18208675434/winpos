using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Maticsoft.BLL
{
    /// <summary>
    /// DBSCALE_KEY_BEANBLL
    /// </summary>
    public partial class DBSCALE_KEY_BEANBLL
    {
        private readonly Maticsoft.DAL.DBSCALE_KEY_BEANDAL dal = new Maticsoft.DAL.DBSCALE_KEY_BEANDAL();
        public DBSCALE_KEY_BEANBLL()
        { }
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
        public int Add(Maticsoft.Model.DBSCALE_KEY_BEANMODEL model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.DBSCALE_KEY_BEANMODEL model)
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
        public Maticsoft.Model.DBSCALE_KEY_BEANMODEL GetModel(int _id)
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
        public List<Maticsoft.Model.DBSCALE_KEY_BEANMODEL> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.DBSCALE_KEY_BEANMODEL> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.DBSCALE_KEY_BEANMODEL> modelList = new List<Maticsoft.Model.DBSCALE_KEY_BEANMODEL>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.DBSCALE_KEY_BEANMODEL model;
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

        public bool AddScalse(List<Maticsoft.Model.DBSCALE_KEY_BEANMODEL> lstmodel, string createurl, string shopid)
        {
            return dal.AddScales(lstmodel, createurl, shopid);

        }


        public List<string> GetDiatinctByScaleIP(string strwhere)
        {
            return dal.GetDiatinctByScaleIP(strwhere);
        }

        #endregion  ExtensionMethod
    }
}
