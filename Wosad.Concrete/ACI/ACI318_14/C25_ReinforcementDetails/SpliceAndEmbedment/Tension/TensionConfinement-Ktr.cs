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
using Wosad.Concrete.ACI.Infrastructure.Entities;
using Wosad.Common.Reports; using Wosad.Common.CalculationLogger.Interfaces; using Wosad.Common.CalculationLogger;

namespace Wosad.Concrete.ACI318_11
{
    public partial class DevelopmentTension :Development
    {

[ReportElement(
new string[] { "Atr","s","n","Ktr" },
new string[] { "Atr", "s", "n","12-2" },
new string[] { "Atr-12.2.3", "s-12.2.3","n-12.2.3","Ktr" })]

        internal double GetKtr(double transverseRebarArea, double transverseRebarSpacing, double numberOfBarsAlongSplittingPlane)
        {
            if (transverseRebarArea==0.0||transverseRebarSpacing==0.0 ||numberOfBarsAlongSplittingPlane==0.0)
            {
                // It shall be permitted to use Ktr = 0 as a design simplification even
                //  if transverse reinforcement is present.
                return 0.0;
            }
                //Atr = total cross-sectional area of all transverse reinforcement which is 
                //within the spacing s and which crosses
                //the potential plane of splitting through the reinforcement being developed, in.2
                //for example this would be the area of the sirrups
                double Atr = transverseRebarArea;
                        ICalcLogEntry ent1 = Log.CreateNewEntry();
                        ent1.ValueName = "Atr";
                        ent1.DescriptionReference = "Atr-12.2.3";
                        ent1.FormulaID = "Atr";
                        ent1.VariableValue = Atr.ToString();
                        AddToLog(ent1);
                //Maximum spacing of transverse reinforcement within ld, center-to-center, in.
                //for example the stirrup spacing
                // refer to PCA notes Example 4.3

                double s = transverseRebarSpacing;
                        ICalcLogEntry ent2 = Log.CreateNewEntry();
                        ent2.ValueName = "s";
                        ent2.DescriptionReference = "s-12.2.3";
                        ent2.FormulaID = "s";
                        ent2.VariableValue = s.ToString();
                        AddToLog(ent2);
                // n is usually the number of bars being develeoped
                // refer to PCA notes Example 4.3
                double n = numberOfBarsAlongSplittingPlane;
                        ICalcLogEntry ent3 = Log.CreateNewEntry();
                        ent3.ValueName = "n";
                        ent3.DescriptionReference = "n-12.2.3";
                        ent3.FormulaID = "n";
                        ent3.VariableValue = n.ToString();
            
            ICalcLogEntry ent = Log.CreateNewEntry();
            ent.ValueName = "Ktr";
                    ent.AddDependencyValue("Atr", transverseRebarArea);
                    ent.AddDependencyValue("s", transverseRebarSpacing);
                    ent.AddDependencyValue("n", numberOfBarsAlongSplittingPlane);
            ent.Reference = "ACI Eq. 12-2";
            ent.DescriptionReference = "Ktr";
            ent.FormulaID = "12-2";

            double Ktr = 40 * Atr / (s * n);
            ent.VariableValue = Ktr.ToString();

            return Ktr;
        }

[ReportElement(
new string[] { "ConfinementTerm", },
new string[] { "P-12.2.3-1", "P-12.2.3-2" },
new string[] { "ConfinementTerm1", "ConfinementTerm2" })]
           
        internal double GetConfinementTerm(double cb, double Ktr)
        {
            if (db==0.0)
            {
                throw new Exception("Rebar diameter cannot be 0.0. Chek input");
            }
            double ConfinementTerm;
            ICalcLogEntry ent1 = Log.CreateNewEntry();
            ent1.ValueName = "ConfinementTerm";
                ent1.AddDependencyValue("cb", cb);
                ent1.AddDependencyValue("Ktr", Ktr);
                ent1.AddDependencyValue("db", db);
            ent1.Reference = "ACI Section 12.2.3";
            ent1.DescriptionReference = "ConfinementTerm1";
            ent1.FormulaID = "P-12.2.3-1";
            double conf = (cb + Ktr) / db;
            ent1.VariableValue = conf.ToString();
            AddToLog(ent1);

            if (conf>2.5)
            {
                conf=ConfinementTerm = 2.5;
                
                ICalcLogEntry ent2 = Log.CreateNewEntry();
                ent2.ValueName = "ConfinementTerm";

                ent2.Reference = "ACI Section 12.2.3";
                ent2.DescriptionReference = "ConfinementTerm2";
                ent2.FormulaID = "P-12.2.3-2";
                ent2.VariableValue = ConfinementTerm.ToString();
                AddToLog(ent2);
            }
            else
            {
                ConfinementTerm = conf;
            }
            return conf;
        }
    }
}
