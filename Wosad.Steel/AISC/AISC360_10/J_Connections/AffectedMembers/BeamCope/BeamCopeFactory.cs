using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Section.Interfaces;
using Wosad.Common.Section.SectionTypes;
using Wosad.Steel.AISC.Interfaces;
using Wosad.Steel.AISC.SteelEntities;
using Wosad.Steel.AISC.SteelEntities.Materials;

namespace Wosad.Steel.AISC.AISC360_10.Connections
{
    public class BeamCopeFactory
    {

        public IBeamCope GetCope(BeamCopeCase BeamCopeCase, double d, double b_f, double t_f, double t_w, double d_c, double c, double F_y, double F_u)
       {
           ISteelMaterial material = new SteelMaterial(F_y, F_u, SteelConstants.ModulusOfElasticity, SteelConstants.ShearModulus);
           ISectionI section = new SectionI(null, d, b_f, t_f, t_w);
           IBeamCope cope=null;
           switch (BeamCopeCase)
           {
               case BeamCopeCase.Uncoped:
                   cope = new BeamUncoped(section, material);
                   break;
               case BeamCopeCase.CopedTopFlange:
                   cope = new BeamCopeSingle(c, d_c, section, material);
                   break;
               case BeamCopeCase.CopedBothFlanges:
                   cope = new BeamCopeDouble(c, d_c, section, material);
                   break;
           }
           return cope;

       }

    }
}
