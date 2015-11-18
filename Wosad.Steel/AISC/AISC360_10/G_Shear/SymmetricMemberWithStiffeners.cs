using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC.Interfaces;

namespace Wosad.Steel.AISC.AISC360_10.G_Shear
{
    public partial class ShearMemberSymmetricWithStiffeners: ShearMember
    {
        public ShearMemberSymmetricWithStiffeners(double h, double t_w, double a, ISteelMaterial material, bool IsTeeShape = false)
        :base(h,t_w,a,material,IsTeeShape)
        {

        }

        protected override double Get_k_v()
        {
            double k_v;

            if (a/h>3 || a/h>Math.Pow((260 / ((h / t_w))), 2))
            {
                k_v = 5.0;
            }
            else
	        {
                //(G2-6)
                k_v = 5.0 + ((5.0) / (Math.Pow(((a / h)), 2)));
	        }

            return k_v;
        }
    }
}
