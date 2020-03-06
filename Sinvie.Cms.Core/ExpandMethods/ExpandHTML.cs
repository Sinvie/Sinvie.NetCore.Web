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
using System.Web;
using System.Text.RegularExpressions;

namespace Sinvie.Cms.Core
{
    /// <summary>
    /// 对html的一些处理
    /// </summary>
    public static class ExpandHTML
    {
        #region 去除html标签
        /// <summary>
        /// 把html相关的标记去除
        /// </summary>
        /// <param name="html">待处理的html代码</param>
        /// <returns>去掉标记的代码</returns>
        /// <remarks>
        /// 此方法常用来把从数据库中去除的内容显示到网页上,因为内容有可能有html代码,会影响显示页面
        /// </remarks>
        public static string Exp_RegexHTML(this string html)
        {
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" no[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记 
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性 网管网bitsCN.com 
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件 
            html = regex4.Replace(html, ""); //过滤iframe 
            html = regex5.Replace(html, ""); //过滤frameset 
            html = regex6.Replace(html, ""); //过滤frameset 
            html = regex7.Replace(html, ""); //过滤frameset 
            html = regex8.Replace(html, ""); //过滤frameset 
            html = regex9.Replace(html, "");
            html = html.Replace(" ", "");


            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            return html;
        }
        /// <summary>
        /// 把html相关的标记去除
        /// </summary>
        /// <param name="html">待处理的html代码</param>
        /// <param name="listRegex">如果默认方法没有处理完全,则可以进行相关的补充操作</param>
        /// <returns>去掉标记的html</returns>
        /// <remarks>
        ///如果基本的去标记语句不能达到目的,则可以使用委托方法继续处理
        /// </remarks>
        public static string Exp_RegexHTML(this string html, Func<string, string> listRegex)
        {
            var refS = html.Exp_RegexHTML();
            var s = listRegex(refS);

            return s;
        }
        /// <summary>
        /// 把当前资料转换成html可以正常接收的字符
        /// <para>目前处理了大/小于号</para>
        /// </summary>
        /// <param name="str1">要显示的内容</param>
        /// <returns>安全的html字符串</returns>
        public static string Exp_ToHtml(this string str1)
        {
            return str1.Exp_Trim().Replace("<", " &lt;").Replace(">", "&gt;");
        }
        /// <summary>
        /// 把当前资料转换成html可以正常接收的字符,如果当前是任意的空,则产生html空格字符
        /// <para>在ToHtml的基础上做了扩展</para>
        /// </summary>
        /// <param name="str1">要显示的内容</param>
        /// <returns>安全的html字符串</returns>
        public static string Exp_ToHtmlEmpty(this string str1)
        {
            str1 = str1.Exp_Trim();
            if (str1.Length <= 0)
            {
                return "&nbsp;";
            }
            else
            {
                return str1.Exp_ToHtml();
            }

        }
        #endregion 

        #region 字符串编码
        /// <summary>
        /// 对字符串进行URL编码
        /// </summary>
        /// <param name="instr">字符串</param>
        /// <param name="charset">编码</param>
        /// <returns>对字符串进行URL编码</returns>
        public static string Exp_UrlEncode(this string instr, string charset)
        {
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;
                try
                {
                    res = HttpUtility.UrlEncode(instr, Encoding.GetEncoding(charset));
                }
                catch (Exception)
                {
                    res = HttpUtility.UrlEncode(instr, Encoding.GetEncoding("GB2312"));
                }
                return res;
            }
        }
        
        /// <summary>
        /// 对字符串进行URL解码
        /// </summary>
        /// <param name="instr">字符串</param>
        /// <param name="charset">编码</param>
        /// <returns>对字符串进行URL解码</returns>
        public static string Exp_UrlDecode(this string instr, string charset)
        {
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;

                try
                {
                    res = HttpUtility.UrlDecode(instr, Encoding.GetEncoding(charset));

                }
                catch (Exception)
                {
                    res = HttpUtility.UrlDecode(instr, Encoding.GetEncoding("GB2312"));
                }


                return res;

            }
        }
        #endregion 
                

    }
}
