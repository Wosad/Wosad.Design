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
using Wosad.Steel.AISC.SteelEntities;
using Wosad.Common.CalculationLogger;
 using Wosad.Common.CalculationLogger;
 

namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public abstract partial class FlexuralMemberIBase : FlexuralMember
    {
        //Yielding
        public override SteelLimitStateValue GetMinorPlasticMomentCapacity()
        {
            SteelLimitStateValue ls = new SteelLimitStateValue();
            double Mn = 0.0;
            double Mp = GetMinorPlasticMomentCapacity().Value;
            double M = 1.6 * Fy * Sy;
            double My = M/12.0;
            
            #region My
            ICalcLogEntry MyEntry = new CalcLogEntry();
            MyEntry.ValueName = "My";
            MyEntry.AddDependencyValue("Fy", Math.Round(Fy, 3));
            MyEntry.AddDependencyValue("Sy", Math.Round(Sy, 3));
            MyEntry.AddDependencyValue("M", Math.Round(M, 3));
            MyEntry.Reference = "";
            MyEntry.DescriptionReference = "/Templates/Steel/AISC360_10/Flexure/F6_Mp.docx";
            MyEntry.FormulaID = null; //reference to formula from code
            MyEntry.VariableValue = Math.Round(My, 3).ToString();
            #endregion
            this.AddToLog(MyEntry);

            M = Math.Min(Mp, My); //(F6-1)
            Mn = M / 12.0;

            
            #region Mn
            ICalcLogEntry MnEntry = new CalcLogEntry();
            MnEntry.ValueName = "Mn";
            MnEntry.AddDependencyValue("M", Math.Round(M, 3));
            MnEntry.Reference = "";
            MnEntry.DescriptionReference = "/Templates/Steel/AISC360_10/Flexure/Mn.docx";
            MnEntry.FormulaID = null; //reference to formula from code
            MnEntry.VariableValue = Math.Round(Mn, 3).ToString();
            #endregion
            this.AddToLog(MnEntry);

            ls.IsApplicable = true;
            ls.Value = Mn;
            return ls;
        }
    }
}
