using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;//请先添加引用
namespace SCZM.DAL.Repair
{
    /// <summary>
    /// 数据访问类repair_Scheme。
    /// </summary>
    public partial class repair_Scheme
    {
        public repair_Scheme()
        { }
        #region  基本方法
        /// <summary>
        /// 某列是否存在
        /// </summary>
        public bool Exists(string FieldName, string FieldValue, int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from repair_Scheme");
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
        public int Add(SCZM.Model.Repair.repair_Scheme model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into repair_Scheme(");
            strSql.Append("IntentionId,SchemeCode,RepairContent,FlagENGKC,FlagPPMKC,FlagENG,FlagPPM,FlagMCV,FlagELE,FlagVM,FlagRM,FlagSM,FlagUM,FlagVR,SchemeDate,PromiseLeaveDate,TimeFee,PartFee,SchemeFee,FlagDel,OperaDepId,OperaId,OperaName,OperaTime,FlagSP,FlagOther,AttachmentId_Scheme,SchemeDate_predict,AttachmentId_Scheme_predict,SchemeFee_predict,Memo)");
            strSql.Append(" values (");
            strSql.Append("@IntentionId,@SchemeCode,@RepairContent,@FlagENGKC,@FlagPPMKC,@FlagENG,@FlagPPM,@FlagMCV,@FlagELE,@FlagVM,@FlagRM,@FlagSM,@FlagUM,@FlagVR,@SchemeDate,@PromiseLeaveDate,@TimeFee,@PartFee,@SchemeFee,@FlagDel,@OperaDepId,@OperaId,@OperaName,@OperaTime,@FlagSP,@FlagOther,@AttachmentId_Scheme,@SchemeDate_predict,@AttachmentId_Scheme_predict,@SchemeFee_predict,@Memo )");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
				new SqlParameter("@IntentionId", SqlDbType.Int,4),
				new SqlParameter("@SchemeCode", SqlDbType.VarChar,20),
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
				new SqlParameter("@SchemeDate", SqlDbType.SmallDateTime),
				new SqlParameter("@PromiseLeaveDate", SqlDbType.SmallDateTime),
				new SqlParameter("@TimeFee", SqlDbType.Decimal,9),
				new SqlParameter("@PartFee", SqlDbType.Decimal,9),
				new SqlParameter("@SchemeFee", SqlDbType.Decimal,9),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaDepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
                new SqlParameter("@FlagSP",SqlDbType.Int,4),
                new SqlParameter("@FlagOther",SqlDbType.Int,4),
                new SqlParameter("@AttachmentId_Scheme",SqlDbType.VarChar,100),
                new SqlParameter("@SchemeDate_predict",SqlDbType.SmallDateTime),
                //AttachmentId_Scheme_predict,SchemeFee_predict,Memo
                new SqlParameter("@AttachmentId_Scheme_predict",SqlDbType.VarChar,100),
                new SqlParameter("@SchemeFee_predict",SqlDbType.Decimal,18),
                new SqlParameter("@Memo",SqlDbType.NVarChar,200),
				new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.IntentionId;
            parameters[1].Value = model.SchemeCode;
            parameters[2].Value = model.RepairContent;
            parameters[3].Value = model.FlagENGKC;
            parameters[4].Value = model.FlagPPMKC;
            parameters[5].Value = model.FlagENG;
            parameters[6].Value = model.FlagPPM;
            parameters[7].Value = model.FlagMCV;
            parameters[8].Value = model.FlagELE;
            parameters[9].Value = model.FlagVM;
            parameters[10].Value = model.FlagRM;
            parameters[11].Value = model.FlagSM;
            parameters[12].Value = model.FlagUM;
            parameters[13].Value = model.FlagVR;
            parameters[14].Value = model.SchemeDate;
            parameters[15].Value = model.PromiseLeaveDate;
            parameters[16].Value = model.TimeFee;
            parameters[17].Value = model.PartFee;
            parameters[18].Value = model.SchemeFee;
            parameters[19].Value = model.FlagDel;
            parameters[20].Value = model.OperaDepId;
            parameters[21].Value = model.OperaId;
            parameters[22].Value = model.OperaName;
            parameters[23].Value = model.OperaTime;
            parameters[24].Value = model.FlagSP;
            parameters[25].Value = model.FlagOther;
            parameters[26].Value = model.AttachmentId_Scheme;
            parameters[27].Value = model.SchemeDate_predict;
            parameters[28].Value = model.AttachmentId_Scheme_predict;
            parameters[29].Value = model.SchemeFee_predict;
            parameters[30].Value = model.Memo;
            parameters[31].Direction = ParameterDirection.Output;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql4=new StringBuilder();
            strSql4.Append("Update repair_Intention set RepairState=30 where ID=@IntentionId and FlagDel=0");
            SqlParameter[] parameters4 ={
                    new SqlParameter("@IntentionId",SqlDbType.Int,4)
                                       };
            parameters4[0].Value = model.IntentionId;
            cmd = new CommandInfo(strSql4.ToString(), parameters4);
            sqllist.Add(cmd);
            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[31].Value;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(SCZM.Model.Repair.repair_Scheme model)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update repair_Scheme set ");
            strSql.Append("IntentionId=@IntentionId,");
            strSql.Append("SchemeCode=@SchemeCode,");
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
            strSql.Append("SchemeDate=@SchemeDate,");
            strSql.Append("PromiseLeaveDate=@PromiseLeaveDate,");
            strSql.Append("TimeFee=@TimeFee,");
            strSql.Append("PartFee=@PartFee,");
            strSql.Append("SchemeFee=@SchemeFee,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaDepId=@OperaDepId,");
            strSql.Append("OperaId=@OperaId,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime,");
            strSql.Append("FlagSP=@FlagSP,");
            strSql.Append("FlagOther=@FlagOther,");
            strSql.Append("AttachmentId_Scheme=@AttachmentId_Scheme,");
            strSql.Append("SchemeDate_predict=@SchemeDate_predict,");
            strSql.Append("AttachmentId_Scheme_predict=@AttachmentId_Scheme_predict,");
            strSql.Append("SchemeFee_predict=@SchemeFee_predict,");
            strSql.Append("Memo=@Memo");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@IntentionId", SqlDbType.Int,4),
				new SqlParameter("@SchemeCode", SqlDbType.VarChar,20),
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
				new SqlParameter("@SchemeDate", SqlDbType.SmallDateTime),
				new SqlParameter("@PromiseLeaveDate", SqlDbType.SmallDateTime),
				new SqlParameter("@TimeFee", SqlDbType.Decimal,9),
				new SqlParameter("@PartFee", SqlDbType.Decimal,9),
				new SqlParameter("@SchemeFee", SqlDbType.Decimal,9),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaDepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
                new SqlParameter("@FlagSP",SqlDbType.Int,4),
                new SqlParameter("@FlagOther",SqlDbType.Int,4),
                new SqlParameter("@AttachmentId_Scheme",SqlDbType.VarChar,100),
                new SqlParameter("@SchemeDate_predict",SqlDbType.SmallDateTime),
                new SqlParameter("@AttachmentId_Scheme_predict",SqlDbType.VarChar,100),
                new SqlParameter("@SchemeFee_predict",SqlDbType.Decimal,18),
                new SqlParameter("@Memo",SqlDbType.NVarChar,200),
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.IntentionId;
            parameters[1].Value = model.SchemeCode;
            parameters[2].Value = model.RepairContent;
            parameters[3].Value = model.FlagENGKC;
            parameters[4].Value = model.FlagPPMKC;
            parameters[5].Value = model.FlagENG;
            parameters[6].Value = model.FlagPPM;
            parameters[7].Value = model.FlagMCV;
            parameters[8].Value = model.FlagELE;
            parameters[9].Value = model.FlagVM;
            parameters[10].Value = model.FlagRM;
            parameters[11].Value = model.FlagSM;
            parameters[12].Value = model.FlagUM;
            parameters[13].Value = model.FlagVR;
            parameters[14].Value = model.SchemeDate;
            parameters[15].Value = model.PromiseLeaveDate;
            parameters[16].Value = model.TimeFee;
            parameters[17].Value = model.PartFee;
            parameters[18].Value = model.SchemeFee;
            parameters[19].Value = model.FlagDel;
            parameters[20].Value = model.OperaDepId;
            parameters[21].Value = model.OperaId;
            parameters[22].Value = model.OperaName;
            parameters[23].Value = model.OperaTime;
            parameters[24].Value = model.FlagSP;
            parameters[25].Value = model.FlagOther;
            parameters[26].Value = model.AttachmentId_Scheme;
            parameters[27].Value = model.SchemeDate_predict;
            parameters[28].Value = model.AttachmentId_Scheme_predict;
            parameters[29].Value = model.SchemeFee_predict;
            parameters[30].Value = model.Memo;
            parameters[31].Value = model.ID;

            cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql6 = new StringBuilder();
            strSql6.Append("Update repair_Intention set RepairState=30 where ID=@IntentionId and FlagDel=0");
            SqlParameter[] parameters6 ={
                    new SqlParameter("@IntentionId",SqlDbType.Int,4)
                                       };
            parameters6[0].Value = model.IntentionId;
            cmd = new CommandInfo(strSql6.ToString(), parameters6);
            sqllist.Add(cmd);
            int rows = DbHelperSQL.ExecuteSqlTran(sqllist);
            return rows;
        }


        /// <summary>
        /// 得到一个对象实体 主表
        /// </summary>
        public SCZM.Model.Repair.repair_Scheme GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,IntentionId,SchemeCode,RepairContent,FlagENGKC,FlagPPMKC,FlagENG,FlagPPM,FlagMCV,FlagELE,FlagVM,FlagRM,FlagSM,FlagUM,FlagVR,FlagSP,FlagOther,SchemeDate,PromiseLeaveDate,TimeFee,PartFee,SchemeFee,FlagDel,OperaDepId,OperaId,OperaName,OperaTime,SchemeDate_predict,AttachmentId_Scheme_predict,SchemeFee_predict,Memo from repair_Scheme ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.Repair.repair_Scheme model = new SCZM.Model.Repair.repair_Scheme();
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
        public SCZM.Model.Repair.repair_Scheme DataRowToModel(DataRow row)
        {
            SCZM.Model.Repair.repair_Scheme model = new SCZM.Model.Repair.repair_Scheme();
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
                if (row["SchemeCode"] != null)
                {
                    model.SchemeCode = row["SchemeCode"].ToString();
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
                if (row["SchemeDate"] != null && row["SchemeDate"].ToString() != "")
                {
                    model.SchemeDate = DateTime.Parse(row["SchemeDate"].ToString());
                }
                if (row["PromiseLeaveDate"] != null && row["PromiseLeaveDate"].ToString() != "")
                {
                    model.PromiseLeaveDate = DateTime.Parse(row["PromiseLeaveDate"].ToString());
                }
                if (row["TimeFee"] != null && row["TimeFee"].ToString() != "")
                {
                    model.TimeFee = decimal.Parse(row["TimeFee"].ToString());
                }
                if (row["PartFee"] != null && row["PartFee"].ToString() != "")
                {
                    model.PartFee = decimal.Parse(row["PartFee"].ToString());
                }
                if (row["SchemeFee"] != null && row["SchemeFee"].ToString() != "")
                {
                    model.SchemeFee = decimal.Parse(row["SchemeFee"].ToString());
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
                if (row["SchemeDate_predict"] != null && row["SchemeDate_predict"].ToString()!="") {
                    model.SchemeDate_predict = DateTime.Parse(row["SchemeDate_predict"].ToString());
                }
                if (row["AttachmentId_Scheme_predict"] != null && row["AttachmentId_Scheme_predict"].ToString() != "") {
                    model.AttachmentId_Scheme_predict = row["AttachmentId_Scheme_predict"].ToString();
                }
                if (row["SchemeFee_predict"] != null && row["SchemeFee_predict"].ToString() != "")
                {
                    model.SchemeFee = decimal.Parse(row["SchemeFee_predict"].ToString());
                }
                if (row["Memo"] != null && row["Memo"].ToString() != "") {
                    model.Memo = row["Memo"].ToString();
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
            strSql.Append("update repair_Scheme set FlagDel=1 ");
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
            strSql.Append("select a.ID,case when b.ID is null then case a.CustTypeId when 2 then -1 else b.ID end else b.ID end as SchemeId,b.SchemeCode,b.SchemeDate,b.SchemeFee,a.IntentionCode,a.CustName,a.MachineModelId,d.MachineModel,a.MachineCode,a.RepairTypeId,a.RepairState,c.RepairTypeName,a.BusinessDepId,e.DepName as BusinessDep,a.OperaName as IntentionOperaName,a.OperaTime as IntentionOperaTime,b.OperaName as SchemeOperaName,b.OperaTime as SchemeOperaTime,a.Linkman,a.CustTypeId,b.SchemeDate_predict,b.AttachmentId_Scheme_predict,b.SchemeFee_predict,b.Memo,a.AttachmentId_Agreement,b.AttachmentId_Scheme ");
            strSql.Append("FROM repair_Intention a ");
            strSql.Append("left join repair_Scheme b on b.IntentionId=a.ID and b.FlagDel=0 ");
            strSql.Append("left join base_RepairType c on a.RepairTypeId=c.ID and c.FlagDel=0 ");
            strSql.Append("left join base_MachineModel d on a.MachineModelId=d.ID and d.FlagDel=0 ");
            strSql.Append("left join sys_Department e on a.BusinessDepId =e.ID and e.FlagDel=0  ");
            //strSql.Append("where a.FlagDel=0 and a.RepairState>=20 ");
            //------------------2018/3/30------可跳过入场登记-----------
            strSql.Append("where a.FlagDel=0 ");
            //------------------------------------------------------------
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
            strSql.Append("select a.ID,a.IntentionId,a.SchemeCode,b.CustName,b.IntentionCode,b.RepairState,b.RepairAdress,b.MachineStatus,b.MachineCode,d.MachineModel,a.RepairContent,a.FlagENGKC,a.FlagPPMKC,a.FlagENG,a.FlagPPM,a.FlagMCV,a.FlagELE,a.FlagVM,a.FlagRM,a.FlagSM,a.FlagUM,a.FlagVR,a.FlagSP,a.FlagOther,a.SchemeDate,a.PromiseLeaveDate,b.ExpectLeaveDate,a.TimeFee,a.PartFee,a.SchemeFee,a.OperaDepId,a.OperaId,a.OperaName,a.OperaTime,a.AttachmentId_Scheme,(select PerName+',' from (select distinct bb.PerName from Repair_Assignment aa left join sys_Person bb on aa.MainRepair=bb.ID where aa.FlagDel=0 and aa.IntentionId=b.ID ) a for xml path('')) as MainRepairs,a.SchemeDate_predict,a.AttachmentId_Scheme_predict,a.SchemeFee_predict,a.Memo,b.AttachmentId_Agreement ");
            strSql.Append("FROM repair_Scheme a ");
            strSql.Append("left join repair_Intention b on a.IntentionId=b.ID and b.FlagDel=0 ");
            strSql.Append("left join base_RepairType c on b.RepairTypeId=c.ID and c.FlagDel=0 ");
            strSql.Append("left join base_MachineModel d on b.MachineModelId=d.ID and d.FlagDel=0 ");
            strSql.Append("where a.FlagDel=0 and a.ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            string AttachmentId_Scheme_predict = "";
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                AttachmentId_Scheme_predict = (ds.Tables[0].Rows[0]["AttachmentId_Scheme_predict"].ToString() == "" ? "" : ds.Tables[0].Rows[0]["AttachmentId_Scheme_predict"].ToString() + ",")
                    ;
            }
            DataTable dt4 = new System.sys_Attachment().GetAttachment(AttachmentId_Scheme_predict);
            dt4.TableName = "AttachmentId_Scheme_predict";
            ds.Tables.Add(dt4.Copy());

            string AttachmentId_Scheme = "";
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                AttachmentId_Scheme = (ds.Tables[0].Rows[0]["AttachmentId_Scheme"].ToString() == "" ? "" : ds.Tables[0].Rows[0]["AttachmentId_Scheme"].ToString() + ",")
                    ;
            }
            DataTable dt3 = new System.sys_Attachment().GetAttachment(AttachmentId_Scheme);
            dt3.TableName = "AttachmentId_Scheme";
            ds.Tables.Add(dt3.Copy());

            string AttachmentId_Agreement = "";
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                AttachmentId_Agreement = (ds.Tables[0].Rows[0]["AttachmentId_Agreement"].ToString() == "" ? "" : ds.Tables[0].Rows[0]["AttachmentId_Agreement"].ToString() + ",")
                    ;
            }
            DataTable dt2 = new System.sys_Attachment().GetAttachment(AttachmentId_Agreement);
            dt2.TableName = "AttachmentId_Agreement";
            ds.Tables.Add(dt2.Copy());
            
            return ds;
        }
        /// <summary>
        /// 获取最大ID+1
        /// </summary>
        /// <returns></returns>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "repair_Scheme");
        }
        public int DelAttachmentId_Agreement(string IDList) {
            string strsql = "update repair_Intention set AttachmentId_Agreement=null,AgreementDate=null,FlagAgreement=0 where FlagDel=0 and ID in(select IntentionId from repair_Scheme a where ID in(" + IDList + ")) ";
            return DbHelperSQL.ExecuteSql(strsql);
        }
        #endregion  扩展方法
    }
}

