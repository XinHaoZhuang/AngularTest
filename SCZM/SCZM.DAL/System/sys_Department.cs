using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.System
{
    /// <summary>
    /// 数据访问类sys_Department。

    /// </summary>
    public partial class sys_Department
    {
        public sys_Department()
        { }
        #region  基本方法
        /// <summary>
        /// 是否存在该记录

        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sys_Department");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据

        /// </summary>
        public int Add(SCZM.Model.System.sys_Department model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_Department(");
            strSql.Append("DepName,LevelId,SupId,SupList,SortId,DepTypeId,QDCode,DepTel,FlagUse,FlagDel,OperaName,OperaTime)");
            strSql.Append(" values (");
            strSql.Append("@DepName,@LevelId,@SupId,@SupList,@SortId,@DepTypeId,@QDCode,@DepTel,@FlagUse,@FlagDel,@OperaName,@OperaTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@DepName", SqlDbType.NVarChar,50),
					new SqlParameter("@LevelId", SqlDbType.Int,4),
					new SqlParameter("@SupId", SqlDbType.Int,4),
					new SqlParameter("@SupList", SqlDbType.VarChar,50),
					new SqlParameter("@SortId", SqlDbType.Int,4),
					new SqlParameter("@DepTypeId", SqlDbType.Int,4),
					new SqlParameter("@QDCode", SqlDbType.VarChar,20),
					new SqlParameter("@DepTel", SqlDbType.VarChar,50),
					new SqlParameter("@FlagUse", SqlDbType.Bit,1),
					new SqlParameter("@FlagDel", SqlDbType.Bit,1),
					new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
					new SqlParameter("@OperaTime", SqlDbType.DateTime)};
            parameters[0].Value = model.DepName;
            parameters[1].Value = model.LevelId;
            parameters[2].Value = model.SupId;
            parameters[3].Value = model.SupList;
            parameters[4].Value = model.SortId;
            parameters[5].Value = model.DepTypeId;
            parameters[6].Value = model.QDCode;
            parameters[7].Value = model.DepTel;
            parameters[8].Value = model.FlagUse;
            parameters[9].Value = model.FlagDel;
            parameters[10].Value = model.OperaName;
            parameters[11].Value = model.OperaTime;

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
        public int Update(SCZM.Model.System.sys_Department model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Department set ");
            strSql.Append("DepName=@DepName,");
            strSql.Append("LevelId=@LevelId,");
            strSql.Append("SupId=@SupId,");
            strSql.Append("SupList=@SupList,");
            strSql.Append("SortId=@SortId,");
            strSql.Append("DepTypeId=@DepTypeId,");
            strSql.Append("QDCode=@QDCode,");
            strSql.Append("DepTel=@DepTel,");
            strSql.Append("FlagUse=@FlagUse,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@DepName", SqlDbType.NVarChar,50),
					new SqlParameter("@LevelId", SqlDbType.Int,4),
					new SqlParameter("@SupId", SqlDbType.Int,4),
					new SqlParameter("@SupList", SqlDbType.VarChar,50),
					new SqlParameter("@SortId", SqlDbType.Int,4),
					new SqlParameter("@DepTypeId", SqlDbType.Int,4),
					new SqlParameter("@QDCode", SqlDbType.VarChar,20),
					new SqlParameter("@DepTel", SqlDbType.VarChar,50),
					new SqlParameter("@FlagUse", SqlDbType.Bit,1),
					new SqlParameter("@FlagDel", SqlDbType.Bit,1),
					new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
					new SqlParameter("@OperaTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.DepName;
            parameters[1].Value = model.LevelId;
            parameters[2].Value = model.SupId;
            parameters[3].Value = model.SupList;
            parameters[4].Value = model.SortId;
            parameters[5].Value = model.DepTypeId;
            parameters[6].Value = model.QDCode;
            parameters[7].Value = model.DepTel;
            parameters[8].Value = model.FlagUse;
            parameters[9].Value = model.FlagDel;
            parameters[10].Value = model.OperaName;
            parameters[11].Value = model.OperaTime;
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
            strSql.Append("update sys_Department set FlagDel=1 ");
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
            strSql.Append("update sys_Department set FlagDel=1 ");
            strSql.Append(" where FlagDel=0 and ID in(" + IDList + ")");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }


        /// <summary>
        /// 得到一个对象实体

        /// </summary>
        public SCZM.Model.System.sys_Department GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,DepName,LevelId,SupId,SupList,SortId,DepTypeId,QDCode,DepTel,FlagUse,FlagDel,OperaName,OperaTime from sys_Department ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.System.sys_Department model = new SCZM.Model.System.sys_Department();
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
        public SCZM.Model.System.sys_Department DataRowToModel(DataRow row)
        {
            SCZM.Model.System.sys_Department model = new SCZM.Model.System.sys_Department();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["DepName"] != null)
                {
                    model.DepName = row["DepName"].ToString();
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
                if (row["DepTypeId"] != null && row["DepTypeId"].ToString() != "")
                {
                    model.DepTypeId = int.Parse(row["DepTypeId"].ToString());
                }
                if (row["QDCode"] != null)
                {
                    model.QDCode = row["QDCode"].ToString();
                }
                if (row["DepTel"] != null)
                {
                    model.DepTel = row["DepTel"].ToString();
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
            strSql.Append("select ID,DepName,LevelId,SupId,SupList,SortId,DepTypeId,QDCode,DepTel,FlagUse,FlagDel,OperaName,OperaTime ");
            strSql.Append("FROM sys_Department");
            strSql.Append(" where FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" order by SupList,SortId,DepName");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表 通过ID
        /// </summary>
        public DataSet GetList(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,DepName,LevelId,SupId,SupList,SortId,DepTypeId,QDCode,DepTel,FlagUse,FlagDel,OperaName,OperaTime ");
            strSql.Append("FROM sys_Department ");
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
            strSql.Append("select a.ID,a.DepName,a.SortId,a.DepTypeId,a.QDCode,a.DepTel,a.FlagUse,a.OperaName,a.OperaTime,b.DepTypeName ");
            strSql.Append("FROM sys_Department a left join sys_DepartmentType b on a.DepTypeId=b.ID ");
            strSql.Append("where FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.SupList,a.SortId,a.DepName");
            return DbHelperSQL.Query(strSql.ToString(), parameterList);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM sys_Department ");
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
            strSql.Append(")AS Row, T.*  from sys_Department T ");
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
            parameters[0].Value = "sys_Department";
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
        /// 得到存在下级的部门列表

        /// </summary>
        /// <param name="IDlist"></param>
        /// <returns></returns>
        public DataSet GetExistsSubList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,DepName FROM sys_Department where FlagDel=0 and ID in (select distinct SupId from sys_Department where FlagDel=0 and SupId in(" + IDlist + ") and ID not in(" + IDlist + ") )");
            strSql.Append(" order by SupId,SortId,ID  ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 得到存在人员的部门列表

        /// </summary>
        /// <param name="IDlist"></param>
        /// <returns></returns>
        public DataSet GetExistsPerList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,DepName FROM sys_Department where FlagDel=0 and ID in (select distinct DepId from sys_Person where FlagDel=0 )and ID in(" + IDlist + ") ");
            strSql.Append(" order by SupId,SortId,ID  ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 得到部门类别列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetDepTypeList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM sys_DepartmentType ");
            strSql.Append(" order by ID  ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 得到部门控制权限列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetCtrlDepList(int DepId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select b.ID as DepId,b.DepName ");
            strSql.Append("FROM sys_DepartmentCtrl a inner join sys_Department b on a.CtrlDepId=b.ID and b.FlagDel=0 ");
            strSql.Append("where a.FlagDel=0 and a.DepId=@DepId");
            SqlParameter[] parameters = {
					new SqlParameter("@DepId", SqlDbType.Int,4)};
            parameters[0].Value = DepId;
            strSql.Append(" order by a.ID");
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获得数据列表 Cache使用
        /// </summary>
        public DataSet GetListForCommon()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.DepName,a.LevelId,a.SupId,a.SupList,a.SortId,a.DepTypeId,a.QDCode,a.FlagUse ");
            strSql.Append("FROM sys_Department a  ");
            strSql.Append("where a.FlagDel=0");
            
            strSql.Append(" order by a.SupList,a.SortId,a.DepName");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得登录人控制的专卖店列表

        /// </summary>
        public DataSet GetShopListByLogin(int perId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CtrlDepId as ShopId,QDCode as ShopCode,CtrlDepName as ShopName ");
            strSql.Append("FROM v_sys_PersonCtrl a ");
            strSql.Append("where DepTypeId=15 and FlagUse=1 and FlagUse=1 and PerId=" + perId.ToString());

            strSql.Append(" order by CtrlDepName");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得登录人控制的专卖店列表 包含分中心


        /// </summary>
        public DataSet GetCentShopListByLogin(int perId, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.CtrlDepId as ShopId,a.QDCode as ShopCode,a.CtrlDepName as ShopName,b.CENT_ID as CentCode,b.CENT_NAME as CentName ");
            strSql.Append("FROM v_sys_PersonCtrl a left join base_Shop b on a.QDCode=b.SHOP_ID ");
            strSql.Append("where a.DepTypeId=15 and b.SHOP_ID is not null and a.PerId=" + perId.ToString());
            if (strWhere != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by b.CENT_NAME,a.CtrlDepName");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得登录人控制的分中心列表

        /// </summary>
        public DataSet GetCentListByLogin(int perId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CtrlDepId as CentId,QDCode as CentCode,CtrlDepName as CentName ");
            strSql.Append("FROM v_sys_PersonCtrl a ");
            strSql.Append("where DepTypeId=10 and FlagUse=1 and PerId=" + perId.ToString());

            strSql.Append(" order by CtrlDepName");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得部门ID 根据前端编码
        /// </summary>
        public int GetDepIdByQDCode(string qdCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ID ");
            strSql.Append("FROM sys_Department ");
            strSql.Append("where FlagUse=1 and QDCode='" + qdCode+"'");

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
        /// 获得专卖店、分中心信息
        /// </summary>
        public DataSet GetShopInfo(int depId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID as ShopId,b.SHOP_ID as ShopCode,b.SHOP_NAME as ShopName,a.SupId as CentId,b.CENT_ID as CentCode,b.CENT_NAME as CentName ");
            strSql.Append("from sys_Department a left join base_Shop b on a.QDCode=b.SHOP_ID ");
            strSql.Append("where ID=" + depId.ToString());
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 父部门上级列表变更时，对应修改子部门的上级列表
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="SupList"></param>
        /// <returns></returns>
        public int UpdateChildDepartment(int ID, string SupList) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update sys_Department set SupList=b.SupList_new,LevelId=(len(SupList_new)-len(replace(SupList_new,',',''))) from sys_Department a,");
            strSql.Append("(select  a.ID,a.SupId,a.SupList,(CHARINDEX('," + ID + ",',','+a.SupList)-1) as IdLength ,STUFF(a.SupList,1,CHARINDEX('," + ID + ",',','+a.SupList)-1,'" + SupList + "') as SupList_new");
            strSql.Append(" from sys_Department a where ','+a.SupList like '%," + ID + ",%' and a.FlagDel=0 ) b ");
            strSql.Append("where a.ID=b.ID and a.FlagDel=0 ");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        /// <summary>
        /// 检查更新部门时，部门表的SupList是否发生变更 无变更返回Ture 有变更返回False。
        /// </summary>
        /// <param name="ID">部门ID</param>
        /// <param name="SupList">更新后的Suplist</param>
        /// <returns></returns>
        public bool Exists(int ID, string SupList) {
            string strSql = "Select COUNT(1) from sys_Department where FlagDel=0 and ID=" + ID + " and SupList='" + SupList + "'";
            return DbHelperSQL.Exists(strSql);
        }
        #endregion  扩展方法
    }
}

