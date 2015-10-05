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
    public class AffectedElementInFlexureAndShear: AffectedElementInShear, ISteelFlexuralMember
    {

        public AffectedElementInFlexureAndShear(ISteelSection Section, SteelDesignFormat DesignFormat, ICalcLog CalcLog)
            : base(Section, DesignFormat, CalcLog)
        {

        }

        public AffectedElementInFlexureAndShear(ISection Section, ISteelMaterial Material, SteelDesignFormat DesignFormat, ICalcLog CalcLog)
            :base(Section,Material, DesignFormat, CalcLog)
        {

        }

       #region ISteelFlexuralMember Members

       public double UnbracedLengthFlexure
       {
           get
           {
               throw new NotImplementedException();
           }
           set
           {
               throw new NotImplementedException();
           }
       }

       public double EffectiveLengthFactorFlexure
       {
           get
           {
               throw new NotImplementedException();
           }
           set
           {
               throw new NotImplementedException();
           }
       }

       public double GetFlexuralCapacityMajorAxis( FlexuralCompressionFiberPosition compressionFiberLocation)
       {
           throw new NotImplementedException();
       }

       public double GetFlexuralCapacityMinorAxis( FlexuralCompressionFiberPosition compressionFiberLocation)
       {
           throw new NotImplementedException();
       }

       #endregion
    }
}
