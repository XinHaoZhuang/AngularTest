using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model.Repair;
namespace SCZM.BLL.Repair
{
    /// <summary>
    /// 业务逻辑类Repair_Assignment 的摘要说明。
    /// </summary>
    public partial class Repair_Assignment
    {
        private readonly SCZM.DAL.Repair.Repair_Assignment dal = new SCZM.DAL.Repair.Repair_Assignment();
        public Repair_Assignment()
        { }
        #region  基本方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SCZM.Model.Repair.Repair_Assignment model, out string message)
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
        public bool Update(SCZM.Model.Repair.Repair_Assignment model, string procedureList_prev, out string message)
        {
            message = "保存成功！";
            int rows = 0;
            if (procedureList_prev != "")
            {
               rows = dal.Update(model, procedureList_prev);
            }
            else {
               rows = dal.Update(model);
            }
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
        public SCZM.Model.Repair.Repair_Assignment GetModel(int ID)
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
        public bool UndoList(string IDList, out string message)
        {
            message = "作废成功！";
            int rows = dal.UndoList(IDList);
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
        /// <summary>
        /// 获取下拉菜单
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetComboList(string strWhere)
        {
            return dal.GetComboList(strWhere);
        }
        /// <summary>
        /// 获取相应工序下拉菜单
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetProcessComboList(string strWhere)
        {
            return dal.GetProcessComboList(strWhere);
        }

        public DataSet GetScheduleList(string strWhere,int PerId,bool IsAdmin) {
            return dal.GetScheduleList(strWhere,PerId,IsAdmin);
        }
        public DataSet GetScheduleList_search(string strWhere, int PerId, bool IsAdmin)
        {
            return dal.GetScheduleList_search(strWhere, PerId, IsAdmin);
        }
        public DataSet GetAssignmentList(string strWhere, int PerId, bool IsAdmin, string strWhere_all)
        {
            return dal.GetAssignmentList(strWhere, PerId, IsAdmin,strWhere_all);
        }
        public DataSet GetAssignmentList_search(string strWhere, int PerId, bool IsAdmin, string strWhere_all)
        {
            return dal.GetAssignmentList_search(strWhere, PerId, IsAdmin,strWhere_all);
        }
        public DataSet GetAssignmentProcedureList(string strWhere) {
            return dal.GetAssignmentProcedureList(strWhere);
        }
        public int UpdateRepairSecond(int AssignmentProcedureId, int RepairSecond_All, int RepairSecond_Repair, int RepairSecond_Pause)
        {
            return dal.UpdateRepairSecond(AssignmentProcedureId, RepairSecond_All, RepairSecond_Repair, RepairSecond_Pause);
        }
        public int CancelRepairSecond(int AssignmentProcedureId)
        {
            return dal.CancelRepairSecond(AssignmentProcedureId);
        }
        #endregion  扩展方法
    }
}

