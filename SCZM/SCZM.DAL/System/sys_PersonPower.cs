using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using SCZM.DBUtility;//Please add references
namespace SCZM.DAL.System
{
    /// <summary>
    /// 数据访问类:sys_PersonPower
    /// </summary>
    public partial class sys_PersonPower
    {
        public sys_PersonPower()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录

        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sys_PersonPower");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据

        /// </summary>
        public int Add(SCZM.Model.System.sys_PersonPower model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_PersonPower(");
            strSql.Append("PerId,PowerId,FlagDel,OperaName,OperaTime)");
            strSql.Append(" values (");
            strSql.Append("@PerId,@PowerId,@FlagDel,@OperaName,@OperaTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@PerId", SqlDbType.Int,4),
					new SqlParameter("@PowerId", SqlDbType.Int,4),
					new SqlParameter("@FlagDel", SqlDbType.Bit,1),
					new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
					new SqlParameter("@OperaTime", SqlDbType.DateTime)};
            parameters[0].Value = model.PerId;
            parameters[1].Value = model.PowerId;
            parameters[2].Value = model.FlagDel;
            parameters[3].Value = model.OperaName;
            parameters[4].Value = model.OperaTime;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        /// 更新一条数据

        /// </summary>
        public int Update(SCZM.Model.System.sys_PersonPower model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_PersonPower set ");
            strSql.Append("PerId=@PerId,");
            strSql.Append("PowerId=@PowerId,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@PerId", SqlDbType.Int,4),
					new SqlParameter("@PowerId", SqlDbType.Int,4),
					new SqlParameter("@FlagDel", SqlDbType.Bit,1),
					new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
					new SqlParameter("@OperaTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.PerId;
            parameters[1].Value = model.PowerId;
            parameters[2].Value = model.FlagDel;
            parameters[3].Value = model.OperaName;
            parameters[4].Value = model.OperaTime;
            parameters[5].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 删除一条数据

        /// </summary>
        public int Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_PersonPower set FlagDel=1 ");
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
        public int DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_PersonPower set FlagDel=1 ");
            strSql.Append(" where FlagDel=0 and PerId in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());

            return rows;
            
        }


        /// <summary>
        /// 得到一个对象实体

        /// </summary>
        public SCZM.Model.System.sys_PersonPower GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,PerId,PowerId,FlagDel,OperaName,OperaTime from sys_PersonPower ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            SCZM.Model.System.sys_PersonPower model = new SCZM.Model.System.sys_PersonPower();
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
        public SCZM.Model.System.sys_PersonPower DataRowToModel(DataRow row)
        {
            SCZM.Model.System.sys_PersonPower model = new SCZM.Model.System.sys_PersonPower();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["PerId"] != null && row["PerId"].ToString() != "")
                {
                    model.PerId = int.Parse(row["PerId"].ToString());
                }
                if (row["PowerId"] != null && row["PowerId"].ToString() != "")
                {
                    model.PowerId = int.Parse(row["PowerId"].ToString());
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
            strSql.Append("select ID,PerId,PowerId,FlagDel,OperaName,OperaTime ");
            strSql.Append("FROM sys_PersonPower");
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
            strSql.Append("select ID,PerId,PowerId,FlagDel,OperaName,OperaTime ");
            strSql.Append("FROM sys_PersonPower");
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
            strSql.Append("select ID,PerId,PowerId,FlagDel,OperaName,OperaTime ");
            strSql.Append("FROM sys_PersonPower");
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
            strSql.Append("select count(1) FROM sys_PersonPower ");
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
            strSql.Append(")AS Row, T.*  from sys_PersonPower T ");
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
            parameters[0].Value = "sys_PersonPower";
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
        /// 获得人员权限设置数据列表
        /// </summary>
        public DataSet GetPerSettingList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.PerName,a.Account,a.RoleName,(case when b.PerId is null then '未设置' else '已设置' end) as FlagSet, b.OperaName,b.OperaTime from sys_Person a ");
            strSql.Append("left join (select distinct PerId,OperaName,OperaTime from sys_PersonPower where FlagDel=0) b on a.ID=b.PerId ");
            strSql.Append("where a.FlagDel=0 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 得到一个人员的所有权限列表（包括不具有的权限）

        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public DataSet GetPerPowerAllList(int perId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.PowerId,a.MenuId,a.PowerName,(case when b.PowerId is not null then 1 else 0 end) as FlagRole,(case when b.PowerId is not null or c.PowerId is not null then 1 else 0 end) as FlagSet ");
            strSql.Append("from sys_MenuPower a  ");
            strSql.Append("left join (select distinct PowerId from sys_RolePower where RoleId in(select RoleId from sys_PersonRole where FlagDel=0 and PerId=" + perId.ToString() + "))b on a.PowerId=b.PowerId ");
            strSql.Append("left join sys_PersonPower c on c.FlagDel=0 and c.PerId=" + perId.ToString() + " and a.PowerId=c.PowerId ");
            strSql.Append("where a.FlagDel=0 ");

            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 更新一批数据

        /// </summary>
        public int Update(List<SCZM.Model.System.sys_PersonPower> powerList)
        {
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("update sys_PersonPower set FlagDel=1 where FlagDel=0 and PerId=@PerId");
            SqlParameter[] parameters2 = {
					new SqlParameter("@PerId", SqlDbType.Int,4)};
            parameters2[0].Value = powerList[0].PerId;
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            StringBuilder strSql;
            foreach (SCZM.Model.System.sys_PersonPower model in powerList)
            {
                strSql = new StringBuilder();
                strSql.Append("insert into sys_PersonPower(");
                strSql.Append("PerId,PowerId,FlagDel,OperaName,OperaTime)");
                strSql.Append(" values (");
                strSql.Append("@PerId,@PowerId,@FlagDel,@OperaName,@OperaTime)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@PerId", SqlDbType.Int,4),
					new SqlParameter("@PowerId", SqlDbType.Int,4),
					new SqlParameter("@FlagDel", SqlDbType.Bit,1),
					new SqlParameter("@OperaName", SqlDbType.VarChar,20),
					new SqlParameter("@OperaTime", SqlDbType.DateTime)};
                parameters[0].Value = model.PerId;
                parameters[1].Value = model.PowerId;
                parameters[2].Value = model.FlagDel;
                parameters[3].Value = model.OperaName;
                parameters[4].Value = model.OperaTime;
                cmd = new CommandInfo(strSql.ToString(), parameters);
                sqllist.Add(cmd);
            }

            int rows = DbHelperSQL.ExecuteSqlTran(sqllist);
            return rows;
        }
        /// <summary>
        /// 得到个人权限列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetPersonPowerList(int perId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct PowerId from sys_PersonPower where FlagDel=0 and PerId=" + perId);
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  ExtensionMethod
    }
}

