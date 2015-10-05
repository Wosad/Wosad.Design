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
using Wosad.Common.Interfaces;

namespace Wosad.Analysis
{
    public class BeamCantileverLoadFactory : BeamLoadFactory
    {
        //overrides beam load factory  for the cantilever beam case.
        public BeamCantileverLoadFactory(IParameterExtractor Extractor)
            : base(Extractor)
        {

        }
        public override LoadBeam GetConcentratedCase(string subCase)
        {
            LoadBeam Load = null;
            switch (subCase)
            {
                case "1":
                    Load = new LoadConcentratedSpecial(P, LoadConcentratedSpecialCase.CantileverTip);
                    break;
                default:
                    Load = new LoadConcentratedGeneral(P, LoadDimension_a);
                    break;
            }
            return Load;
        }

        public override LoadBeam GetPartialDistributedCase(string subCase)
        {
            return new  LoadDistributedGeneral(w,0.0,LoadDimension_b);
        }

        public override LoadBeam GetVaryingCase(string subCase)
        {
            LoadBeam Load = null;
            switch (subCase)
            {
                case "2":
                    Load = new LoadDistributedUniform(w, LoadDistributedSpecialCase.InvertedTriangle);
                    break;
                default:
                    Load = new LoadDistributedUniform(w, LoadDistributedSpecialCase.Triangle);
                    break;
            }
            return Load;
        }
    

    }
}
