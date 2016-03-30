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

using Wosad.Steel.AISC.SteelEntities.Materials;
using Wosad.Steel.AISC.SteelEntities;
using Wosad.Steel.AISC.SteelEntities.Sections;
using Wosad.Common.Section.SectionTypes;
using Wosad.Steel.AISC.AISC360v10.Compression;
using Wosad.Common.Mathematics;

namespace Wosad.Steel.AISC360v10.Connections.AffectedElements
{
    public partial class AffectedElement
    {
        public double GetWhitmoreSectionWidth(double l, double b_con)
        {
            double WhitmoreAngle = 30;
            double b_Whitmore = l * 2*Math.Tan(WhitmoreAngle.ToRadians()) + b_con;
            return b_Whitmore;
        }
    }
}
