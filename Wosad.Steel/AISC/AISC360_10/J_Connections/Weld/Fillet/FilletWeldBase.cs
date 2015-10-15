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

namespace Wosad.Steel.AISC.AISC360_10.J_Connections.Weld
{
    public abstract partial class FilletWeldBase : Weld
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Fy">Base metal yield stress</param>
        /// <param name="Fu">Base metal ultimate stress</param>
        /// <param name="Fexx">Electrode strength</param>
        /// <param name="Size">Weld leg size</param>
        /// <param name="Log">Calculation log (for report generation)</param>
        public FilletWeldBase(double Fy, double Fu, double Fexx, double Size, ICalcLog Log)
            : base(Fy, Fu, Fexx, Size,Log)
        {

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Fy">Base metal yield stress</param>
        /// <param name="Fu">Base metal ultimate stress</param>
        /// <param name="Fexx">Electrode strength</param>
        /// <param name="Size">Weld leg size</param>
        public FilletWeldBase(double Fy, double Fu, double Fexx, double Size)
            : base(Fy, Fu, Fexx, Size)
        {

        }

        /// <summary>
        /// Weld lize (leg length)
        /// </summary>

        private double b;

        public double Size
        {
            get { return b; }
            set { b = value; }
        }
        

        /// <summary>
        /// Gets design shear stress per AISC Table J2.5 Available Strength of Welded Joints
        /// </summary>
        /// <param name="typeOfConnection"></param>
        /// <returns></returns>
        public double GetShearStrength()
        {

            //Weld metal
            double f1 = DesignFormat == SteelDesignFormat.LRFD ? 0.8 : 1 / 1.88;
            double s1 = f1 * 0.6 * this.WeldMaterial.ElectrodeStrength;
            return s1;
        }

        /// <summary>
        /// Effective throat area
        /// </summary>
        /// <returns>Effective area through plane 2-2 of weld per commentary J2.4 Figure C-J2.10 </returns>
        public double GetEffectiveAreaPerUnitLength()
        {
            double throat = Size * Math.Cos(Math.PI / 4.0);
            return throat;
        }
    }
}
