using System;
using System.Collections.Generic;
namespace SCZM.Model.System
{
    /// <summary>
    /// 实体类sys_Mail_Send 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class sys_Mail_Send
    {
        public sys_Mail_Send()
        { }
        #region Model
        private int _id;
        private string _mailname;
        private string _receiveperid;
        private string _receivepername;
        private string _mailcontent;
        private string _attachment;
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
        public string MailName
        {
            set { _mailname = value; }
            get { return _mailname; }
        }
        /// <summary>
        /// 接收部门ID列表
        /// </summary>
        public string ReceivePerId
        {
            set { _receiveperid = value; }
            get { return _receiveperid; }
        }
        /// <summary>
        /// 接收部门名称列表
        /// </summary>
        public string ReceivePerName
        {
            set { _receivepername = value; }
            get { return _receivepername; }
        }
        /// <summary>
        /// 公告内容
        /// </summary>
        public string MailContent
        {
            set { _mailcontent = value; }
            get { return _mailcontent; }
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

        private List<sys_Mail_Send_ReceivePerson> _sys_mail_send_receivepersons;
        /// <summary>
        /// 子类 
        /// </summary>
        public List<sys_Mail_Send_ReceivePerson> sys_Mail_Send_ReceivePersons
        {
            set { _sys_mail_send_receivepersons = value; }
            get { return _sys_mail_send_receivepersons; }
        }

    }
    /// <summary>
    /// 实体类sys_Mail_Send_ReceivePerson 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class sys_Mail_Send_ReceivePerson
    {
        public sys_Mail_Send_ReceivePerson()
        { }
        #region Model
        private int _id;
        private int _mailid;
        private int _receiveperid;
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
        public int MailId
        {
            set { _mailid = value; }
            get { return _mailid; }
        }
        /// <summary>
        /// 接收人员
        /// </summary>
        public int ReceivePerId
        {
            set { _receiveperid = value; }
            get { return _receiveperid; }
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

