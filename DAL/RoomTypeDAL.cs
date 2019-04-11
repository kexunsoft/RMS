using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MDL;

namespace DAL
{
    public class RoomTypeDAL
    {
        /// <summary>
        /// 查询房间
        /// </summary>
        /// <returns>l房间对象集合</returns>
        public static List<RoomTypeMDL> selectRoom() {
            string sql = "select * from RoomType";
            
          SqlDataReader dr=  DBhelper.MySqlDataReader(sql,null);
          List<RoomTypeMDL> list = new List<RoomTypeMDL>();
            while(dr.Read()){
                RoomTypeMDL rt = new RoomTypeMDL();
                rt.RTID = Convert.ToInt32(dr[0]);
                rt.RTName = dr[1].ToString();
                rt.RTConsume = Convert.ToDouble(dr[2]);
                //rt.RTIsDisCount = Convert.ToInt32(dr[3]);
                rt.RTMount = Convert.ToInt32(dr[4]);
                list.Add(rt);
            }
            dr.Close();
            return list;
        }
        //梁晨煜
        public static List<RoomTypeMDL> getlist()
        {
            string sql = "select * from roomtype ";
            List<RoomTypeMDL> list = new List<RoomTypeMDL>();

            using (SqlDataReader sda = DBhelper.MySqlDataReader(sql, null))
            {
                while (sda.Read())
                {
                    RoomTypeMDL rt = new RoomTypeMDL();
                    rt.rtid = Convert.ToInt32(sda["rtid"]);
                    rt.rtname = sda[1].ToString();
                    rt.rtmount = Convert.ToInt32(sda[4]);
                    list.Add(rt);
                }
                return list;
            }
        }

        public static int insertroom(RoomTypeMDL rt)
        {
            string sql = "insert into roomtype(rtname,rtmount) values(@rtname,@rtmount)";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@rtname", rt.rtname));
            list.Add(new SqlParameter("@rtmount", rt.rtmount));
            return DBhelper.MyExecuteNonQuery(sql, list.ToArray());
        }
        public static int updateroom(RoomTypeMDL rt)
        {
            string sql = "update roomtype set rtname=@rtname,rtmount=@rtmount where rtid=@rtid";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@rtname", rt.rtname));
            list.Add(new SqlParameter("@rtmount", rt.rtmount));
            list.Add(new SqlParameter("@rtid", rt.rtid));
            return DBhelper.MyExecuteNonQuery(sql, list.ToArray());
        }
        public static int deleteroom(RoomTypeMDL rt)
        {
            string sql = "delete from roomtype where rtid=@rtid";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@rtid", rt.rtid));
            return DBhelper.MyExecuteNonQuery(sql, list.ToArray());
        }
    }
}
