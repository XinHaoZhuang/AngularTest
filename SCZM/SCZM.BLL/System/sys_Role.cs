using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model;
namespace SCZM.BLL.System
{
    /// <summary>
    /// 业务逻辑类sys_Role 的摘要说明。

    /// </summary>
    public partial class sys_Role
    {
        private readonly SCZM.DAL.System.sys_Role dal = new SCZM.DAL.System.sys_Role();
        public sys_Role()
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
        public int Add(SCZM.Model.System.sys_Role model, out string message)
        {
            message = "新增成功！";
            if (dal.ExistsRoleName(model.RoleName))
            {
                message = "对不起，该角色名称已存在！";
                return 0;
            }
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
        public bool Update(SCZM.Model.System.sys_Role model, out string message)
        {
            message = "修改成功！";
            string oldRolename = dal.GetModel(model.ID).RoleName;
            if (oldRolename != model.RoleName)
            {
                if (dal.ExistsRoleName(model.RoleName))
                {
                    message = "对不起，该角色名称已存在！";
                    return false;
                }
            }
            int rows = dal.Update(model);
            if (rows == 0)
            {
                message = "对不起，该条数据已被其他人删除！";
                return false;
            }
            else
            {
                if (oldRolename != model.RoleName)
                {
                    if (dal.UpdatePerRoleName(oldRolename, model.RoleName) == false)
                    {
                        message = "修改成功,但更新人员角色名称失败！";
                        return true;
                    }
                }
                SetRolePowerCache(model.ID);
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
            string tempStr = "";
            DataTable dt = dal.GetExistsPerList(IDList).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (tempStr == "")
                    {
                        tempStr = dt.Rows[i]["RoleName"].ToString();
                    }
                    else
                    {
                        tempStr += "、" + dt.Rows[i]["RoleName"].ToString();
                    }
                }
                message = "对不起，角色（" + tempStr + "）已被使用，不能删除！！";
                return false;
            }

            int rows = dal.DeleteList(IDList);
            if (rows == 0)
            {
                message = "对不起，所选数据已被其他人删除！";
                return false;
            }
            else
            {
                string[] roleIdArr = IDList.Split(',');
                foreach (string roleId in roleIdArr)
                {
                    SetRolePowerCache(int.Parse(roleId));
                }
                return true;
            }
        }

        /// <summary>
        /// 得到一个对象实体

        /// </summary>
        public SCZM.Model.System.sys_Role GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。

        /// </summary>
        public SCZM.Model.System.sys_Role GetModelByCache(int ID)
        {

            string CacheKey = "sys_RoleModel-" + ID;
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
            return (SCZM.Model.System.sys_Role)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表  根据ID
        /// </summary>
        public DataSet GetList(int ID)
        {
            return dal.GetList(ID);
        }
        /// <summary>
        /// 获得数据列表  根据Para
        /// </summary>
        public DataSet GetList(string strWhere, List<SqlParameter> parameterList)
        {
            return dal.GetList(strWhere, parameterList);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SCZM.Model.System.sys_Role> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SCZM.Model.System.sys_Role> DataTableToList(DataTable dt)
        {
            List<SCZM.Model.System.sys_Role> modelList = new List<SCZM.Model.System.sys_Role>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SCZM.Model.System.sys_Role model;
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

        ///// <summary>
        ///// 获取记录总数
        ///// </summary>
        //public int GetRecordCount(string strWhere)
        //{
        //    return dal.GetRecordCount(strWhere);
        //}
        ///// <summary>
        ///// 分页获取数据列表
        ///// </summary>
        //public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        //{
        //    return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        //}
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
        /// 得到存在人员的角色列表

        /// </summary>
        /// <param name="IDlist"></param>
        /// <returns></returns>
        public DataSet GetExistsPerList(string IDlist)
        {
            return dal.GetExistsPerList(IDlist);
        }
        /// <summary>
        /// 获得数据列表 通过SQL语句
        /// </summary>
        public DataSet GetListAll(string strWhere)
        {
            return dal.GetListAll(strWhere);
        }
        /// <summary>
        /// 得到一个角色的所有权限列表（包括不具有的权限）

        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public DataSet GetRolePowerAllList(int roleId)
        {
            return dal.GetRolePowerAllList(roleId);
        }
        /// <summary>
        /// 更改人员表角色名称

        /// </summary>
        /// <param name="oldRoleName"></param>
        /// <param name="newRoleName"></param>
        /// <returns></returns>
        public bool UpdatePerRoleName(string oldRoleName, string newRoleName)
        {
            return dal.UpdatePerRoleName(oldRoleName, newRoleName);
        }

        /// <summary>
        /// 得到sys_RolePower数据列表，从缓存中

        /// </summary>
        public DataSet GetListByCache_sys_RolePower(int roleId)
        {
            string CacheKey = "Cache_sys_RolePower_" + roleId;
            object objList = SCZM.Common.DataCache.GetCache(CacheKey);
            if (objList == null)
            {
                try
                {
                    objList = dal.GetRolePowerList(roleId);
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
        /// 角色权限改变时，写入缓存
        /// </summary>
        private void SetRolePowerCache(int roleId)
        {
            string CacheKey = "Cache_sys_RolePower_" + roleId;
            object objList = SCZM.Common.DataCache.GetCache(CacheKey);
            if (objList != null)
            {
                try
                {
                    objList = dal.GetRolePowerList(roleId);
                    if (objList != null)
                    {
                        int DataSetCache = SCZM.Common.ConfigHelper.GetConfigInt("DataSetCache");
                        SCZM.Common.DataCache.SetCache(CacheKey, objList, DateTime.Now.AddMinutes(DataSetCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
        }
        #endregion  扩展方法
    }
}

