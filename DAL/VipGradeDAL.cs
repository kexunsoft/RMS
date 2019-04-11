using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDL;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class VipGradeDAL
    {
        public static List<VipGradeMDL> getlist(VipGradeMDL vg)
        {
            string sql = "select * from vipgrade where 1=1";
            List<SqlParameter> list = new List<SqlParameter>();
            List<VipGradeMDL> list1 = new List<VipGradeMDL>();
            if (vg != null)
            {
                if (vg.vgid > 0)
                {
                    sql += " and vgid=@vgid";
                    list.Add(new SqlParameter("@vgid", vg.vgid));
                }
            }
            using (SqlDataReader sda = DBhelper.MySqlDataReader(sql, list.ToArray()))
            {
                while (sda.Read())
                {
                    VipGradeMDL v = new VipGradeMDL();
                    v.vgid = Convert.ToInt32(sda[0]);
                    v.vgname = sda[1].ToString();
                    v.vgdiscount = Convert.ToDouble(sda[2]);
                    list1.Add(v);
                }
                return list1;
            }
        }
        public static int insertvg(VipGradeMDL vg)
        {
            string sql = "insert into vipgrade values(@vgname,@vgdiscount)";
            SqlParameter[] spt ={
                                new SqlParameter("@vgname",vg.vgname),
                                new SqlParameter("@vgdiscount",vg.vgdiscount)
                                };
            return DBhelper.MyExecuteNonQuery(sql, spt);
        }
        public static int updatevg(VipGradeMDL vg)
        {
            string sql = "update vipgrade set vgname=@vgname,vgdiscount=@vgdiscount where vgid=@vgid";
            SqlParameter[] spt ={
                                new SqlParameter("@vgname",vg.vgname),
                                new SqlParameter("@vgdiscount",vg.vgdiscount),
                                new SqlParameter("@vgid",vg.vgid)
                                };
            return DBhelper.MyExecuteNonQuery(sql, spt);
        }
        public static int deletevg(VipGradeMDL vg)
        {
            string sql = "delete from vipgrade where vgid=@vgid";
            SqlParameter[] spt ={
                                
                                new SqlParameter("@vgid",vg.vgid)
                                };
            return DBhelper.MyExecuteNonQuery(sql, spt);
        }
    }
}
