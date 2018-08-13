using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Text;
using System.Text.RegularExpressions;
using SCZM.Common;
using SCZM.WebUI;
using LitJson;

namespace SCZM.Web.Ashx.System
{
    /// <summary>
    /// sys_Upload 的摘要说明


    /// </summary>
    public class sys_Upload : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string btn = RequestHelper.GetQueryString("Btn");
            string error = BaseWeb.CheckUserBtnPower();
            if (error != "")
            {
                showError(context, "请重新登录！");
                return;
            }
            //取得处事类型
            string action = RequestHelper.GetQueryString("action");
            
            switch (action)
            {
                case "GetList": //获取上传文件日志列表
                    GetList(context, btn);
                    break;

                case "DelData": //删除上传文件日志信息
                    DelData(context, btn);
                    break;
                case "EditorFile": //编辑器文件


                    EditorFile(context);
                    break;
                case "ManagerFile": //管理文件
                    ManagerFile(context);
                    break;
                default: //普通上传


                    UpLoadFile(context);
                    break;

            }

        }
        #region 获取上传文件日志列表==============================
        private void GetList(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string fileName = RequestHelper.GetString("fileName");
            string fileUse = RequestHelper.GetString("fileUse");
            string operaName = RequestHelper.GetString("operaName");
            string beginDate = RequestHelper.GetString("beginDate");
            string endDate = RequestHelper.GetString("endDate");

            StringBuilder strWhere = new StringBuilder();
            strWhere.Append("Source=1 and ");
            List<SqlParameter> parameterList = new List<SqlParameter>();
            SqlParameter tempParameter = new SqlParameter();

            if (fileName != "")
            {
                strWhere.Append("FileName like '%' + @FileName + '%' and ");
                tempParameter = new SqlParameter("@FileName", SqlDbType.NVarChar);
                tempParameter.Value = fileName;
                parameterList.Add(tempParameter);
            }
            if (fileUse != "")
            {
                strWhere.Append("FilePath like '%' + @FilePath + '%' and ");
                tempParameter = new SqlParameter("@FilePath", SqlDbType.NVarChar);
                tempParameter.Value = fileUse;
                parameterList.Add(tempParameter);
            }

            if (operaName != "")
            {
                strWhere.Append("OperaName like '%' + @OperaName + '%' and ");
                tempParameter = new SqlParameter("@OperaName", SqlDbType.NVarChar);
                tempParameter.Value = operaName;
                parameterList.Add(tempParameter);
            }
            if (beginDate != "")
            {
                strWhere.Append("OperaTime >= @BeginOperTime and ");
                tempParameter = new SqlParameter("@BeginOperTime", SqlDbType.DateTime);
                tempParameter.Value = DateTime.Parse(beginDate);
                parameterList.Add(tempParameter);
            }
            if (endDate != "")
            {
                strWhere.Append("OperaTime <= @EndOperTime and ");
                tempParameter = new SqlParameter("@EndOperTime", SqlDbType.DateTime);
                tempParameter.Value = DateTime.Parse(endDate + " 23:59:59");
                parameterList.Add(tempParameter);
            }

            BLL.System.sys_Attachment bll = new BLL.System.sys_Attachment();
            try
            {
                DataTable dt = bll.GetList(Utils.DelLastChar(strWhere.ToString(), " and "), parameterList).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":");
                jsonStr.Append("{\"total\":" + dt.Rows.Count + ",\"rows\":");
                jsonStr.Append(rowsStr);
                jsonStr.Append("}}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        #endregion

        #region 删除上传文件日志信息==============================
        private void DelData(HttpContext context, string btn)
        {
            if (btn != "btnDel")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }



            BLL.System.sys_Attachment bll = new BLL.System.sys_Attachment();

            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            //string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                UpLoad upFiles = new UpLoad();
                DataTable dt = bll.GetList("Source=1 and UseId=0").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        upFiles.fileDel(dt.Rows[i]["FilePath"].ToString());
                    }
                }
                bll.Delete("Source=1 and UseId=0");
                
                //status = "1";
                operaAction = Enums.ActionEnum.Delete.ToString();
                operaMemo = "清理上传文件" ;

                //写入操作日志
                BaseWeb.AddOpera(loginUserModel, int.Parse(RequestHelper.GetQueryString("MenuId")), operaAction, operaMemo);

                context.Response.Write("{\"status\":\"1\",\"msg\":\"清理成功！\"}");
                return;

            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }

        }
        #endregion
        #region 上传文件处理===================================
        private void UpLoadFile(HttpContext context)
        {
            string _delfile = RequestHelper.GetString("DelFilePath");
            HttpPostedFile _upfile = context.Request.Files["File"];
            bool _iswater = false; //默认不打水印
            bool _isthumbnail = false; //默认不生成缩略图

            if (RequestHelper.GetQueryString("IsWater") == "1")
                _iswater = true;
            if (RequestHelper.GetQueryString("IsThumbnail") == "1")
                _isthumbnail = true;
            if (_upfile == null)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"请选择要上传文件！\"}");
                return;
            }
            string upLoadPath = RequestHelper.GetQueryString("UpLoadPath");
            string fileType = "";
            if (RequestHelper.GetQueryString("FileType") != null)
            {
                fileType = RequestHelper.GetQueryString("FileType");
            }
            UpLoad upFiles = new UpLoad();
            string remsg = upFiles.fileSaveAs(_upfile, upLoadPath, _isthumbnail, _iswater, fileType);
            //删除已存在的旧文件


            if (!string.IsNullOrEmpty(_delfile))
            {
                Utils.DeleteUpFile(_delfile);
            }

            ResponseInfo(context, remsg);

            ////返回成功信息
            //context.Response.Write(remsg);
            //context.Response.End();
        }
        #endregion
        

        #region 编辑器上传处理===================================
        private void EditorFile(HttpContext context)
        {
            bool _iswater = false; //默认不打水印
            if (context.Request.QueryString["IsWater"] == "1")
                _iswater = true;
            HttpPostedFile imgFile = context.Request.Files["imgFile"];
            string upLoadPath = RequestHelper.GetQueryString("UpLoadPath");
            if (imgFile == null)
            {
                showError(context, "请选择要上传文件！");
                return;
            }
            UpLoad upFiles = new UpLoad();
            string remsg = upFiles.fileSaveAs(imgFile,upLoadPath, false, _iswater,"");

            ResponseInfo(context, remsg);
            
        }
        private void ResponseInfo(HttpContext context,string remsg)
        {
            JsonData jd = JsonMapper.ToObject(remsg);
            string status = jd["status"].ToString();
            string msg = jd["msg"].ToString();
            if (status == "0")
            {
                showError(context, msg);
                return;
            }


            string filePath = jd["path"].ToString(); //取得上传后的路径
            string fileName = jd["name"].ToString(); //取得上传的文件名

            Model.System.sys_Attachment model = new Model.System.sys_Attachment();
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            model.Source = 1;
            model.FileName = fileName;
            model.FilePath = filePath;
            model.FileUse = "";
            model.UseId = 0;
            model.OperaName = loginUserModel.PerName;
            model.OperaTime = DateTime.Now;
            string operaMessage = "";
            BLL.System.sys_Attachment bll = new BLL.System.sys_Attachment();
            int fileId = bll.Add(model, out operaMessage);


            Hashtable hash = new Hashtable();
            hash["error"] = 0;
            hash["url"] = filePath;
            hash["fileName"] = fileName;
            hash["fileId"] = fileId;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(hash));
            context.Response.End();
        }
        //显示错误
        private void showError(HttpContext context, string message)
        {
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(hash));
            context.Response.End();
        }
        #endregion

        #region 浏览文件处理=====================================
        private void ManagerFile(HttpContext context)
        {
            //Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig();
            //String aspxUrl = context.Request.Path.Substring(0, context.Request.Path.LastIndexOf("/") + 1);

            //根目录路径，相对路径

            String rootPath = Utils.GetAppSettingValue("appName") +"UpLoad/"; //站点目录+上传目录
            //根目录URL，可以指定绝对路径，比如 http://www.yoursite.com/attached/
            String rootUrl = Utils.GetAppSettingValue("appName") + "UpLoad/";
            //图片扩展名


            String fileTypes = "gif,jpg,jpeg,png,bmp";

            String currentPath = "";
            String currentUrl = "";
            String currentDirPath = "";
            String moveupDirPath = "";

            String dirPath = Utils.GetMapPath(rootPath);
            String dirName = context.Request.QueryString["dir"];

            //根据path参数，设置各路径和URL
            String path = context.Request.QueryString["path"];
            path = String.IsNullOrEmpty(path) ? "" : path;
            if (path == "")
            {
                currentPath = dirPath;
                currentUrl = rootUrl;
                currentDirPath = "";
                moveupDirPath = "";
            }
            else
            {
                currentPath = dirPath + path;
                currentUrl = rootUrl + path;
                currentDirPath = path;
                moveupDirPath = Regex.Replace(currentDirPath, @"(.*?)[^\/]+\/$", "$1");
            }

            //排序形式，name or size or type
            String order = context.Request.QueryString["order"];
            order = String.IsNullOrEmpty(order) ? "" : order.ToLower();

            //不允许使用..移动到上一级目录


            if (Regex.IsMatch(path, @"\.\."))
            {
                context.Response.Write("Access is not allowed.");
                context.Response.End();
            }
            //最后一个字符不是/
            if (path != "" && !path.EndsWith("/"))
            {
                context.Response.Write("Parameter is not valid.");
                context.Response.End();
            }
            //目录不存在或不是目录
            if (!Directory.Exists(currentPath))
            {
                context.Response.Write("Directory does not exist.");
                context.Response.End();
            }

            //遍历目录取得文件信息
            string[] dirList = Directory.GetDirectories(currentPath);
            string[] fileList = Directory.GetFiles(currentPath);

            switch (order)
            {
                case "size":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new SizeSorter());
                    break;
                case "type":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new TypeSorter());
                    break;
                case "name":
                default:
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new NameSorter());
                    break;
            }

            Hashtable result = new Hashtable();
            result["moveup_dir_path"] = moveupDirPath;
            result["current_dir_path"] = currentDirPath;
            result["current_url"] = currentUrl;
            result["total_count"] = dirList.Length + fileList.Length;
            List<Hashtable> dirFileList = new List<Hashtable>();
            result["file_list"] = dirFileList;
            for (int i = 0; i < dirList.Length; i++)
            {
                DirectoryInfo dir = new DirectoryInfo(dirList[i]);
                Hashtable hash = new Hashtable();
                hash["is_dir"] = true;
                hash["has_file"] = (dir.GetFileSystemInfos().Length > 0);
                hash["filesize"] = 0;
                hash["is_photo"] = false;
                hash["filetype"] = "";
                hash["filename"] = dir.Name;
                hash["datetime"] = dir.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }
            for (int i = 0; i < fileList.Length; i++)
            {
                FileInfo file = new FileInfo(fileList[i]);
                Hashtable hash = new Hashtable();
                hash["is_dir"] = false;
                hash["has_file"] = false;
                hash["filesize"] = file.Length;
                hash["is_photo"] = (Array.IndexOf(fileTypes.Split(','), file.Extension.Substring(1).ToLower()) >= 0);
                hash["filetype"] = file.Extension.Substring(1);
                hash["filename"] = file.Name;
                hash["datetime"] = file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }
            context.Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(result));
            context.Response.End();
        }

        #region Helper
        public class NameSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.FullName.CompareTo(yInfo.FullName);
            }
        }

        public class SizeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.Length.CompareTo(yInfo.Length);
            }
        }

        public class TypeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.Extension.CompareTo(yInfo.Extension);
            }
        }
        #endregion
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