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
using System.Data;
using System.Reflection;
using System.Collections;
using System.Text.RegularExpressions;

namespace Sinvie.Cms.Core
{
    /// <summary>
    /// object扩展,请详细了解此类
    /// <remarks>
    ///对很多类型进行了扩展操作,请一定要详细了解此类
    /// </remarks>
    /// </summary>
    public static class ExpandObject
    {
        ///// <summary>
        ///// 遍历获取类的属性及属性的值
        ///// </summary>
        ///// <typeparam name="T">某类型</typeparam>
        ///// <param name="t">T类实例对象</param>
        ///// <returns>自定义的Property对象</returns>
        //public static List<Property> Exp_Property<T>(this T t)
        //{
        //    var vList = new List<Property>();
        //    string tStr = string.Empty;
        //    if (t == null)
        //    {
        //        return null;
        //    }
        //    System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
        //    if (properties.Length <= 0)
        //    {
        //        return null;
        //    }
        //    foreach (System.Reflection.PropertyInfo item in properties)
        //    {
        //        string name = item.Name;
        //        object value = item.GetValue(t, null);
        //        if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
        //        {
        //            vList.Add(new Property() { Text = name, Value = value, PropertyType = item.PropertyType.Name.ToLower() });
        //        }
        //        else
        //        {
        //            Exp_Property(value);
        //        }
        //    }
        //    return vList;
        //}

        /// <summary>
        /// 先判断空,然后去前后空格
        /// </summary>
        /// <param name="objThis">待操作资料</param>
        /// <returns>先判断空,然后去前后空格</returns>
        /// <remarks>
        /// 当你不知道对象是否是null则请一定加上此方法
        /// </remarks>
        public static string Exp_Trim(this object objThis)
        {
            var objRef = objThis ?? string.Empty;
            return objRef.ToString().Trim();
        }
        /// <summary>
        /// 把资料转换成money,即,小数点后两位
        /// </summary>
        /// <param name="objThis">objThis</param>
        /// <returns>把资料转换成money,即,小数点后两位</returns>
        public static string Exp_Money(this decimal objThis)
        {
            objThis = Math.Round(objThis, 2);
            return objThis.ToString();
        }
        /// <summary>
        /// 把资料转换成money,即,小数点后两位
        /// </summary>
        /// <param name="objThis">objThis</param>
        /// <returns>把资料转换成money,即,小数点后两位</returns>
        public static string Exp_Money(this double objThis)
        {
            objThis = Math.Round(objThis, 2);
            return objThis.ToString();
        }
        /// <summary>
        /// 把资料转换成money,即,小数点后两位
        /// </summary>
        /// <param name="objThis">objThis</param>
        /// <returns>把资料转换成money,即,小数点后两位</returns>
        public static string Exp_Money(this decimal? objThis)
        {
            objThis = Math.Round(objThis ?? 0, 2);
            return objThis.ToString();
        }
        /// <summary>
        /// 把资料转换成money,即,小数点后两位
        /// </summary>
        /// <param name="objThis">objThis</param>
        /// <returns>把资料转换成money,即,小数点后两位</returns>
        public static string Exp_Money(this double? objThis)
        {
            objThis = Math.Round(objThis ?? 0, 2);
            return objThis.ToString();
        }
        /// <summary>
        /// 扩展的Exp_Trim方法
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="objThis">待操作数据</param>
        /// <param name="ToStr">相关的ToString的实现</param>
        /// <returns>处理后的值</returns>
        /// <remarks>
        /// <code>
        /// 方法会先判断null,如果null则返回空字符串
        /// 如果ToStr方法没有定义,则返回普通的trim
        /// 如果有则进行ToStr处理
        /// </code>
        /// </remarks>
        /// <example>
        /// <code>
        /// datatime dt=new ......
        /// string s=dt.Exp_Trim(p=>p.tostring("yyyyMMdd"));
        /// </code>
        /// </example>
        public static string Exp_Trim<T>(this T objThis, Func<T, string> ToStr)
        {
            string strE = string.Empty;
            if(objThis == null)
            {
                return strE;
            }
            else
            {
                // T TObj = (T)objThis;
                if(ToStr != null)
                {
                    return ToStr(objThis).Trim();
                }
                else
                {
                    return objThis.ToString().Trim();
                }

            }
        }
        /// <summary>
        /// 直接的类型转换,等同于(type)obj.可以让代码统一好看
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="objThis">待操作对象</param>
        /// <returns>类型转换后的结果</returns>
        /// <example>
        /// <code>
        ///int i=123;
        ///string s=i.Exp_To&quot;string>();
        ///等同于
        ///string s=(string)i;
        /// </code>
        /// </example>
        public static T Exp_To<T>(this object objThis)
        {
            return (T)objThis;
        }
        /// <summary>
        /// 转换成string然后使用Decimal.TryParse进行转换
        /// </summary>
        /// <param name="obj">obj对象</param>
        /// <returns>转换后的结果</returns>
        public static decimal Exp_DecimalTryParse(this object obj)
        {
            decimal dec = 0;
            decimal.TryParse(obj.Exp_Trim(), out dec);
            return dec;
        }
        /// <summary>
        /// 转换成string然后使用Decimal.TryParse进行转换
        /// </summary>
        /// <param name="obj">obj对象</param>
        /// <param name="round">读取小数点后的位数</param>
        /// <returns>转换后的结果</returns>
        public static decimal Exp_DecimalTryParse(this object obj, int round)
        {
            if (obj == null)
            {
                return 0;
            }
            decimal dec = 0;
            decimal.TryParse(obj.Exp_Trim(), out dec);
            if (round >= 0)
            {
                return Math.Round(dec, round);
            }
            return dec;
        }
        /// <summary>
        /// 转换成char类型<para>会先把obj转换成string</para>
        /// </summary>
        /// <param name="obj">obj 对象</param>
        /// <returns>char类型</returns>
        public static char Exp_CharTryParse(this object obj)
        {
            char c = ' ';
            char.TryParse(obj.Exp_Trim(), out c);
            return c;
        }

