using System;
using System.Collections.Generic;
using System.Text;
using SCZM.Common;

namespace SCZM.DAL.System
{
    /// <summary>
    /// 数据访问类:站点配置
    /// </summary>
    public partial class sys_Config
    {
        private static object lockHelper = new object();

        /// <summary>
        ///  读取站点配置文件
        /// </summary>
        public Model.System.sys_Config loadConfig(string configFilePath)
        {
            return (Model.System.sys_Config)SerializationHelper.Load(typeof(Model.System.sys_Config), configFilePath);
        }

        /// <summary>
        /// 写入站点配置文件
        /// </summary>
        public Model.System.sys_Config saveConifg(Model.System.sys_Config model, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(model, configFilePath);
            }
            return model;
        }

    }
}
