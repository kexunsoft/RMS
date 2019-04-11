using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using MDL;

namespace BLL
{
    public class RoomTypeBLL
    {
        public static List<RoomTypeMDL> selectRoom() {
            return RoomTypeDAL.selectRoom();
 
        }
        public static List<RoomTypeMDL> getlist()
        {
            return RoomTypeDAL.getlist();
        }
        public static int insertroom(RoomTypeMDL rt)
        {
            return RoomTypeDAL.insertroom(rt);
        }
        public static int updateroom(RoomTypeMDL rt)
        {
            return RoomTypeDAL.updateroom(rt);
        }
        public static int deleteroom(RoomTypeMDL rt)
        {
            return RoomTypeDAL.deleteroom(rt);
        }
    }
}
