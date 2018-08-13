using System;
namespace SCZM.Model.System
{
    /// <summary>
    /// sys_Menu:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class sys_Menu
    {
        public sys_Menu()
        { }
        #region Model
        private int _id;
        private string _menuname;
        private string _linkurl;
        private int? _levelid;
        private int? _supid;
        private string _suplist;
        private int? _sortid;
        private int? _flagdel = 0;
        private string _powerlist;
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
        /// 菜单名称
        /// </summary>
        public string MenuName
        {
            set { _menuname = value; }
            get { return _menuname; }
        }
        /// <summary>
        /// 链接URL
        /// </summary>
        public string LinkUrl
        {
            set { _linkurl = value; }
            get { return _linkurl; }
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
        /// 删除标记
        /// </summary>
        public int? FlagDel
        {
            set { _flagdel = value; }
            get { return _flagdel; }
        }
        /// <summary>
        /// 权限
        /// </summary>
        public string PowerList
        {
            set { _powerlist = value; }
            get { return _powerlist; }
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

