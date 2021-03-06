﻿using System;
namespace SCZM.Model.Base
{
    /// <summary>
    /// 实体类base_PartnerType 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class base_PartnerType
    {
        public base_PartnerType()
        { }
        #region Model
        private int _id;
        private string _partnertypename;
        private string _memo;
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
        /// 合作单位类别
        /// </summary>
        public string PartnerTypeName
        {
            set { _partnertypename = value; }
            get { return _partnertypename; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
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

