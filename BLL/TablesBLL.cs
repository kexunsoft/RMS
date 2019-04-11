using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDL;
using DAL;

namespace BLL
{
    public class TablesBLL
    {
        public static List<TablesMDL> selectTable(int RTID)
        {
          return  TablesDAL.selectTable(RTID);
        }
        public static int UpdateTables(int TableID, int State) {
           return TablesDAL.UpdateTables(TableID,State);
        
        }
        public static string GetCBID(int TableID)
        {
           return TablesDAL.GetCBID(TableID);
        }
        //梁晨煜
        public static List<TablesMDL> getlist(RoomTypeMDL rt)
        {
            return TablesDAL.getlist(rt);
        }
        public static int inserttb(TablesMDL tb)
        {
            return TablesDAL.inserttb(tb);
        }
        public static int updatetb(TablesMDL tb)
        {
            return TablesDAL.updatetb(tb);
        }
        public static int deletetb(TablesMDL tb)
        {
            return TablesDAL.deletetb(tb);
        }
        /// <summary>
        /// 查询剩余的桌子数
        /// </summary>
        /// <param name="RTID">房间类型</param>
        /// <returns>剩余的桌子数</returns>
        public static int GetTableCount(int RTID)
        {
            return TablesDAL.GetTableCount(RTID);
        }
    }
}
