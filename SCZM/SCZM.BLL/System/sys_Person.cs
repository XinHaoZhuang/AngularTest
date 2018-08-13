using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model;
namespace SCZM.BLL.System
{
    /// <summary>
    /// sys_Person
    /// </summary>
    public partial class sys_Person
    {
        private readonly SCZM.DAL.System.sys_Person dal = new SCZM.DAL.System.sys_Person();
        public sys_Person()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录

        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }
        /// <summary>
        /// 查询用户名是否存在

        /// </summary>
        public bool ExistsAccount(string Account)
        {
            return dal.ExistsAccount(Account);
        }
        /// <summary>
        /// 查询用户名、密码是否正确

        /// </summary>
        public string UpdatePwd(string account, string oldPwd,string newPwd)
        {
            //先取得该用户的随机密钥

            string salt = dal.GetSalt(account);
            if (string.IsNullOrEmpty(salt))
            {
                return "数据异常！";
            }
            //把明文进行加密重新赋值

            oldPwd = DESEncrypt.Encrypt(oldPwd, salt);
            if (!dal.Exists(account, oldPwd))
            {
                return "原密码不正确！";
            }
            newPwd = DESEncrypt.Encrypt(newPwd, salt);
            if (!dal.UpdatePwd(account, newPwd))
            {
                return "修改不成功！";
            }
            return "";
        }
        /// <summary>
        /// 增加一条数据

