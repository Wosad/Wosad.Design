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
using Wosad.Common.CalculationLogger.Interfaces;


namespace Wosad.Concrete.ACI318_11
{
    public partial class TensionLapSplice : Splice, ISplice
    {

        [ReportElement(
        new string[] { "lts", },
        new string[] { "P-12.15.1-1", "P-12.15.1-2" },
        new string[] { "lsDifferentDiameter", "lsA", "lsA" })]
           
        private double GetLs()
        {
            double ls;

            if (Rebar1Diameter>11/8 ||Rebar2Diameter>11/8)
            {
                throw new Exception("Lap splices not permittedn for sizes over #11");
            }

            double ld1 = Rebar1DevelopmentLength;
            double ld2 = Rebar2DevelopmentLength;

            double ls1;
            double ls2;

            if (Rebar1Diameter != Rebar2Diameter)
            {

               
                ICalcLogEntry ent1 = Log.CreateNewEntry();
                ent1.ValueName = "lts";
                    ent1.AddDependencyValue("lte1", ld1);
                    ent1.AddDependencyValue("lte2", ld2);
                ent1.Reference = "ACI Section 12.15.3";
                ent1.DescriptionReference = "lsDifferentDiameter";

                if (spliceClass == TensionLapSpliceClass.A)
                {
                    ls1 = ld1;
                    ls2 = ld2;
                            ls = Math.Max(ls1, ls2);
                    ent1.FormulaID = "P-12.15.1-1";
                    ent1.VariableValue = ls.ToString(); 

                    
                }
                else //class B
                {
                    ls1 = 1.3 * ld1;
                    ls2 = 1.3 * ld2;

                    ls =
                        Math.Min(ls1, ls2) > Math.Max(ld1, ld2) ?
                        Math.Min(ls1, ls2) :
                        Math.Max(ld1, ld2);
                        
                    ent1.FormulaID = "P-12.15.1-2";
                    ent1.VariableValue = ls.ToString(); 
                }

                AddToLog(ent1);
            }
            else //if both diameters are same
            {
                
                ICalcLogEntry ent1 = Log.CreateNewEntry();
                ent1.ValueName = "ls";
                ent1.AddDependencyValue("ld", ld1);
                ent1.Reference = "ACI Section 12.15.1";
                
                AddToLog(ent1);
                if (spliceClass == TensionLapSpliceClass.A)
                {
                ent1.DescriptionReference = "lsA";
                ent1.FormulaID = "P-12.15.1-1";
                        ls = 1.0 * ld1;
                }
                else
                {
                    ent1.DescriptionReference = "lsB";
                    ent1.FormulaID = "P-12.15.1-2";
                        ls = 1.3 * ld1;
                }
                ent1.VariableValue = ls.ToString();
                AddToLog(ent1);
            }


            return ls;
        }
    }
}
