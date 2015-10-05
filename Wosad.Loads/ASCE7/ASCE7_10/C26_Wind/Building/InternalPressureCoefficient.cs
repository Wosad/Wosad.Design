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
using Wosad.Common.CalculationLogger;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Loads.ASCE7.Entities;

namespace Wosad.Loads.ASCE.ASCE7_10.WindLoads.Building
{
    public partial class WindBuilding : WindStructure
    {

        public double GetInternalPressureCoefficient(WindEnclosureType EnclosureType)
        {
            double GCpi = 0.0;

            
            #region EnclosureClass
            ICalcLogEntry EnclosureClassEntry = new CalcLogEntry();
            EnclosureClassEntry.ValueName = "GCpi";
            EnclosureClassEntry.Reference = "";
            EnclosureClassEntry.FormulaID = null; //reference to formula from code

            #endregion


            switch (EnclosureType)
            {
                case WindEnclosureType.Open:
                    GCpi= 0.0;
                    EnclosureClassEntry.DescriptionReference = "/Templates/Loads/ASCE7_10/Wind/WindInternalPressureOpen.docx";
                    break;
                case WindEnclosureType.PartiallyEnclosed:
                    GCpi= 0.55;
                    EnclosureClassEntry.DescriptionReference = "/Templates/Loads/ASCE7_10/Wind/WindInternalPressurePartiallyEnclosed.docx";
                    break;
                case WindEnclosureType.Enclosed:
                    GCpi= 0.18;
                    EnclosureClassEntry.DescriptionReference = "/Templates/Loads/ASCE7_10/Wind/WindInternalPressureEnclosed.docx";
                    break;
            }

            EnclosureClassEntry.VariableValue = GCpi.ToString();
            this.AddToLog(EnclosureClassEntry);
            return GCpi;
        }

        public double GetInternalPressureCoefficient(WindEnclosureType EnclosureType, double OpeningArea,
             double InternalVolume)
        {
            double GCpi = this.GetInternalPressureCoefficient(EnclosureType);
            double Vi = InternalVolume;
            double Aog = OpeningArea;
            double Ri = 1.0;
            double GCpiR = GCpi * Ri;

            //WindInternalPressureCoefficientLargeVolumeRi.docx

            
            #region GCpiR
            ICalcLogEntry GCpiREntry = new CalcLogEntry();
            GCpiREntry.ValueName = "GCpiR";
            GCpiREntry.AddDependencyValue("GCpi", Math.Round(GCpi, 3));
            GCpiREntry.AddDependencyValue("Vi", Math.Round(Vi, 3));
            GCpiREntry.Reference = "";
            GCpiREntry.DescriptionReference = "/Templates/Loads/ASCE7_10/Wind/WindInternalPressureCoefficientLargeVolumeRi.docx";
            GCpiREntry.FormulaID = null; //reference to formula from code
            GCpiREntry.VariableValue = Math.Round(GCpiR, 3).ToString();
            #endregion
            this.AddToLog(GCpiREntry);
            
            return GCpiR;
        }
    }
}

