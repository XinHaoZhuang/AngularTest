using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SCZM.Common;
using SCZM.WebUI;

namespace SCZM.Web.Admin
{
    public partial class export : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string exportType = Request["txtExportType"];

            if (exportType == "1")
            {
                string error = BaseWeb.CheckUserBtnPower();
                if (error != "")
                {
                    return;
                }
                string attachmentId = Request["txtAttachmentId"];
                if (attachmentId == "")
                {
                    return;
                }
                BLL.System.sys_Attachment bll = new BLL.System.sys_Attachment();
                Model.System.sys_Attachment model = bll.GetModel(Utils.StrToInt(attachmentId,0));

                string filename = Utils.GetMapPath(model.FilePath);
                if (filename == "")
                {
                    return;
                }
                System.IO.FileInfo file = new System.IO.FileInfo(filename);
                Response.Clear();
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/x-bittorrent";
                Response.WriteFile(file.FullName);
                Response.End();  

            }
            else
            {
                string filename = Request["txtName"];
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "utf-8";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.AppendHeader("content-disposition", "attachment;filename=\"" + System.Web.HttpUtility.UrlEncode(System.Text.Encoding.UTF8.GetBytes(filename)) + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx\"");
                Response.ContentType = "Application/ms-excel";
                Response.Write("<html>\n<head>\n");
                Response.Write("<style type=\"text/css\">\n.pb{font-size:13px;border-collapse:collapse;} " +
                               "\n.pb th{font-weight:bold;text-align:center;border:0.5pt solid windowtext;padding:2px;} " +
                               "\n.pb td{border:0.5pt solid windowtext;padding:2px;}\n</style>\n</head>\n");
                Response.Write("<body>\n" + Request["txtContent"] + "\n</body>\n</html>");
                Response.Flush();
                Response.End();
            }
        }
    }
}