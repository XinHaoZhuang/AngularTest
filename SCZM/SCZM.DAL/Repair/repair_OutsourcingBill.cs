using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.Repair
{
    /// <summary>
    /// 数据访问类repair_OutsourcingBill。
    /// </summary>
    public partial class repair_OutsourcingBill
    {
        public repair_OutsourcingBill()
        { }
        #region  基本方法
        /// <summary>
        /// 某列是否存在
        /// </summary>
        public bool Exists(string FieldName, string FieldValue, int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from repair_OutsourcingBill");
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
        public int Add(SCZM.Model.Repair.repair_OutsourcingBill model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into repair_OutsourcingBill(");
            strSql.Append("IntentionId,HappendDate,Address,FeeItemId,Plant,PlantContent,PayFee,SystemFee,XsSp,ReimbursementDate,Memo,FlagDel,OperaDepId,OperaId,OperaName,OperaTime,FlagRepair,FlagZB,FlagOther)");
            strSql.Append(" values (");
            strSql.Append("@IntentionId,@HappendDate,@Address,@FeeItemId,@Plant,@PlantContent,@PayFee,@SystemFee,@XsSp,@ReimbursementDate,@Memo,@FlagDel,@OperaDepId,@OperaId,@OperaName,@OperaTime,@FlagRepair,@FlagZB,@FlagOther)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
				new SqlParameter("@IntentionId", SqlDbType.Int,4),
				new SqlParameter("@HappendDate", SqlDbType.SmallDateTime),
				new SqlParameter("@Address", SqlDbType.NVarChar,10),
				new SqlParameter("@FeeItemId", SqlDbType.Int,4),
				new SqlParameter("@Plant", SqlDbType.NVarChar,30),
				new SqlParameter("@PlantContent", SqlDbType.NVarChar,200),
				new SqlParameter("@PayFee", SqlDbType.Decimal,9),
				new SqlParameter("@SystemFee", SqlDbType.Decimal,9),
				new SqlParameter("@XsSp", SqlDbType.Int,4),
				new SqlParameter("@ReimbursementDate", SqlDbType.SmallDateTime),
				new SqlParameter("@Memo", SqlDbType.NVarChar,200),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaDepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
                                        new SqlParameter("@FlagRepair",SqlDbType.Int,4),
                                        new SqlParameter("@FlagZB",SqlDbType.Int,4),
                                        new SqlParameter("@FlagOther",SqlDbType.Int,4)};
            parameters[0].Value = model.IntentionId;
            parameters[1].Value = model.HappendDate;
            parameters[2].Value = model.Address;
            parameters[3].Value = model.FeeItemId;
            parameters[4].Value = model.Plant;
            parameters[5].Value = model.PlantContent;
            parameters[6].Value = model.PayFee;
            parameters[7].Value = model.SystemFee;
            parameters[8].Value = model.XsSp;
            parameters[9].Value = model.ReimbursementDate;
            parameters[10].Value = model.Memo;
            parameters[11].Value = model.FlagDel;
            parameters[12].Value = model.OperaDepId;
            parameters[13].Value = model.OperaId;
            parameters[14].Value = model.OperaName;
            parameters[15].Value = model.OperaTime;
            parameters[16].Value = model.FlagRepair;
            parameters[17].Value = model.FlagZB;
            parameters[18].Value = model.FlagOther;

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
        public int Update(SCZM.Model.Repair.repair_OutsourcingBill model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update repair_OutsourcingBill set ");
            strSql.Append("IntentionId=@IntentionId,");
            strSql.Append("HappendDate=@HappendDate,");
            strSql.Append("Address=@Address,");
            strSql.Append("FeeItemId=@FeeItemId,");
            strSql.Append("Plant=@Plant,");
            strSql.Append("PlantContent=@PlantContent,");
            strSql.Append("PayFee=@PayFee,");
            strSql.Append("SystemFee=@SystemFee,");
            strSql.Append("XsSp=@XsSp,");
            strSql.Append("ReimbursementDate=@ReimbursementDate,");
            strSql.Append("Memo=@Memo,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaDepId=@OperaDepId,");
            strSql.Append("OperaId=@OperaId,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime,");
            strSql.Append("FlagRepair=@FlagRepair,");
            strSql.Append("FlagZB=@FlagZB,");
            strSql.Append("FlagOther=@FlagOther");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@IntentionId", SqlDbType.Int,4),
				new SqlParameter("@HappendDate", SqlDbType.SmallDateTime),
				new SqlParameter("@Address", SqlDbType.NVarChar,10),
				new SqlParameter("@FeeItemId", SqlDbType.Int,4),
				new SqlParameter("@Plant", SqlDbType.NVarChar,30),
				new SqlParameter("@PlantContent", SqlDbType.NVarChar,200),
				new SqlParameter("@PayFee", SqlDbType.Decimal,9),
				new SqlParameter("@SystemFee", SqlDbType.Decimal,9),
				new SqlParameter("@XsSp", SqlDbType.Int,4),
				new SqlParameter("@ReimbursementDate", SqlDbType.SmallDateTime),
				new SqlParameter("@Memo", SqlDbType.NVarChar,200),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaDepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
                new SqlParameter("@FlagRepair",SqlDbType.Int,4),
                new SqlParameter("@FlagZB",SqlDbType.Int,4),
                new SqlParameter("@FlagOther",SqlDbType.Int,4),
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.IntentionId;
            parameters[1].Value = model.HappendDate;
            parameters[2].Value = model.Address;
            parameters[3].Value = model.FeeItemId;
            parameters[4].Value = model.Plant;
            parameters[5].Value = model.PlantContent;
            parameters[6].Value = model.PayFee;
            parameters[7].Value = model.SystemFee;
            parameters[8].Value = model.XsSp;
            parameters[9].Value = model.ReimbursementDate;
            parameters[10].Value = model.Memo;
            parameters[11].Value = model.FlagDel;
            parameters[12].Value = model.OperaDepId;
            parameters[13].Value = model.OperaId;
            parameters[14].Value = model.OperaName;
            parameters[15].Value = model.OperaTime;
            parameters[16].Value = model.FlagRepair;
            parameters[17].Value = model.FlagZB;
            parameters[18].Value = model.FlagOther;
            parameters[19].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SCZM.Model.Repair.repair_OutsourcingBill GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,IntentionId,HappendDate,Address,FeeItemId,Plant,PlantContent,PayFee,SystemFee,XsSp,FlagRepair,FlagZB,FlagOther,ReimbursementDate,Memo,FlagDel,OperaDepId,OperaId,OperaName,OperaTime from repair_OutsourcingBill ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.Repair.repair_OutsourcingBill model = new SCZM.Model.Repair.repair_OutsourcingBill();
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
        public SCZM.Model.Repair.repair_OutsourcingBill DataRowToModel(DataRow row)
        {
            SCZM.Model.Repair.repair_OutsourcingBill model = new SCZM.Model.Repair.repair_OutsourcingBill();
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
                if (row["HappendDate"] != null && row["HappendDate"].ToString() != "")
                {
                    model.HappendDate = DateTime.Parse(row["HappendDate"].ToString());
                }
                if (row["Address"] != null)
                {
                    model.Address = row["Address"].ToString();
                }
                if (row["FeeItemId"] != null && row["FeeItemId"].ToString() != "")
                {
                    model.FeeItemId = int.Parse(row["FeeItemId"].ToString());
                }
                if (row["Plant"] != null)
                {
                    model.Plant = row["Plant"].ToString();
                }
                if (row["PlantContent"] != null)
                {
                    model.PlantContent = row["PlantContent"].ToString();
                }
                if (row["PayFee"] != null && row["PayFee"].ToString() != "")
                {
                    model.PayFee = decimal.Parse(row["PayFee"].ToString());
                }
                if (row["SystemFee"] != null && row["SystemFee"].ToString() != "")
                {
                    model.SystemFee = decimal.Parse(row["SystemFee"].ToString());
                }
                if (row["XsSp"] != null && row["XsSp"].ToString() != "")
                {
                    model.XsSp = int.Parse(row["XsSp"].ToString());
                }
                if (row["FlagRepair"] != null && row["FlagRepair"].ToString() != "")
                {
                    model.FlagRepair = int.Parse(row["FlagRepair"].ToString());
                }
                if (row["FlagZB"] != null && row["FlagZB"].ToString() != "")
                {
                    model.FlagZB = int.Parse(row["FlagZB"].ToString());
                }
                if (row["FlagOther"] != null && row["FlagOther"].ToString() != "")
                {
                    model.FlagOther = int.Parse(row["FlagOther"].ToString());
                }
                if (row["ReimbursementDate"] != null && row["ReimbursementDate"].ToString() != "")
                {
                    model.ReimbursementDate = DateTime.Parse(row["ReimbursementDate"].ToString());
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
            strSql.Append("update repair_OutsourcingBill set FlagDel=1 ");
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
            strSql.Append("select a.ID,a.IntentionId,b.CustTypeId,b.CustName,b.IntentionCode,b.MachineCode,c.MachineModel,d.FeeItemName,a.HappendDate,a.Address,a.FeeItemId,a.Plant,a.PlantContent,a.PayFee,a.SystemFee,a.XsSp,a.FlagRepair,a.FlagZB,a.FlagOther,a.ReimbursementDate,a.Memo,a.OperaDepId,a.OperaId,a.OperaName,a.OperaTime ");
            strSql.Append("FROM repair_OutsourcingBill a ");
            strSql.Append("left join repair_Intention b on a.IntentionId=b.ID and b.FlagDel=0 ");
            strSql.Append("left join base_MachineModel c on b.MachineModelId=c.ID and c.FlagDel=0 ");
            strSql.Append("left join base_FeeItem d on a.FeeItemId=d.ID and d.FlagDel=0 ");
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
            strSql.Append("select a.ID,a.IntentionId,b.CustTypeId,b.CustName,b.IntentionCode,b.MachineCode,c.MachineModel,d.FeeItemName,a.HappendDate,a.Address,a.FeeItemId,a.Plant,a.PlantContent,a.PayFee,a.SystemFee,a.XsSp,a.FlagRepair,a.FlagZB,a.FlagOther,a.ReimbursementDate,a.Memo,a.OperaDepId,a.OperaId,a.OperaName,a.OperaTime ");
            strSql.Append("FROM repair_OutsourcingBill a ");
            strSql.Append("left join repair_Intention b on a.IntentionId=b.ID and b.FlagDel=0 ");
            strSql.Append("left join base_MachineModel c on b.MachineModelId=c.ID and c.FlagDel=0 ");
            strSql.Append("left join base_FeeItem d on a.FeeItemId=d.ID and d.FlagDel=0 ");
            strSql.Append("where a.FlagDel=0 and a.ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
  
        #endregion  扩展方法
    }
}

