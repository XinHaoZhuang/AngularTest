using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using SCZM.DBUtility;//Please add references
namespace SCZM.DAL.System
{
    /// <summary>
    /// 数据访问类:sys_Person
    /// </summary>
    public partial class sys_Person
    {
        public sys_Person()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录

        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sys_Person ");
            strSql.Append("where FlagDel=0 and ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 查询用户名是否存在

        /// </summary>
        public bool ExistsAccount(string account)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sys_Person");
            strSql.Append(" where FlagDel=0 and Account=@Account ");
            SqlParameter[] parameters = {
					new SqlParameter("@Account", SqlDbType.NVarChar,100)};
            parameters[0].Value = account;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 查询用户名、密码是否正确

        /// </summary>
        public bool Exists(string account, string password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sys_Person ");
            strSql.Append("where FlagDel=0 and Account=@Account and Password=@Password");
            SqlParameter[] parameters = {
					new SqlParameter("@Account", SqlDbType.VarChar,20),
                    new SqlParameter("@Password", SqlDbType.VarChar,50),
			};
            parameters[0].Value = account;
            parameters[1].Value = password;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 增加一条数据

        /// </summary>
        public int Add(SCZM.Model.System.sys_Person model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_Person(");
            strSql.Append("PerName,DepId,PostId,PerTel,PerEmail,DDNo,WXNo,Account,Password,Salt,IsAdmin,RoleId,RoleName,CtrlPersonType,CtrlDepId,CtrlPerId,FlagDel,OperaName,OperaTime)");
            strSql.Append(" values (");
            strSql.Append("@PerName,@DepId,@PostId,@PerTel,@PerEmail,@DDNo,@WXNo,@Account,@Password,@Salt,@IsAdmin,@RoleId,@RoleName,@CtrlPersonType,@CtrlDepId,@CtrlPerId,@FlagDel,@OperaName,@OperaTime)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
				new SqlParameter("@PerName", SqlDbType.NVarChar,20),
				new SqlParameter("@DepId", SqlDbType.Int,4),
				new SqlParameter("@PostId", SqlDbType.Int,4),
				new SqlParameter("@PerTel", SqlDbType.VarChar,50),
				new SqlParameter("@PerEmail", SqlDbType.VarChar,50),
				new SqlParameter("@DDNo", SqlDbType.VarChar,50),
				new SqlParameter("@WXNo", SqlDbType.VarChar,50),
				new SqlParameter("@Account", SqlDbType.NVarChar,20),
				new SqlParameter("@Password", SqlDbType.VarChar,50),
				new SqlParameter("@Salt", SqlDbType.VarChar,50),
				new SqlParameter("@IsAdmin", SqlDbType.Bit,1),
				new SqlParameter("@RoleId", SqlDbType.VarChar,50),
				new SqlParameter("@RoleName", SqlDbType.NVarChar,50),
				new SqlParameter("@CtrlPersonType", SqlDbType.Int,4),
				new SqlParameter("@CtrlDepId", SqlDbType.Int,4),
				new SqlParameter("@CtrlPerId", SqlDbType.VarChar,1000),
				new SqlParameter("@FlagDel", SqlDbType.Bit,1),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
				new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.PerName;
            parameters[1].Value = model.DepId;
            parameters[2].Value = model.PostId;
            parameters[3].Value = model.PerTel;
            parameters[4].Value = model.PerEmail;
            parameters[5].Value = model.DDNo;
            parameters[6].Value = model.WXNo;
            parameters[7].Value = model.Account;
            parameters[8].Value = model.Password;
            parameters[9].Value = model.Salt;
            parameters[10].Value = model.IsAdmin;
            parameters[11].Value = model.RoleId;
            parameters[12].Value = model.RoleName;
            parameters[13].Value = model.CtrlPersonType;
            parameters[14].Value = model.CtrlDepId;
            parameters[15].Value = model.CtrlPerId;
            parameters[16].Value = model.FlagDel;
            parameters[17].Value = model.OperaName;
            parameters[18].Value = model.OperaTime;
            parameters[19].Direction = ParameterDirection.Output;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("insert into sys_PersonDep(PerId,DepId,OperaName) ");
            strSql3.Append("values(@PerId,@DepId,@OperaName)");
            SqlParameter[] parameters3 = {
                    new SqlParameter("@PerId", SqlDbType.Int,4),
                    new SqlParameter("@DepId", SqlDbType.Int,4),
                    new SqlParameter("@OperaName", SqlDbType.NVarChar,20)
                                         };
            parameters3[0].Direction = ParameterDirection.InputOutput;
            parameters3[1].Value = model.DepId;
            parameters3[2].Value = model.OperaName;
            cmd = new CommandInfo(strSql3.ToString(), parameters3);
            sqllist.Add(cmd);

            if (model.RoleId != "")
            {
                StringBuilder strSql5;

                string[] sArray = model.RoleId.Split(',');
                foreach (string roleId in sArray)
                {
                    strSql5 = new StringBuilder();
                    strSql5.Append("insert into sys_PersonRole(PerId,RoleId,OperaName) ");
                    strSql5.Append("values(@PerId,@RoleId,@OperaName)");
                    SqlParameter[] parameters5 = {
                    new SqlParameter("@PerId", SqlDbType.Int,4),
                    new SqlParameter("@RoleId", SqlDbType.Int,4),
                    new SqlParameter("@OperaName", SqlDbType.NVarChar,20)
                                         };
                    parameters5[0].Direction = ParameterDirection.InputOutput;
                    parameters5[1].Value = int.Parse(roleId);
                    parameters5[2].Value = model.OperaName;
                    cmd = new CommandInfo(strSql5.ToString(), parameters5);
                    sqllist.Add(cmd);
                }
            }
            //人员控制部门
            if (model.CtrlPerId != "")
            {
                StringBuilder strSql7;

                string[] sArray = model.CtrlPerId.Split(',');
                foreach (string ctrlPerId in sArray)
                {
                    strSql7 = new StringBuilder();
                    strSql7.Append("insert into sys_PersonCtrl(PerId,CtrlPerId) ");
                    strSql7.Append("values(@PerId,@CtrlPerId)");
                    SqlParameter[] parameters7 = {
                    new SqlParameter("@PerId", SqlDbType.Int,4),
                    new SqlParameter("@CtrlPerId", SqlDbType.Int,4)
                                         };
                    parameters7[0].Direction = ParameterDirection.InputOutput;
                    parameters7[1].Value = int.Parse(ctrlPerId);
                    cmd = new CommandInfo(strSql7.ToString(), parameters7);
                    sqllist.Add(cmd);
                }
            }
            
            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[19].Value;     
        }
        /// <summary>
        /// 更新一条数据

        /// </summary>
        public int Update(SCZM.Model.System.sys_Person model,int flagDep,int flagRole,int flagCtrlPer)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Person set ");
            strSql.Append("PerName=@PerName,");
            strSql.Append("DepId=@DepId,");
            strSql.Append("PostId=@PostId,");
            strSql.Append("PerTel=@PerTel,");
            strSql.Append("PerEmail=@PerEmail,");
            strSql.Append("DDNo=@DDNo,");
            strSql.Append("WXNo=@WXNo,");
            strSql.Append("Account=@Account,");
            strSql.Append("Password=@Password,");
            strSql.Append("Salt=@Salt,");
            strSql.Append("IsAdmin=@IsAdmin,");
            strSql.Append("RoleId=@RoleId,");
            strSql.Append("RoleName=@RoleName,");
            strSql.Append("CtrlPersonType=@CtrlPersonType,");
            strSql.Append("CtrlDepId=@CtrlDepId,");
            strSql.Append("CtrlPerId=@CtrlPerId,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@PerName", SqlDbType.NVarChar,20),
				new SqlParameter("@DepId", SqlDbType.Int,4),
				new SqlParameter("@PostId", SqlDbType.Int,4),
				new SqlParameter("@PerTel", SqlDbType.VarChar,50),
				new SqlParameter("@PerEmail", SqlDbType.VarChar,50),
				new SqlParameter("@DDNo", SqlDbType.VarChar,50),
				new SqlParameter("@WXNo", SqlDbType.VarChar,50),
				new SqlParameter("@Account", SqlDbType.NVarChar,20),
				new SqlParameter("@Password", SqlDbType.VarChar,50),
				new SqlParameter("@Salt", SqlDbType.VarChar,50),
				new SqlParameter("@IsAdmin", SqlDbType.Bit,1),
				new SqlParameter("@RoleId", SqlDbType.VarChar,50),
				new SqlParameter("@RoleName", SqlDbType.NVarChar,50),
				new SqlParameter("@CtrlPersonType", SqlDbType.Int,4),
				new SqlParameter("@CtrlDepId", SqlDbType.Int,4),
				new SqlParameter("@CtrlPerId", SqlDbType.VarChar,1000),
				new SqlParameter("@FlagDel", SqlDbType.Bit,1),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.PerName;
            parameters[1].Value = model.DepId;
            parameters[2].Value = model.PostId;
            parameters[3].Value = model.PerTel;
            parameters[4].Value = model.PerEmail;
            parameters[5].Value = model.DDNo;
            parameters[6].Value = model.WXNo;
            parameters[7].Value = model.Account;
            parameters[8].Value = model.Password;
            parameters[9].Value = model.Salt;
            parameters[10].Value = model.IsAdmin;
            parameters[11].Value = model.RoleId;
            parameters[12].Value = model.RoleName;
            parameters[13].Value = model.CtrlPersonType;
            parameters[14].Value = model.CtrlDepId;
            parameters[15].Value = model.CtrlPerId;
            parameters[16].Value = model.FlagDel;
            parameters[17].Value = model.OperaName;
            parameters[18].Value = model.OperaTime;
            parameters[19].Value = model.ID;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            if (flagDep == 1)
            {
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("update sys_PersonDep set FlagDel=1 where FlagDel=0 and PerId=@PerId");
                SqlParameter[] parameters2 = {
                    new SqlParameter("@PerId", SqlDbType.Int,4)};
                parameters2[0].Value = model.ID;
                cmd = new CommandInfo(strSql2.ToString(), parameters2);
                sqllist.Add(cmd);

                StringBuilder strSql3 = new StringBuilder();
                strSql3.Append("insert into sys_PersonDep(PerId,DepId,OperaName) ");
                strSql3.Append("values(@PerId,@DepId,@OperaName)");
                SqlParameter[] parameters3 = {
                    new SqlParameter("@PerId", SqlDbType.Int,4),
                    new SqlParameter("@DepId", SqlDbType.Int,4),
                    new SqlParameter("@OperaName", SqlDbType.NVarChar,20)
                                         };
                parameters3[0].Value = model.ID;
                parameters3[1].Value = model.DepId;
                parameters3[2].Value = model.OperaName;
                cmd = new CommandInfo(strSql3.ToString(), parameters3);
                sqllist.Add(cmd);
            }
            if (flagRole == 1)
            {
                StringBuilder strSql4 = new StringBuilder();
                strSql4.Append("update sys_PersonRole set FlagDel=1 where FlagDel=0 and PerId=@PerId");
                SqlParameter[] parameters4 = {
                    new SqlParameter("@PerId", SqlDbType.Int,4)};
                parameters4[0].Value = model.ID;
                cmd = new CommandInfo(strSql4.ToString(), parameters4);
                sqllist.Add(cmd);

                if (model.RoleId != "")
                {
                    StringBuilder strSql5;

                    string[] sArray = model.RoleId.Split(',');
                    foreach (string roleId in sArray)
                    {
                        strSql5 = new StringBuilder();
                        strSql5.Append("insert into sys_PersonRole(PerId,RoleId,OperaName) ");
                        strSql5.Append("values(@PerId,@RoleId,@OperaName)");
                        SqlParameter[] parameters5 = {
                    new SqlParameter("@PerId", SqlDbType.Int,4),
                    new SqlParameter("@RoleId", SqlDbType.Int,4),
                    new SqlParameter("@OperaName", SqlDbType.NVarChar,20)
                                         };
                        parameters5[0].Value = model.ID;
                        parameters5[1].Value = int.Parse(roleId);
                        parameters5[2].Value = model.OperaName;
                        cmd = new CommandInfo(strSql5.ToString(), parameters5);
                        sqllist.Add(cmd);
                    }
                }
            }
            //人员控制   
            if (flagCtrlPer == 1)
            {
                StringBuilder strSql6 = new StringBuilder();
                strSql6.Append("delete from sys_PersonCtrl where PerId=@PerId");
                SqlParameter[] parameters6 = {
                    new SqlParameter("@PerId", SqlDbType.Int,4)};
                parameters6[0].Value = model.ID;
                cmd = new CommandInfo(strSql6.ToString(), parameters6);
                sqllist.Add(cmd);

                if (model.CtrlPerId != "")
                {
                    StringBuilder strSql7;

                    string[] sArray = model.CtrlPerId.Split(',');
                    foreach (string ctrlPerId in sArray)
                    {
                        strSql7 = new StringBuilder();
                        strSql7.Append("insert into sys_PersonCtrl(PerId,CtrlPerId) ");
                        strSql7.Append("values(@PerId,@CtrlPerId)");
                        SqlParameter[] parameters7 = {
                    new SqlParameter("@PerId", SqlDbType.Int,4),
                    new SqlParameter("@CtrlPerId", SqlDbType.Int,4)
                                         };
                        parameters7[0].Value = model.ID;
                        parameters7[1].Value = int.Parse(ctrlPerId);
                        cmd = new CommandInfo(strSql7.ToString(), parameters7);
                        sqllist.Add(cmd);
                    }
                }
            }
            int rows = DbHelperSQL.ExecuteSqlTran(sqllist);
            return rows;
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public int DeleteList(string IDlist)
        {
            string strSql="";
            List<string> sqllist=new List<string>();
            strSql = "update sys_PersonDep set FlagDel=1 ";
            strSql+=" where PerId in (" + IDlist + ")";
            sqllist.Add(strSql);

            strSql = "update sys_PersonRole set FlagDel=1 ";
            strSql += " where PerId in (" + IDlist + ")";
            sqllist.Add(strSql);

            strSql = "update sys_PersonCtrl set FlagDel=1 ";
            strSql += " where PerId in (" + IDlist + ")";
            sqllist.Add(strSql);

            strSql="update sys_Person set FlagDel=1 ";
            strSql+=" where ID in (" + IDlist + ")";
            sqllist.Add(strSql);

            int rows = DbHelperSQL.ExecuteSqlTran(sqllist);
            return rows;
        }


        /// <summary>
        /// 得到一个对象实体

        /// </summary>
        public SCZM.Model.System.sys_Person GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,PerName,DepId,PostId,PerTel,PerEmail,DDNo,WXNo,Account,Password,Salt,IsAdmin,RoleId,RoleName,CtrlPersonType,CtrlDepId,CtrlPerId,FlagDel,OperaName,OperaTime from sys_Person ");
            strSql.Append(" where FlagDel=0 and ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            SCZM.Model.System.sys_Person model = new SCZM.Model.System.sys_Person();
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
        /// 得到一个对象实体

        /// </summary>
        public SCZM.Model.System.sys_Person DataRowToModel(DataRow row)
        {
            SCZM.Model.System.sys_Person model = new SCZM.Model.System.sys_Person();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["PerName"] != null)
                {
                    model.PerName = row["PerName"].ToString();
                }
                if (row["DepId"] != null && row["DepId"].ToString() != "")
                {
                    model.DepId = int.Parse(row["DepId"].ToString());
                }
                if (row["PostId"] != null && row["PostId"].ToString() != "")
                {
                    model.PostId = int.Parse(row["PostId"].ToString());
                }
                if (row["PerTel"] != null)
                {
                    model.PerTel = row["PerTel"].ToString();
                }
                if (row["PerEmail"] != null)
                {
                    model.PerEmail = row["PerEmail"].ToString();
                }
                if (row["DDNo"] != null)
                {
                    model.DDNo = row["DDNo"].ToString();
                }
                if (row["WXNo"] != null)
                {
                    model.WXNo = row["WXNo"].ToString();
                }
                if (row["Account"] != null)
                {
                    model.Account = row["Account"].ToString();
                }
                if (row["Password"] != null)
                {
                    model.Password = row["Password"].ToString();
                }
                if (row["Salt"] != null)
                {
                    model.Salt = row["Salt"].ToString();
                }
                if (row["IsAdmin"] != null && row["IsAdmin"].ToString() != "")
                {
                    if ((row["IsAdmin"].ToString() == "1") || (row["IsAdmin"].ToString().ToLower() == "true"))
                    {
                        model.IsAdmin = true;
                    }
                    else
                    {
                        model.IsAdmin = false;
                    }
                }
                if (row["RoleId"] != null)
                {
                    model.RoleId = row["RoleId"].ToString();
                }
                if (row["RoleName"] != null)
                {
                    model.RoleName = row["RoleName"].ToString();
                }
                if (row["CtrlPersonType"] != null && row["CtrlPersonType"].ToString() != "")
                {
                    model.CtrlPersonType = int.Parse(row["CtrlPersonType"].ToString());
                }
                if (row["CtrlDepId"] != null && row["CtrlDepId"].ToString() != "")
                {
                    model.CtrlDepId = int.Parse(row["CtrlDepId"].ToString());
                }
                if (row["CtrlPerId"] != null)
                {
                    model.CtrlPerId = row["CtrlPerId"].ToString();
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
            strSql.Append("select a.ID,a.OperaName,a.OperaTime,a.PerName,a.DepId,a.PostId,a.PerTel,a.PerEmail,a.Account,a.IsAdmin, (case when a.IsAdmin=0  then '否' else '是' end) as Lock,a.RoleId,a.RoleName,b.DepName,c.PostName ");
            strSql.Append("FROM sys_Person a left join sys_Department b on b.FlagDel=0 and a.DepId=b.Id left join sys_Post c on c.FlagDel=0 and a.PostId=c.ID ");
            strSql.Append("where a.FlagDel=0 ");
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
            strSql.Append("select a.ID,a.OperaName,a.OperaTime,a.PerName,a.DepId,a.PostId,a.PerTel,a.PerEmail,a.DDNo,a.WXNo,a.Account,a.IsAdmin, (case when a.IsAdmin=0  then '否' else '是' end) as Lock,a.RoleId,a.RoleName,a.CtrlPersonType,a.CtrlDepId,a.CtrlPerId,b.DepName ");
            strSql.Append("FROM sys_Person a left join sys_Department b on a.DepId=b.Id ");
            strSql.Append("where a.FlagDel=0 and a.ID=@ID ");
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
            strSql.Append("select a.ID,a.OperaName,a.OperaTime,a.PerName,a.DepId,a.PostId,a.PerTel,a.PerEmail,a.Account,a.IsAdmin, (case when a.IsAdmin=0  then '否' else '是' end) as Lock,a.RoleId,a.RoleName,b.DepName,c.PostName ");
            strSql.Append("FROM sys_Person a  left join sys_Department b on a.DepId=b.Id  left join sys_Post c on a.PostId=c.ID ");
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
            strSql.Append("select count(1) FROM sys_Person where FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
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
            strSql.Append(")AS Row, T.*  from sys_Person T where T.FlagDel=0");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" and " + strWhere);
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
            parameters[0].Value = "sys_Person";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        /// <summary>
        /// 根据用户名取得Salt
        /// </summary>
        public string GetSalt(string Account)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 Salt from sys_Person");
            strSql.Append(" where FlagDel=0 and Account=@Account");
            SqlParameter[] parameters = {
                    new SqlParameter("@Account", SqlDbType.NVarChar,100)};
            parameters[0].Value = Account;
            string salt = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString(), parameters));

            //string str = "select top 1 Salt from sys_Person where FlagDel=0 and Account='" + Account + "'";
            //string salt = Convert.ToString(DbHelperSQL.GetSingle(str));
            if (string.IsNullOrEmpty(salt))
            {
                return "";
            }
            return salt;
        }
        /// <summary>
        /// 根据账号密码返回一个实体sys_LoginUser
        /// </summary>
        public Model.System.sys_LoginUser GetModel(string account, string password)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select top 1 a.*,b.DepName,b.DepTypeId from sys_Person a inner join sys_Department b on b.FlagDel=0 and a.DepId=b.ID ");
            strSql.Append("where a.FlagDel=0 and a.Account=@account and a.Password=@password ");
            SqlParameter[] parameters = {
                    new SqlParameter("@account", SqlDbType.NVarChar,100),
                    new SqlParameter("@password", SqlDbType.NVarChar,100)};
            parameters[0].Value = account;
            parameters[1].Value = password;

            DataTable dt = DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
            if (dt.Rows.Count>0)
            {
                Model.System.sys_LoginUser model = new Model.System.sys_LoginUser();
                DataRow row = dt.Rows[0];
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["PerName"] != null)
                {
                    model.PerName = row["PerName"].ToString();
                }
                if (row["Account"] != null)
                {
                    model.Account = row["Account"].ToString();
                }
                if (row["DepId"] != null && row["DepId"].ToString() != "")
                {
                    model.DepId = int.Parse(row["DepId"].ToString());
                }
                if (row["DepTypeId"] != null && row["DepTypeId"].ToString() != "")
                {
                    model.DepTypeId = int.Parse(row["DepTypeId"].ToString());
                }
                if (row["PostId"] != null && row["PostId"].ToString() != "")
                {
                    model.PostId = int.Parse(row["PostId"].ToString());
                }
                if (row["IsAdmin"] != null && row["IsAdmin"].ToString() != "")
                {
                    if ((row["IsAdmin"].ToString() == "1") || (row["IsAdmin"].ToString().ToLower() == "true"))
                    {
                        model.IsAdmin = true;
                    }
                    else
                    {
                        model.IsAdmin = false;
                    }
                }
                if (row["RoleName"] != null)
                {
                    model.RoleName = row["RoleName"].ToString();
                }
                if (row["RoleName"] != null)
                {
                    model.RoleName = row["RoleName"].ToString();
                }
                return model;
            }
            return null;
        }

        /// <summary>
        /// 获得登录用户的菜单数据列表

        /// </summary>
        public DataSet GetUserMenu(int perId)
        {
            string strSql = "p_sys_GetUserMenu";
            SqlParameter[] parameters = {
					new SqlParameter("@PerId", SqlDbType.Int,4)};
            parameters[0].Value = perId;
            return DbHelperSQL.RunProcedure(strSql, parameters, "dt");
        }
        /// <summary>
        /// 判断用户的按钮权限

        /// </summary>
        public DataSet CheckUserBtnPower(int menuId, string url, int perId, string elementName)
        {
            string strSql = "p_sys_CheckUserBtnPower";
            SqlParameter[] parameters = {
                    new SqlParameter("@MenuId", SqlDbType.Int,4),
                    new SqlParameter("@Url", SqlDbType.VarChar, 200),
					new SqlParameter("@PerId", SqlDbType.Int,4),
                    new SqlParameter("@ElementName", SqlDbType.VarChar, 20)                    
                                        };
            parameters[0].Value = menuId;
            parameters[1].Value = url;
            parameters[2].Value = perId;
            parameters[3].Value = elementName;
            return DbHelperSQL.RunProcedure(strSql, parameters, "dt");
        }
        /// <summary>
        /// 获得用户的页面按钮

        /// </summary>
        public DataSet GetUserBtn(int menuId, string url, int perId)
        {
            string strSql = "p_sys_GetUserBtn";
            SqlParameter[] parameters = {
                    new SqlParameter("@MenuId", SqlDbType.Int,4),
                    new SqlParameter("@Url", SqlDbType.VarChar, 200),
					new SqlParameter("@PerId", SqlDbType.Int,4)                  
                                        };
            parameters[0].Value = menuId;
            parameters[1].Value = url;
            parameters[2].Value = perId;
            return DbHelperSQL.RunProcedure(strSql, parameters, "dt");
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        public bool UpdatePwd(string account, string newPwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Person set ");
            strSql.Append("Password=@Password ");
            strSql.Append(" where FlagDel=0 and Account=@Account");
            SqlParameter[] parameters = {
					new SqlParameter("@Password", SqlDbType.VarChar,50),
					new SqlParameter("@Account", SqlDbType.VarChar,20)};
            parameters[0].Value = newPwd;
            parameters[1].Value = account;
            
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到个人角色列表 全部
        /// </summary>
        /// <returns></returns>
        public DataSet GetPersonRoleList(int perId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct RoleId from sys_PersonRole where FlagDel=0 and PerId=" + perId);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 或得所有的人员列表 包括上级部门 为人员树使用
        /// </summary>
        /// <returns></returns>
        public DataSet GetDepPersonList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select 'B'+cast(a.ID as varchar(10)) as ID,a.DepName as Name,'B'+cast(a.SupId  as varchar(10)) as SupId,'B'+cast(a.SortId as varchar(10)) as SortId from sys_Department a ");
            strSql.Append("where exists ( ");
            strSql.Append("select 1 from (select distinct SupList from sys_Department where ID in(select distinct DepId from sys_Person where FlagDel=0)) b ");
            strSql.Append("where ','+b.SupList like '%,'+cast(a.ID as varchar(10))+',%') ");
            strSql.Append("union ");
            strSql.Append("select 'B'+cast(a.ID as varchar(10)) as ID,a.DepName as Name,'B'+cast(a.SupId  as varchar(10)) as SupId,'B'+cast(a.SortId as varchar(10)) as SortId from sys_Department a ");
            strSql.Append("where ID in(select distinct DepId from sys_Person where FlagDel=0) ");
            strSql.Append("union all ");
            strSql.Append("select cast(ID as varchar(10)),PerName as Name,'B'+cast(DepId as varchar(10)) as SupId,'A'+cast(ID as varchar(10)) as SortId from sys_Person where FlagDel=0 ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表 通过Where条件
        /// </summary>
        public DataSet GetComboList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID as PerId,a.PerName,b.DepName,a.DepId ");
            strSql.Append("FROM sys_Person a left join sys_Department b on a.DepId=b.ID ");
            strSql.Append("where a.FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by b.DepName,a.PerName");
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  ExtensionMethod
    }
}

