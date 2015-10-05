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
using Wosad.Steel.AISC.Exceptions;
 
 
 
 

namespace Wosad.Steel.AISC.AISC360_10.General.Compactness
{
    public class WebOfSinglySymI : WebOfDoublySymI
    {
         ISectionI SectionI;
         FlexuralCompressionFiberPosition compressionFiberPosition;

         public WebOfSinglySymI(ISteelMaterial Material, double Width, double Thickness,
             FlexuralCompressionFiberPosition compressionFiberPosition)
            :base(Material,Width,Thickness)
        {
            this.compressionFiberPosition = compressionFiberPosition;
        }

        public WebOfSinglySymI(ISteelMaterial Material, ISectionI SectionI,
            FlexuralCompressionFiberPosition compressionFiberPosition)
            :base(Material,SectionI)
        {
            this.SectionI = SectionI;
            this.compressionFiberPosition = compressionFiberPosition;

        }

        public override double GetLambda_p(StressType stress)
        {


            if (stress == StressType.Flexure)
            {

                double Mp = 0;
                double My = 0;
                double hc = 0;
                double hp = 0;
                double Sc = 0;

                switch (compressionFiberPosition)
                {
                    case FlexuralCompressionFiberPosition.Top:
                        Sc = SectionI.SectionModulusXTop;
                        hc = SectionI.Height - SectionI.CentroidYtoBottomEdge - SectionI.FlangeThicknessTop;
                        hp = SectionI.Height - SectionI.PlasticCentroidYtoBottomEdge - SectionI.FlangeThicknessTop;
                        break;
                    case FlexuralCompressionFiberPosition.Bottom:
                        Sc = SectionI.SectionModulusXBot;
                        hc = SectionI.CentroidYtoBottomEdge - SectionI.FlangeThicknessBottom;
                        hp = SectionI.PlasticCentroidYtoBottomEdge - SectionI.FlangeThicknessBottom;
                        break;
                    default:
                        throw new Exception("Left and Right Compression Fiber positions are not supported");
                }
                double Fy =  Material.YieldStress;
                My = Sc * Fy;
                double Z = SectionI.PlasticSectionModulusX;
                Mp = Z * Fy;

                double lambda_r = this.GetLambda_r(StressType.Flexure);

                double lambda_p;
                lambda_p = (hc/hp*SqrtE_Fy())/Math.Pow(0.54*(Mp/My)-0.09,2);
                lambda_p = lambda_p > lambda_r ? lambda_r : lambda_p;
                return lambda_p;
            }
            else
            {
                throw new ShapeParameterNotApplicableException("Lambda_p");
            }
        } 
    }
}
