using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model;
namespace SCZM.BLL.System
{
    /// <summary>
    /// 业务逻辑类sys_Department 的摘要说明。

    /// </summary>
    public partial class sys_Department
    {
        private readonly SCZM.DAL.System.sys_Department dal = new SCZM.DAL.System.sys_Department();
        public sys_Department()
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
        public int Add(SCZM.Model.System.sys_Department model, out string message)
        {
            message = "新增成功！";
            int rowId = dal.Add(model);
            if (rowId < 1)
            {
                message = "新增失败！";
            }
            SetDepartmentCache();
            return rowId;
        }

        /// <summary>
        /// 更新一条数据

        /// </summary>
        public bool Update(SCZM.Model.System.sys_Department model, out string message)
        {
            //----------------------------检查新上级是否为自己或子部门-------第一位是0 所以index最小是1---2018/3/19------------------
            if (model.SupList.IndexOf("," + model.ID + ",") > 0 ? false : true)
            {
                message = "修改成功！";
                //----------------------------检查Suplist是否变动-----------2018/3/19-----------------
                bool Result = dal.Exists(model.ID, model.SupList);
                //------------------------------------------------------------------------------------
                int rows = dal.Update(model);
                if (rows == 0)
                {
                    message = "对不起，该条数据已被其他人删除！";
                    return false;
                }
                else
                {
                    //-----------------------针对变更后子部门上级列表无法随父部门变更的修复--2018/3/8---zhx------------------
                    //所需 ID：model.ID SupListNew:model.SupList
                    if (!Result)
                    {
                        dal.UpdateChildDepartment(model.ID, model.SupList);
                    }
                    //-------------------------------------------------------------------------------------------------------
                    SetDepartmentCache();
                    return true;
                }
                //------------------------------------------------------------------------------------------------------------
            }
            else {
                message = "对不起，不能变更到当前下属单位内！";
                return false;
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
            DataTable dt = dal.GetExistsSubList(IDList).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (tempStr == "")
                    {
                        tempStr = dt.Rows[i]["DepName"].ToString();
                    }
                    else
                    {
                        tempStr += "、" + dt.Rows[i]["DepName"].ToString();
                    }
                }
                message = tempStr + "存在下级部门，不能删除！";
                return false;
            }
            tempStr = "";
            dt = null;
            dt = dal.GetExistsPerList(IDList).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (tempStr == "")
                    {
                        tempStr = dt.Rows[i]["DepName"].ToString();
                    }
                    else
                    {
                        tempStr += "、" + dt.Rows[i]["DepName"].ToString();
                    }
                }
                message = tempStr + "已设置人员，不能删除！";
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
                SetDepartmentCache();
                return true;
            }
        }

        /// <summary>
        /// 得到一个对象实体

        /// </summary>
        public SCZM.Model.System.sys_Department GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。

        /// </summary>
        public SCZM.Model.System.sys_Department GetModelByCache(int ID)
        {

            string CacheKey = "sys_DepartmentModel-" + ID;
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
            return (SCZM.Model.System.sys_Department)objModel;
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
        public List<SCZM.Model.System.sys_Department> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SCZM.Model.System.sys_Department> DataTableToList(DataTable dt)
        {
            List<SCZM.Model.System.sys_Department> modelList = new List<SCZM.Model.System.sys_Department>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SCZM.Model.System.sys_Department model;
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
        /// 得到部门类别列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetDepTypeList()
        {
            return dal.GetDepTypeList();
        }
        /// <summary>
        /// 得到部门控制权限列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetCtrlDepList(int DepId)
        {
            return dal.GetCtrlDepList(DepId);
        }
        /// <summary>
        /// 得到sys_Department数据列表，从缓存中

        /// </summary>
        public DataSet GetListByCache_sys_Department()
        {
            string CacheKey = "Cache_sys_Department";
            object objList = SCZM.Common.DataCache.GetCache(CacheKey);
            //阻止从缓存中读取数据，获取数据库中最新改动
            //objList=null;
            //-----------------------------
            if (objList == null)
            {
                try
                {
                    objList = dal.GetListForCommon();
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
        /// 信息改变时，写入缓存
        /// </summary>
        private void SetDepartmentCache()
        {
            string CacheKey = "Cache_sys_Department";
            object objList = SCZM.Common.DataCache.GetCache(CacheKey);
            if (objList != null)
            {
                try
                {
                    objList = dal.GetListForCommon();
                    if (objList != null)
                    {
                        int DataSetCache = SCZM.Common.ConfigHelper.GetConfigInt("DataSetCache");
                        SCZM.Common.DataCache.SetCache(CacheKey, objList, DateTime.Now.AddMinutes(DataSetCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
        }
        /// <summary>
        /// 获得登录人控制的专卖店列表

        /// </summary>
        public DataSet GetShopListByLogin(int perId)
        {
            return dal.GetShopListByLogin(perId);
        }
        /// <summary>
        /// 获得登录人控制的专卖店列表 包含分中心


        /// </summary>
        public DataSet GetCentShopListByLogin(int perId, string strWhere)
        {
            return dal.GetCentShopListByLogin(perId, strWhere);
        }
        /// <summary>
        /// 获得登录人控制的分中心列表

        /// </summary>
        public DataSet GetCentListByLogin(int perId)
        {
            return dal.GetCentListByLogin(perId);
        }
        /// <summary>
        /// 获得部门ID 根据前端编码
        /// </summary>
        public int GetDepIdByQDCode(string qdCode)
        {
            return dal.GetDepIdByQDCode(qdCode);
        }
        /// <summary>
        /// 获得专卖店、分中心信息
        /// </summary>
        public DataSet GetShopInfo(int depId)
        {
            return dal.GetShopInfo(depId);
        }
        #endregion  扩展方法
    }
}

