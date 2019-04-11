using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDL;
using DAL;
using System.Data;

namespace BLL
{
    public class VipsBLL
    {
        public static List<VipsMDL> GetVip(int VipID) {

          return  VipsDAL.GetVip(VipID);
        }
        public static List<VipsMDL> GetVips(int VipID)
        {

            return VipsDAL.GetVips(VipID);
        }
        public static int AddVip(VipsMDL v) {
            return VipsDAL.AddVip(v);
        }
        public static int GetMaxID() {
            return VipsDAL.GetMaxID();
        }
        public static int DeleteVip(int VipID) {
            return VipsDAL.DeleteVip(VipID);
        }
        public static DataTable GetJL() {
            return VipsDAL.GetJL();
        }
        public static int UpdateVip(VipsMDL v) {
            return VipsDAL.UpdateVip(v);
        }
        
    }
}
