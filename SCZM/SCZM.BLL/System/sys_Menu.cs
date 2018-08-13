using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model;
namespace SCZM.BLL.System
{
    /// <summary>
    /// sys_Menu
    /// </summary>
    public partial class sys_Menu
    {
        private readonly SCZM.DAL.System.sys_Menu dal = new SCZM.DAL.System.sys_Menu();
        public sys_Menu()
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
        public int Add(SCZM.Model.System.sys_Menu model, string pageStr1, string pageStr2, string pageStr3, string pageStr4, string pageStr5, string pageStr6, string pageStr7, string pageStr8, out string message)
        {
            message = "新增成功！";
            int rowId= dal.Add(model);
            if (rowId > 0)
            {
                DataTable dt = dal.SetMenuPower(rowId, model.PowerList, pageStr1, pageStr2, pageStr3, pageStr4, pageStr5, pageStr6, pageStr7, pageStr8).Tables[0];
                if (Utils.ObjToInt(dt.Rows[0][0], 0) == 0)
                {
                    message = "菜单新增成功，但权限添加失败!";
                }
                SetMenuCache();
            }
            else
            {
                message = "新增失败！";
            }
            return rowId;
        }

        /// <summary>
        /// 更新一条数据

        /// </summary>
        public bool Update(SCZM.Model.System.sys_Menu model, string pageStr1, string pageStr2, string pageStr3, string pageStr4, string pageStr5, string pageStr6, string pageStr7, string pageStr8, out string message)
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
                DataTable dt = dal.SetMenuPower(model.ID, model.PowerList, pageStr1, pageStr2, pageStr3, pageStr4, pageStr5, pageStr6, pageStr7, pageStr8).Tables[0];
                if (Utils.ObjToInt(dt.Rows[0][0], 0) == 0)
                {
                    message = "菜单修改成功，但权限添加失败!";
                }
                SetMenuCache();
                
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
                SetMenuCache();
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
                        tempStr = dt.Rows[i]["MenuName"].ToString();
                    }
                    else
                    {
                        tempStr += "、" + dt.Rows[i]["MenuName"].ToString();
                    }
                }
                message="菜单（" + tempStr + "）存在下级菜单，不能删除！";
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
                SetMenuCache();
                return true;
            }
        }

        /// <summary>
        /// 得到一个对象实体

        /// </summary>
        public SCZM.Model.System.sys_Menu GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public SCZM.Model.System.sys_Menu GetModelByCache(int ID)
        {

            string CacheKey = "sys_MenuModel-" + ID;
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
            return (SCZM.Model.System.sys_Menu)objModel;
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
        public List<SCZM.Model.System.sys_Menu> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SCZM.Model.System.sys_Menu> DataTableToList(DataTable dt)
        {
            List<SCZM.Model.System.sys_Menu> modelList = new List<SCZM.Model.System.sys_Menu>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SCZM.Model.System.sys_Menu model;
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
        /// 设置菜单权限
        /// </summary>
        public DataSet SetMenuPower(int menuId, string powerNameStr, string pageStr1, string pageStr2, string pageStr3, string pageStr4, string pageStr5, string pageStr6, string pageStr7, string pageStr8)
        {
            return dal.SetMenuPower(menuId, powerNameStr, pageStr1, pageStr2, pageStr3, pageStr4, pageStr5, pageStr6, pageStr7, pageStr8);
        }

        /// <summary>
        /// 得到存在下级的菜单列表

        /// </summary>
        /// <param name="IDlist"></param>
        /// <returns></returns>
        public DataSet GetExistsSubList(string IDlist)
        {
            return dal.GetExistsSubList(IDlist);
        }
        /// <summary>
        /// 得到菜单的注册页面列表

        /// </summary>
        /// <param name="IDlist"></param>
        /// <returns></returns>
        public DataSet GetPageList(int menuId)
        {
            return dal.GetPageList(menuId);
        }
        /// <summary>
        /// 得到没有权限的按钮列表

        /// </summary>
        /// <param name="perId">人员ID</param>
        /// <returns></returns>
        public DataSet GetNoPowerBtn(int perId)
        {
            return dal.GetNoPowerBtn(perId);
        }
        /// <summary>
        /// 获得待审核单据列表

        /// </summary>
        public DataSet GetTodoList(int perId)
        {
            return dal.GetTodoList(perId);
        }
        /// <summary>
        /// 获得未完成单据列表

        /// </summary>
        public DataSet GetNodoList(int perId)
        {
            return dal.GetNodoList(perId);
        }
        #region 权限判断使用
        /// <summary>
        /// 得到sys_MenuPage数据列表，从缓存中

        /// </summary>
        public DataSet GetListByCache_sys_MenuPage()
        {
            string CacheKey = "Cache_sys_MenuPage";
            object objList = SCZM.Common.DataCache.GetCache(CacheKey);
            if (objList == null)
            {
                try
                {
                    objList = dal.GetMenuPageList();
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
        /// 得到sys_MenuPageElement数据列表，从缓存中

        /// </summary>
        public DataSet GetListByCache_sys_MenuPageElement()
        {
            string CacheKey = "Cache_sys_MenuPageElement";
            object objList = SCZM.Common.DataCache.GetCache(CacheKey);
            if (objList == null)
            {
                try
                {
                    objList = dal.GetMenuPageElementList();
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
        /// menu改变时，写入缓存
        /// </summary>
        private void SetMenuCache()
        {
            int DataSetCache = SCZM.Common.ConfigHelper.GetConfigInt("DataSetCache");
            string CacheKey = "Cache_sys_MenuPage";
            object objList = dal.GetMenuPageList();
            if (objList != null)
            {
                SCZM.Common.DataCache.SetCache(CacheKey, objList, DateTime.Now.AddMinutes(DataSetCache), TimeSpan.Zero);
            }
            CacheKey = "Cache_sys_MenuPageElement";
            objList = dal.GetMenuPageElementList();
            if (objList != null)
            {
                SCZM.Common.DataCache.SetCache(CacheKey, objList, DateTime.Now.AddMinutes(DataSetCache), TimeSpan.Zero);
            }
        }

        #endregion 权限判断使用
        //效率测试临时表

        public void test(int type, double time)
        {
            dal.test(type, time);
        }
        #endregion  ExtensionMethod
    }
}

