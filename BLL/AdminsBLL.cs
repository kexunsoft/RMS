using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using MDL;

namespace BLL
{
    public class AdminsBLL
    {
        public static List<AdminsMDL> GetLogin(string a)
        {
            return AdminsDAL.GetLogin(a);
        }
        public static int logintype(AdminsMDL a)
        {
            return AdminsDAL.logintype(a);
        }
        public static string selectpwd(AdminsMDL a)
        {
            return AdminsDAL.selectpwd(a);
        }
        public static int updatepwd(AdminsMDL aa, AdminsMDL am)
        {
            return AdminsDAL.updatepwd(aa, am);
        }
        //lcy
        public static List<AdminsMDL> getlist(AdminsMDL a)
        {
            return AdminsDAL.Getlist(a);
        }
        public static int insertadmin(AdminsMDL a)
        {
            return AdminsDAL.insertadmin(a);
        }
        public static int updateadmin(AdminsMDL a)
        {
            return AdminsDAL.updateadmin(a);
        }
        public static int deleteadmin(AdminsMDL a)
        {
            return AdminsDAL.deleteadmin(a);
        }
    }
}
