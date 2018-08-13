using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SCZM.DBUtility;//Please add references
namespace SCZM.DAL.System
{
    /// <summary>
    /// 数据访问类:sys_Menu
    /// </summary>
    public partial class sys_Menu
    {
        public sys_Menu()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录

        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sys_Menu");
            strSql.Append(" where  ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据

        /// </summary>
        public int Add(SCZM.Model.System.sys_Menu model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_Menu(");
            strSql.Append("MenuName,LinkUrl,LevelId,SupId,SupList,SortId,PowerList,FlagDel,OperaName,OperaTime)");
            strSql.Append(" values (");
            strSql.Append("@MenuName,@LinkUrl,@LevelId,@SupId,@SupList,@SortId,@PowerList,@FlagDel,@OperaName,@OperaTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@MenuName", SqlDbType.NVarChar,50),
					new SqlParameter("@LinkUrl", SqlDbType.VarChar,100),
					new SqlParameter("@LevelId", SqlDbType.Int,4),
					new SqlParameter("@SupId", SqlDbType.Int,4),
					new SqlParameter("@SupList", SqlDbType.VarChar,50),
					new SqlParameter("@SortId", SqlDbType.Int,4),
					new SqlParameter("@PowerList", SqlDbType.NVarChar,100),
                    new SqlParameter("@FlagDel", SqlDbType.Int,4),
					new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
					new SqlParameter("@OperaTime", SqlDbType.DateTime)};
            parameters[0].Value = model.MenuName;
            parameters[1].Value = model.LinkUrl;
            parameters[2].Value = model.LevelId;
            parameters[3].Value = model.SupId;
            parameters[4].Value = model.SupList;
            parameters[5].Value = model.SortId;
            parameters[6].Value = model.PowerList;
            parameters[7].Value = model.FlagDel;
            parameters[8].Value = model.OperaName;
            parameters[9].Value = model.OperaTime;

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
        public int Update(SCZM.Model.System.sys_Menu model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Menu set ");
            strSql.Append("MenuName=@MenuName,");
            strSql.Append("LinkUrl=@LinkUrl,");
            strSql.Append("LevelId=@LevelId,");
            strSql.Append("SupId=@SupId,");
            strSql.Append("SupList=@SupList,");
            strSql.Append("SortId=@SortId,");
            strSql.Append("PowerList=@PowerList,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@MenuName", SqlDbType.NVarChar,50),
					new SqlParameter("@LinkUrl", SqlDbType.VarChar,100),
					new SqlParameter("@LevelId", SqlDbType.Int,4),
					new SqlParameter("@SupId", SqlDbType.Int,4),
					new SqlParameter("@SupList", SqlDbType.VarChar,50),
					new SqlParameter("@SortId", SqlDbType.Int,4),
					new SqlParameter("@PowerList", SqlDbType.NVarChar,100),
					new SqlParameter("@FlagDel", SqlDbType.Int,4),
					new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
					new SqlParameter("@OperaTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.MenuName;
            parameters[1].Value = model.LinkUrl;
            parameters[2].Value = model.LevelId;
            parameters[3].Value = model.SupId;
            parameters[4].Value = model.SupList;
            parameters[5].Value = model.SortId;
            parameters[6].Value = model.PowerList;
            parameters[7].Value = model.FlagDel;
            parameters[8].Value = model.OperaName;
            parameters[9].Value = model.OperaTime;
            parameters[10].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 删除一条数据

        /// </summary>
        public int Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_Menu ");
            strSql.Append(" where  ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public int DeleteList(string IDlist)
        {
            List<string> sqllist = new List<string>();
            string strSql = "delete from sys_MenuPageElement where PageId in(select PageId from sys_MenuPage where MenuId in(" + IDlist + "))";
            sqllist.Add(strSql);
            strSql = "delete from sys_MenuPage where MenuId in(" + IDlist + ")";
            sqllist.Add(strSql);
            strSql = "delete from sys_MenuPower where MenuId in(" + IDlist + ")";
            sqllist.Add(strSql);

            strSql = "delete from sys_Menu where ID in(" + IDlist + ")";
            sqllist.Add(strSql);

            int rows = DbHelperSQL.ExecuteSqlTran(sqllist);

            return rows;
        }


        /// <summary>
        /// 得到一个对象实体

        /// </summary>
        public SCZM.Model.System.sys_Menu GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,MenuName,LinkUrl,LevelId,SupId,SupList,SortId,PowerList,FlagDel,OperaName,OperaTime from sys_Menu");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            SCZM.Model.System.sys_Menu model = new SCZM.Model.System.sys_Menu();
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
        public SCZM.Model.System.sys_Menu DataRowToModel(DataRow row)
        {
            SCZM.Model.System.sys_Menu model = new SCZM.Model.System.sys_Menu();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["MenuName"] != null)
                {
                    model.MenuName = row["MenuName"].ToString();
                }
                if (row["LinkUrl"] != null)
                {
                    model.LinkUrl = row["LinkUrl"].ToString();
                }
                if (row["LevelId"] != null && row["LevelId"].ToString() != "")
                {
                    model.LevelId = int.Parse(row["LevelId"].ToString());
                }
                if (row["SupId"] != null && row["SupId"].ToString() != "")
                {
                    model.SupId = int.Parse(row["SupId"].ToString());
                }
                if (row["SupList"] != null)
                {
                    model.SupList = row["SupList"].ToString();
                }
                if (row["SortId"] != null && row["SortId"].ToString() != "")
                {
                    model.SortId = int.Parse(row["SortId"].ToString());
                }
                if (row["PowerList"] != null)
                {
                    model.PowerList = row["PowerList"].ToString();
                }
                if (row["FlagDel"] != null && row["FlagDel"].ToString() != "")
                {
                    model.FlagDel = int.Parse(row["FlagDel"].ToString());
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
            strSql.Append("select ID,MenuName,LinkUrl,LevelId,SupId,SupList,SortId,PowerList,FlagDel,OperaName,OperaTime ");
            strSql.Append("FROM sys_Menu where FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by SupList,SortId,ID ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表 通过ID
        /// </summary>
        public DataSet GetList(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,MenuName,LinkUrl,LevelId,SupId,SupList,SortId,PowerList,FlagDel,OperaName,OperaTime ");
            strSql.Append("FROM sys_Menu");
            strSql.Append(" where  ID=@ID ");
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
            strSql.Append("select ID,MenuName,LinkUrl,LevelId,SupId,SupList,SortId,PowerList,FlagDel,OperaName,OperaTime ");
            strSql.Append("FROM sys_Menu");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parameterList);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM sys_Menu ");
            strSql.Append("where  1=1 ");
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
            strSql.Append(")AS Row, T.*  from sys_Menu T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" where " + strWhere);
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
            parameters[0].Value = "sys_Menu";
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
        /// 设置菜单权限
        /// </summary>
        public DataSet SetMenuPower(int menuId, string powerNameStr, string pageStr1, string pageStr2, string pageStr3, string pageStr4, string pageStr5, string pageStr6, string pageStr7, string pageStr8)
        {
            string strSql = "p_sys_AddPower";
            SqlParameter[] parameters = {
                    new SqlParameter("@MenuId", SqlDbType.Int,4),
                    new SqlParameter("@PowerNameStr", SqlDbType.NVarChar, 100),
					new SqlParameter("@PageStr1", SqlDbType.NVarChar, 1000),  
                    new SqlParameter("@PageStr2", SqlDbType.NVarChar, 1000), 
                    new SqlParameter("@PageStr3", SqlDbType.NVarChar, 1000), 
                    new SqlParameter("@PageStr4", SqlDbType.NVarChar, 1000), 
                    new SqlParameter("@PageStr5", SqlDbType.NVarChar, 1000), 
                    new SqlParameter("@PageStr6", SqlDbType.NVarChar, 1000), 
                    new SqlParameter("@PageStr7", SqlDbType.NVarChar, 1000), 
                    new SqlParameter("@PageStr8", SqlDbType.NVarChar, 1000)
                                        };
            parameters[0].Value = menuId;
            parameters[1].Value = powerNameStr;
            parameters[2].Value = pageStr1;
            parameters[3].Value = pageStr2;
            parameters[4].Value = pageStr3;
            parameters[5].Value = pageStr4;
            parameters[6].Value = pageStr5;
            parameters[7].Value = pageStr6;
            parameters[8].Value = pageStr7;
            parameters[9].Value = pageStr8;
            return DbHelperSQL.RunProcedure(strSql, parameters, "dt");
        }
        /// <summary>
        /// 得到存在下级的菜单列表

        /// </summary>
        /// <param name="IDlist"></param>
        /// <returns></returns>
        public DataSet GetExistsSubList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM sys_Menu where  ID in (select distinct SupId from sys_Menu where SupId in(" + IDlist + ") and ID not in(" + IDlist + ") )");
            strSql.Append(" order by SupId,SortId,ID  ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 得到菜单的注册页面列表

        /// </summary>
        /// <param name="IDlist"></param>
        /// <returns></returns>
        public DataSet GetPageList(int menuId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from sys_MenuPage where MenuId=" + menuId);
            strSql.Append(" order by PageId  ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 得到没有权限的按钮列表

        /// </summary>
        /// <param name="perId">人员ID</param>
        /// <returns></returns>
        public DataSet GetNoPowerBtn(int perId)
        {
            string strSql = "p_sys_GetNoPowerBtn";
            SqlParameter[] parameters = {
                    new SqlParameter("@perId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = perId;
            return DbHelperSQL.RunProcedure(strSql, parameters, "dt");
        }
        /// <summary>
        /// 获得待审核单据列表

        /// </summary>
        public DataSet GetTodoList(int perId)
        {
            string strSql = "p_sys_GetTodoList";
            SqlParameter[] parameters = {
                    new SqlParameter("@perId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = perId;
            return DbHelperSQL.RunProcedure(strSql, parameters, "dt");
        }
        /// <summary>
        /// 获得未完成单据列表

        /// </summary>
        public DataSet GetNodoList(int perId)
        {
            string strSql = "p_sys_GetNodoList";
            SqlParameter[] parameters = {
                    new SqlParameter("@perId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = perId;
            return DbHelperSQL.RunProcedure(strSql, parameters, "dt");
        }
        #region 缓存权限使用
        /// <summary>
        /// 得到菜单的注册页面列表 全部
        /// </summary>
        /// <returns></returns>
        public DataSet GetMenuPageList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PageId,MenuId,PageUrl from sys_MenuPage");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 得到菜单的注册页面的元素列表 全部
        /// </summary>
        /// <returns></returns>
        public DataSet GetMenuPageElementList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PageId,ElementName,PowerId from sys_MenuPageElement");
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion 缓存权限使用
        //效率测试临时表

        public void test(int type, double time)
        {
            string str = "insert into test(type,time) values(" + type.ToString() + "," + time.ToString() + ")";
            DbHelperSQL.ExecuteSql(str);
        }

        #endregion  ExtensionMethod
    }
}

