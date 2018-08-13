using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.System
{
    /// <summary>
    /// 数据访问类sys_Attachment。

    /// </summary>
    public partial class sys_Attachment
    {
        public sys_Attachment()
        { }
        #region  基本方法
        /// <summary>
        /// 是否存在该记录

        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sys_Attachment");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据

        /// </summary>
        public int Add(SCZM.Model.System.sys_Attachment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_Attachment(");
            strSql.Append("Source,FileName,FilePath,FileUse,UseId,OperaName,OperaTime)");
            strSql.Append(" values (");
            strSql.Append("@Source,@FileName,@FilePath,@FileUse,@UseId,@OperaName,@OperaTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Source", SqlDbType.Int,4),
					new SqlParameter("@FileName", SqlDbType.NVarChar,50),
					new SqlParameter("@FilePath", SqlDbType.NVarChar,100),
					new SqlParameter("@FileUse", SqlDbType.NVarChar,10),
					new SqlParameter("@UseId", SqlDbType.Int,4),
					new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
					new SqlParameter("@OperaTime", SqlDbType.DateTime)};
            parameters[0].Value = model.Source;
            parameters[1].Value = model.FileName;
            parameters[2].Value = model.FilePath;
            parameters[3].Value = model.FileUse;
            parameters[4].Value = model.UseId;
            parameters[5].Value = model.OperaName;
            parameters[6].Value = model.OperaTime;

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
        public int Update(SCZM.Model.System.sys_Attachment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Attachment set ");
            strSql.Append("Source=@Source,");
            strSql.Append("FileName=@FileName,");
            strSql.Append("FilePath=@FilePath,");
            strSql.Append("FileUse=@FileUse,");
            strSql.Append("UseId=@UseId,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@Source", SqlDbType.Int,4),
					new SqlParameter("@FileName", SqlDbType.NVarChar,50),
					new SqlParameter("@FilePath", SqlDbType.NVarChar,100),
					new SqlParameter("@FileUse", SqlDbType.NVarChar,10),
					new SqlParameter("@UseId", SqlDbType.Int,4),
					new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
					new SqlParameter("@OperaTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.Source;
            parameters[1].Value = model.FileName;
            parameters[2].Value = model.FilePath;
            parameters[3].Value = model.FileUse;
            parameters[4].Value = model.UseId;
            parameters[5].Value = model.OperaName;
            parameters[6].Value = model.OperaTime;
            parameters[7].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 删除一条数据

        /// </summary>
        public int Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_Attachment ");
            strSql.Append(" where ID=@ID ");
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
            strSql.Append("delete from sys_Attachment ");
            strSql.Append(" where ID in(" + IDList + ")");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }


        /// <summary>
        /// 得到一个对象实体

        /// </summary>
        public SCZM.Model.System.sys_Attachment GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Source,FileName,FilePath,FileUse,UseId,OperaName,OperaTime from sys_Attachment ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.System.sys_Attachment model = new SCZM.Model.System.sys_Attachment();
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
        public SCZM.Model.System.sys_Attachment DataRowToModel(DataRow row)
        {
            SCZM.Model.System.sys_Attachment model = new SCZM.Model.System.sys_Attachment();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["Source"] != null && row["Source"].ToString() != "")
                {
                    model.Source = int.Parse(row["Source"].ToString());
                }
                if (row["FileName"] != null)
                {
                    model.FileName = row["FileName"].ToString();
                }
                if (row["FilePath"] != null)
                {
                    model.FilePath = row["FilePath"].ToString();
                }
                if (row["FileUse"] != null)
                {
                    model.FileUse = row["FileUse"].ToString();
                }
                if (row["UseId"] != null && row["UseId"].ToString() != "")
                {
                    model.UseId = int.Parse(row["UseId"].ToString());
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
            strSql.Append("select ID,Source,FileName,FilePath,FileUse,UseId,OperaName,OperaTime ");
            strSql.Append("FROM sys_Attachment");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表 通过ID
        /// </summary>
        public DataSet GetList(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Source,FileName,FilePath,FileUse,UseId,OperaName,OperaTime ");
            strSql.Append("FROM sys_Attachment");
            strSql.Append(" where ID=@ID ");
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
            strSql.Append("select ID,Source,FileName,FilePath,FileUse,UseId,OperaName,OperaTime ");
            strSql.Append("FROM sys_Attachment");
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
            strSql.Append("select count(1) FROM sys_Attachment ");
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
            strSql.Append(")AS Row, T.*  from sys_Attachment T ");
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
            parameters[0].Value = "sys_Attachment";
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
        /// 删除使用标志
        /// </summary>
        public int DelUseList( string FileUse, int UseId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Attachment set FileUse='',UseId=0");
            strSql.Append(" where FileUse='" + FileUse + "' and UseId=" + UseId.ToString());
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }
        /// <summary>
        /// 批量更新使用标志
        /// </summary>
        public int UpdateUseList(string IDList, string FileUse, int UseId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Attachment set FileUse='',UseId=0 ");
            strSql.Append("where FileUse='" + FileUse + "' and UseId=" + UseId.ToString());
            DbHelperSQL.ExecuteSql(strSql.ToString());

            strSql.Clear();
            strSql.Append("update sys_Attachment set FileUse='" + FileUse + "',UseId=" + UseId.ToString());
            strSql.Append(" where ID in(" + IDList + ")");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public int Delete(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_Attachment ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }
        /// <summary>
        /// 根据ID字符串获取附件
        /// </summary>
        /// <param name="AttachmentIdStr"></param>
        /// <returns></returns>
        public DataTable GetAttachment(string AttachmentIdStr)
        {
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select ID,FileName,FilePath from sys_Attachment ");
            if (AttachmentIdStr == "")
            {
                strSql2.Append("where ID =0");
            }
            else if (AttachmentIdStr.LastIndexOf(",") == -1)
            {
                strSql2.Append("where ID =" + AttachmentIdStr);
            }
            else
            {
                strSql2.Append("where ID in(" + AttachmentIdStr.Substring(0, AttachmentIdStr.LastIndexOf(",")) + ")");
            }
            return DbHelperSQL.Query(strSql2.ToString()).Tables[0];
        }
        #endregion  扩展方法
    }
}

