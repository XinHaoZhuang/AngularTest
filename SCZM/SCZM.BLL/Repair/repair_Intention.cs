using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model.Repair;
namespace SCZM.BLL.Repair
{
    /// <summary>
    /// 业务逻辑类repair_Intention 的摘要说明。
    /// </summary>
    public partial class repair_Intention
    {
        private readonly SCZM.DAL.Repair.repair_Intention dal = new SCZM.DAL.Repair.repair_Intention();
        public repair_Intention()
        { }
        #region  基本方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SCZM.Model.Repair.repair_Intention model, out string message)
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
                string IDList = (model.AttachmentId_Agreement == "" || model.AttachmentId_Agreement == null ? "" : model.AttachmentId_Agreement + ",")
                    ;
                string FileUse = "维修意向";
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
        public bool Update(SCZM.Model.Repair.repair_Intention model, out string message)
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
                string IDList = (model.AttachmentId_Agreement == "" || model.AttachmentId_Agreement == null ? "" : model.AttachmentId_Agreement + ",")
                    ;
                string FileUse = "维修意向";
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
        public SCZM.Model.Repair.repair_Intention GetModel(int ID)
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
            //------------------检查是否调用---------------------------------
            if (dal.CheckUse(IDList) > 0) {
                message = "对不起，所选数据正在被使用，无法删除！";
                return false;
            }
            //---------------------------------------------------------------
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
        public int GetMaxId() {
            return dal.GetMaxId();
        }
        /// <summary>
        /// 入厂登记
        /// </summary>
        public bool SaveActualEnterDate(string IDList,DateTime ActualEnterDate,string OperaName_Enter, out string message)
        {
            message = "入厂登记成功！";
            int rows = dal.ActualEnterDate(IDList, ActualEnterDate, OperaName_Enter);
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
        /// 取消入厂登记
        /// </summary>
        public bool CancelActualEnterDate(string IDList, out string message)
        {
            message = "取消入厂登记成功！";
            int rows = dal.CancelActualEnterDate(IDList);
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
        /// 获取下拉菜单
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetComboList(string strWhere) {
            return dal.GetComboList(strWhere);
        }
        public bool SaveAgreement(int ID, string AttachmentId_Agreement, DateTime AgreementDate, out string message)
        {
            message = "上传成功";
            int row = dal.SaveAgreement(ID, AttachmentId_Agreement, AgreementDate);
            if (row == 0)
            {
                message = "对不起，所选数据已被其他人删除！";
                return false;
            }
            else {
                BLL.System.sys_Attachment attachmenBLL = new BLL.System.sys_Attachment();
                string IDList = (AttachmentId_Agreement == "" || AttachmentId_Agreement == null ? "" : AttachmentId_Agreement + ",");
                string FileUse = "维修协议";
                if (IDList != "")
                {
                    attachmenBLL.UpdateUseList(Utils.DelLastComma(IDList), FileUse, ID);
                }
                return true;
            }
        }
        /// <summary>
        /// 出厂登记
        /// </summary>
        public bool SaveActualLeaveDate(string IDList, DateTime ActualLeaveDate,int LeaveTypeId, string OperaName_Leave, out string message)
        {
            message = "出厂登记成功！";
            int rows = dal.ActualLeaveDate(IDList, ActualLeaveDate,LeaveTypeId, OperaName_Leave);
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
        /// 取消出厂登记
        /// </summary>
        public bool CancelActualLeaveDate(string IDList, out string message)
        {
            message = "取消出厂登记成功！";
            int rows = dal.CancelActualLeaveDate(IDList);
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
        public DataSet GetList_Leave(string strWhere)
        {
            return dal.GetList_Leave(strWhere);
        }
        public int UpdateAttachmentList(string IdList) {
            return dal.UpdateAttachmentList(IdList);
        }
        #endregion  扩展方法
    }
}

