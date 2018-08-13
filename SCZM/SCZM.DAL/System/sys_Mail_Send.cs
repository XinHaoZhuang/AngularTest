using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;//请先添加引用
namespace SCZM.DAL.System
{
    /// <summary>
    /// 数据访问类sys_Mail_Send。

    /// </summary>
    public partial class sys_Mail_Send
    {
        public sys_Mail_Send()
        { }
        #region  基本方法
        /// <summary>
        /// 是否存在该记录

        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sys_Mail_Send");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据,及其子表数据
        /// </summary>
        public int Add(SCZM.Model.System.sys_Mail_Send model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_Mail_Send(");
            strSql.Append("MailName,ReceivePerId,ReceivePerName,MailContent,Attachment,BillState,FlagDel,OperaId,OperaName,OperaTime)");
            strSql.Append(" values (");
            strSql.Append("@MailName,@ReceivePerId,@ReceivePerName,@MailContent,@Attachment,@BillState,@FlagDel,@OperaId,@OperaName,@OperaTime)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@MailName", SqlDbType.NVarChar,50),
					new SqlParameter("@ReceivePerId", SqlDbType.VarChar,1000),
					new SqlParameter("@ReceivePerName", SqlDbType.NVarChar,2000),
					new SqlParameter("@MailContent", SqlDbType.NText),
					new SqlParameter("@Attachment", SqlDbType.VarChar,100),
					new SqlParameter("@BillState", SqlDbType.Int,4),
					new SqlParameter("@FlagDel", SqlDbType.Bit,1),
					new SqlParameter("@OperaId", SqlDbType.Int,4),
					new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
					new SqlParameter("@OperaTime", SqlDbType.DateTime),
					new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.MailName;
            parameters[1].Value = model.ReceivePerId;
            parameters[2].Value = model.ReceivePerName;
            parameters[3].Value = model.MailContent;
            parameters[4].Value = model.Attachment;
            parameters[5].Value = model.BillState;
            parameters[6].Value = model.FlagDel;
            parameters[7].Value = model.OperaId;
            parameters[8].Value = model.OperaName;
            parameters[9].Value = model.OperaTime;
            parameters[10].Direction = ParameterDirection.Output;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql2;
            if (model.sys_Mail_Send_ReceivePersons != null)
            {
                foreach (SCZM.Model.System.sys_Mail_Send_ReceivePerson models in model.sys_Mail_Send_ReceivePersons)
                {
                    strSql2 = new StringBuilder();
                    strSql2.Append("insert into sys_Mail_Send_ReceivePerson(");
                    strSql2.Append("MailId,ReceivePerId,FlagDel)");
                    strSql2.Append(" values (");
                    strSql2.Append("@MailId,@ReceivePerId,@FlagDel)");
                    SqlParameter[] parameters2 = {
							new SqlParameter("@MailId", SqlDbType.Int,4),
							new SqlParameter("@ReceivePerId", SqlDbType.Int,4),
							new SqlParameter("@FlagDel", SqlDbType.Bit,1)};
                    parameters2[0].Direction = ParameterDirection.InputOutput;
                    parameters2[1].Value = models.ReceivePerId;
                    parameters2[2].Value = models.FlagDel;

                    cmd = new CommandInfo(strSql2.ToString(), parameters2);
                    sqllist.Add(cmd);
                }
            }
            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[10].Value;
        }
        /// <summary>
        /// 更新一条数据

        /// </summary>
        public int Update(SCZM.Model.System.sys_Mail_Send model)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Mail_Send set ");
            strSql.Append("MailName=@MailName,");
            strSql.Append("ReceivePerId=@ReceivePerId,");
            strSql.Append("ReceivePerName=@ReceivePerName,");
            strSql.Append("MailContent=@MailContent,");
            strSql.Append("Attachment=@Attachment,");
            strSql.Append("BillState=@BillState,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaId=@OperaId,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@MailName", SqlDbType.NVarChar,50),
					new SqlParameter("@ReceivePerId", SqlDbType.VarChar,1000),
					new SqlParameter("@ReceivePerName", SqlDbType.NVarChar,2000),
					new SqlParameter("@MailContent", SqlDbType.NText),
					new SqlParameter("@Attachment", SqlDbType.VarChar,100),
					new SqlParameter("@BillState", SqlDbType.Int,4),
					new SqlParameter("@FlagDel", SqlDbType.Bit,1),
					new SqlParameter("@OperaId", SqlDbType.Int,4),
					new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
					new SqlParameter("@OperaTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.MailName;
            parameters[1].Value = model.ReceivePerId;
            parameters[2].Value = model.ReceivePerName;
            parameters[3].Value = model.MailContent;
            parameters[4].Value = model.Attachment;
            parameters[5].Value = model.BillState;
            parameters[6].Value = model.FlagDel;
            parameters[7].Value = model.OperaId;
            parameters[8].Value = model.OperaName;
            parameters[9].Value = model.OperaTime;
            parameters[10].Value = model.ID;

            cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from  sys_Mail_Send_ReceivePerson where MailId=@MailId ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@MailId", SqlDbType.Int,4)};
            parameters2[0].Value = model.ID;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            StringBuilder strSql3;
            if (model.sys_Mail_Send_ReceivePersons != null)
            {
                foreach (SCZM.Model.System.sys_Mail_Send_ReceivePerson models in model.sys_Mail_Send_ReceivePersons)
                {
                    strSql3 = new StringBuilder();
                    strSql3.Append("insert into sys_Mail_Send_ReceivePerson(");
                    strSql3.Append("MailId,ReceivePerId,FlagDel)");
                    strSql3.Append(" values (");
                    strSql3.Append("@MailId,@ReceivePerId,@FlagDel)");
                    SqlParameter[] parameters3 = {
							new SqlParameter("@MailId", SqlDbType.Int,4),
							new SqlParameter("@ReceivePerId", SqlDbType.Int,4),
							new SqlParameter("@FlagDel", SqlDbType.Bit,1)};
                    parameters3[0].Value = model.ID;
                    parameters3[1].Value = models.ReceivePerId;
                    parameters3[2].Value = models.FlagDel;

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
            strSql2.Append("update sys_Mail_Send_ReceivePerson set FlagDel=1 ");
            strSql2.Append("where FlagDel=0 and MailId=@MailId ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@MailId", SqlDbType.Int,4)};
            parameters2[0].Value = ID;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Mail_Send set FlagDel=1 ");
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
            strSql.Append("update sys_Mail_Send_ReceivePerson set FlagDel=1 ");
            strSql.Append("where FlagDel=0 and MailId in(" + IDList + ")");
            sqllist.Add(strSql.ToString());

            strSql = new StringBuilder();
            strSql.Append("update sys_Mail_Send set FlagDel=1 ");
            strSql.Append("where FlagDel=0 and ID in(" + IDList + ")");
            sqllist.Add(strSql.ToString());
            int rows = DbHelperSQL.ExecuteSqlTran(sqllist);
            return rows;
        }


        /// <summary>
        /// 得到一个对象实体 主表
        /// </summary>
        public SCZM.Model.System.sys_Mail_Send GetModelMain(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,MailName,ReceivePerId,ReceivePerName,MailContent,Attachment,BillState,FlagDel,OperaId,OperaName,OperaTime from sys_Mail_Send ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.System.sys_Mail_Send model = new SCZM.Model.System.sys_Mail_Send();
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
        public SCZM.Model.System.sys_Mail_Send GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,MailName,ReceivePerId,ReceivePerName,MailContent,Attachment,BillState,FlagDel,OperaId,OperaName,OperaTime from sys_Mail_Send ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.System.sys_Mail_Send model = new SCZM.Model.System.sys_Mail_Send();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region  父表信息
                model = DataRowToModel(ds.Tables[0].Rows[0]);
                #endregion  父表信息end

                #region  子表信息
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("select ID,MailId,ReceivePerId,FlagDel from sys_Mail_Send_ReceivePerson ");
                strSql2.Append(" where FlagDel=0 and MailId=@MailId ");
                SqlParameter[] parameters2 = {
						new SqlParameter("@MailId", SqlDbType.Int,4)};
                parameters2[0].Value = ID;
                DataSet ds2 = DbHelperSQL.Query(strSql2.ToString(), parameters2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    #region  子表字段信息
                    int i = ds2.Tables[0].Rows.Count;
                    List<SCZM.Model.System.sys_Mail_Send_ReceivePerson> models = new List<SCZM.Model.System.sys_Mail_Send_ReceivePerson>();
                    SCZM.Model.System.sys_Mail_Send_ReceivePerson modelt;
                    for (int n = 0; n < i; n++)
                    {
                        modelt = new SCZM.Model.System.sys_Mail_Send_ReceivePerson();
                        if (ds2.Tables[0].Rows[n]["ID"].ToString() != null && ds2.Tables[0].Rows[n]["ID"].ToString() != "")
                        {
                            modelt.ID = int.Parse(ds2.Tables[0].Rows[n]["ID"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["MailId"].ToString() != null && ds2.Tables[0].Rows[n]["MailId"].ToString() != "")
                        {
                            modelt.MailId = int.Parse(ds2.Tables[0].Rows[n]["MailId"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["ReceivePerId"].ToString() != null && ds2.Tables[0].Rows[n]["ReceivePerId"].ToString() != "")
                        {
                            modelt.ReceivePerId = int.Parse(ds2.Tables[0].Rows[n]["ReceivePerId"].ToString());
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
                    model.sys_Mail_Send_ReceivePersons = models;
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
        public SCZM.Model.System.sys_Mail_Send DataRowToModel(DataRow row)
        {
            SCZM.Model.System.sys_Mail_Send model = new SCZM.Model.System.sys_Mail_Send();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["MailName"] != null)
                {
                    model.MailName = row["MailName"].ToString();
                }
                if (row["ReceivePerId"] != null)
                {
                    model.ReceivePerId = row["ReceivePerId"].ToString();
                }
                if (row["ReceivePerName"] != null)
                {
                    model.ReceivePerName = row["ReceivePerName"].ToString();
                }
                if (row["MailContent"] != null)
                {
                    model.MailContent = row["MailContent"].ToString();
                }
                if (row["Attachment"] != null)
                {
                    model.Attachment = row["Attachment"].ToString();
                }
                if (row["BillState"] != null && row["BillState"].ToString() != "")
                {
                    model.BillState = int.Parse(row["BillState"].ToString());
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


        /// <summary>
        /// 获得数据列表 通过ID
        /// </summary>
        public DataSet GetList(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.MailName,a.ReceivePerId,a.ReceivePerName,a.MailContent,a.Attachment,a.BillState,a.FlagDel,a.OperaId,a.OperaName,a.OperaTime ");
            strSql.Append("FROM sys_Mail_Send a ");
            strSql.Append("where ID=@ID ");
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
            strSql.Append("select a.ID,a.MailName,a.ReceivePerId,a.ReceivePerName,a.MailContent,a.Attachment,a.BillState,a.FlagDel,a.OperaId,a.OperaName,a.OperaTime ");
            strSql.Append("FROM sys_Mail_Send a ");
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
            strSql.Append("select count(1) FROM sys_Mail_Send ");
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
            strSql.Append(")AS Row, T.*  from sys_Mail_Send T ");
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
            parameters[0].Value = "sys_Mail_Send";
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
        /// 获得发件箱数据列表 通过SQL语句
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.MailName,a.ReceivePerId,a.ReceivePerName,a.BillState,a.OperaName,a.OperaTime ");
            strSql.Append("FROM sys_Mail_Send a ");
            strSql.Append("where a.FlagDel=0 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.OperaTime desc");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 发送邮件

        /// </summary>
        public int Send(int mailId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_Mail_Receive(PerId,MailId,FlagRead,FlagDel) select b.ReceivePerId,a.ID,0,0  ");
            strSql.Append("from sys_Mail_Send a left join sys_Mail_Send_ReceivePerson b on a.ID=b.MailId and b.FlagDel=0 ");
            strSql.Append("where a.FlagDel=0 and a.ID=" + mailId.ToString());

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }
        
        /// <summary>
        /// 获得收件箱数据列表

        /// </summary>
        public DataSet GetReceiveList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.MailId,a.FlagRead,b.MailName,b.ReceivePerId,b.ReceivePerName,b.OperaName,b.OperaTime  ");
            strSql.Append("from sys_Mail_Receive a ");
            strSql.Append("left join sys_Mail_Send b on a.MailId=b.ID ");
            strSql.Append("where a.FlagDel=0 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by b.OperaTime desc");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 批量删除收件箱数据

        /// </summary>
        public int DeleteReceiveList(string IDList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Mail_Receive set FlagDel=1,DelTime=getdate() ");
            strSql.Append("where FlagDel=0 and ID in(" + IDList + ")");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }
        /// <summary>
        /// 批量更新收件箱已读标记

        /// </summary>
        public int UpdateFlagRead(string IDList, int flagRead)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Mail_Receive set FlagRead=" + flagRead.ToString());
            strSql.Append(" where FlagDel=0 and ID in(" + IDList + ")");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }
        /// <summary>
        /// 获得收件箱未读条数

        /// </summary>
        /// <param name="perId"></param>
        /// <returns></returns>
        public int GetReceiveNoRead(int perId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select COUNT(1) from sys_Mail_Receive where FlagDel=0 and FlagRead=0 and PerId=" + perId.ToString());
            return (int)DbHelperSQL.GetSingle(strSql.ToString());
        }
        #endregion  扩展方法
    }
}

