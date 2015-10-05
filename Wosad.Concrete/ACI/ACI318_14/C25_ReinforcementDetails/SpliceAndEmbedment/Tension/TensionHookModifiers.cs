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
using Wosad.Concrete.ACI.Infrastructure.Entities.Rebar;
using Wosad.Common.CalculationLogger.Interfaces;

namespace Wosad.Concrete.ACI318_11
{
    public partial class StandardHookInTension : Development
    {
        
        private double GetSideCoverModifier(HookType hookType, double sideCover, double barExtensionCover)
        {
            //(a) For No. 11 bar and smaller hooks with side cover
            //(normal to plane of hook) not less than 2-1/2 in., and
            //for 90-degree hook with cover on bar extension
            //beyond hook not less than 2 in.
            
            double SideCoverModifier=1.0;

            if (hookType== HookType.Degree90)
            {
                if (sideCover!=0.0 && barExtensionCover!=0.0)
                {
                    if (db<=11/8)
                    {
                        if (sideCover>=2.5 && barExtensionCover>=2.0)
                        {
                            SideCoverModifier = SetSideCoverModifier();
                        }
                    }
                }
                
            }
            else
            {
                if (sideCover!=0.0)
                {
                    if (db <= 11 / 8)
                    {
                        if (sideCover>=2.5)
                        {
                            SideCoverModifier = SetSideCoverModifier();
                        }
                    }
                }
            }
            return SideCoverModifier;
        }


        private double GetConfinementModifier(HookType hookType, bool enclosingRebarIsPerpendicular, double enclosingRebarSpacing)
        {
            double confinementModifier=1.0;
            if (hookType== HookType.Degree90 && db<=11/8)
            {
                //      (b) For 90-degree hooks of No. 11 and smaller bars
                //      that are either enclosed within ties or stirrups
                //      perpendicular to the bar being developed, spaced
                //      not greater than 3db along ldh; or enclosed within
                //      ties or stirrups parallel to the bar being developed,
                //      spaced not greater than 3db along the length of the
                //      tail extension of the hook plus bend.................... 0.8
                if (enclosingRebarIsPerpendicular == true && enclosingRebarSpacing <= 3.0 * db)
                {
                    confinementModifier = SetConfinementModifier();
                } 
                if (enclosingRebarIsPerpendicular ==true && enclosingRebarSpacing<3.0*db)
                {
                    confinementModifier = SetConfinementModifier();
                }
            }
            if (hookType== HookType.Degree180 && db<=11/8)
            {
                if (enclosingRebarIsPerpendicular==true && enclosingRebarSpacing<=3*db)
                {
                    confinementModifier = SetConfinementModifier();
                }
            }
            return confinementModifier;
        }

[ReportElement(
new string[] {"SideCoverModifier" },
new string[] { "P-12.5.3-1"},
new string[] { "SideCoverModifier"})]
                   
        private double SetSideCoverModifier()
        {
            double SideCoverModifier;
            ICalcLogEntry ent1 = Log.CreateNewEntry();
            ent1.ValueName = "SideCoverModifier";
            ent1.Reference = "ACI Section 12.5.3";
            ent1.DescriptionReference = "SideCoverModifier";
            ent1.FormulaID = "P-12.5.3-1";
                SideCoverModifier = 0.7;
            ent1.VariableValue = SideCoverModifier.ToString();
            AddToLog(ent1);
            return SideCoverModifier;
        }

[ReportElement(
new string[] { "ConfinementModifier", },
new string[] { "P-12.5.3-1" },
new string[] { "ConfinementModifier" })]
                   
        private double SetConfinementModifier()
        {
            double ConfinementModifier;
            ICalcLogEntry ent1 = Log.CreateNewEntry();
            ent1.ValueName = "ConfinementModifier";
            ent1.Reference = "ACI Section 12.5.3";
            ent1.DescriptionReference = "ConfinementModifier";
            ent1.FormulaID = "P-12.5.3-1";
                ConfinementModifier = 0.8;
            ent1.VariableValue = ConfinementModifier.ToString();
            AddToLog(ent1);
            return ConfinementModifier;
        }
    }
}
