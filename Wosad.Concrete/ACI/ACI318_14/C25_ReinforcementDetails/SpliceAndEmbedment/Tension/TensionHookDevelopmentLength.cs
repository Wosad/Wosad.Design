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
using Wosad.Concrete.ACI;
using Wosad.Common.Entities;
using Wosad.Common.Reports; 
using Wosad.Common.CalculationLogger.Interfaces; 
using Wosad.Common.CalculationLogger;
using Wosad.Concrete.ACI.Infrastructure.Entities.Rebar;
using Wosad.Common.CalculationLogger.Interfaces;

namespace Wosad.Concrete.ACI318_14
{
    public partial class StandardHookInTension : Development
    {

        private double GetBasicDevelopmentLength()
        {
            double ldh;
            double ksi_e = GetKsi_e();
            double fy = Rebar.Material.YieldStress;
            double lambda = GetHookLambda();
            double sqrt_fc = GetSqrt_fc();
            double fc = Concrete.SpecifiedCompressiveStrength;


            ldh = 0.02 * ksi_e * fy / (lambda * sqrt_fc) * db;

            
            
            return ldh;

        }


        public double GetDevelopmentLength()
        {
            
            double ldh = this.GetBasicDevelopmentLength();
            ldh = this.CheckDevelopmentLengthForExcessAndMinimum(ldh);

            return ldh;
        
        }
        public double GetDevelopmentLength(HookType hookType, double sideCover, double barExtensionCover, bool enclosingRebarIsPerpendicular, double enclosingRebarSpacing)
        {
            double ldh = this.GetBasicDevelopmentLength();
            //ldh modifiers per 12.5.3 
            // (a)
            double SideCoverModifier = GetSideCoverModifier(hookType, sideCover, barExtensionCover); 
            // (b) & (c)
            double ConfinementModifier = GetConfinementModifier(hookType, enclosingRebarIsPerpendicular, enclosingRebarSpacing);
            // (d) -- see below
           
                if (SideCoverModifier==0.0 || ConfinementModifier==0.0)
                {
                    throw new Exception("Hook development modifiers cannot be 0");
                }
            ldh = ldh * SideCoverModifier * ConfinementModifier;
            ldh = this.CheckDevelopmentLengthForExcessAndMinimum(ldh);

            return ldh;
          }




    private double CheckDevelopmentLengthForExcessAndMinimum(double ldh)
    {
    //(d)

    ldh = CheckExcessReinforcement(ldh, true, true);

    //Check minimum ldh length
    //ldh shall not be less than the larger of 8db and 6 in.

    double ldhMinDia = 8 * db;
    double ldhMinLen = 6;
    double ldhMin = ldhMinDia > ldhMinLen ? ldhMinDia : ldhMinLen;

    if (ldhMin>ldh)
    {
        ldh = ldhMin;

    }

    return ldh;
    }
     
    }
}
