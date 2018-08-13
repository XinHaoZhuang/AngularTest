using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model.Proj;
namespace SCZM.BLL.Proj
{
	/// <summary>
	/// 业务逻辑类proj_Project 的摘要说明。
	/// </summary>
	public partial class proj_Project
	{
		private readonly SCZM.DAL.Proj.proj_Project dal=new SCZM.DAL.Proj.proj_Project();
		public proj_Project()
		{}
		#region  基本方法
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(SCZM.Model.Proj.proj_Project model, out string message)
		{
			message = "保存成功！";
			int rowId= dal.Add(model);
			if (rowId <1)
			{
				message = "保存失败！";
			}
			return rowId;
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SCZM.Model.Proj.proj_Project model,out string message)
		{
			message = "保存成功！";
			int rows= dal.Update(model);
			if (rows == 0)
			{
				message = "对不起，您没有控制该条记录的权限！";
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
		public SCZM.Model.Proj.proj_Project GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 获得数据列表 通过Where条件
		/// </summary>
        public DataSet GetList(string strWhere, int operaId)
		{
            return dal.GetList(strWhere, operaId);
		}

		/// <summary>
		/// 获得数据明细 根据ID
		/// </summary>
		public DataSet GetDetail(int ID)
		{
			return dal.GetDetail(ID);
		}

		#endregion  基本方法
		#region  扩展方法
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDList, int operaId, out string message)
		{
			message = "删除成功！";
            int rows = dal.DeleteList(IDList, operaId);
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
        /// 获得参照数据列表
        /// </summary>
        /// <param name="strWhere">where条件</param>
        /// <param name="operaId">操作人</param>
        /// <param name="flagContract">是否申请合同 0未申请 1已申请且审批通过</param>
        /// <returns></returns>
        public DataSet GetComboList(string strWhere, int operaId, int flagContract)
        {
            return dal.GetComboList(strWhere, operaId, flagContract);
        }
		#endregion  扩展方法
	}
}

