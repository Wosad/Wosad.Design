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


namespace Wosad.Analysis.BeamForces.SimpleWithOverhang
{
    public class BeamSimpleWithOverhangFactory : IBeamCaseFactory
    {

        BeamSimpleWithOverhang beam;

        public ISingleLoadCaseBeam GetCase(LoadBeam load, IAnalysisBeam beam)
        {
            this.beam = beam as BeamSimpleWithOverhang;
            ISingleLoadCaseBeam BeamLoadCase = null;
            if (load is LoadConcentrated)
            {
                BeamLoadCase = GetConcentratedLoadCase(load);
            }
            else if (load is LoadDistributed)
            {
                BeamLoadCase = GetDistributedLoadCase(load);
            }

            return BeamLoadCase;
        }

        private ISingleLoadCaseBeam GetConcentratedLoadCase(LoadBeam load)
        {
            ISingleLoadCaseBeam beamCase = null;

            if (load is LoadConcentratedGeneral) //1B.1
            {
                LoadConcentratedGeneral cl = load as LoadConcentratedGeneral;
                beamCase = new ConcentratedLoadBetweenSupports(beam, cl.P, cl.XLocation);
            }
            else if (load is LoadConcentratedSpecial)
            {
                LoadConcentratedSpecial cl = load as LoadConcentratedSpecial;
                if (cl.Case == LoadConcentratedSpecialCase.CantileverTip)
                {
                    beamCase = new ConcentratedLoadOverhang(beam, cl.P, beam.OverhangLength);
                }
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
                    case LoadDistributedSpecialCase.CantileverOverhang:
                        beamCase = new  DistributedLoadOverhang(beam,cl.Value); //2C.2
                        break;
                    case LoadDistributedSpecialCase.CantileverMainSpan:
                        beamCase = new DistributedLoadBetweenSupports(beam, cl.Value); //2C.1
                        break;
                    default:
                        beamCase = new UniformLoadFull(beam, cl.Value); //2B.1
                        break;
                }
                
            }

            return beamCase;
        }

    }
}
