﻿using System;
namespace SCZM.Model.Base
{
    /// <summary>
    /// 实体类base_SettlementType 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class base_SettlementType
    {
        public base_SettlementType()
        { }
        #region Model
        private int _id;
        private string _settlementtypename;
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
        /// 结算类型
        /// </summary>
        public string SettlementTypeName
        {
            set { _settlementtypename = value; }
            get { return _settlementtypename; }
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

