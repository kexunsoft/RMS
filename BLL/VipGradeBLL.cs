using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDL;
using DAL;

namespace BLL
{
    public class VipGradeBLL
    {
        public static List<VipGradeMDL> getlist(VipGradeMDL vg)
        {
            return VipGradeDAL.getlist(vg);
        }
        public static int insertvg(VipGradeMDL vg)
        {
            return VipGradeDAL.insertvg(vg);
        }
        public static int updatevg(VipGradeMDL vg)
        {
            return VipGradeDAL.updatevg(vg);
        }
        public static int deletevg(VipGradeMDL vg)
        {
            return VipGradeDAL.deletevg(vg);
        }
    }
}
