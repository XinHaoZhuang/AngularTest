using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;

namespace SCZM.BLL.System
{
    /// <summary>
    /// sys_Common
    /// </summary>
    public partial class sys_Common
    {
        private readonly SCZM.DAL.System.sys_Common dal = new SCZM.DAL.System.sys_Common();
        public sys_Common()
        { }
        /// <summary>
        /// 获得最新申请单号
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="signName">标识</param>
        /// <returns></returns>
        public string GetNewApplyNo(string tableName, string signName)
        {
            return dal.GetNewApplyNo(tableName, signName);
        }
    }
}
