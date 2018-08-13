using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCZM.Common;
using SCZM.Model;
using SCZM.BLL;
using System.Web;
using System.Web.Security;

namespace SCZM.WebUI
{
    public class BaseWeb
    {
        /// <summary>
        /// 判断管理员是否已经登录(解决Session超时问题)
        /// </summary>
        public static bool IsManagerLogin()
        {
            //如果Session为Null
            
            if (HttpContext.Current.Session[Keys.SESSION_LoginUser] != null)
            {
                return true;
            }
            return false;

        }
        /// <summary>
        /// 取得登录session信息
        /// </summary>
        /// <returns></returns>
        public static Model.System.sys_LoginUser GetLoginInfo()
        {
            if (IsManagerLogin())
            {
                Model.System.sys_LoginUser loginUserModel = HttpContext.Current.Session[Keys.SESSION_LoginUser] as Model.System.sys_LoginUser;
                if (loginUserModel != null)
                {
                    return loginUserModel;
                }
            }
            return null;
        }
        /// <summary>
        /// 校验客户端LoginSalt
        /// </summary>
        /// <returns></returns>
        public static bool CheckLogin(string loginSalt)
        {
            bool result = false;
            Model.System.sys_LoginUser model = new Model.System.sys_LoginUser();
            model = GetLoginInfo();
            if (model != null)
            {
                if (model.Salt == loginSalt)
                {
                    result = true;
                }
            }
            return result;
            
        }

