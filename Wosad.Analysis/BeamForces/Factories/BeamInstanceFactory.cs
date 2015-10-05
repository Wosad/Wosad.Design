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
using Wosad.Analysis.BeamForces;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Interfaces;

namespace Wosad.Analysis
{
    public class BeamInstanceFactory
    {
        IParameterExtractor e;

        public BeamInstanceFactory(IParameterExtractor Extractor)
        {
            this.e = Extractor;
        }


        public Beam CreateBeamInstance(string BeamCaseId, List<LoadBeam> loads, ICalcLog Log)
        {
            double L = e.GetParam("L");
            double LoadDimension_a = e.GetParam("LoadDimension_a");

            Beam bm = null;
            if (BeamCaseId.StartsWith("C1") == true)
            {
                bm = new BeamSimple(L, loads, Log);
            }
            else if (BeamCaseId.StartsWith("C2") == true)
            {
                bm = new BeamSimpleWithOverhang(L, LoadDimension_a, loads, Log);
            }
            else if (BeamCaseId.StartsWith("C3") == true)
            {
                bm = new BeamPinnedFixed(L,loads, Log);
            }
            else if (BeamCaseId.StartsWith("C4") == true)
            {
                bm = new BeamFixedFixed(L, loads, Log);
            }
            else //else if (BeamCaseId.StartsWith("C5") == true)
            {
                bm = new BeamCantilever(L, loads, Log);
            }

            return bm;
        }
    }
}
