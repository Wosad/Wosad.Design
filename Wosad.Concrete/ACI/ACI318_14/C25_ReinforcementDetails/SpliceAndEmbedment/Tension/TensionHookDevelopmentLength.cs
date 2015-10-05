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
using Wosad.Common.Reports; using Wosad.Common.CalculationLogger.Interfaces; using Wosad.Common.CalculationLogger;
using p = Wosad.Concrete.ACI318_11.TensionHookParagraphs;
using f = Wosad.Concrete.ACI318_11.TensionHookFormulas;
using v = Wosad.Concrete.ACI318_11.TensionHookValues;
using d = Wosad.Concrete.ACI318_11.TensionHookDescriptions;
using dv = Wosad.Concrete.ACI318_11.DevelopmentValues;
using gv = Wosad.Concrete.ACI318_11.GeneralValues;
using gd = Wosad.Concrete.ACI318_11.GeneralDescriptions;
using gf = Wosad.Concrete.ACI318_11.GeneralFormulas;
using Wosad.Concrete.ACI.Infrastructure.Entities.Rebar;
using Wosad.Common.CalculationLogger.Interfaces;

namespace Wosad.Concrete.ACI318_11
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

            ICalcLogEntry ent1 = Log.CreateNewEntry();
            ent1.ValueName = v.ldh;
            ent1.AddDependencyValue(gv.lambda, lambda);
            ent1.AddDependencyValue(dv.ksi_e, ksi_e);
            ent1.AddDependencyValue(gv.fy, fy);
            ent1.AddDependencyValue(gv.fc, fc);
            ent1.Reference = "ACI Section 12.5.2";
            ent1.DescriptionReference = d.ldh;
            ent1.FormulaID = p._5._2_1;
            ldh = 0.02 * ksi_e * fy / (lambda * sqrt_fc) * db;
            ent1.VariableValue = ldh.ToString();
            AddToLog(ent1);
            
            
            return ldh;

        }

        [ReportElement(
        new string[] { v.ldh },
        new string[] { p._5._2_1, p._5._1_1 },
        new string[] { d.ldh, d.ldhMin })]

        internal double GetDevelopmentLength()
        {
            
            double ldh = this.GetBasicDevelopmentLength();
            ldh = this.CheckDevelopmentLengthForExcessAndMinimum(ldh);

            return ldh;
        
        }
        internal double GetDevelopmentLength(HookType hookType, double sideCover, double barExtensionCover, bool enclosingRebarIsPerpendicular, double enclosingRebarSpacing)
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


[ReportElement(
new string[] { v.ldh },
new string[] { p._5._1_1 },
new string[] { d.ldhMin })]

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
        ICalcLogEntry ent3 = Log.CreateNewEntry();
        ent3.ValueName = v.ldh;
        ent3.AddDependencyValue(gv.db, db);
        ent3.Reference = "ACI Section 12.5.1";
        ent3.DescriptionReference = d.ldhMin;
        ent3.FormulaID = p._5._1_1;
        ldh = ldhMin;
        ent3.VariableValue = ldh.ToString();
        AddToLog(ent3); 
    }

    return ldh;
    }
     
    }
}
