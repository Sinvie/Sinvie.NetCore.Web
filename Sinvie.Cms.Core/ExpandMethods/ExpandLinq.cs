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


using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Sinvie.Cms.Core
{
    /// <summary>
    /// 对linq中Distinct进行设置的类
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class LinqDistinct<T> : IEqualityComparer<T>
    {
        /// <summary>
        /// 构造一个可以进行distinct的类
        /// </summary>
        /// <param name="func">equals的重写委托</param>
        /// <param name="funcGetC">GetHashCode的重写委托</param>
        public LinqDistinct(Func<T, T, bool> func, Func<T, int> funcGetC)
        {
            _IsEquals = func;
            _GetHashCode = funcGetC;
        }
        /// <summary>
        /// query的重写委托
        /// </summary>
        public Func<T, T, bool> _IsEquals { get; set; }
        /// <summary>
        /// gethashcode的重写委托
        /// </summary>
        public Func<T, int> _GetHashCode { get; set; }
        /// <summary>
        /// 判断两者是否相等
        /// </summary>
        /// <param name="t1">第一个元素</param>
        /// <param name="t2">第二个元素</param>
        /// <returns>是否相等</returns>
        public bool Equals(T t1, T t2)
        {
            if(t1 == null || t2 == null)
            {
                return false;
            }
            if(_IsEquals != null)
            {
                return _IsEquals(t1, t2);
            }
            else
            {
                return t1.Equals(t2);
            }

        }
        /// <summary>
        /// 获取主键值
        /// </summary>
        /// <param name="t1"></param>
        /// <returns></returns>
        public int GetHashCode(T t1)
        {
            if(_GetHashCode != null)
            {

                return _GetHashCode(t1);
            }
            return t1.GetHashCode();
        }

    }
    public static class ExpandLinq
    {
        #region  Json对象与字符串 序列化与反序列化

        /// <summary>
        /// 将单个JSON对象转化成对应实体 返回指定类型实体
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="jsonString">字符串</param>
        /// <param name="type">1没有list类型,2有list类型</param>
        /// <param name="IsArrayString">是否JSON数组对象字符串</param>
        /// <returns></returns>
        public static T Exp_JsonToModel<T>(this string jsonString, bool IsArrayString) {
            //格式化字符串
            if (IsArrayString) {
                jsonString = jsonString.Replace("\\", "").Replace("\"[", "[").Replace("]\"", "]");
            } else {
                jsonString = jsonString.Replace("\\", "").Replace("\"[", "").Replace("]\"", "");
            }
            return (T)JsonConvert.DeserializeObject<T>(jsonString);
        }

        /// <summary>
        /// 将实体对象转化成成json字符串并返回
        /// </summary>
        /// <typeparam name="T">实体对象类型</typeparam>
        /// <param name="m">实体对象</param>
        /// <returns>返回转化后的JSON字符串</returns>
        public static string Exp_ModelToJson<T>(this T m)
        {
            return JsonConvert.SerializeObject(m);
        }

        /// <summary>
        /// 将JSON对象数组转化成实体对象集合List
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> Exp_JsonToModelList<T>(this string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }

        #endregion
    }
}