        /// </summary>
        public int Add(SCZM.Model.System.sys_Person model, out string message)
        {
            message = "新增成功！";
            if (model.Account != "")
            {
                if (dal.ExistsAccount(model.Account))
                {
                    message = "对不起，该账号已存在！";
                    return 0;
                }
            }
            //获得6位的salt加密字符串
            model.Salt = Utils.GetCheckCode(6);
            //以随机生成的6位字符串做为密钥加密
            model.Password = DESEncrypt.Encrypt("123", model.Salt);
            model.IsAdmin = false;
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
        public bool Update(SCZM.Model.System.sys_Person model,out string message)
        {
            message = "修改成功！";
            int flagDep = 0;
            int flagRole = 0;
            int flagCtrlPer = 0;
            Model.System.sys_Person tempModel = dal.GetModel(model.ID);
            if (tempModel == null)
            {
                message = "对不起，该条数据已被其他人删除！";
                return false;
            }
            if (model.Account != "" && model.Account != tempModel.Account)
            {
                if (dal.ExistsAccount(model.Account))
                {
                    message = "对不起，账号已存在！";
                    return false;
                }
            }
            model.Salt = tempModel.Salt;
            model.Password = tempModel.Password;
            model.IsAdmin = tempModel.IsAdmin;

            if (model.DepId != tempModel.DepId)
            {
                flagDep = 1;
            }
            if (model.RoleId != tempModel.RoleId)
            {
                flagRole = 1;
            }
            if (model.CtrlPerId != tempModel.CtrlPerId)
            {
                flagCtrlPer = 1;
            }
            int rows = dal.Update(model, flagDep, flagRole, flagCtrlPer);
			if (rows == 0)
			{
				message = "对不起，该条数据已被其他人删除！";
				return false;
			}
			else
			{
                SetPersonRoleCache(model.ID);
				return true;
			}
        }
        /// <summary>
        /// 删除一批数据

        /// </summary>
        public bool DeleteList(string IDlist, out string message)
        {
            message = "删除成功！";

            int rows = dal.DeleteList(IDlist);
            if (rows == 0)
            {
                message = "对不起，所选数据已被其他人删除！";
                return false;
            }
            else
            {
                string[] perIdArr = IDlist.Split(',');
                foreach (string perId in perIdArr)
                {
                    SetPersonRoleCache(int.Parse(perId));
                }
                return true;
            }
        }

        /// <summary>
        /// 得到一个对象实体

        /// </summary>
        public SCZM.Model.System.sys_Person GetModel(int ID)
        {

            return dal.GetModel(ID);
        }
        /// <summary>
        /// 根据用户名密码返回一个实体

        /// </summary>
        public Model.System.sys_LoginUser GetModel(string user_name, string password, bool is_encrypt)
        {
            //检查一下是否需要加密

            if (is_encrypt)
            {
                //先取得该用户的随机密钥
                string salt = dal.GetSalt(user_name);
                if (string.IsNullOrEmpty(salt))
                {
                    return null;
                }
                //把明文进行加密重新赋值

                password = DESEncrypt.Encrypt(password, salt);
            }
            return dal.GetModel(user_name, password);
        }
        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public SCZM.Model.System.sys_Person GetModelByCache(int ID)
        {

            string CacheKey = "sys_PersonModel-" + ID;
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
            return (SCZM.Model.System.sys_Person)objModel;
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
        public List<SCZM.Model.System.sys_Person> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SCZM.Model.System.sys_Person> DataTableToList(DataTable dt)
        {
            List<SCZM.Model.System.sys_Person> modelList = new List<SCZM.Model.System.sys_Person>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SCZM.Model.System.sys_Person model;
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
        /// 分页获取数据列表
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
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod
        /// <summary>
        /// 根据用户名取得Salt
        /// </summary>
        public string GetSalt(string Account)
        {
            return dal.GetSalt(Account);
        }
        /// <summary>
        /// 获得登录用户的菜单数据列表

        /// </summary>
        public DataSet GetUserMenu(int perId)
        {
            return dal.GetUserMenu(perId);
        }
        /// <summary>
        /// 判断用户的按钮权限

        /// </summary>
        public bool CheckUserBtnPower(int menuId, string url, int perId, string elementName)
        {
            bool result = false;
            DataTable dt = dal.CheckUserBtnPower(menuId, url, perId, elementName).Tables[0];
            if (dt.Rows.Count > 0 && Utils.ObjToInt(dt.Rows[0][0], 0) > 0)
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 获得用户的页面按钮

        /// </summary>
        public DataSet GetUserBtn(int menuId, string url, int perId)
        {
            return dal.GetUserBtn(menuId, url, perId);
        }

        /// <summary>
        /// 得到sys_PersonRole数据列表，从缓存中

        /// </summary>
        public DataSet GetListByCache_sys_PersonRole(int perId)
        {
            string CacheKey = "Cache_sys_PersonRole_" + perId;
            object objList = SCZM.Common.DataCache.GetCache(CacheKey);
            if (objList == null)
            {
                try
                {
                    objList = dal.GetPersonRoleList(perId);
                    if (objList != null)
                    {
                        int DataSetCache = SCZM.Common.ConfigHelper.GetConfigInt("DataSetCache");
                        SCZM.Common.DataCache.SetCache(CacheKey, objList, DateTime.Now.AddMinutes(DataSetCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (DataSet)objList;
        }

        /// <summary>
        /// 个人角色改变时，写入缓存
        /// </summary>
        private void SetPersonRoleCache(int perId)
        {
            string CacheKey = "Cache_sys_PersonRole_" + perId;
            object objList = SCZM.Common.DataCache.GetCache(CacheKey);
            if (objList != null)
            {
                try
                {
                    objList = dal.GetPersonRoleList(perId);
                    if (objList != null)
                    {
                        int DataSetCache = SCZM.Common.ConfigHelper.GetConfigInt("DataSetCache");
                        SCZM.Common.DataCache.SetCache(CacheKey, objList, DateTime.Now.AddMinutes(DataSetCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
        }
        /// <summary>
        /// 得到个人密码
        /// </summary>
        /// <param name="perId"></param>
        /// <returns></returns>
        public string GetPwd(int perId)
        {
            string pwd = "";
            Model.System.sys_Person model = dal.GetModel(perId);
            pwd = DESEncrypt.Decrypt(model.Password, model.Salt);
            return pwd;
        }
        /// <summary>
        /// 初始化个人密码

        /// </summary>
        /// <param name="perId"></param>
        /// <returns></returns>
        public string InitPwd(int perId)
        {
            Model.System.sys_Person model = dal.GetModel(perId);
            string pwd=DESEncrypt.Encrypt("123", model.Salt);
            if (!dal.UpdatePwd(model.Account, pwd))
            {
                return "";
            }
            return "123";
        }
        /// <summary>
        /// 或得所有的人员列表 包括上级部门 为人员树使用
        /// </summary>
        /// <returns></returns>
        public DataSet GetDepPersonList()
        {
            return dal.GetDepPersonList();
        }
        /// <summary>
        /// 获得数据列表 通过Where条件
        /// </summary>
        public DataSet GetComboList(string strWhere)
        {
            return dal.GetComboList(strWhere);
        }
        #endregion  ExtensionMethod
    }
}

