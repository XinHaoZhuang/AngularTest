﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;//请先添加引用
namespace SCZM.DAL.System
{
    /// <summary>
    /// 数据访问类sys_Process。

    /// </summary>
    public partial class sys_Process
    {
        public sys_Process()
        { }
        #region  基本方法
        /// <summary>
        /// 是否存在该记录

        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sys_Process");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据,及其子表数据
        /// </summary>
        public int Add(SCZM.Model.System.sys_Process model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_Process(");
            strSql.Append("ProcessName,Memo,FlagUse,FlagDel,OperaName,OperaTime)");
            strSql.Append(" values (");
            strSql.Append("@ProcessName,@Memo,@FlagUse,@FlagDel,@OperaName,@OperaTime)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ProcessName", SqlDbType.NVarChar,50),
					new SqlParameter("@Memo", SqlDbType.NVarChar,100),
					new SqlParameter("@FlagUse", SqlDbType.Bit,1),
					new SqlParameter("@FlagDel", SqlDbType.Bit,1),
					new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
					new SqlParameter("@OperaTime", SqlDbType.DateTime),
					new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.ProcessName;
            parameters[1].Value = model.Memo;
            parameters[2].Value = model.FlagUse;
            parameters[3].Value = model.FlagDel;
            parameters[4].Value = model.OperaName;
            parameters[5].Value = model.OperaTime;
            parameters[6].Direction = ParameterDirection.Output;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql2;
            if (model.sys_ProcessDetails != null)
            {
                foreach (SCZM.Model.System.sys_ProcessDetail models in model.sys_ProcessDetails)
                {
                    strSql2 = new StringBuilder();
                    strSql2.Append("insert into sys_ProcessDetail(");
                    strSql2.Append("ProcessId,OrderId,PostId,FlagDel)");
                    strSql2.Append(" values (");
                    strSql2.Append("@ProcessId,@OrderId,@PostId,@FlagDel)");
                    SqlParameter[] parameters2 = {
							new SqlParameter("@ProcessId", SqlDbType.Int,4),
							new SqlParameter("@OrderId", SqlDbType.Int,4),
							new SqlParameter("@PostId", SqlDbType.Int,4),
							new SqlParameter("@FlagDel", SqlDbType.Bit,1)};
                    parameters2[0].Direction = ParameterDirection.InputOutput;
                    parameters2[1].Value = models.OrderId;
                    parameters2[2].Value = models.PostId;
                    parameters2[3].Value = models.FlagDel;

                    cmd = new CommandInfo(strSql2.ToString(), parameters2);
                    sqllist.Add(cmd);
                }
            }
            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[6].Value;
        }
        /// <summary>
        /// 更新一条数据

        /// </summary>
        public int Update(SCZM.Model.System.sys_Process model)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Process set ");
            strSql.Append("ProcessName=@ProcessName,");
            strSql.Append("Memo=@Memo,");
            strSql.Append("FlagUse=@FlagUse,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProcessName", SqlDbType.NVarChar,50),
					new SqlParameter("@Memo", SqlDbType.NVarChar,100),
					new SqlParameter("@FlagUse", SqlDbType.Bit,1),
					new SqlParameter("@FlagDel", SqlDbType.Bit,1),
					new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
					new SqlParameter("@OperaTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.ProcessName;
            parameters[1].Value = model.Memo;
            parameters[2].Value = model.FlagUse;
            parameters[3].Value = model.FlagDel;
            parameters[4].Value = model.OperaName;
            parameters[5].Value = model.OperaTime;
            parameters[6].Value = model.ID;

            cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("update sys_ProcessDetail set FlagDel=1 where FlagDel=0 and ProcessId=@ProcessId ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@ProcessId", SqlDbType.Int,4)};
            parameters2[0].Value = model.ID;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            StringBuilder strSql3;
            if (model.sys_ProcessDetails != null)
            {
                foreach (SCZM.Model.System.sys_ProcessDetail models in model.sys_ProcessDetails)
                {
                    strSql3 = new StringBuilder();
                    strSql3.Append("insert into sys_ProcessDetail(");
                    strSql3.Append("ProcessId,OrderId,PostId,FlagDel)");
                    strSql3.Append(" values (");
                    strSql3.Append("@ProcessId,@OrderId,@PostId,@FlagDel)");
                    SqlParameter[] parameters3 = {
							new SqlParameter("@ProcessId", SqlDbType.Int,4),
							new SqlParameter("@OrderId", SqlDbType.Int,4),
							new SqlParameter("@PostId", SqlDbType.Int,4),
							new SqlParameter("@FlagDel", SqlDbType.Bit,1)};
                    parameters3[0].Value = model.ID;
                    parameters3[1].Value = models.OrderId;
                    parameters3[2].Value = models.PostId;
                    parameters3[3].Value = models.FlagDel;

                    cmd = new CommandInfo(strSql3.ToString(), parameters3);
                    sqllist.Add(cmd);
                }
            }
            int rows = DbHelperSQL.ExecuteSqlTran(sqllist);
            return rows;
        }

