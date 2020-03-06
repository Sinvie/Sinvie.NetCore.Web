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

namespace Sinvie.Cms.Core
{
    /// <summary>
    /// 对DateTime类型的扩展方法类
    /// </summary>
    /// <remarks>
    /// 此类中的方法都是静态扩展类,请正确应用相应的命名空间
    /// </remarks>
    public static class ExpandDateTime
    {
        /// <summary>
        /// 日期的格式化类型
        /// </summary>
        public enum DateFormType
        {
            /// <summary>
            /// 英文日期 yyyy-MM-dd
            /// </summary>
            EnDate = 0,
            /// <summary>
            /// 英文时间  HH:mm:ss
            /// </summary>
            EnTime = 1,
            /// <summary>
            /// 英文时间日期 yyyy-MM-dd HH:mm:ss
            /// </summary>
            EnDateTIme = 2,
            /// <summary>
            /// 英文日期 MM-dd
            /// </summary>
            EnMMdd = 3,
            /// <summary>
            /// 中文日期 yyyy年MM月dd日
            /// </summary>
            CnDate = 4,
            /// <summary>
            /// 中文时间 HH点mm分ss
            /// </summary>
            CnTime = 5,
            /// <summary>
            /// 中文日期时间 yyyy年MM月dd日HH点mm分ss
            /// </summary>
            CnDatetTime = 6,
            /// <summary>
            /// 中文日期 MM月dd日
            /// </summary>
            CnMMdd = 7

        }

