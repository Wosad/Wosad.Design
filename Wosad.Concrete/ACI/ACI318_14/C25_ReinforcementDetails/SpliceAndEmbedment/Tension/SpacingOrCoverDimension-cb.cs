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
using p = Wosad.Concrete.ACI318_11.TensionDevelopmentParagraphs;
using f = Wosad.Concrete.ACI318_11.TensionDevelopmentFormulas;
using v = Wosad.Concrete.ACI318_11.TensionDevelopmentValues;
using d = Wosad.Concrete.ACI318_11.TensionDevelopmentDescriptions;
using dv = Wosad.Concrete.ACI318_11.DevelopmentValues;
using Wosad.Common.CalculationLogger.Interfaces;

namespace Wosad.Concrete.ACI318_11
{
    public partial class DevelopmentTension: Development
    {
        //cb = smaller of: 
        //(a) the distance from center of a
        //bar or wire to nearest concrete surface, and
        
        //(b) one-half the center-to-center spacing of
        //bars or wires being developed, in.,
        internal double GetCb()
        {
            double DistFromCenterToSurface = clearCover + Rebar.Diameter / 2.0;
            double HalfOfCenterToCenterDistance = (clearSpacing + Rebar.Diameter)/2.0;
            double cb= Math.Min(DistFromCenterToSurface, HalfOfCenterToCenterDistance);

            
            ICalcLogEntry ent = Log.CreateNewEntry();
            ent.ValueName = v.cb;
            ent.AddDependencyValue(v.cc, clearCover);
            ent.AddDependencyValue(v.cl_s, clearSpacing);
            ent.AddDependencyValue(v.bar_surface_distance_center, DistFromCenterToSurface);
            ent.AddDependencyValue(v.bar_half_distance_center, HalfOfCenterToCenterDistance);
            ent.Reference = "ACI Section R12.2";
            ent.DescriptionReference = d.cb;
            ent.FormulaID = f.cb;
            ent.VariableValue = cb.ToString();
            AddToLog(ent);

            return cb;
        }
    }
}

