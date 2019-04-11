using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;
using MDL;


namespace BLL
{
  public  class YDBLL
    {
      public static DataTable GetYD() {
          return YdDAL.GetYD();
      }
      public static int AddYd(YDMDL y) {
          return YdDAL.AddYd(y);
      }
      public static int DeleteYD(string YDID) {
          return YdDAL.DeleteYD(YDID);
      }
      public static int UPTB(string YDID)
      {
          return YdDAL.UPTB(YDID);
      }
    }
}
