using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model.Repair;
namespace SCZM.BLL.Repair
{
    /// <summary>
    /// 业务逻辑类repair_SettlementList 的摘要说明。
    /// </summary>
    public partial class repair_SettlementList
    {
        private readonly SCZM.DAL.Repair.repair_SettlementList dal = new SCZM.DAL.Repair.repair_SettlementList();
        public repair_SettlementList()
        { }
        #region  基本方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SCZM.Model.Repair.repair_SettlementList model, out string message)
        {
            message = "保存成功！";
            int rowId = dal.Add(model);
            if (rowId < 1)
            {
                message = "保存失败！";
            }
            else
            {
                DAL.Repair.repair_Intention dal_Intention = new DAL.Repair.repair_Intention();
                dal_Intention.UpdateRepairState(model.IntentionId.ToString(), 40);

                BLL.System.sys_Attachment attachmenBLL = new BLL.System.sys_Attachment();
                string IDList = (model.AttachmentId_Settlement == "" || model.AttachmentId_Settlement == null ? "" : model.AttachmentId_Settlement + ",")
                    ;
                string FileUse = "维修结算";
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
        public bool Update(SCZM.Model.Repair.repair_SettlementList model, out string message)
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
                string IDList = (model.AttachmentId_Settlement == "" || model.AttachmentId_Settlement == null ? "" : model.AttachmentId_Settlement + ",")
                    ;
                string FileUse = "维修结算";
                if (IDList != "")
                {
                    attachmenBLL.UpdateUseList(Utils.DelLastComma(IDList), FileUse, rows);
                }
                return true;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SCZM.Model.Repair.repair_SettlementList GetModel(int ID)
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
        public bool DeleteList(string IDList,string IntentionIdList, out string message)
        {
            message = "删除成功！";
            int rows = dal.DeleteList(IDList,IntentionIdList);
            if (rows == 0)
            {
                message = "对不起，所选数据已被其他人删除！";
                return false;
            }
            else
            {
                DAL.Repair.repair_Intention dal_Intention = new DAL.Repair.repair_Intention();
                dal_Intention.UpdateRepairState(IntentionIdList, 30);
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
        #endregion  扩展方法
    }
}

