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
using dv = Wosad.Concrete.ACI318_11.DevelopmentValues;
using df = Wosad.Concrete.ACI318_11.DevelopmentFormulas;
using v = Wosad.Concrete.ACI318_11.TensionHookValues;
using d = Wosad.Concrete.ACI318_11.TensionHookDescriptions;
using f = Wosad.Concrete.ACI318_11.TensionHookFormulas;
using gv = Wosad.Concrete.ACI318_11.GeneralValues;
using gd = Wosad.Concrete.ACI318_11.GeneralDescriptions;
using gf = Wosad.Concrete.ACI318_11.GeneralFormulas;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Concrete.ACI.Entities;

namespace Wosad.Concrete.ACI318_11
{
    public partial class StandardHookInTension : Development
    {

[ReportElement(
new string[] {gv.lambda },
new string[] {gf.lambda },
new string[] {d.lambda })]
           
        private double GetHookLambda()
        {
            double lambda;
            
            ICalcLogEntry ent = Log.CreateNewEntry();
            ent.ValueName = "lambda";
            ent.Reference = "ACI Section 12.5.2";
            ent.DescriptionReference = "lambda";
            ent.FormulaID = "P-12.5.2-1";

            if (Concrete.TypeByWeight== ConcreteTypeByWeight.Lightweight)
            {
                lambda = 0.75;
            }
            else
            {
                lambda = 1.0;     
            }
            ent.VariableValue = lambda.ToString();
            AddToLog(ent);

            return lambda;
        }
    }
}
