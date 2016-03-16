#region Copyright
   /*Copyright (C) 2015 Wosad Inc

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
   */
#endregion
 
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
