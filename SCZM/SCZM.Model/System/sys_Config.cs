using System;
using System.Collections.Generic;
using System.Text;


namespace SCZM.Model.System
{
    /// <summary>
    /// 站点配置实体类

    /// </summary>
    [Serializable]
    public class sys_Config
    {
        public sys_Config()
        { }
        private string _webname = "";
        private string _webversiontime = "";
        private string _webinsideurl = "";
        private string _weburl = "";
        private string _weblogo = "";
        private string _webcompany = "";
        private string _webaddress = "";
        private string _webtel = "";
        private string _webfax = "";
        private string _webmail = "";
        private string _webcrod = "";
        

        private int _mobilestatus = 1;
        private string _mobiledomain = "";
        private int _logstatus = 0;
        private int _webstatus = 1;
        private string _webclosereason = "";


        private string _smsapiurl = "";
        private string _smsusername = "";
        private string _smspassword = "";
        private string _smsnickname = "";

        private string _emailsmtp = "";
        private int _emailport = 25;
        private string _emailfrom = "";
        private string _emailusername = "";
        private string _emailpassword = "";
        private string _emailnickname = "";

        private string _fileextension = "";
        private int _attachsize = 0;
        private int _imgsize = 0;
        private int _imgmaxheight = 0;
        private int _imgmaxwidth = 0;
        private int _thumbnailheight = 0;
        private int _thumbnailwidth = 0;
        private int _watermarktype = 0;
        private int _watermarkposition = 9;
        private int _watermarkimgquality = 80;
        private string _watermarkpic = "";
        private int _watermarktransparency = 10;
        private string _watermarktext = "";
        private string _watermarkfont = "";
        private int _watermarkfontsize = 12;
        private string _wxcorpid = "";

        
        private string _wxintentioncorpsecret = "";

        
        private string _wxschedulecorpsecret = "";

        private string _wxschedulesearchcorpsecret = "";

        private string _wxschedulerewardcorpsecret = "";

        private string _wxschedulerewardmanagercorpsecret = "";

        

       

        private string _sysencryptstring = "KeXinDa";


