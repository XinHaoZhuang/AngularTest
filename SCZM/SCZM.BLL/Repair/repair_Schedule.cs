using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model.Repair;
namespace SCZM.BLL.Repair
{
    /// <summary>
    /// 业务逻辑类repair_Schedule 的摘要说明。
    /// </summary>
    public partial class repair_Schedule
    {
        private readonly SCZM.DAL.Repair.repair_Schedule dal = new SCZM.DAL.Repair.repair_Schedule();
        public repair_Schedule()
        { }
        #region  基本方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SCZM.Model.Repair.repair_Schedule model, out string message)
        {
            message = "保存成功！";
            if (!dal.CheckSaveData(model.ScheduleDate,model.ID ,model.AssignmentProcedureId)) {
                message = "请合理保存反馈时间";
                return 0;
            }
            int ScheduleType_current=dal.GetScheduleType(model.AssignmentProcedureId);
            switch (ScheduleType_current)
            {
                case 1:
                    if (model.ScheduleType == 1) {
                        message = "当前进度以保存，请返回查看";
                        return 0;
                    }
                    break;
                case 2:
                    if (model.ScheduleType == 2 || model.ScheduleType == 3) {
                        message = "当前进度以保存，请返回查看";
                        return 0;
                    }
                    break;
                case 3:
                    message = "当前进度以保存，请返回查看";
                    return 0;
                case -1:
                    if (model.ScheduleType == 2 || model.ScheduleType == 3) {
                        message = "当前进度以保存，请返回查看";
                        return 0;
                    }
                    break;
                default:
                    break;
            }
            int rowId = dal.Add(model);
            if (rowId < 1)
            {
                message = "保存失败！";
            }
            else
            {
                BLL.System.sys_Attachment attachmenBLL = new BLL.System.sys_Attachment();
                string IDList = (model.AttachmentList_Schedule == "" || model.AttachmentList_Schedule == null ? "" : model.AttachmentList_Schedule + ",")
                    ;
                string FileUse = "进度反馈";
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
        public bool Update(SCZM.Model.Repair.repair_Schedule model, out string message)
        {
            message = "保存成功！";
            if (!dal.CheckSaveData(model.ScheduleDate, model.ID, model.AssignmentProcedureId))
            {
                message = "请合理保存反馈时间";
                return false;
            }
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
        public SCZM.Model.Repair.repair_Schedule GetModel(int ID)
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
        /// 获得数据列表 通过Where条件
        /// </summary>
        public DataSet GetList_HaveAttachment(string strWhere)
        {
            return dal.GetList_HaveAttachment(strWhere);
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
        /// 删除最后数据
        /// </summary>
        public bool DeleteLastSchedule(int lastId,int prevId, out string message)
        {
            message = "删除成功！";
            DataTable dt = dal.GetDetail(lastId).Tables[0];
            int rows = dal.DeleteLastSchedule(lastId,prevId);
            if (rows == 0)
            {
                message = "对不起，所选数据已被其他人删除！";
                return false;
            }
            else
            {
                if (dt.Rows[0]["ScheduleType"].ToString() == "3") {
                    DAL.Repair.Repair_Assignment dal_assignment = new DAL.Repair.Repair_Assignment();
                    dal_assignment.CancelRepairSecond(Utils.StrToInt(dt.Rows[0]["AssignmentProcedureId"].ToString(),0));
                }
                return true;
            }
        }
        public DataSet GetRepairSceond(int AssignmentProcedureId) {
            return dal.GetRepairSceond(AssignmentProcedureId);
        }
        public int GetScheduleType(int AssignmentProcedureId)
        {
            return dal.GetScheduleType(AssignmentProcedureId);
        }
        #endregion  扩展方法
    }
}

