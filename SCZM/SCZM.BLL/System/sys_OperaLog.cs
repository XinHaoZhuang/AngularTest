using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model;
namespace SCZM.BLL.System
{
    /// <summary>
    /// sys_OperaLog
    /// </summary>
    public partial class sys_OperaLog
    {
        private readonly SCZM.DAL.System.sys_OperaLog dal = new SCZM.DAL.System.sys_OperaLog();
        public sys_OperaLog()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录

        /// </summary>
        public bool Exists(long ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据

        /// </summary>
        public long Add(SCZM.Model.System.sys_OperaLog model)
        {
            return  dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据

        /// </summary>
        public bool Update(SCZM.Model.System.sys_OperaLog model, out string message)
        {
            message = "修改成功！";
            int rows = dal.Update(model);
            if (rows == 0)
            {
                message = "对不起，该条数据已被其他人删除！";
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 删除一条数据

        /// </summary>
        public bool Delete(long ID, out string message)
        {
            message = "删除成功！";

            int rows = dal.Delete(ID);
            if (rows == 0)
            {
                message = "对不起，该条数据已被其他人删除！";
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDList, out string message)
        {
            message = "删除成功！";

            int rows = dal.DeleteList(IDList);
            if (rows == 0)
            {
                message = "对不起，所选数据已被其他人删除！";
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 得到一个对象实体

        /// </summary>
        public SCZM.Model.System.sys_OperaLog GetModel(long ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public SCZM.Model.System.sys_OperaLog GetModelByCache(long ID)
        {

            string CacheKey = "sys_OperaLogModel-" + ID;
            object objModel = SCZM.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ID);
                    if (objModel != null)
                    {
                        int ModelCache = SCZM.Common.ConfigHelper.GetConfigInt("ModelCache");
                        SCZM.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (SCZM.Model.System.sys_OperaLog)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据

        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SCZM.Model.System.sys_OperaLog> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SCZM.Model.System.sys_OperaLog> DataTableToList(DataTable dt)
        {
            List<SCZM.Model.System.sys_OperaLog> modelList = new List<SCZM.Model.System.sys_OperaLog>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SCZM.Model.System.sys_OperaLog model;
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
        /// 获得带模块名的数据列表

        /// </summary>
        public DataSet GetList_Menu(string strWhere, List<SqlParameter> parameterList)
        {
            return dal.GetList_Menu(strWhere, parameterList);
        }
        /// <summary>
        /// 根据筛选条件批量删除数据

        /// </summary>
        public bool DeleteListByFilter(string strWhere, List<SqlParameter> parameterList, out string message)
        {
            message = "删除成功！";
            int rows = dal.DeleteListByFilter(strWhere, parameterList);
            if (rows == 0)
            {
                message = "对不起，没有符合条件的数据！";
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion  ExtensionMethod
    }
}

