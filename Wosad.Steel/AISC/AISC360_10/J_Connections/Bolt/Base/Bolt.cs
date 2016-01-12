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

using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Steel.AISC.SteelEntities.Bolts;
using d = Wosad.Steel.AISC.AISC360_10.Connections.Bolted.BoltDescriptions;
using f = Wosad.Steel.AISC.AISC360_10.Connections.Bolted.BoltFormulas;
using v = Wosad.Steel.AISC.AISC360_10.Connections.Bolted.BoltValues;

namespace Wosad.Steel.AISC.AISC360_10.Connections.Bolted
{
    public abstract partial class Bolt: BoltBase
    {
        public Bolt(double Diameter, BoltThreadCase ThreadType, 
            ICalcLog log): base(Diameter,
            ThreadType,log)
        {
            BoltHoleSizeCalculated = false;
        }

        public Bolt(double Diameter,ICalcLog log)
            : base(Diameter,
                BoltThreadCase.Included, log)
        {
            BoltHoleSizeCalculated = false;
        }

        public override abstract double NominalTensileStress { get; }
        public override abstract double NominalShearStress { get; }

        public override double GetAvailableTensileStrength()
        {
            double Ab = this.Area;//nominal unthreaded bolt area
            double Fnt = NominalTensileStress;
            double phiR_n;
            double Rn = Ab*Fnt; //Formula  J3-1

            ICalcLogEntry ent = Log.CreateNewEntry();

                phiR_n = 0.75 * Rn;
                ent.ValueName = v.phiRn;
                ent.DescriptionReference = d.phiRn.TensileStrength;
                ent.FormulaID = f.J3_1.LRFD;

            
            ent.AddDependencyValue(v.Fnt, Fnt);
            ent.AddDependencyValue(v.Ab, Ab);
            ent.Reference = "AISC Formula J3-1";
            ent.VariableValue = Rn.ToString();
            AddToLog(ent);

            return phiR_n;
        }

        public override double GetAvailableShearStrength(double N_ShearPlanes)
        {
            double Ab = this.Area;//nominal unthreaded bolt area
            double Fnv = NominalShearStress;
            double R;
            double Rn = Ab * Fnv; //Formula  J3-1

            ICalcLogEntry ent = Log.CreateNewEntry();


            R = 0.75 * Rn * N_ShearPlanes;
                ent.ValueName = v.phiRn;
                ent.DescriptionReference = d.phiRn.ShearStrength;
                ent.FormulaID = f.J3_1.LRFD;


            ent.AddDependencyValue(v.Fnv, Fnv);
            ent.AddDependencyValue(v.Ab, Ab);
            ent.Reference = "AISC Formula J3-1";
            ent.VariableValue = R.ToString();
            AddToLog(ent);

            return R;
        }


        
    }
}
