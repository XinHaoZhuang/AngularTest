using System;
namespace SCZM.Model.System
{
    /// <summary>
    /// sys_OperaLog:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class sys_OperaLog
    {
        public sys_OperaLog()
        { }
        #region Model
        private long _id;
        private int _perid;
        private string _pername;
        private string _peraccount;
        private int _menuid;
        private string _operatype;
        private string _memo;
        private DateTime _operatime = DateTime.Now;
        private string _loginip;
        /// <summary>
        /// 流水号

        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int PerId
        {
            set { _perid = value; }
            get { return _perid; }
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
        public string PerAccount
        {
            set { _peraccount = value; }
            get { return _peraccount; }
        }
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int MenuId
        {
            set { _menuid = value; }
            get { return _menuid; }
        }
        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperaType
        {
            set { _operatype = value; }
            get { return _operatype; }
        }
        /// <summary>
        /// 操作
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperaTime
        {
            set { _operatime = value; }
            get { return _operatime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LoginIP
        {
            set { _loginip = value; }
            get { return _loginip; }
        }
        #endregion Model

    }
}

