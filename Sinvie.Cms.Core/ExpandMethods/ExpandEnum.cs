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
    /// 处理枚举的扩展方法类
    /// </summary>
    public static class ExpandEnum
    {
        /// <summary>
        /// 把一个枚举转换成一个list泛型类
        /// </summary>
        /// <typeparam name="SType">需要返回的list的类型</typeparam>
        /// <typeparam name="EType">枚举类型</typeparam>
        /// <param name="func">表示怎样把枚举的值和表示,赋予给SType</param>
        /// <returns> 把一个枚举转换成一个list泛型类</returns>
        /// <remarks>
        /// 使用此方法,可以把一个枚举放入到一个下拉框控件中
        /// </remarks>
        /// <example>
        /// <code>
        /// var list = ExpandEnum.Exp_GetEnumList&quot;TempItem, PublicEnum.EnumTransaction.enumType>((S, I) =>new TempItem(){ID = I,类型 = S});
        /// TempItem是你需要的返回的类型,是一个自己定义的强类型,
        /// PublicEnum.EnumTransaction.enumType是一个枚举类型
        /// </code>
        /// </example>
        public static List<SType> Exp_GetEnumList<SType, EType>(Func<string, int, SType> func)
            where EType : IComparable
        {
            //枚举的具体类型
            var ET = typeof(EType);
            //
            var ValueS = Enum.GetValues(ET);
            List<SType> list = new List<SType>();
            foreach(var item in ValueS)
            {
                var Temp = Enum.GetName(ET, item);
                list.Add(func(Temp, (int)item));
            }
            return list;
        }

        /// <summary>
        /// 日期：2012-09-04
        /// 功能：根据ID，获取枚举的项
        /// </summary>
        /// <example>ProExp.Exp_GetEnum</example>
        /// <returns></returns>
        public static EType Exp_GetEnum<EType>(this int id) where EType : IComparable
        {
            var Values = Enum.GetValues(typeof(EType));

            foreach (var item in Values)
            {
                if ((int)item == id)
                {
                    return item.Exp_To<EType>();
                }
            }
            return default(EType);
        }

        /// <summary>
        /// 根据ID，获取枚举的字符
        /// </summary>
        public static string Exp_GetEnumStr<EType>(this int id) where EType : IComparable
        {
            return id.Exp_To<EType>().ToString();
        }
    }
}
