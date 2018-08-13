using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.Proj
{
    /// <summary>
    /// 数据访问类proj_Receipts。
    /// </summary>
    public partial class proj_Receipts
    {
        public proj_Receipts()
        { }
        #region  基本方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SCZM.Model.Proj.proj_Receipts model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into proj_Receipts(");
            strSql.Append("ContractId,ReceiveDate,SettlementTypeId,ReceiveNat,Memo,FlagDel,OperaId,OperaName,OperaTime)");
            strSql.Append(" values (");
            strSql.Append("@ContractId,@ReceiveDate,@SettlementTypeId,@ReceiveNat,@Memo,@FlagDel,@OperaId,@OperaName,@OperaTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
				new SqlParameter("@ContractId", SqlDbType.Int,4),
				new SqlParameter("@ReceiveDate", SqlDbType.SmallDateTime),
                new SqlParameter("@SettlementTypeId",SqlDbType.Int,4),
				new SqlParameter("@ReceiveNat", SqlDbType.Decimal,9),
				new SqlParameter("@Memo", SqlDbType.NVarChar,200),
				new SqlParameter("@FlagDel", SqlDbType.Bit,1),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
				new SqlParameter("@OperaTime", SqlDbType.DateTime)};
            parameters[0].Value = model.ContractId;
            parameters[1].Value = model.ReceiveDate;
            parameters[2].Value = model.SettlementTypeId;
            parameters[3].Value = model.ReceiveNat;
            parameters[4].Value = model.Memo;
            parameters[5].Value = model.FlagDel;
            parameters[6].Value = model.OperaId;
            parameters[7].Value = model.OperaName;
            parameters[8].Value = model.OperaTime;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(SCZM.Model.Proj.proj_Receipts model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update proj_Receipts set ");
            strSql.Append("ContractId=@ContractId,");
            strSql.Append("ReceiveDate=@ReceiveDate,");
            strSql.Append("SettlementTypeId=@SettlementTypeId,");
            strSql.Append("ReceiveNat=@ReceiveNat,");
            strSql.Append("Memo=@Memo,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaId=@OperaId,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ContractId", SqlDbType.Int,4),
				new SqlParameter("@ReceiveDate", SqlDbType.SmallDateTime),
                new SqlParameter("@SettlementTypeId",SqlDbType.Int,4),
				new SqlParameter("@ReceiveNat", SqlDbType.Decimal,9),
				new SqlParameter("@Memo", SqlDbType.NVarChar,200),
				new SqlParameter("@FlagDel", SqlDbType.Bit,1),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.ContractId;
            parameters[1].Value = model.ReceiveDate;
            parameters[2].Value = model.SettlementTypeId;
            parameters[3].Value = model.ReceiveNat;
            parameters[4].Value = model.Memo;
            parameters[5].Value = model.FlagDel;
            parameters[6].Value = model.OperaId;
            parameters[7].Value = model.OperaName;
            parameters[8].Value = model.OperaTime;
            parameters[9].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SCZM.Model.Proj.proj_Receipts GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ContractId,ReceiveDate,SettlementTypeId,ReceiveNat,Memo,FlagDel,OperaId,OperaName,OperaTime from proj_Receipts ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.Proj.proj_Receipts model = new SCZM.Model.Proj.proj_Receipts();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体 子方法 从DataRow中
        /// </summary>
        public SCZM.Model.Proj.proj_Receipts DataRowToModel(DataRow row)
        {
            SCZM.Model.Proj.proj_Receipts model = new SCZM.Model.Proj.proj_Receipts();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["ContractId"] != null && row["ContractId"].ToString() != "")
                {
                    model.ContractId = int.Parse(row["ContractId"].ToString());
                }
                if (row["ReceiveDate"] != null && row["ReceiveDate"].ToString() != "")
                {
                    model.ReceiveDate = DateTime.Parse(row["ReceiveDate"].ToString());
                }
                if (row["SettlementTypeId"] != null && row["SettlementTypeId"].ToString() != "")
                {
                    model.SettlementTypeId = int.Parse(row["SettlementTypeId"].ToString());
                }
                if (row["ReceiveNat"] != null && row["ReceiveNat"].ToString() != "")
                {
                    model.ReceiveNat = decimal.Parse(row["ReceiveNat"].ToString());
                }
                if (row["Memo"] != null)
                {
                    model.Memo = row["Memo"].ToString();
                }
                if (row["FlagDel"] != null && row["FlagDel"].ToString() != "")
                {
                    if ((row["FlagDel"].ToString() == "1") || (row["FlagDel"].ToString().ToLower() == "true"))
                    {
                        model.FlagDel = true;
                    }
                    else
                    {
                        model.FlagDel = false;
                    }
                }
                if (row["OperaId"] != null && row["OperaId"].ToString() != "")
                {
                    model.OperaId = int.Parse(row["OperaId"].ToString());
                }
                if (row["OperaName"] != null)
                {
                    model.OperaName = row["OperaName"].ToString();
                }
                if (row["OperaTime"] != null && row["OperaTime"].ToString() != "")
                {
                    model.OperaTime = DateTime.Parse(row["OperaTime"].ToString());
                }
            }
            return model;
        }

        #endregion  基本方法
        #region  扩展方法
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public int DeleteList(string IDList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update proj_Receipts set FlagDel=1 ");
            strSql.Append(" where FlagDel=0 and ID in(" + IDList + ")");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }

        /// <summary>
        /// 获得数据列表 通过Where条件
        /// </summary>
        public DataSet GetList(string strWhere, int operaId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,b.ContractCode,c.ProjName,d.CustName,a.ReceiveDate,e.SettlementTypeName,a.ReceiveNat,a.OperaName,a.OperaTime ");
            strSql.Append("FROM proj_Receipts a ");
            strSql.Append("left join proj_Contract b on a.ContractId=b.ID ");
            strSql.Append("left join proj_Project c on b.ProjId=c.ID ");
            strSql.Append("left join base_Customer d on c.CustId=d.ID ");
            strSql.Append("left join base_SettlementType e on a.SettlementTypeId=e.ID ");
            strSql.Append("where a.FlagDel=0 and c.ProjManagerId in(select e.CtrlPerId from v_sys_PersonCtrl e where e.PerId=" + operaId.ToString() + ") ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.OperaTime desc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据明细 通过ID
        /// </summary>
        public DataSet GetDetail(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ReceiveDate,a.SettlementTypeId,a.ReceiveNat,a.Memo,a.OperaName,a.OperaTime,b.ID as ContractId,b.ContractCode,g.CompanyName,b.ContractDate,b.ContractNat,c.ProjName,d.CustName,e.PerName as SvcManagerName,f.PerName as ProjManagerName ");
            strSql.Append("FROM proj_Receipts a ");
            strSql.Append("left join proj_Contract b on a.ContractId=b.ID ");
            strSql.Append("left join proj_Project c on b.ProjId=c.ID ");
            strSql.Append("left join base_Customer d on c.CustId=d.ID ");
            strSql.Append("left join sys_Person e on c.SvcManagerId=e.ID ");
            strSql.Append("left join sys_Person f on c.ProjManagerId=f.ID ");
            strSql.Append("left join base_Company g on b.CompanyId=g.ID ");
            strSql.Append("where a.FlagDel=0 and a.ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        #endregion  扩展方法
    }
}

