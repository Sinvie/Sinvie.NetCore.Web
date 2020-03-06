//======================================================================
//
//        Copyright (C) 贵州宝玉科技    
//        All rights reserved
//
//        filename :ExpandDateTime
//        description :
//
//        modify by 邹兴武 2020-03-05
//        mail:5170380@163.com
//
//======================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Sinvie.Cms.Core
{
    /// <summary>
    /// 操作xml的类
    /// </summary>
    public static class ExpandXML
    {
        /// <summary>
        /// 获得应用程序的相关版本号
        /// </summary>
        /// <remarks>
        /// 此方法专门用来为应用程序获取url上的内部版本号而用,使用的是linq的xml获取
        /// </remarks>
        /// <param name="strURL">应用程序发布的地址,不带http和文件名称,最后不带斜杠</param>
        /// <param name="ApplicationName">引用程序发布的文件名称,不带扩展名</param>
        /// <returns>如果是空字符串则表示没有获取到</returns>
        public static string GetApplicationVersion(string strURL, string ApplicationName)
        {
            try
            {
                string URL = string.Format("http://{0}/{1}.application", strURL, ApplicationName);
                XElement doc = XElement.Load(URL);
                var docVersionItems = (from pp in doc.Elements().Attributes()
                                       where pp.Name == "version"
                                       select pp).Take(1);

                foreach (var item in docVersionItems)
                {
                    return item.Value;
                }
                return "";
            }
            catch 
            {
                return "";
            }
        }
    }
}
