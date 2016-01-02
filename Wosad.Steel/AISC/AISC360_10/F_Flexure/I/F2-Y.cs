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
using Wosad.Common.CalculationLogger;
using Wosad.Steel.AISC.SteelEntities;
 using Wosad.Common.CalculationLogger;

namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public partial class BeamIDoublySymmetricCompact : BeamIDoublySymmetricBase, ISteelBeamFlexure
    {
        //Yielding F2.1
        public virtual double GetYieldingMomentCapacity()
        //this method must be virtual as it may be not applicable for non compact sections
        {
            //double Fy = this.Section.Material.YieldStress;
            //double Zx = Section.SectionBase.PlasticSectionModulusX;
            double Mn = GetMajorPlasticMomentCapacity().Value;
            double M = GetFlexuralDesignValue(Mn);


            return M;
                
                
        }


        public override SteelLimitStateValue GetMajorPlasticMomentCapacity()
        {

            SteelLimitStateValue ls = new SteelLimitStateValue();
            double Mp = 0.0;


            double Fy = this.Section.Material.YieldStress;
            double Zx = Section.Shape.PlasticSectionModulusX;

            double M = Fy * Zx;
            Mp = M / 12.0;

            #region Mp
            ICalcLogEntry MpEntry = new CalcLogEntry();
            MpEntry.ValueName = "Mp";
            MpEntry.AddDependencyValue("Fy", Math.Round(Fy, 3));
            MpEntry.AddDependencyValue("Zx", Math.Round(Zx, 3));
            MpEntry.AddDependencyValue("M", Math.Round(M, 3));
            MpEntry.Reference = "";
            MpEntry.DescriptionReference = "/Templates/Steel/AISC360_10/Flexure/F2_Mp.docx";
            MpEntry.FormulaID = null; //reference to formula from code
            MpEntry.VariableValue = Math.Round(Mp, 3).ToString();
            #endregion
            this.AddToLog(MpEntry);


            ls.IsApplicable = true;
            ls.Value = Mp;
            return ls;
            

        }
        public override SteelLimitStateValue GetMinorPlasticMomentCapacity()
        {
            double Mp = 0.0;
            SteelLimitStateValue ls = new SteelLimitStateValue();

            double Fy = this.Section.Material.YieldStress;
            double Zy = Section.Shape.PlasticSectionModulusY;
            double M = Fy * Zy;
            Mp = M / 12.0;

            
            #region Mp
            ICalcLogEntry MpEntry = new CalcLogEntry();
            MpEntry.ValueName = "Mp";
            MpEntry.AddDependencyValue("Fy", Math.Round(Fy, 3));
            MpEntry.AddDependencyValue("Zy", Math.Round(Zy, 3));
            MpEntry.AddDependencyValue("M", Math.Round(M, 3));
            MpEntry.Reference = "";
            MpEntry.DescriptionReference = "/Templates/Steel/AISC360_10/Flexure/F6_Mp.docx";
            MpEntry.FormulaID = null; //reference to formula from code
            MpEntry.VariableValue = Math.Round(Mp, 3).ToString();
            #endregion
            this.AddToLog(MpEntry);

            ls.IsApplicable = true;
            ls.Value = Mp;
            return ls;
        }


    }
}
