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
using Wosad.Steel.AISC.Interfaces;
 

namespace  Wosad.Steel.AISC360_10.HSS.ConcentratedForces
{
    public abstract partial class RhsToPlateConnection: HssToPlateConnection
    {


        public double GetChordStressInteractionQf(bool ConnectingSurfaceInTension)
        {
            return this.GetChordStressInteractionQf(PlateOrientation.Transverse, 0.0, ConnectingSurfaceInTension);
        }

        public double GetChordStressInteractionQf(PlateOrientation PlateOrientation, double RequiredAxialStrenghPro, double RequiredMomentStrengthMro)
        {
            ISteelSection s = GetHssSteelSection();
            double U = GetUtilizationRatio(s, RequiredAxialStrenghPro, RequiredMomentStrengthMro);
            return this.GetChordStressInteractionQf(PlateOrientation, U, false);
        }

        public double GetChordStressInteractionQf(PlateOrientation PlateOrientation, double HssUtilizationRatio)
        {

            return this.GetChordStressInteractionQf(PlateOrientation, HssUtilizationRatio, false);
        }

        internal double GetChordStressInteractionQf(PlateOrientation PlateOrientation, double HssUtilizationRatio, bool ConnectingSurfaceInTension)
        {
            double U = HssUtilizationRatio;
            double Qf = 0.0;
            double Beta = GetBeta();

            if (ConnectingSurfaceInTension==false)
            {
                if (PlateOrientation == PlateOrientation.Transverse)
                {
                    //(K1-16)
                    Qf = 1.3 - 0.4 * U / Beta;
                    Qf = Qf < 1.0 ? Qf : 1.0;
                }
                else
                {
                    //(K1-17)
                    Qf = Math.Sqrt(1.0 - Math.Pow(U, 2));
                } 
            }
            else
            {
                Qf = 1.0;
            }
            return Qf;
        }
    }
}
