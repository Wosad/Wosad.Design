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
using Wosad.Common.Entities;
using Wosad.Common.Section.Interfaces;
using Wosad.Steel.AISC.Interfaces;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Section.Interfaces;
using Wosad.Steel.AISC.Interfaces;
using Wosad.Steel.AISC.SteelEntities;
using Wosad.Steel.AISC.SteelEntities.Sections;
using Wosad.Steel.AISC.Code;
using Wosad.Steel.AISC;
using Wosad.Steel.AISC.SteelEntities.Bolts;

namespace Wosad.Analytics.Steel.AISC360_10.Connections.AffectedElements
{
    public  class AffectedElementWithHoles : SteelDesignElement
    {
        public AffectedElementWithHoles()
        {

        }
        public AffectedElementWithHoles(ICalcLog log): base(log)
        {

        }
        public double GetBearingStrengthAtBoltHole(double l_c, double d_b, double t, ISteelMaterial Material, BoltHoleType BoltHoleType, 
            BoltHoleDeformationType BoltHoleDeformationType, bool IsUnstiffenedHollowSection)
        {
            double F_u = Material.UltimateStress;
            double F_y = Material.YieldStress;
            double phiR_n;
            if (IsUnstiffenedHollowSection == false)
            {
                        double phiR_n1;
                        double phiR_n2;
           
                if (BoltHoleType == Wosad.Steel.AISC.SteelEntities.Bolts.BoltHoleType.LongSlottedPerpendicular)
                {
                        //(J3-6c)
                        phiR_n1=0.75*(1.0*l_c*t*F_u);
                        phiR_n2=0.75*(2.0*d_b*t*F_u);
                }
                else
                {
                    if (BoltHoleDeformationType == Wosad.Steel.AISC.BoltHoleDeformationType.ConsideredUnderServiceLoad)
                    {
                         //(J3-6a)
                        phiR_n1=0.75*(1.2*l_c*t*F_u);
                        phiR_n2=0.75*(2.4*d_b*t*F_u);
 
                    }
                    else
                    {
                       //(J3-6b)
                        phiR_n1=0.75*(1.5*l_c*t*F_u);
                        phiR_n2=0.75*(3.0*d_b*t*F_u);
                    }
                }
                phiR_n = Math.Min(phiR_n1, phiR_n2);
            }
            else
	            {
                    phiR_n=0.75*(1.8*d_b*t*F_y);
	            }

            return phiR_n;
            }
        }

    
}
