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
using Wosad.Common.Entities; 
using Wosad.Common.Section.Interfaces; 
using Wosad.Steel.AISC.Interfaces;
using Wosad.Common.CalculationLogger.Interfaces; 
using Wosad.Steel.AISC.Interfaces;
using Wosad.Steel.AISC.Code;

namespace  Wosad.Analytics.Steel.AISC360_10.Connections.AffectedElements
{
    public partial class AffectedElementInShear: AffectedElementBase
    {

        public AffectedElementInShear(ISteelSection Section, SteelDesignFormat DesignFormat, ICalcLog CalcLog)
            : base(Section, DesignFormat, CalcLog)
        {

        }

        public AffectedElementInShear(ISection Section, ISteelMaterial Material,  SteelDesignFormat DesignFormat, ICalcLog CalcLog)
            :base(Section,Material,DesignFormat, CalcLog)
        {

        }

        public double GetShearCapacity()
        {
            throw new NotImplementedException();
        }
    }
}