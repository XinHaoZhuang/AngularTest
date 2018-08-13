using System;
using System.Collections.Generic;
namespace SCZM.Model.System
{
    /// <summary>
    /// 实体类sys_Bulletin 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class sys_Bulletin
    {
        public sys_Bulletin()
        { }
        #region Model
        private int _id;
        private string _bulletinname;
        private string _receivedepid;
        private string _receivedepname;
        private string _bulletincontent;
        private string _attachment;
        private int _flagtop = 0;
        private int _billstate = 0;
        private bool _flagdel = false;
        private int? _operaid;
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
        /// 公告主题
        /// </summary>
        public string BulletinName
        {
            set { _bulletinname = value; }
            get { return _bulletinname; }
        }
        /// <summary>
        /// 接收部门ID列表
        /// </summary>
        public string ReceiveDepId
        {
            set { _receivedepid = value; }
            get { return _receivedepid; }
        }
        /// <summary>
        /// 接收部门名称列表
        /// </summary>
        public string ReceiveDepName
        {
            set { _receivedepname = value; }
            get { return _receivedepname; }
        }
        /// <summary>
        /// 公告内容
        /// </summary>
        public string BulletinContent
        {
            set { _bulletincontent = value; }
            get { return _bulletincontent; }
        }
        /// <summary>
        /// 附件列表
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
        }
        /// <summary>
        /// 是否制定
        /// </summary>
        public int FlagTop
        {
            set { _flagtop = value; }
            get { return _flagtop; }
        }
        /// <summary>
        /// 公告状态 0草稿 1已发布

        /// </summary>
        public int BillState
        {
            set { _billstate = value; }
            get { return _billstate; }
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
        /// 最终操作人ID
        /// </summary>
        public int? OperaId
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
        public DateTime? OperaTime
        {
            set { _operatime = value; }
            get { return _operatime; }
        }
        #endregion Model

        private List<sys_BulletinReceiveDep> _sys_bulletinreceivedeps;
        /// <summary>
        /// 子类 
        /// </summary>
        public List<sys_BulletinReceiveDep> sys_BulletinReceiveDeps
        {
            set { _sys_bulletinreceivedeps = value; }
            get { return _sys_bulletinreceivedeps; }
        }

    }
    /// <summary>
    /// 实体类sys_BulletinReceiveDep 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class sys_BulletinReceiveDep
    {
        public sys_BulletinReceiveDep()
        { }
        #region Model
        private int _id;
        private int _bulletinid;
        private int _receivedepid;
        private bool _flagdel = false;
        /// <summary>
        /// 流水号

        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int BulletinId
        {
            set { _bulletinid = value; }
            get { return _bulletinid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ReceiveDepId
        {
            set { _receivedepid = value; }
            get { return _receivedepid; }
        }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool FlagDel
        {
            set { _flagdel = value; }
            get { return _flagdel; }
        }
        #endregion Model

    }
}

