using System;
namespace SCZM.Model.System
{
    /// <summary>
    /// sys_Person:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class sys_Person
    {
        public sys_Person()
        { }
        #region Model
        private int _id;
        private string _pername;
        private int _depid;
        private int _deptypeid;
        private int? _postid;
        private string _pertel;
        private string _peremail;
        private string _ddno;
        private string _wxno;
        private string _account;
        private string _password;
        private string _salt;
        private bool _isadmin;
        private string _roleid;
        private string _rolename;
        private int _ctrlpersontype = 1;
        private int _ctrldepid = 0;
        private string _ctrlperid;
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
        /// 姓名
        /// </summary>
        public string PerName
        {
            set { _pername = value; }
            get { return _pername; }
        }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepId
        {
            set { _depid = value; }
            get { return _depid; }
        }
        /// <summary>
        /// 部门类型ID
        /// </summary>
        public int DepTypeId
        {
            set { _deptypeid = value; }
            get { return _deptypeid; }
        }
        /// <summary>
        /// 岗位ID
        /// </summary>
        public int? PostId
        {
            set { _postid = value; }
            get { return _postid; }
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string PerTel
        {
            set { _pertel = value; }
            get { return _pertel; }
        }
        /// <summary>
        /// Email
        /// </summary>
        public string PerEmail
        {
            set { _peremail = value; }
            get { return _peremail; }
        }
        /// <summary>
        /// 钉钉号

        /// </summary>
        public string DDNo
        {
            set { _ddno = value; }
            get { return _ddno; }
        }
        /// <summary>
        /// 微信号

        /// </summary>
        public string WXNo
        {
            set { _wxno = value; }
            get { return _wxno; }
        }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account
        {
            set { _account = value; }
            get { return _account; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 随机码

        /// </summary>
        public string Salt
        {
            set { _salt = value; }
            get { return _salt; }
        }
        /// <summary>
        /// 是否管理员

        /// </summary>
        public bool IsAdmin
        {
            set { _isadmin = value; }
            get { return _isadmin; }
        }
        /// <summary>
        /// 角色ID字符串

        /// </summary>
        public string RoleId
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
        /// <summary>
        /// 角色名字符串
        /// </summary>
        public string RoleName
        {
            set { _rolename = value; }
            get { return _rolename; }
        }
        /// <summary>
        /// 人员控制类型 1本人 2全部 3按部门 4按人
        /// </summary>
        public int CtrlPersonType
        {
            set { _ctrlpersontype = value; }
            get { return _ctrlpersontype; }
        }
        /// <summary>
        /// 控制部门ID
        /// </summary>
        public int CtrlDepId
        {
            set { _ctrldepid = value; }
            get { return _ctrldepid; }
        }
        /// <summary>
        /// 控制人员ID字符串
        /// </summary>
        public string CtrlPerId
        {
            set { _ctrlperid = value; }
            get { return _ctrlperid; }
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

    }

    /// <summary>
    /// sys_LoginUser:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class sys_LoginUser
    {
        public sys_LoginUser()
        { }
        #region Model
        private int _id;
        private string _pername;
        private string _account;
        private string _pwd;
        private int _depid;
        private int _deptypeid;
        private int? _postid;
        private string _depname;
        private bool _isadmin;
        private string _rolename;
        private string _salt;
        private string _loginip;
        private DateTime? _logintime = DateTime.Now;
        /// <summary>
        /// 流水号

        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string PerName
        {
            set { _pername = value; }
            get { return _pername; }
        }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account
        {
            set { _account = value; }
            get { return _account; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd
        {
            set { _pwd = value; }
            get { return _pwd; }
        }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepId
        {
            set { _depid = value; }
            get { return _depid; }
        }
        /// <summary>
        /// 部门类型ID
        /// </summary>
        public int DepTypeId
        {
            set { _deptypeid = value; }
            get { return _deptypeid; }
        }
        /// <summary>
        /// 岗位ID
        /// </summary>
        public int? PostId
        {
            set { _postid = value; }
            get { return _postid; }
        }
        /// <summary>
        /// 部门
        /// </summary>
        public string DepName
        {
            set { _depname = value; }
            get { return _depname; }
        }
        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin
        {
            set { _isadmin = value; }
            get { return _isadmin; }
        }
        /// <summary>
        /// 角色名字符串
        /// </summary>
        public string RoleName
        {
            set { _rolename = value; }
            get { return _rolename; }
        }

        /// <summary>
        /// 随机码
        /// </summary>
        public string Salt
        {
            set { _salt = value; }
            get { return _salt; }
        }
        /// <summary>
        /// 登录IP
        /// </summary>
        public string LoginIP
        {
            set { _loginip = value; }
            get { return _loginip; }
        }
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime? LoginTime
        {
            set { _logintime = value; }
            get { return _logintime; }
        }
        #endregion Model

    }
}

