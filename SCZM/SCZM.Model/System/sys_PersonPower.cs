using System;
namespace SCZM.Model.System
{
    /// <summary>
    /// sys_PersonPower:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class sys_PersonPower
    {
        public sys_PersonPower()
        { }
        #region Model
        private int _id;
        private int _perid;
        private int _powerid;
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
        /// 员工ID
        /// </summary>
        public int PerId
        {
            set { _perid = value; }
            get { return _perid; }
        }
        /// <summary>
        /// 权限ID
        /// </summary>
        public int PowerId
        {
            set { _powerid = value; }
            get { return _powerid; }
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

