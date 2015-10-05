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


namespace Wosad.Analysis.BeamForces.Simple
{
    public class BeamSimpleFactory : IBeamCaseFactory
    {
        BeamSimple beam;
        //BeamSimple simpleBeam;

        public ISingleLoadCaseBeam GetCase(LoadBeam load, IAnalysisBeam beam)
        {
            this.beam = beam as BeamSimple;
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

        private ISingleLoadCaseBeam GetConcentratedLoadCase(LoadBeam load)
        {
            ISingleLoadCaseBeam beamCase = null;
            if (load is LoadConcentratedSpecial) //1A.1
            {
                LoadConcentratedSpecial cl = load as LoadConcentratedSpecial;
                 beamCase = new ConcentratedLoadAtCenter(beam, cl.P);
            }
            if (load is LoadConcentratedGeneral) //1A.2
            {
                LoadConcentratedGeneral cl = load as LoadConcentratedGeneral;
                beamCase = new ConcentratedLoadAtAnyPoint(beam, cl.P, cl.XLocation);
            }
            if (load is LoadConcentratedDoubleSymmetrical) //1A.3
            {
                LoadConcentratedDoubleSymmetrical cl = load as LoadConcentratedDoubleSymmetrical;
                 beamCase =
                    new TwoConcentratedLoadsSymmetrical(beam, cl.P, cl.Dimension_a);
            }

            if (load is LoadConcentratedDoubleUnsymmetrical) //1A.4
            {
                LoadConcentratedDoubleUnsymmetrical cl = load as LoadConcentratedDoubleUnsymmetrical;
                 beamCase =
                    new TwoConcentratedLoadsUnsymmetrical(beam, cl.P1, cl.P2, cl.Dimension_a, cl.Dimension_b);
            }
            if (load is LoadConcentratedCenterWithEndMoments) //1A.5
            {
                LoadConcentratedCenterWithEndMoments cl = load as LoadConcentratedCenterWithEndMoments;
                 beamCase =
                    new ConcentratedLoadAtCenterAndVarEndMoments(beam, cl.P, cl.M1, cl.M2);
            }
            return beamCase;
        }

        private ISingleLoadCaseBeam GetDistributedLoadCase(LoadBeam load)
        {
            ISingleLoadCaseBeam beamCase = null;

            if (load is LoadDistributedUniform) 
            {
                LoadDistributedUniform cl = load as LoadDistributedUniform;
                switch (cl.Case)
                {
                    case LoadDistributedSpecialCase.Triangle:
                        beamCase = new DistributedUniformlyIncreasingToEnd(beam, cl.Value);//1D.1
                        break;
                    case LoadDistributedSpecialCase.DoubleTriangle:
                        beamCase = new DistributedDoubleTriangle(beam, cl.Value); //1D.2
                        break;
                    default:
                        beamCase = new UniformLoad(beam, cl.Value); //1B.1
                        break;
                }
                
            }

            if (load is LoadDistributedUniformWithEndMoments) //1B.2
            {
                LoadDistributedUniformWithEndMoments cl = load as LoadDistributedUniformWithEndMoments;
                beamCase = new UniformLoadAndEndMoments(beam, cl.Value, cl.M1, cl.M2);
            }

            if (load is LoadDistributedGeneral) //1C.1
            {
                LoadDistributedGeneral cl = load as LoadDistributedGeneral;
                beamCase = new UniformPartialLoad(beam,cl.Value,cl.XLocationStart,cl.XLocationEnd-cl.XLocationStart);
            }

            return beamCase;
        }
        private ISingleLoadCaseBeam GetMomentLoadCase(LoadBeam load)
        {
            ISingleLoadCaseBeam beamCase = null;

            if (load is LoadMomentLeftEnd)//1E.1
            {
                LoadMomentLeftEnd cl = load as LoadMomentLeftEnd;
                beamCase = new MomentAtOneEnd(beam, cl.Mo);
            }
            if (load is LoadMomentGeneral)//1E.2
            {
                LoadMomentGeneral cl = load as LoadMomentGeneral;
                beamCase = new MomentAtAnyPoint(beam, cl.Mo,cl.Location);
            }
            if (load is LoadMomentBothEnds)//1E.3
            {
                LoadMomentBothEnds cl = load as LoadMomentBothEnds;
                beamCase = new MomentAtBothEnds(beam, cl.M1,cl.M2 );
            }
            if (load is LoadMomentRightEnd)//1E.4
            {
                LoadMomentRightEnd cl = load as LoadMomentRightEnd;
                beamCase = new MomentAtFarEnd(beam, cl.Mo);
            }
            return beamCase;
        }

    }
}
