using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDL;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class VipsDAL
    {
        //查询会员信息
        public static List<VipsMDL> GetVip(int VipID) {
            string sql = "select * from Vips v left join dbo.VIPGrade g on v.GradeID=g.VGID where 1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (VipID!=0)
            {
                sql += " and VipID=@VipID and VipEndDate>=GETDATE()";
                sp.Add(new SqlParameter("@VipID", VipID));
            }
          
           SqlDataReader dr= DBhelper.MySqlDataReader(sql,sp.ToArray());
           List<VipsMDL> list = new List<VipsMDL>();
            while(dr.Read()){
                VipsMDL v = new VipsMDL();
                v.VipID = Convert.ToInt32(dr[0]);
                v.VipName = dr[1].ToString();
                v.VipSex = dr[2].ToString();
                v.GradeID = Convert.ToInt32(dr[3]);
                v.VipTel = dr[4].ToString();
                v.VipStartDate = dr[5].ToString();
                v.VipEndDate = dr[6].ToString();
              //
                   // v.VipType = Convert.ToInt32(dr[7]);
                
              
                if (dr[8].ToString() == "")
                {
                    v.VGName = "其他会员";
                }
                else {
                    v.VGName = dr[8].ToString();
                }
               
                if (dr[9].ToString() == "")
                {
                    v.VGDiscount = 1.0;
                }
                else {
                    v.VGDiscount = Convert.ToDouble(dr[9]);
                }
               
                list.Add(v);
            }
            dr.Close();
            return list;
        }
        public static List<VipsMDL> GetVips(int VipID)
        {
            string sql = "select * from Vips v left join dbo.VIPGrade g on v.GradeID=g.VGID where 1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (VipID != 0)
            {
                sql += " and VipID=@VipID";
                sp.Add(new SqlParameter("@VipID", VipID));
            }

            SqlDataReader dr = DBhelper.MySqlDataReader(sql, sp.ToArray());
            List<VipsMDL> list = new List<VipsMDL>();
            while (dr.Read())
            {
                VipsMDL v = new VipsMDL();
                v.VipID = Convert.ToInt32(dr[0]);
                v.VipName = dr[1].ToString();
                v.VipSex = dr[2].ToString();
                v.GradeID = Convert.ToInt32(dr[3]);
                v.VipTel = dr[4].ToString();
                v.VipStartDate = dr[5].ToString();
                v.VipEndDate = dr[6].ToString();

                //v.VipType = Convert.ToInt32(dr[7]);


                if (dr[8].ToString() == "")
                {
                    v.VGName = "其他会员";
                }
                else
                {
                    v.VGName = dr[8].ToString();
                }

                if (dr[9].ToString() == "")
                {
                    v.VGDiscount = 1.0;
                }
                else
                {
                    v.VGDiscount = Convert.ToDouble(dr[9]);
                }

                list.Add(v);
            }
            dr.Close();
            return list;
        }
        public static int AddVip(VipsMDL v) {

            string sql = "insert into Vips values(@VipName,@VipSex,@GradeID,@VipTel,GETDATE(),@VipEndDate)";
            SqlParameter[] sp = { 
                                new SqlParameter("@VipName",v.VipName),
                                new SqlParameter("@VipSex",v.VipSex),
                                new SqlParameter("@GradeID",v.GradeID),
                                new SqlParameter("@VipTel",v.VipTel),
                                new SqlParameter("@VipEndDate",v.VipEndDate),

                                };
            return DBhelper.MyExecuteNonQuery(sql,sp);
        }
        public static int GetMaxID() {
            string sql = "select max(VipID) from dbo.Vips";
            object o = DBhelper.MyExecuteScalar(sql,null);
            if (!(o is DBNull))
            {
                return Convert.ToInt32(o);
            }
            else {
                return 0;
            }
              
           
           
        }
        public static int DeleteVip(int VipID) {
            string sql = "delete from Vips where VipID=@VipID";
            SqlParameter[] sp = { 
                                new SqlParameter("@VipID",VipID)
                                };
            return DBhelper.MyExecuteNonQuery(sql,sp);
        }
        public static DataTable GetJL() {
            string sql = "select v.VipID,VipName,CBID,CBSale from dbo.ConsumerBill c  join Vips v on c.VipID=v.VipID";
            return DBhelper.dd(sql, null);
        
        }
        public static int UpdateVip(VipsMDL v) {
            string sql = "update Vips set VipName=@VipName,VipSex=@VipSex,GradeID=@GradeID,VipTel=@VipTel,VipEndDate=@VipEndDate where VipID=@VipID";
            SqlParameter[] sp = { 
                               new SqlParameter("@VipName",v.VipName),
                               new SqlParameter("@VipSex",v.VipSex),
                               new SqlParameter("@VipTel",v.VipTel),
                               new SqlParameter("@VipEndDate",v.VipEndDate),
                               new SqlParameter("@GradeID",v.GradeID),
                               new SqlParameter("@VipID",v.VipID)

                                };
            return DBhelper.MyExecuteNonQuery(sql,sp);

        }
    }
}
