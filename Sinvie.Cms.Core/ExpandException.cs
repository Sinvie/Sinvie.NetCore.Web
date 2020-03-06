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

namespace Sinvie
{
    public static class ExpandException
    {
        /// <summary>
        /// 系统错误时保存错误日志
        /// </summary>
        public static void Exp_WriteErrorLog(this Exception err)
        {
            string error = string.Empty;
            string errortime = string.Empty;
            string erroraddr = string.Empty;
            string errorinfo = string.Empty;
            string errorsource = string.Empty;
            string errortrace = string.Empty;

            error += "发生时间:" + System.DateTime.Now.ToString() + "\r\n";
            errortime = "发生时间:" + System.DateTime.Now.ToString();

            error += "异常信息: " + err.Message + "\r\n";
            errorinfo = "异常信息: " + err.Message;

            errorsource = "错误源:" + err.Source;
            errortrace = "堆栈信息:" + err.StackTrace;
            error += "---------------------------------------------------\r\n";
            HttpContext.Current.Server.ClearError();

            //独占方式，因为文件只能由一个进程写入.
            System.IO.StreamWriter writer = null;
            try
            {
                // 写入日志
                string year = DateTime.Now.ToString("yyyyMM");
                string path = string.Empty;
                string filename = System.DateTime.Now.ToString("yyyyMMddHHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + ".txt";
                path = HttpContext.Current.Server.MapPath("/errorlog/") + year + "/";
                //如果目录不存在则创建
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                System.IO.FileInfo file = new System.IO.FileInfo(path + "/" + filename);
                writer = new System.IO.StreamWriter(file.FullName, true);//文件不存在就创建,true表示追加
                writer.WriteLine(errortime);
                writer.WriteLine(erroraddr);
                writer.WriteLine(errorinfo);
                writer.WriteLine(errorsource);
                writer.WriteLine(errortrace);
                writer.WriteLine("--------------------------------------------------------------------------------------");
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
