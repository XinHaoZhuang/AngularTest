using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.System
{
    /// <summary>
    /// 数据访问类sys_Process_Exec。

    /// </summary>
    public partial class sys_Process_Exec
    {
        public sys_Process_Exec()
        { }
        #region  基本方法
        /// <summary>
        /// 是否存在该记录

        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sys_Process_Exec");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据

        /// </summary>
        public int Add(SCZM.Model.System.sys_Process_Exec model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_Process_Exec(");
            strSql.Append("ProcessId,BillSign,BillId,OrderId,PostId,PostName,AuditorId,AuditorName,AuditorTime,FlagAudit,AuditorOpinion,FlagDel)");
            strSql.Append(" values (");
            strSql.Append("@ProcessId,@BillSign,@BillId,@OrderId,@PostId,@PostName,@AuditorId,@AuditorName,@AuditorTime,@FlagAudit,@AuditorOpinion,@FlagDel)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ProcessId", SqlDbType.Int,4),
					new SqlParameter("@BillSign", SqlDbType.VarChar,50),
					new SqlParameter("@BillId", SqlDbType.Int,4),
					new SqlParameter("@OrderId", SqlDbType.Int,4),
					new SqlParameter("@PostId", SqlDbType.Int,4),
					new SqlParameter("@PostName", SqlDbType.NVarChar,10),
					new SqlParameter("@AuditorId", SqlDbType.NChar,10),
					new SqlParameter("@AuditorName", SqlDbType.NVarChar,20),
					new SqlParameter("@AuditorTime", SqlDbType.DateTime),
					new SqlParameter("@FlagAudit", SqlDbType.Int,4),
					new SqlParameter("@AuditorOpinion", SqlDbType.NVarChar,100),
					new SqlParameter("@FlagDel", SqlDbType.Bit,1)};
            parameters[0].Value = model.ProcessId;
            parameters[1].Value = model.BillSign;
            parameters[2].Value = model.BillId;
            parameters[3].Value = model.OrderId;
            parameters[4].Value = model.PostId;
            parameters[5].Value = model.PostName;
            parameters[6].Value = model.AuditorId;
            parameters[7].Value = model.AuditorName;
            parameters[8].Value = model.AuditorTime;
            parameters[9].Value = model.FlagAudit;
            parameters[10].Value = model.AuditorOpinion;
            parameters[11].Value = model.FlagDel;

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
        public int Update(SCZM.Model.System.sys_Process_Exec model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Process_Exec set ");
            strSql.Append("ProcessId=@ProcessId,");
            strSql.Append("BillSign=@BillSign,");
            strSql.Append("BillId=@BillId,");
            strSql.Append("OrderId=@OrderId,");
            strSql.Append("PostId=@PostId,");
            strSql.Append("PostName=@PostName,");
            strSql.Append("AuditorId=@AuditorId,");
            strSql.Append("AuditorName=@AuditorName,");
            strSql.Append("AuditorTime=@AuditorTime,");
            strSql.Append("FlagAudit=@FlagAudit,");
            strSql.Append("AuditorOpinion=@AuditorOpinion,");
            strSql.Append("FlagDel=@FlagDel");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProcessId", SqlDbType.Int,4),
					new SqlParameter("@BillSign", SqlDbType.VarChar,50),
					new SqlParameter("@BillId", SqlDbType.Int,4),
					new SqlParameter("@OrderId", SqlDbType.Int,4),
					new SqlParameter("@PostId", SqlDbType.Int,4),
					new SqlParameter("@PostName", SqlDbType.NVarChar,10),
					new SqlParameter("@AuditorId", SqlDbType.NChar,10),
					new SqlParameter("@AuditorName", SqlDbType.NVarChar,20),
					new SqlParameter("@AuditorTime", SqlDbType.DateTime),
					new SqlParameter("@FlagAudit", SqlDbType.Int,4),
					new SqlParameter("@AuditorOpinion", SqlDbType.NVarChar,100),
					new SqlParameter("@FlagDel", SqlDbType.Bit,1),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.ProcessId;
            parameters[1].Value = model.BillSign;
            parameters[2].Value = model.BillId;
            parameters[3].Value = model.OrderId;
            parameters[4].Value = model.PostId;
            parameters[5].Value = model.PostName;
            parameters[6].Value = model.AuditorId;
            parameters[7].Value = model.AuditorName;
            parameters[8].Value = model.AuditorTime;
            parameters[9].Value = model.FlagAudit;
            parameters[10].Value = model.AuditorOpinion;
            parameters[11].Value = model.FlagDel;
            parameters[12].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 删除一条数据

        /// </summary>
        public int Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Process_Exec set FlagDel=1 ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public int DeleteList(string IDList)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Process_Exec set FlagDel=1 ");
            strSql.Append(" where FlagDel=0 and ID in(" + IDList + ")");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }


        /// <summary>
        /// 得到一个对象实体

        /// </summary>
        public SCZM.Model.System.sys_Process_Exec GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ProcessId,BillSign,BillId,OrderId,PostId,PostName,AuditorId,AuditorName,AuditorTime,FlagAudit,AuditorOpinion,FlagDel from sys_Process_Exec ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.System.sys_Process_Exec model = new SCZM.Model.System.sys_Process_Exec();
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
        public SCZM.Model.System.sys_Process_Exec DataRowToModel(DataRow row)
        {
            SCZM.Model.System.sys_Process_Exec model = new SCZM.Model.System.sys_Process_Exec();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["ProcessId"] != null && row["ProcessId"].ToString() != "")
                {
                    model.ProcessId = int.Parse(row["ProcessId"].ToString());
                }
                if (row["BillSign"] != null)
                {
                    model.BillSign = row["BillSign"].ToString();
                }
                if (row["BillId"] != null && row["BillId"].ToString() != "")
                {
                    model.BillId = int.Parse(row["BillId"].ToString());
                }
                if (row["OrderId"] != null && row["OrderId"].ToString() != "")
                {
                    model.OrderId = int.Parse(row["OrderId"].ToString());
                }
                if (row["PostId"] != null && row["PostId"].ToString() != "")
                {
                    model.PostId = int.Parse(row["PostId"].ToString());
                }
                if (row["PostName"] != null)
                {
                    model.PostName = row["PostName"].ToString();
                }
                if (row["AuditorId"] != null)
                {
                    model.AuditorId = row["AuditorId"].ToString();
                }
                if (row["AuditorName"] != null)
                {
                    model.AuditorName = row["AuditorName"].ToString();
                }
                if (row["AuditorTime"] != null && row["AuditorTime"].ToString() != "")
                {
                    model.AuditorTime = DateTime.Parse(row["AuditorTime"].ToString());
                }
                if (row["FlagAudit"] != null && row["FlagAudit"].ToString() != "")
                {
                    model.FlagAudit = int.Parse(row["FlagAudit"].ToString());
                }
                if (row["AuditorOpinion"] != null)
                {
                    model.AuditorOpinion = row["AuditorOpinion"].ToString();
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
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表 通过SQL语句
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ProcessId,BillSign,BillId,OrderId,PostId,PostName,AuditorId,AuditorName,AuditorTime,FlagAudit,AuditorOpinion,FlagDel ");
            strSql.Append("FROM sys_Process_Exec");
            strSql.Append(" where FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表 通过ID
        /// </summary>
        public DataSet GetList(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ProcessId,BillSign,BillId,OrderId,PostId,PostName,AuditorId,AuditorName,AuditorTime,FlagAudit,AuditorOpinion,FlagDel ");
            strSql.Append("FROM sys_Process_Exec");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得数据列表 通过Param
        /// </summary>
        public DataSet GetList(string strWhere, List<SqlParameter> parameterList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ProcessId,BillSign,BillId,OrderId,PostId,PostName,AuditorId,AuditorName,AuditorTime,FlagAudit,AuditorOpinion,FlagDel ");
            strSql.Append("FROM sys_Process_Exec");
            strSql.Append(" where FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parameterList);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM sys_Process_Exec ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from sys_Process_Exec T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "sys_Process_Exec";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  基本方法
        #region  扩展方法
        /// <summary>
        /// 通过审批流判断单据动作是否合法 新增 修改 
        /// </summary>
        /// <param name="billSign">单据识别号</param>
        /// <param name="billId">单据流水号</param>
        /// <param name="action">动作 1 add\2 edit</param>
        /// <param name="depId">单据部门ID</param>
        /// <param name="operaId">操作人ID</param>
        /// <returns>错误信息</returns>
        public string CheckActionByProcess_Edit(string billSign, int billId, int action, int depId,int operaId)
        {
            string message = "";
            string procName = "p_sys_CheckActionByProcess_Edit";
            SqlParameter[] parameters = {
					new SqlParameter("@billSign", SqlDbType.VarChar),
                    new SqlParameter("@billId", SqlDbType.Int,4),
                    new SqlParameter("@action", SqlDbType.Int,4),
                    new SqlParameter("@depId", SqlDbType.Int,4),
                    new SqlParameter("@operaId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = billSign;
            parameters[1].Value = billId;
            parameters[2].Value = action;
            parameters[3].Value = depId;
            parameters[4].Value = operaId;
            DataTable dt = DbHelperSQL.RunProcedure(procName, parameters, "dt").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "0")
                {
                    message = dt.Rows[0][1].ToString();
                }
            }
            return message;
        }
        /// <summary>
        /// 通过审批流判断单据动作是否合法 删除 
        /// </summary>
        /// <param name="billSign">单据识别号</param>
        /// <param name="billIdStr">单据流水号</param>
        /// <param name="operaId">操作人ID</param>
        /// <returns>错误信息</returns>
        public string CheckActionByProcess_Del(string billSign, string billIdStr, int operaId)
        {
            string message = "";
            string procName = "p_sys_CheckActionByProcess_Del";
            SqlParameter[] parameters = {
					new SqlParameter("@billSign", SqlDbType.VarChar),
                    new SqlParameter("@billIdStr", SqlDbType.VarChar),
                    new SqlParameter("@operaId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = billSign;
            parameters[1].Value = billIdStr;
            parameters[2].Value = operaId;
            DataTable dt = DbHelperSQL.RunProcedure(procName, parameters, "dt").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "0")
                {
                    message = dt.Rows[0][1].ToString();
                }
            }
            return message;
        }
        /// <summary>
        /// 设置单据审批流 新增 修改 
        /// </summary>
        /// <param name="billSign">单据识别号</param>
        /// <param name="billId">单据流水号</param>
        /// <param name="action">动作1 add\2 update</param>
        /// <param name="operaId">操作人ID</param>
        /// <returns></returns>
        public DataSet SetBillProcess_Edit(string billSign, int billId, int action, int operaId)
        {
            string procName = "p_sys_SetBillProcess_Edit";
            SqlParameter[] parameters = {
					new SqlParameter("@billSign", SqlDbType.VarChar),
                    new SqlParameter("@billId", SqlDbType.Int,4),
                    new SqlParameter("@action", SqlDbType.Int),
                    new SqlParameter("@operaId", SqlDbType.Int)
                                        };
            parameters[0].Value = billSign;
            parameters[1].Value = billId;
            parameters[2].Value = action;
            parameters[3].Value = operaId;
            return DbHelperSQL.RunProcedure(procName, parameters, "dt");
        }
        /// <summary>
        /// 设置单据审批流 删除 
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="billId">单据流水号</param>
        /// <returns></returns>
        public DataSet SetBillProcess_Del(string tableName, string billIdStr)
        {
            string procName = "p_sys_SetBillProcess_Del";
            SqlParameter[] parameters = {
					new SqlParameter("@tableName", SqlDbType.VarChar),
                    new SqlParameter("@billIdStr", SqlDbType.VarChar)
                                        };
            parameters[0].Value = tableName;
            parameters[1].Value = billIdStr;

            return DbHelperSQL.RunProcedure(procName, parameters, "dt");
        }
        /// <summary>
        /// 是否存在已提交的单据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="billId">单据流水号</param>
        /// <returns></returns>
        public DataSet ExistSubmit(string tableName, string billIdStr)
        {
            string procName = "p_sys_ExistSubmit";
            SqlParameter[] parameters = {
					new SqlParameter("@tableName", SqlDbType.VarChar),
                    new SqlParameter("@billIdStr", SqlDbType.VarChar)
                                        };
            parameters[0].Value = tableName;
            parameters[1].Value = billIdStr;

            return DbHelperSQL.RunProcedure(procName, parameters, "dt");
        }
        /// <summary>
        /// 检测是否可以提交 
        /// </summary>
        /// /// <param name="tableName">表名</param>
        /// <param name="billSign">单据识别号</param>
        /// /// <param name="billIdStr">单据部门 新增时传入，其他传0</param>
        /// <param name="billIdStr">单据流水号</param>
        /// <param name="operaId">操作人ID</param>
        /// <returns>错误信息</returns>
        public string CheckSubmit(string tableName,string billSign,int depId, string billIdStr, int operaId)
        {
            string message = "";
            string procName = "p_sys_CheckSubmit";
            SqlParameter[] parameters = {
                    new SqlParameter("@tableName", SqlDbType.VarChar),
					new SqlParameter("@billSign", SqlDbType.VarChar),
                    new SqlParameter("@depId", SqlDbType.Int,4),
                    new SqlParameter("@billIdStr", SqlDbType.VarChar),
                    new SqlParameter("@operaId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = tableName;
            parameters[1].Value = billSign;
            parameters[2].Value = depId;
            parameters[3].Value = billIdStr;
            parameters[4].Value = operaId;
            DataTable dt = DbHelperSQL.RunProcedure(procName, parameters, "dt").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "0")
                {
                    message = dt.Rows[0][1].ToString();
                }
            }
            return message;
        }
        /// <summary>
        /// 检测是否可以取消提交 
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="billIdStr">单据流水号</param>
        /// <param name="operaId">操作人ID</param>
        /// <returns>错误信息</returns>
        public string CheckCancelSubmit(string tableName, string billIdStr, int operaId)
        {
            string message = "";
            string procName = "p_sys_CheckCancelSubmit";
            SqlParameter[] parameters = {
					new SqlParameter("@tableName", SqlDbType.VarChar),
                    new SqlParameter("@billIdStr", SqlDbType.VarChar),
                    new SqlParameter("@operaId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = tableName;
            parameters[1].Value = billIdStr;
            parameters[2].Value = operaId;
            DataTable dt = DbHelperSQL.RunProcedure(procName, parameters, "dt").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "0")
                {
                    message = dt.Rows[0][1].ToString();
                }
            }
            return message;
        }
        /// <summary>
        /// 提交或取消提交 
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="billIdStr">单据流水号</param>
        /// <param name="action">1 提交 2取消提交</param>
        /// <param name="operaId">操作人</param>
        /// <returns></returns>
        public string SetBillProcess_Submit(string tableName, string billIdStr, int action, int operaId)
        {
            string message = "";
            string procName = "p_sys_SetBillProcess_Submit";
            SqlParameter[] parameters = {
					new SqlParameter("@tableName", SqlDbType.VarChar),
                    new SqlParameter("@billIdStr", SqlDbType.VarChar),
                    new SqlParameter("@action", SqlDbType.Int,4),
                    new SqlParameter("@operaId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = tableName;
            parameters[1].Value = billIdStr;
            parameters[2].Value = action;
            parameters[3].Value = operaId;
            DataTable dt = DbHelperSQL.RunProcedure(procName, parameters, "dt").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "0")
                {
                    message = dt.Rows[0][1].ToString();
                }
            }
            return message;
        }
        /// <summary>
        /// 设置单据审批流 审核 取消审核 单张单据 适合审批结束后有处理动作的业务

        /// </summary>
        /// <param name="billSign">单据识别号</param>
        /// <param name="billId">单据流水号</param>
        /// <param name="tableName">表名</param>
        /// <param name="action">动作1 audit\2 cancelaudit</param>
        /// <param name="operaId">操作人ID</param>
        /// <param name="flagAudit">审批状态 1同意2不同意</param>
        /// <param name="auditorOpinion">审批意见</param>
        /// <returns></returns>
        public DataSet SetBillProcess_Audit(string billSign, int billId,  int action, int operaId, int flagAudit, string auditorOpinion)
        {
            string procName = "p_sys_SetBillProcess_Audit";
            SqlParameter[] parameters = {
					new SqlParameter("@billSign", SqlDbType.VarChar),
                    new SqlParameter("@billId", SqlDbType.Int,4),
                    new SqlParameter("@action", SqlDbType.Int),
                    new SqlParameter("@operaId", SqlDbType.Int),
                    new SqlParameter("@flagAudit", SqlDbType.Int),
                    new SqlParameter("@auditorOpinion", SqlDbType.NVarChar)
                                        };
            parameters[0].Value = billSign;
            parameters[1].Value = billId;
            parameters[2].Value = action;
            parameters[3].Value = operaId;
            parameters[4].Value = flagAudit;
            parameters[5].Value = auditorOpinion;
            return DbHelperSQL.RunProcedure(procName, parameters, "dt");
        }

        /// <summary>
        /// 设置单据审批流 审核 取消审核 批量审核消审 适合审批结束后没有处理动作的业务
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="billId">单据流水号</param>
        /// <param name="action">动作1 audit\2 cancelaudit</param>
        /// <param name="operaId">操作人ID</param>
        /// <param name="flagAudit">审批状态 1同意2不同意</param>
        /// <param name="auditorOpinion">审批意见</param>
        /// <returns></returns>
        public DataSet SetBillProcess_Audit_Batch(string tableName, string billIdStr, int action, int operaId, int flagAudit, string auditorOpinion)
        {
            string procName = "p_sys_SetBillProcess_Audit_Batch";
            SqlParameter[] parameters = {
					new SqlParameter("@tableName", SqlDbType.VarChar),
                    new SqlParameter("@billIdStr", SqlDbType.VarChar),
                    new SqlParameter("@action", SqlDbType.Int),
                    new SqlParameter("@operaId", SqlDbType.Int),
                    new SqlParameter("@flagAudit", SqlDbType.Int),
                    new SqlParameter("@auditorOpinion", SqlDbType.NVarChar)
                                        };
            parameters[0].Value = tableName;
            parameters[1].Value = billIdStr;
            parameters[2].Value = action;
            parameters[3].Value = operaId;
            parameters[4].Value = flagAudit;
            parameters[5].Value = auditorOpinion;
            return DbHelperSQL.RunProcedure(procName, parameters, "dt");
        }
        
        /// <summary>
        /// 获得单据的审批流状态

        /// </summary>
        /// <param name="billSign"></param>
        /// <param name="billId"></param>
        /// <param name="depId"></param>
        /// <returns></returns>
        public DataSet GetBillProcess(string billSign, int billId)
        {
            string procName = "p_sys_GetBillProcess";
            SqlParameter[] parameters = {
					new SqlParameter("@billSign", SqlDbType.VarChar),
                    new SqlParameter("@billId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = billSign;
            parameters[1].Value = billId;
            return DbHelperSQL.RunProcedure(procName, parameters, "dt");
            
        }
        /// <summary>
        /// 获得单据的审批历史记录

        /// </summary>
        /// <param name="billSign"></param>
        /// <param name="billId"></param>
        /// <returns></returns>
        public DataSet GetBillProcessHistory(string billSign, int billId)
        {
            string procName = "p_sys_GetBillProcessHistory";
            SqlParameter[] parameters = {
					new SqlParameter("@billSign", SqlDbType.VarChar),
                    new SqlParameter("@billId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = billSign;
            parameters[1].Value = billId;
            return DbHelperSQL.RunProcedure(procName, parameters, "dt");
        }
        /// <summary>
        /// 获得单据状态

        /// </summary>
        /// <param name="billSign"></param>
        /// <param name="billId"></param>
        /// <returns></returns>
        public string GetBillState(string billSign, int billId)
        {
            string billStateMemo = "";
            string procName = "p_sys_GetBillState";
            SqlParameter[] parameters = {
					new SqlParameter("@billSign", SqlDbType.VarChar),
                    new SqlParameter("@billId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = billSign;
            parameters[1].Value = billId;
            DataTable dt= DbHelperSQL.RunProcedure(procName, parameters, "dt").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                billStateMemo = dt.Rows[0][0].ToString();
            }
            return billStateMemo;
        }
        /// <summary>
        /// 删除指定的sys_Process_Exec,更新对应表为草稿状态（类似取消提交，不记录历史）
        /// </summary>
        /// <param name="billSign">单据标识</param>
        /// <param name="billId">单据ID</param>
        /// <param name="tableName">表名</param>
        /// <param name="OperaId">操作人ID</param>
        /// <param name="OperaName">操作人</param>
        /// <returns></returns>
        public int DelBillProcess(string billSign, int billId,string tableName,int OperaId,string OperaName) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_Process_Exec where BillId=@billId and BillSign=@billSign and FlagDel=0;");
            SqlParameter[] parameters ={
                    new SqlParameter("@billId",SqlDbType.Int,4),
                    new SqlParameter("@billSign",SqlDbType.VarChar)
                                      };
            parameters[0].Value = billId;
            parameters[1].Value = billSign;
            int delNum = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (delNum >= 0)
            {
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("update "+tableName+" set AuditNextId=0,BillState=0,OperaId=@OperaId,OperaName=@OperaName,OperaTime=getdate() where ID=@billId");
                SqlParameter[] parameters2 ={
                    new SqlParameter("@OperaId",SqlDbType.Int,4),
                    new SqlParameter("@OperaName",SqlDbType.NVarChar),
                    new SqlParameter("@billId",SqlDbType.Int,4)
                                      };
                parameters2[0].Value = OperaId;
                parameters2[1].Value = OperaName;
                parameters2[2].Value = billId;
                int updateNum = DbHelperSQL.ExecuteSql(strSql2.ToString(), parameters2);
                if (updateNum >= 0)
                {
                    return updateNum;
                }
            }
            return delNum;
        }
        #endregion  扩展方法
    }
}

