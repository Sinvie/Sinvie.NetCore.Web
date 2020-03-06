//======================================================================
//
//        Copyright (C) 贵州宝玉科技    
//        All rights reserved
//
//        filename : ExpandPage.cs
//        description :
//
//        created by 周济 at 2011/2/20 15:53:58
//        mail:13912699233@139.com
//
//======================================================================


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Data;

namespace Sinvie
{
    /// <summary>
    /// 对ｗｅｂ的ｐａｇｅ进行各种扩展
    /// </summary>
    public static class ExpandPage
    {
        /// <summary>
        /// 跳转并且执行End方法
        /// </summary>
        public static void Exp_Redirect(this Page page,string url)
        {
            page.Response.Redirect(url, false);
            System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        /// <summary>
        /// Repeater控件绑定
        /// </summary>
        public static void Exp_RepBind(this Repeater Rep, object DataSource)
        {
            Rep.DataSource = DataSource;
            Rep.DataBind();
        }
        #region ListControl控件公用绑定
        /// <summary>
        /// 绑定list列表控件 不包含默认项，需要时自行添加
        /// </summary>
        /// <param name="control1">ListControl子控件如：DropDownList,CheckBoxList,RadioButtonList等等</param>
        /// <param name="DataSource">绑定数据源</param>
        /// <param name="Text">用于显示的键名称</param>
        /// <param name="Value">用于绑定值的键名称</param>
        public static void Exp_BindListControl(this ListControl control1, object DataSource, string Text, string Value)
        {
            control1.DataSource = DataSource;
            control1.DataValueField = Value;
            control1.DataTextField = Text;
            control1.DataBind();
        }
        /// <summary>
        /// 绑定list列表控件 不包含默认项，需要时自行添加
        /// </summary>
        /// <param name="control1">ListControl子控件如：DropDownList,CheckBoxList,RadioButtonList等等</param>
        /// <param name="DataSource">绑定数据源</param>
        /// <param name="Text">用于显示的键名称</param>
        /// <param name="Value">用于绑定值的键名称</param>
        /// <param name="Value">用于绑定值的键名称</param>
        public static void Exp_BindListControl(this ListControl control1, object DataSource, string Text, string Value,bool HasDefault)
        {
            control1.Exp_BindListControl(DataSource, Text, Value);
            if (HasDefault)
            {
                control1.Items.Insert(0, new ListItem("请选择", ""));
            }
        }
        /// <summary>
        /// 设置List列表控件 的选中项 主要是CheckBoxList控件
        /// </summary>
        /// <param name="control1">ListControl子控件，主要指CheckBoxList控件</param>
        /// <param name="strSelectValue">值的字符中用separator字符分隔</param>
        /// <param name="separator">值字符串的分隔符号</param>
        public static void Exp_SetListControlValue(this ListControl control1, string strSelectValue,char separator)
        {
            if (strSelectValue.Length > 0)
            {
                var arrData = new List<string>();
                if (strSelectValue.Contains(separator))
                {
                    arrData = strSelectValue.Exp_SplitToList(separator, p => p.Exp_Trim());
                }
                else
                {
                    arrData.Add(strSelectValue);
                }
                foreach (ListItem item in control1.Items)
                {
                    if (arrData.Contains(item.Value))
                    {
                        item.Selected = true;
                    }
                }
            }
        }
        /// <summary>
        /// 设置List列表控件的选中项 用separator符号分隔 separator为空时为*
        /// </summary>
        /// <param name="control1">ListControl子控件，主要指CheckBoxList控件</param>
        /// <param name="separator">用于分隔选择项的分隔符</param>
        /// <returns>返回拼接后的值</returns>
        public static string Exp_GetListControlValue(this ListControl control1, string separator)
        {
            var arrData = new List<string>();
            foreach (ListItem item in control1.Items)
            {
                if (item.Selected)
                {
                    arrData.Add(item.Value);
                }
            }
            separator = separator.Length<=0?"*":separator;
            return string.Join(separator, arrData.ToArray());
        }
        /// <summary>
        /// 设置List列表控件 的选中项
        /// </summary>
        /// <param name="control1">ListControl子控件，主要指CheckBoxList,RadioButtonList控件</param>
        /// <param name="iValue">值的字符中用separator字符分隔</param>
        public static void Exp_SetListControlValue(this ListControl control1, int iValue)
        {
            if (iValue > 0)
            {
                foreach (ListItem item in control1.Items)
                {
                    if (iValue.ToString().Equals(item.Value))
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 设置List列表控件的选中项 只能单选情况
        /// </summary>
        /// <param name="control1">ListControl子控件，主要指RadioButtonList控件</param>
        /// <returns>返回拼接后的值</returns>
        public static string Exp_GetListControlValue(this ListControl control1)
        {
            foreach (ListItem item in control1.Items)
            {
                if (item.Selected)
                {
                    return item.Value;
                }
            }
            return "";
        }
        #endregion

        /// <summary>
        /// 当前的Eval使用的转换
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="page"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        static object ExpHelper<TEntity, TResult>(Page page, Func<TEntity, TResult> func) where TEntity : class
        {
            var itm = page.GetDataItem();
            var objItem = itm as TEntity;
            return func(objItem);
        }

        /// <summary>
        /// 强类型的数据绑定
        /// </summary>
        /// <typeparam name="T">要转换的资料</typeparam>
        /// <param name="page">当前页面</param>
        /// <param name="func">获取资料</param>
        /// <returns></returns>
        public static object Eval<T>(this Page page, Func<T, object> func) where T : class
        {
            return ExpHelper<T, object>(page, func);
        }

        /// <summary>
        /// 列表页面 筛选区获取某条件的url参数
        /// </summary>
        /// <param name="ParmName">参数名称</param>
        /// <param name="ParmValue">参数值</param>
        /// <returns></returns>
        public static object Exp_QueryString(this string ParmName, object ParmValue)
        { 
            var strQuery = string.Empty;
            var strCurrentParm = string.Empty;
            var QueryString = System.Web.HttpContext.Current.Request.QueryString.ToString();
            if (QueryString.Length <= 0)
            {
                strQuery = "{0}={1}".Exp_Format(ParmName, ParmValue);
            }
            else
            {
                QueryString += "&";
                var strExpression = "{0}=(?<val>.*?)&".Exp_Format(ParmName);
                strCurrentParm = "{0}={1}&".Exp_Format(ParmName, ParmValue);
                Regex ParmRegex = new Regex(strExpression, RegexOptions.Singleline);
                var matcher = ParmRegex.Matches(QueryString);
                if (matcher.Count > 0)
                {
                    strQuery = QueryString.Replace(matcher[0].Value, strCurrentParm);
                }
                else
                {
                    strQuery = QueryString + strCurrentParm;
                }
                if (strQuery.EndsWith("&"))
                {
                    strQuery = strQuery.Exp_SubStr(strQuery.Length - 1, "");
                }
            }
            return strQuery;
        }


        /// <summary>
        /// 列表页面 筛选区获取某条件的url参数
        /// </summary>
        /// <param name="ParmName">参数名称</param>
        /// <param name="ParmValue">参数值</param>
        /// <returns></returns>
        public static object Exp_QueryString(this string QueryString, string ParmName, object ParmValue)
        {
            var strQuery = string.Empty;
            var strCurrentParm = string.Empty;
            if (QueryString.Length <= 0)
            {
                strQuery = "{0}={1}".Exp_Format(ParmName, ParmValue);
            }
            else
            {
                QueryString += "&";
                var strExpression = "{0}=(?<val>.*?)&".Exp_Format(ParmName);
                if (ParmValue != null)
                {
                    strCurrentParm = "{0}={1}&".Exp_Format(ParmName, ParmValue);
                }
                Regex ParmRegex = new Regex(strExpression, RegexOptions.Singleline);
                var matcher = ParmRegex.Matches(QueryString);
                if (matcher.Count > 0)
                {
                    strQuery = QueryString.Replace(matcher[0].Value, strCurrentParm);
                }
                else
                {
                    strQuery = QueryString + strCurrentParm;
                }
                if (strQuery.EndsWith("&"))
                {
                    strQuery = strQuery.Exp_SubStr(strQuery.Length - 1, "");
                }
            }
            return strQuery;
        }


        /// <summary>
        /// 系统错误时保存错误日志
        /// </summary>
        public static void Exp_WriteErrorLogFile(this System.Web.UI.Page page, Exception err)
        {
            string error = string.Empty;
            string errortime = string.Empty;
            string erroraddr = string.Empty;
            string errorinfo = string.Empty;
            string errorsource = string.Empty;
            string errortrace = string.Empty;

            error += "发生时间:" + System.DateTime.Now.ToString() + "\r\n";
            errortime = "发生时间:" + System.DateTime.Now.ToString();

            error += "发生异常页: " + page.Request.Url.ToString() + "\r\n";
            erroraddr = "发生异常页: " + page.Request.Url.ToString();

            error += "异常信息: " + err.Message + "\r\n";
            errorinfo = "异常信息: " + err.Message;

            //error +="错误源:"+objErr.Source+"<br>";
            //error += "堆栈信息:" + objErr.StackTrace + "<br>";
            errorsource = "错误源:" + err.Source;
            errortrace = "堆栈信息:" + err.StackTrace;
            error += "---------------------------------------------------\r\n";
            page.Server.ClearError();

            //独占方式，因为文件只能由一个进程写入.
            System.IO.StreamWriter writer = null;
            try
            {
                lock (page)
                {
                    // 写入日志
                    string year = DateTime.Now.Year.ToString();
                    string month = DateTime.Now.Month.ToString();
                    string path = string.Empty;
                    string filename = System.DateTime.Now.ToString("yyyyMMddHHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + ".log";
                    path = page.Server.MapPath("/errorlog/") + year + "/" + month;
                    //如果目录不存在则创建
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    System.IO.FileInfo file = new System.IO.FileInfo(path + "/" + filename);
                    writer = new System.IO.StreamWriter(file.FullName, true);//文件不存在就创建,true表示追加
                    writer.WriteLine("用户IP:" + page.Request.UserHostAddress);
                    writer.WriteLine(errortime);
                    writer.WriteLine(erroraddr);
                    writer.WriteLine(errorinfo);
                    writer.WriteLine(errorsource);
                    writer.WriteLine(errortrace);
                    writer.WriteLine("--------------------------------------------------------------------------------------");
                }
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// 系统错误时保存错误日志
        /// </summary>
        public static void Exp_WriteErrorLogFile(this System.Web.UI.Page page, string err)
        {
            string error = string.Empty;
            string errortime = string.Empty;
            string erroraddr = string.Empty;
            string errorinfo = string.Empty;
            string errorsource = string.Empty;
            string errortrace = string.Empty;

            error += "发生时间:" + System.DateTime.Now.ToString() + "\r\n";
            errortime = "发生时间:" + System.DateTime.Now.ToString();

            error += "发生异常页: " + page.Request.Url.ToString() + "\r\n";
            erroraddr = "发生异常页: " + page.Request.Url.ToString();

            error += "错误信息: " + err + "\r\n";
            errorinfo = "错误信息: " + err;

            error += "---------------------------------------------------\r\n";
            page.Server.ClearError();

            //独占方式，因为文件只能由一个进程写入.
            System.IO.StreamWriter writer = null;
            try
            {
                lock (page)
                {
                    // 写入日志
                    string year = DateTime.Now.Year.ToString();
                    string month = DateTime.Now.Month.ToString();
                    string path = string.Empty;
                    string filename = System.DateTime.Now.ToString("yyyyMMddHHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + ".log";//553548177
                    path = page.Server.MapPath("/errorlog/") + year + "/" + month;
                    //如果目录不存在则创建
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    System.IO.FileInfo file = new System.IO.FileInfo(path + "/" + filename);
                    writer = new System.IO.StreamWriter(file.FullName, true);//文件不存在就创建,true表示追加
                    writer.WriteLine("用户IP:" + page.Request.UserHostAddress);
                    writer.WriteLine(errortime);
                    writer.WriteLine(erroraddr);
                    writer.WriteLine(errorinfo);
                    writer.WriteLine(errorsource);
                    writer.WriteLine(errortrace);
                    writer.WriteLine("--------------------------------------------------------------------------------------");
                }
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

    }
}