        /// <summary>
        /// 把日期转换成星期,1234567
        /// <para>
        /// 星期天对应7
        /// </para>
        /// </summary>
        /// <param name="DT">待处理的字符</param>
        /// <returns>对应的星期整数</returns>
        public static int Exp_ToWeek(this DateTime DT)
        {
            return DT.Exp_ToWeek(false);
        }
        /// <summary>
        /// 把日期转换成星期
        /// <para>
        /// 可以采用 默认的方法:0123456 或者采用 1234567
        /// </para>
        /// </summary>
        /// <param name="DT">待处理</param>
        /// <param name="blnDefault"><para>true:默认1234560</para><para>false:1234567</para></param>
        /// <returns>对应的星期整数</returns>
        public static int Exp_ToWeek(this DateTime DT, bool blnDefault)
        {
            DayOfWeek dw = DT.DayOfWeek;
            if(blnDefault)
            {
                return dw.Exp_To<int>();
            }
            else
            {

                switch(dw)
                {
                    case DayOfWeek.Monday:
                        return 1;
                    case DayOfWeek.Tuesday:
                        return 2;
                    case DayOfWeek.Wednesday:
                        return 3;
                    case DayOfWeek.Thursday:
                        return 4;
                    case DayOfWeek.Friday:
                        return 5;
                    case DayOfWeek.Saturday:
                        return 6;
                    default:// DayOfWeek.Sunday:
                        return 7;
                }
            }
        }
        /// <summary>
        /// 把日期转换成中文星期
        /// <para>
        /// 星期一....星期日
        /// </para>
        /// </summary>
        /// <param name="dateTime">待处理的资料</param>
        /// <returns>对应的星期的中文名称</returns>
        public static string Exp_ToWeekCH(this DateTime dateTime)
        {
            return dateTime.Exp_ToWeekCH(p => p, p => p);
        }
        /// <summary>
        /// 把日期转换成中文星期
        /// <para>
        /// 可以自定义星期日中 <paramref name="日"/> 字符,如星期天
        /// </para>        
        /// </summary>
        /// <param name="dateTime">待操作资料</param>
        /// <param name="strPrefix">中文名称的前缀处理,默认是星期</param>
        /// <param name="strName">当是星期天的时候,用作处理的,默认是 日</param>
        /// <returns></returns>
        public static string Exp_ToWeekCH(this DateTime dateTime, string strPrefix, string strName)
        {
            return dateTime.Exp_ToWeekCH(p => strPrefix, p => strName);
        }
        /// <summary>
        /// 把日期转换成中文星期
        /// <para>
        /// 可以自定义星期日中 <paramref name="日"/> 字符,如星期天
        /// </para>
        /// </summary>
        /// <param name="dateTime">待操作资料</param>
        /// <param name="fu_Prefix">中文名称的前缀处理,默认是星期</param>
        /// <param name="fu_Name">当是星期天的时候,用作处理的,可以 默认参数值为<paramref name="日"/> </param>
        /// <returns>把日期转换成中文星期</returns>
        /// <example>
        /// <code>
        ///  var ss = d.ToWeekCH(p => "周", p => "天");
        /// </code>
        /// <code>
        /// var sss = d.ToWeekCH(p => p, p => p);
        /// </code>
        /// </example>
        public static string Exp_ToWeekCH(this DateTime dateTime, Func<string, string> fu_Prefix, Func<string, string> fu_Name)
        {

            DayOfWeek dw = dateTime.DayOfWeek;

            var strPrefix = fu_Prefix("星期");

            switch(dw)
            {
                case DayOfWeek.Monday:
                    return strPrefix + "一";
                case DayOfWeek.Tuesday:
                    return strPrefix + "二";
                case DayOfWeek.Wednesday:
                    return strPrefix + "三";
                case DayOfWeek.Thursday:
                    return strPrefix + "四";
                case DayOfWeek.Friday:
                    return strPrefix + "五";
                case DayOfWeek.Saturday:
                    return strPrefix + "六";
                default:
                    var s = strPrefix + fu_Name("日");
                    return s;
            }
        }
        /// <summary>
        ///中文化日期
        /// </summary>
        /// <param name="DT">日期</param>
        /// <returns>yyyy年MM月dd日</returns>
        public static string Exp_GetString(this DateTime DT)
        {
            return DT.ToString("yyyy年MM月dd日");
        }
        /// <summary>
        ///中文化日期
        /// </summary>
        /// <param name="DT">日期</param>
        /// <returns>yyyy年MM月dd日</returns>
        public static string Exp_GetString(this DateTime? DT)
        {
            return (DT ?? DateTime.Now).ToString("yyyy年MM月dd日");
        }
        /// <summary>
        ///中文化日期
        /// </summary>
        /// <param name="DT">日期</param>
        /// <returns>yyyy年MM月dd日hh:mm</returns>
        public static string Exp_GetDateTimeString(this DateTime DT)
        {
            return DT.ToString("yyyy年MM月dd日hh:mm");
        }
        /// <summary>
        ///中文化日期
        /// </summary>
        /// <param name="DT">日期</param>
        /// <returns>yyyy年MM月dd日hh:mm</returns>
        public static string Exp_GetDateTimeString(this DateTime? DT)
        {
            return (DT ?? DateTime.Now).ToString("yyyy年MM月dd日hh:mm");
        }
        /// <summary>
        /// 统一格式化日期
        /// </summary>
        /// <param name="DT">日期</param>
        /// <param name="e">格式化的类型</param>
        /// <returns> 统一格式化日期</returns>
        public static string Exp_GetString(this DateTime DT, DateFormType e)
        {
            switch(e)
            {
                case DateFormType.EnDate:
                    return DT.ToString("yyyy-MM-dd");
                case DateFormType.EnTime:
                    return DT.ToString("HH:mm:ss");
                case DateFormType.EnDateTIme:
                    return DT.ToString("yyyy-MM-dd HH:mm:ss");
                case DateFormType.CnDate:
                    return DT.ToString("yyyy年MM月dd日");
                case DateFormType.CnTime:
                    return DT.ToString("HH点mm分ss");
                case DateFormType.CnDatetTime:
                    return DT.ToString("yyyy年MM月dd日HH点mm分ss");
                case DateFormType.CnMMdd:
                    return DT.ToString("MM月dd日");
                case DateFormType.EnMMdd:
                    return DT.ToString("MM-dd");
                default:
                    return DT.ToString();
            }
        }
        /// <summary>
        /// 统一格式化日期
        /// </summary>
        /// <param name="DT">日期</param>
        /// <param name="e">格式化的类型</param>
        /// <returns> 统一格式化日期</returns>
        public static string Exp_GetString(this DateTime? DT, DateFormType e)
        {
            return DT.Exp_IsNull().Exp_GetString(e);

        }
        /// <summary>
        /// 如果时间为null则给一个默认时间
        /// </summary>
        /// <param name="thisDT">当前时间</param>
        /// <param name="DefDt">默认时间</param>
        /// <returns>如果时间为null则给一个默认时间</returns>
        public static DateTime Exp_IsNull(this DateTime? thisDT, DateTime DefDt)
        {
            return thisDT ?? DefDt;
        }
        /// <summary>
        /// 如果时间为null则给一个默认时间
        /// </summary>
        /// <param name="thisDT">当前时间</param>
        /// <returns>如果时间为null则给一个默认时间</returns>
        public static DateTime Exp_IsNull(this DateTime? thisDT)
        {
            return thisDT ?? DateTime.Now;
        }
        /// <summary>
        /// 获得一个月的第一天
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <returns>第一天</returns>
        public static DateTime Exp_WeekFirstDay(this DateTime dateTime)
        {
            var dt = new DateTime(dateTime.Year, dateTime.Month, 1);
            return dt;
        }
        /// <summary>
        /// 获得一个月的最后一天
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <returns>第一天</returns>
        public static DateTime Exp_WeekLastDay(this DateTime dateTime)
        {
            var dt = new DateTime(dateTime.Year, dateTime.Month + 1, 1);
            return dt.AddDays(-1);
        }
        /// <summary>
        /// 获得一个月的第一天
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <returns>第一天</returns>
        public static DateTime Exp_MonthFirstDay(this DateTime dateTime)
        {
            var dt = new DateTime(dateTime.Year, dateTime.Month, 1);
            return dt;
        }
        /// <summary>
        /// 获得一个月的最后一天
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <returns>第一天</returns>
        public static DateTime Exp_MonthLastDay(this DateTime dateTime)
        {
            var dt = new DateTime(dateTime.Year, dateTime.Month + 1, 1);
            return dt.AddDays(-1);
        }

        /// <summary>
        /// 获取当前时间所在年的第一天(例如:2010年01月01日)
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <returns>第一天</returns>
        public static DateTime Exp_YearFirstDay(this DateTime dateTime)
        {
            var dt = new DateTime(dateTime.Year, 1, 1);
            return dt;
        }

        /// <summary>
        /// 今天距离指定日期,有多少天（正数表示还有多少天，负数表示已过去多少天）
        /// </summary>
        /// <param name="dt">指定的日期</param>
        /// <returns>天数(正数表示还有多少天，负数表示已过去多少天)</returns>
        public static int Exp_DistanceNowDay(this DateTime dt)
        {
            return (dt - DateTime.Now).Days;
        }
    }
}
