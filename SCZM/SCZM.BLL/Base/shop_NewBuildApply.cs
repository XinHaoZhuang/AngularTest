using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UHPROJ.Common;
using UHPROJ.Model.Base;
namespace UHPROJ.BLL.Base
{
    /// <summary>
    /// 业务逻辑类shop_NewBuildApply 的摘要说明。
    /// </summary>
    public partial class shop_NewBuildApply
    {
        private readonly UHPROJ.DAL.Base.shop_NewBuildApply dal = new UHPROJ.DAL.Base.shop_NewBuildApply();
        public shop_NewBuildApply()
        { }
        #region  基本方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(UHPROJ.Model.Base.shop_NewBuildApply model, int submitFlag, out string message)
        {
            message = "保存成功！";
            string errMessage = "";
            BLL.System.sys_Process_Exec procBLL = new BLL.System.sys_Process_Exec();
            string tableName = "shop_NewBuildApply";
            if (submitFlag == 1)
            {
                message = "保存并提交成功！";
                errMessage = procBLL.CheckSubmit(tableName, model.BillSign, model.DepId, "", model.OperaId);
                if (errMessage != "")
                {
                    message = errMessage;
                    return 0;
                }
            }
            int rowId = dal.Add(model);
            if (rowId < 1)
            {
                message = "保存失败！";
            }
            else
            {
                BLL.System.sys_Attachment attachmenBLL = new BLL.System.sys_Attachment();
                string IDList = (model.AttachmentId_ZSRW == "" || model.AttachmentId_ZSRW == null ? "" : model.AttachmentId_ZSRW + ",") +
                    (model.AttachmentId_GH == "" || model.AttachmentId_GH == null ? "" : model.AttachmentId_GH + ",")
                    ;
                string FileUse = "加盟申请";
                if (IDList != "")
                {
                    attachmenBLL.UpdateUseList(Utils.DelLastComma(IDList), FileUse, rowId);
                }
                if (submitFlag == 1)
                {
                    errMessage = procBLL.SetBillProcess_Submit(tableName, rowId.ToString(), 1, model.OperaId);
                    if (errMessage != "")
                    {
                        message = "保存成功,但提交失败，请联系系统管理员！";
                    }
                }
            }
            return rowId;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(UHPROJ.Model.Base.shop_NewBuildApply model, int submitFlag, out string message)
        {
            message = "保存成功！";
            string errMessage = "";
            BLL.System.sys_Process_Exec procBLL = new BLL.System.sys_Process_Exec();
            string tableName = "shop_NewBuildApply";
            if (submitFlag == 1)
            {
                message = "保存并提交成功！";
                errMessage = procBLL.CheckSubmit(tableName, model.BillSign, model.DepId, "", model.OperaId);
                if (errMessage != "")
                {
                    message = errMessage;
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
                BLL.System.sys_Attachment attachmenBLL = new BLL.System.sys_Attachment();
                string IDList = (model.AttachmentId_ZSRW == "" || model.AttachmentId_ZSRW == null ? "" : model.AttachmentId_ZSRW + ",") +
                    (model.AttachmentId_GH == "" || model.AttachmentId_GH == null ? "" : model.AttachmentId_GH + ",")
                    ;
                string FileUse = "加盟申请";
                if (IDList != "")
                {
                    attachmenBLL.UpdateUseList(Utils.DelLastComma(IDList), FileUse, model.ID);
                }
                if (submitFlag == 1)
                {
                    errMessage = procBLL.SetBillProcess_Submit(tableName, model.ID.ToString(), 1, model.OperaId);
                    if (errMessage != "")
                    {
                        message = "保存成功,但提交失败，请联系系统管理员！";
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public UHPROJ.Model.Base.shop_NewBuildApply GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获取申请页面的数据列表 通过存储过程
        /// </summary>
        /// <param name="strWhere">where条件</param>
        /// <param name="perId">查询人ID</param>
        /// <param name="billState">单据状态0全部 1审批完成 2 审批未完成 3审批中 4审批不同意 5未提交 6已提交</param>
        /// <returns></returns>
        public DataSet GetApplyList(string strWhere, int perId, int billState)
        {
            return dal.GetApplyList(strWhere, perId, billState);
        }

        /// <summary>
        /// 获取审核页面的数据列表 通过存储过程
        /// </summary>
        /// <param name="strWhere">where条件</param>
        /// <param name="perId">查询人ID</param>
        /// <param name="billState">单据状态0全部 1审批完成 2 审批未完成 3审批中 4审批不同意 5未提交 6已提交</param>
        /// <param name="auditState">审核状态 0全部 1已审核 2 未审核	3 审批同意	4 审批不同意</param>
        /// <returns></returns>
        public DataSet GetAuditList(string strWhere, int perId, int billState, int auditState)
        {
            return dal.GetAuditList(strWhere, perId, billState, auditState);
        }

        /// <summary>
        /// 获得数据明细 根据ID
        /// </summary>
        public DataSet GetDetail(int ID)
        {
            return dal.GetDetail(ID);
        }

        /// <summary>
        /// 批量提交数据
        /// </summary>
        /// <param name="IDList">ID字符串</param>
        /// <param name="operaId">操作人ID</param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public bool Submit(string IDList, int operaId, out string message)
        {
            message = "提交成功！";
            BLL.System.sys_Process_Exec procBLL = new BLL.System.sys_Process_Exec();
            string tableName = "shop_NewBuildApply";
            string errMessage = procBLL.CheckSubmit(tableName, "", 0, IDList, operaId);
            if (errMessage != "")
            {
                message = errMessage;
                return false;
            }
            errMessage = procBLL.SetBillProcess_Submit(tableName, IDList, 1, operaId);
            if (errMessage != "")
            {
                message = errMessage;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 批量取消提交数据
        /// </summary>
        /// <param name="IDList">ID字符串</param>
        /// <param name="operaId">操作人ID</param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public bool CancelSubmit(string IDList, int operaId, out string message)
        {
            message = "取消提交成功！";
            BLL.System.sys_Process_Exec procBLL = new BLL.System.sys_Process_Exec();
            string tableName = "shop_NewBuildApply";
            string errMessage = procBLL.CheckCancelSubmit(tableName, IDList, operaId);
            if (errMessage != "")
            {
                message = errMessage;
                return false;
            }
            errMessage = procBLL.SetBillProcess_Submit(tableName, IDList, 1, operaId);
            if (errMessage != "")
            {
                message = errMessage;
                return false;
            }
            return true;
        }

        #endregion  基本方法
        #region  扩展方法
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDList, out string message)
        {
            message = "删除成功！";
            string[] idArr = IDList.Split(',');
            BLL.System.sys_Process_Exec procBLL = new BLL.System.sys_Process_Exec();
            string tableName = "shop_NewBuildApply";
            string errMessage = procBLL.ExistSubmit(tableName, IDList);
            if (errMessage != "")
            {
                message = errMessage;
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
                errMessage = procBLL.SetBillProcess_Del(tableName, IDList);
                return true;
            }
        }

        /// <summary>
        /// 批量审核数据
        /// </summary>
        /// <param name="IDList">ID字符串</param>
        /// <param name="operaId">操作人ID</param>
        /// <param name="flagAudit">审核状态</param>
        /// <param name="auditorOpinion">审核意见</param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public bool Audit(string IDList, int operaId, int flagAudit, string auditorOpinion, out string message)
        {
            message = "审批成功！";
            BLL.System.sys_Process_Exec procBLL = new BLL.System.sys_Process_Exec();
            string tableName = "shop_NewBuildApply";
            string resultMessage = "";
            bool result = procBLL.SetBillProcess_Audit_Batch(tableName, IDList, 1, operaId, flagAudit, auditorOpinion, out resultMessage);
            if (result == false)
            {
                message = resultMessage;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 批量取消审核数据
        /// </summary>
        /// <param name="IDList">ID字符串</param>
        /// <param name="operaId">操作人ID</param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public bool CanclAudit(string IDList, int operaId, out string message)
        {
            message = "取消审批成功！";
            BLL.System.sys_Process_Exec procBLL = new BLL.System.sys_Process_Exec();
            string tableName = "shop_NewBuildApply";
            string resultMessage = "";
            bool result = procBLL.SetBillProcess_Audit_Batch(tableName, IDList, 2, operaId, 0, "", out resultMessage);
            if (result == false)
            {
                message = resultMessage;
                return false;
            }
            return true;
        }

        #endregion  扩展方法
    }
}

