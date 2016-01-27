using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC.Interfaces;
using Wosad.Steel.AISC.SteelEntities.Materials;


namespace Wosad.Steel.AISC.AISC360_10.Shear
{
    public class ShearMemberFactory
    {
        public IShearMember GetShearMemberNonCircular(ShearNonCircularCase ShearCase, double h, double t_w, double a, double F_y,double E)
        {
            ISteelMaterial material = new SteelMaterial(F_y,E);
            IShearMember member =null;
            bool IsTeeShape;


            switch (ShearCase)
            {
                case ShearNonCircularCase.MemberWithStiffeners:
                     IsTeeShape =false;
                     member = new ShearMemberWithStiffeners(h, t_w, a, material, IsTeeShape);
                    break;
                case ShearNonCircularCase.Tee:
                    IsTeeShape = true;
                    member = new ShearMemberWithoutStiffeners(h, t_w, material, IsTeeShape);
                    break;
                default:
                    IsTeeShape = false;
                    member = new ShearMemberWithoutStiffeners(h, t_w, material, IsTeeShape);
                    break;
            }
            return member;
        }
        public IShearMember GetShearMemberCircular(double D, double t_nom, bool Is_SAW_member, double L_v, double F_y, double E)
        {
            ISteelMaterial material = new SteelMaterial(F_y,E);
            ShearMemberCircular member = new ShearMemberCircular(D, t_nom, Is_SAW_member, L_v, material);
            return member;
        }
    }
}
