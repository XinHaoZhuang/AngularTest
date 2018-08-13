using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model.Repair;

namespace SCZM.BLL.Repair
{
    /// <summary>
    /// 业务逻辑类repair_Report 的摘要说明。
    /// </summary>
    public partial class repair_Report
    {
        private readonly DAL.Repair.repair_Report dal = new DAL.Repair.repair_Report();
        public repair_Report() { }
        /// <summary>
        /// 获取台帐 通过where条件
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetRepairTZList(string strWhere)
        {
            return dal.GetRepairTZList(strWhere);
        }
        /// <summary>
        /// 获取进度 通过where条件
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetRepairProgressList(string strWhere)
        {
            return dal.GetRepairProgressList(strWhere);
        }
        /// <summary>
        /// 获取项目统计 通过where条件
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetItemStatistics(string strWhere) {
            return dal.GetItemStatistics(strWhere);
        }
        /// <summary>
        /// 获取零件统计 通过where条件
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetPartStatistics(string strWhere)
        {
            return dal.GetPartStatistics(strWhere);
        }
        public DataSet GetRepairerRewardDetail(string strWhere)
        {
            return dal.GetRepairerRewardDetail(strWhere);
        }
        public DataSet GetRepairerReward(string strWhere)
        {
            return dal.GetRepairerReward(strWhere);
        }
        public DataSet GetRepairerReward_Audit(string strWhere)
        {
            return dal.GetRepairerReward_Audit(strWhere);
        }
        public DataSet GetRepairerSaturation(string strWhere_Procedure, string strWhere_Repairer, int RepairDepId,string lastDay)
        {
            return dal.GetRepairerSaturation(strWhere_Procedure,strWhere_Repairer,RepairDepId,lastDay);
        }
        public DataSet GetRepairerSaturationDetail(string strWhere_Procedure,string lastDay)
        {
            return dal.GetRepairerSaturationDetail(strWhere_Procedure,lastDay);
        }
        public DataSet GetIntentionNum(string strWhere) {
            return dal.GetIntentionNum(strWhere);
        }
        public DataSet GetRepairProgress_Intention(string strWhere,int perId) {
            return dal.GetRepairProgress_Intention(strWhere,perId);
        }
        public DataSet GetRepairIntentionStatistics(string strWhere) {
            return dal.GetRepairIntentionStatistics(strWhere);
        }
        public int SaveRepairReward_Audit(Model.Repair.Repair_Assignment_Procedure model)
        {
            return dal.SaveRepairReward_Audit(model);
        }
        public int SaveRepairReward_CancelAudit(string IDStr)
        {
            return dal.SaveRepairReward_CancelAudit(IDStr);
        }
        public int SaveRepairReward_MultipleAudit(string IDStr, Model.Repair.Repair_Assignment_Procedure model) {
            return dal.SaveRepairReward_MultipleAudit(IDStr, model);
        }
        public DataSet GetWeeks(int year,string FirstDay,string FirstDayOfNextMonth) {
            return dal.GetWeeks(year, FirstDay, FirstDayOfNextMonth);
        }
        public DataSet GetRepairSchemeFee(string strWhere,string strWhere_Receive) {
            return dal.GetRepairSchemeFee(strWhere,strWhere_Receive);
        }
        public DataSet GetRepairEfficiency_Procedure(string strWhere, string strOrder) {
            return dal.GetRepairEfficiency_Procedure(strWhere, strOrder);
        }
        public DataSet GetRepairTimeStatistics(string strWhere) {
            return dal.GetRepairTimeStatistics(strWhere);
        }
        public DataSet GetRepairEfficiency_Person(string strWhere) {
            return dal.GetRepairEfficiency_Person(strWhere);
        }
    }
}
