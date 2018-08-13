using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using SCZM.Common;
using SCZM.WebUI;

namespace SCZM.Web.Ashx.System
{
    /// <summary>
    /// sys_Config 的摘要说明


    /// </summary>
    public class sys_Config : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            string btn = RequestHelper.GetQueryString("Btn");
            string error = BaseWeb.CheckUserBtnPower();
            if (error != "")
            {
                context.Response.Write(error);
                return;
            }
            //取得处事类型
            string action = RequestHelper.GetQueryString("action");

            switch (action)
            {

                case "GetData": //获取配置信息
                    GetData(context, btn);
                    break;
                case "SaveData": //保存配置信息
                    SaveData(context, btn);
                    break;

            }
        }

        #region 获取配置信息==============================
        private void GetData(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                BLL.System.sys_Config bll = new BLL.System.sys_Config();


                Model.System.sys_Config model = bll.loadConfig();
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":");
                
                jsonStr.Append("{\"webname\":\"" + model.webname + "\",\"webversiontime\":\"" + model.webversiontime + "\",\"webinsideurl\":\"" + model.webinsideurl + "\",\"weburl\":\"" + model.weburl + "\",\"weblogo\":\"" + model.weblogo + "\",\"webcompany\":\"" + model.webcompany + "\",\"webaddress\":\"" + model.webaddress + "\",\"webtel\":\"" + model.webtel + "\",\"webfax\":\"" + model.webfax + "\",\"webcrod\":\"" + model.webcrod + "\"");
                jsonStr.Append(",\"mobilestatus\":" + model.mobilestatus.ToString() + ",\"mobiledomain\":\"" + model.mobiledomain + "\",\"logstatus\":" + model.logstatus.ToString() + ",\"webstatus\":" + model.webstatus + ",\"webclosereason\":\"" + model.webclosereason + "\"");
                jsonStr.Append(",\"smsapiurl\":\"" + model.smsapiurl + "\",\"smsusername\":\"" + model.smsusername + "\",\"smspassword\":\"" + DESEncrypt.Encrypt(model.smspassword,model.sysencryptstring) + "\",\"smsnickname\":\"" + model.smsnickname + "\"");
                jsonStr.Append(",\"emailsmtp\":\"" + model.emailsmtp + "\",\"emailport\":" + model.emailport.ToString() + ",\"emailfrom\":\"" + model.emailfrom + "\",\"emailusername\":\"" + model.emailusername + "\",\"emailpassword\":\"" + DESEncrypt.Encrypt(model.emailpassword, model.sysencryptstring) + "\",\"emailnickname\":\"" + model.emailnickname + "\"");
                jsonStr.Append(",\"fileextension\":\"" + model.fileextension + "\",\"attachsize\":" + model.attachsize.ToString() + ",\"imgsize\":" + model.imgsize.ToString() + ",\"imgmaxheight\":" + model.imgmaxheight.ToString() + ",\"imgmaxwidth\":" + model.imgmaxwidth.ToString() + ",\"thumbnailheight\":" + model.thumbnailheight.ToString() + ",\"thumbnailwidth\":" + model.thumbnailwidth.ToString() + ",\"watermarktype\":" + model.watermarktype.ToString() + ",\"watermarkposition\":" + model.watermarkposition.ToString() + ",\"watermarkimgquality\":" + model.watermarkimgquality.ToString() + "");
                jsonStr.Append(",\"watermarkpic\":\"" + model.watermarkpic + "\",\"watermarktransparency\":" + model.watermarktransparency.ToString() + ",\"watermarktext\":\"" + model.watermarktext + "\",\"watermarkfont\":\"" + model.watermarkfont + "\",\"watermarkfontsize\":" + model.watermarkfontsize.ToString());
                jsonStr.Append(",\"smscount\":\"" + GetSmsCount() + "\"");
                jsonStr.Append(",\"wxcorpid\":\"" + model.WxCorpid + "\",\"wxintentioncorpsecret\":\"" + model.WxIntentionCorpSecret + "\",\"wxschedulecorpsecret\":\"" + model.WxScheduleCorpSecret + "\",\"wxschedulesearchcorpsecret\":\"" + model.WxscheduleSearchCorpSecret + "\",\"wxschedulerewardcorpsecret\":\"" + model.WxscheduleRewardCorpSecret + "\",\"wxschedulerewardmanagercorpsecret\":\"" + model.WxscheduleRewardManagerCorpSecret + "\"");
                jsonStr.Append("}}");
                context.Response.Write(jsonStr.ToString());
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        #endregion
        #region 保存配置信息==============================
        private void SaveData(HttpContext context, string btn)
        {
            if (btn != "btnSave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string id = RequestHelper.GetString("id");
            BLL.System.sys_Config bll = new BLL.System.sys_Config();
            Model.System.sys_Config model = bll.loadConfig();

            model.webname = RequestHelper.GetString("webname");
            model.webversiontime = RequestHelper.GetString("webversiontime");
            model.webinsideurl = RequestHelper.GetString("webinsideurl");
            model.weburl = RequestHelper.GetString("weburl");
            model.webcrod = RequestHelper.GetString("webcrod");
            model.webcompany = RequestHelper.GetString("webcompany");
            model.webaddress = RequestHelper.GetString("webaddress");
            model.webtel = RequestHelper.GetString("webtel");
            model.webfax = RequestHelper.GetString("webfax");

            model.mobilestatus =RequestHelper.GetInt("mobilestatus",0);
            model.logstatus = RequestHelper.GetInt("logstatus", 1);
            model.webstatus = RequestHelper.GetInt("webstatus", 1);
            model.webclosereason = RequestHelper.GetString("webclosereason");

            model.smsapiurl = RequestHelper.GetString("smsapiurl");
            model.smsusername = RequestHelper.GetString("smsusername");
            if (DESEncrypt.Encrypt(model.smspassword,model.sysencryptstring) != RequestHelper.GetString("smspassword"))
            {
                model.smspassword = RequestHelper.GetString("smspassword");
            }
            model.smsnickname = RequestHelper.GetString("smsnickname");

            model.emailsmtp = RequestHelper.GetString("emailsmtp");
            model.emailport = RequestHelper.GetInt("emailport",25);
            model.emailfrom = RequestHelper.GetString("emailfrom");
            model.emailusername = RequestHelper.GetString("emailusername");
            if (DESEncrypt.Encrypt(model.emailpassword,model.sysencryptstring) != RequestHelper.GetString("emailpassword"))
            {
                model.emailpassword = RequestHelper.GetString("emailpassword");
            }
            model.emailnickname = RequestHelper.GetString("emailnickname");


            model.fileextension = RequestHelper.GetString("fileextension");
            model.attachsize = RequestHelper.GetInt("attachsize", 0);
            model.imgsize = RequestHelper.GetInt("imgsize", 0);
            model.imgmaxwidth = RequestHelper.GetInt("imgmaxwidth", 0);
            model.imgmaxheight = RequestHelper.GetInt("imgmaxheight", 0);
            model.thumbnailwidth = RequestHelper.GetInt("thumbnailwidth", 0);
            model.thumbnailheight = RequestHelper.GetInt("thumbnailheight", 0);
            model.watermarktype = RequestHelper.GetInt("watermarktype", 0);

            model.watermarkposition = RequestHelper.GetInt("watermarkposition", 5);
            model.watermarkimgquality = RequestHelper.GetInt("watermarkimgquality", 80);
            model.watermarkpic = RequestHelper.GetString("watermarkpic");
            model.watermarktransparency = RequestHelper.GetInt("watermarktransparency", 5);
            model.watermarktext = RequestHelper.GetString("watermarktext");
            model.watermarkfont = RequestHelper.GetString("watermarkfont");
            model.watermarkfontsize = RequestHelper.GetInt("watermarkfontsize", 12);

            model.WxCorpid = RequestHelper.GetString("wxcorpid");
            model.WxIntentionCorpSecret = RequestHelper.GetString("wxintentioncorpsecret");
            model.WxScheduleCorpSecret = RequestHelper.GetString("wxschedulecorpsecret");
            model.WxscheduleSearchCorpSecret = RequestHelper.GetString("wxschedulesearchcorpsecret");
            model.WxscheduleRewardCorpSecret = RequestHelper.GetString("wxschedulerewardcorpsecret");
            model.WxscheduleRewardManagerCorpSecret = RequestHelper.GetString("wxschedulerewardmanagercorpsecret");

            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {

                bll.saveConifg(model);
                status = "1";
                operaAction = Enums.ActionEnum.Edit.ToString();
                operaMemo = "修改系统配置";


                //写入操作日志
                BaseWeb.AddOpera(loginUserModel, int.Parse(RequestHelper.GetQueryString("MenuId")), operaAction, operaMemo);

                context.Response.Write("{\"status\":\"" + status + "\",\"msg\":\"修改成功！\"}");
                return;
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        #endregion
        #region 获取短信数量=================================
        private string GetSmsCount()
        {
            string code = string.Empty;
            int count = new BLL.System.sys_SMS().GetAccountQuantity(out code);
            if (code == "115")
            {
                return "查询出错：请完善账户信息";
            }
            else if (code == "-1")
            {
                return "账号未注册";
            }
            else if (code == "-2")
            {
                return "帐号或密码错误";
            }
            else if (code == "-3")
            {
                return "其他错误";
            }
            return count + " 条";
        }
        #endregion
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}