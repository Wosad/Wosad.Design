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


namespace Wosad.Analysis.BeamForces.PinnedFixed
{
    public class BeamPinnedFixedFactory : IBeamCaseFactory
    {
        BeamPinnedFixed beam;
        public ISingleLoadCaseBeam GetCase(LoadBeam load, IAnalysisBeam beam)
        {
            this.beam = beam as BeamPinnedFixed;

            ISingleLoadCaseBeam BeamLoadCase = null;
            if (load is LoadConcentrated)
            {
                BeamLoadCase = GetConcentratedLoadCase(load);
            }
            else if (load is LoadDistributed)
            {
                BeamLoadCase = GetDistributedLoadCase(load);
            }
            else if (load is LoadMoment)
            {
                BeamLoadCase = GetMomentLoadCase(load);
            }

            return BeamLoadCase;
        }

        private ISingleLoadCaseBeam GetDistributedLoadCase(LoadBeam load)
        {
            ISingleLoadCaseBeam beamCase = null;
            if (load is LoadDistributedUniform)
            {
                LoadDistributedUniform cl = load as LoadDistributedUniform;
                beamCase = new UniformlyDistributedLoad(beam, cl.Value); //3B.1

                return beamCase;
            }

            if (load is LoadDistributedGeneral) //3C.1
            {
                LoadDistributedGeneral cl = load as LoadDistributedGeneral;
                beamCase = new UniformPartialLoad(beam, cl.Value, cl.XLocationStart, 
                    cl.XLocationEnd - cl.XLocationStart);
            }
            return beamCase;
        }

        private ISingleLoadCaseBeam GetConcentratedLoadCase(LoadBeam load)
        {
            ISingleLoadCaseBeam beamCase = null;
            if (load is LoadConcentratedSpecial) //3A.1
            {
                LoadConcentratedSpecial cl = load as LoadConcentratedSpecial;
                beamCase = new ConcentratedLoadAtCenter(beam, cl.P);
            }
            if (load is LoadConcentratedGeneral) //3A.2
            {
                LoadConcentratedGeneral cl = load as LoadConcentratedGeneral;
                beamCase = new ConcentratedLoadAtAnyPoint(beam, cl.P, cl.XLocation);
            }
            return beamCase; 
        }

        private ISingleLoadCaseBeam GetMomentLoadCase(LoadBeam load)
        {
            ISingleLoadCaseBeam beamCase = null;
            LoadMomentLeftEnd cl = load as LoadMomentLeftEnd; //3E.1
            beamCase = new MomentAtFreeEnd(beam, cl.Mo);
            return beamCase; 
        }

    }
}
