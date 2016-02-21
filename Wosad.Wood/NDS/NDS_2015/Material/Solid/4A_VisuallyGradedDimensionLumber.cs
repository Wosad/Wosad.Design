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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Wosad.Wood.NDS.Entities;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.CalculationLogger;
using Wosad.Wood.NDS.Material;
using Wosad.Wood.Properties;

namespace Wosad.Wood.NDS.NDS_2015.Material
{
    public class VisuallyGradedDimensionLumber : WoodSolidMaterial
    {

        string Species;
        CommercialGrade Grade;
        SizeClassification SizeClass;


        public VisuallyGradedDimensionLumber(string Species, CommercialGrade Grade, SizeClassification SizeClass, ICalcLog CalcLog)
            :base(Resources.NDS2012_Table4A, Species,Grade.ToString(), CalcLog)
        {
             this.Species   =Species;
             this.Grade     =Grade;
             this.SizeClass =SizeClass;
        }

        public VisuallyGradedDimensionLumber(string Species, CommercialGrade Grade, ICalcLog CalcLog)
            : this(Species, Grade, SizeClassification.None, CalcLog)
        {
            this.Species = Species;
            this.Grade = Grade;
        }



        protected override string GetResource()
        {
            return Resources.NDS2012_Table4A;
        }



        protected override void CreateReport()
        {
            
              double Fb = Bending;
              double Ft = TensionParallelToGrain;
              double Fv = ShearParallelToGrain;
              double FcPerp = CompresionPerpendicularToGrain;
              double Fc = CompresionParallelToGrain;
              double E = ModulusOfElasticity;
              double Emin = ModulusOfElasticityMin;
              string CommercialGradeString = CommercialGradeStringConverter.GetCommercialGradeString(Grade);

              
              #region Fb
              ICalcLogEntry FbEntry = new CalcLogEntry();
              FbEntry.ValueName = "Fb";
              FbEntry.AddDependencyValue("WoodSpecies", Species);
              FbEntry.AddDependencyValue("ComGrade", CommercialGradeString);
              FbEntry.AddDependencyValue("Fb", Math.Round(Fb, 3));
              FbEntry.AddDependencyValue("Ft", Math.Round(Ft, 3));
              FbEntry.AddDependencyValue("Fv", Math.Round(Fv, 3));
              FbEntry.AddDependencyValue("Fc_Perp", Math.Round(FcPerp, 3));
              FbEntry.AddDependencyValue("Fc", Math.Round(Fc, 3));
              FbEntry.AddDependencyValue("E", Math.Round(E, 3));
              FbEntry.AddDependencyValue("Emin", Math.Round(Emin, 3));
              FbEntry.Reference = "";
              FbEntry.DescriptionReference = "/Templates/Wood/NDS2015/ReferenceDesignValues.docx";
              FbEntry.FormulaID = null; //reference to formula from code
              FbEntry.VariableValue = Math.Round(Fb, 3).ToString();
              #endregion


              this.AddToLog(FbEntry);

        }
    }
}