        /// <summary>
        /// 转换成string然后使用DateTime.TryParse进行转换
        /// </summary>
        /// <param name="obj">obj对象</param>
        /// <returns>转换后的结果</returns>
        public static DateTime Exp_DateTimeTryParse(this object obj)
        {
            DateTime DT;
            DateTime.TryParse(obj.Exp_Trim(), out DT);
            return DT;
        }
        /// <summary>
        /// 转换成string然后使用int.TryParse进行转换
        /// </summary>
        /// <param name="obj">obj对象</param>
        /// <returns>int.TryParse</returns>
        public static int Exp_IntTryParse(this object obj)
        {
            int int1 = 0;
            int.TryParse(obj.Exp_Trim(), out int1);
            return int1;
        }
        /// <summary>
        /// 将decimal转换成int
        /// </summary>
        /// <param name="obj">obj对象</param>
        /// <returns>int</returns>
        public static int Exp_DecimalToInt(this object obj)
        {
            return Convert.ToInt32(obj.Exp_DecimalTryParse(10));
        }
        /// <summary>
        /// 转换成string然后使用int.TryParse进行转换
        /// </summary>
        /// <param name="obj">obj对象</param>
        /// <returns>int.TryParse</returns>
        public static bool Exp_BoolTryParse(this object obj)
        {
            bool int1 = false;
            bool.TryParse(obj.Exp_Trim(), out int1);
            return int1;
        }
      
