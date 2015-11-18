using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC.Interfaces;

namespace Wosad.Steel.AISC.AISC360_10.G_Shear
{
    public partial class ShearMemberSymmetricWithoutStiffeners : ShearMember
    {
        public ShearMemberSymmetricWithoutStiffeners(double h, double t_w, ISteelMaterial material, bool IsTeeShape = false) 
        :base (h,t_w,0,material,IsTeeShape)
        {

        }

        protected override double Get_k_v()
        {
            if (h/t_w>260)
            {
                throw new Exception("Web slenderness exceeds h/t_w =260 limit. Revise member section.");
            }
            //section G2.1(b)
            if (IsTeeShape == true)
            {
                return 1.2;
            }
            else
            {
                return 5.0;
            }
        }
    }
}
