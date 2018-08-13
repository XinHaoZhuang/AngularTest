using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;//请先添加引用
namespace SCZM.DAL.Repair
{
    /// <summary>
    /// 数据访问类repair_AccessoriesBill。
    /// </summary>
    public partial class repair_AccessoriesBill
    {
        public repair_AccessoriesBill()
        { }
        #region  基本方法
        /// <summary>
        /// 某列是否存在
        /// </summary>
        public bool Exists(string FieldName, string FieldValue, int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from repair_AccessoriesBill");
            strSql.Append(" where FlagDel=0 and  " + FieldName + "=@" + FieldName + " and ID<>@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@" + FieldName, SqlDbType.VarChar),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = FieldValue;
            parameters[1].Value = ID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据,及其子表数据
        /// </summary>
        public int Add(SCZM.Model.Repair.repair_AccessoriesBill model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into repair_AccessoriesBill(");
            strSql.Append("IntentionId,GetAccessoriesDate,AllFee,AllFee_actual,Memo,FlagDel,OperaDepId,OperaId,OperaName,OperaTime,UserName,BillType)");
            strSql.Append(" values (");
            strSql.Append("@IntentionId,@GetAccessoriesDate,@AllFee,@AllFee_actual,@Memo,@FlagDel,@OperaDepId,@OperaId,@OperaName,@OperaTime,@UserName,@BillType)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
				new SqlParameter("@IntentionId", SqlDbType.Int,4),
				new SqlParameter("@GetAccessoriesDate", SqlDbType.SmallDateTime),
				new SqlParameter("@AllFee", SqlDbType.Decimal,9),
				new SqlParameter("@AllFee_actual", SqlDbType.Decimal,9),
				new SqlParameter("@Memo", SqlDbType.NVarChar,200),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaDepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
                new SqlParameter("@UserName",SqlDbType.NVarChar,10),
                new SqlParameter("@BillType",SqlDbType.Int,4),
				new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.IntentionId;
            parameters[1].Value = model.GetAccessoriesDate;
            parameters[2].Value = model.AllFee;
            parameters[3].Value = model.AllFee_actual;
            parameters[4].Value = model.Memo;
            parameters[5].Value = model.FlagDel;
            parameters[6].Value = model.OperaDepId;
            parameters[7].Value = model.OperaId;
            parameters[8].Value = model.OperaName;
            parameters[9].Value = model.OperaTime;
            parameters[10].Value = model.UserName;
            parameters[11].Value = model.BillType;
            parameters[12].Direction = ParameterDirection.Output;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql2;
            if (model.repair_AccessoriesBill_Accessoriess != null)
            {
                foreach (SCZM.Model.Repair.repair_AccessoriesBill_Accessories models in model.repair_AccessoriesBill_Accessoriess)
                {
                    strSql2 = new StringBuilder();
                    strSql2.Append("insert into repair_AccessoriesBill_Accessories(");
                    strSql2.Append("AccessoriesBillId,AccessoriesDate,AccessoriesId,AccessoriesNum,AccessoriesNat,AccessoriesFee,FlagDel,AccessoriesMemo)");
                    strSql2.Append(" values (");
                    strSql2.Append("@AccessoriesBillId,@AccessoriesDate,@AccessoriesId,@AccessoriesNum,@AccessoriesNat,@AccessoriesFee,@FlagDel,@AccessoriesMemo)");
                    SqlParameter[] parameters2 = {
						new SqlParameter("@AccessoriesBillId", SqlDbType.Int,4),
						new SqlParameter("@AccessoriesDate", SqlDbType.SmallDateTime),
						new SqlParameter("@AccessoriesId", SqlDbType.Int,4),
						new SqlParameter("@AccessoriesNum", SqlDbType.Decimal,9),
						new SqlParameter("@AccessoriesNat", SqlDbType.Decimal,9),
						new SqlParameter("@AccessoriesFee", SqlDbType.Decimal,9),
						new SqlParameter("@FlagDel", SqlDbType.Int,4),
                                                 new SqlParameter("@AccessoriesMemo",SqlDbType.NVarChar,200)};
                    parameters2[0].Direction = ParameterDirection.InputOutput;
                    parameters2[1].Value = models.AccessoriesDate;
                    parameters2[2].Value = models.AccessoriesId;
                    parameters2[3].Value = models.AccessoriesNum;
                    parameters2[4].Value = models.AccessoriesNat;
                    parameters2[5].Value = models.AccessoriesFee;
                    parameters2[6].Value = models.FlagDel;
                    parameters2[7].Value = models.AccessoriesMemo;

                    cmd = new CommandInfo(strSql2.ToString(), parameters2);
                    sqllist.Add(cmd);
                }
            }
            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[12].Value;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(SCZM.Model.Repair.repair_AccessoriesBill model)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update repair_AccessoriesBill set ");
            strSql.Append("IntentionId=@IntentionId,");
            strSql.Append("GetAccessoriesDate=@GetAccessoriesDate,");
            strSql.Append("AllFee=@AllFee,");
            strSql.Append("AllFee_actual=@AllFee_actual,");
            strSql.Append("Memo=@Memo,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaDepId=@OperaDepId,");
            strSql.Append("OperaId=@OperaId,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("BillType=@BillType");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@IntentionId", SqlDbType.Int,4),
				new SqlParameter("@GetAccessoriesDate", SqlDbType.SmallDateTime),
				new SqlParameter("@AllFee", SqlDbType.Decimal,9),
				new SqlParameter("@AllFee_actual", SqlDbType.Decimal,9),
				new SqlParameter("@Memo", SqlDbType.NVarChar,200),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaDepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
                new SqlParameter("@UserName",SqlDbType.NVarChar,10),
                new SqlParameter("@BillType",SqlDbType.Int,4),
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.IntentionId;
            parameters[1].Value = model.GetAccessoriesDate;
            parameters[2].Value = model.AllFee;
            parameters[3].Value = model.AllFee_actual;
            parameters[4].Value = model.Memo;
            parameters[5].Value = model.FlagDel;
            parameters[6].Value = model.OperaDepId;
            parameters[7].Value = model.OperaId;
            parameters[8].Value = model.OperaName;
            parameters[9].Value = model.OperaTime;
            parameters[10].Value = model.UserName;
            parameters[11].Value = model.BillType;
            parameters[12].Value = model.ID;

            cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from  repair_AccessoriesBill_Accessories where AccessoriesBillId=@AccessoriesBillId ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@AccessoriesBillId", SqlDbType.Int,4)};
            parameters2[0].Value = model.ID;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            StringBuilder strSql3;
            if (model.repair_AccessoriesBill_Accessoriess != null)
            {
                foreach (SCZM.Model.Repair.repair_AccessoriesBill_Accessories models in model.repair_AccessoriesBill_Accessoriess)
                {
                    strSql3 = new StringBuilder();
                    strSql3.Append("insert into repair_AccessoriesBill_Accessories(");
                    strSql3.Append("AccessoriesBillId,AccessoriesDate,AccessoriesId,AccessoriesNum,AccessoriesNat,AccessoriesFee,FlagDel,AccessoriesMemo)");
                    strSql3.Append(" values (");
                    strSql3.Append("@AccessoriesBillId,@AccessoriesDate,@AccessoriesId,@AccessoriesNum,@AccessoriesNat,@AccessoriesFee,@FlagDel,@AccessoriesMemo)");
                    SqlParameter[] parameters3 = {
						new SqlParameter("@AccessoriesBillId", SqlDbType.Int,4),
						new SqlParameter("@AccessoriesDate", SqlDbType.SmallDateTime),
						new SqlParameter("@AccessoriesId", SqlDbType.Int,4),
						new SqlParameter("@AccessoriesNum", SqlDbType.Decimal,9),
						new SqlParameter("@AccessoriesNat", SqlDbType.Decimal,9),
						new SqlParameter("@AccessoriesFee", SqlDbType.Decimal,9),
						new SqlParameter("@FlagDel", SqlDbType.Int,4),
                                                 new SqlParameter("@AccessoriesMemo",SqlDbType.NVarChar,200)};
                    parameters3[0].Value = model.ID;
                    parameters3[1].Value = models.AccessoriesDate;
                    parameters3[2].Value = models.AccessoriesId;
                    parameters3[3].Value = models.AccessoriesNum;
                    parameters3[4].Value = models.AccessoriesNat;
                    parameters3[5].Value = models.AccessoriesFee;
                    parameters3[6].Value = models.FlagDel;
                    parameters3[7].Value = models.AccessoriesMemo;

                    cmd = new CommandInfo(strSql3.ToString(), parameters3);
                    sqllist.Add(cmd);
                }
            }
            int rows = DbHelperSQL.ExecuteSqlTran(sqllist);
            return rows;
        }


        /// <summary>
        /// 得到一个对象实体 主表
        /// </summary>
        public SCZM.Model.Repair.repair_AccessoriesBill GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,IntentionId,GetAccessoriesDate,AllFee,AllFee_actual,Memo,FlagDel,OperaDepId,OperaId,OperaName,OperaTime,UserName,BillType from repair_AccessoriesBill ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.Repair.repair_AccessoriesBill model = new SCZM.Model.Repair.repair_AccessoriesBill();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region  父表信息
                model = DataRowToModel(ds.Tables[0].Rows[0]);
                #endregion  父表信息end

                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体 子方法 从DataRow中
        /// </summary>
        public SCZM.Model.Repair.repair_AccessoriesBill DataRowToModel(DataRow row)
        {
            SCZM.Model.Repair.repair_AccessoriesBill model = new SCZM.Model.Repair.repair_AccessoriesBill();
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
                if (row["GetAccessoriesDate"] != null && row["GetAccessoriesDate"].ToString() != "")
                {
                    model.GetAccessoriesDate = DateTime.Parse(row["GetAccessoriesDate"].ToString());
                }
                if (row["AllFee"] != null && row["AllFee"].ToString() != "")
                {
                    model.AllFee = decimal.Parse(row["AllFee"].ToString());
                }
                if (row["AllFee_actual"] != null && row["AllFee_actual"].ToString() != "")
                {
                    model.AllFee_actual = decimal.Parse(row["AllFee_actual"].ToString());
                }
                if (row["Memo"] != null)
                {
                    model.Memo = row["Memo"].ToString();
                }
                if (row["BillType"] != null && row["BillType"].ToString() != "") {
                    model.BillType = int.Parse(row["BillType"].ToString());
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
                if (row["UserName"] != null)
                {
                    model.OperaName = row["UserName"].ToString();
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
            List<string> sqllist = new List<string>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update repair_AccessoriesBill_Accessories set FlagDel=1 ");
            strSql.Append("where FlagDel=0 and AccessoriesBillId in(" + IDList + ")");
            sqllist.Add(strSql.ToString());

            strSql = new StringBuilder();
            strSql.Append("update repair_AccessoriesBill set FlagDel=1 ");
            strSql.Append("where FlagDel=0 and ID in(" + IDList + ")");
            sqllist.Add(strSql.ToString());
            int rows = DbHelperSQL.ExecuteSqlTran(sqllist);
            return rows;
        }

        /// <summary>
        /// 获得数据列表 通过Where条件
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.IntentionId,b.IntentionCode,b.CustName,c.MachineModel,b.MachineCode,a.GetAccessoriesDate,a.AllFee,a.AllFee_actual,a.Memo,a.OperaDepId,a.OperaId,a.OperaName,a.OperaTime,a.UserName,a.BillType ");
            strSql.Append("FROM repair_AccessoriesBill a ");
            strSql.Append("left join repair_Intention b on b.FlagDel=0 and a.IntentionId=b.ID ");
            strSql.Append("left join base_MachineModel c on b.MachineModelId=c.ID and c.FlagDel=0 ");
            strSql.Append("where a.FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.OperaTime ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据明细 通过ID
        /// </summary>
        public DataSet GetDetail(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.IntentionId,b.IntentionCode,b.CustName,c.MachineModel,b.MachineCode,a.GetAccessoriesDate,a.AllFee,a.AllFee_actual,a.Memo,a.OperaDepId,a.OperaId,a.OperaName,a.OperaTime,a.UserName,a.BillType ");
            strSql.Append("FROM repair_AccessoriesBill a ");
            strSql.Append("left join repair_Intention b on b.FlagDel=0 and a.IntentionId=b.ID ");
            strSql.Append("left join base_MachineModel c on b.MachineModelId=c.ID and c.FlagDel=0 ");
            strSql.Append("where a.FlagDel=0 and a.ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select a.ID,a.AccessoriesBillId,a.AccessoriesDate,a.AccessoriesId,b.AccessoriesName,a.AccessoriesNum,a.AccessoriesNat,a.AccessoriesFee,a.AccessoriesMemo,b.AccessoriesName+'('+b.Unit+')' as 'AccessoriesName--Unit' ");
            strSql2.Append("FROM repair_AccessoriesBill_Accessories a ");
            strSql2.Append("left join base_Accessories b on b.FlagDel=0 and a.AccessoriesId=b.ID ");
            strSql2.Append("where a.FlagDel=0 and AccessoriesBillId=@AccessoriesBillId ");
            SqlParameter[] parameters2 = {
				new SqlParameter("@AccessoriesBillId", SqlDbType.Int,4)};
            parameters2[0].Value = ID;
            DataTable dt2 = DbHelperSQL.Query(strSql2.ToString(), parameters2).Tables[0];
            dt2.TableName = "AccessoriesBill_Accessories";
            ds.Tables.Add(dt2.Copy());
            return ds;
        }

        #endregion  扩展方法
    }
}

