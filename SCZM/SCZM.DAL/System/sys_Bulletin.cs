using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;//请先添加引用
namespace SCZM.DAL.System
{
    /// <summary>
    /// 数据访问类sys_Bulletin。

    /// </summary>
    public partial class sys_Bulletin
    {
        public sys_Bulletin()
        { }
        #region  基本方法
        /// <summary>
        /// 是否存在该记录

        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sys_Bulletin");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据,及其子表数据
        /// </summary>
        public int Add(SCZM.Model.System.sys_Bulletin model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_Bulletin(");
            strSql.Append("BulletinName,ReceiveDepId,ReceiveDepName,BulletinContent,Attachment,FlagTop,BillState,FlagDel,OperaId,OperaName,OperaTime)");
            strSql.Append(" values (");
            strSql.Append("@BulletinName,@ReceiveDepId,@ReceiveDepName,@BulletinContent,@Attachment,@FlagTop,@BillState,@FlagDel,@OperaId,@OperaName,@OperaTime)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@BulletinName", SqlDbType.NVarChar,50),
					new SqlParameter("@ReceiveDepId", SqlDbType.VarChar,1000),
					new SqlParameter("@ReceiveDepName", SqlDbType.NVarChar,2000),
					new SqlParameter("@BulletinContent", SqlDbType.NText),
					new SqlParameter("@Attachment", SqlDbType.VarChar,100),
					new SqlParameter("@FlagTop", SqlDbType.Int,4),
					new SqlParameter("@BillState", SqlDbType.Int,4),
					new SqlParameter("@FlagDel", SqlDbType.Bit,1),
					new SqlParameter("@OperaId", SqlDbType.Int,4),
					new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
					new SqlParameter("@OperaTime", SqlDbType.DateTime),
					new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.BulletinName;
            parameters[1].Value = model.ReceiveDepId;
            parameters[2].Value = model.ReceiveDepName;
            parameters[3].Value = model.BulletinContent;
            parameters[4].Value = model.Attachment;
            parameters[5].Value = model.FlagTop;
            parameters[6].Value = model.BillState;
            parameters[7].Value = model.FlagDel;
            parameters[8].Value = model.OperaId;
            parameters[9].Value = model.OperaName;
            parameters[10].Value = model.OperaTime;
            parameters[11].Direction = ParameterDirection.Output;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql2;
            if (model.sys_BulletinReceiveDeps != null)
            {
                foreach (SCZM.Model.System.sys_BulletinReceiveDep models in model.sys_BulletinReceiveDeps)
                {
                    strSql2 = new StringBuilder();
                    strSql2.Append("insert into sys_BulletinReceiveDep(");
                    strSql2.Append("BulletinId,ReceiveDepId,FlagDel)");
                    strSql2.Append(" values (");
                    strSql2.Append("@BulletinId,@ReceiveDepId,@FlagDel)");
                    SqlParameter[] parameters2 = {
							new SqlParameter("@BulletinId", SqlDbType.Int,4),
							new SqlParameter("@ReceiveDepId", SqlDbType.Int,4),
							new SqlParameter("@FlagDel", SqlDbType.Bit,1)};
                    parameters2[0].Direction = ParameterDirection.InputOutput;
                    parameters2[1].Value = models.ReceiveDepId;
                    parameters2[2].Value = models.FlagDel;

                    cmd = new CommandInfo(strSql2.ToString(), parameters2);
                    sqllist.Add(cmd);
                }
            }
            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[11].Value;
        }
        /// <summary>
        /// 更新一条数据

        /// </summary>
        public int Update(SCZM.Model.System.sys_Bulletin model)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Bulletin set ");
            strSql.Append("BulletinName=@BulletinName,");
            strSql.Append("ReceiveDepId=@ReceiveDepId,");
            strSql.Append("ReceiveDepName=@ReceiveDepName,");
            strSql.Append("BulletinContent=@BulletinContent,");
            strSql.Append("Attachment=@Attachment,");
            strSql.Append("FlagTop=@FlagTop,");
            strSql.Append("BillState=@BillState,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaId=@OperaId,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BulletinName", SqlDbType.NVarChar,50),
					new SqlParameter("@ReceiveDepId", SqlDbType.VarChar,1000),
					new SqlParameter("@ReceiveDepName", SqlDbType.NVarChar,2000),
					new SqlParameter("@BulletinContent", SqlDbType.NText),
					new SqlParameter("@Attachment", SqlDbType.VarChar,100),
					new SqlParameter("@FlagTop", SqlDbType.Int,4),
					new SqlParameter("@BillState", SqlDbType.Int,4),
					new SqlParameter("@FlagDel", SqlDbType.Bit,1),
					new SqlParameter("@OperaId", SqlDbType.Int,4),
					new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
					new SqlParameter("@OperaTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.BulletinName;
            parameters[1].Value = model.ReceiveDepId;
            parameters[2].Value = model.ReceiveDepName;
            parameters[3].Value = model.BulletinContent;
            parameters[4].Value = model.Attachment;
            parameters[5].Value = model.FlagTop;
            parameters[6].Value = model.BillState;
            parameters[7].Value = model.FlagDel;
            parameters[8].Value = model.OperaId;
            parameters[9].Value = model.OperaName;
            parameters[10].Value = model.OperaTime;
            parameters[11].Value = model.ID;

            cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from  sys_BulletinReceiveDep where BulletinId=@BulletinId ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@BulletinId", SqlDbType.Int,4)};
            parameters2[0].Value = model.ID;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            StringBuilder strSql3;
            if (model.sys_BulletinReceiveDeps != null)
            {
                foreach (SCZM.Model.System.sys_BulletinReceiveDep models in model.sys_BulletinReceiveDeps)
                {
                    strSql3 = new StringBuilder();
                    strSql3.Append("insert into sys_BulletinReceiveDep(");
                    strSql3.Append("BulletinId,ReceiveDepId,FlagDel)");
                    strSql3.Append(" values (");
                    strSql3.Append("@BulletinId,@ReceiveDepId,@FlagDel)");
                    SqlParameter[] parameters3 = {
							new SqlParameter("@BulletinId", SqlDbType.Int,4),
							new SqlParameter("@ReceiveDepId", SqlDbType.Int,4),
							new SqlParameter("@FlagDel", SqlDbType.Bit,1)};
                    parameters3[0].Value = model.ID;
                    parameters3[1].Value = models.ReceiveDepId;
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
            strSql2.Append("update sys_BulletinReceiveDep set FlagDel=1 ");
            strSql2.Append("where FlagDel=0 and BulletinId=@BulletinId ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@BulletinId", SqlDbType.Int,4)};
            parameters2[0].Value = ID;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Bulletin set FlagDel=1 ");
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
            strSql.Append("update sys_BulletinReceiveDep set FlagDel=1 ");
            strSql.Append("where FlagDel=0 and BulletinId in(" + IDList + ")");
            sqllist.Add(strSql.ToString());

            strSql = new StringBuilder();
            strSql.Append("update sys_Bulletin set FlagDel=1 ");
            strSql.Append("where FlagDel=0 and ID in(" + IDList + ")");
            sqllist.Add(strSql.ToString());
            int rows = DbHelperSQL.ExecuteSqlTran(sqllist);
            return rows;
        }


        /// <summary>
        /// 得到一个对象实体 主表
        /// </summary>
        public SCZM.Model.System.sys_Bulletin GetModelMain(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,BulletinName,ReceiveDepId,ReceiveDepName,BulletinContent,Attachment,FlagTop,BillState,FlagDel,OperaId,OperaName,OperaTime from sys_Bulletin ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.System.sys_Bulletin model = new SCZM.Model.System.sys_Bulletin();
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
        public SCZM.Model.System.sys_Bulletin GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,BulletinName,ReceiveDepId,ReceiveDepName,BulletinContent,Attachment,FlagTop,BillState,FlagDel,OperaId,OperaName,OperaTime from sys_Bulletin ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.System.sys_Bulletin model = new SCZM.Model.System.sys_Bulletin();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region  父表信息
                model = DataRowToModel(ds.Tables[0].Rows[0]);
                #endregion  父表信息end

                #region  子表信息
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("select ID,BulletinId,ReceiveDepId,FlagDel from sys_BulletinReceiveDep ");
                strSql2.Append(" where FlagDel=0 and BulletinId=@BulletinId ");
                SqlParameter[] parameters2 = {
						new SqlParameter("@BulletinId", SqlDbType.Int,4)};
                parameters2[0].Value = ID;
                DataSet ds2 = DbHelperSQL.Query(strSql2.ToString(), parameters2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    #region  子表字段信息
                    int i = ds2.Tables[0].Rows.Count;
                    List<SCZM.Model.System.sys_BulletinReceiveDep> models = new List<SCZM.Model.System.sys_BulletinReceiveDep>();
                    SCZM.Model.System.sys_BulletinReceiveDep modelt;
                    for (int n = 0; n < i; n++)
                    {
                        modelt = new SCZM.Model.System.sys_BulletinReceiveDep();
                        if (ds2.Tables[0].Rows[n]["ID"].ToString() != null && ds2.Tables[0].Rows[n]["ID"].ToString() != "")
                        {
                            modelt.ID = int.Parse(ds2.Tables[0].Rows[n]["ID"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["BulletinId"].ToString() != null && ds2.Tables[0].Rows[n]["BulletinId"].ToString() != "")
                        {
                            modelt.BulletinId = int.Parse(ds2.Tables[0].Rows[n]["BulletinId"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["ReceiveDepId"].ToString() != null && ds2.Tables[0].Rows[n]["ReceiveDepId"].ToString() != "")
                        {
                            modelt.ReceiveDepId = int.Parse(ds2.Tables[0].Rows[n]["ReceiveDepId"].ToString());
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
                    model.sys_BulletinReceiveDeps = models;
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
        public SCZM.Model.System.sys_Bulletin DataRowToModel(DataRow row)
        {
            SCZM.Model.System.sys_Bulletin model = new SCZM.Model.System.sys_Bulletin();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["BulletinName"] != null)
                {
                    model.BulletinName = row["BulletinName"].ToString();
                }
                if (row["ReceiveDepId"] != null)
                {
                    model.ReceiveDepId = row["ReceiveDepId"].ToString();
                }
                if (row["ReceiveDepName"] != null)
                {
                    model.ReceiveDepName = row["ReceiveDepName"].ToString();
                }
                if (row["BulletinContent"] != null)
                {
                    model.BulletinContent = row["BulletinContent"].ToString();
                }
                if (row["Attachment"] != null)
                {
                    model.Attachment = row["Attachment"].ToString();
                }
                if (row["FlagTop"] != null && row["FlagTop"].ToString() != "")
                {
                    model.FlagTop = int.Parse(row["FlagTop"].ToString());
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
            strSql.Append("select a.ID,a.BulletinName,a.ReceiveDepId,a.ReceiveDepName,a.BulletinContent,a.Attachment,a.FlagTop,a.BillState,a.FlagDel,a.OperaId,a.OperaName,a.OperaTime ");
            strSql.Append("FROM sys_Bulletin a ");
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
            strSql.Append("select a.ID,a.BulletinName,a.ReceiveDepId,a.ReceiveDepName,a.BulletinContent,a.Attachment,a.FlagTop,a.BillState,a.FlagDel,a.OperaId,a.OperaName,a.OperaTime ");
            strSql.Append("FROM sys_Bulletin a ");
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
            strSql.Append("select count(1) FROM sys_Bulletin ");
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
            strSql.Append(")AS Row, T.*  from sys_Bulletin T ");
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
            parameters[0].Value = "sys_Bulletin";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  基本方法

        #region 扩展方法
        /// <summary>
        /// 获得数据列表 通过SQL语句
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.BulletinName,a.ReceiveDepId,a.ReceiveDepName,a.FlagTop,a.BillState,a.OperaName,a.OperaTime ");
            strSql.Append("FROM sys_Bulletin a ");
            strSql.Append("where a.FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" order by a.FlagTop desc,a.OperaTime desc");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 批量发布公告、取消发布公告

        /// </summary>
        public int Submit(string IDList, int billState,int operaId,string operaName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Bulletin set BillState=" + billState.ToString() + ", OperaId=" + operaId.ToString() + ",OperaName='" + operaName + "',OperaTime=getdate() ");
            strSql.Append("where FlagDel=0 and ID in(" + IDList + ")");

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }
        /// <summary>
        /// 批量置顶公告、取消置顶公告

        /// </summary>
        public int SetTop(string IDList, int flagTop, int operaId, string operaName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Bulletin set FlagTop=" + flagTop.ToString() + ", OperaId=" + operaId.ToString() + ",OperaName='" + operaName + "',OperaTime=getdate() ");
            strSql.Append("where FlagDel=0 and ID in(" + IDList + ")");

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }
        /// <summary>
        /// 获得数据列表 通过个人的权限

        /// </summary>
        public DataSet GetListByPower(string strWhere,int recordNum,int depId)
        {
            StringBuilder strSql = new StringBuilder();
            if (recordNum == 0)
            {
                strSql.Append("select a.ID,a.BulletinName,a.ReceiveDepId,a.ReceiveDepName,a.FlagTop,a.OperaName,a.OperaTime ");
            }
            else
            {
                strSql.Append("select top " + recordNum.ToString() + " a.ID,a.BulletinName,a.ReceiveDepId,a.ReceiveDepName,a.FlagTop,a.OperaName,a.OperaTime ");
            }
            strSql.Append("FROM sys_Bulletin a ");
            strSql.Append("where a.FlagDel=0 and a.BillState=1 and a.id in( ");
            strSql.Append("select distinct b.BulletinId from sys_BulletinReceiveDep b ");
            strSql.Append("where b.FlagDel=0 and exists(select c.ID from sys_Department c where c.FlagDel=0 and c.ID=" + depId.ToString() + " and (c.SupList like '%,'+cast(b.ReceiveDepId as varchar(20))+',%' or c.ID=b.ReceiveDepId))) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.FlagTop desc,a.OperaTime desc");
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion 扩展方法
    }
}

