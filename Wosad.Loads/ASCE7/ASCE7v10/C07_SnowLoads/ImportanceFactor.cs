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
using Wosad.Common.CalculationLogger; using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Data;
using Wosad.Common.Entities;
using Wosad.Common.CalculationLogger; using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Loads.Properties;
using Wosad.Loads.ASCE7.Entities;

namespace Wosad.Loads.ASCE.ASCE7_10.SnowLoads
{
    public partial class SnowStructure
    {
        public double GetImportanceFactor(BuildingRiskCategory RiskCategory)
        {
            double Is = 1.0;

            switch (RiskCategory)
            {
                case BuildingRiskCategory.I:
                    Is = 0.8;
                    break;
                case BuildingRiskCategory.II:
                    Is = 1.0;
                    break;
                case BuildingRiskCategory.III:
                    Is = 1.1;
                    break;
                case BuildingRiskCategory.IV:
                    Is = 1.2;
                    break;
            }

            #region Is
            ICalcLogEntry IsEntry = new CalcLogEntry();
            IsEntry.ValueName = "Is";
            IsEntry.AddDependencyValue("RiskCategory", RiskCategory.ToString());
            IsEntry.Reference = "";
            IsEntry.DescriptionReference = "/Templates/Loads/ASCE7_10/Snow/SnowImportanceFactorIs.docx";
            IsEntry.FormulaID = "Table 1.5-2"; //reference to formula from code
            IsEntry.VariableValue = Is.ToString();
            #endregion
            this.AddToLog(IsEntry);

            return Is;
        }
    }
}
