using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger;
using Wosad.Concrete.ACI;
using Wosad.Concrete.ACI318_14.Materials;

namespace Wosad.Concrete.ACI318_14
{
    public class SectionFactory
    {
       public ConcreteSectionFlexure GetNonPrestressedDoublyReinforcedRectangularSection(double b, double h, 
            double A_s1,double A_s2,double c_cntr1,double c_cntr2, 
            ConcreteMaterial concreteMaterial, IRebarMaterial rebarMaterial)
        {
            return GetNonPrestressedDoublyReinforcedRectangularSection(b, h, A_s1, A_s2, c_cntr1, c_cntr2, 0, 0, 0, 0, concreteMaterial, rebarMaterial);
        }


        public ConcreteSectionFlexure GetNonPrestressedDoublyReinforcedRectangularSection(double b, double h, 
            double A_s1,double A_s2,double c_cntr1,double c_cntr2, 
            double A_s_prime1,double A_s_prime2, double c_cntr_prime1, double c_cntr_prime2, 
            ConcreteMaterial concrete, IRebarMaterial rebar)
        {
            CrossSectionRectangularShape Section = new CrossSectionRectangularShape(concrete, null, b, h);
             List<RebarPoint> LongitudinalBars = new List<RebarPoint>();

            Rebar bottom1 = new Rebar(A_s1, rebar);
            RebarPoint pointBottom1 = new RebarPoint(bottom1, new RebarCoordinate() { X = 0, Y = -h / 2.0 + c_cntr1 });
            LongitudinalBars.Add(pointBottom1);


            if (A_s2!=0)
            {
                Rebar bottom2 = new Rebar(A_s2, rebar);
                RebarPoint pointBottom2 = new RebarPoint(bottom2, new RebarCoordinate() { X = 0, Y = -h / 2.0 + c_cntr2 });
                LongitudinalBars.Add(pointBottom2);
            }

            if (A_s_prime1 != 0)
            {
                Rebar top1 = new Rebar(A_s_prime1, rebar);
                RebarPoint pointTop1 = new RebarPoint(top1, new RebarCoordinate() { X = 0, Y = h / 2.0 - c_cntr_prime1 });
                LongitudinalBars.Add(pointTop1);
            }

            if (A_s_prime2 != 0)
            {
                Rebar top2 = new Rebar(A_s_prime2, rebar);
                RebarPoint pointTop2 = new RebarPoint(top2, new RebarCoordinate() { X = 0, Y = h / 2.0 - c_cntr_prime2 });
                LongitudinalBars.Add(pointTop2);
            }

            CalcLog log = new CalcLog();
            ConcreteSectionFlexure beam = new ConcreteSectionFlexure(Section, LongitudinalBars, log);
            return beam;
        }
    }
}
