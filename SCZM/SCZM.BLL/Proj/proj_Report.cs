using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
namespace SCZM.BLL.Proj
{
    public partial class proj_Report
    {
        private readonly SCZM.DAL.Proj.proj_Report dal = new SCZM.DAL.Proj.proj_Report();
        public proj_Report()
        { }
        /// <summary>
        /// 得到项目合同明细
        /// </summary>
        /// <param name="strWhere">where条件</param>
        /// <param name="operaId">操作人ID</param>
        /// <returns></returns>
        public DataSet GetContractMX(string strWhere, int operaId)
        {
            return dal.GetContractMX(strWhere, operaId);
        }
        /// <summary>
        /// 得到合作单位合同明细
        /// </summary>
        /// <param name="strWhere">where条件</param>
        /// <param name="operaId">操作人ID</param>
        /// <returns></returns>
        public DataSet GetPartnerContractMX(string strWhere, int operaId)
        {
            return dal.GetPartnerContractMX(strWhere, operaId);
        }
        /// <summary>
        /// 获得项目台账
        /// </summary>
        /// <param name="projName">项目名</param>
        /// <param name="custName">客户名</param>
        /// <param name="projType">项目类型</param>
        /// <param name="contractCode">合同号</param>
        /// <param name="contractNat_min">最小合同金额</param>
        /// <param name="contractNat_max">最大合同金额</param>
        /// <param name="contractDate_min">最小合同日期</param>
        /// <param name="contractDate_max">最大合同日期</param>
        /// <param name="calDate">统计截止时间</param>
        /// <param name="operaId">操作人ID</param>
        /// <returns></returns>
        public DataSet GetPorjHZ(string projName, string custName, int projType, string contractCode, string contractNat_min, string contractNat_max, string contractDate_min, string contractDate_max, DateTime calDate, int operaId)
        {
            return dal.GetPorjHZ(projName, custName, projType, contractCode, contractNat_min, contractNat_max, contractDate_min, contractDate_max, calDate, operaId);
        }
        /// <summary>
        /// 获得项目合同的所有信息 主表、预算、实际、发票
        /// </summary>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        public DataSet GetContractAll(int ContractId)
        {
            return dal.GetContractAll(ContractId);
        }
        /// <summary>
        /// 获得合作单位合同的所有信息 主表、预算、实际、发票
        /// </summary>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        public DataSet GetPartnerContractAll(int ContractId)
        {
            return dal.GetPartnerContractAll(ContractId);
        }
        /// <summary>
        /// 或得项目档案卡
        /// </summary>
        /// <param name="ProjId">项目ID</param>
        /// <returns></returns>
        public DataSet GetProjCard(int ProjId)
        {
            return dal.GetProjCard(ProjId);
        }
    }
}
