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
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Steel.AISC.Code;

namespace Wosad.Steel.AISC.AISC360_10.Connections.Weld
{
    public class PJPGrooveWeld : GrooveWeld
    {

        public PJPGrooveWeld(double Fy, double Fu, double Fexx, double Size,   ICalcLog Log)
            : base(Fy, Fu, Fexx, Size, Log)
        {

        }

        public PJPGrooveWeld(double Fy, double Fu, double Fexx, double Size)
            : base(Fy, Fu, Fexx, Size)
        {

        }
        /// <summary>
        /// Gets design compression stress per AISC Table J2.5 Available Strength of Welded Joints
        /// </summary>
        /// <param name="typeOfConnection"></param>
        /// <returns></returns>
        public double GetCompressionDesignStress(TypeOfCompressionLoading typeOfConnection)
        {
            double s = 0.0;
            if (typeOfConnection == TypeOfCompressionLoading.Column)
            {
                //Compressive stress need not be considered in design of welds joining the parts.
                // Base metal provisions are specified here
                double phi1 = 0.9;
                double phiR_n1 = phi1* this.BaseMaterial.YieldStress;
                s = phiR_n1;
            }
            else if (typeOfConnection == TypeOfCompressionLoading.NonColumnFinishedToBear)
	            {
                    //Base metal
                    double phi1 = 0.9;
                    double phiR_n1 = phi1* this.BaseMaterial.YieldStress;
                    //Weld metal
                    double phi2 = 0.8;
                    double phiR_n2 = phi2* 0.9 * this.WeldMaterial.ElectrodeStrength;
                    s =Math.Min(phiR_n1,phiR_n2);
	            }
            else
	            {
                    //Base metal
                    double phi1  =0.9 ;
                    double phiR_n1 = phi1* this.BaseMaterial.YieldStress;
                    //Weld metal
                    double phi2 = 0.8;
                    double phiR_n2 = phi2* 0.6 * this.WeldMaterial.ElectrodeStrength;
                    s =Math.Min(phiR_n1,phiR_n2);
	            }
            return s;
         }
        /// <summary>
        /// Gets design tension stress per AISC Table J2.5 Available Strength of Welded Joints
        /// </summary>
        /// <param name="typeOfConnection"></param>
        /// <returns></returns>
        public double GetTensionNormalToWeldAxisDesignStress()
        {
            //Base metal strength
            double phi_1 = 0.75;
            double phiR_n1 = phi_1 * this.BaseMaterial.UltimateStress;
            // Weld strength 
            double phi2 =0.8;
            double phiR_n2 = phi2* 0.6 * this.WeldMaterial.ElectrodeStrength;

            double s = Math.Min(phiR_n1, phiR_n2);
            return s;
        }
        /// <summary>
        /// Gets design shear stress per AISC Table J2.5 Available Strength of Welded Joints
        /// </summary>
        /// <param name="typeOfConnection"></param>
        /// <returns></returns>
        public double GetShearDesignStress()
        {

           //Weld metal
            double phi = 0.75;
            double phiR_n = phi* 0.6 * this.WeldMaterial.ElectrodeStrength;
                    
            throw new NotImplementedException();
        }

        public double GetMinimumEffectiveThroat(double MaterialThicknessOfThinnerPartJoined)
        {
            double matThickness = MaterialThicknessOfThinnerPartJoined;
            double t_min = 1 / 8;

            if (matThickness <= 1 / 4) { t_min = 1 / 8; }
            else if (matThickness > 1 / 4 && matThickness <= 1 / 2) { t_min = 3 / 16; }
            else if (matThickness > 1 / 2 && matThickness <= 3 / 4) { t_min = 1 / 4; }
            else if (matThickness > 3 / 4 && matThickness <= 11 / 2) { t_min = 5 / 16; }
            else if (matThickness > 11 / 2 && matThickness <= 21 / 4) { t_min = 3 / 8; }
            else if (matThickness > 21 / 4 && matThickness <= 6) { t_min = 1 / 2; }
            else { t_min = 5 / 8; }

            return t_min;
        }
    }
}
