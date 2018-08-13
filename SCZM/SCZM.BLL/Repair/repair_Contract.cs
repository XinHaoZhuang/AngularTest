using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model.Repair;
namespace SCZM.BLL.Repair
{
    /// <summary>
    /// 业务逻辑类repair_Contract 的摘要说明。
    /// </summary>
    public partial class repair_Contract
    {
        private readonly SCZM.DAL.Repair.repair_Contract dal = new SCZM.DAL.Repair.repair_Contract();
        public repair_Contract()
        { }
        #region  基本方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SCZM.Model.Repair.repair_Contract model, out string message)
        {
            message = "保存成功！";
            int rowId = dal.Add(model);
            if (rowId < 1)
            {
                message = "保存失败！";
            }
            else
            {
                BLL.System.sys_Attachment attachmenBLL = new BLL.System.sys_Attachment();
                string IDList = (model.AttachmentId_Contract == "" || model.AttachmentId_Contract == null ? "" : model.AttachmentId_Contract + ",")
                    ;
                string FileUse = "维修合同";
                if (IDList != "")
                {
                    attachmenBLL.UpdateUseList(Utils.DelLastComma(IDList), FileUse, rowId);
                }
            }
            return rowId;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SCZM.Model.Repair.repair_Contract model, out string message)
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
                BLL.System.sys_Attachment attachmenBLL = new BLL.System.sys_Attachment();
                string IDList = (model.AttachmentId_Contract == "" || model.AttachmentId_Contract == null ? "" : model.AttachmentId_Contract + ",")
                    ;
                string FileUse = "维修合同";
                if (IDList != "")
                {
                    attachmenBLL.UpdateUseList(Utils.DelLastComma(IDList), FileUse, model.ID);
                }
                return true;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SCZM.Model.Repair.repair_Contract GetModel(int ID)
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
        /// 最大ID+1
        /// </summary>
        /// <returns></returns>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }
        public DataSet GetIntentionCode_LastCombo(string strWhere) {
            return dal.GetIntentionCode_LastCombo(strWhere);
        }
        #endregion  扩展方法
    }
}

