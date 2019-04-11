using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDL;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class AdminsDAL
    {
        public static List<AdminsMDL> GetLogin(string user)
        {
            string sql = "select * from admins where Username=@username";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@username", user));
           
            SqlDataReader dr =  DBhelper.MySqlDataReader(sql, list.ToArray());
            List<AdminsMDL> ad=new List<AdminsMDL>();
            while(dr.Read()){
                AdminsMDL adm = new AdminsMDL();
                adm.userid = Convert.ToInt32(dr[0]);
                adm.UserName = dr[1].ToString();
                adm.UserPWD = dr[2].ToString();
                adm.UserCompellation = dr[3].ToString();
                adm.LoginType = Convert.ToInt32(dr[6]);
                adm.headID = Convert.ToInt32(dr[7]);
                ad.Add(adm);
            }
            dr.Close();
            return ad;
        }
        public static int logintype(AdminsMDL a)
        {
            string sql = "update admins set LoginType=@logintype where username=@username ";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@username", a.UserName));
          
            list.Add(new SqlParameter("@logintype", a.LoginType));
            return Convert.ToInt32(DBhelper.MyExecuteNonQuery(sql, list.ToArray()));
        }
        public static string selectpwd(AdminsMDL a)
        {
            string sql = "select usercompellation from admins where username=@username and userpwd=@userpwd";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@username", a.UserName));
            list.Add(new SqlParameter("@userpwd", a.UserPWD));
            return DBhelper.MyExecuteScalar(sql, list.ToArray()).ToString();
        }
        public static int updatepwd(AdminsMDL a, AdminsMDL aa)
        {
            string sql = "update admins set userpwd=@Uerpwd where username=@username and userpwd=@Userpwd";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@username", a.UserName));
            list.Add(new SqlParameter("@userpwd", a.UserPWD));
            list.Add(new SqlParameter("@Uerpwd", aa.UserPWD));
            return DBhelper.MyExecuteNonQuery(sql, list.ToArray());
        }
        //lcy
        public static List<AdminsMDL> Getlist(AdminsMDL a)
        {
            string sql = "select * from admins where 1=1";
            List<SqlParameter> list = new List<SqlParameter>();
            List<AdminsMDL> list1 = new List<AdminsMDL>();
            if (a != null)
            {
                if (a.userid > 0)
                {
                    sql += " and userid=@userid";
                    list.Add(new SqlParameter("@username", a.UserName));
                }
            }
            using (SqlDataReader sda = DBhelper.MySqlDataReader(sql, list.ToArray()))
            {
                while (sda.Read())
                {
                    AdminsMDL ad = new AdminsMDL();
                    ad.userid = Convert.ToInt32(sda[0]);
                    ad.UserName = sda[1].ToString();
                    ad.UserCompellation = sda[3].ToString();
                    list1.Add(ad);
                }
                return list1;
            }
        }
        public static int insertadmin(AdminsMDL ad)
        {
            string sql = "insert into admins values(@username,@userpwd,@usercompellation,1,1,1,1)";
            SqlParameter[] spt ={new SqlParameter("@username",ad.UserName),
                                 new SqlParameter("@userpwd",ad.UserPWD),
                                new SqlParameter("@usercompellation",ad.UserCompellation)};
            return DBhelper.MyExecuteNonQuery(sql, spt);
        }
        public static int updateadmin(AdminsMDL a)
        {
            string sql = "update admins set username=@username,usercompellation=@usercompellation where userid=@userid";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@username", a.UserName));
            list.Add(new SqlParameter("@usercompellation", a.UserCompellation));
            list.Add(new SqlParameter("@userid", a.userid));
            return DBhelper.MyExecuteNonQuery(sql, list.ToArray());
        }
        public static int deleteadmin(AdminsMDL a)
        {
            string sql = "delete from admins where userid=@userid";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@userid", a.userid));
            return DBhelper.MyExecuteNonQuery(sql, list.ToArray());
        }
    }
}
