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
using Wosad.Concrete.ACI;
using Wosad.Concrete.ACI.Infrastructure.Entities.Rebar;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Concrete.ACI.Infrastructure.Entities.Section.Strains;
using Wosad.Common.Section.Interfaces;

namespace Wosad.Concrete.ACI318_11
{
    /// <summary>
    ///  This class encpsulates all sectional (flexure, shear axial load )analysis per ACI.
    /// </summary>
    public partial class ConcreteSection : ConcreteFlexuralSectionBase
    {


        /// <summary>
        ///  Constructor used for flexure and axial load analysis.
        /// </summary>
        public ConcreteSection(IConcreteSection Section, List<RebarPoint> Bars, ICalcLog log)
            : base(Section, Bars, log)
        {
            //set default analysis type to strain compatibility
            AnalysisType = FlexuralAnalysisType.StrainCompatibility;
        }

        /// <summary>
        ///  Constructor used for shear analysis.
        /// </summary>
        public ConcreteSection(IConcreteSection Section, ICalcLog log)
            : base(Section, null, log)
        {
            //set default analysis type to strain compatibility
            AnalysisType = FlexuralAnalysisType.StrainCompatibility;
        }

    }
}