        /// <summary>
        /// 删除一条数据，及子表所有相关数据

        /// </summary>
        public int Delete(int ID)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo();
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("update sys_ProcessDetail set FlagDel=1 ");
            strSql2.Append("where FlagDel=0 and ProcessId=@ProcessId ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@ProcessId", SqlDbType.Int,4)};
            parameters2[0].Value = ID;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Process set FlagDel=1 ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            int rows = DbHelperSQL.ExecuteSqlTran(sqllist);
            return rows;
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public int DeleteList(string IDList)
        {
            List<string> sqllist = new List<string>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_ProcessDetail set FlagDel=1 ");
            strSql.Append("where FlagDel=0 and ProcessId in(" + IDList + ")");
            sqllist.Add(strSql.ToString());

            strSql = new StringBuilder();
            strSql.Append("update sys_Process set FlagDel=1 ");
            strSql.Append("where FlagDel=0 and ID in(" + IDList + ")");
            sqllist.Add(strSql.ToString());
            int rows = DbHelperSQL.ExecuteSqlTran(sqllist);
            return rows;
        }


        /// <summary>
        /// 得到一个对象实体 主表
        /// </summary>
        public SCZM.Model.System.sys_Process GetModelMain(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ProcessName,Memo,FlagUse,FlagDel,OperaName,OperaTime from sys_Process ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.System.sys_Process model = new SCZM.Model.System.sys_Process();
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
        /// 得到一个对象实体

        /// </summary>
        public SCZM.Model.System.sys_Process GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ProcessName,Memo,FlagUse,FlagDel,OperaName,OperaTime from sys_Process ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.System.sys_Process model = new SCZM.Model.System.sys_Process();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region  父表信息
                model = DataRowToModel(ds.Tables[0].Rows[0]);
                #endregion  父表信息end

                #region  子表信息
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("select ID,ProcessId,OrderId,PostId,FlagDel from sys_ProcessDetail ");
                strSql2.Append(" where FlagDel=0 and ProcessId=@ProcessId ");
                SqlParameter[] parameters2 = {
						new SqlParameter("@ProcessId", SqlDbType.Int,4)};
                parameters2[0].Value = ID;
                DataSet ds2 = DbHelperSQL.Query(strSql2.ToString(), parameters2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    #region  子表字段信息
                    int i = ds2.Tables[0].Rows.Count;
                    List<SCZM.Model.System.sys_ProcessDetail> models = new List<SCZM.Model.System.sys_ProcessDetail>();
                    SCZM.Model.System.sys_ProcessDetail modelt;
                    for (int n = 0; n < i; n++)
                    {
                        modelt = new SCZM.Model.System.sys_ProcessDetail();
                        if (ds2.Tables[0].Rows[n]["ID"].ToString() != null && ds2.Tables[0].Rows[n]["ID"].ToString() != "")
                        {
                            modelt.ID = int.Parse(ds2.Tables[0].Rows[n]["ID"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["ProcessId"].ToString() != null && ds2.Tables[0].Rows[n]["ProcessId"].ToString() != "")
                        {
                            modelt.ProcessId = int.Parse(ds2.Tables[0].Rows[n]["ProcessId"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["OrderId"].ToString() != null && ds2.Tables[0].Rows[n]["OrderId"].ToString() != "")
                        {
                            modelt.OrderId = int.Parse(ds2.Tables[0].Rows[n]["OrderId"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["PostId"].ToString() != null && ds2.Tables[0].Rows[n]["PostId"].ToString() != "")
                        {
                            modelt.PostId = int.Parse(ds2.Tables[0].Rows[n]["PostId"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["FlagDel"].ToString() != null && ds2.Tables[0].Rows[n]["FlagDel"].ToString() != "")
                        {
                            if ((ds2.Tables[0].Rows[n]["FlagDel"].ToString() == "1") || (ds2.Tables[0].Rows[n]["FlagDel"].ToString().ToLower() == "true"))
                            {
                                modelt.FlagDel = true;
                            }
                            else
                            {
                                modelt.FlagDel = false;
                            }
                        }
                        models.Add(modelt);
                    }
                    model.sys_ProcessDetails = models;
                    #endregion  子表字段信息end
                }
                #endregion  子表信息end

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
        public SCZM.Model.System.sys_Process DataRowToModel(DataRow row)
        {
            SCZM.Model.System.sys_Process model = new SCZM.Model.System.sys_Process();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["ProcessName"] != null)
                {
                    model.ProcessName = row["ProcessName"].ToString();
                }
                if (row["Memo"] != null)
                {
                    model.Memo = row["Memo"].ToString();
                }
                if (row["FlagUse"] != null && row["FlagUse"].ToString() != "")
                {
                    if ((row["FlagUse"].ToString() == "1") || (row["FlagUse"].ToString().ToLower() == "true"))
                    {
                        model.FlagUse = true;
                    }
                    else
                    {
                        model.FlagUse = false;
                    }
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

        /// <summary>
        /// 获得数据列表 通过SQL语句
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.ProcessName,a.Memo,a.FlagUse,a.FlagDel,a.OperaName,a.OperaTime ");
            strSql.Append("FROM sys_Process a ");
            strSql.Append("where a.FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" order by a.ProcessName");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表 通过ID
        /// </summary>
        public DataSet GetList(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.ProcessName,a.Memo,a.FlagUse,a.OperaName,a.OperaTime,b.BillNameNameStr ");
            strSql.Append("FROM sys_Process a ");
            strSql.Append("left join (select STUFF((");
            strSql.Append("select '，'+(case when a.SupId>0 then b.BillName+'-'+a.BillName else a.BillName end) from sys_Process_BillSet a left join sys_Process_BillSet b on a.SupId=b.ID where a.FlagDel=0 and a.ProcessId=" + ID.ToString() + " for xml path('')),1,1,'') as BillNameNameStr ) b on 1=1 ");
            strSql.Append("where a.FlagDel=0 and ID=@ID ");
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
            strSql.Append("select a.ID,a.ProcessName,a.Memo,a.FlagUse,a.FlagDel,a.OperaName,a.OperaTime ");
            strSql.Append("FROM sys_Process a ");
            strSql.Append("where a.FlagDel=0");
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
            strSql.Append("select count(1) FROM sys_Process ");
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
            strSql.Append(")AS Row, T.*  from sys_Process T ");
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
            parameters[0].Value = "sys_Process";
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
        /// 获得审批流明细列表 通过ID
        /// </summary>
        public DataSet GetDetailList(int processId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.OrderId,a.PostId,b.PostName ");
            strSql.Append("FROM sys_ProcessDetail a left join sys_Post b on b.FlagDel=0 and a.PostId=b.ID ");
            strSql.Append("where a.FlagDel=0 and ProcessId=@ProcessId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProcessId", SqlDbType.Int,4)};
            parameters[0].Value = processId;
            strSql.Append("order by a.OrderId ");
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获得可用的审批流数据列表 通过SQL语句
        /// </summary>
        public DataSet GetUseList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.ProcessName,a.Memo ");
            strSql.Append("FROM sys_Process a ");
            strSql.Append("where a.FlagDel=0 and a.FlagUse=1 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" order by a.ProcessName");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 是否已使用

        /// </summary>
        public bool Used(string IDList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sys_Process_BillSet");
            strSql.Append(" where FlagDel=0 and ProcessId in(" + IDList + ") ");

            return DbHelperSQL.Exists(strSql.ToString());
        }
        #endregion  扩展方法
    }
}

