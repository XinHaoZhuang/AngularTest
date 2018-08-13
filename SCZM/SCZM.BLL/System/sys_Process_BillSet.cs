using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model;
namespace SCZM.BLL.System
{
    /// <summary>
    /// 业务逻辑类sys_Process_BillSet 的摘要说明。

    /// </summary>
    public partial class sys_Process_BillSet
    {
        private readonly SCZM.DAL.System.sys_Process_BillSet dal = new SCZM.DAL.System.sys_Process_BillSet();
        public sys_Process_BillSet()
        { }
        #region  基本方法
        /// <summary>
        /// 是否存在该记录

        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据

        /// </summary>
        public int Add(SCZM.Model.System.sys_Process_BillSet model, out string message)
        {
            message = "新增成功！";
            int rowId = dal.Add(model);
            if (rowId < 1)
            {
                message = "新增失败！";
            }
            return rowId;
        }

        /// <summary>
        /// 更新一条数据

        /// </summary>
        public bool Update(SCZM.Model.System.sys_Process_BillSet model, out string message)
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
        public bool Delete(int ID, out string message)
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
        public SCZM.Model.System.sys_Process_BillSet GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。

        /// </summary>
        public SCZM.Model.System.sys_Process_BillSet GetModelByCache(int ID)
        {

            string CacheKey = "sys_Process_BillSetModel-" + ID;
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
            return (SCZM.Model.System.sys_Process_BillSet)objModel;
        }

        /// <summary>
        /// 获得数据列表 通过SQL语句
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表 根据ID
        /// </summary>
        public DataSet GetList(int ID)
        {
            return dal.GetList(ID);
        }
        /// <summary>
        /// 获得数据列表 根据Para
        /// </summary>
        public DataSet GetList(string strWhere, List<SqlParameter> parameterList)
        {
            return dal.GetList(strWhere, parameterList);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SCZM.Model.System.sys_Process_BillSet> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SCZM.Model.System.sys_Process_BillSet> DataTableToList(DataTable dt)
        {
            List<SCZM.Model.System.sys_Process_BillSet> modelList = new List<SCZM.Model.System.sys_Process_BillSet>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SCZM.Model.System.sys_Process_BillSet model;
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
        /// 获取记录总数
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
        //public DataSet GetListByPage(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetListByPage(PageSize,PageIndex,strWhere);
        //}

        #endregion  基本方法
        #region  扩展方法
        /// <summary>
        /// 获得数据列表-带审批流信息 通过SQL语句
        /// </summary>
        public DataSet GetMxList(string strWhere)
        {
            return dal.GetMxList(strWhere);
        }
        /// <summary>
        /// 是否存在单据的审批流
        /// </summary>
        public bool ExistsBillProc(string billSign)
        {
            return dal.ExistsBillProc(billSign);
        }
        #endregion  扩展方法
    }
}

