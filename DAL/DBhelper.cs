using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{

    class DBhelper
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        static SqlConnection con = new SqlConnection("server=.;database=EateryDB;uid=sa;pwd=123");
        /// <summary>
        /// 增删改的方法
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="sp">参数化</param>
        /// <returns>执行结果</returns>
        public static int MyExecuteNonQuery(string sql, SqlParameter[] sp)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(sql,con);
            if(sp!=null)
            cmd.Parameters.AddRange(sp);
         int count=   cmd.ExecuteNonQuery();
         con.Close();
         return count;

        }
        /// <summary>
        /// 白道查询
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="sp">参数化</param>
        /// <returns>返回读取器</returns>
        public static SqlDataReader MySqlDataReader(string sql, SqlParameter[] sp)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            if (sp != null)
                cmd.Parameters.AddRange(sp);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            
            return dr;
        
        }
        public static SqlDataReader MySqlDataReaderProc(string proc, SqlParameter[] sp)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (sp != null)
                cmd.Parameters.AddRange(sp);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            return dr;

        }
        /// <summary>
        /// 单值查询
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="sp">参数化</param>
        /// <returns>ExecuteScalar对象</returns>
        public static object MyExecuteScalar(string sql, SqlParameter[] sp)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            if (sp != null)
                cmd.Parameters.AddRange(sp);
          object sr=  cmd.ExecuteScalar();
            con.Close();
            return sr;
        
        }
        public static DataTable dd(string sql, SqlParameter[] sp)
        {
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            return dt;
                    
        }
    }
}
