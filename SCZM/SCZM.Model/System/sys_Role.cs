using System;
using System.Collections.Generic;
namespace SCZM.Model.System
{
    /// <summary>
    /// 实体类sys_Role 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class sys_Role
    {
        public sys_Role()
        { }
        #region Model
        private int _id;
        private string _rolename;
        private string _memo;
        private bool _flagdel = false;
        private string _operaname;
        private DateTime? _operatime = DateTime.Now;
        /// <summary>
        /// 流水号

        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName
        {
            set { _rolename = value; }
            get { return _rolename; }
        }
        /// <summary>
        /// 说明
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool FlagDel
        {
            set { _flagdel = value; }
            get { return _flagdel; }
        }
        /// <summary>
        /// 最终操作人
        /// </summary>
        public string OperaName
        {
            set { _operaname = value; }
            get { return _operaname; }
        }
        /// <summary>
        /// 最终操作时间

        /// </summary>
        public DateTime? OperaTime
        {
            set { _operatime = value; }
            get { return _operatime; }
        }
        #endregion Model

        private List<sys_RolePower> _sys_rolepowers;
        /// <summary>
        /// 子类 
        /// </summary>
        public List<sys_RolePower> sys_RolePowers
        {
            set { _sys_rolepowers = value; }
            get { return _sys_rolepowers; }
        }

    }
    /// <summary>
    /// 实体类sys_RolePower 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class sys_RolePower
    {
        public sys_RolePower()
        { }
        #region Model
        private int _id;
        private int _roleid;
        private int _powerid;
        /// <summary>
        /// 流水号

        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
        /// <summary>
        /// 权限ID
        /// </summary>
        public int PowerId
        {
            set { _powerid = value; }
            get { return _powerid; }
        }
        #endregion Model

    }
}

