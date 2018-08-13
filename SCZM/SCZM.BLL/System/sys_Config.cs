using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Text;
using SCZM.Common;

namespace SCZM.BLL.System
{
    public partial class sys_Config
    {
        private readonly DAL.System.sys_Config dal = new DAL.System.sys_Config();

        /// <summary>
        ///  读取配置文件
        /// </summary>
        public Model.System.sys_Config loadConfig()
        {
            Model.System.sys_Config model = CacheHelper.Get<Model.System.sys_Config>(Keys.CACHE_SITE_CONFIG);
            if (model == null)
            {
                CacheHelper.Insert(Keys.CACHE_SITE_CONFIG, dal.loadConfig(Utils.GetXmlMapPath(Keys.FILE_SITE_XML_CONFING)),
                    Utils.GetXmlMapPath(Keys.FILE_SITE_XML_CONFING));
                model = CacheHelper.Get<Model.System.sys_Config>(Keys.CACHE_SITE_CONFIG);
            }
            return model;
        }

        /// <summary>
        ///  保存配置文件
        /// </summary>
        public Model.System.sys_Config saveConifg(Model.System.sys_Config model)
        {
            return dal.saveConifg(model, Utils.GetXmlMapPath(Keys.FILE_SITE_XML_CONFING));
        }

    }
}
