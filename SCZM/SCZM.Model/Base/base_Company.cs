using System;
namespace SCZM.Model.Base
{
    /// <summary>
    /// 实体类base_Company 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class base_Company
    {
        public base_Company()
        { }
        #region Model
        private int _id;
        private string _companyname;
        private int _sortid;
        private bool _flagdel = false;
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
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 排序号
        /// </summary>
        public int SortId
        {
            set { _sortid = value; }
            get { return _sortid; }
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
        public DateTime OperaTime
        {
            set { _operatime = value; }
            get { return _operatime; }
        }
        #endregion Model

    }
}

