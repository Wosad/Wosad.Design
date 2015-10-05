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
using Wosad.Common.CalculationLogger; 
using Wosad.Common.CalculationLogger.Interfaces;

namespace Wosad.Loads.ASCE.ASCE7_10.WindLoads.Building.DirectionalProcedure.MWFRS
{
    public partial class Mwfrs : BuildingDirectionalProcedureElement
    {

        public double GetDesignPressureNet(double qz, double qh, double G, double Cpl, double Cpw)
        {
            double p1 = qz * G * Cpw;
            double p2 = qh * G * Cpl;
            double p = p1 - p2;

            
            #region p
            ICalcLogEntry pEntry = new CalcLogEntry();
            pEntry.ValueName = "p";
            pEntry.AddDependencyValue("p1", Math.Round(p1, 3));
            pEntry.AddDependencyValue("p2", Math.Round(p2, 3));
            pEntry.AddDependencyValue("Cpl", Math.Round(Cpl, 3));
            pEntry.AddDependencyValue("Cpw", Math.Round(Cpw, 3));
            pEntry.AddDependencyValue("G", Math.Round(G, 3));
            pEntry.AddDependencyValue("qz", Math.Round(qz, 3));
            pEntry.AddDependencyValue("qh", Math.Round(qh, 3));

            pEntry.Reference = "";
            pEntry.DescriptionReference = "/Templates/Loads/ASCE7_10/Wind/WindPressure/WindPressureMWFRSNoInternal.docx";
            pEntry.FormulaID = null; //reference to formula from code
            pEntry.VariableValue = Math.Round(p, 3).ToString();
            #endregion
            this.AddToLog(pEntry);

            return p;
        }

        public double GetDesignPressure(double q, double G, double Cp, double qi, double GCpi)
        {
           double p1 = q * G * Cp - qi * GCpi;
           double p2 = q * G * Cp + qi * GCpi;
           double p = Math.Max(Math.Abs(p1), Math.Abs(p2));


           #region p
           ICalcLogEntry pEntry = new CalcLogEntry();
           pEntry.ValueName = "p";
           pEntry.AddDependencyValue("p1", Math.Round(p1, 3));
           pEntry.AddDependencyValue("p2", Math.Round(p2, 3));
           pEntry.AddDependencyValue("q", Math.Round(q, 3));
           pEntry.AddDependencyValue("qi", Math.Round(qi, 3));
           pEntry.AddDependencyValue("GCpi", Math.Round(GCpi, 3));
           pEntry.AddDependencyValue("Cp", Math.Round(Cp, 3));
           pEntry.AddDependencyValue("G", Math.Round(G, 3));

           pEntry.Reference = "";
           pEntry.DescriptionReference = "/Templates/Loads/ASCE7_10/Wind/WindPressure/WindPressureMWFRSWithInternal.docx";
           pEntry.FormulaID = null; //reference to formula from code
           pEntry.VariableValue = Math.Round(p, 3).ToString();
           #endregion
           this.AddToLog(pEntry);

           return p;
        }
    }
}
