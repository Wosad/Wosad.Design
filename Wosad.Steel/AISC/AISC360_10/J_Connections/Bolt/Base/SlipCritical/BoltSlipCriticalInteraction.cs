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
using Wosad.Steel.AISC.Code;
using Wosad.Steel.AISC.Interfaces;
using d = Wosad.Steel.AISC.AISC360_10.Connections.Bolted.BoltDescriptions;
using f = Wosad.Steel.AISC.AISC360_10.Connections.Bolted.BoltFormulas;
using v = Wosad.Steel.AISC.AISC360_10.Connections.Bolted.BoltValues;

namespace Wosad.Steel.AISC.AISC360_10.Connections.Bolted
{
    public abstract partial class BoltSlipCritical : Bolt, IBoltSlipCritical
    {

        public double GetSlipResistanceReductionFactor()
        {
            double ksc = 0.0;
            //Get tension per bolt
            double T= Math.Abs(this.FindMaximumForce(ForceType.F1, true).F1);
            double Tb = MinimumPretension;
            double Du = pretensionMultiplier;

            
            ICalcLogEntry ent = Log.CreateNewEntry();
            ent.ValueName = v.ksc;
            ent.DescriptionReference = d.ksc;

            if (Tb==0.0)
            {
                throw new Exception("Bolt pretension cannot be zero");
            }

            if (Du == 0.0)
            {
                throw new Exception("Multiplier that reflects the ratio of the mean installed bolt pretension to the specified minimum bolt pretension cannot be zero");
            }
            

            if (DesignFormat == SteelDesignFormat.LRFD)
            {
                ksc = 1.0-T/(Du*Tb);
                ent.AddDependencyValue(v.Tu, T);
                ent.Reference = "AISC Formula J3-5a";
                ent.FormulaID = f.J3_5.LRFD;
                ent.VariableValue = ksc.ToString();
            }
            else
            {
                ksc = 1.0 - 1.5 * T / (Du * Tb);
            }

            AddToLog(ent);

            return ksc;
        }
    }
}
