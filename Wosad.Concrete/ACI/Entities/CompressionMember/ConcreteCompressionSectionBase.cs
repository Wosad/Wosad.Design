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
using Wosad.Common.Entities;
using Wosad.Common.Mathematics;
using Wosad.Common.Section.Interfaces;

 

namespace Wosad.Concrete.ACI
{
    public abstract partial class ConcreteCompressionSectionBase: ConcreteFlexuralSectionBase
    {

        public ConcreteCompressionSectionBase(IConcreteSection Section,
            List<RebarPoint> LongitudinalBars, ICalcLog log)
            : base(Section, LongitudinalBars, log)
        {

        }

        public  double GetNominalMoment(double P_u, FlexuralCompressionFiberPosition CompressionFiberPosition )
        {

            TCIterationBound bound = GetCompressionSolutionBoundaries(CompressionFiberPosition); 
            SectionAnalysisResult IteratedResult = FindResultByVaryingSteelStrain(CompressionFiberPosition, bound,P_u);
            double M_n = IteratedResult.Moment;

            return M_n;
        }

        private TCIterationBound GetCompressionSolutionBoundaries(FlexuralCompressionFiberPosition CompressionFiberPosition)
        {
            
            double MaxSteelStrain = CalculateMaximumSteelStrain(CompressionFiberPosition);
            
            TCIterationBound bound = new TCIterationBound();
            bound.MaxStrain = StrainUltimateConcrete.Value;
            bound.MinStrain = -MaxSteelStrain;
            

            if (CompressionFiberPosition == FlexuralCompressionFiberPosition.Left && CompressionFiberPosition == FlexuralCompressionFiberPosition.Right)
            {
                throw new Exception("Weak axis column interaction is not supported.");
            }

            return bound;

        }

        /// <summary>
        /// Pure axial strength. Implementation here is for Non prestressed section
        /// </summary>
        /// <returns></returns>
        protected virtual double GetP_o()
        {
            //22.4.2.2 
            double A_g = Section.SliceableShape.A;
            double A_s = LongitudinalBars.Sum(b => b.Rebar.Area);
            double f_c = Section.Material.SpecifiedCompressiveStrength;
            double f_yA_s = LongitudinalBars.Sum(b => b.Rebar.Area * b.Rebar.Material.YieldStress);
            //(22.4.2.2)
            double P_o = 0.85 * f_c * (A_g - A_s)+f_yA_s;
            return P_o;
        }




    }
}
