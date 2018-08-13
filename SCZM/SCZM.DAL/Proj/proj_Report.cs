using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;//请先添加引用

namespace SCZM.DAL.Proj
{
    /// <summary>
    /// /// <summary>
    /// 数据访问类proj_Report。
    /// </summary>
    /// </summary>
    public partial class proj_Report
    {
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

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select b.ProjName,c.CustName,f.CompanyName,a.ProjType,a.ContractCode,a.ContractDate,a.ContractNat,a.StartDate,a.CompleteDate,a.Timescale,d.PerName as SvcManagerName,e.PerName as ProjManagerName,b.ProjManagerHistory ");
            strSql.Append("FROM proj_Contract a ");
            strSql.Append("left join proj_Project b on a.ProjId=b.ID ");
            strSql.Append("left join base_Customer c on b.CustId=c.ID ");
            strSql.Append("left join sys_Person d on b.SvcManagerId=d.ID ");
            strSql.Append("left join sys_Person e on b.ProjManagerId=e.ID ");
            strSql.Append("left join base_Company f on a.CompanyId=f.ID ");
            strSql.Append("where a.FlagDel=0 and a.BillState=1 and b.ProjManagerId in(select CtrlPerId from v_sys_PersonCtrl where PerId=" + operaId.ToString() + ")");
            if (strWhere != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.ContractDate");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 得到合作单位合同明细
        /// </summary>
        /// <param name="strWhere">where条件</param>
        /// <param name="operaId">操作人ID</param>
        /// <returns></returns>
        public DataSet GetPartnerContractMX(string strWhere, int operaId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select b.ProjName,c.PartnerName,f.CompanyName,g.PartnerTypeName,a.ContractCode,a.ContractDate,a.ContractNat,a.StartDate,a.CompleteDate,a.Timescale,d.PerName as SvcManagerName,e.PerName as ProjManagerName,b.ProjManagerHistory  ");
            strSql.Append("FROM proj_PartnerContract a ");
            strSql.Append("left join proj_Project b on a.ProjId=b.ID ");
            strSql.Append("left join base_Partner c on a.PartnerId=c.ID ");
            strSql.Append("left join sys_Person d on b.SvcManagerId=d.ID ");
            strSql.Append("left join sys_Person e on b.ProjManagerId=e.ID ");
            strSql.Append("left join base_Company f on a.CompanyId=f.ID ");
            strSql.Append("left join base_PartnerType g on a.PartnerTypeId=g.ID ");
            strSql.Append("where a.FlagDel=0 and a.BillState=1 and b.ProjManagerId in(select CtrlPerId from v_sys_PersonCtrl where PerId=" + operaId.ToString() + ")");
            if (strWhere != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.ContractDate");
            return DbHelperSQL.Query(strSql.ToString());
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
            string procName = "p_proj_GetPorjHZ";
            SqlParameter[] parameters = {
					new SqlParameter("@projName", SqlDbType.NVarChar),
                    new SqlParameter("@custName", SqlDbType.NVarChar),
                    new SqlParameter("@projType", SqlDbType.Int,4),
                    new SqlParameter("@contractCode", SqlDbType.VarChar),
                    new SqlParameter("@contractNat_min", SqlDbType.Decimal,9),
                    new SqlParameter("@contractNat_max", SqlDbType.Decimal,9),
                    new SqlParameter("@contractDate_min", SqlDbType.SmallDateTime),
                    new SqlParameter("@contractDate_max", SqlDbType.SmallDateTime),
                    new SqlParameter("@calDate", SqlDbType.DateTime),
                    new SqlParameter("@operaId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = projName;
            parameters[1].Value = custName;
            parameters[2].Value = projType;
            parameters[3].Value = contractCode;
            if (contractNat_min == "")
            {
                parameters[4].Value = null;
            }
            else
            {
                parameters[4].Value = Convert.ToDecimal(contractNat_min);
            }
            if (contractNat_max == "")
            {
                parameters[5].Value = null;
            }
            else
            {
                parameters[5].Value = Convert.ToDecimal(contractNat_max);
            }
            if (contractDate_min == "")
            {
                parameters[6].Value = null;
            }
            else
            {
                parameters[6].Value = Convert.ToDateTime(contractDate_min);
            }
            if (contractDate_max == "")
            {
                parameters[7].Value = null;
            }
            else
            {
                parameters[7].Value = Convert.ToDateTime(contractDate_max);
            }
            parameters[8].Value = calDate;
            parameters[9].Value = operaId;
            return DbHelperSQL.RunProcedure(procName, parameters, "dt");
        }
        /// <summary>
        /// 获得项目合同的所有信息 主表、预算、实际、发票
        /// </summary>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        public DataSet GetContractAll(int ContractId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select b.ProjName,c.CustName,d.CompanyName,a.ProjType,a.ContractCode,a.ContractDate,a.ContractNat,a.StartDate,a.CompleteDate,a.Timescale,a.Memo,e.PerName as SvcManagerName,f.PerName as ProjManagerName ");
            strSql.Append("from proj_Contract a ");
            strSql.Append("left join proj_Project b on a.ProjId=b.ID ");
            strSql.Append("left join base_Customer c on b.CustId=c.ID ");
            strSql.Append("left join base_Company d on a.CompanyId=d.ID ");
            strSql.Append("left join sys_Person e on b.SvcManagerId=e.ID ");
            strSql.Append("left join sys_Person f on b.ProjManagerId=f.ID ");
            strSql.Append("where a.FlagDel=0 and a.ID=" + ContractId.ToString());
            DataSet ds = DbHelperSQL.Query(strSql.ToString());

            DataTable dt = null;
            strSql.Clear();
            strSql.Append("select Progress,ReceiveDate,ReceiveRate,ReceiveNat from proj_ContractDetail where FlagDel=0 and ContractId=" + ContractId.ToString() + " order by ReceiveDate,ID");
            dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            dt.TableName = "budget";
            ds.Tables.Add(dt.Copy());

            dt = null;
            strSql.Clear();
            strSql.Append("select ReceiveDate,ReceiveNat from proj_Receipts where FlagDel=0 and ContractId=" + ContractId.ToString() + " order by ReceiveDate,ID");
            dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            dt.TableName = "fact";
            ds.Tables.Add(dt.Copy());

            dt = null;
            strSql.Clear();
            strSql.Append("select InvoiceCode,InvoiceDate,InvoiceNat from proj_SalesInvoice where FlagDel=0 and ContractId=" + ContractId.ToString() + " order by InvoiceDate,ID");
            dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            dt.TableName = "invoice";
            ds.Tables.Add(dt.Copy());

            return ds;

        }
        /// <summary>
        /// 获得合作单位合同的所有信息 主表、预算、实际、发票
        /// </summary>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        public DataSet GetPartnerContractAll(int ContractId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select b.ProjName,c.PartnerName,e.PartnerTypeName,d.CompanyName,a.ContractCode,a.ContractDate,a.ContractNat,a.StartDate,a.CompleteDate,a.Timescale,a.Memo ");
            strSql.Append("from proj_PartnerContract a ");
            strSql.Append("left join proj_Project b on a.ProjId=b.ID ");
            strSql.Append("left join base_Partner c on a.PartnerId=c.ID ");
            strSql.Append("left join base_Company d on a.CompanyId=d.ID ");
            strSql.Append("left join base_PartnerType e on a.PartnerTypeId=e.ID ");
            strSql.Append("where a.FlagDel=0 and a.ID=" + ContractId.ToString());
            DataSet ds = DbHelperSQL.Query(strSql.ToString());

            DataTable dt = null;
            strSql.Clear();
            strSql.Append("select Progress,PayDate,PayRate,PayNat from proj_PartnerContractDetail where FlagDel=0 and ContractId=" + ContractId.ToString() + " order by PayDate,ID");
            dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            dt.TableName = "budget";
            ds.Tables.Add(dt.Copy());

            dt = null;
            strSql.Clear();
            strSql.Append("select InvoiceCode,PayDate,PayNat from proj_Payment where FlagDel=0 and BillState=1 and ContractId=" + ContractId.ToString() + " order by PayDate,ID");
            dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            dt.TableName = "fact";
            ds.Tables.Add(dt.Copy());

            dt = null;
            strSql.Clear();
            strSql.Append("select InvoiceCode,InvoiceDate,InvoiceNat from proj_PurchaseInvoice where FlagDel=0 and ContractId=" + ContractId.ToString() + " order by InvoiceDate,ID");
            dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            dt.TableName = "invoice";
            ds.Tables.Add(dt.Copy());

            return ds;

        }
        /// <summary>
        /// 或得项目档案卡
        /// </summary>
        /// <param name="ProjId">项目ID</param>
        /// <returns></returns>
        public DataSet GetProjCard(int ProjId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select b.ProjName,c.CustName,d.CompanyName,a.ProjType,a.ContractCode,a.ContractDate,a.ContractNat,a.StartDate,a.CompleteDate,a.Timescale,a.Memo,e.PerName as SvcManagerName,f.PerName as ProjManagerName ");
            strSql.Append("from proj_Contract a ");
            strSql.Append("left join proj_Project b on a.ProjId=b.ID ");
            strSql.Append("left join base_Customer c on b.CustId=c.ID ");
            strSql.Append("left join base_Company d on a.CompanyId=d.ID ");
            strSql.Append("left join sys_Person e on b.SvcManagerId=e.ID ");
            strSql.Append("left join sys_Person f on b.ProjManagerId=f.ID ");
            strSql.Append("where a.FlagDel=0 and a.BillState=1 and a.ProjId=" + ProjId.ToString());
            DataSet ds = DbHelperSQL.Query(strSql.ToString());

            string procName = "p_proj_GetContractBalance";
            SqlParameter[] parameters = {
                    new SqlParameter("@projId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = ProjId;

            DataTable dt = DbHelperSQL.RunProcedure(procName, parameters, "dt").Tables[0];
            dt.TableName = "detail";
            ds.Tables.Add(dt.Copy());
            return ds;
        }
    }
}
