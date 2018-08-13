using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model;
namespace SCZM.BLL.System
{
    /// <summary>
    /// sys_PersonPower
    /// </summary>
    public partial class sys_PersonPower
    {
        private readonly SCZM.DAL.System.sys_PersonPower dal = new SCZM.DAL.System.sys_PersonPower();
        public sys_PersonPower()
        { }
        #region  BasicMethod
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
        public int Add(SCZM.Model.System.sys_PersonPower model, out string message)
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
        public bool Update(SCZM.Model.System.sys_PersonPower model, out string message)
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
                string[] perIdArr = IDList.Split(',');
                foreach (string perId in perIdArr)
                {
                    SetPersonPowerCache(int.Parse(perId));
                }
                return true;
            }
        }

        /// <summary>
        /// 得到一个对象实体

        /// </summary>
        public SCZM.Model.System.sys_PersonPower GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public SCZM.Model.System.sys_PersonPower GetModelByCache(int ID)
        {

            string CacheKey = "sys_PersonPowerModel-" + ID;
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
            return (SCZM.Model.System.sys_PersonPower)objModel;
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
        public List<SCZM.Model.System.sys_PersonPower> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SCZM.Model.System.sys_PersonPower> DataTableToList(DataTable dt)
        {
            List<SCZM.Model.System.sys_PersonPower> modelList = new List<SCZM.Model.System.sys_PersonPower>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SCZM.Model.System.sys_PersonPower model;
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
        /// 获得人员权限设置数据列表
        /// </summary>
        public DataSet GetPerSettingList(string strWhere)
        {
            return dal.GetPerSettingList(strWhere);
        }
        /// <summary>
        /// 得到一个人员的所有权限列表（包括不具有的权限）

        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public DataSet GetPerPowerAllList(int perId)
        {
            return dal.GetPerPowerAllList(perId);
        }
        /// <summary>
        /// 更新一批数据

        /// </summary>
        public bool Update(List<SCZM.Model.System.sys_PersonPower> powerList, out string message)
        {
            message = "设置成功！";
            int rows = dal.Update(powerList);
            if (rows == 0)
            {
                message = "对不起，该条数据已被其他人删除！";
                return false;
            }
            else
            {
                SetPersonPowerCache(powerList[0].PerId);
                return true;
            }
        }
        /// <summary>
        /// 得到sys_PersonPower数据列表，从缓存中

        /// </summary>
        public DataSet GetListByCache_sys_PersonPower(int perId)
        {
            string CacheKey = "Cache_sys_PersonPower_" + perId;
            object objList = SCZM.Common.DataCache.GetCache(CacheKey);
            if (objList == null)
            {
                try
                {
                    objList = dal.GetPersonPowerList(perId);
                    if (objList != null)
                    {
                        int DataSetCache = SCZM.Common.ConfigHelper.GetConfigInt("DataSetCache");
                        SCZM.Common.DataCache.SetCache(CacheKey, objList, DateTime.Now.AddMinutes(DataSetCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (DataSet)objList;
        }
        /// <summary>
        /// 个人权限改变时，写入缓存
        /// </summary>
        private void SetPersonPowerCache(int perId)
        {
            string CacheKey = "Cache_sys_PersonPower_" + perId;
            object objList = SCZM.Common.DataCache.GetCache(CacheKey);
            if (objList != null)
            {
                try
                {
                    objList = dal.GetPersonPowerList(perId);
                    if (objList != null)
                    {
                        int DataSetCache = SCZM.Common.ConfigHelper.GetConfigInt("DataSetCache");
                        SCZM.Common.DataCache.SetCache(CacheKey, objList, DateTime.Now.AddMinutes(DataSetCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
        }
        
        #endregion  ExtensionMethod
    }
}

