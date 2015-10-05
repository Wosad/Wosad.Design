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
using Wosad.Common.Section.Interfaces;


namespace Wosad.Common.Section.Predefined
{
    /// <summary>
    /// Predefined sections are used in case when 
    /// section properties are known: for example in cases of
    /// catalog sections or when section properties are provided by user.
    /// </summary>
    public abstract class SectionPredefinedBase : SectionBase
    {

        public abstract override ISection Clone();

        public SectionPredefinedBase()
        {

        }
        public SectionPredefinedBase(ISection sec)
        {
           this.Area                  = sec.Area                        ;
           this.I_x      = sec.MomentOfInertiaX            ;
           this.I_y      = sec.MomentOfInertiaY            ;
           this.S_x_Top    = sec.SectionModulusXTop          ;
           this.S_x_Bot    = sec.SectionModulusXBot          ;
           this.S_y_Left   = sec.SectionModulusYLeft         ;
           this.S_y_Right  = sec.SectionModulusYRight        ;
           this.Z_x= sec.PlasticSectionModulusX      ;
           this.Z_y= sec.PlasticSectionModulusY      ;
           this.r_x     = sec.RadiusOfGyrationX           ;
           this.r_y     = sec.RadiusOfGyrationY           ;
           this.elasticCentroidCoordinate.X = sec.CentroidXtoLeftEdge;
           this.elasticCentroidCoordinate.Y = sec.CentroidYtoBottomEdge;
           this.plasticCentroidCoordinate.X     = sec.PlasticCentroidXtoLeftEdge           ;
           this.plasticCentroidCoordinate.Y    = sec.PlasticCentroidYtoBottomEdge         ;
           this.C_w       = sec.WarpingConstant             ;
           this.J     = sec.TorsionalConstant           ;
        }
    }
}
