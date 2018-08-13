using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model.Base;
namespace SCZM.BLL.Base
{
    /// <summary>
    /// 业务逻辑类base_ProcessClass 的摘要说明。
    /// </summary>
    public partial class base_ProcessClass
    {
        private readonly SCZM.DAL.Base.base_ProcessClass dal = new SCZM.DAL.Base.base_ProcessClass();
        public base_ProcessClass()
        { }
        #region  基本方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SCZM.Model.Base.base_ProcessClass model, out string message)
        {
            message = "保存成功！";
            int rowId = dal.Add(model);
            if (rowId < 1)
            {
                message = "保存失败！";
            }
            return rowId;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SCZM.Model.Base.base_ProcessClass model, out string message)
        {
            message = "保存成功！";
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
        /// 得到一个对象实体
        /// </summary>
        public SCZM.Model.Base.base_ProcessClass GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获得数据列表 通过Where条件
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据明细 根据ID
        /// </summary>
        public DataSet GetDetail(int ID)
        {
            return dal.GetDetail(ID);
        }

        #endregion  基本方法
        #region  扩展方法
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
        /// 获得数据列表 通过Where条件
        /// </summary>
        public DataSet GetComboList(string strWhere)
        {
            return dal.GetComboList(strWhere);
        }
        #endregion  扩展方法
    }
}

