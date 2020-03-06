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
    public static class ExpandStringBuilder
    {
        /// <summary>
        /// 写字符串
        /// </summary>
        /// <param name="sb">原始StringBuilder对象</param>
        /// <param name="len">制表符数目</param>
        /// <param name="str">添加内容</param>
        /// <returns>将指定的字符添加到StringBuilder中并返回</returns>
        public static StringBuilder AppendLineNum(this StringBuilder sb, int len, string str)
        {
            var lt = string.Empty;
            for (var i = 0; i < len; i++)
            {
                lt += "\t";
            }
            sb.AppendLine(lt + str);
            return sb;
        }

        /// <summary>
        /// 写字符串
        /// </summary>
        /// <param name="sb">原始StringBuilder对象</param>
        /// <param name="len">制表符数目</param>
        /// <param name="str">添加内容</param>
        /// <returns>将指定的字符添加到StringBuilder中并返回</returns>
        public static StringBuilder AppendNum(this StringBuilder sb, int len, string str)
        {
            var lt = string.Empty;
            for (var i = 0; i < len; i++)
            {
                lt += "\t";
            }
            sb.Append(lt + str);
            return sb;
        }
    }
}
