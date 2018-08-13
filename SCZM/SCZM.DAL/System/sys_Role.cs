using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SCZM.DBUtility;//Please add references
namespace SCZM.DAL.System
{
    /// <summary>
    /// 数据访问类:sys_Role
    /// </summary>
    public partial class sys_Role
    {
        public sys_Role()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录

        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sys_Role");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据,及其子表数据
        /// </summary>
        public int Add(SCZM.Model.System.sys_Role model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_Role(");
            strSql.Append("RoleName,Memo,FlagDel,OperaName,OperaTime)");
            strSql.Append(" values (");
            strSql.Append("@RoleName,@Memo,@FlagDel,@OperaName,@OperaTime)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleName", SqlDbType.NVarChar,50),
					new SqlParameter("@Memo", SqlDbType.NVarChar,100),
					new SqlParameter("@FlagDel", SqlDbType.Bit,1),
					new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
					new SqlParameter("@OperaTime", SqlDbType.DateTime),
					new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.RoleName;
            parameters[1].Value = model.Memo;
            parameters[2].Value = model.FlagDel;
            parameters[3].Value = model.OperaName;
            parameters[4].Value = model.OperaTime;
            parameters[5].Direction = ParameterDirection.Output;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql2;
            foreach (SCZM.Model.System.sys_RolePower models in model.sys_RolePowers)
            {
                strSql2 = new StringBuilder();
                strSql2.Append("insert into sys_RolePower(");
                strSql2.Append("RoleId,PowerId)");
                strSql2.Append(" values (");
                strSql2.Append("@RoleId,@PowerId)");
                SqlParameter[] parameters2 = {
						new SqlParameter("@RoleId", SqlDbType.Int,4),
						new SqlParameter("@PowerId", SqlDbType.Int,4)};
                parameters2[0].Direction = ParameterDirection.InputOutput;
                parameters2[1].Value = models.PowerId;

                cmd = new CommandInfo(strSql2.ToString(), parameters2);
                sqllist.Add(cmd);
            }
            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[5].Value;
        }
        
        /// <summary>
        /// 更新一条数据

        /// </summary>
        public int Update(SCZM.Model.System.sys_Role model)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Role set ");
            strSql.Append("RoleName=@RoleName,");
            strSql.Append("Memo=@Memo,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleName", SqlDbType.NVarChar,50),
					new SqlParameter("@Memo", SqlDbType.NVarChar,100),
					new SqlParameter("@FlagDel", SqlDbType.Bit,1),
					new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
					new SqlParameter("@OperaTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.RoleName;
            parameters[1].Value = model.Memo;
            parameters[2].Value = model.FlagDel;
            parameters[3].Value = model.OperaName;
            parameters[4].Value = model.OperaTime;
            parameters[5].Value = model.ID;

            cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from sys_RolePower where RoleId=@RoleId ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@RoleId", SqlDbType.Int,4)};
            parameters2[0].Value = model.ID;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            StringBuilder strSql3;
            foreach (SCZM.Model.System.sys_RolePower models in model.sys_RolePowers)
            {
                strSql3 = new StringBuilder();
                strSql3.Append("insert into sys_RolePower(");
                strSql3.Append("RoleId,PowerId)");
                strSql3.Append(" values (");
                strSql3.Append("@RoleId,@PowerId)");
                SqlParameter[] parameters3 = {
						new SqlParameter("@RoleId", SqlDbType.Int,4),
						new SqlParameter("@PowerId", SqlDbType.Int,4)};
                parameters3[0].Value = model.ID;
                parameters3[1].Value = models.PowerId;

                cmd = new CommandInfo(strSql3.ToString(), parameters3);
                sqllist.Add(cmd);
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
            strSql2.Append("delete sys_RolePower ");
            strSql2.Append("where RoleId=@RoleId ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@RoleId", SqlDbType.Int,4)};
            parameters2[0].Value = ID;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Role set FlagDel=1 ");
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
        /// 删除一条数据

        /// </summary>
        public int DeleteList(string IDList)
        {
            List<string> sqllist = new List<string>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_RolePower ");
            strSql.Append("where RoleId in(" + IDList + ")");
            sqllist.Add(strSql.ToString());

            strSql = new StringBuilder();
            strSql.Append("update sys_Role set FlagDel=1 ");
            strSql.Append("where FlagDel=0 and ID in(" + IDList + ")");
            sqllist.Add(strSql.ToString());
            int rows = DbHelperSQL.ExecuteSqlTran(sqllist);
            return rows;
        }

        /// <summary>
        /// 得到一个对象实体 主表
        /// </summary>
        public SCZM.Model.System.sys_Role GetModelMain(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,RoleName,Memo,FlagDel,OperaName,OperaTime from sys_Role ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.System.sys_Role model = new SCZM.Model.System.sys_Role();
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
        public SCZM.Model.System.sys_Role GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,RoleName,Memo,FlagDel,OperaName,OperaTime from sys_Role ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.System.sys_Role model = new SCZM.Model.System.sys_Role();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region  父表信息
                model = DataRowToModel(ds.Tables[0].Rows[0]);
                #endregion  父表信息end

                #region  子表信息
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("select ID,RoleId,PowerId from sys_RolePower ");
                strSql2.Append(" where RoleId=@RoleId ");
                SqlParameter[] parameters2 = {
						new SqlParameter("@RoleId", SqlDbType.Int,4)};
                parameters2[0].Value = ID;
                DataSet ds2 = DbHelperSQL.Query(strSql2.ToString(), parameters2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    #region  子表字段信息
                    int i = ds2.Tables[0].Rows.Count;
                    List<SCZM.Model.System.sys_RolePower> models = new List<SCZM.Model.System.sys_RolePower>();
                    SCZM.Model.System.sys_RolePower modelt;
                    for (int n = 0; n < i; n++)
                    {
                        modelt = new SCZM.Model.System.sys_RolePower();
                        if (ds2.Tables[0].Rows[n]["ID"].ToString() != null && ds2.Tables[0].Rows[n]["ID"].ToString() != "")
                        {
                            modelt.ID = int.Parse(ds2.Tables[0].Rows[n]["ID"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["RoleId"].ToString() != null && ds2.Tables[0].Rows[n]["RoleId"].ToString() != "")
                        {
                            modelt.RoleId = int.Parse(ds2.Tables[0].Rows[n]["RoleId"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["PowerId"].ToString() != null && ds2.Tables[0].Rows[n]["PowerId"].ToString() != "")
                        {
                            modelt.PowerId = int.Parse(ds2.Tables[0].Rows[n]["PowerId"].ToString());
                        }
                        models.Add(modelt);
                    }
                    model.sys_RolePowers = models;
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
        /// 得到一个对象实体

        /// </summary>
        public SCZM.Model.System.sys_Role DataRowToModel(DataRow row)
        {
            SCZM.Model.System.sys_Role model = new SCZM.Model.System.sys_Role();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["RoleName"] != null)
                {
                    model.RoleName = row["RoleName"].ToString();
                }
                if (row["Memo"] != null)
                {
                    model.Memo = row["Memo"].ToString();
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
            strSql.Append("select a.ID,a.RoleName,a.Memo,a.FlagDel,a.OperaName,a.OperaTime ");
            strSql.Append("FROM sys_Role a ");
            strSql.Append(" where a.FlagDel=0");
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
            strSql.Append("select a.ID,a.RoleName,a.Memo,a.FlagDel,a.OperaName,a.OperaTime ");
            strSql.Append("FROM sys_Role a");
            strSql.Append(" where a.FlagDel=0 and ID=@ID ");
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
            strSql.Append("select a.ID,a.RoleName,a.Memo,a.FlagDel,a.OperaName,a.OperaTime ");
            strSql.Append("FROM sys_Role a");
            strSql.Append(" where a.FlagDel=0");
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
            strSql.Append("select count(1) FROM sys_Role ");
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
            strSql.Append(")AS Row, T.*  from sys_Role T ");
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
            parameters[0].Value = "sys_Role";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/


        #endregion  Method
        #region  ExtensionMethod
        /// <summary>
        /// 查询角色名是否存在

        /// </summary>
        public bool ExistsRoleName(string roleName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sys_Role");
            strSql.Append(" where FlagDel=0 and RoleName=@RoleName");
            SqlParameter[] parameters = {
                    new SqlParameter("@RoleName", SqlDbType.NVarChar,100)
                                        };
            parameters[0].Value = roleName;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 得到存在人员的角色列表

        /// </summary>
        /// <param name="IDlist"></param>
        /// <returns></returns>
        public DataSet GetExistsPerList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM sys_Role where FlagDel=0 and ID in (select distinct RoleId from sys_PersonRole where FlagDel=0 )and ID in(" + IDlist + ") ");
            strSql.Append(" order by ID  ");
            
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表 通过SQL语句
        /// </summary>
        public DataSet GetListAll(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.RoleName,a.Memo,a.OperaName,a.OperaTime,(case when b.RoleId is null then 0 else 1 end) as FlagUse ");
            strSql.Append("from sys_Role a ");
            strSql.Append("left join( ");
            strSql.Append("select distinct RoleId from sys_PersonRole where FlagDel=0 ");
            strSql.Append(") b on a.ID=b.RoleId ");
            strSql.Append(" where a.FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 得到一个角色的所有权限列表（包括不具有的权限）
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public DataSet GetRolePowerAllList(int roleId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.PowerId,a.MenuId,a.PowerName,(case when b.PowerId is null then 0 else 1 end) as FlagSet from sys_MenuPower a ");
            strSql.Append("left join sys_RolePower b on a.PowerId=b.PowerId and b.RoleId=@RoleId  where a.FlagDel=0 ");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleId", SqlDbType.Int,4)};
            parameters[0].Value = roleId;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更改人员表角色名称
        /// </summary>
        /// <param name="oldRoleName"></param>
        /// <param name="newRoleName"></param>
        /// <returns></returns>
        public bool UpdatePerRoleName(string oldRoleName, string newRoleName)
        {
            try
            {
                string strSql = "update sys_Person set RoleName=substring(replace(','+RoleName+',',',' + @oldRoleName + ',',',' + @newRoleName + ','),2,LEN(replace(','+RoleName+',',',' + @oldRoleName + ',',',' + @newRoleName + ','))-2) ";
                strSql += "where ','+RoleName+',' like '%,'+@oldRoleName+',%'";
                SqlParameter[] parameters = {
                    new SqlParameter("@oldRoleName", SqlDbType.VarChar,50),
					new SqlParameter("@newRoleName", SqlDbType.VarChar,50)};
                parameters[0].Value = oldRoleName;
                parameters[1].Value = newRoleName;
                DbHelperSQL.ExecuteSql(strSql,parameters);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 得到角色权限列表 全部
        /// </summary>
        /// <returns></returns>
        public DataSet GetRolePowerList(int roleId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct PowerId from sys_RolePower where RoleId=" + roleId);
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion
    }
}

