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
using Wosad.Concrete.ACI.Entities;

namespace Wosad.Concrete.ACI
{
    //prestressed strand
    public partial class AstmA416 : IPrestressedRebarMaterial
    {
        public AstmA416(Grade Grade, StrandType StrandType)
        {
            this.grade = Grade;
            this.strandType = StrandType;
            yieldStress = Grade == Grade.Grade250 ? 225000.0 : 243000.0; //psi
            fpu = Grade == Grade.Grade250 ? 250000.0 : 270000.0; //psi
        }

        private Grade grade;

        public Grade ReinforcementGrade
        {
            get { return grade; }
            set { grade = value; }
        }

        private StrandType strandType;

        public StrandType StrandType
        {
            get { return strandType; }
            set { strandType = value; }
        }

        double fpu;



         public double GetPermissibleStressAtJacking()
         {
             throw new NotImplementedException();
         }

         public double GetPermissibleStressAtTransfer()
         {
             throw new NotImplementedException();
         }

         public double GetStressAtNominalFlexuralStrength()
         {
             throw new NotImplementedException();
         }

         public string Name { get; set; }
         public double StressAtJacking { get; set; }
         public double StressAtTransfer { get; set; }
         public double StressEffective { get; set; }

         double yieldStress;
         public double YieldStress
         {
             get { return yieldStress; }
         }

         public double GetUltimateStrain(double Diameter)
         {
             throw new NotImplementedException();
         }

         public double GetStress(double Strain)
         {
             throw new NotImplementedException();
         }

         public double GetDesignStress()
         {
             throw new NotImplementedException();
         }


    }


}
