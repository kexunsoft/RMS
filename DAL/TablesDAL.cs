using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDL;
using System.Data.SqlClient;


namespace DAL
{
    public class TablesDAL
    {
        /// <summary>
        /// 查询桌子
        /// </summary>
        /// <param name="RTID"></param>
        /// <returns></returns>
        public static List<TablesMDL> selectTable(int RTID)
        {
            string sql = "select * from dbo.Tables where 1=1";
            List<SqlParameter> sp=new List<SqlParameter>();
            if (RTID != 0)
            {
                sql += " and RTID=@RTID";
                sp.Add(new SqlParameter("@RTID", RTID));
            }
            else {
                sql += "and TableState=0";
            
            }
          
            
            SqlDataReader dr = DBhelper.MySqlDataReader(sql, sp.ToArray());
            List<TablesMDL> list = new List<TablesMDL>();
            while (dr.Read())
            {
                TablesMDL tm = new TablesMDL();
                tm.TableID = Convert.ToInt32(dr[0]);
                tm.TableName = dr[1].ToString();
                tm.RTID = Convert.ToInt32(dr[2]);
                tm.TableArea = dr[3].ToString();
                tm.TableState = Convert.ToInt32(dr[4]);
                list.Add(tm);
            }
            dr.Close();
            return list;

        }
        public static int UpdateTables(int TableID, int State)
        {

            string sql = "update Tables set TableState=@TableState where TableID=@TableID";
            SqlParameter[] sp = { 
                                new SqlParameter("@TableState",State),
                                new SqlParameter("@TableID",TableID)
                                };
            return DBhelper.MyExecuteNonQuery(sql, sp);


        }
        public static string GetCBID( int TableID)
        {
            string sql =   @"select  CBID 
                                      from Tables t join ConsumerBill c 
                                      on t.TableID=c.TableID 
                                      where t.TableID=@TableID and CBClose=0 "; 
                                      
            SqlParameter[] sp = { 
                            
                            new SqlParameter("@TableID",TableID)
                            };
            object c= DBhelper.MyExecuteScalar(sql, sp);
            if (c!=null)
            {
                return c.ToString();
            }
            return "尚未开单";
        }
        public static List<TablesMDL> getlist(RoomTypeMDL rt)
        {
            string sql = "select * from tables a join roomtype b on a.rtid=b.rtid where 1=1";
            List<SqlParameter> list1 = new List<SqlParameter>();
            if (rt != null)
            {
                if (rt.rtname != "")
                {
                    sql += " and b.rtname=@rtname";
                    list1.Add(new SqlParameter("@rtname", rt.rtname));
                }
            }
            List<TablesMDL> list = new List<TablesMDL>();
            using (SqlDataReader sda = DBhelper.MySqlDataReader(sql, list1.ToArray()))
            {
                while (sda.Read())
                {
                    TablesMDL tb = new TablesMDL();
                    tb.tablename = sda[1].ToString();
                    tb.tablearea = sda[3].ToString();
                    if (Convert.ToInt32(sda["tablestate"]) == 0)
                    {
                        tb.tablestate = "可用";
                    }
                    else if (Convert.ToInt32(sda["tablestate"]) == 1)
                    {
                        tb.tablestate = "占用";
                    }
                    else if (Convert.ToInt32(sda["tablestate"]) == 2)
                    {
                        tb.tablestate = "预订";
                    }
                    else if (Convert.ToInt32(sda["tablestate"]) == 3)
                    {
                        tb.tablestate = "停用";
                    }
                    tb.rtid = sda[6].ToString();
                    tb.tableid = Convert.ToInt32(sda[0]);
                    list.Add(tb);
                }
            }
            return list;
        }
        public static int inserttb(TablesMDL tb)
        {
            string sql = "insert into tables values(@tablename,@rtid,@tablearea,@tablestate)";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@tablename", tb.tablename));
            list.Add(new SqlParameter("@rtid", tb.rtid));
            list.Add(new SqlParameter("@tablearea", tb.tablearea));
            list.Add(new SqlParameter("@tablestate", tb.tablestate));
            return DBhelper.MyExecuteNonQuery(sql, list.ToArray());
        }
        public static int updatetb(TablesMDL tb)
        {
            string sql = "update tables set tablename=@tablename,rtid=@rtid,tablearea=@tablearea where tableid=@tableid";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@tablename", tb.tablename));
            list.Add(new SqlParameter("@rtid", tb.rtid));
            list.Add(new SqlParameter("@tablearea", tb.tablearea));
            list.Add(new SqlParameter("@tableid", tb.tableid));
            return DBhelper.MyExecuteNonQuery(sql, list.ToArray());
        }
        public static int deletetb(TablesMDL tb)
        {
            string sql = "delete from tables where tableid=@tableid";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@tableid", tb.tableid));
            return DBhelper.MyExecuteNonQuery(sql, list.ToArray());
        }
        /// <summary>
        /// 查询剩余的桌子数
        /// </summary>
        /// <param name="RTID">房间类型</param>
        /// <returns>剩余的桌子数</returns>
        public static int GetTableCount(int RTID) {
            string sql = "select RTMount-(select COUNT(*) from Tables where RTID=@RTID) from RoomType where RTID=@RTID";
            SqlParameter[] sp = { 
                               new SqlParameter("@RTID",RTID)
                                 
                                };
            object o = DBhelper.MyExecuteScalar(sql, sp);
            //有数据
            if (!(o is DBNull))
            {
                return Convert.ToInt32(o);
            }
            else {

                return 10000;
            }
        }
        }
       

}
