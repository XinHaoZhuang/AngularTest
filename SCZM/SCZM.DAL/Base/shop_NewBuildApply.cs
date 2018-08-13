using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using UHPROJ.DBUtility;
namespace UHPROJ.DAL.Base
{
    /// <summary>
    /// 数据访问类shop_NewBuildApply。
    /// </summary>
    public partial class shop_NewBuildApply
    {
        public shop_NewBuildApply()
        { }
        #region  基本方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(UHPROJ.Model.Base.shop_NewBuildApply model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into shop_NewBuildApply(");
            strSql.Append("ApplyNo,CityAreaId,CityName,CountyName,MarketLevel,ShopName,ShopAddress,ShopType,ShopClass,DisplayArea,DisplayArea_CHG,DisplayArea_YG,BuildDate,DistributorName,DistributorTelephone,DistributorMobile,DistributorEmail,DistributorContext,Category,FranchiseFee,GuaranteeMoney,FirstPayment,AttachmentId_ZSRW,AttachmentId_GH,BillSign,AuditNextId,BillState,FlagDel,DepId,OperaId,OperaName,OperaTime,AuditEndTime)");
            strSql.Append(" values (");
            strSql.Append("@ApplyNo,@CityAreaId,@CityName,@CountyName,@MarketLevel,@ShopName,@ShopAddress,@ShopType,@ShopClass,@DisplayArea,@DisplayArea_CHG,@DisplayArea_YG,@BuildDate,@DistributorName,@DistributorTelephone,@DistributorMobile,@DistributorEmail,@DistributorContext,@Category,@FranchiseFee,@GuaranteeMoney,@FirstPayment,@AttachmentId_ZSRW,@AttachmentId_GH,@BillSign,@AuditNextId,@BillState,@FlagDel,@DepId,@OperaId,@OperaName,@OperaTime,@AuditEndTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
				new SqlParameter("@ApplyNo", SqlDbType.VarChar,20),
				new SqlParameter("@CityAreaId", SqlDbType.Int,4),
				new SqlParameter("@CityName", SqlDbType.NVarChar,10),
				new SqlParameter("@CountyName", SqlDbType.NVarChar,10),
				new SqlParameter("@MarketLevel", SqlDbType.Int,4),
				new SqlParameter("@ShopName", SqlDbType.NVarChar,50),
				new SqlParameter("@ShopAddress", SqlDbType.NVarChar,50),
				new SqlParameter("@ShopType", SqlDbType.Int,4),
				new SqlParameter("@ShopClass", SqlDbType.Int,4),
				new SqlParameter("@DisplayArea", SqlDbType.Decimal,9),
				new SqlParameter("@DisplayArea_CHG", SqlDbType.Decimal,9),
				new SqlParameter("@DisplayArea_YG", SqlDbType.Decimal,9),
				new SqlParameter("@BuildDate", SqlDbType.SmallDateTime),
				new SqlParameter("@DistributorName", SqlDbType.NVarChar,10),
				new SqlParameter("@DistributorTelephone", SqlDbType.VarChar,20),
				new SqlParameter("@DistributorMobile", SqlDbType.VarChar,20),
				new SqlParameter("@DistributorEmail", SqlDbType.VarChar,30),
				new SqlParameter("@DistributorContext", SqlDbType.NVarChar,500),
				new SqlParameter("@Category", SqlDbType.Int,4),
				new SqlParameter("@FranchiseFee", SqlDbType.Decimal,9),
				new SqlParameter("@GuaranteeMoney", SqlDbType.Decimal,9),
				new SqlParameter("@FirstPayment", SqlDbType.Decimal,9),
				new SqlParameter("@AttachmentId_ZSRW", SqlDbType.VarChar,100),
				new SqlParameter("@AttachmentId_GH", SqlDbType.VarChar,100),
				new SqlParameter("@BillSign", SqlDbType.VarChar,50),
				new SqlParameter("@AuditNextId", SqlDbType.Int,4),
				new SqlParameter("@BillState", SqlDbType.Int,4),
				new SqlParameter("@FlagDel", SqlDbType.Bit,1),
				new SqlParameter("@DepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
				new SqlParameter("@AuditEndTime", SqlDbType.DateTime)};
            parameters[0].Value = model.ApplyNo;
            parameters[1].Value = model.CityAreaId;
            parameters[2].Value = model.CityName;
            parameters[3].Value = model.CountyName;
            parameters[4].Value = model.MarketLevel;
            parameters[5].Value = model.ShopName;
            parameters[6].Value = model.ShopAddress;
            parameters[7].Value = model.ShopType;
            parameters[8].Value = model.ShopClass;
            parameters[9].Value = model.DisplayArea;
            parameters[10].Value = model.DisplayArea_CHG;
            parameters[11].Value = model.DisplayArea_YG;
            parameters[12].Value = model.BuildDate;
            parameters[13].Value = model.DistributorName;
            parameters[14].Value = model.DistributorTelephone;
            parameters[15].Value = model.DistributorMobile;
            parameters[16].Value = model.DistributorEmail;
            parameters[17].Value = model.DistributorContext;
            parameters[18].Value = model.Category;
            parameters[19].Value = model.FranchiseFee;
            parameters[20].Value = model.GuaranteeMoney;
            parameters[21].Value = model.FirstPayment;
            parameters[22].Value = model.AttachmentId_ZSRW;
            parameters[23].Value = model.AttachmentId_GH;
            parameters[24].Value = model.BillSign;
            parameters[25].Value = model.AuditNextId;
            parameters[26].Value = model.BillState;
            parameters[27].Value = model.FlagDel;
            parameters[28].Value = model.DepId;
            parameters[29].Value = model.OperaId;
            parameters[30].Value = model.OperaName;
            parameters[31].Value = model.OperaTime;
            parameters[32].Value = model.AuditEndTime;

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
        public int Update(UHPROJ.Model.Base.shop_NewBuildApply model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update shop_NewBuildApply set ");
            strSql.Append("ApplyNo=@ApplyNo,");
            strSql.Append("CityAreaId=@CityAreaId,");
            strSql.Append("CityName=@CityName,");
            strSql.Append("CountyName=@CountyName,");
            strSql.Append("MarketLevel=@MarketLevel,");
            strSql.Append("ShopName=@ShopName,");
            strSql.Append("ShopAddress=@ShopAddress,");
            strSql.Append("ShopType=@ShopType,");
            strSql.Append("ShopClass=@ShopClass,");
            strSql.Append("DisplayArea=@DisplayArea,");
            strSql.Append("DisplayArea_CHG=@DisplayArea_CHG,");
            strSql.Append("DisplayArea_YG=@DisplayArea_YG,");
            strSql.Append("BuildDate=@BuildDate,");
            strSql.Append("DistributorName=@DistributorName,");
            strSql.Append("DistributorTelephone=@DistributorTelephone,");
            strSql.Append("DistributorMobile=@DistributorMobile,");
            strSql.Append("DistributorEmail=@DistributorEmail,");
            strSql.Append("DistributorContext=@DistributorContext,");
            strSql.Append("Category=@Category,");
            strSql.Append("FranchiseFee=@FranchiseFee,");
            strSql.Append("GuaranteeMoney=@GuaranteeMoney,");
            strSql.Append("FirstPayment=@FirstPayment,");
            strSql.Append("AttachmentId_ZSRW=@AttachmentId_ZSRW,");
            strSql.Append("AttachmentId_GH=@AttachmentId_GH,");
            strSql.Append("BillSign=@BillSign,");
            strSql.Append("AuditNextId=@AuditNextId,");
            strSql.Append("BillState=@BillState,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("DepId=@DepId,");
            strSql.Append("OperaId=@OperaId,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime,");
            strSql.Append("AuditEndTime=@AuditEndTime");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ApplyNo", SqlDbType.VarChar,20),
				new SqlParameter("@CityAreaId", SqlDbType.Int,4),
				new SqlParameter("@CityName", SqlDbType.NVarChar,10),
				new SqlParameter("@CountyName", SqlDbType.NVarChar,10),
				new SqlParameter("@MarketLevel", SqlDbType.Int,4),
				new SqlParameter("@ShopName", SqlDbType.NVarChar,50),
				new SqlParameter("@ShopAddress", SqlDbType.NVarChar,50),
				new SqlParameter("@ShopType", SqlDbType.Int,4),
				new SqlParameter("@ShopClass", SqlDbType.Int,4),
				new SqlParameter("@DisplayArea", SqlDbType.Decimal,9),
				new SqlParameter("@DisplayArea_CHG", SqlDbType.Decimal,9),
				new SqlParameter("@DisplayArea_YG", SqlDbType.Decimal,9),
				new SqlParameter("@BuildDate", SqlDbType.SmallDateTime),
				new SqlParameter("@DistributorName", SqlDbType.NVarChar,10),
				new SqlParameter("@DistributorTelephone", SqlDbType.VarChar,20),
				new SqlParameter("@DistributorMobile", SqlDbType.VarChar,20),
				new SqlParameter("@DistributorEmail", SqlDbType.VarChar,30),
				new SqlParameter("@DistributorContext", SqlDbType.NVarChar,500),
				new SqlParameter("@Category", SqlDbType.Int,4),
				new SqlParameter("@FranchiseFee", SqlDbType.Decimal,9),
				new SqlParameter("@GuaranteeMoney", SqlDbType.Decimal,9),
				new SqlParameter("@FirstPayment", SqlDbType.Decimal,9),
				new SqlParameter("@AttachmentId_ZSRW", SqlDbType.VarChar,100),
				new SqlParameter("@AttachmentId_GH", SqlDbType.VarChar,100),
				new SqlParameter("@BillSign", SqlDbType.VarChar,50),
				new SqlParameter("@AuditNextId", SqlDbType.Int,4),
				new SqlParameter("@BillState", SqlDbType.Int,4),
				new SqlParameter("@FlagDel", SqlDbType.Bit,1),
				new SqlParameter("@DepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
				new SqlParameter("@AuditEndTime", SqlDbType.DateTime),
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.ApplyNo;
            parameters[1].Value = model.CityAreaId;
            parameters[2].Value = model.CityName;
            parameters[3].Value = model.CountyName;
            parameters[4].Value = model.MarketLevel;
            parameters[5].Value = model.ShopName;
            parameters[6].Value = model.ShopAddress;
            parameters[7].Value = model.ShopType;
            parameters[8].Value = model.ShopClass;
            parameters[9].Value = model.DisplayArea;
            parameters[10].Value = model.DisplayArea_CHG;
            parameters[11].Value = model.DisplayArea_YG;
            parameters[12].Value = model.BuildDate;
            parameters[13].Value = model.DistributorName;
            parameters[14].Value = model.DistributorTelephone;
            parameters[15].Value = model.DistributorMobile;
            parameters[16].Value = model.DistributorEmail;
            parameters[17].Value = model.DistributorContext;
            parameters[18].Value = model.Category;
            parameters[19].Value = model.FranchiseFee;
            parameters[20].Value = model.GuaranteeMoney;
            parameters[21].Value = model.FirstPayment;
            parameters[22].Value = model.AttachmentId_ZSRW;
            parameters[23].Value = model.AttachmentId_GH;
            parameters[24].Value = model.BillSign;
            parameters[25].Value = model.AuditNextId;
            parameters[26].Value = model.BillState;
            parameters[27].Value = model.FlagDel;
            parameters[28].Value = model.DepId;
            parameters[29].Value = model.OperaId;
            parameters[30].Value = model.OperaName;
            parameters[31].Value = model.OperaTime;
            parameters[32].Value = model.AuditEndTime;
            parameters[33].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public UHPROJ.Model.Base.shop_NewBuildApply GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ApplyNo,CityAreaId,CityName,CountyName,MarketLevel,ShopName,ShopAddress,ShopType,ShopClass,DisplayArea,DisplayArea_CHG,DisplayArea_YG,BuildDate,DistributorName,DistributorTelephone,DistributorMobile,DistributorEmail,DistributorContext,Category,FranchiseFee,GuaranteeMoney,FirstPayment,AttachmentId_ZSRW,AttachmentId_GH,BillSign,AuditNextId,BillState,FlagDel,DepId,OperaId,OperaName,OperaTime,AuditEndTime from shop_NewBuildApply ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            UHPROJ.Model.Base.shop_NewBuildApply model = new UHPROJ.Model.Base.shop_NewBuildApply();
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
        public UHPROJ.Model.Base.shop_NewBuildApply DataRowToModel(DataRow row)
        {
            UHPROJ.Model.Base.shop_NewBuildApply model = new UHPROJ.Model.Base.shop_NewBuildApply();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["ApplyNo"] != null)
                {
                    model.ApplyNo = row["ApplyNo"].ToString();
                }
                if (row["CityAreaId"] != null && row["CityAreaId"].ToString() != "")
                {
                    model.CityAreaId = int.Parse(row["CityAreaId"].ToString());
                }
                if (row["CityName"] != null)
                {
                    model.CityName = row["CityName"].ToString();
                }
                if (row["CountyName"] != null)
                {
                    model.CountyName = row["CountyName"].ToString();
                }
                if (row["MarketLevel"] != null && row["MarketLevel"].ToString() != "")
                {
                    model.MarketLevel = int.Parse(row["MarketLevel"].ToString());
                }
                if (row["ShopName"] != null)
                {
                    model.ShopName = row["ShopName"].ToString();
                }
                if (row["ShopAddress"] != null)
                {
                    model.ShopAddress = row["ShopAddress"].ToString();
                }
                if (row["ShopType"] != null && row["ShopType"].ToString() != "")
                {
                    model.ShopType = int.Parse(row["ShopType"].ToString());
                }
                if (row["ShopClass"] != null && row["ShopClass"].ToString() != "")
                {
                    model.ShopClass = int.Parse(row["ShopClass"].ToString());
                }
                if (row["DisplayArea"] != null && row["DisplayArea"].ToString() != "")
                {
                    model.DisplayArea = decimal.Parse(row["DisplayArea"].ToString());
                }
                if (row["DisplayArea_CHG"] != null && row["DisplayArea_CHG"].ToString() != "")
                {
                    model.DisplayArea_CHG = decimal.Parse(row["DisplayArea_CHG"].ToString());
                }
                if (row["DisplayArea_YG"] != null && row["DisplayArea_YG"].ToString() != "")
                {
                    model.DisplayArea_YG = decimal.Parse(row["DisplayArea_YG"].ToString());
                }
                if (row["BuildDate"] != null && row["BuildDate"].ToString() != "")
                {
                    model.BuildDate = DateTime.Parse(row["BuildDate"].ToString());
                }
                if (row["DistributorName"] != null)
                {
                    model.DistributorName = row["DistributorName"].ToString();
                }
                if (row["DistributorTelephone"] != null)
                {
                    model.DistributorTelephone = row["DistributorTelephone"].ToString();
                }
                if (row["DistributorMobile"] != null)
                {
                    model.DistributorMobile = row["DistributorMobile"].ToString();
                }
                if (row["DistributorEmail"] != null)
                {
                    model.DistributorEmail = row["DistributorEmail"].ToString();
                }
                if (row["DistributorContext"] != null)
                {
                    model.DistributorContext = row["DistributorContext"].ToString();
                }
                if (row["Category"] != null && row["Category"].ToString() != "")
                {
                    model.Category = int.Parse(row["Category"].ToString());
                }
                if (row["FranchiseFee"] != null && row["FranchiseFee"].ToString() != "")
                {
                    model.FranchiseFee = decimal.Parse(row["FranchiseFee"].ToString());
                }
                if (row["GuaranteeMoney"] != null && row["GuaranteeMoney"].ToString() != "")
                {
                    model.GuaranteeMoney = decimal.Parse(row["GuaranteeMoney"].ToString());
                }
                if (row["FirstPayment"] != null && row["FirstPayment"].ToString() != "")
                {
                    model.FirstPayment = decimal.Parse(row["FirstPayment"].ToString());
                }
                if (row["AttachmentId_ZSRW"] != null)
                {
                    model.AttachmentId_ZSRW = row["AttachmentId_ZSRW"].ToString();
                }
                if (row["AttachmentId_GH"] != null)
                {
                    model.AttachmentId_GH = row["AttachmentId_GH"].ToString();
                }
                if (row["BillSign"] != null)
                {
                    model.BillSign = row["BillSign"].ToString();
                }
                if (row["AuditNextId"] != null && row["AuditNextId"].ToString() != "")
                {
                    model.AuditNextId = int.Parse(row["AuditNextId"].ToString());
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
                if (row["DepId"] != null && row["DepId"].ToString() != "")
                {
                    model.DepId = int.Parse(row["DepId"].ToString());
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
                if (row["AuditEndTime"] != null && row["AuditEndTime"].ToString() != "")
                {
                    model.AuditEndTime = DateTime.Parse(row["AuditEndTime"].ToString());
                }
            }
            return model;
        }

        #endregion  基本方法
        #region  扩展方法
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public int DeleteList(string IDList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update shop_NewBuildApply set FlagDel=1 ");
            strSql.Append(" where FlagDel=0 and ID in(" + IDList + ")");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }

        /// <summary>
        /// 获取申请页面的数据列表 通过存储过程
        /// </summary>
        /// <param name="strWhere">where条件</param>
        /// <param name="perId">查询人ID</param>
        /// <param name="billState">单据状态0全部 1审批完成 2 审批未完成 3审批中 4审批不同意 5未提交 6已提交</param>
        /// <returns></returns>
        public DataSet GetApplyList(string strWhere, int perId, int billState)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.ApplyNo,a.CityAreaId,a.CityName,a.CountyName,a.MarketLevel,a.ShopName,a.ShopAddress,a.ShopType,a.ShopClass,a.DisplayArea,a.DisplayArea_CHG,a.DisplayArea_YG,a.BuildDate,a.DistributorName,a.DistributorTelephone,a.DistributorMobile,a.DistributorEmail,a.DistributorContext,a.Category,a.FranchiseFee,a.GuaranteeMoney,a.FirstPayment,a.AttachmentId_ZSRW,a.AttachmentId_GH,a.BillSign,a.AuditNextId,a.BillState,a.DepId,a.OperaId,a.OperaName,a.OperaTime,a.AuditEndTime ");
            strSql.Append("FROM shop_NewBuildApply a ");
            string strOrder = "a.OperaTime desc";
            string procName = "p_sys_GetApplyList";
            SqlParameter[] parameters = {
				new SqlParameter("@strSql", SqlDbType.VarChar),
				new SqlParameter("@strWhere", SqlDbType.VarChar),
				new SqlParameter("@strOrder", SqlDbType.VarChar),
				new SqlParameter("@perId", SqlDbType.Int,4),
				new SqlParameter("@billState", SqlDbType.Int,4)};
            parameters[0].Value = strSql.ToString();
            parameters[1].Value = strWhere;
            parameters[2].Value = strOrder;
            parameters[3].Value = perId;
            parameters[4].Value = billState;
            return DbHelperSQL.RunProcedure(procName, parameters, "dt");
        }

        /// <summary>
        /// 获取审核页面的数据列表 通过存储过程
        /// </summary>
        /// <param name="strWhere">where条件</param>
        /// <param name="perId">查询人ID</param>
        /// <param name="billState">单据状态0全部 1审批完成 2 审批未完成 3审批中 4审批不同意 5未提交 6已提交</param>
        /// <param name="auditState">审核状态 0全部 1已审核 2 未审核	3 审批同意	4 审批不同意</param>
        /// <returns></returns>
        public DataSet GetAuditList(string strWhere, int perId, int billState, int auditState)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.ApplyNo,a.CityAreaId,a.CityName,a.CountyName,a.MarketLevel,a.ShopName,a.ShopAddress,a.ShopType,a.ShopClass,a.DisplayArea,a.DisplayArea_CHG,a.DisplayArea_YG,a.BuildDate,a.DistributorName,a.DistributorTelephone,a.DistributorMobile,a.DistributorEmail,a.DistributorContext,a.Category,a.FranchiseFee,a.GuaranteeMoney,a.FirstPayment,a.AttachmentId_ZSRW,a.AttachmentId_GH,a.BillSign,a.AuditNextId,a.BillState,a.DepId,a.OperaId,a.OperaName,a.OperaTime,a.AuditEndTime ");
            strSql.Append("FROM shop_NewBuildApply a ");
            string strOrder = "a.ID desc";
            string procName = "p_sys_GetAuditList";
            SqlParameter[] parameters = {
				new SqlParameter("@strSql", SqlDbType.VarChar),
				new SqlParameter("@strWhere", SqlDbType.VarChar),
				new SqlParameter("@strOrder", SqlDbType.VarChar),
				new SqlParameter("@perId", SqlDbType.Int,4),
				new SqlParameter("@billState", SqlDbType.Int,4),
				new SqlParameter("@auditState", SqlDbType.Int,4)};
            parameters[0].Value = strSql.ToString();
            parameters[1].Value = strWhere;
            parameters[2].Value = strOrder;
            parameters[3].Value = perId;
            parameters[4].Value = billState;
            parameters[5].Value = auditState;
            return DbHelperSQL.RunProcedure(procName, parameters, "dt");
        }

        /// <summary>
        /// 获得数据明细 通过ID
        /// </summary>
        public DataSet GetDetail(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.ApplyNo,a.CityAreaId,a.CityName,a.CountyName,a.MarketLevel,a.ShopName,a.ShopAddress,a.ShopType,a.ShopClass,a.DisplayArea,a.DisplayArea_CHG,a.DisplayArea_YG,a.BuildDate,a.DistributorName,a.DistributorTelephone,a.DistributorMobile,a.DistributorEmail,a.DistributorContext,a.Category,a.FranchiseFee,a.GuaranteeMoney,a.FirstPayment,a.AttachmentId_ZSRW,a.AttachmentId_GH,a.BillSign,a.AuditNextId,a.BillState,a.DepId,a.OperaId,a.OperaName,a.OperaTime,a.AuditEndTime ");
            strSql.Append("FROM shop_NewBuildApply a ");
            strSql.Append("where a.FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            string AttachmentId = "";
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                AttachmentId = (ds.Tables[0].Rows[0]["AttachmentId_ZSRW"].ToString() == "" ? "" : ds.Tables[0].Rows[0]["AttachmentId_ZSRW"].ToString() + ",") +
                    (ds.Tables[0].Rows[0]["AttachmentId_GH"].ToString() == "" ? "" : ds.Tables[0].Rows[0]["AttachmentId_GH"].ToString() + ",")
                    ;
            }
            DataTable dt = new System.sys_Attachment().GetAttachment(AttachmentId);
            dt.TableName = "attachment";
            ds.Tables.Add(dt.Copy());
            return ds;
        }

        #endregion  扩展方法
    }
}