        /// <summary>
        /// 从登录session信息获取操作日志信息
        /// </summary>
        /// <returns></returns>
        public static Model.System.sys_OperaLog GetOperaModel()
        {
            Model.System.sys_OperaLog operaModel = new Model.System.sys_OperaLog();
            Model.System.sys_LoginUser LoginUserModel = GetLoginInfo();
            if (LoginUserModel != null)
            {
                operaModel.PerId = LoginUserModel.ID;
                operaModel.PerName = LoginUserModel.PerName;
                operaModel.PerAccount = LoginUserModel.Account;
                operaModel.OperaTime = DateTime.Now;
                operaModel.LoginIP = LoginUserModel.LoginIP;

            }
            return operaModel;

        }
        /// <summary>
        /// 从登录Model获取操作日志信息
        /// </summary>
        /// <returns></returns>
        public static Model.System.sys_OperaLog GetOperaModel(Model.System.sys_LoginUser LoginUserModel)
        {
            Model.System.sys_OperaLog operaModel = new Model.System.sys_OperaLog();
            if (LoginUserModel != null)
            {
                operaModel.PerId = LoginUserModel.ID;
                operaModel.PerName = LoginUserModel.PerName;
                operaModel.PerAccount = LoginUserModel.Account;
                operaModel.OperaTime = DateTime.Now;
                operaModel.LoginIP = LoginUserModel.LoginIP;

            }
            return operaModel;

        }
        /// <summary>
        /// 写入操作日志信息
        /// </summary>
        /// <returns></returns>
        public static void AddOpera(Model.System.sys_LoginUser loginUserModel, int menuId, string operaType, string memo)
        {
            Model.System.sys_Config configModel = new BLL.System.sys_Config().loadConfig();
            if (configModel.logstatus == 1)
            {
                Model.System.sys_OperaLog operaModel = new Model.System.sys_OperaLog();
                if (loginUserModel != null)
                {
                    operaModel.PerId = loginUserModel.ID;
                    operaModel.PerName = loginUserModel.PerName;
                    operaModel.PerAccount = loginUserModel.Account;
                    operaModel.MenuId = menuId;
                    operaModel.OperaType = operaType;
                    operaModel.Memo = memo;
                    operaModel.OperaTime = DateTime.Now;
                    operaModel.LoginIP = loginUserModel.LoginIP;
                    try
                    {
                        new BLL.System.sys_OperaLog().Add(operaModel);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }

        }
        /// <summary>
        /// 校验页面传入参数
        /// </summary>
        /// <param name="loginSalt">客户端Salt</param>
        /// <param name="menuId">页面ID</param>
        /// <param name="url">页面URL</param>
        /// <returns></returns>
        public static string CheckPageParam(string loginSalt, string menuId,string url,Model.System.sys_LoginUser loginUserModel)
        {
            string result = "";
            if (url == "")
            {
                result = "{\"status\":\"0.1\",\"msg\":\"非法传入页面！\"}";
                return result;
            }
            if (menuId == "")
            {
                result = "{\"status\":\"0.1\",\"msg\":\"菜单ID不能为空！\"}";
                return result;
            }
            if (loginSalt == "")
            {
                result = "{\"status\":\"0.1\",\"msg\":\"Salt不能为空！\"}";
                return result;
            }

            if (loginUserModel == null || loginUserModel.Salt != loginSalt)
            {
                result = "{\"status\":\"0.1\",\"msg\":\"对不起，登录超时，请重新登录！\"}";
                return result;
            }
            String domain = Utils.GetUrlDomain(url).ToLower();
            Model.System.sys_Config configModel = new BLL.System.sys_Config().loadConfig();
            //string[] domainArray = (configModel.webinsideurl + "," + configModel.weburl).Split(',');
            //if (domain != "wx.ssccm.cn:8080" || RequestHelper.GetIP() != "127.0.0.1")
            //{
            //    if (domain == "" || !domainArray.Contains(domain))
            //    {
            //        result = "{\"status\":\"0.1\",\"msg\":\"非法传入页面！\"}";
            //        return result;
            //    }
            //}
            
            return result;
        }
        /// <summary>
        /// 获取页面ID
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static int GetPageId(string menuId, string url)
        {
            int pageId = 0;
            BLL.System.sys_Menu menuBll = new BLL.System.sys_Menu();
            DataTable menuPageDT = menuBll.GetListByCache_sys_MenuPage().Tables[0];
            DataRow[] pageDR = menuPageDT.Select("MenuId=" + menuId + " and '" + url + "' like '%' + PageUrl +'%'");

            if (pageDR.Length > 0)
            {
                pageId = Utils.ObjToInt(pageDR[0]["PageId"],0);
            }
            return pageId;
        }


        /// <summary>
        /// 得到界面按钮列表 重新获取数据
        /// </summary>
        /// <param name="perId"></param>
        /// <param name="pageId"></param>
        /// <returns>string[] 包含两列 第一列为btn，第二列为0,1；0没有权限，1有权限</returns>
        public static List<string[]> GetPageElementList(int perId, int pageId,out string roleIdStr)
        {
            List<string[]> pageElementPowerList = new List<string[]>();
            string[] item;
            roleIdStr = "";
            if (pageId == 0)
            {
                return pageElementPowerList;
            }
            BLL.System.sys_Menu menuBll = new BLL.System.sys_Menu();
            DataTable menuPageElementDT = menuBll.GetListByCache_sys_MenuPageElement().Tables[0];
            DataRow[] powerDR = menuPageElementDT.Select("PageId=" + pageId);
            int powerCount = 0;

            for (int i = 0; i < powerDR.Length; i++)
            {
                string btn=powerDR[i]["ElementName"].ToString();
                item = new string[] { btn, "1" };
                if (CheckListContain(pageElementPowerList,item)==0)
                {    
                    powerCount= 0;
                    DataTable personRoleDT = new BLL.System.sys_Person().GetListByCache_sys_PersonRole(perId).Tables[0];
                    for (int j = 0; j < personRoleDT.Rows.Count; j++)
                    {
                        string tmpRoleIdstr=",Cache_sys_RolePower_" + personRoleDT.Rows[j]["RoleId"].ToString();
                        if (!(roleIdStr+",").Contains(tmpRoleIdstr + ","))
                        {
                            roleIdStr += tmpRoleIdstr;
                        }
                        if (powerCount == 0)
                        {
                            DataTable rolePowerDT = new BLL.System.sys_Role().GetListByCache_sys_RolePower(Utils.ObjToInt(personRoleDT.Rows[j]["RoleId"], 0)).Tables[0];
                            powerCount += rolePowerDT.Select("PowerId=" + powerDR[i]["PowerId"].ToString()).Length;
                        }
                    }
                    if (powerCount == 0)
                    {
                        DataTable personPowerDT = new BLL.System.sys_PersonPower().GetListByCache_sys_PersonPower(perId).Tables[0];
                        powerCount = personPowerDT.Select("PowerId=" + powerDR[i]["PowerId"].ToString()).Length;
                    }
                    item = new string[2] { btn, "0" };
                    int index = CheckListContain(pageElementPowerList, item);
                    if (index>0)
                    {
                        if (powerCount> 0)
                        {
                            pageElementPowerList.RemoveAt(index-1);
                            pageElementPowerList.Add(new string[] { btn ,"1"});
                        }
                    }
                    else
                    {
                        if (powerCount > 0)
                        {
                            pageElementPowerList.Add(new string[] { btn, "1" });
                        }
                        else
                        {
                            pageElementPowerList.Add(new string[] { btn, "0" });
                        }
                    }
                }
            }
            return pageElementPowerList;
        }

        /// <summary>
        /// 得到界面按钮列表，从缓存中。

        /// </summary>
        public static List<string[]> GetPageElementListByCache(int perId, int pageId)
        {

            string CacheKey = "Cache_PerPagePower_" + perId + "_" + pageId;
            object objList = SCZM.Common.DataCache.GetCache(CacheKey);
            if (objList == null)
            {
                try
                {
                    string roleIdStr;
                    objList = GetPageElementList(perId, pageId,out roleIdStr);
                    string cacheStr = "Cache_sys_PersonPower_" + perId + ",Cache_sys_PersonRole_" + perId + roleIdStr;

                    string[] cacheDependsArray = cacheStr.Split(',');
                    System.Web.Caching.CacheDependency cacheDepends = new System.Web.Caching.CacheDependency(null,cacheDependsArray);
                    System.Web.Caching.Cache objCache = HttpRuntime.Cache;
                    objCache.Insert(CacheKey, objList, cacheDepends, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(30));
                    //objCache.Insert(CacheKey, objList, cacheDepends, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
                }
                catch { }
            }
            return (List<string[]>)objList;
        }
        /// <summary>
        /// 校验用户按钮权限
        /// </summary>
        /// <returns></returns>
        public static string CheckUserBtnPower()
        {
            string loginSalt = RequestHelper.GetQueryString("LoginSalt");
            string menuId = RequestHelper.GetQueryString("MenuId");
            string url = RequestHelper.GetUrlReferrer();
            string btn = RequestHelper.GetQueryString("Btn");
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();

            string result = CheckPageParam(loginSalt,menuId,url,loginUserModel);
            if (result != "")
            {
                return result;
            }
            if (loginUserModel.IsAdmin == true)
            {
                return result;
            }
            try
            {
                
                #region 从数据库取权限方法

                    
                //BLL.System.sys_Person bll = new BLL.System.sys_Person();
                //if (bll.CheckUserBtnPower(Utils.StrToInt(menuId, 0), url, loginUserModel.ID, btn) == false)
                //{
                //    result = "{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}";
                //    return result;
                //}
                    
                #endregion

                //DateTime aa = DateTime.Now;
                #region 从缓存读权限
                int pageId = GetPageId(menuId, url);
                if (pageId == 0)
                {
                    result = "{\"status\":\"0.1\",\"msg\":\"非法传入页面！\"}";
                    return result;
                }
                    
                #region 从Cache_PerPagePower缓存读权限

                List<string[]> pageElementPowerList = GetPageElementListByCache(loginUserModel.ID, pageId);
                string[] item = new string[] { btn, "1" };
                if (CheckListContain(pageElementPowerList, item) == 0)
                {
                    result = "{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}";
                    return result;
                }

                #endregion
                #region 循环写法
                //int powerCount1 = 0;
                //int powerCount2 = 0;
                //BLL.System.sys_Menu menuBll = new BLL.System.sys_Menu();
                //DataTable menuPageDT = menuBll.GetListByCache_sys_MenuPage().Tables[0];
                //DataTable menuPageElementDT = menuBll.GetListByCache_sys_MenuPageElement().Tables[0];
                //int i = 0;
                //string pageIdStr = "";
                //string powerIdStr = "";

                //DataRow[] pageDR = menuPageDT.Select("MenuId=" + menuId + " and '" + url + "' like '%' + PageUrl +'%'");

                //if (pageDR.Length > 0)
                //{
                //    pageIdStr = "PageId=" + pageDR[0]["PageId"].ToString();
                //    DataRow[] powerDR = menuPageElementDT.Select(pageIdStr + " and ElementName='" + btn + "'");
                //    if (powerDR.Length > 0)
                //    {
                //        for (i = 0; i < powerDR.Length; i++)
                //        {
                //            powerIdStr += "PowerId=" + powerDR[i]["PowerId"].ToString() + " or ";
                //        }
                //        powerIdStr = "(" + powerIdStr.Substring(0, powerIdStr.Length - 4) + ")";

                //        DataTable personRoleDT = new BLL.System.sys_Person().GetListByCache_sys_PersonRole(loginUserModel.ID).Tables[0];
                //        if (personRoleDT.Rows.Count > 0)
                //        {
                //            DataTable rolePowerDT = null;
                //            powerCount1 = 0;
                //            for (i = 0; i < personRoleDT.Rows.Count; i++)
                //            {
                //                rolePowerDT = new BLL.System.sys_Role().GetListByCache_sys_RolePower(Utils.ObjToInt(personRoleDT.Rows[i]["RoleId"],0)).Tables[0];
                //                powerCount1 += rolePowerDT.Select(powerIdStr).Length;
                //            }
                                
                //        }
                //        if (powerCount1 == 0)
                //        {
                //            DataTable personPowerDT = new BLL.System.sys_PersonPower().GetListByCache_sys_PersonPower(loginUserModel.ID).Tables[0];
                //            powerCount2 = personPowerDT.Select(powerIdStr).Length;
                //        }
                //    }
                //}
                //if (powerCount1 + powerCount2 == 0)
                //{
                //    result = "{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}";
                //    return result;
                //}
                #endregion
                #region linq写法 速度不如循环写法快

                //var powerQuery = 
                //    from menuPage in menuPageDT.AsEnumerable()
                //    join menuPageElement in menuPageElementDT.AsEnumerable() on menuPage.Field<int>("PageId") equals menuPageElement.Field<int>("PageId")
                //    where menuPage.Field<int>("MenuId") == Utils.StrToInt(menuId, 0) && url.Contains(menuPage.Field<string>("PageUrl")) && menuPageElement.Field<string>("ElementName") == btn
                //    select menuPageElement.Field<int>("PowerId");
                //powerCount1 =(
                //    from personRole in personRoleDT.AsEnumerable()
                //    join rolePower in rolePowerDT.AsEnumerable() on personRole.Field<int>("RoleId") equals rolePower.Field<int>("RoleId")
                //    join powerId in powerQuery on rolePower.Field<int>("PowerId") equals powerId
                //    where personRole.Field<int>("PerId") == loginUserModel.ID
                //    select rolePower.Field<int>("PowerId")).Count<int>();
                    
                //if (powerCount1 == 0)
                //{
                //    powerCount2 = (
                //        from personPower in personPowerDT.AsEnumerable()
                //        join powerId in powerQuery on personPower.Field<int>("PowerId") equals powerId
                //        where personPower.Field<int>("PerId") == loginUserModel.ID
                //        select personPower.Field<int>("PowerId")).Count<int>();
                //}
                //if (powerCount1 + powerCount2 == 0)
                //{
                //    result = "{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}";
                //    return result;
                //}
                #endregion
                    
                #endregion
                    
                //DateTime bb = DateTime.Now;
                //double cc = bb.Subtract(aa).TotalMilliseconds;
                //menuBll.test(1, cc);
                    
                
                return result;
            }
            catch(Exception e)
            {
                result = "{\"status\":\"0\",\"msg\":\"对不起，系统错误：" + Utils.HtmlEncode(e.Message) + "\"}";
                return result;
            }
        }

        private static int CheckListContain(List<string[]> list, string[] strArr)
        {
            bool contain = false;
            int index = 0;
            foreach (string[] item in list)
            {
                if (item.SequenceEqual(strArr))
                {
                    contain = true;
                    break;
                }
            }
            if (contain)
            {
                return index + 1;
            }
            else
            {
                return 0;
            }

        }

    }
}
