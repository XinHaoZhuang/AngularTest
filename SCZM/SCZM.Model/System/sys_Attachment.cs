using System;
namespace SCZM.Model.System
{
    /// <summary>
    /// 实体类sys_Attachment 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class sys_Attachment
    {
        public sys_Attachment()
        { }
        #region Model
        private int _id;
        private int? _source = 0;
        private string _filename;
        private string _filepath;
        private string _fileuse;
        private int? _useid = 0;
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
        /// 文件来源 1上传2系统生成
        /// </summary>
        public int? Source
        {
            set { _source = value; }
            get { return _source; }
        }
        /// <summary>
        /// 文件名

        /// </summary>
        public string FileName
        {
            set { _filename = value; }
            get { return _filename; }
        }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath
        {
            set { _filepath = value; }
            get { return _filepath; }
        }
        /// <summary>
        /// 用途

        /// </summary>
        public string FileUse
        {
            set { _fileuse = value; }
            get { return _fileuse; }
        }
        /// <summary>
        /// 用途ID
        /// </summary>
        public int? UseId
        {
            set { _useid = value; }
            get { return _useid; }
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