        /// <summary>
        /// 如果thisInt大于intValue则返回ReInt 否则返回intValue
        /// </summary>
        /// <param name="thisInt">thisInt</param>
        /// <param name="intValue">intValue</param>
        /// <param name="RefInt">RefInt</param>
        /// <returns> 如果thisInt大于intValue则返回ReInt 否则返回intValue</returns>
        public static int Exp_IsGreaterThan(this int thisInt, int intValue, int RefInt)
        {

            if(thisInt > intValue)
            {
                return RefInt;
            }
            else
            {
                return intValue;
            }
        }


        /// <summary>
        /// 把list中的资料连接成一个字符串,前后是没有分隔符的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="strFG">分隔符</param>
        /// <param name="fu">需要连接的内容,指定要把那个属性连接到字符串中</param>
        /// <returns>把list中的资料连接成一个字符串</returns>
        public static string Exp_JoinList<T>(this List<T> list, string strFG, Func<T, string> fu)
        {
            var strS = list.Exp_JoinList(strFG, fu, null);
            return strS;
        }
        /// <summary>
        /// 把list中的资料连接成一个字符串,前后分隔符,可以通过fulen来控制
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="strFG">分隔符</param>
        /// <param name="fu">需要连接的内容,指定要把那个属性连接到字符串中</param>
        /// <param name="fuLen">int:已经连接好的字符的长度  stringbuilder:当前的字符串  return:最后的结果 </param>
        /// <returns>把list中的资料连接成一个字符串</returns>
        public static string Exp_JoinList<T>(this List<T> list, string strFG, Func<T, string> fu, Func<int, StringBuilder, string> fuLen)
        {
            list = list ?? new List<T>();
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < list.Count; i++)
            {
                if(i != 0)
                {
                    sb.Append(strFG);
                }
                var TempS = fu(list[i]);
                sb.Append(TempS);
            }
            if(fuLen != null)
            {
                var strSb = fuLen(sb.Length, sb);
                return strSb.ToString();
            }
            else
            {
                return sb.ToString();
            }

        }
        /// <summary>
        /// 获得dataset的第一个datatable
        /// </summary>
        /// <param name="ds">ds</param>
        /// <returns>获得dataset的第一个datatable</returns>
        /// <remarks>
        /// 即 return ds.Tables[0]
        /// </remarks>
        public static DataTable Exp_DataSetToDataTable(this DataSet ds)
        {
            if(ds == null)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
            }
        }
      
        /// <summary>
        /// 把一个数组转换成另外一个数组,其中可以使用func进行操作
        /// </summary>
        /// <typeparam name="T">原始数组的类型</typeparam>
        /// <typeparam name="K">目标数组的类型</typeparam>
        /// <param name="obj1">原始资料</param>
        /// <param name="obj2">目标资料</param>
        /// <param name="func">处理方法</param>
        /// <example>
        /// <code>
        /// 此方法有问题,待测试
        /// </code>
        /// </example>
        public static void Exp_ArrayToArray<T, K>(this T[] obj1, K[] obj2, Action<T, K> func)
        {
            for(int i = 0; i < obj1.Length; i++)
            {
                func(obj1[i], obj2[i]);
            }
        }
        /// <summary>
        /// 从一个数组中获取对应的值,可以使用func进行相关的类型转换
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <typeparam name="K">K</typeparam>
        /// <param name="list">list</param>
        /// <param name="index">index</param>
        /// <param name="func">func</param>
        /// <returns> 从一个数组中获取对应的值,可以使用func进行相关的类型转换</returns>
        /// <remarks>
        /// <code>
        /// 如果list是null,或者index大于list的最大长度 则返回default(K)
        /// 如果 func没有定义则强行转换成K类型
        /// func只是用来进行把t类型转换成k类型用,如果能正常进行类型转换则可以省略
        /// </code>
        /// </remarks>
        public static K Exp_GetListValue<T, K>(this List<T> list, int index, Func<T, K> func)
        {
            if(list == null) return default(K);
            if(list.Count <= index) return default(K);
            var item = list[index];
            if(func != null)
            {
                return func(item);
            }
            return item.Exp_To<K>();
        }
       



        /// <summary>
        /// 如果obj的值为null则返回默认的DefValue,否则执行func
        /// </summary>
        /// <typeparam name="T">原始类型</typeparam>
        /// <typeparam name="K">要返回的类型</typeparam>
        /// <param name="obj">要处理的信息</param>
        /// <param name="func">当不是null时候,要进行处理的方法</param>
        /// <param name="DefValue">当时null时候的默认值</param>
        /// <returns>如果obj的值为null则返回默认的DefValue,否则执行func</returns>
        public static K Exp_ObjectIsNull<T, K>(this T obj, Func<T, K> func, K DefValue)
        {
            if(obj == null)
            {
                return DefValue;
            }
            else
            {
                return func(obj);
            }
        }
        /// <summary>
        /// 判断对象是否是null,如果是则返回defvalue,否则使用func
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="obj">要处理的方法</param>
        /// <param name="func">如果不是null则进行处理的方法</param>
        /// <param name="DefValue">如果是null的默认值</param>
        /// <param name="funcAll">最后进行处理的方法,无论是否null都会运行</param>
        /// <returns>判断对象是否是null,如果是则返回defvalue,否则使用func</returns>
        public static T Exp_ObjectIsNull<T>(this T obj, Action<T> func, T DefValue, Action<T> funcAll)
        {
            T temp;
            if(obj == null)
            {
                temp = DefValue;
            }
            else
            {
                temp = obj;
                if(func != null)
                {
                    func(temp);
                }
            }
            if(funcAll != null)
            {
                funcAll(temp);
            }
            return temp;
        }
        /// <summary>
        /// 获取第一个值
        /// <remarks>
        /// entity中不需要此方法了
        /// </remarks>
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>值</returns>
        public static T Exp_First<T>(this IQueryable<T> obj)
        {
            if(obj != null)
            {
                foreach(var item in obj)
                {
                    return item;
                }
            }
            return default(T);
        }
        /// <summary>
        /// 如果 obj为null或者count小于等于0 则返回 false
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>值</returns>
        public static bool Exp_Exists<T>(this IQueryable<T> obj)
        {
            if(obj != null)
            {
                if(obj.Count() > 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 如果obj大于0 则返回 defint ,否则返回 0
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="defint">默认</param>
        /// <returns>值</returns>
        public static int Exp_ExceedZero(this int obj, int defint)
        {
            if(obj > 0) return defint;
            return 0;
        }
        /// <summary>
        /// 扩展的as的写法
        /// </summary>
        /// <typeparam name="T">元类型</typeparam>
        /// <typeparam name="V">返回的类型</typeparam>
        /// <param name="obj">要转换的</param>
        /// <returns>as 后的 V 资料</returns>
        public static V Exp_As<T, V>(this T obj) where V : class
        {
            return (obj as V);
        }
        /// <summary>
        /// 判断当前对象是否是null
        /// <remarks>
        /// 当前方法先判断null,然后会判断 dbnull
        /// </remarks>
        /// </summary>
        /// <param name="obj">要判断的对象</param>
        /// <returns>true:null  false:非null</returns>
        public static bool Exp_IsNullObject(this object obj)
        {
            return obj.Exp_IsNull<object>();
        }

        /// <summary>
        /// 判断当前对象是否是null
        /// <remarks>
        /// 当前方法先判断null,然后会判断 dbnull
        /// </remarks>
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">要判断的对象</param>
        /// <returns>true:null  false:非null</returns>
        public static bool Exp_IsNull<T>(this T obj)
        {
            if(obj == null)
            {
                return true;
            }
            if(DBNull.Value.Equals(obj))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 取随机数  
        /// </summary>
        /// <param name="length">随机数的长度</param>
        /// <returns></returns>
        public static string Exp_BuildRandomStr(this int length)
        {
            Random rand = new Random();

            int num = rand.Next();

            string str = num.ToString();

            if(str.Length > length)
            {
                str = str.Substring(0, length);
            }
            else if(str.Length < length)
            {
                int n = length - str.Length;
                while(n > 0)
                {
                    str.Insert(0, "0");
                    n--;
                }
            }

            return str;
        }

        /// <summary>
        /// 把父类obj的资料复制到子类 thisobj中,
        /// 请一定注意obj里面的属性,必须完全被thisobj包含
        /// </summary>
        /// <param name="thisObj">子类</param>
        /// <param name="obj">父类</param>
        public static void Exp_ObjCopy(this object thisObj, object obj)
        {
            Type t = obj.GetType();
            PropertyInfo[] info = t.GetProperties();
            foreach(PropertyInfo i in info)
            {
                var item = i.GetValue(obj, null);
                i.SetValue(thisObj, item, null);
            }
        }
        /// <summary>
        /// 把一个IEnumerable约束的对象转换成一个list
        /// </summary>
        /// <typeparam name="LT">必须被list约束的对象</typeparam>
        /// <typeparam name="T">LT里面的泛型资料</typeparam>
        /// <typeparam name="K">要转换的list的泛型</typeparam>
        /// <param name="thisObj">要转换的IEnumerable</param>
        /// <param name="funcClassTOClass">表示怎样把T类型转换成k类型</param>
        /// <returns>转换结果</returns>
        public static List<K> Exp_ListToList<LT, T, K>
            (this LT thisObj, Func<T, K> funcClassTOClass) where LT : IEnumerable<T>
        {
            var List = new List<K>();
            foreach(T item in thisObj)
            {
                if(funcClassTOClass != null)
                {
                    List.Add(funcClassTOClass(item));
                }
            }
            return List;
        }


        /// <summary>
        /// 去除字符串中的HTML标签 并去除其中的两种格式的空格
        /// </summary>
        public static string Exp_RegexHTML(this object content)
        {
            string v = content.Exp_Trim();
            Regex regex = new Regex(@"<[^>]+>|</[^>]+>");
            var stroutput = regex.Replace(v, "").Replace("&nbsp;", "").Replace("　", "").Replace("  ", "").Replace("\r", "").Replace("\n", "");
            return stroutput;
        }
        
        /// <summary>
        /// 获取具有类别的标题
        /// </summary>
        /// <param name="Title">标题对象</param>
        /// <param name="len">显示字数</param>
        /// <param name="Type">类别名称对象</param>
        /// <returns>截取后的字符串</returns>
        public static string Exp_LenTitle(this object Title, int len, object Type)
        {
            var TitleName = Title.ToString();
            var CateName = Type.Exp_Trim();
            var subTitle = "";
            int clen = 0;
            int tlen = 0;
            for (int i = 0; i < CateName.Length; i++)
            {
                subTitle = CateName.Substring(i, 1);
                byte[] byte_len = Encoding.Default.GetBytes(subTitle);
                if (byte_len.Length > 1)
                    clen += 2;
                else
                {
                    if (Regex.IsMatch(subTitle, "[A-Z]"))
                    {
                        clen += 2;
                    }
                    else
                    {
                        clen += 1;
                    }
                }
            }
            for (int i = 0; i < TitleName.Length; i++)
            {
                subTitle = TitleName.Substring(i, 1);
                byte[] byte_len = Encoding.Default.GetBytes(subTitle);
                if (byte_len.Length > 1)
                    tlen += 2;
                else
                {
                    if (Regex.IsMatch(subTitle, "[A-Z]"))
                    {
                        tlen += 2;
                    }
                    else
                    {
                        tlen += 1;
                    }
                }
            }
            var cutlen = len - clen;
            cutlen = cutlen % 2 == 0 ? cutlen : cutlen - 1;
            if (TitleName.Length > 0 && tlen > cutlen)
            {
                var str = TitleName.Exp_SubString(cutlen, "");
                return str;
            }
            return TitleName;
        }
    }
}
