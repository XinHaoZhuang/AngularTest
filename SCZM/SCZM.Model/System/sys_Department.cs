using System;
namespace SCZM.Model.System
{
    /// <summary>
    /// 实体类sys_Department 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class sys_Department
    {
        public sys_Department()
        { }
        #region Model
        private int _id;
        private string _depname;
        private int? _levelid;
        private int? _supid;
        private string _suplist;
        private int? _sortid;
        private int? _deptypeid;
        private string _qdcode;
        private string _deptel;
        private bool _flaguse = true;
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
        /// 部门名称
        /// </summary>
        public string DepName
        {
            set { _depname = value; }
            get { return _depname; }
        }
        /// <summary>
        /// 级别
        /// </summary>
        public int? LevelId
        {
            set { _levelid = value; }
            get { return _levelid; }
        }
        /// <summary>
        /// 上级ID
        /// </summary>
        public int? SupId
        {
            set { _supid = value; }
            get { return _supid; }
        }
        /// <summary>
        /// 上级ID列表
        /// </summary>
        public string SupList
        {
            set { _suplist = value; }
            get { return _suplist; }
        }
        /// <summary>
        /// 排序号

        /// </summary>
        public int? SortId
        {
            set { _sortid = value; }
            get { return _sortid; }
        }
        /// <summary>
        /// 部门类型 Id
        /// </summary>
        public int? DepTypeId
        {
            set { _deptypeid = value; }
            get { return _deptypeid; }
        }
        /// <summary>
        /// 前端系统编码
        /// </summary>
        public string QDCode
        {
            set { _qdcode = value; }
            get { return _qdcode; }
        }
        /// <summary>
        /// 部门电话
        /// </summary>
        public string DepTel
        {
            set { _deptel = value; }
            get { return _deptel; }
        }
        /// <summary>
        /// 使用标记
        /// </summary>
        public bool FlagUse
        {
            set { _flaguse = value; }
            get { return _flaguse; }
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
}

