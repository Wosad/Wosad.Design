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
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Interfaces;

using Wosad.Steel.AISC.Interfaces;
using d = Wosad.Steel.AISC.AISC360v10.Connections.Bolted.BoltDescriptions;
using f = Wosad.Steel.AISC.AISC360v10.Connections.Bolted.BoltFormulas;
using v = Wosad.Steel.AISC.AISC360v10.Connections.Bolted.BoltValues;

namespace Wosad.Steel.AISC.AISC360v10.Connections.Bolted
{
    public abstract partial class BoltSlipCritical : Bolt, IBoltSlipCritical
    {

        public double GetReducedSlipResistance(double T_u)
        {
            double phiR_n = this.GetSlipResistance();
            double k_sc = GetSlipResistanceReductionFactor(T_u);
            return k_sc * phiR_n;
        }
        public double GetSlipResistanceReductionFactor(double T_u)
        {
            double ksc = 0.0;
            //Get tension per bolt
            double Du = pretensionMultiplier;
            double phiR_n = this.GetAvailableTensileStrength();

            if (T_u>phiR_n)
            {
                throw new Exception("Bolt factored force exceeeds capacity. Reduced slip strength cannot be calculated.");
            }
            ICalcLogEntry ent = Log.CreateNewEntry();
            ent.ValueName = v.ksc;
            ent.DescriptionReference = d.ksc;

            if (T_b==0.0)
            {
                throw new Exception("Bolt pretension cannot be zero");
            }

            if (Du == 0.0)
            {
                throw new Exception("Multiplier that reflects the ratio of the mean installed bolt pretension to the specified minimum bolt pretension cannot be zero");
            }

                ksc = 1.0-T_u/(Du*T_b);
                ent.AddDependencyValue(v.Tu, T_u);
                ent.Reference = "AISC Formula J3-5a";
                ent.FormulaID = f.J3_5.LRFD;
                ent.VariableValue = ksc.ToString();


            AddToLog(ent);

            return ksc;
        }
    }
}