        #region 网站基本信息==================================
        /// <summary>
        /// 网站名称
        /// </summary>
        public string webname
        {
            get { return _webname; }
            set { _webname = value; }
        }
        /// <summary>
        /// 版本发布日期
        /// </summary>
        public string webversiontime
        {
            get { return _webversiontime; }
            set { _webversiontime = value; }
        }
        /// <summary>
        /// 内网地址
        /// </summary>
        public string webinsideurl
        {
            get { return _webinsideurl; }
            set { _webinsideurl = value; }
        }
        /// <summary>
        /// 网站域名
        /// </summary>
        public string weburl
        {
            get { return _weburl; }
            set { _weburl = value; }
        }
        /// <summary>
        /// 网站LOGO
        /// </summary>
        public string weblogo
        {
            get { return _weblogo; }
            set { _weblogo = value; }
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string webcompany
        {
            get { return _webcompany; }
            set { _webcompany = value; }
        }
        /// <summary>
        /// 通讯地址
        /// </summary>
        public string webaddress
        {
            get { return _webaddress; }
            set { _webaddress = value; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string webtel
        {
            get { return _webtel; }
            set { _webtel = value; }
        }
        /// <summary>
        /// 传真号码
        /// </summary>
        public string webfax
        {
            get { return _webfax; }
            set { _webfax = value; }
        }
        /// <summary>
        /// 管理员邮箱

        /// </summary>
        public string webmail
        {
            get { return _webmail; }
            set { _webmail = value; }
        }
        /// <summary>
        /// 网站备案号

        /// </summary>
        public string webcrod
        {
            get { return _webcrod; }
            set { _webcrod = value; }
        }
        
        #endregion

        #region 功能权限设置==================================

        /// <summary>
        /// 手机网站状态0关闭1开启

        /// </summary>
        public int mobilestatus
        {
            get { return _mobilestatus; }
            set { _mobilestatus = value; }
        }
        /// <summary>
        /// 手机网站绑定域名
        /// </summary>
        public string mobiledomain
        {
            get { return _mobiledomain; }
            set { _mobiledomain = value; }
        }
        /// <summary>
        /// 后台管理日志
        /// </summary>
        public int logstatus
        {
            get { return _logstatus; }
            set { _logstatus = value; }
        }
        /// <summary>
        /// 是否关闭网站
        /// </summary>
        public int webstatus
        {
            get { return _webstatus; }
            set { _webstatus = value; }
        }
        /// <summary>
        /// 关闭原因描述
        /// </summary>
        public string webclosereason
        {
            get { return _webclosereason; }
            set { _webclosereason = value; }
        }

        #endregion

        #region 短信平台设置==================================
        /// <summary>
        /// 短信API地址
        /// </summary>
        public string smsapiurl
        {
            get { return _smsapiurl; }
            set { _smsapiurl = value; }
        }
        /// <summary>
        /// 短信平台登录账户名

        /// </summary>
        public string smsusername
        {
            get { return _smsusername; }
            set { _smsusername = value; }
        }
        /// <summary>
        /// 短信平台登录密码
        /// </summary>
        public string smspassword
        {
            get { return _smspassword; }
            set { _smspassword = value; }
        }
        /// <summary>
        /// 手机短信签名
        /// </summary>
        public string smsnickname
        {
            get { return _smsnickname; }
            set { _smsnickname = value; }
        }
        #endregion

        #region 邮件发送设置==================================
        /// <summary>
        /// STMP服务器

        /// </summary>
        public string emailsmtp
        {
            get { return _emailsmtp; }
            set { _emailsmtp = value; }
        }
        /// <summary>
        /// SMTP端口
        /// </summary>
        public int emailport
        {
            get { return _emailport; }
            set { _emailport = value; }
        }
        /// <summary>
        /// 发件人地址
        /// </summary>
        public string emailfrom
        {
            get { return _emailfrom; }
            set { _emailfrom = value; }
        }
        /// <summary>
        /// 邮箱账号
        /// </summary>
        public string emailusername
        {
            get { return _emailusername; }
            set { _emailusername = value; }
        }
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public string emailpassword
        {
            get { return _emailpassword; }
            set { _emailpassword = value; }
        }
        /// <summary>
        /// 发件人昵称

        /// </summary>
        public string emailnickname
        {
            get { return _emailnickname; }
            set { _emailnickname = value; }
        }
        #endregion

        #region 文件上传设置==================================

        /// <summary>
        /// 附件上传类型
        /// </summary>
        public string fileextension
        {
            get { return _fileextension; }
            set { _fileextension = value; }
        }
        /// <summary>
        /// 文件上传大小
        /// </summary>
        public int attachsize
        {
            get { return _attachsize; }
            set { _attachsize = value; }
        }
        /// <summary>
        /// 图片上传大小
        /// </summary>
        public int imgsize
        {
            get { return _imgsize; }
            set { _imgsize = value; }
        }
        /// <summary>
        /// 图片最大高度(像素)
        /// </summary>
        public int imgmaxheight
        {
            get { return _imgmaxheight; }
            set { _imgmaxheight = value; }
        }
        /// <summary>
        /// 图片最大宽度(像素)
        /// </summary>
        public int imgmaxwidth
        {
            get { return _imgmaxwidth; }
            set { _imgmaxwidth = value; }
        }
        /// <summary>
        /// 生成缩略图高度(像素)
        /// </summary>
        public int thumbnailheight
        {
            get { return _thumbnailheight; }
            set { _thumbnailheight = value; }
        }
        /// <summary>
        /// 生成缩略图宽度(像素)
        /// </summary>
        public int thumbnailwidth
        {
            get { return _thumbnailwidth; }
            set { _thumbnailwidth = value; }
        }
        /// <summary>
        /// 图片水印类型
        /// </summary>
        public int watermarktype
        {
            get { return _watermarktype; }
            set { _watermarktype = value; }
        }
        /// <summary>
        /// 图片水印位置
        /// </summary>
        public int watermarkposition
        {
            get { return _watermarkposition; }
            set { _watermarkposition = value; }
        }
        /// <summary>
        /// 图片生成质量
        /// </summary>
        public int watermarkimgquality
        {
            get { return _watermarkimgquality; }
            set { _watermarkimgquality = value; }
        }
        /// <summary>
        /// 图片水印文件
        /// </summary>
        public string watermarkpic
        {
            get { return _watermarkpic; }
            set { _watermarkpic = value; }
        }
        /// <summary>
        /// 水印透明度

        /// </summary>
        public int watermarktransparency
        {
            get { return _watermarktransparency; }
            set { _watermarktransparency = value; }
        }
        /// <summary>
        /// 水印文字
        /// </summary>
        public string watermarktext
        {
            get { return _watermarktext; }
            set { _watermarktext = value; }
        }
        /// <summary>
        /// 文字字体
        /// </summary>
        public string watermarkfont
        {
            get { return _watermarkfont; }
            set { _watermarkfont = value; }
        }
        /// <summary>
        /// 文字大小(像素)
        /// </summary>
        public int watermarkfontsize
        {
            get { return _watermarkfontsize; }
            set { _watermarkfontsize = value; }
        }
        #endregion
        #region 安装初始化设置================================
        /// <summary>
        /// 加密字符串

        /// </summary>
        public string sysencryptstring
        {
            get { return _sysencryptstring; }
            set { _sysencryptstring = value; }
        }
        #endregion
        #region 微信
        /// <summary>
        /// 企业ID
        /// </summary>
        public string WxCorpid
        {
            get { return _wxcorpid; }
            set { _wxcorpid = value; }
        }
        /// <summary>
        /// 维修意向密钥
        /// </summary>
        public string WxIntentionCorpSecret
        {
            get { return _wxintentioncorpsecret; }
            set { _wxintentioncorpsecret = value; }
        }
        /// <summary>
        /// 进度反馈密钥
        /// </summary>
        public string WxScheduleCorpSecret
        {
            get { return _wxschedulecorpsecret; }
            set { _wxschedulecorpsecret = value; }
        }
        /// <summary>
        /// 进度查询密钥
        /// </summary>
        public string WxscheduleSearchCorpSecret
        {
            get { return _wxschedulesearchcorpsecret; }
            set { _wxschedulesearchcorpsecret = value; }
        }
        /// <summary>
        /// 维修奖励
        /// </summary>
        public string WxscheduleRewardCorpSecret
        {
            get { return _wxschedulerewardcorpsecret; }
            set { _wxschedulerewardcorpsecret = value; }
        }
        /// <summary>
        /// 维修奖励(管理)
        /// </summary>
        public string WxscheduleRewardManagerCorpSecret
        {
            get { return _wxschedulerewardmanagercorpsecret; }
            set { _wxschedulerewardmanagercorpsecret = value; }
        }
        #endregion
    }
}
