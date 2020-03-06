//======================================================================
//
//        Copyright (C) 贵州宝玉科技    
//        All rights reserved
//
//        filename :ExpandMSSQL
//        description :
//
//        created by 周济 at  2010-04-06 15:28:40
//        mail:13912699233@139.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Sinvie
{
    /// <summary>
    /// mssql资料处理
    /// </summary>
    public static class ExpandMSSQL
    {
        /// <summary>
        /// 产生分页sql语句
        /// </summary>
        /// <param name="Begin">起始记录</param>
        /// <param name="PageSize">每页数量</param>
        /// <param name="SelectList">结果</param>
        /// <param name="Table">查询主体</param>
        /// <param name="Key">主键资料</param>
        /// <param name="Where">查询条件,可以为空</param>
        /// <param name="Orderby">排序条件,如果为空则自动为key</param>
        /// <returns>sql</returns>
        public static string GetPageSQL(int Begin, int PageSize, string SelectList, string Table, string Key, string Where, string Orderby)
        {
            Orderby = Orderby.Exp_IsEmpty(Key);
            StringBuilder SQL = new StringBuilder();
            SQL.Append(" select top ");
            SQL.Append(PageSize);
            SQL.Append("   ");
            SQL.Append(SelectList);
            SQL.Append("  from  ");
            SQL.Append(Table);
            SQL.Append("   where    ");
            if (Where.Exp_Trim().Length > 0)
            {
                SQL.Append(Where);
                SQL.Append("  and   ");
            }
            SQL.Append(Key);
            SQL.Append("   not in (   ");
            SQL.Append("  select top   ");
            SQL.Append(Begin);
            SQL.Append("  ");
            SQL.Append(Key);
            SQL.Append("  from  ");
            SQL.Append(Table);
            if (Where.Exp_Trim().Length > 0)
            {
                SQL.Append("  where    ");
                SQL.Append(Where);
            }
            SQL.Append("  order by  ");
            SQL.Append(Orderby);
            SQL.Append(" )  order by ");
            SQL.Append(Orderby);
            SQL.Append("  ");

            //if (SoleUser.User.userAdmin)
            //{
            //    StringBuilder sb = new StringBuilder();
            //    sb.Append("SQL语句:");
            //    sb.AppendLine(SQL.ToString());
            //    sb.AppendLine();
            //    string.Format(@"c:\zjSQL\Sql{0}.txt", DateTime.Now.ToString("yyyyMMdd")).Exp_FileWrite(sb);
            //}

            return SQL.ToString();
        }

        /// <summary>
        /// 取得SQL2005以上数据库的分页SQL语句
        /// </summary>
        /// <param name="Begin">每页数据大小</param>
        /// <param name="PageSize">页显示记录数</param>
        /// <param name="Table">表名或视图名</param>
        /// <param name="SelectList">查询的字段，默认所有</param>
        /// <param name="Where">查询条件</param>
        /// <param name="OrderBy">排序方式，默认ID倒序,无ID的一定要填写该字段,否则出错</param>
        /// <returns>生成的分页SQ语句L</returns>
        public static string GetPageSQL(int Begin, int PageSize, string Table, string SelectList, string Where, string OrderBy)
        {
            SelectList = string.IsNullOrEmpty(SelectList) ? "*" : SelectList;
            OrderBy = string.IsNullOrEmpty(OrderBy) ? "ID DESC" : OrderBy;
            var OrderBy1 =OrderBy.Contains(".") ? OrderBy.Exp_Split('.', 1) : OrderBy;
            if (OrderBy.Contains(","))
            {
                var arr2 = new List<string>();
                var arr1 = OrderBy.Exp_SplitToList(',', p => p);
                foreach (var s in arr1)
                { 
                    arr2.Add(s.Contains(".") ? s.Exp_Split('.', 1) : s);
                }
                OrderBy1 = arr2.Exp_JoinList(",", p => p);
            }
            string sql = @"SELECT *
                            FROM (SELECT ROW_NUMBER() OVER(ORDER BY {3}) AS rownum,
                                    {1}
                                  FROM {0} where {2}) AS D
                            WHERE rownum> " + Begin + " AND rownum<=" + (Begin + PageSize) + " ORDER BY {4}";
            return string.Format(sql, Table, SelectList, Where, OrderBy, OrderBy1);
        }


        /// <summary>
        /// 获得分页总记录数的sql语句
        /// </summary>
        /// <param name="Table">查询主体</param>
        /// <param name="Where">查询条件</param>
        /// <returns>sql</returns>
        public static string GetPageSQL(string Table, string Where)
        {
            StringBuilder SQL = new StringBuilder();
            SQL.Append(" select count(1) from ");
            SQL.Append(Table);
            if (Where.Exp_Trim().Length > 0)
            {
                SQL.Append(" where  ");
                SQL.Append(Where);
            }
            return SQL.ToString();
        }
        /// <summary>
        /// 判断相关的值有没有重复的
        /// </summary>
        /// <param name="Table">表名</param>
        /// <param name="id">主键值</param>
        /// <param name="strName">字段名</param>
        /// <param name="strNameValue">字段对应值</param>
        /// <param name="intLen">字段长度</param>
        /// <param name="Where">其他条件,如有需求 请务必加上 and </param>
        /// <returns>执行的结果</returns>
        public static bool GetExistNameSql_Go(string Table, int id, string strName, string strNameValue, int intLen, string Where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from  ");
            strSql.AppendLine(Table);
            strSql.Append(" where  flag=1 and ");
            strSql.Append(strName + "=@Name ");
            strSql.Append(Where);
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,intLen),
                    new SqlParameter("@ID",SqlDbType.Int,4)
                                        };
            parameters[0].Value = strNameValue;
            parameters[1].Value = id;
            if (id > 0)
            {
                strSql.Append(" and id<>@ID ");
            }

            return MSSQLHelper.SQLConType.C1.Exp_Exists(strSql.ToString());
        }
        /// <summary>
        /// 判断相关的值有没有重复的
        /// </summary>
        /// <param name="Table">表名</param>
        /// <param name="id">主键值</param>
        /// <param name="strName">字段名</param>
        /// <param name="strNameValue">字段对应值</param>
        /// <param name="sdbtype">字段长度</param>
        /// <param name="Where">其他条件,如有需求 请务必加上 and </param>
        /// <returns>执行的结果</returns>
        public static bool GetExistNameSql_Go(string Table, int id, string strName, object strNameValue, SqlDbType sdbtype, string Where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from  ");
            strSql.AppendLine(Table);
            strSql.Append(" where  flag=1 and ");
            strSql.Append(strName + "=@Name ");
            strSql.Append(Where);
            SqlParameter[] parameters = {
					new SqlParameter("@Name",sdbtype),
                    new SqlParameter("@ID",SqlDbType.Int,4)
                                        };
            parameters[0].Value = strNameValue;
            parameters[1].Value = id;
            if (id > 0)
            {
                strSql.Append(" and id<>@ID ");
            }

            return MSSQLHelper.SQLConType.C1.Exp_Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 判断相关的值有没有重复的(只接受一个参数,参数名称为@Name)
        /// </summary>
        /// <param name="Table">表名</param>
        /// <param name="id">主键</param>
        /// <param name="strNameSQL">关键的查询语句(只接受一个参数,参数名称为@Name)</param>
        /// <param name="NameValue">查询语句中的资料的值</param>
        /// <param name="sqlType">类型</param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public static bool GetExistObjSql_Go(string Table, int id, string strNameSQL, object NameValue, SqlDbType sqlType, string Where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from  ");
            strSql.AppendLine(Table);
            strSql.Append(" where  flag=1 and ");
            strSql.Append(strNameSQL);
            strSql.Append(Where);
            SqlParameter[] parameters = {
					new SqlParameter("@Name", sqlType),
                    new SqlParameter("@ID",SqlDbType.Int,4)
                                        };
            parameters[0].Value = NameValue;
            parameters[1].Value = id;
            if (id > 0)
            {
                strSql.Append(" and id<>@ID ");
            }

            return MSSQLHelper.SQLConType.C1.Exp_Exists(strSql.ToString(), parameters);
        }
    }
}
