using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model;
namespace SCZM.BLL.System
{
    /// <summary>
    /// 业务逻辑类sys_Process_Exec 的摘要说明。

    /// </summary>
    public partial class sys_Process_Exec
    {
        private readonly SCZM.DAL.System.sys_Process_Exec dal = new SCZM.DAL.System.sys_Process_Exec();
        public sys_Process_Exec()
        { }
        #region  基本方法
        /// <summary>
        /// 是否存在该记录

        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据

        /// </summary>
        public int Add(SCZM.Model.System.sys_Process_Exec model, out string message)
        {
            message = "新增成功！";
            int rowId = dal.Add(model);
            if (rowId < 1)
            {
                message = "新增失败！";
            }
            return rowId;
        }

        /// <summary>
        /// 更新一条数据

        /// </summary>
        public bool Update(SCZM.Model.System.sys_Process_Exec model, out string message)
        {
            message = "修改成功！";
            int rows = dal.Update(model);
            if (rows == 0)
            {
                message = "对不起，该条数据已被其他人删除！";
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 删除一条数据

        /// </summary>
        public bool Delete(int ID, out string message)
        {
            message = "删除成功！";

            int rows = dal.Delete(ID);
            if (rows == 0)
            {
                message = "对不起，该条数据已被其他人删除！";
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDList, out string message)
        {
            message = "删除成功！";

            int rows = dal.DeleteList(IDList);
            if (rows == 0)
            {
                message = "对不起，所选数据已被其他人删除！";
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 得到一个对象实体

        /// </summary>
        public SCZM.Model.System.sys_Process_Exec GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。

        /// </summary>
        public SCZM.Model.System.sys_Process_Exec GetModelByCache(int ID)
        {

            string CacheKey = "sys_Process_ExecModel-" + ID;
            object objModel = SCZM.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ID);
                    if (objModel != null)
                    {
                        int ModelCache = SCZM.Common.ConfigHelper.GetConfigInt("ModelCache");
                        SCZM.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (SCZM.Model.System.sys_Process_Exec)objModel;
        }

        /// <summary>
        /// 获得数据列表 通过SQL语句
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表 根据ID
        /// </summary>
        public DataSet GetList(int ID)
        {
            return dal.GetList(ID);
        }
        /// <summary>
        /// 获得数据列表 根据Para
        /// </summary>
        public DataSet GetList(string strWhere, List<SqlParameter> parameterList)
        {
            return dal.GetList(strWhere, parameterList);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SCZM.Model.System.sys_Process_Exec> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SCZM.Model.System.sys_Process_Exec> DataTableToList(DataTable dt)
        {
            List<SCZM.Model.System.sys_Process_Exec> modelList = new List<SCZM.Model.System.sys_Process_Exec>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SCZM.Model.System.sys_Process_Exec model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetListByPage(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetListByPage(PageSize,PageIndex,strWhere);
        //}

        #endregion  基本方法
        #region  扩展方法
        /// <summary>
        /// 通过审批流判断单据动作是否合法 新增 修改 适合未区分提交动作的方法
        /// </summary>
        /// <param name="billSign">单据识别号</param>
        /// <param name="billId">单据流水号</param>
        /// <param name="action">动作 1 add\2 edit</param>
        /// <param name="depId">单据部门ID</param>
        /// <param name="operaId">操作人ID</param>
        /// <returns>错误信息</returns>
        public string CheckActionByProcess_Edit(string billSign, int billId, int action, int depId, int operaId)
        {
            return dal.CheckActionByProcess_Edit(billSign,billId,action,depId,operaId);
        }
        /// <summary>
        /// 通过审批流判断单据动作是否合法 删除 适合未区分提交动作的方法
        /// </summary>
        /// <param name="billSign">单据识别号</param>
        /// <param name="billIdStr">单据流水号</param>
        /// <param name="operaId">操作人ID</param>
        /// <returns>错误信息</returns>
        public string CheckActionByProcess_Del(string billSign, string billIdStr, int operaId)
        {
            return dal.CheckActionByProcess_Del(billSign,billIdStr,operaId);
        }
        /// <summary>
        /// 设置单据审批流 新增 修改 适合未区分提交动作的方法
        /// </summary>
        /// <param name="billSign">单据识别号</param>
        /// <param name="billId">单据流水号</param>
        /// <param name="action">动作1 add\2 update</param>
        /// <param name="operaId">操作人ID</param>
        /// <returns></returns>
        public string SetBillProcess_Edit(string billSign, int billId, int action, int operaId)
        {
            string message = "";
            DataTable dt = dal.SetBillProcess_Edit(billSign, billId, action, operaId).Tables[0];
            if (dt != null & dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "0")
                {
                    message = dt.Rows[0][1].ToString();
                }
            }
            return message;
        }
        /// <summary>
        /// 设置单据审批流 删除 
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="billId">单据流水号</param>
        /// <returns></returns>
        public string SetBillProcess_Del(string tableName, string billIdStr)
        {
            string message = "";
            DataTable dt = dal.SetBillProcess_Del(tableName, billIdStr).Tables[0];
            if (dt != null & dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "0")
                {
                    message = dt.Rows[0][1].ToString();
                }
            }
            return message;
        }
        /// <summary>
        /// 是否存在已提交的单据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="billId">单据流水号</param>
        /// <returns></returns>
        public string ExistSubmit(string tableName, string billIdStr)
        {
            string message = "";
            DataTable dt = dal.ExistSubmit(tableName, billIdStr).Tables[0];
            if (dt != null & dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "0")
                {
                    message = dt.Rows[0][1].ToString();
                }
            }
            return message;
        }
        /// <summary>
        /// 检测是否可以提交 适合区分提交动作的方法

        /// </summary>
        /// <param name="billSign">单据识别号</param>
        /// <param name="billIdStr">单据流水号</param>
        /// <param name="billIdStr">单据部门 新增时传入，其他传0</param>
        /// <param name="operaId">操作人ID</param>
        /// <returns>错误信息</returns>
        public string CheckSubmit(string tableName, string billSign, int depId, string billIdStr, int operaId)
        {
            return dal.CheckSubmit(tableName, billSign, depId, billIdStr, operaId);
        }
        /// <summary>
        /// 检测是否可以取消提交 适合区分提交动作的方法

        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="billIdStr">单据流水号</param>
        /// <param name="operaId">操作人ID</param>
        /// <returns>错误信息</returns>
        public string CheckCancelSubmit(string tableName, string billIdStr, int operaId)
        {
            return dal.CheckCancelSubmit(tableName, billIdStr, operaId);
        }
        /// <summary>
        /// 提交或取消提交 
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="billIdStr">单据流水号</param>
        /// <param name="action">1 提交 2取消提交</param>
        /// <param name="operaId">操作人</param>
        /// <returns></returns>
        public string SetBillProcess_Submit(string tableName, string billIdStr, int action, int operaId)
        {
            return dal.SetBillProcess_Submit(tableName, billIdStr, action, operaId);
        }
        /// <summary>
        /// 设置单据审批流 审核 取消审核 单张单据 适合审批结束后有处理动作的业务

        /// </summary>
        /// <param name="billSign">单据识别号</param>
        /// <param name="billId">单据流水号</param>
        /// <param name="tableName">表名</param>
        /// <param name="action">动作1 audit\2 cancelaudit</param>
        /// <param name="operaId">操作人ID</param>
        /// <param name="flagAudit">审批状态 1同意2不同意</param>
        /// <param name="auditorOpinion">审批意见</param>
        /// <returns></returns>
        public string SetBillProcess_Audit(string billSign, int billId, int action, int operaId, int flagAudit, string auditorOpinion,out int billState)
        {
            string message = "";
            billState = 0;
            DataTable dt= dal.SetBillProcess_Audit(billSign, billId, action, operaId, flagAudit, auditorOpinion).Tables[0];
            if (dt != null & dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "0")
                {
                    message = dt.Rows[0][1].ToString();
                }
                else
                {
                    billState = Utils.ObjToInt(dt.Rows[0][1], 0);
                }
            }
            return message;
        }
        /// <summary>
        /// 设置单据审批流 审核 取消审核 批量审核消审 适合审批结束后没有处理动作的业务
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="billId">单据流水号</param>
        /// <param name="action">动作1 audit\2 cancelaudit</param>
        /// <param name="operaId">操作人ID</param>
        /// <param name="flagAudit">审批状态 1同意2不同意</param>
        /// <param name="auditorOpinion">审批意见</param>
        /// <returns></returns>
        public bool SetBillProcess_Audit_Batch(string tableName, string billIdStr, int action, int operaId, int flagAudit, string auditorOpinion,out string message)
        {
            bool result = false;
            message = "";
            DataTable dt = dal.SetBillProcess_Audit_Batch(tableName, billIdStr, action, operaId, flagAudit, auditorOpinion).Tables[0];
            if (dt != null & dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "0")
                {
                    result = false;
                    
                }
                else
                {
                    result = true;
                }
                message = dt.Rows[0][1].ToString();
            }
            return result;
        }
        /// <summary>
        /// 获得单据的审批流状态

        /// </summary>
        /// <param name="billSign"></param>
        /// <param name="billId"></param>
        /// <param name="depId"></param>
        /// <returns></returns>
        public DataSet GetBillProcess(string billSign, int billId)
        {
            return dal.GetBillProcess(billSign, billId);
        }
        /// <summary>
        /// 获得单据的审批历史记录

        /// </summary>
        /// <param name="billSign"></param>
        /// <param name="billId"></param>
        /// <returns></returns>
        public DataSet GetBillProcessHistory(string billSign, int billId)
        {
            return dal.GetBillProcessHistory(billSign, billId);
        }
        /// <summary>
        /// 获得单据状态

        /// </summary>
        /// <param name="billSign"></param>
        /// <param name="billId"></param>
        /// <returns></returns>
        public string GetBillState(string billSign, int billId)
        {
            return dal.GetBillState(billSign, billId);
        }
        /// <summary>
        /// 删除指定的sys_Process_Exec,更新对应表为草稿状态（类似取消提交，不记录历史）
        /// </summary>
        /// <param name="billSign">单据标识</param>
        /// <param name="billId">单据ID</param>
        /// <param name="tableName">表名</param>
        /// <param name="OperaId">操作人ID</param>
        /// <param name="OperaName">操作人</param>
        /// <returns></returns>
        public int DelBillProcess(string billSign,int billId,string tableName,int OperaId,string OperaName ) {
            return dal.DelBillProcess(billSign, billId, tableName, OperaId, OperaName);
        }
        #endregion  扩展方法
    }
}

