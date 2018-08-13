using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.Proj
{
	/// <summary>
	/// 数据访问类proj_Project。
	/// </summary>
	public partial class proj_Project
	{
		public proj_Project()
		{}
        #region  基本方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SCZM.Model.Proj.proj_Project model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into proj_Project(");
            strSql.Append("ProjName,CustId,SvcManagerId,ProjManagerId,ProjManagerHistory,Memo,FlagDel,OperaId,OperaName,OperaTime)");
            strSql.Append(" values (");
            strSql.Append("@ProjName,@CustId,@SvcManagerId,@ProjManagerId,@ProjManagerHistory,@Memo,@FlagDel,@OperaId,@OperaName,@OperaTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
				new SqlParameter("@ProjName", SqlDbType.NVarChar,50),
				new SqlParameter("@CustId", SqlDbType.Int,4),
				new SqlParameter("@SvcManagerId", SqlDbType.Int,4),
				new SqlParameter("@ProjManagerId", SqlDbType.Int,4),
				new SqlParameter("@ProjManagerHistory", SqlDbType.NVarChar,100),
				new SqlParameter("@Memo", SqlDbType.NVarChar,100),
				new SqlParameter("@FlagDel", SqlDbType.Bit,1),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
				new SqlParameter("@OperaTime", SqlDbType.DateTime)};
            parameters[0].Value = model.ProjName;
            parameters[1].Value = model.CustId;
            parameters[2].Value = model.SvcManagerId;
            parameters[3].Value = model.ProjManagerId;
            parameters[4].Value = model.ProjManagerHistory;
            parameters[5].Value = model.Memo;
            parameters[6].Value = model.FlagDel;
            parameters[7].Value = model.OperaId;
            parameters[8].Value = model.OperaName;
            parameters[9].Value = model.OperaTime;

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
        public int Update(SCZM.Model.Proj.proj_Project model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update proj_Project set ");
            strSql.Append("ProjName=@ProjName,");
            strSql.Append("CustId=@CustId,");
            strSql.Append("SvcManagerId=@SvcManagerId,");
            strSql.Append("ProjManagerId=@ProjManagerId,");
            strSql.Append("ProjManagerHistory=@ProjManagerHistory,");
            strSql.Append("Memo=@Memo,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaId=@OperaId,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ProjName", SqlDbType.NVarChar,50),
				new SqlParameter("@CustId", SqlDbType.Int,4),
				new SqlParameter("@SvcManagerId", SqlDbType.Int,4),
				new SqlParameter("@ProjManagerId", SqlDbType.Int,4),
				new SqlParameter("@ProjManagerHistory", SqlDbType.NVarChar,100),
				new SqlParameter("@Memo", SqlDbType.NVarChar,100),
				new SqlParameter("@FlagDel", SqlDbType.Bit,1),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.ProjName;
            parameters[1].Value = model.CustId;
            parameters[2].Value = model.SvcManagerId;
            parameters[3].Value = model.ProjManagerId;
            parameters[4].Value = model.ProjManagerHistory;
            parameters[5].Value = model.Memo;
            parameters[6].Value = model.FlagDel;
            parameters[7].Value = model.OperaId;
            parameters[8].Value = model.OperaName;
            parameters[9].Value = model.OperaTime;
            parameters[10].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SCZM.Model.Proj.proj_Project GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ProjName,CustId,SvcManagerId,ProjManagerId,ProjManagerHistory,Memo,FlagDel,OperaId,OperaName,OperaTime from proj_Project ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.Proj.proj_Project model = new SCZM.Model.Proj.proj_Project();
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
        public SCZM.Model.Proj.proj_Project DataRowToModel(DataRow row)
        {
            SCZM.Model.Proj.proj_Project model = new SCZM.Model.Proj.proj_Project();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["ProjName"] != null)
                {
                    model.ProjName = row["ProjName"].ToString();
                }
                if (row["CustId"] != null && row["CustId"].ToString() != "")
                {
                    model.CustId = int.Parse(row["CustId"].ToString());
                }
                if (row["SvcManagerId"] != null && row["SvcManagerId"].ToString() != "")
                {
                    model.SvcManagerId = int.Parse(row["SvcManagerId"].ToString());
                }
                if (row["ProjManagerId"] != null && row["ProjManagerId"].ToString() != "")
                {
                    model.ProjManagerId = int.Parse(row["ProjManagerId"].ToString());
                }
                if (row["ProjManagerHistory"] != null)
                {
                    model.ProjManagerHistory = row["ProjManagerHistory"].ToString();
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

        #endregion  基本方法
		#region  扩展方法
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public int DeleteList(string IDList,int operaId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update proj_Project set FlagDel=1 ");
            strSql.Append(" where FlagDel=0 and ProjManagerId in(select e.CtrlPerId from v_sys_PersonCtrl e where e.PerId=" + operaId.ToString() + ") and ID in(" + IDList + ")");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows;
		}

		/// <summary>
		/// 获得数据列表 通过Where条件
		/// </summary>
        public DataSet GetList(string strWhere, int operaId)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select a.ID,a.ProjName,b.CustName,c.PerName as SvcManagerName,d.PerName as ProjManagerName,a.OperaName,a.OperaTime  ");
			strSql.Append("FROM proj_Project a ");
            strSql.Append("left join base_Customer b on a.CustId=b.ID ");
            strSql.Append("left join sys_Person c on a.SvcManagerId=c.ID ");
            strSql.Append("left join sys_Person d on a.ProjManagerId=d.ID ");
            strSql.Append("where a.FlagDel=0 and a.ProjManagerId in(select e.CtrlPerId from v_sys_PersonCtrl e where e.PerId=" + operaId.ToString() + ") ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得数据明细 通过ID
		/// </summary>
		public DataSet GetDetail(int ID)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select a.ID,a.ProjName,a.CustId,a.SvcManagerId,a.ProjManagerId,c.PerName as SvcManagerName,d.PerName as ProjManagerName,a.ProjManagerHistory,a.Memo,a.OperaId,a.OperaName,a.OperaTime ");
			strSql.Append("FROM proj_Project a ");
            strSql.Append("left join sys_Person c on a.SvcManagerId=c.ID ");
            strSql.Append("left join sys_Person d on a.ProjManagerId=d.ID ");
			strSql.Append("where a.FlagDel=0 and a.ID=@ID ");
			SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			return DbHelperSQL.Query(strSql.ToString(), parameters);
		}
        /// <summary>
        /// 获得参照数据列表
        /// </summary>
        /// <param name="strWhere">where条件</param>
        /// <param name="operaId">操作人</param>
        /// <param name="flagContract">是否申请合同 0未申请 1已申请且审批通过</param>
        /// <returns></returns>
        public DataSet GetComboList(string strWhere, int operaId,int flagContract)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 20 a.ID as ProjId,a.ProjName,b.CustName,c.PerName as SvcManagerName,d.PerName as ProjManagerName ");
            strSql.Append("FROM proj_Project a ");
            strSql.Append("left join base_Customer b on a.CustId=b.ID ");
            strSql.Append("left join sys_Person c on a.SvcManagerId=c.ID ");
            strSql.Append("left join sys_Person d on a.ProjManagerId=d.ID ");
            strSql.Append("where a.FlagDel=0 and a.ProjManagerId in(select e.CtrlPerId from v_sys_PersonCtrl e where e.PerId=" + operaId.ToString() + ") ");

            if (flagContract == 0)
            {
                //strSql.Append(" and a.ID not in(select f.ProjId from proj_Contract f where f.FlagDel=0 and f.BillState in(0,1,3))");
                strSql.Append(" and a.ID not in(select f.ProjId from proj_Contract f where f.FlagDel=0)");
            }
            else
            {
                strSql.Append(" and a.ID in(select f.ProjId from proj_Contract f where f.FlagDel=0 and f.BillState=1)");
            }
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.ID");
            return DbHelperSQL.Query(strSql.ToString());
        }
		#endregion  扩展方法
	}
}

