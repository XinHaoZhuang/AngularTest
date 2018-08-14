using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.Repair
{
    /// <summary>
    /// 数据访问类repair_Schedule。
    /// </summary>
    public partial class repair_Schedule
    {
        public repair_Schedule()
        { }
        #region  基本方法
        /// <summary>
        /// 某列是否存在
        /// </summary>
        public bool Exists(string FieldName, string FieldValue, int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from repair_Schedule");
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
        public int Add(SCZM.Model.Repair.repair_Schedule model)
        {
            String strSql0 = "update repair_Schedule set FlagNew=1 where FlagDel=0 and FlagNew=0 and AssignmentProcedureId=" + model.AssignmentProcedureId;
            DbHelperSQL.ExecuteSql(strSql0);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into repair_Schedule(");
            strSql.Append("AssignmentId,AssignmentProcedureId,ProcedureId,ScheduleType,ScheduleDate,Memo,FlagDel,OperaDepId,OperaId,OperaName,OperaTime,AttachmentList_Schedule,PauseReason)");
            strSql.Append(" values (");
            strSql.Append("@AssignmentId,@AssignmentProcedureId,@ProcedureId,@ScheduleType,@ScheduleDate,@Memo,@FlagDel,@OperaDepId,@OperaId,@OperaName,@OperaTime,@AttachmentList_Schedule,@PauseReason)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
				new SqlParameter("@AssignmentId", SqlDbType.Int,4),
				new SqlParameter("@AssignmentProcedureId", SqlDbType.Int,4),
				new SqlParameter("@ProcedureId", SqlDbType.Int,4),
				new SqlParameter("@ScheduleType", SqlDbType.Int,4),
				new SqlParameter("@ScheduleDate", SqlDbType.SmallDateTime),
                new SqlParameter("@Memo",SqlDbType.NVarChar,200),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaDepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
                new SqlParameter("@AttachmentList_Schedule",SqlDbType.VarChar,100),
                                        new SqlParameter("@PauseReason",SqlDbType.Int,4)};
            parameters[0].Value = model.AssignmentId;
            parameters[1].Value = model.AssignmentProcedureId;
            parameters[2].Value = model.ProcedureId;
            parameters[3].Value = model.ScheduleType;
            parameters[4].Value = model.ScheduleDate;
            parameters[5].Value = model.Memo;
            parameters[6].Value = model.FlagDel;
            parameters[7].Value = model.OperaDepId;
            parameters[8].Value = model.OperaId;
            parameters[9].Value = model.OperaName;
            parameters[10].Value = model.OperaTime;
            parameters[11].Value = model.AttachmentList_Schedule;
            parameters[12].Value = model.PauseReason;

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
        public int Update(SCZM.Model.Repair.repair_Schedule model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update repair_Schedule set ");
            strSql.Append("AssignmentId=@AssignmentId,");
            strSql.Append("AssignmentProcedureId=@AssignmentProcedureId,");
            strSql.Append("ProcedureId=@ProcedureId,");
            strSql.Append("ScheduleType=@ScheduleType,");
            strSql.Append("ScheduleDate=@ScheduleDate,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaDepId=@OperaDepId,");
            strSql.Append("OperaId=@OperaId,");
            strSql.Append("OperaName=@OperaName,");
            //strSql.Append("OperaTime=@OperaTime,");
            strSql.Append("AttachmentList_Schedule=@AttachmentList_Schedule,");
            strSql.Append("PauseReason=@PauseReason,");
            strSql.Append("Memo=@Memo");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@AssignmentId", SqlDbType.Int,4),
				new SqlParameter("@AssignmentProcedureId", SqlDbType.Int,4),
				new SqlParameter("@ProcedureId", SqlDbType.Int,4),
				new SqlParameter("@ScheduleType", SqlDbType.Int,4),
				new SqlParameter("@ScheduleDate", SqlDbType.SmallDateTime),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaDepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
                //new SqlParameter("@OperaTime", SqlDbType.DateTime),
                new SqlParameter("@AttachmentList_Schedule",SqlDbType.VarChar,100),
                new SqlParameter("@PauseReason",SqlDbType.Int,4),
                new SqlParameter("@Memo",SqlDbType.NVarChar,200),
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.AssignmentId;
            parameters[1].Value = model.AssignmentProcedureId;
            parameters[2].Value = model.ProcedureId;
            parameters[3].Value = model.ScheduleType;
            parameters[4].Value = model.ScheduleDate;
            parameters[5].Value = model.FlagDel;
            parameters[6].Value = model.OperaDepId;
            parameters[7].Value = model.OperaId;
            parameters[8].Value = model.OperaName;
            //parameters[9].Value = model.OperaTime;
            parameters[9].Value = model.AttachmentList_Schedule;
            parameters[10].Value = model.PauseReason;
            parameters[11].Value = model.Memo;
            parameters[12].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SCZM.Model.Repair.repair_Schedule GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,AssignmentId,AssignmentProcedureId,ProcedureId,ScheduleType,ScheduleDate,FlagDel,OperaDepId,OperaId,OperaName,OperaTime,AttachmentList_Schedule,PauseReason from repair_Schedule ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.Repair.repair_Schedule model = new SCZM.Model.Repair.repair_Schedule();
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
        public SCZM.Model.Repair.repair_Schedule DataRowToModel(DataRow row)
        {
            SCZM.Model.Repair.repair_Schedule model = new SCZM.Model.Repair.repair_Schedule();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["AssignmentId"] != null && row["AssignmentId"].ToString() != "")
                {
                    model.AssignmentId = int.Parse(row["AssignmentId"].ToString());
                }
                if (row["AssignmentProcedureId"] != null && row["AssignmentProcedureId"].ToString() != "")
                {
                    model.AssignmentProcedureId = int.Parse(row["AssignmentProcedureId"].ToString());
                }
                if (row["ProcedureId"] != null)
                {
                    model.ProcedureId = int.Parse(row["ProcedureId"].ToString());
                }
                if (row["ScheduleType"] != null && row["ScheduleType"].ToString() != "")
                {
                    model.ScheduleType = int.Parse(row["ScheduleType"].ToString());
                }
                if (row["ScheduleDate"] != null && row["ScheduleDate"].ToString() != "")
                {
                    model.ScheduleDate = DateTime.Parse(row["ScheduleDate"].ToString());
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
                if (row["AttachmentList_Schedule"] != null && row["AttachmentList_Schedule"].ToString() != "")
                {
                    model.AttachmentList_Schedule = row["AttachmentList_Schedule"].ToString();
                }
                if (row["PauseReason"] != null) {
                    model.PauseReason = int.Parse(row["PauseReason"].ToString());
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
            strSql.Append("update repair_Schedule set FlagDel=1 ");
            strSql.Append(" where FlagDel=0 and ID in(" + IDList + ")");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public int DeleteLastSchedule(int lastId,int prevId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update repair_Schedule set FlagDel=1 ");
            strSql.Append(" where FlagDel=0 and ID="+lastId);
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            StringBuilder strSql_prev = new StringBuilder();
            strSql_prev.Append("update repair_Schedule set FlagNew=0 ");
            strSql_prev.Append(" where FlagDel=0 and ID=" + prevId);
            DbHelperSQL.ExecuteSql(strSql_prev.ToString());
            return rows;
        }
        /// <summary>
        /// 获得数据列表 通过Where条件
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.AssignmentId,a.AssignmentProcedureId,a.Memo,a.ProcedureId,b.AssignmentCode,c.ProcedureName,e.PerName as MainRepairName,a.ScheduleType,a.ScheduleDate,a.OperaDepId,a.OperaId,a.OperaName,a.OperaTime,a.AttachmentList_Schedule,a.PauseReason ");
            strSql.Append("FROM repair_Schedule a ");
            strSql.Append("left join repair_Assignment b on a.AssignmentId=b.ID and b.FlagDel=0 ");
            strSql.Append("left join base_Procedure c on a.ProcedureId=c.ID and c.FlagDel=0 ");
            strSql.Append("left join sys_Person e on b.MainRepair=e.ID ");
            strSql.Append("where a.FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.OperaTime asc");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表 通过Where条件
        /// </summary>
        public DataSet GetList_HaveAttachment(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.AssignmentId,a.AssignmentProcedureId,a.Memo,a.ProcedureId,b.AssignmentCode,c.ProcedureName,e.PerName as MainRepairName,a.ScheduleType,a.ScheduleDate,a.OperaDepId,a.OperaId,a.OperaName,a.OperaTime,a.AttachmentList_Schedule,(select FileName+'⊥' from sys_Attachment where CHARINDEX(','+LTRIM(ID)+',',','+a.AttachmentList_Schedule+',')>0 for xml path(''))as AttachmentList_ScheduleName,(select FilePath+'⊥' from sys_Attachment where CHARINDEX(','+LTRIM(ID)+',',','+a.AttachmentList_Schedule+',')>0 for xml path(''))as AttachmentList_SchedulePath,a.PauseReason ");
            strSql.Append("FROM repair_Schedule a ");
            strSql.Append("left join repair_Assignment b on a.AssignmentId=b.ID and b.FlagDel=0 ");
            strSql.Append("left join base_Procedure c on a.ProcedureId=c.ID and c.FlagDel=0 ");
            strSql.Append("left join sys_Person e on b.MainRepair=e.ID ");
            strSql.Append("where a.FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.OperaTime asc");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据明细 通过ID
        /// </summary>
        public DataSet GetDetail(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.AssignmentId,a.Memo,a.AssignmentProcedureId,a.ProcedureId,a.ScheduleType,a.ScheduleDate,a.OperaDepId,a.OperaId,a.OperaName,a.OperaTime,a.AttachmentList_Schedule,a.PauseReason ");
            strSql.Append("FROM repair_Schedule a ");
            strSql.Append("where a.FlagDel=0 and a.ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            string AttachmentId = "";
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                AttachmentId = (ds.Tables[0].Rows[0]["AttachmentList_Schedule"].ToString() == "" ? "" : ds.Tables[0].Rows[0]["AttachmentList_Schedule"].ToString() + ",");
            }
            DataTable dt = new System.sys_Attachment().GetAttachment(AttachmentId);
            dt.TableName = "attachment";
            ds.Tables.Add(dt.Copy());
            return ds;
        }
        public DataSet GetRepairSceond(int AssignmentProcedureId) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("with cte as(select *,DATEDIFF(S,'1970-01-01 08:00:00',ScheduleDate) as second from repair_Schedule where FlagDel=0 and AssignmentProcedureId="+AssignmentProcedureId+"),");
            strSql.Append("cte_1 as(select *,DATEDIFF(S,'1970-01-01 08:00:00',ScheduleDate) as second from repair_Schedule where FlagDel=0 and AssignmentProcedureId="+AssignmentProcedureId+" and ScheduleType=1),");
            strSql.Append("cte_2and3 as(select *,DATEDIFF(S,'1970-01-01 08:00:00',ScheduleDate) as second from repair_Schedule where FlagDel=0 and AssignmentProcedureId="+AssignmentProcedureId+" and ScheduleType in(2,3)),");
            strSql.Append("cte_min1 as (select MIN(DATEDIFF(S,'1970-01-01 08:00:00',ScheduleDate)) as min_second from repair_Schedule where FlagDel=0 and AssignmentProcedureId="+AssignmentProcedureId+" and ScheduleType=1 ),");
            strSql.Append("cte_max3 as(select MAX(DATEDIFF(S,'1970-01-01 08:00:00',ScheduleDate)) as max_second from repair_Schedule where FlagDel=0 and AssignmentProcedureId="+AssignmentProcedureId+" and ScheduleType=3 )");
            strSql.Append("select RepairSecond,AllSecond,(AllSecond-RepairSecond)as PauseSecond from(select (select SUM(CAST(second as bigint)) from cte_2and3)-(select SUM(CAST(second as bigint)) from cte_1) as RepairSecond,((select max_second from cte_max3)-(select min_second from cte_min1))as AllSecond) as a");
            return DbHelperSQL.Query(strSql.ToString());
        }
        public int GetScheduleType(int AssignmentProcedureId)
        {
            string strSql = "select ScheduleType from Repair_Schedule where FlagNew=0 and FlagDel=0 and AssignmentProcedureId=" + AssignmentProcedureId;
            object obj=DbHelperSQL.GetSingle(strSql);
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }
            else {
                return -1;
            }
        }
        public bool CheckSaveData(DateTime ScheduleDate, int ScheduleId, int AssignmentProcedureId) {

            try
            {
                if (ScheduleId == null||ScheduleId == -1)
                {
                    string strSql = "select ScheduleDate from Repair_Schedule where FlagNew=0 and FlagDel=0 and AssignmentProcedureId=" + AssignmentProcedureId;
                    DateTime currentDate = Convert.ToDateTime(DbHelperSQL.GetSingle(strSql));
                    if (currentDate > ScheduleDate)
                    {
                        return false;
                    }
                }
                else
                {
                    string strSql = @"with
 cte_prev as (
	select MAX(ScheduleDate) as date_Prev from repair_Schedule where FlagDel = 0 and AssignmentProcedureId = " + AssignmentProcedureId + @" and ID<" + ScheduleId + @"
),
cte_fut as (
	select min(ScheduleDate) as date_Prev from repair_Schedule where FlagDel = 0 and AssignmentProcedureId = " + AssignmentProcedureId + @" and ID>" + ScheduleId + @"
)
select (select date_Prev from cte_prev) as date_Prev,(select date_Prev from cte_fut) as date_Prev";
                    DataTable dt=DbHelperSQL.Query(strSql).Tables[0];
                    if (dt.Rows[0][0] != null)
                    {
                        if (Convert.ToDateTime(dt.Rows[0][0]) > ScheduleDate) {
                            return false;
                        }
                    }
                    if (dt.Rows[0][1] != null) {
                        if (Convert.ToDateTime(dt.Rows[0][1]) < ScheduleDate)
                        {
                            return false;
                        }
                    }
                }
            }
            catch
            {
                return true;
            }
            return true;
        }
        #endregion  扩展方法
    }
}

