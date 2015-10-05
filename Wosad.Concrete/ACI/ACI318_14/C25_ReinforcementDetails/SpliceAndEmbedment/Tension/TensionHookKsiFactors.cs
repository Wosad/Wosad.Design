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
using v = Wosad.Concrete.ACI318_11.TensionHookValues;
using d = Wosad.Concrete.ACI318_11.TensionHookDescriptions;
using f = Wosad.Concrete.ACI318_11.TensionHookFormulas;


namespace Wosad.Concrete.ACI318_11
{
    public partial class StandardHookInTension : Development
    {

[ReportElement(
new string[] { v.ksi_e },
new string[] { f._5._2_2},
new string[] { d.ksi_e  })]
           
        private double GetKsi_e()
        {
            ICalcLogEntry ent = Log.CreateNewEntry();
            ent.ValueName = v.ksi_e;
            ent.Reference = "ACI Section 12.5.2";
            double ksi_e;
            ent.DescriptionReference = d.ksi_e;
            ent.FormulaID = f._5._2_2;

            if (Rebar.IsEpoxyCoated==true)
            {

                ksi_e = 1.2;
            }
            else
            {
                ksi_e = 1.0;
            }
            ent.VariableValue = ksi_e.ToString();
            AddToLog(ent);

            return ksi_e;
        }
    }
}
