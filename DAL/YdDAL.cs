using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MDL;
using System.Data.SqlClient;
namespace DAL
{
   public class YdDAL
    {
       public static DataTable GetYD() {
           string sql = "select YDID,KName,KPhone,RTName,TableName,YTime,YJ,Price,bz from YuDing y join RoomType r on y.RTID=r.RTID join Tables t on t.TableID=y.TBID";
           return DBhelper.dd(sql,null);
       } 
       public static int  AddYd(YDMDL y){
           string sql = "insert into YuDing values(@YDID,@KName,@KPhone,@RTID,@TBID,@YTime,@YJ,@Price,@bz,@daoda)";
           SqlParameter[] sp = { 
                               new SqlParameter("@YDID",y.YDID),
                               new SqlParameter("@KName",y.KName),
                               new SqlParameter("@KPhone",y.KPhone),
                               new SqlParameter("@RTID",y.RTID),
                               new SqlParameter("@TBID",y.TBID),
                               new SqlParameter("@YTime",y.YTime),
                               new SqlParameter("@YJ",y.YJ),
                               new SqlParameter("@Price",y.Price),
                               new SqlParameter("@bz",y.bz),
                               new SqlParameter("@daoda",y.daoda),


                               };
          return DBhelper.MyExecuteNonQuery(sql,sp);
       }
       public static int DeleteYD(string YDID) {
           string sql = "delete from YuDing where YDID=@YDID";
           SqlParameter[] sp = { 
                               new SqlParameter("@YDID",YDID)
                               };
         
           return DBhelper.MyExecuteNonQuery(sql, sp); 
       
       }
       public static int UPTB(string YDID)
       {
           string sql_U = "update Tables set TableState=0 where TableID in (select TBID from YuDing where YDID=@YDID)";

           SqlParameter[] sp_U = { 
                               new SqlParameter("@YDID",YDID)
                               };
          return DBhelper.MyExecuteNonQuery(sql_U, sp_U);
       }
     
            
   }
}
