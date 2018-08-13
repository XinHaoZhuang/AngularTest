using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.Repair
{
    /// <summary>
    /// 数据访问类repair_Contract。
    /// </summary>
    public partial class repair_Contract
    {
        public repair_Contract()
        { }
        #region  基本方法
        /// <summary>
        /// 某列是否存在
        /// </summary>
        public bool Exists(string FieldName, string FieldValue, int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from repair_Contract");
            strSql.Append(" where FlagDel=0 and  " + FieldName + "=@" + FieldName + " and ID<>@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@" + FieldName, SqlDbType.VarChar),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = FieldValue;
            parameters[1].Value = ID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SCZM.Model.Repair.repair_Contract model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into repair_Contract(");
            strSql.Append("IntentionId,WarrantyPeriod,WarrantyContent,AttachmentId_Contract,ContractDate,FlagDel,OperaDepId,OperaId,OperaName,OperaTime,ContractCode)");
            strSql.Append(" values (");
            strSql.Append("@IntentionId,@WarrantyPeriod,@WarrantyContent,@AttachmentId_Contract,@ContractDate,@FlagDel,@OperaDepId,@OperaId,@OperaName,@OperaTime,@ContractCode)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
				new SqlParameter("@IntentionId", SqlDbType.Int,4),
				new SqlParameter("@WarrantyPeriod", SqlDbType.Int,4),
				new SqlParameter("@WarrantyContent", SqlDbType.NVarChar,500),
				new SqlParameter("@AttachmentId_Contract", SqlDbType.VarChar,100),
				new SqlParameter("@ContractDate", SqlDbType.SmallDateTime),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaDepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
                new SqlParameter("@ContractCode",SqlDbType.VarChar,30)};
            parameters[0].Value = model.IntentionId;
            parameters[1].Value = model.WarrantyPeriod;
            parameters[2].Value = model.WarrantyContent;
            parameters[3].Value = model.AttachmentId_Contract;
            parameters[4].Value = model.ContractDate;
            parameters[5].Value = model.FlagDel;
            parameters[6].Value = model.OperaDepId;
            parameters[7].Value = model.OperaId;
            parameters[8].Value = model.OperaName;
            parameters[9].Value = model.OperaTime;
            parameters[10].Value = model.ContractCode;

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
        public int Update(SCZM.Model.Repair.repair_Contract model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update repair_Contract set ");
            strSql.Append("IntentionId=@IntentionId,");
            strSql.Append("WarrantyPeriod=@WarrantyPeriod,");
            strSql.Append("WarrantyContent=@WarrantyContent,");
            strSql.Append("AttachmentId_Contract=@AttachmentId_Contract,");
            strSql.Append("ContractDate=@ContractDate,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaDepId=@OperaDepId,");
            strSql.Append("OperaId=@OperaId,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime,");
            strSql.Append("ContractCode=@ContractCode");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@IntentionId", SqlDbType.Int,4),
				new SqlParameter("@WarrantyPeriod", SqlDbType.Int,4),
				new SqlParameter("@WarrantyContent", SqlDbType.NVarChar,500),
				new SqlParameter("@AttachmentId_Contract", SqlDbType.VarChar,100),
				new SqlParameter("@ContractDate", SqlDbType.SmallDateTime),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaDepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
                new SqlParameter("@ContractCode",SqlDbType.VarChar,30),
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.IntentionId;
            parameters[1].Value = model.WarrantyPeriod;
            parameters[2].Value = model.WarrantyContent;
            parameters[3].Value = model.AttachmentId_Contract;
            parameters[4].Value = model.ContractDate;
            parameters[5].Value = model.FlagDel;
            parameters[6].Value = model.OperaDepId;
            parameters[7].Value = model.OperaId;
            parameters[8].Value = model.OperaName;
            parameters[9].Value = model.OperaTime;
            parameters[10].Value = model.ContractCode;
            parameters[11].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SCZM.Model.Repair.repair_Contract GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,IntentionId,ContractCode,WarrantyPeriod,WarrantyContent,AttachmentId_Contract,ContractDate,FlagDel,OperaDepId,OperaId,OperaName,OperaTime from repair_Contract ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.Repair.repair_Contract model = new SCZM.Model.Repair.repair_Contract();
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
        public SCZM.Model.Repair.repair_Contract DataRowToModel(DataRow row)
        {
            SCZM.Model.Repair.repair_Contract model = new SCZM.Model.Repair.repair_Contract();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["IntentionId"] != null && row["IntentionId"].ToString() != "")
                {
                    model.IntentionId = int.Parse(row["IntentionId"].ToString());
                }
                if (row["ContractCode"] != null && row["ContractCode"].ToString() != "")
                {
                    model.ContractCode = row["ContractCode"].ToString();
                }
                if (row["WarrantyPeriod"] != null && row["WarrantyPeriod"].ToString() != "")
                {
                    model.WarrantyPeriod = int.Parse(row["WarrantyPeriod"].ToString());
                }
                if (row["WarrantyContent"] != null)
                {
                    model.WarrantyContent = row["WarrantyContent"].ToString();
                }
                if (row["AttachmentId_Contract"] != null)
                {
                    model.AttachmentId_Contract = row["AttachmentId_Contract"].ToString();
                }
                if (row["ContractDate"] != null && row["ContractDate"].ToString() != "")
                {
                    model.ContractDate = DateTime.Parse(row["ContractDate"].ToString());
                }
                if (row["FlagDel"] != null && row["FlagDel"].ToString() != "")
                {
                    model.FlagDel = int.Parse(row["FlagDel"].ToString());
                }
                if (row["OperaDepId"] != null && row["OperaDepId"].ToString() != "")
                {
                    model.OperaDepId = int.Parse(row["OperaDepId"].ToString());
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
            strSql.Append("update repair_Contract set FlagDel=1 ");
            strSql.Append(" where FlagDel=0 and ID in(" + IDList + ")");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }

        /// <summary>
        /// 获得数据列表 通过Where条件
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.IntentionId,a.ContractCode,a.WarrantyPeriod,a.WarrantyContent,a.AttachmentId_Contract,a.ContractDate,b.IntentionCode,b.CustTypeId,b.CustName,c.MachineModel,b.MachineCode,d.SettlementFee_rebate,a.OperaDepId,a.OperaId,a.OperaName,a.OperaTime ");
            strSql.Append("FROM repair_Contract a ");
            strSql.Append("left join repair_Intention b on a.IntentionId=b.ID and b.FlagDel=0 ");
            strSql.Append("left join base_MachineModel c on b.MachineModelId=c.ID and c.FlagDel=0 ");
            strSql.Append("left join repair_SettlementList d on a.IntentionId=d.IntentionId and d.FlagDel=0 ");
            strSql.Append("where a.FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据明细 通过ID
        /// </summary>
        public DataSet GetDetail(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.IntentionId,a.ContractCode,a.WarrantyPeriod,a.WarrantyContent,a.AttachmentId_Contract,a.ContractDate,b.IntentionCode,b.CustTypeId,b.CustName,c.MachineModel,b.MachineCode,d.SettlementFee_rebate,a.OperaDepId,a.OperaId,a.OperaName,a.OperaTime ");
            strSql.Append("FROM repair_Contract a ");
            strSql.Append("left join repair_Intention b on a.IntentionId=b.ID and b.FlagDel=0 ");
            strSql.Append("left join base_MachineModel c on b.MachineModelId=c.ID and c.FlagDel=0 ");
            strSql.Append("left join repair_SettlementList d on a.IntentionId=d.IntentionId and d.FlagDel=0 ");
            strSql.Append("where a.FlagDel=0 and a.ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            string AttachmentId = "";
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                AttachmentId = (ds.Tables[0].Rows[0]["AttachmentId_Contract"].ToString() == "" ? "" : ds.Tables[0].Rows[0]["AttachmentId_Contract"].ToString() + ",")
                    ;
            }
            DataTable dt = new System.sys_Attachment().GetAttachment(AttachmentId);
            dt.TableName = "attachment";
            ds.Tables.Add(dt.Copy());
            return ds;
        }
        /// <summary>
        /// 获取最大ID+1
        /// </summary>
        /// <returns></returns>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "repair_Contract");
        }
        public DataSet GetIntentionCode_LastCombo(string strWhere) {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select a.ID,a.IntentionId,a.ContractCode,a.WarrantyPeriod,a.WarrantyContent,a.AttachmentId_Contract,a.ContractDate,b.IntentionCode,b.CustTypeId,b.CustName,c.MachineModel,b.MachineCode,d.SettlementFee_rebate,a.OperaDepId,a.OperaId,a.OperaName,a.OperaTime ");
            strSql.Append("select a.ContractCode ");
            strSql.Append("FROM repair_Contract a ");
            strSql.Append("left join repair_Intention b on a.IntentionId=b.ID and b.FlagDel=0 ");
            //strSql.Append("left join base_MachineModel c on b.MachineModelId=c.ID and c.FlagDel=0 ");
            //strSql.Append("left join repair_SettlementList d on a.IntentionId=d.IntentionId and d.FlagDel=0 ");
            strSql.Append("where a.FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  扩展方法
    }
}

