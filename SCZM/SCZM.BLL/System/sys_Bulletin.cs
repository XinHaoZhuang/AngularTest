using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model;
namespace SCZM.BLL
{
    /// <summary>
    /// 业务逻辑类sys_Bulletin 的摘要说明。

    /// </summary>
    public partial class sys_Bulletin
    {
        private readonly SCZM.DAL.System.sys_Bulletin dal = new SCZM.DAL.System.sys_Bulletin();
        public sys_Bulletin()
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
        public int Add(SCZM.Model.System.sys_Bulletin model,string fileId, out string message)
        {
            message = "新增成功！";
            int rowId = dal.Add(model);
            if (rowId < 1)
            {
                message = "新增失败！";
            }
            else
            {
                if (fileId != "")
                {
                    new DAL.System.sys_Attachment().UpdateUseList(fileId, "公告", rowId);
                }
                if (model.Attachment != "")
                {
                    new DAL.System.sys_Attachment().UpdateUseList(fileId, "公告", rowId);
                }
            }
            return rowId;
        }

        /// <summary>
        /// 更新一条数据

        /// </summary>
        public bool Update(SCZM.Model.System.sys_Bulletin model, string fileId, out string message)
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
                DAL.System.sys_Attachment attachDal = new DAL.System.sys_Attachment();

                attachDal.DelUseList("公告", model.ID);
                if (fileId != "")
                {
                    attachDal.UpdateUseList(fileId, "公告", model.ID);
                }
                if (model.Attachment != "")
                {
                    attachDal.UpdateUseList(fileId, "公告", model.ID);
                }
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
        public SCZM.Model.System.sys_Bulletin GetModel(int ID)
        {

            return dal.GetModel(ID);
        }
        /// <summary>
        /// 得到一个对象实体 主表
        /// </summary>
        public SCZM.Model.System.sys_Bulletin GetModelMain(int ID)
        {

            return dal.GetModelMain(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。

        /// </summary>
        public SCZM.Model.System.sys_Bulletin GetModelByCache(int ID)
        {

            string CacheKey = "sys_BulletinModel-" + ID;
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
            return (SCZM.Model.System.sys_Bulletin)objModel;
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
        public List<SCZM.Model.System.sys_Bulletin> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SCZM.Model.System.sys_Bulletin> DataTableToList(DataTable dt)
        {
            List<SCZM.Model.System.sys_Bulletin> modelList = new List<SCZM.Model.System.sys_Bulletin>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SCZM.Model.System.sys_Bulletin model;
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
        /// 批量发布公告、取消发布公告

        /// </summary>
        public bool Submit(string IDList, int billState, int operaId, string operaName, out string message)
        {
            if (billState == 0)
            {
                message = "取消发布成功！";
            }
            else
            {
                message = "发布成功！";
            }
            int rows = dal.Submit(IDList,billState,operaId,operaName);
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
        /// 批量置顶公告
        /// </summary>
        public bool SetTop(string IDList, int flagTop, int operaId, string operaName, out string message)
        {
            if (flagTop == 0)
            {
                message = "取消置顶成功！";
            }
            else
            {
                message = "置顶成功！";
            }
            int rows = dal.SetTop(IDList, flagTop, operaId, operaName);
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
        /// 获得数据列表 通过个人的权限

        /// </summary>
        public DataSet GetListByPower(string strWhere, int recordNum, int depId)
        {
            return dal.GetListByPower(strWhere, recordNum, depId);
        }
        #endregion  扩展方法
    }
}

