using System;
namespace SCZM.Model.Proj
{
    /// <summary>
    /// 实体类proj_Project 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class proj_Project
    {
        public proj_Project()
        { }
        #region Model
        private int _id;
        private string _projname;
        private int _custid;
        private int? _svcmanagerid;
        private int? _projmanagerid;
        private string _projmanagerhistory;
        private string _memo;
        private bool _flagdel = false;
        private int _operaid = 0;
        private string _operaname;
        private DateTime _operatime = DateTime.Now;
        /// <summary>
        /// 流水号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjName
        {
            set { _projname = value; }
            get { return _projname; }
        }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustId
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 业务经理
        /// </summary>
        public int? SvcManagerId
        {
            set { _svcmanagerid = value; }
            get { return _svcmanagerid; }
        }
        /// <summary>
        /// 项目经理
        /// </summary>
        public int? ProjManagerId
        {
            set { _projmanagerid = value; }
            get { return _projmanagerid; }
        }
        /// <summary>
        /// 项目经理历史
        /// </summary>
        public string ProjManagerHistory
        {
            set { _projmanagerhistory = value; }
            get { return _projmanagerhistory; }
        }
        /// <summary>
        /// 备注
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
        /// 操作人ID
        /// </summary>
        public int OperaId
        {
            set { _operaid = value; }
            get { return _operaid; }
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
        public DateTime OperaTime
        {
            set { _operatime = value; }
            get { return _operatime; }
        }
        #endregion Model

    }
}

