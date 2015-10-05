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
using Wosad.Loads.ASCE.ASCE7_10.WindLoads;
using Wosad.Loads.ASCE7.Entities;

namespace Wosad.Loads.ASCE.ASCE7_10.WindLoads.Building.DirectionalProcedure
{
    public partial class BuildingDirectionalProcedureElement: WindBuilding
    {

        public double GetVelocityPressure(double Kz, double Kzt, double Kd, double V, WindVelocityLocation Location)
        {
            double qz= 0.00256 * Kz * Kzt * Kd * Math.Pow(V, 2); //(28.3-1)

            
            #region qz
            ICalcLogEntry qzEntry = new CalcLogEntry();
            qzEntry.ValueName = "qz";
            qzEntry.AddDependencyValue("Kz", Math.Round(Kz, 3));
            qzEntry.AddDependencyValue("Kzt", Math.Round(Kzt, 3));
            qzEntry.AddDependencyValue("Kd", Math.Round(Kd, 3));
            qzEntry.AddDependencyValue("V", Math.Round(V, 3));
            if (Location == WindVelocityLocation.Roof)
            {
                qzEntry.AddDependencyValue("locType", "h");
            }
            else
            {
                qzEntry.AddDependencyValue("locType", "z");
            }
            qzEntry.Reference = "";
            qzEntry.DescriptionReference = "/Templates/Loads/ASCE7_10/Wind/WindVelocityPressure.docx";
            qzEntry.FormulaID = null; //reference to formula from code
            qzEntry.VariableValue = Math.Round(qz, 3).ToString();
            #endregion

            this.AddToLog(qzEntry);

            return qz;
        }
    }
}
