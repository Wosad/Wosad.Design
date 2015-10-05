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
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Wood.NDS.Entities;
using Wosad.Wood.Properties;


namespace Wosad.Wood.NDS.NDS_2015.Material
{
    public class SouthernPine : WoodSolidMaterial
    {
        string Species;
        CommercialGrade Grade;
        SizeClassification SizeClass;
        public SouthernPine(string Species, CommercialGrade Grade, SizeClassification SizeClass, ICalcLog CalcLog)
            :base(Resources.NDS2012_Table4B, Species,Grade.ToString(), CalcLog)
        {
             this.Species   =Species;
             this.Grade     =Grade;
             this.SizeClass =SizeClass;
        }


        protected override string GetResource()
        {
            throw new NotImplementedException();
        }

        protected override void CreateReport()
        {
            throw new NotImplementedException();
        }
    }
}
