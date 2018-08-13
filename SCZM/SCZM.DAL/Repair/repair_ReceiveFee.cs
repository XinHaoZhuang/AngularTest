using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.Repair
{
    /// <summary>
    /// 数据访问类repair_ReceiveFee。
    /// </summary>
    public partial class repair_ReceiveFee
    {
        public repair_ReceiveFee()
        { }
        #region  基本方法
        /// <summary>
        /// 某列是否存在
        /// </summary>
        public bool Exists(string FieldName, string FieldValue, int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from repair_ReceiveFee");
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
        public int Add(SCZM.Model.Repair.repair_ReceiveFee model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into repair_ReceiveFee(");
            strSql.Append("IntentionId,PayType,ReceiveFee,ReceiveDate,FlagDel,OperaDepId,OperaId,OperaName,OperaTime)");
            strSql.Append(" values (");
            strSql.Append("@IntentionId,@PayType,@ReceiveFee,@ReceiveDate,@FlagDel,@OperaDepId,@OperaId,@OperaName,@OperaTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
				new SqlParameter("@IntentionId", SqlDbType.Int,4),
				new SqlParameter("@PayType", SqlDbType.Int,4),
				new SqlParameter("@ReceiveFee", SqlDbType.Decimal,9),
				new SqlParameter("@ReceiveDate", SqlDbType.SmallDateTime),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaDepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime)};
            parameters[0].Value = model.IntentionId;
            parameters[1].Value = model.PayType;
            parameters[2].Value = model.ReceiveFee;
            parameters[3].Value = model.ReceiveDate;
            parameters[4].Value = model.FlagDel;
            parameters[5].Value = model.OperaDepId;
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
                //string strSql0 = "update repair_Intention set RepairState=50 where FlagDel=0 and ID=" + model.IntentionId;
                //DbHelperSQL.ExecuteSql(strSql0);
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(SCZM.Model.Repair.repair_ReceiveFee model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update repair_ReceiveFee set ");
            strSql.Append("IntentionId=@IntentionId,");
            strSql.Append("PayType=@PayType,");
            strSql.Append("ReceiveFee=@ReceiveFee,");
            strSql.Append("ReceiveDate=@ReceiveDate,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaDepId=@OperaDepId,");
            strSql.Append("OperaId=@OperaId,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@IntentionId", SqlDbType.Int,4),
				new SqlParameter("@PayType", SqlDbType.Int,4),
				new SqlParameter("@ReceiveFee", SqlDbType.Decimal,9),
				new SqlParameter("@ReceiveDate", SqlDbType.SmallDateTime),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaDepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.IntentionId;
            parameters[1].Value = model.PayType;
            parameters[2].Value = model.ReceiveFee;
            parameters[3].Value = model.ReceiveDate;
            parameters[4].Value = model.FlagDel;
            parameters[5].Value = model.OperaDepId;
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
        public SCZM.Model.Repair.repair_ReceiveFee GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,IntentionId,PayType,ReceiveFee,ReceiveDate,FlagDel,OperaDepId,OperaId,OperaName,OperaTime from repair_ReceiveFee ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.Repair.repair_ReceiveFee model = new SCZM.Model.Repair.repair_ReceiveFee();
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
        public SCZM.Model.Repair.repair_ReceiveFee DataRowToModel(DataRow row)
        {
            SCZM.Model.Repair.repair_ReceiveFee model = new SCZM.Model.Repair.repair_ReceiveFee();
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
                if (row["PayType"] != null && row["PayType"].ToString() != "")
                {
                    model.PayType = int.Parse(row["PayType"].ToString());
                }
                if (row["ReceiveFee"] != null && row["ReceiveFee"].ToString() != "")
                {
                    model.ReceiveFee = decimal.Parse(row["ReceiveFee"].ToString());
                }
                if (row["ReceiveDate"] != null && row["ReceiveDate"].ToString() != "")
                {
                    model.ReceiveDate = DateTime.Parse(row["ReceiveDate"].ToString());
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
            strSql.Append("update repair_ReceiveFee set FlagDel=1 ");
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
            strSql.Append("select a.ID,a.IntentionId,a.PayType,a.ReceiveFee,a.ReceiveDate,b.IntentionCode,b.CustTypeId,b.CustName,c.MachineModel,b.MachineCode,d.SettlementFee_rebate,a.OperaDepId,a.OperaId,a.OperaName,a.OperaTime ");
            strSql.Append("FROM repair_ReceiveFee a ");
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
            strSql.Append("select a.ID,a.IntentionId,a.PayType,a.ReceiveFee,a.ReceiveDate,b.IntentionCode,b.CustTypeId,b.CustName,c.MachineModel,b.MachineCode,d.SettlementFee_rebate,a.OperaDepId,a.OperaId,a.OperaName,a.OperaTime ");
            strSql.Append("FROM repair_ReceiveFee a ");
            strSql.Append("left join repair_Intention b on a.IntentionId=b.ID and b.FlagDel=0 ");
            strSql.Append("left join base_MachineModel c on b.MachineModelId=c.ID and c.FlagDel=0 ");
            strSql.Append("left join repair_SettlementList d on a.IntentionId=d.IntentionId and d.FlagDel=0 ");
            strSql.Append("where a.FlagDel=0 and a.ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        #endregion  扩展方法
    }
}

