using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model.Base;
namespace SCZM.BLL.Base
{
	/// <summary>
	/// 业务逻辑类base_CustomerInformation 的摘要说明。
	/// </summary>
	public partial class base_CustomerInformation
	{
		private readonly SCZM.DAL.Base.base_CustomerInformation dal=new SCZM.DAL.Base.base_CustomerInformation();
		public base_CustomerInformation()
		{}
		#region  基本方法
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(SCZM.Model.Base.base_CustomerInformation model, out string message)
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
		public bool Update(SCZM.Model.Base.base_CustomerInformation model,out string message)
		{
			message = "保存成功！";
			int rows= dal.Update(model);
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
		/// 得到一个对象实体
		/// </summary>
		public SCZM.Model.Base.base_CustomerInformation GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 获得数据列表 通过Where条件
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

        public DataSet GetComboList(string strWhere)
		{
            return dal.GetComboList(strWhere);
		}
        public DataSet GetList_Import(string strWhere)
        {
            return dal.GetList_Import(strWhere);
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
        public int ImportData(string rows,out string message) {
            message = "导入成功";
            int num=dal.ImportData(rows);
            if (num == 0) {
                message = "导入失败";
            }
            return num;
        }
        public void truncateTable() {
            dal.truncateTable();
        }
        public DataSet chooseMachineCode() {
            if (dal.DelMachineCode_Null() >= 0)
            {
                return dal.chooseMachineCode();
            }
            else {
                return null;
            }
        }
        public int DelList_Import(string IdStr) {
            return dal.DelList_Import(IdStr);
        }
        public int InsertInformation()
        {
            return dal.InsertInformation();
        }
        public DataSet GetMachineLevel_Undo() {
            return dal.GetMachineLevel_Undo();
        }
        public int setLevel(string rows) {
            return dal.setLevel(rows);
        }
        public int setMachineId()
        {
            return dal.setMachineId();
        }
		#endregion  扩展方法
	}
}

