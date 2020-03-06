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
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;

namespace Sinvie.Cms.Core
{
    /// <summary>
    /// string的静态扩展类
    /// <remarks>常用扩展累之一,请详细了解此类中的各种方法</remarks>
    /// </summary>
    public static class ExpandString
    {
        /// <summary>
        /// 将转义后的html标签还原，主要处理：&amp;+&lt;+&gt;
        /// </summary>
        /// <param name="str">带有转义html标签的字符串</param>
        /// <returns></returns>
        public static string Exp_HtmlTrim(this string str)
        {
            return str.Exp_Trim().Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">");
        }


        #region 加密解密

        #region .Net Core中的AES加密解密
        /// <summary>
        /// .Net Core中，对指定字符串AES加密
        /// 收集：邹兴武 2020-03-05
        /// </summary>
        /// <param name="input">要加密的字符串</param>
        /// <param name="key">加密密钥</param>
        /// <returns>返回加密后的字符串</returns>
        public static string Exp_AESEncrypt(this string input, string key)
        {
            var encryptKey = Encoding.UTF8.GetBytes(key);
            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(encryptKey, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor,
                            CryptoStreamMode.Write))

                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(input);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result,
                            iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }
        /// <summary>
        /// .Net Core中，对加密的字符进行解密操作
        /// 收集：邹兴武 2020-03-05
        /// </summary>
        /// <param name="input">要加密的字符串</param>
        /// <param name="key">解密密钥</param>
        /// <returns>返回解密后的字符串</returns>
        public static string Exp_AESDecrypt(this string input, string key)
        {
            var fullCipher = Convert.FromBase64String(input);

            var iv = new byte[16];
            var cipher = new byte[fullCipher.Length - iv.Length];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);
            var decryptKey = Encoding.UTF8.GetBytes(key);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(decryptKey, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt,
                            decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }


        /// <summary>
        /// .Net Core中，对字符串进行解密处理
        /// </summary>
        /// <param name="strText">待处理的字符串</param>
        /// <returns>返回明文</returns>
        public static string Exp_AESDecrypt(this string strText)
        {
            return strText.Exp_AESDecrypt("201012071715");
        }
        /// <summary>
        /// .Net Core中，对字符串进行解密处理,不会报错
        /// </summary>
        /// <param name="strText">待处理的字符串</param>
        /// <returns>返回明文</returns>
        public static string Exp_AESDecryptTry(this string strText)
        {
            try
            {
                return strText.Exp_AESDecrypt("201012071715");
            }
            catch
            {
                return "";
            }

        }
        /// <summary>
        /// .Net Core中，对字符串进行解密处理,不会报错
        /// </summary>
        /// <param name="strText">待处理的字符串</param>
        /// <returns>返回明文</returns>
        public static string Exp_AESDecryptTry(this string strText, string sDecrKey)
        {
            try
            {
                return strText.Exp_AESDecrypt(sDecrKey);
            }
            catch
            {
                return "";
            }

        }
        /// <summary>
        /// .Net Core中，对字符串进行解密处理
        /// </summary>
        /// <param name="strText">待处理的字符串</param>
        /// <returns>返回明文</returns>
        public static string Exp_AESEncrypt(this string strText)
        {
            return strText.Exp_AESEncrypt("201012071715");
        }
        #endregion

        #region .Net Framework加密解密操作

        /// <summary>
        /// 对字符串进行加密处理
        /// </summary>
        /// <param name="strText">待处理的字符串</param>
        /// <param name="strEncrKey">密钥(密钥最好使用数字,否则不可以解密)</param>
        /// <returns>返回密文</returns> 
        /// <example>
        /// 对字符串"张三加密"
        /// <code>
        ///     var s="张三".Encrypt("123");
        /// </code>
        /// </example>
        public static string Exp_Encrypt(this string strText, string strEncrKey)
        {
            //byte[] byKey = null;
            //byKey = Encoding.UTF8.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(strEncrKey, "md5").Substring(0, 8));
            //DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
            //MemoryStream ms = new MemoryStream();
            //des.Key = byKey;
            //des.IV = byKey;
            //CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            //cs.Write(inputByteArray, 0, inputByteArray.Length);
            //cs.FlushFinalBlock();
            //return Convert.ToBase64String(ms.ToArray());

            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(strText);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(strEncrKey);

            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AESEncryptBytes(bytesToBeEncrypted, passwordBytes);

            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }
        private static byte[] AESEncryptBytes(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            var saltBytes = new byte[9] { 13, 34, 27, 67, 189, 255, 104, 219, 122 };

            using (var ms = new MemoryStream())
            {
                using (var AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(32);
                    AES.IV = key.GetBytes(16);
                    AES.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(),
                        CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }
        /// <summary>
        /// 对字符串进行解密处理
        /// </summary>
        /// <param name="strText">待处理的字符串</param>
        /// <param name="sDecrKey">密钥(密钥必须是数字,否则会报错)</param>
        /// <returns>返回明文</returns>
        public static string Exp_Decrypt(this string strText, string sDecrKey)
        {
            byte[] bytesToBeDecrypted = Convert.FromBase64String(strText);

            byte[] passwordBytes = Encoding.UTF8.GetBytes(sDecrKey);

            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AESDecryptBytes(bytesToBeDecrypted, passwordBytes);

            string result = Encoding.UTF8.GetString(bytesDecrypted);

            return result;

        }
        private static byte[] AESDecryptBytes(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            var saltBytes = new byte[9] { 13, 34, 27, 67, 189, 255, 104, 219, 122 };

            using (var ms = new MemoryStream())
            {
                using (var AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(32);
                    AES.IV = key.GetBytes(16);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }

                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }
        /// <summary>
        /// 对字符串进行解密处理
        /// </summary>
        /// <param name="strText">待处理的字符串</param>
        /// <returns>返回明文</returns>
        public static string Exp_Decrypt(this string strText)
        {
            return strText.Exp_Decrypt("201012071715");
        }
        /// <summary>
        /// 对字符串进行解密处理,不会报错
        /// </summary>
        /// <param name="strText">待处理的字符串</param>
        /// <returns>返回明文</returns>
        public static string Exp_DecryptTry(this string strText)
        {
            try
            {
                return strText.Exp_Decrypt("201012071715");
            }
            catch
            {
                return "";
            }

        }
        /// <summary>
        /// 对字符串进行解密处理,不会报错
        /// </summary>
        /// <param name="strText">待处理的字符串</param>
        /// <returns>返回明文</returns>
        public static string Exp_DecryptTry(this string strText, string sDecrKey)
        {
            try
            {
                return strText.Exp_Decrypt(sDecrKey);
            }
            catch
            {
                return "";
            }

        }
        /// <summary>
        /// 对字符串进行解密处理
        /// </summary>
        /// <param name="strText">待处理的字符串</param>
        /// <returns>返回明文</returns>
        public static string Exp_Encrypt(this string strText)
        {
            return strText.Exp_Encrypt("201012071715");
        }

        #endregion

        #endregion

        #region 字符串处理方法
        /// <summary>
        /// 对某个object对象直接获取子串
        /// </summary>
        public static string Exp_SubString(this object o, int len, string stuffix)
        {
            return o.Exp_Trim().Exp_SubString(len, stuffix);
        }
        /// <summary>
        /// 获取指定字符串的字节长度
        /// </summary>
        /// <param name="str1">待处理的字符</param>
        /// <returns>当前字符的字节长度</returns>
        public static int Exp_BytLen(this string str1)
        {
            byte[] bytStr = System.Text.Encoding.Default.GetBytes(str1.Exp_Trim());
            return bytStr.Length;
        }
        /// <summary>
        /// 如果字符串的长度大于<paramref name="len"/>则从左截取,并且在最后面加<paramref name="..."/>
        /// <remarks>
        /// 此方法常用在网页上,此方法判断中文
        /// </remarks>
        /// </summary>
        /// <param name="str">带处理的字符</param>
        /// <param name="intlen">待判断的字符长度</param>
        /// <param name="strSuffix">字符后缀</param>
        /// <returns>处理后的字符串</returns>
        public static string Exp_SubString(this string str, int intlen, string strSuffix)
        {
            int intMyLen = str.Exp_BytLen();
            if (intMyLen > intlen)
            {
                return str.Exp_LeftByt(intlen) + strSuffix;
            }
            else
            {
                return str;
            }
        }
        /// <summary>
        /// 如果字符串的长度大于<paramref name="len"/>则从左截取,并且在最后面加<paramref name="..."/>
        /// <remarks>
        /// 此方法常用在网页上,此方法判断中文
        /// </remarks>
        /// </summary>
        /// <param name="str">带处理的字符</param>
        /// <param name="intlen">待判断的字符长度</param>
        /// <returns>处理后的字符串</returns>
        public static string Exp_SubString(this string str, int intlen)
        {
            return str.Exp_SubString(intlen, "...");
        }
        /// <summary>
        /// 根据字符的实际长度来从左边开始截取,截取的字符不会超过指定长度
        /// </summary>
        /// <param name="str">带操作字符串</param>
        /// <param name="len">指定长度</param>
        /// <returns>截取后的文字</returns>
        public static string Exp_LeftByt(this string str, int len)
        {
            int iLen = 0;
            var SB = new StringBuilder();

            for (var i = 0; i < str.Length; i++)
            {
                var item = str.Substring(i, 1);
                iLen += System.Text.Encoding.Default.GetBytes(item).Length;
                if (iLen > len)
                {
                    break;
                }
                SB.Append(item);
            }
            return SB.ToString();
        }

        /// <summary>
        /// 判断指定的字符串是否为空
        /// </summary>
        /// <param name="str">待判断的字符串</param>
        /// <returns>如果为""或者" "或null 都 返回true,否则返回false</returns>
        public static bool Exp_IsEmpty(this object str)
        {
            return string.IsNullOrEmpty(str.ToString().Trim());
        }

        /// <summary>
        /// 判断指定字符串str长度大于0，则返回str，否则返回替换字符串str1
        /// </summary>
        /// <param name="str">待判断的字符串</param>
        /// <param name="str1">小于等于0的的字符串</param>
        /// <returns>如果str的长度大于0则返回str否者返回str1</returns>
        public static string Exp_IsEmpty(this string str, string str1)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str1;
            }
            else
            {
                return str;
            }
        }
        /// <summary>
        /// 判断指定字符串str长度大于0，则先通过fun处理后，返回处理后的结果，否则返回替换字符串str1
        /// </summary>
        /// <param name="str">待判断的字符串</param>
        /// <param name="str1">小于等于0的的字符串</param>
        /// <param name="func">用来处理str,如果str小于0则不会执行</param>
        /// <returns>如果str的长度大于0则返回str否者返回str1</returns>
        public static string Exp_IsEmpty(this string str, string str1, Func<string, string> func)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str1;
            }
            else
            {
                return func(str);
            }
        }
        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="str">对象</param>
        /// <param name="o">参数</param>
        /// <returns></returns>
        public static string Exp_Format(this string str, params object[] o)
        {
            return string.Format(str, o);
        }
        /// <summary>
        /// 在str的左右同时加上o
        /// </summary>
        /// <param name="str">str</param>
        /// <param name="o">0</param>
        /// <returns>在str的左右同时加上o</returns>
        public static string Exp_RLFill(this string str, string o)
        {
            return o + str + o;
        }
        /// <summary>
        /// 从右边获取指定长度的字符串,如果不足长度则补充str2
        /// </summary>
        /// <param name="str1">str1</param>
        /// <param name="len">len</param>
        /// <param name="str2">str2</param>
        /// <returns>从右边获取指定长度的字符串,如果不足长度则补充str2</returns>
        public static string Exp_Right(this string str1, int len, string str2)
        {
            if (str1.Length > len)
            {
                return str1.Exp_Right(len);
            }
            else
            {
                string str3 = "";
                for (var i = 0; i < len - str1.Length; i++)
                {
                    str3 += str2;
                }
                str1 += str3;
                return str1;
            }

        }
        /// <summary>
        /// 从右边获取制定长度的字符串
        /// </summary>
        /// <param name="str1">str1</param>
        /// <param name="len">len</param>
        /// <returns>从右边获取制定长度的字符串</returns>
        public static string Exp_Right(this string str1, int len)
        {
            str1 = str1.Exp_Trim();
            int intLen = str1.Length;
            if (len <= 0)
            {
                return "";
            }
            if (len >= intLen)
            {
                return str1;
            }
            return str1.Substring(intLen - len, len);

        }
        /// <summary>
        ///从左边获取指定长度的字符串
        /// </summary>
        /// <param name="str1">str1</param>
        /// <param name="len">len</param>
        /// <returns>从左边获取指定长度的字符串</returns>
        public static string Exp_Left(this string str1, int len)
        {
            str1 = str1.Exp_Trim();
            int intLen = str1.Length;
            if (len <= 0)
            {
                return "";
            }
            if (len >= intLen)
            {
                return str1;
            }
            return str1.Substring(0, len);
        }

        #endregion

        /// <summary>
        /// 对字符串进行分割,然后直接获得对应的索引资料
        /// </summary>
        /// <param name="str">要分割的字符串</param>
        /// <param name="str1">分隔符</param>
        /// <param name="Index">索引,从0开始的</param>
        /// <returns>如果索引不正确,或者异常的返回空字符串</returns>
        public static string Exp_Split(this string str, char str1, int Index)
        {
            try
            {
                if (str.Length > 0)
                {
                    var AStr = str.Split(str1);
                    if (AStr.Length > Index)
                    {
                        return AStr[Index];
                    }
                }
            }
            catch
            { }
            return string.Empty;
        }


        /// <summary>
        /// 把一个字符串分组,转换成一个list
        /// </summary>
        /// <typeparam name="T">list中的类型</typeparam>
        /// <param name="str">要处理的字符串</param>
        /// <param name="str1">分割字符</param>
        /// <param name="func">类型转换委托</param>
        /// <returns>把一个字符串分组,转换成一个list</returns>
        public static List<T> Exp_SplitToList<T>(this string str, char str1, Func<string, T> func)
        {
            if (str.Exp_Trim().Length <= 0)
            {
                return null;
            }
            List<T> list = new List<T>();
            string[] AStr;
            try
            {
                AStr = str.Split(str1);
            }
            catch
            {
                return null;
            }

            foreach (var item in AStr)
            {
                if (func != null)
                {
                    list.Add(func(item));
                }
                else
                {
                    list.Add(item.Exp_To<T>());
                }
            }
            return list;
        }
        
        /// <summary>
        /// 生成json字符串
        /// </summary>
        /// <param name="json">原串</param>
        /// <param name="text">要添加的项名称</param>
        /// <param name="value">要添加项对应的值</param>
        public static string Exp_JsonInsert(this string json, string text, string value)
        {
            if (json.Length <= 0)
            {
                return "{" + text + ":\"" + value + "\"}";
            }
            else
            {
                var format = "," + text + ":\"" + value + "\"}";
                var json1 = json.Substring(0, json.Length - 1);
                return json1 + format;
            }
        }

        #region 汉字转拼音

        #region 数组信息

        private static int[] pyValue = new int[]
{ -20319, -20317, -20304, -20295, -20292, -20283, -20265, -20257, -20242,
    -20230, -20051, -20036, -20032, -20026, -20002, -19990, -19986, -19982,
    -19976, -19805, -19784, -19775, -19774, -19763, -19756, -19751, -19746,
    -19741, -19739, -19728, -19725, -19715, -19540, -19531, -19525, -19515,
    -19500, -19484, -19479, -19467, -19289, -19288, -19281, -19275, -19270,
    -19263, -19261, -19249, -19243, -19242, -19238, -19235, -19227, -19224,
    -19218, -19212, -19038, -19023, -19018, -19006, -19003, -18996, -18977,
    -18961, -18952, -18783, -18774, -18773, -18763, -18756, -18741, -18735,
    -18731, -18722, -18710, -18697, -18696, -18526, -18518, -18501, -18490,
    -18478, -18463, -18448, -18447, -18446, -18239, -18237, -18231, -18220,
    -18211, -18201, -18184, -18183, -18181, -18012, -17997, -17988, -17970,
    -17964, -17961, -17950, -17947, -17931, -17928, -17922, -17759, -17752,
    -17733, -17730, -17721, -17703, -17701, -17697, -17692, -17683, -17676,
    -17496, -17487, -17482, -17468, -17454, -17433, -17427, -17417, -17202,
    -17185, -16983, -16970, -16942, -16915, -16733, -16708, -16706, -16689,
    -16664, -16657, -16647, -16474, -16470, -16465, -16459, -16452, -16448,
    -16433, -16429, -16427, -16423, -16419, -16412, -16407, -16403, -16401,
    -16393, -16220, -16216, -16212, -16205, -16202, -16187, -16180, -16171,
    -16169, -16158, -16155, -15959, -15958, -15944, -15933, -15920, -15915,
    -15903, -15889, -15878, -15707, -15701, -15681, -15667, -15661, -15659,
    -15652, -15640, -15631, -15625, -15454, -15448, -15436, -15435, -15419,
    -15416, -15408, -15394, -15385, -15377, -15375, -15369, -15363, -15362,
    -15183, -15180, -15165, -15158, -15153, -15150, -15149, -15144, -15143,
    -15141, -15140, -15139, -15128, -15121, -15119, -15117, -15110, -15109,
    -14941, -14937, -14933, -14930, -14929, -14928, -14926, -14922, -14921,
    -14914, -14908, -14902, -14894, -14889, -14882, -14873, -14871, -14857,
    -14678, -14674, -14670, -14668, -14663, -14654, -14645, -14630, -14594,
    -14429, -14407, -14399, -14384, -14379, -14368, -14355, -14353, -14345,
    -14170, -14159, -14151, -14149, -14145, -14140, -14137, -14135, -14125,
    -14123, -14122, -14112, -14109, -14099, -14097, -14094, -14092, -14090,
    -14087, -14083, -13917, -13914, -13910, -13907, -13906, -13905, -13896,
    -13894, -13878, -13870, -13859, -13847, -13831, -13658, -13611, -13601,
    -13406, -13404, -13400, -13398, -13395, -13391, -13387, -13383, -13367,
    -13359, -13356, -13343, -13340, -13329, -13326, -13318, -13147, -13138,
    -13120, -13107, -13096, -13095, -13091, -13076, -13068, -13063, -13060,
    -12888, -12875, -12871, -12860, -12858, -12852, -12849, -12838, -12831,
    -12829, -12812, -12802, -12607, -12597, -12594, -12585, -12556, -12359,
    -12346, -12320, -12300, -12120, -12099, -12089, -12074, -12067, -12058,
    -12039, -11867, -11861, -11847, -11831, -11798, -11781, -11604, -11589,
    -11536, -11358, -11340, -11339, -11324, -11303, -11097, -11077, -11067,
    -11055, -11052, -11045, -11041, -11038, -11024, -11020, -11019, -11018,
    -11014, -10838, -10832, -10815, -10800, -10790, -10780, -10764, -10587,
    -10544, -10533, -10519, -10331, -10329, -10328, -10322, -10315, -10309,
    -10307, -10296, -10281, -10274, -10270, -10262, -10260, -10256, -10254
};
        private static string[] pyName = new string[]
 { "A", "Ai", "An", "Ang", "Ao", "Ba", "Bai", "Ban", "Bang", "Bao", "Bei",
     "Ben", "Beng", "Bi", "Bian", "Biao", "Bie", "Bin", "Bing", "Bo", "Bu",
     "Ba", "Cai", "Can", "Cang", "Cao", "Ce", "Ceng", "Cha", "Chai", "Chan",
     "Chang", "Chao", "Che", "Chen", "Cheng", "Chi", "Chong", "Chou", "Chu",
     "Chuai", "Chuan", "Chuang", "Chui", "Chun", "Chuo", "Ci", "Cong", "Cou",
     "Cu", "Cuan", "Cui", "Cun", "Cuo", "Da", "Dai", "Dan", "Dang", "Dao", "De",
     "Deng", "Di", "Dian", "Diao", "Die", "Ding", "Diu", "Dong", "Dou", "Du",
     "Duan", "Dui", "Dun", "Duo", "E", "En", "Er", "Fa", "Fan", "Fang", "Fei",
     "Fen", "Feng", "Fo", "Fou", "Fu", "Ga", "Gai", "Gan", "Gang", "Gao", "Ge",
     "Gei", "Gen", "Geng", "Gong", "Gou", "Gu", "Gua", "Guai", "Guan", "Guang",
     "Gui", "Gun", "Guo", "Ha", "Hai", "Han", "Hang", "Hao", "He", "Hei", "Hen",
     "Heng", "Hong", "Hou", "Hu", "Hua", "Huai", "Huan", "Huang", "Hui", "Hun",
     "Huo", "Ji", "Jia", "Jian", "Jiang", "Jiao", "Jie", "Jin", "Jing", "Jiong",
     "Jiu", "Ju", "Juan", "Jue", "Jun", "Ka", "Kai", "Kan", "Kang", "Kao", "Ke",
     "Ken", "Keng", "Kong", "Kou", "Ku", "Kua", "Kuai", "Kuan", "Kuang", "Kui",
     "Kun", "Kuo", "La", "Lai", "Lan", "Lang", "Lao", "Le", "Lei", "Leng", "Li",
     "Lia", "Lian", "Liang", "Liao", "Lie", "Lin", "Ling", "Liu", "Long", "Lou",
     "Lu", "Lv", "Luan", "Lue", "Lun", "Luo", "Ma", "Mai", "Man", "Mang", "Mao",
     "Me", "Mei", "Men", "Meng", "Mi", "Mian", "Miao", "Mie", "Min", "Ming", "Miu",
     "Mo", "Mou", "Mu", "Na", "Nai", "Nan", "Nang", "Nao", "Ne", "Nei", "Nen",
     "Neng", "Ni", "Nian", "Niang", "Niao", "Nie", "Nin", "Ning", "Niu", "Nong",
     "Nu", "Nv", "Nuan", "Nue", "Nuo", "O", "Ou", "Pa", "Pai", "Pan", "Pang",
     "Pao", "Pei", "Pen", "Peng", "Pi", "Pian", "Piao", "Pie", "Pin", "Ping",
     "Po", "Pu", "Qi", "Qia", "Qian", "Qiang", "Qiao", "Qie", "Qin", "Qing",
     "Qiong", "Qiu", "Qu", "Quan", "Que", "Qun", "Ran", "Rang", "Rao", "Re",
     "Ren", "Reng", "Ri", "Rong", "Rou", "Ru", "Ruan", "Rui", "Run", "Ruo",
     "Sa", "Sai", "San", "Sang", "Sao", "Se", "Sen", "Seng", "Sha", "Shai",
     "Shan", "Shang", "Shao", "She", "Shen", "Sheng", "Shi", "Shou", "Shu",
     "Shua", "Shuai", "Shuan", "Shuang", "Shui", "Shun", "Shuo", "Si", "Song",
     "Sou", "Su", "Suan", "Sui", "Sun", "Suo", "Ta", "Tai", "Tan", "Tang",
     "Tao", "Te", "Teng", "Ti", "Tian", "Tiao", "Tie", "Ting", "Tong", "Tou",
     "Tu", "Tuan", "Tui", "Tun", "Tuo", "Wa", "Wai", "Wan", "Wang", "Wei",
     "Wen", "Weng", "Wo", "Wu", "Xi", "Xia", "Xian", "Xiang", "Xiao", "Xie",
     "Xin", "Xing", "Xiong", "Xiu", "Xu", "Xuan", "Xue", "Xun", "Ya", "Yan",
     "Yang", "Yao", "Ye", "Yi", "Yin", "Ying", "Yo", "Yong", "You", "Yu",
     "Yuan", "Yue", "Yun", "Za", "Zai", "Zan", "Zang", "Zao", "Ze", "Zei",
     "Zen", "Zeng", "Zha", "Zhai", "Zhan", "Zhang", "Zhao", "Zhe", "Zhen",
     "Zheng", "Zhi", "Zhong", "Zhou", "Zhu", "Zhua", "Zhuai", "Zhuan",
     "Zhuang", "Zhui", "Zhun", "Zhuo", "Zi", "Zong", "Zou", "Zu", "Zuan",
     "Zui", "Zun", "Zuo" };
        #endregion

        #region 方法调用_只有一个参数
        /// <summary>
        /// 把汉字转换成拼音
        /// </summary>
        /// <param name="hzString">汉字</param>
        /// <returns>拼音</returns>
        public static string Exp_PingYing(this string hzString)
        {
            return hzString.Exp_PingYing(10);
        }

        #endregion

        #region 方法调用_有两个参数
        /// <summary>
        /// 把汉字换成拼音
        /// </summary>
        /// <param name="hzString">汉字</param>
        /// <param name="maxLength">长度</param>
        /// <returns>拼音</returns>
        public static string Exp_PingYing(this string hzString, int maxLength)
        {
            char str = '"';//英文状态双引号处理

            if (string.IsNullOrEmpty(hzString))//输入为空
                return null;

            if (maxLength <= 1)
                maxLength = 10;

            //字符处理
            hzString = hzString.Trim().Replace(" ", "").Replace("?", "_").Replace("\\", "_").Replace("/", "_").Replace(":", "").Replace("*", "").Replace(">", "").Replace("<", "").Replace("?", "").Replace("|", "").Replace("\"", "'").Replace("(", "_").Replace(")", "_").Replace(";", "_");
            hzString = hzString.Replace("，", ",").Replace(str.ToString(), "").Replace(str.ToString(), "").Replace("；", "_").Replace("。", "_").Replace("[", "").Replace("]", "").Replace("【", "").Replace("】", "");
            hzString = hzString.Replace("{", "").Replace("}", "").Replace("^", "").Replace("&", "_").Replace("=", "").Replace("~", "_").Replace("@", "_").Replace("￥", "");
            if (hzString.Length > maxLength)
            {
                hzString = hzString.Substring(0, maxLength);
            }
            Regex regex = new Regex(@"([a-zA-Z0-9\._]+)", RegexOptions.IgnoreCase);
            if (regex.IsMatch(hzString))
            {
                if (hzString.Equals(regex.Match(hzString).Groups[1].Value, StringComparison.OrdinalIgnoreCase))
                {
                    return hzString;
                }
            }
            // 匹配中文字符          
            regex = new Regex("^[\u4e00-\u9fa5]$");
            byte[] array = new byte[2];
            string pyString = "";
            int chrAsc = 0;
            int i1 = 0;
            int i2 = 0;
            char[] noWChar = hzString.ToCharArray();
            for (int j = 0; j < noWChar.Length; j++)
            {// 中文字符          
                if (regex.IsMatch(noWChar[j].ToString()))
                {
                    array = System.Text.Encoding.Default.GetBytes(noWChar[j].ToString());
                    i1 = (short)(array[0]);
                    i2 = (short)(array[1]);
                    chrAsc = i1 * 256 + i2 - 65536;
                    if (chrAsc > 0 && chrAsc < 160)
                    {
                        pyString += noWChar[j];
                    }
                    else
                    {
                        // 修正部分文字          
                        if (chrAsc == -9254)  // 修正"圳"字      
                            pyString += "Zhen";
                        else
                        {
                            for (int i = (pyValue.Length - 1); i >= 0; i--)
                            {
                                if (pyValue[i] <= chrAsc)
                                {
                                    pyString += pyName[i];
                                    break;
                                }
                            }
                        }
                    }
                }
                // 非中文字符      
                else
                {
                    pyString += noWChar[j].ToString();
                }
            }
            return pyString;
        }

        #endregion

        #region 金额转汉字
        private static readonly String cnNumber = "零壹贰叁肆伍陆柒捌玖";
        private static readonly String cnUnit = "分角元拾佰仟万拾佰仟亿拾佰仟兆拾佰仟";


        // 以下是货币金额中文大写转换方法
        /// <summary>
        /// 货币金额中文大写转换方法
        /// </summary>
        /// <param name="MoneyString">阿拉伯货币</param>
        /// <returns>中文货币</returns>
        public static String Exp_GetMoneyCN(this String MoneyString)
        {
            String[] tmpString = MoneyString.Split('.');
            String intString = MoneyString;   // 默认为整数
            String decString = "";            // 保存小数部分字串
            String rmbCapital = "";            // 保存中文大写字串
            int k;
            int j;
            int n;

            if (tmpString.Length > 1)
            {
                intString = tmpString[0];             // 取整数部分
                decString = tmpString[1];             // 取小数部分
            }
            decString += "00";
            decString = decString.Substring(0, 2);   // 保留两位小数位
            intString += decString;

            try
            {
                k = intString.Length - 1;
                if (k > 0 && k < 18)
                {
                    for (int i = 0; i <= k; i++)
                    {
                        j = (int)intString[i] - 48;
                        // rmbCapital = rmbCapital + cnNumber[j] + cnUnit[k-i];     // 供调试用的直接转换
                        n = i + 1 >= k ? (int)intString[k] - 48 : (int)intString[i + 1] - 48; // 等效于 if( ){ }else{ }
                        if (j == 0)
                        {
                            if (k - i == 2 || k - i == 6 || k - i == 10 || k - i == 14)
                            {
                                rmbCapital += cnUnit[k - i];
                            }
                            else
                            {
                                if (n != 0)
                                {
                                    rmbCapital += cnNumber[j];
                                }
                            }
                        }
                        else
                        {
                            rmbCapital = rmbCapital + cnNumber[j] + cnUnit[k - i];
                        }
                    }

                    rmbCapital = rmbCapital.Replace("兆亿万", "兆");
                    rmbCapital = rmbCapital.Replace("兆亿", "兆");
                    rmbCapital = rmbCapital.Replace("亿万", "亿");
                    rmbCapital = rmbCapital.TrimStart('元');
                    rmbCapital = rmbCapital.TrimStart('零');

                    return rmbCapital;
                }
                else
                {
                    return "";   // 超出转换范围时，返回零长字串
                }
            }
            catch
            {
                return "";   // 含有非数值字符时，返回零长字串
            }
        }

        #endregion

        #endregion




    }
}





