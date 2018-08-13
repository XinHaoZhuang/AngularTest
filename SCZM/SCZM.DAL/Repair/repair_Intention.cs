using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;//请先添加引用
namespace SCZM.DAL.Repair
{
    /// <summary>
    /// 数据访问类repair_Intention。
    /// </summary>
    public partial class repair_Intention
    {
        public repair_Intention()
        { }
        #region  基本方法
        /// <summary>
        /// 某列是否存在
        /// </summary>
        public bool Exists(string FieldName, string FieldValue, int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from repair_Intention");
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
        public int Add(SCZM.Model.Repair.repair_Intention model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into repair_Intention(");
            strSql.Append("IntentionCode,IntentionDate,IntentionType,IntentionCode_Last,BusinessDepId,BusinessPerName,CustTypeId,CustName,MachineModelId,MachineCode,EngineModel,EngineCode,SMR,FlagFXGCH,Linkman,LinkPhone,MachineAdress,Machine,MachineStatus,FlagResult,RepairTypeId,RepairContent,FlagENGKC,FlagPPMKC,FlagENG,FlagPPM,FlagMCV,FlagELE,FlagVM,FlagRM,FlagSM,FlagUM,FlagVR,RepairAdress,ExpectEnterDate,ExpectLeaveDate,ExpectTimeFee,ExpectPartFee,ExpectFee,CustOpinion,RepairState,FlagDel,OperaDepId,OperaId,OperaName,OperaTime,ActualEnterDate,OperaName_Enter,OperaTime_Enter,ActualLeaveDate,OperaName_Leave,OperaTime_Leave,FlagSP,FlagOther,RepairMode,FlagLocale)");
            strSql.Append(" values (");
            strSql.Append("@IntentionCode,@IntentionDate,@IntentionType,@IntentionCode_Last,@BusinessDepId,@BusinessPerName,@CustTypeId,@CustName,@MachineModelId,@MachineCode,@EngineModel,@EngineCode,@SMR,@FlagFXGCH,@Linkman,@LinkPhone,@MachineAdress,@Machine,@MachineStatus,@FlagResult,@RepairTypeId,@RepairContent,@FlagENGKC,@FlagPPMKC,@FlagENG,@FlagPPM,@FlagMCV,@FlagELE,@FlagVM,@FlagRM,@FlagSM,@FlagUM,@FlagVR,@RepairAdress,@ExpectEnterDate,@ExpectLeaveDate,@ExpectTimeFee,@ExpectPartFee,@ExpectFee,@CustOpinion,@RepairState,@FlagDel,@OperaDepId,@OperaId,@OperaName,@OperaTime,@ActualEnterDate,@OperaName_Enter,@OperaTime_Enter,@ActualLeaveDate,@OperaName_Leave,@OperaTime_Leave,@FlagSP,@FlagOther,@RepairMode,@FlagLocale)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
				new SqlParameter("@IntentionCode", SqlDbType.VarChar,20),
				new SqlParameter("@IntentionDate", SqlDbType.SmallDateTime),
				new SqlParameter("@IntentionType", SqlDbType.Int,4),
				new SqlParameter("@IntentionCode_Last", SqlDbType.VarChar,20),
				new SqlParameter("@BusinessDepId", SqlDbType.Int,4),
				new SqlParameter("@BusinessPerName", SqlDbType.NVarChar,10),
				new SqlParameter("@CustTypeId", SqlDbType.Int,4),
				new SqlParameter("@CustName", SqlDbType.NVarChar,50),
				new SqlParameter("@MachineModelId", SqlDbType.Int,4),
				new SqlParameter("@MachineCode", SqlDbType.VarChar,20),
				new SqlParameter("@EngineModel", SqlDbType.VarChar,30),
				new SqlParameter("@EngineCode", SqlDbType.VarChar,30),
				new SqlParameter("@SMR", SqlDbType.Int,4),
				new SqlParameter("@FlagFXGCH", SqlDbType.Int,4),
				new SqlParameter("@Linkman", SqlDbType.NVarChar,10),
				new SqlParameter("@LinkPhone", SqlDbType.VarChar,20),
				new SqlParameter("@MachineAdress", SqlDbType.NVarChar,50),
				new SqlParameter("@Machine", SqlDbType.NVarChar,50),
				new SqlParameter("@MachineStatus", SqlDbType.NVarChar,200),
				new SqlParameter("@FlagResult", SqlDbType.Int,4),
				new SqlParameter("@RepairTypeId", SqlDbType.Int,4),
				new SqlParameter("@RepairContent", SqlDbType.NVarChar,200),
				new SqlParameter("@FlagENGKC", SqlDbType.Int,4),
				new SqlParameter("@FlagPPMKC", SqlDbType.Int,4),
				new SqlParameter("@FlagENG", SqlDbType.Int,4),
				new SqlParameter("@FlagPPM", SqlDbType.Int,4),
				new SqlParameter("@FlagMCV", SqlDbType.Int,4),
				new SqlParameter("@FlagELE", SqlDbType.Int,4),
				new SqlParameter("@FlagVM", SqlDbType.Int,4),
				new SqlParameter("@FlagRM", SqlDbType.Int,4),
				new SqlParameter("@FlagSM", SqlDbType.Int,4),
				new SqlParameter("@FlagUM", SqlDbType.Int,4),
				new SqlParameter("@FlagVR", SqlDbType.Int,4),
				new SqlParameter("@RepairAdress", SqlDbType.NVarChar,10),
				new SqlParameter("@ExpectEnterDate", SqlDbType.SmallDateTime),
				new SqlParameter("@ExpectLeaveDate", SqlDbType.SmallDateTime),
				new SqlParameter("@ExpectTimeFee", SqlDbType.Decimal,9),
				new SqlParameter("@ExpectPartFee", SqlDbType.Decimal,9),
				new SqlParameter("@ExpectFee", SqlDbType.Decimal,9),
				new SqlParameter("@CustOpinion", SqlDbType.NVarChar,200),
				new SqlParameter("@RepairState", SqlDbType.Int,4),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaDepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
				new SqlParameter("@ActualEnterDate", SqlDbType.SmallDateTime),
				new SqlParameter("@OperaName_Enter", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime_Enter", SqlDbType.DateTime),
				new SqlParameter("@ActualLeaveDate", SqlDbType.SmallDateTime),
				new SqlParameter("@OperaName_Leave", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime_Leave", SqlDbType.DateTime),
                new SqlParameter("@FlagSP",SqlDbType.Int,4),
                new SqlParameter("@FlagOther",SqlDbType.Int,4),
                new SqlParameter("@RepairMode",SqlDbType.Int,4),
                new SqlParameter("@FlagLocale",SqlDbType.Int,4),
                new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.IntentionCode;
            parameters[1].Value = model.IntentionDate;
            parameters[2].Value = model.IntentionType;
            parameters[3].Value = model.IntentionCode_Last;
            parameters[4].Value = model.BusinessDepId;
            parameters[5].Value = model.BusinessPerName;
            parameters[6].Value = model.CustTypeId;
            parameters[7].Value = model.CustName;
            parameters[8].Value = model.MachineModelId;
            parameters[9].Value = model.MachineCode;
            parameters[10].Value = model.EngineModel;
            parameters[11].Value = model.EngineCode;
            parameters[12].Value = model.SMR;
            parameters[13].Value = model.FlagFXGCH;
            parameters[14].Value = model.Linkman;
            parameters[15].Value = model.LinkPhone;
            parameters[16].Value = model.MachineAdress;
            parameters[17].Value = model.Machine;
            parameters[18].Value = model.MachineStatus;
            parameters[19].Value = model.FlagResult;
            parameters[20].Value = model.RepairTypeId;
            parameters[21].Value = model.RepairContent;
            parameters[22].Value = model.FlagENGKC;
            parameters[23].Value = model.FlagPPMKC;
            parameters[24].Value = model.FlagENG;
            parameters[25].Value = model.FlagPPM;
            parameters[26].Value = model.FlagMCV;
            parameters[27].Value = model.FlagELE;
            parameters[28].Value = model.FlagVM;
            parameters[29].Value = model.FlagRM;
            parameters[30].Value = model.FlagSM;
            parameters[31].Value = model.FlagUM;
            parameters[32].Value = model.FlagVR;
            parameters[33].Value = model.RepairAdress;
            parameters[34].Value = model.ExpectEnterDate;
            parameters[35].Value = model.ExpectLeaveDate;
            parameters[36].Value = model.ExpectTimeFee;
            parameters[37].Value = model.ExpectPartFee;
            parameters[38].Value = model.ExpectFee;
            parameters[39].Value = model.CustOpinion;
            parameters[40].Value = model.RepairState;
            parameters[41].Value = model.FlagDel;
            parameters[42].Value = model.OperaDepId;
            parameters[43].Value = model.OperaId;
            parameters[44].Value = model.OperaName;
            parameters[45].Value = model.OperaTime;
            parameters[46].Value = model.ActualEnterDate;
            parameters[47].Value = model.OperaName_Enter;
            parameters[48].Value = model.OperaTime_Enter;
            parameters[49].Value = model.ActualLeaveDate;
            parameters[50].Value = model.OperaName_Leave;
            parameters[51].Value = model.OperaTime_Leave;
            parameters[52].Value = model.FlagSP;
            parameters[53].Value = model.FlagOther;
            parameters[54].Value = model.RepairMode;
            parameters[55].Value = model.FlagLocale;
            parameters[56].Direction = ParameterDirection.Output;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql2;
            if (model.repair_Intention_Packages != null)
            {
                foreach (SCZM.Model.Repair.repair_Intention_Package models in model.repair_Intention_Packages)
                {
                    strSql2 = new StringBuilder();
                    strSql2.Append("insert into repair_Intention_Package(");
                    strSql2.Append("IntentionId,PackageId,PackageNum,FlagDel)");
                    strSql2.Append(" values (");
                    strSql2.Append("@IntentionId,@PackageId,@PackageNum,@FlagDel)");
                    SqlParameter[] parameters2 = {
						new SqlParameter("@IntentionId", SqlDbType.Int,4),
						new SqlParameter("@PackageId", SqlDbType.Int,4),
                        new SqlParameter("@PackageNum",SqlDbType.Int,4),
						new SqlParameter("@FlagDel", SqlDbType.Int,4)};
                    parameters2[0].Direction = ParameterDirection.InputOutput;
                    parameters2[1].Value = models.PackageId;
                    parameters2[2].Value = models.PackageNum;
                    parameters2[3].Value = models.FlagDel;

                    cmd = new CommandInfo(strSql2.ToString(), parameters2);
                    sqllist.Add(cmd);
                }
            }
            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[56].Value;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(SCZM.Model.Repair.repair_Intention model)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update repair_Intention set ");
            strSql.Append("IntentionCode=@IntentionCode,");
            strSql.Append("IntentionDate=@IntentionDate,");
            strSql.Append("IntentionType=@IntentionType,");
            strSql.Append("IntentionCode_Last=@IntentionCode_Last,");
            strSql.Append("BusinessDepId=@BusinessDepId,");
            strSql.Append("BusinessPerName=@BusinessPerName,");
            strSql.Append("CustTypeId=@CustTypeId,");
            strSql.Append("CustName=@CustName,");
            strSql.Append("MachineModelId=@MachineModelId,");
            strSql.Append("MachineCode=@MachineCode,");
            strSql.Append("EngineModel=@EngineModel,");
            strSql.Append("EngineCode=@EngineCode,");
            strSql.Append("SMR=@SMR,");
            strSql.Append("FlagFXGCH=@FlagFXGCH,");
            strSql.Append("Linkman=@Linkman,");
            strSql.Append("LinkPhone=@LinkPhone,");
            strSql.Append("MachineAdress=@MachineAdress,");
            strSql.Append("Machine=@Machine,");
            strSql.Append("MachineStatus=@MachineStatus,");
            strSql.Append("FlagResult=@FlagResult,");
            strSql.Append("RepairTypeId=@RepairTypeId,");
            strSql.Append("RepairContent=@RepairContent,");
            strSql.Append("FlagENGKC=@FlagENGKC,");
            strSql.Append("FlagPPMKC=@FlagPPMKC,");
            strSql.Append("FlagENG=@FlagENG,");
            strSql.Append("FlagPPM=@FlagPPM,");
            strSql.Append("FlagMCV=@FlagMCV,");
            strSql.Append("FlagELE=@FlagELE,");
            strSql.Append("FlagVM=@FlagVM,");
            strSql.Append("FlagRM=@FlagRM,");
            strSql.Append("FlagSM=@FlagSM,");
            strSql.Append("FlagUM=@FlagUM,");
            strSql.Append("FlagVR=@FlagVR,");
            strSql.Append("RepairAdress=@RepairAdress,");
            strSql.Append("ExpectEnterDate=@ExpectEnterDate,");
            strSql.Append("ExpectLeaveDate=@ExpectLeaveDate,");
            strSql.Append("ExpectTimeFee=@ExpectTimeFee,");
            strSql.Append("ExpectPartFee=@ExpectPartFee,");
            strSql.Append("ExpectFee=@ExpectFee,");
            strSql.Append("CustOpinion=@CustOpinion,");
            strSql.Append("RepairState=@RepairState,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaDepId=@OperaDepId,");
            strSql.Append("OperaId=@OperaId,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime,");
            strSql.Append("ActualEnterDate=@ActualEnterDate,");
            strSql.Append("OperaName_Enter=@OperaName_Enter,");
            strSql.Append("OperaTime_Enter=@OperaTime_Enter,");
            strSql.Append("ActualLeaveDate=@ActualLeaveDate,");
            strSql.Append("OperaName_Leave=@OperaName_Leave,");
            strSql.Append("OperaTime_Leave=@OperaTime_Leave,");
            strSql.Append("FlagSP=@FlagSP,");
            strSql.Append("FlagOther=@FlagOther,");
            strSql.Append("RepairMode=@RepairMode,");
            strSql.Append("FlagLocale=@FlagLocale");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@IntentionCode", SqlDbType.VarChar,20),
				new SqlParameter("@IntentionDate", SqlDbType.SmallDateTime),
				new SqlParameter("@IntentionType", SqlDbType.Int,4),
				new SqlParameter("@IntentionCode_Last", SqlDbType.VarChar,20),
				new SqlParameter("@BusinessDepId", SqlDbType.Int,4),
				new SqlParameter("@BusinessPerName", SqlDbType.NVarChar,10),
				new SqlParameter("@CustTypeId", SqlDbType.Int,4),
				new SqlParameter("@CustName", SqlDbType.NVarChar,50),
				new SqlParameter("@MachineModelId", SqlDbType.Int,4),
				new SqlParameter("@MachineCode", SqlDbType.VarChar,20),
				new SqlParameter("@EngineModel", SqlDbType.VarChar,30),
				new SqlParameter("@EngineCode", SqlDbType.VarChar,30),
				new SqlParameter("@SMR", SqlDbType.Int,4),
				new SqlParameter("@FlagFXGCH", SqlDbType.Int,4),
				new SqlParameter("@Linkman", SqlDbType.NVarChar,10),
				new SqlParameter("@LinkPhone", SqlDbType.VarChar,20),
				new SqlParameter("@MachineAdress", SqlDbType.NVarChar,50),
				new SqlParameter("@Machine", SqlDbType.NVarChar,50),
				new SqlParameter("@MachineStatus", SqlDbType.NVarChar,200),
				new SqlParameter("@FlagResult", SqlDbType.Int,4),
				new SqlParameter("@RepairTypeId", SqlDbType.Int,4),
				new SqlParameter("@RepairContent", SqlDbType.NVarChar,200),
				new SqlParameter("@FlagENGKC", SqlDbType.Int,4),
				new SqlParameter("@FlagPPMKC", SqlDbType.Int,4),
				new SqlParameter("@FlagENG", SqlDbType.Int,4),
				new SqlParameter("@FlagPPM", SqlDbType.Int,4),
				new SqlParameter("@FlagMCV", SqlDbType.Int,4),
				new SqlParameter("@FlagELE", SqlDbType.Int,4),
				new SqlParameter("@FlagVM", SqlDbType.Int,4),
				new SqlParameter("@FlagRM", SqlDbType.Int,4),
				new SqlParameter("@FlagSM", SqlDbType.Int,4),
				new SqlParameter("@FlagUM", SqlDbType.Int,4),
				new SqlParameter("@FlagVR", SqlDbType.Int,4),
				new SqlParameter("@RepairAdress", SqlDbType.NVarChar,10),
				new SqlParameter("@ExpectEnterDate", SqlDbType.SmallDateTime),
				new SqlParameter("@ExpectLeaveDate", SqlDbType.SmallDateTime),
				new SqlParameter("@ExpectTimeFee", SqlDbType.Decimal,9),
				new SqlParameter("@ExpectPartFee", SqlDbType.Decimal,9),
				new SqlParameter("@ExpectFee", SqlDbType.Decimal,9),
				new SqlParameter("@CustOpinion", SqlDbType.NVarChar,200),
				new SqlParameter("@RepairState", SqlDbType.Int,4),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaDepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
				new SqlParameter("@ActualEnterDate", SqlDbType.SmallDateTime),
				new SqlParameter("@OperaName_Enter", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime_Enter", SqlDbType.DateTime),
				new SqlParameter("@ActualLeaveDate", SqlDbType.SmallDateTime),
				new SqlParameter("@OperaName_Leave", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime_Leave", SqlDbType.DateTime),
                new SqlParameter("@FlagSP",SqlDbType.Int,4),
                new SqlParameter("@FlagOther",SqlDbType.Int,4),
                new SqlParameter("@RepairMode",SqlDbType.Int,4),
                new SqlParameter("@FlagLocale",SqlDbType.Int,4),
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.IntentionCode;
            parameters[1].Value = model.IntentionDate;
            parameters[2].Value = model.IntentionType;
            parameters[3].Value = model.IntentionCode_Last;
            parameters[4].Value = model.BusinessDepId;
            parameters[5].Value = model.BusinessPerName;
            parameters[6].Value = model.CustTypeId;
            parameters[7].Value = model.CustName;
            parameters[8].Value = model.MachineModelId;
            parameters[9].Value = model.MachineCode;
            parameters[10].Value = model.EngineModel;
            parameters[11].Value = model.EngineCode;
            parameters[12].Value = model.SMR;
            parameters[13].Value = model.FlagFXGCH;
            parameters[14].Value = model.Linkman;
            parameters[15].Value = model.LinkPhone;
            parameters[16].Value = model.MachineAdress;
            parameters[17].Value = model.Machine;
            parameters[18].Value = model.MachineStatus;
            parameters[19].Value = model.FlagResult;
            parameters[20].Value = model.RepairTypeId;
            parameters[21].Value = model.RepairContent;
            parameters[22].Value = model.FlagENGKC;
            parameters[23].Value = model.FlagPPMKC;
            parameters[24].Value = model.FlagENG;
            parameters[25].Value = model.FlagPPM;
            parameters[26].Value = model.FlagMCV;
            parameters[27].Value = model.FlagELE;
            parameters[28].Value = model.FlagVM;
            parameters[29].Value = model.FlagRM;
            parameters[30].Value = model.FlagSM;
            parameters[31].Value = model.FlagUM;
            parameters[32].Value = model.FlagVR;
            parameters[33].Value = model.RepairAdress;
            parameters[34].Value = model.ExpectEnterDate;
            parameters[35].Value = model.ExpectLeaveDate;
            parameters[36].Value = model.ExpectTimeFee;
            parameters[37].Value = model.ExpectPartFee;
            parameters[38].Value = model.ExpectFee;
            parameters[39].Value = model.CustOpinion;
            parameters[40].Value = model.RepairState;
            parameters[41].Value = model.FlagDel;
            parameters[42].Value = model.OperaDepId;
            parameters[43].Value = model.OperaId;
            parameters[44].Value = model.OperaName;
            parameters[45].Value = model.OperaTime;
            parameters[46].Value = model.ActualEnterDate;
            parameters[47].Value = model.OperaName_Enter;
            parameters[48].Value = model.OperaTime_Enter;
            parameters[49].Value = model.ActualLeaveDate;
            parameters[50].Value = model.OperaName_Leave;
            parameters[51].Value = model.OperaTime_Leave;
            parameters[52].Value = model.FlagSP;
            parameters[53].Value = model.FlagOther;
            parameters[54].Value = model.RepairMode;
            parameters[55].Value = model.FlagLocale;
            parameters[56].Value = model.ID;

            cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from  repair_Intention_Package where IntentionId=@IntentionId ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@IntentionId", SqlDbType.Int,4)};
            parameters2[0].Value = model.ID;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            StringBuilder strSql3;
            if (model.repair_Intention_Packages != null)
            {
                foreach (SCZM.Model.Repair.repair_Intention_Package models in model.repair_Intention_Packages)
                {
                    strSql3 = new StringBuilder();
                    strSql3.Append("insert into repair_Intention_Package(");
                    strSql3.Append("IntentionId,PackageId,PackageNum,FlagDel)");
                    strSql3.Append(" values (");
                    strSql3.Append("@IntentionId,@PackageId,@PackageNum,@FlagDel)");
                    SqlParameter[] parameters3 = {
						new SqlParameter("@IntentionId", SqlDbType.Int,4),
						new SqlParameter("@PackageId", SqlDbType.Int,4),
                        new SqlParameter("@PackageNum",SqlDbType.Int,4),
						new SqlParameter("@FlagDel", SqlDbType.Int,4)};
                    parameters3[0].Value = model.ID;
                    parameters3[1].Value = models.PackageId;
                    parameters3[2].Value = models.PackageNum;
                    parameters3[3].Value = models.FlagDel;

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
        public SCZM.Model.Repair.repair_Intention GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,IntentionCode,IntentionDate,IntentionType,IntentionCode_Last,BusinessDepId,BusinessPerName,CustTypeId,CustName,MachineModelId,MachineCode,EngineModel,EngineCode,SMR,FlagFXGCH,Linkman,LinkPhone,MachineAdress,Machine,MachineStatus,FlagResult,RepairTypeId,RepairContent,FlagENGKC,FlagPPMKC,FlagENG,FlagPPM,FlagMCV,FlagELE,FlagVM,FlagRM,FlagSM,FlagUM,FlagVR,FlagSP,FlagOther,RepairAdress,ExpectEnterDate,ExpectLeaveDate,ExpectTimeFee,ExpectPartFee,ExpectFee,CustOpinion,FlagAgreement,AgreementDate,AttachmentId_Agreement,RepairState,FlagDel,OperaDepId,OperaId,OperaName,OperaTime,ActualEnterDate,OperaName_Enter,OperaTime_Enter,ActualLeaveDate,OperaName_Leave,OperaTime_Leave,RepairMode,FlagLocale from repair_Intention ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.Repair.repair_Intention model = new SCZM.Model.Repair.repair_Intention();
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
        public SCZM.Model.Repair.repair_Intention DataRowToModel(DataRow row)
        {
            SCZM.Model.Repair.repair_Intention model = new SCZM.Model.Repair.repair_Intention();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["IntentionCode"] != null)
                {
                    model.IntentionCode = row["IntentionCode"].ToString();
                }
                if (row["IntentionDate"] != null && row["IntentionDate"].ToString() != "")
                {
                    model.IntentionDate = DateTime.Parse(row["IntentionDate"].ToString());
                }
                if (row["IntentionType"] != null && row["IntentionType"].ToString() != "")
                {
                    model.IntentionType = int.Parse(row["IntentionType"].ToString());
                }
                if (row["IntentionCode_Last"] != null)
                {
                    model.IntentionCode_Last = row["IntentionCode_Last"].ToString();
                }
                if (row["BusinessDepId"] != null && row["BusinessDepId"].ToString() != "")
                {
                    model.BusinessDepId = int.Parse(row["BusinessDepId"].ToString());
                }
                if (row["BusinessPerName"] != null)
                {
                    model.BusinessPerName = row["BusinessPerName"].ToString();
                }
                if (row["CustTypeId"] != null && row["CustTypeId"].ToString() != "")
                {
                    model.CustTypeId = int.Parse(row["CustTypeId"].ToString());
                }
                if (row["CustName"] != null)
                {
                    model.CustName = row["CustName"].ToString();
                }
                if (row["MachineModelId"] != null && row["MachineModelId"].ToString() != "")
                {
                    model.MachineModelId = int.Parse(row["MachineModelId"].ToString());
                }
                if (row["MachineCode"] != null)
                {
                    model.MachineCode = row["MachineCode"].ToString();
                }
                if (row["EngineModel"] != null)
                {
                    model.EngineModel = row["EngineModel"].ToString();
                }
                if (row["EngineCode"] != null)
                {
                    model.EngineCode = row["EngineCode"].ToString();
                }
                if (row["SMR"] != null && row["SMR"].ToString() != "")
                {
                    model.SMR = int.Parse(row["SMR"].ToString());
                }
                if (row["FlagFXGCH"] != null && row["FlagFXGCH"].ToString() != "")
                {
                    model.FlagFXGCH = int.Parse(row["FlagFXGCH"].ToString());
                }
                if (row["Linkman"] != null)
                {
                    model.Linkman = row["Linkman"].ToString();
                }
                if (row["LinkPhone"] != null)
                {
                    model.LinkPhone = row["LinkPhone"].ToString();
                }
                if (row["MachineAdress"] != null)
                {
                    model.MachineAdress = row["MachineAdress"].ToString();
                }
                if (row["Machine"] != null)
                {
                    model.Machine = row["Machine"].ToString();
                }
                if (row["MachineStatus"] != null)
                {
                    model.MachineStatus = row["MachineStatus"].ToString();
                }
                if (row["FlagResult"] != null && row["FlagResult"].ToString() != "")
                {
                    model.FlagResult = int.Parse(row["FlagResult"].ToString());
                }
                if (row["RepairTypeId"] != null && row["RepairTypeId"].ToString() != "")
                {
                    model.RepairTypeId = int.Parse(row["RepairTypeId"].ToString());
                }
                if (row["RepairContent"] != null)
                {
                    model.RepairContent = row["RepairContent"].ToString();
                }
                if (row["FlagENGKC"] != null && row["FlagENGKC"].ToString() != "")
                {
                    model.FlagENGKC = int.Parse(row["FlagENGKC"].ToString());
                }
                if (row["FlagPPMKC"] != null && row["FlagPPMKC"].ToString() != "")
                {
                    model.FlagPPMKC = int.Parse(row["FlagPPMKC"].ToString());
                }
                if (row["FlagENG"] != null && row["FlagENG"].ToString() != "")
                {
                    model.FlagENG = int.Parse(row["FlagENG"].ToString());
                }
                if (row["FlagPPM"] != null && row["FlagPPM"].ToString() != "")
                {
                    model.FlagPPM = int.Parse(row["FlagPPM"].ToString());
                }
                if (row["FlagMCV"] != null && row["FlagMCV"].ToString() != "")
                {
                    model.FlagMCV = int.Parse(row["FlagMCV"].ToString());
                }
                if (row["FlagELE"] != null && row["FlagELE"].ToString() != "")
                {
                    model.FlagELE = int.Parse(row["FlagELE"].ToString());
                }
                if (row["FlagVM"] != null && row["FlagVM"].ToString() != "")
                {
                    model.FlagVM = int.Parse(row["FlagVM"].ToString());
                }
                if (row["FlagRM"] != null && row["FlagRM"].ToString() != "")
                {
                    model.FlagRM = int.Parse(row["FlagRM"].ToString());
                }
                if (row["FlagSM"] != null && row["FlagSM"].ToString() != "")
                {
                    model.FlagSM = int.Parse(row["FlagSM"].ToString());
                }
                if (row["FlagUM"] != null && row["FlagUM"].ToString() != "")
                {
                    model.FlagUM = int.Parse(row["FlagUM"].ToString());
                }
                if (row["FlagVR"] != null && row["FlagVR"].ToString() != "")
                {
                    model.FlagVR = int.Parse(row["FlagVR"].ToString());
                }
                if (row["FlagSP"] != null && row["FlagSP"].ToString() != "")
                {
                    model.FlagSP = int.Parse(row["FlagSP"].ToString());
                }
                if (row["FlagOther"] != null && row["FlagOther"].ToString() != "")
                {
                    model.FlagOther = int.Parse(row["FlagOther"].ToString());
                }
                if (row["RepairAdress"] != null)
                {
                    model.RepairAdress = row["RepairAdress"].ToString();
                }
                if (row["ExpectEnterDate"] != null && row["ExpectEnterDate"].ToString() != "")
                {
                    model.ExpectEnterDate = DateTime.Parse(row["ExpectEnterDate"].ToString());
                }
                if (row["ExpectLeaveDate"] != null && row["ExpectLeaveDate"].ToString() != "")
                {
                    model.ExpectLeaveDate = DateTime.Parse(row["ExpectLeaveDate"].ToString());
                }
                if (row["ExpectTimeFee"] != null && row["ExpectTimeFee"].ToString() != "")
                {
                    model.ExpectTimeFee = decimal.Parse(row["ExpectTimeFee"].ToString());
                }
                if (row["ExpectPartFee"] != null && row["ExpectPartFee"].ToString() != "")
                {
                    model.ExpectPartFee = decimal.Parse(row["ExpectPartFee"].ToString());
                }
                if (row["ExpectFee"] != null && row["ExpectFee"].ToString() != "")
                {
                    model.ExpectFee = decimal.Parse(row["ExpectFee"].ToString());
                }
                if (row["CustOpinion"] != null)
                {
                    model.CustOpinion = row["CustOpinion"].ToString();
                }
                if (row["FlagAgreement"] != null && row["FlagAgreement"].ToString() != "")
                {
                    model.FlagAgreement = int.Parse(row["FlagAgreement"].ToString());
                }
                if (row["AgreementDate"] != null && row["AgreementDate"].ToString() != "")
                {
                    model.AgreementDate = DateTime.Parse(row["AgreementDate"].ToString());
                }
                if (row["AttachmentId_Agreement"] != null)
                {
                    model.AttachmentId_Agreement = row["AttachmentId_Agreement"].ToString();
                }
                if (row["RepairState"] != null && row["RepairState"].ToString() != "")
                {
                    model.RepairState = int.Parse(row["RepairState"].ToString());
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
                if (row["ActualEnterDate"] != null && row["ActualEnterDate"].ToString() != "")
                {
                    model.ActualEnterDate = DateTime.Parse(row["ActualEnterDate"].ToString());
                }
                if (row["OperaName_Enter"] != null)
                {
                    model.OperaName_Enter = row["OperaName_Enter"].ToString();
                }
                if (row["OperaTime_Enter"] != null && row["OperaTime_Enter"].ToString() != "")
                {
                    model.OperaTime_Enter = DateTime.Parse(row["OperaTime_Enter"].ToString());
                }
                if (row["ActualLeaveDate"] != null && row["ActualLeaveDate"].ToString() != "")
                {
                    model.ActualLeaveDate = DateTime.Parse(row["ActualLeaveDate"].ToString());
                }
                if (row["OperaName_Leave"] != null)
                {
                    model.OperaName_Leave = row["OperaName_Leave"].ToString();
                }
                if (row["OperaTime_Leave"] != null && row["OperaTime_Leave"].ToString() != "")
                {
                    model.OperaTime_Leave = DateTime.Parse(row["OperaTime_Leave"].ToString());
                }
                if (row["RepairMode"] != null && row["RepairMode"].ToString() != "")
                {
                    model.RepairMode = int.Parse(row["RepairMode"].ToString());
                }
                if (row["FlagLocale"] != null && row["FlagLocale"].ToString() != "")
                {
                    model.FlagLocale = int.Parse(row["FlagLocale"].ToString());
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
            strSql.Append("update repair_Intention_Package set FlagDel=1 ");
            strSql.Append("where FlagDel=0 and IntentionId in(" + IDList + ")");
            sqllist.Add(strSql.ToString());

            strSql = new StringBuilder();
            strSql.Append("update repair_Intention set FlagDel=1 ");
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
            strSql.Append("select a.ID,a.IntentionCode,a.IntentionDate,a.IntentionType,a.IntentionCode_Last,a.BusinessDepId,d.DepName as BusinessDepName,a.BusinessPerName,a.CustTypeId,a.CustName,a.MachineModelId,b.MachineModel,a.MachineCode,a.EngineModel,a.EngineCode,a.SMR,a.FlagFXGCH,a.Linkman,a.LinkPhone,a.MachineAdress,a.Machine,a.MachineStatus,a.FlagResult,a.RepairTypeId,c.RepairTypeName,a.RepairContent,a.FlagENGKC,a.FlagPPMKC,a.FlagENG,a.FlagPPM,a.FlagMCV,a.FlagELE,a.FlagVM,a.FlagRM,a.FlagSM,a.FlagUM,a.FlagVR,a.FlagSP,a.FlagOther,a.RepairAdress,a.ExpectEnterDate,a.ExpectLeaveDate,a.ExpectTimeFee,a.ExpectPartFee,a.ExpectFee,a.CustOpinion,a.FlagAgreement,a.AgreementDate,a.AttachmentId_Agreement,a.RepairState,a.OperaDepId,a.OperaId,a.OperaName,a.OperaTime,a.ActualEnterDate,a.OperaName_Enter,a.OperaTime_Enter,a.ActualLeaveDate,a.OperaName_Leave,a.OperaTime_Leave,a.AttachmentId_Agreement,a.RepairMode,a.FlagLocale ");
            strSql.Append("FROM repair_Intention a ");
            strSql.Append("left join base_MachineModel b on a.MachineModelId=b.ID and b.FlagDel=0 ");
            strSql.Append("left join base_RepairType c on a.RepairTypeId=c.ID and c.FlagDel=0 ");
            strSql.Append("left join sys_Department d on a.BusinessDepId=d.ID and d.FlagDel=0 ");
            //strSql.Append("left join sys_Attachment e on a.AttachmentId_Agreement=e.ID ");
            strSql.Append("where a.FlagDel=0");
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
            strSql.Append("select a.ID,a.IntentionCode,a.IntentionDate,a.IntentionType,a.IntentionCode_Last,a.BusinessDepId,a.BusinessPerName,a.CustTypeId,a.CustName,a.MachineModelId,b.MachineModel,a.MachineCode,a.EngineModel,a.EngineCode,a.SMR,a.FlagFXGCH,a.Linkman,a.LinkPhone,a.MachineAdress,a.Machine,a.MachineStatus,a.FlagResult,a.RepairTypeId,c.RepairTypeName,a.RepairContent,a.FlagENGKC,a.FlagPPMKC,a.FlagENG,a.FlagPPM,a.FlagMCV,a.FlagELE,a.FlagVM,a.FlagRM,a.FlagSM,a.FlagUM,a.FlagVR,a.FlagSP,a.FlagOther,a.RepairAdress,a.ExpectEnterDate,a.ExpectLeaveDate,a.ExpectTimeFee,a.ExpectPartFee,a.ExpectFee,a.CustOpinion,a.FlagAgreement,a.AgreementDate,a.AttachmentId_Agreement,a.RepairState,a.OperaDepId,a.OperaId,a.OperaName,a.OperaTime,a.ActualEnterDate,a.OperaName_Enter,a.OperaTime_Enter,a.ActualLeaveDate,a.OperaName_Leave,a.OperaTime_Leave,d.DepName,a.RepairMode,a.FlagLocale ");
            strSql.Append("FROM repair_Intention a ");
            strSql.Append("left join base_MachineModel b on a.MachineModelId=b.ID and b.FlagDel=0 ");
            strSql.Append("left join base_RepairType c on a.RepairTypeId=c.ID and c.FlagDel=0 ");
            //----------------------------------WX-----------------------------------------------
            strSql.Append("left join sys_Department d on a.OperaDepId=d.ID and d.FlagDel=0 ");
            //-----------------------------------------------------------------------------------------
            strSql.Append("where a.FlagDel=0 and a.ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            string AttachmentId = "";
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                AttachmentId = (ds.Tables[0].Rows[0]["AttachmentId_Agreement"].ToString() == "" ? "" : ds.Tables[0].Rows[0]["AttachmentId_Agreement"].ToString() + ",")
                    ;
            }
            DataTable dt = new System.sys_Attachment().GetAttachment(AttachmentId);
            dt.TableName = "attachment";
            ds.Tables.Add(dt.Copy());

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select a.ID,a.IntentionId,a.PackageId,c.MachineModel,b.PackageName,a.PackageNum ");
            strSql2.Append("FROM repair_Intention_Package a ");
            strSql2.Append("left join base_RepairPackage b on a.PackageId=b.ID and b.FlagDel=0 ");
            strSql2.Append("left join base_MachineModel c on b.MachineModelId=c.ID and c.FlagDel=0 ");
            strSql2.Append("where a.FlagDel=0 and IntentionId=@IntentionId ");
            SqlParameter[] parameters2 = {
				new SqlParameter("@IntentionId", SqlDbType.Int,4)};
            parameters2[0].Value = ID;
            DataTable dt2 = DbHelperSQL.Query(strSql2.ToString(), parameters2).Tables[0];
            dt2.TableName = "Intention_Package";
            ds.Tables.Add(dt2.Copy());
            return ds;
        }
        /// <summary>
        /// 获取最大ID+1
        /// </summary>
        /// <returns></returns>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "repair_Intention");
        }
        /// <summary>
        /// 批量登记入厂时间
        /// </summary>
        /// <param name="IDList"></param>
        /// <param name="ActualEnterDate"></param>
        /// <returns></returns>
        public int ActualEnterDate(string IDList, DateTime ActualEnterDate, string OperaName_Enter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql = new StringBuilder();
            strSql.Append("update repair_Intention set ActualEnterDate='" + ActualEnterDate + "', RepairState=20,OperaName_Enter='" + OperaName_Enter + "',OperaTime_Enter='" + DateTime.Now + "'");
            strSql.Append(" where FlagDel=0 and ID in(" + IDList + ")");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }
        /// <summary>
        /// 批量取消登记入厂时间
        /// </summary>
        /// <param name="IDList"></param>
        /// <param name="ActualEnterDate"></param>
        /// <returns></returns>
        public int CancelActualEnterDate(string IDList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql = new StringBuilder();
            strSql.Append("update repair_Intention set ActualEnterDate = null, RepairState=10,OperaName_Enter=null,OperaTime_Enter=null ");
            strSql.Append(" where FlagDel=0 and ID in(" + IDList + ")");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }
        /// <summary>
        /// 维修意向下拉框
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetComboList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 20 a.ID as IntentionId,a.IntentionCode,a.CustTypeId,a.CustName,a.MachineModelId,b.MachineModel,a.MachineCode,a.MachineStatus,a.CustOpinion,a.RepairContent,a.FlagENGKC,a.FlagPPMKC,a.FlagENG,a.FlagPPM,a.FlagMCV,a.FlagELE,a.FlagVM,a.FlagRM,a.FlagSM,a.FlagUM,a.FlagVR,a.FlagSP,a.FlagOther,a.IntentionDate,a.LinkPhone,a.RepairTypeId,e.RepairTypeName,a.BusinessDepId,c.DepName as BusinessDep,a.BusinessPerName,d.ID as SchemeId,d.SchemeCode,d.SchemeFee,f.ID as SettlementId,f.SettlementCode,f.SettlementFee_rebate,a.RepairMode,a.FlagLocale ");
            strSql.Append("from repair_Intention a ");
            strSql.Append("left join base_MachineModel b on a.MachineModelId=b.ID and b.FlagDel=0 ");
            strSql.Append("left join sys_Department c on c.FlagDel=0 and c.ID=a.BusinessDepId ");
            strSql.Append("left join repair_Scheme d on a.ID=d.IntentionId and d.FlagDel=0 ");
            strSql.Append("left join base_RepairType e on a.RepairTypeId=e.ID and e.FlagDel=0 ");
            strSql.Append("left join repair_SettlementList f on a.ID=f.IntentionId and f.FlagDel=0 ");
            strSql.Append("where a.FlagDel=0 ");
            if (strWhere != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.OperaTime desc");
            return DbHelperSQL.Query(strSql.ToString());
        }
        public int SaveAgreement(int ID, string AttachmentId_Agreement, DateTime AgreementDate)
        {
            string strWhere = "update repair_Intention set FlagAgreement=1,AttachmentId_Agreement='" + AttachmentId_Agreement + "',AgreementDate='" + AgreementDate + "' where FlagDel=0 and ID=" + ID;
            return DbHelperSQL.ExecuteSql(strWhere);
        }
        /// <summary>
        /// 批量登记出厂时间
        /// </summary>
        /// <param name="IDList"></param>
        /// <param name="ActualLeaveDate"></param>
        /// <returns></returns>
        public int ActualLeaveDate(string IDList, DateTime ActualLeaveDate, int LeaveTypeId, string OperaName_Leave)
        {
            StringBuilder strSql = new StringBuilder();
            strSql = new StringBuilder();
            strSql.Append("update repair_Intention set ActualLeaveDate='" + ActualLeaveDate + "', OperaName_Leave='" + OperaName_Leave + "',OperaTime_Leave='" + DateTime.Now + "',LeaveTypeId=" + LeaveTypeId + " ");
            strSql.Append(" where FlagDel=0 and ID in(" + IDList + ")");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }
        /// <summary>
        /// 批量取消登记出厂时间
        /// </summary>
        /// <param name="IDList"></param>
        /// <param name="ActualLeaveDate"></param>
        /// <returns></returns>
        public int CancelActualLeaveDate(string IDList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql = new StringBuilder();
            strSql.Append("update repair_Intention set ActualLeaveDate = null,OperaName_Leave=null,OperaTime_Leave=null,LeaveTypeId=null ");
            strSql.Append(" where FlagDel=0 and ID in(" + IDList + ")");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }
        /// <summary>
        /// 获得数据列表 通过Where条件
        /// </summary>
        public DataSet GetList_Leave(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.IntentionCode,a.IntentionDate,a.IntentionType,a.IntentionCode_Last,a.BusinessDepId,d.DepName as BusinessDepName,a.BusinessPerName,a.CustTypeId,a.CustName,a.MachineModelId,b.MachineModel,a.MachineCode,a.EngineModel,a.EngineCode,a.SMR,a.FlagFXGCH,a.Linkman,a.LinkPhone,a.MachineAdress,a.Machine,a.MachineStatus,a.FlagResult,a.RepairTypeId,c.RepairTypeName,a.RepairContent,a.FlagENGKC,a.FlagPPMKC,a.FlagENG,a.FlagPPM,a.FlagMCV,a.FlagELE,a.FlagVM,a.FlagRM,a.FlagSM,a.FlagUM,a.FlagVR,a.FlagSP,a.FlagOther,a.RepairAdress,a.ExpectEnterDate,a.ExpectLeaveDate,a.ExpectTimeFee,a.ExpectPartFee,a.ExpectFee,a.CustOpinion,a.FlagAgreement,a.AgreementDate,a.AttachmentId_Agreement,a.RepairState,a.OperaDepId,a.OperaId,a.OperaName,a.OperaTime,a.ActualEnterDate,a.OperaName_Enter,a.OperaTime_Enter,a.ActualLeaveDate,a.OperaName_Leave,a.OperaTime_Leave,a.AttachmentId_Agreement,f.SettlementFee_rebate,g.ReceiveFee_All,a.LeaveTypeId ");
            strSql.Append("FROM repair_Intention a ");
            strSql.Append("left join base_MachineModel b on a.MachineModelId=b.ID and b.FlagDel=0 ");
            strSql.Append("left join base_RepairType c on a.RepairTypeId=c.ID and c.FlagDel=0 ");
            strSql.Append("left join sys_Department d on a.BusinessDepId=d.ID and d.FlagDel=0 ");
            //strSql.Append("left join sys_Attachment e on a.AttachmentId_Agreement=e.ID ");
            strSql.Append("left join repair_SettlementList f on a.ID=f.IntentionId and f.FlagDel=0 ");
            strSql.Append("left join (select IntentionId,sum(ReceiveFee) as ReceiveFee_All from repair_ReceiveFee where FlagDel=0 group by IntentionId ) g on a.ID=g.IntentionId ");
            strSql.Append("where a.FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.OperaTime desc");
            return DbHelperSQL.Query(strSql.ToString());
        }
        public int UpdateAttachmentList(string IdList)
        {
            string id = IdList;
            return 0;
        }
        public int CheckUse(string IdList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from (");
            strSql.Append("select distinct IntentionId FROM repair_Scheme where FlagDel=0 ");
            strSql.Append("union select distinct IntentionId from repair_Contract where FlagDel=0 ");
            strSql.Append("union select distinct IntentionId from repair_Assignment where FlagDel=0 ");
            strSql.Append("union select distinct IntentionId from repair_SettlementList where FlagDel=0 ");
            strSql.Append(") a ");
            strSql.Append("where a.IntentionId in(" + IdList + ")");
            Object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        public int UpdateRepairState(string IntentionIdList, int RepairState)
        {
            string strSql0 = "Update repair_Intention set RepairState=" + RepairState + " where FlagDel=0 and ID in(" + IntentionIdList + ")";
            return DbHelperSQL.ExecuteSql(strSql0);
        }
        #endregion  扩展方法
    }
}

