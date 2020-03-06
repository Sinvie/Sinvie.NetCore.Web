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
    public static class ExpandInt
    {
        ///  <summary>
        ///  生成随机码
        ///  </summary>
        ///  <param  name="Length">随机码个数</param>
        public static string Exp_Random(this int Length)
        {
            int rand;
            char code;
            string randomcode = String.Empty;
            //生成一定长度的验证码
            System.Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                rand = random.Next();
                if (rand % 3 == 0)
                {
                    code = (char)('A' + (char)(rand % 26));
                }
                else
                {
                    code = (char)('0' + (char)(rand % 10));
                }
                randomcode += code.ToString();
            }
            return randomcode;
        }
        ///  <summary>
        ///  生成随机码,随机码类型：0=字母和数字混合,1=数字,2=字母
        ///  </summary>
        ///  <param  name="Length">随机码个数</param>
        ///  <param  name="iType">随机码类型：0=字母和数字混合,1=数字,2=字母</param>
        public static string Exp_Random(this int Length, int iType)
        {
            int rand;
            char code;
            string randomcode = String.Empty;
            //生成一定长度的验证码
            System.Random random = new Random();
            switch (iType)
            {
                case 1:
                    for (int i = 0; i < Length; i++)
                    {
                        rand = random.Next();
                        code = (char)('0' + (char)(rand % 10));
                        randomcode += code.ToString();
                    }
                    break;
                case 2:
                    for (int i = 0; i < Length; i++)
                    {
                        rand = random.Next();
                        code = (char)('A' + (char)(rand % 26));
                        randomcode += code.ToString();
                    }
                    break;
                default:
                    for (int i = 0; i < Length; i++)
                    {
                        rand = random.Next();
                        if (rand % 3 == 0)
                        {
                            code = (char)('A' + (char)(rand % 26));
                        }
                        else
                        {
                            code = (char)('0' + (char)(rand % 10));
                        }
                        randomcode += code.ToString();
                    }
                    break;
            }

            return randomcode;
        }
    }
}
