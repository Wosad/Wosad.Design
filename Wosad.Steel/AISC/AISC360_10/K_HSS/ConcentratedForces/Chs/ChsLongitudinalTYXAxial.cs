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
 
using Wosad.Common.Mathematics;
using Wosad.Common.CalculationLogger.Interfaces; 
using Wosad.Steel.AISC.Interfaces;
using Wosad.Steel.AISC.SteelEntities.Sections;
using Wosad.Steel.AISC.Code;

namespace  Wosad.Analytics.Steel.AISC360_10.HSS.ConcentratedForces
{
    public class ChsLongitudinalTYXAxial: ChsToPlateConnection
    {
                private double angle;

        public double Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        public ChsLongitudinalTYXAxial(SteelChsSection Hss, SteelPlateSection Plate, double Angle, ICalcLog CalcLog)
            : base(Hss, Plate, CalcLog)
        {
            this.angle = Angle;
        }

        double GetAvailableStrength(SteelDesignFormat format, bool ConnectingSurfaceInTension, double UtilizationRatio)
        {
            double R = 0.0;
            //R = HssLocalYieldingLS(UtilizationRatio, ConnectingSurfaceInTension);
            return R;
        }

        double GetAvailableStrength(SteelDesignFormat format, bool ConnectingSurfaceInTension, double RequiredAxialStrenghPro, double RequiredMomentStrengthMro)
        {
            ISteelSection s = GetHssSteelSection();
            double U = GetUtilizationRatio(s, RequiredAxialStrenghPro, RequiredMomentStrengthMro);
            return this.GetAvailableStrength(format, ConnectingSurfaceInTension, U);
        }

        double HssPlastification(double UtilizationRatio, bool ConnectingSurfaceInTension)
        {
            double R = 0.0;
            double Rn = 0.0;

            double theta = this.angle;
            double sinTheta = Math.Sin(theta.ToRadians());

            double Fy = 0.0; double t = 0.0; double Bp = 0.0; double D = 0.0; double tp = 0.0;
            this.GetTypicalParameters(ref Fy, ref t, ref Bp, ref D,ref tp);
            double lb = tp; //TODO: Add differentiation of tp and lb heere

            double Qf = GetChordStressInteractionQf(UtilizationRatio, ConnectingSurfaceInTension);

            Rn = (5.5 * Fy * Math.Pow(t, 2) * (1.0 + 0.25 * lb / D) * Qf)/sinTheta; //(K1-2)

                R = 0.9 * Rn;


            return R;
        }

        double GetOutOfPlaneMomentForPlateBending(SteelDesignFormat format)
        {
            return 0.0;
        }

        double GetInPlaneMomentForPlateBending(SteelDesignFormat format, bool ConnectingSurfaceInTension, double UtilizationRatio)
        {
            double lb = Plate.Section.Width;
            double R = GetAvailableStrength(format, ConnectingSurfaceInTension, UtilizationRatio);
            return 0.8 * lb * R;
        }
    }
}
