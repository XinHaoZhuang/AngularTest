using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.Repair
{
    /// <summary>
    /// 数据访问类repair_SettlementList。
    /// </summary>
    public partial class repair_SettlementList
    {
        public repair_SettlementList()
        { }
        #region  基本方法
        /// <summary>
        /// 某列是否存在
        /// </summary>
        public bool Exists(string FieldName, string FieldValue, int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from repair_SettlementList");
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
        public int Add(SCZM.Model.Repair.repair_SettlementList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into repair_SettlementList(");
            strSql.Append("SettlementCode,IntentionId,SettlementTypeId,SettlementFee,FlagSX,SettlementFee_rebate,SettlementDate,Memo,FlagDel,OperaDepId,OperaId,OperaName,OperaTime,AttachmentId_Settlement,TimeFee,PartFee)");
            strSql.Append(" values (");
            strSql.Append("@SettlementCode,@IntentionId,@SettlementTypeId,@SettlementFee,@FlagSX,@SettlementFee_rebate,@SettlementDate,@Memo,@FlagDel,@OperaDepId,@OperaId,@OperaName,@OperaTime,@AttachmentId_Settlement,@TimeFee,@PartFee)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
				new SqlParameter("@SettlementCode", SqlDbType.VarChar,30),
				new SqlParameter("@IntentionId", SqlDbType.Int,4),
				new SqlParameter("@SettlementTypeId", SqlDbType.Int,4),
				new SqlParameter("@SettlementFee", SqlDbType.Decimal,9),
				new SqlParameter("@FlagSX", SqlDbType.Int,4),
				new SqlParameter("@SettlementFee_rebate", SqlDbType.Decimal,9),
				new SqlParameter("@SettlementDate", SqlDbType.SmallDateTime),
				new SqlParameter("@Memo", SqlDbType.NVarChar,200),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaDepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
                new SqlParameter("@AttachmentId_Settlement",SqlDbType.VarChar,100),
                new SqlParameter("@TimeFee",SqlDbType.Decimal,18),
                new SqlParameter("@PartFee",SqlDbType.Decimal,18)};
            parameters[0].Value = model.SettlementCode;
            parameters[1].Value = model.IntentionId;
            parameters[2].Value = model.SettlementTypeId;
            parameters[3].Value = model.SettlementFee;
            parameters[4].Value = model.FlagSX;
            parameters[5].Value = model.SettlementFee_rebate;
            parameters[6].Value = model.SettlementDate;
            parameters[7].Value = model.Memo;
            parameters[8].Value = model.FlagDel;
            parameters[9].Value = model.OperaDepId;
            parameters[10].Value = model.OperaId;
            parameters[11].Value = model.OperaName;
            parameters[12].Value = model.OperaTime;
            parameters[13].Value = model.AttachmentId_Settlement;
            parameters[14].Value = model.TimeFee;
            parameters[15].Value = model.PartFee;
           
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
        public int Update(SCZM.Model.Repair.repair_SettlementList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update repair_SettlementList set ");
            strSql.Append("SettlementCode=@SettlementCode,");
            strSql.Append("IntentionId=@IntentionId,");
            strSql.Append("SettlementTypeId=@SettlementTypeId,");
            strSql.Append("SettlementFee=@SettlementFee,");
            strSql.Append("FlagSX=@FlagSX,");
            strSql.Append("SettlementFee_rebate=@SettlementFee_rebate,");
            strSql.Append("SettlementDate=@SettlementDate,");
            strSql.Append("Memo=@Memo,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaDepId=@OperaDepId,");
            strSql.Append("OperaId=@OperaId,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime,");
            strSql.Append("AttachmentId_Settlement=@AttachmentId_Settlement,");
            strSql.Append("TimeFee=@TimeFee,");
            strSql.Append("PartFee=@PartFee ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@SettlementCode", SqlDbType.VarChar,30),
				new SqlParameter("@IntentionId", SqlDbType.Int,4),
				new SqlParameter("@SettlementTypeId", SqlDbType.Int,4),
				new SqlParameter("@SettlementFee", SqlDbType.Decimal,9),
				new SqlParameter("@FlagSX", SqlDbType.Int,4),
				new SqlParameter("@SettlementFee_rebate", SqlDbType.Decimal,9),
				new SqlParameter("@SettlementDate", SqlDbType.SmallDateTime),
				new SqlParameter("@Memo", SqlDbType.NVarChar,200),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaDepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
                new SqlParameter("@AttachmentId_Settlement",SqlDbType.VarChar,100),
                new SqlParameter("@TimeFee",SqlDbType.Decimal,18),
                new SqlParameter("@PartFee",SqlDbType.Decimal,18),
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.SettlementCode;
            parameters[1].Value = model.IntentionId;
            parameters[2].Value = model.SettlementTypeId;
            parameters[3].Value = model.SettlementFee;
            parameters[4].Value = model.FlagSX;
            parameters[5].Value = model.SettlementFee_rebate;
            parameters[6].Value = model.SettlementDate;
            parameters[7].Value = model.Memo;
            parameters[8].Value = model.FlagDel;
            parameters[9].Value = model.OperaDepId;
            parameters[10].Value = model.OperaId;
            parameters[11].Value = model.OperaName;
            parameters[12].Value = model.OperaTime;
            parameters[13].Value = model.AttachmentId_Settlement;
            parameters[14].Value = model.TimeFee;
            parameters[15].Value = model.PartFee;
            parameters[16].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SCZM.Model.Repair.repair_SettlementList GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,SettlementCode,IntentionId,SettlementTypeId,SettlementFee,FlagSX,SettlementFee_rebate,SettlementDate,Memo,FlagDel,OperaDepId,OperaId,OperaName,OperaTime from repair_SettlementList,TimeFee,PartFee ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.Repair.repair_SettlementList model = new SCZM.Model.Repair.repair_SettlementList();
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
        public SCZM.Model.Repair.repair_SettlementList DataRowToModel(DataRow row)
        {
            SCZM.Model.Repair.repair_SettlementList model = new SCZM.Model.Repair.repair_SettlementList();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["SettlementCode"] != null)
                {
                    model.SettlementCode = row["SettlementCode"].ToString();
                }
                if (row["IntentionId"] != null && row["IntentionId"].ToString() != "")
                {
                    model.IntentionId = int.Parse(row["IntentionId"].ToString());
                }
                if (row["SettlementTypeId"] != null && row["SettlementTypeId"].ToString() != "")
                {
                    model.SettlementTypeId = int.Parse(row["SettlementTypeId"].ToString());
                }
                if (row["SettlementFee"] != null && row["SettlementFee"].ToString() != "")
                {
                    model.SettlementFee = decimal.Parse(row["SettlementFee"].ToString());
                }
                if (row["FlagSX"] != null && row["FlagSX"].ToString() != "")
                {
                    model.FlagSX = int.Parse(row["FlagSX"].ToString());
                }
                if (row["SettlementFee_rebate"] != null && row["SettlementFee_rebate"].ToString() != "")
                {
                    model.SettlementFee_rebate = decimal.Parse(row["SettlementFee_rebate"].ToString());
                }
                if (row["SettlementDate"] != null && row["SettlementDate"].ToString() != "")
                {
                    model.SettlementDate = DateTime.Parse(row["SettlementDate"].ToString());
                }
                if (row["Memo"] != null)
                {
                    model.Memo = row["Memo"].ToString();
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
                if (row["TimeFee"] != null && row["TimeFee"].ToString() != "")
                {
                    model.TimeFee = decimal.Parse(row["TimeFee"].ToString());
                }
                if (row["PartFee"] != null && row["PartFee"].ToString() != "")
                {
                    model.PartFee = decimal.Parse(row["PartFee"].ToString());
                }
            }
            return model;
        }

        #endregion  基本方法
        #region  扩展方法
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public int DeleteList(string IDList,string IntentionIdList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update repair_SettlementList set FlagDel=1 ");
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
            strSql.Append("select a.ID,a.SettlementCode,a.IntentionId,b.IntentionCode,b.CustTypeId,b.CustName,b.MachineCode,c.MachineModel,a.SettlementTypeId,d.SettlementName,a.SettlementFee,a.FlagSX,a.SettlementFee_rebate,a.SettlementDate,a.Memo,a.OperaDepId,a.OperaId,a.OperaName,a.OperaTime,a.TimeFee,a.PartFee ");
            strSql.Append("FROM repair_SettlementList a ");
            strSql.Append("left join repair_Intention b on a.IntentionId=b.ID and b.FlagDel=0 ");
            strSql.Append("left join base_MachineModel c on b.MachineModelId=c.ID and c.FlagDel=0 ");
            strSql.Append("left join base_Settlement d on a.SettlementTypeId=d.ID and d.FlagDel=0 ");
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
            strSql.Append("select a.ID,a.SettlementCode,a.IntentionId,b.IntentionCode,b.CustTypeId,b.CustName,b.MachineCode,c.MachineModel,e.ID as SchemeId,a.SettlementTypeId,a.SettlementFee,a.FlagSX,a.SettlementFee_rebate,a.SettlementDate,a.Memo,a.OperaDepId,a.OperaId,a.OperaName,a.OperaTime,a.AttachmentId_Settlement,a.TimeFee,a.PartFee ");
            strSql.Append("FROM repair_SettlementList a ");
            strSql.Append("left join repair_Intention b on a.IntentionId=b.ID and b.FlagDel=0 ");
            strSql.Append("left join base_MachineModel c on b.MachineModelId=c.ID and c.FlagDel=0 ");
            strSql.Append("left join Repair_Scheme e on a.IntentionId=e.IntentionId and e.FlagDel=0 ");
            strSql.Append("where a.FlagDel=0 and a.ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds=DbHelperSQL.Query(strSql.ToString(), parameters);

            string AttachmentId = "";
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                AttachmentId = (ds.Tables[0].Rows[0]["AttachmentId_Settlement"].ToString() == "" ? "" : ds.Tables[0].Rows[0]["AttachmentId_Settlement"].ToString() + ",")
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
            return DbHelperSQL.GetMaxID("ID", "repair_SettlementList");
        }
        #endregion  扩展方法
    }
}

