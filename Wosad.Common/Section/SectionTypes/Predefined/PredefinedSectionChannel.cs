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
    /// Predefined C-section is used for channel shapes having known properties 
    /// from catalog (such as AISC shapes)
    /// </summary>
    public class PredefinedSectionChannel : SectionPredefinedBase, ISectionChannel
    {

        public PredefinedSectionChannel(ISection section)
            : base(section)
        {

        }

        public PredefinedSectionChannel(
           double Heigh,
           double FlangeCentroidDistance,
           double FlangeClearDistance,
           double FlangeThickness,
           double FlangeWidth,
           double WebThickness,
           double FilletDistance,
           ISection section): base(section)
        {
            this.height                  =Heigh                   ;
            this.flangeCentroidDistance  =FlangeCentroidDistance  ;
            this.flangeClearDistance     =FlangeClearDistance     ;
            this.flangeThickness      =FlangeThickness    ;
            this.flangeWidth       =FlangeWidth       ;
            this.webThickness            =WebThickness            ;
            this.filletDistance       =FilletDistance       ;
        }

        double height;

        public double Height
        {
            get { return height; }
        }
        double flangeCentroidDistance;

        public double FlangeCentroidDistance
        {
            get { return flangeCentroidDistance; }
        }
        double flangeClearDistance;

        public double FlangeClearDistance
        {
            get { return flangeClearDistance; }
        }
        double flangeThickness;

        public double FlangeThickness
        {
            get { return flangeThickness; }
        }
        double flangeWidth;

        public double FlangeWidth
        {
            get { return flangeWidth; }
        }
        double webThickness;

        public double WebThickness
        {
            get { return webThickness; }
            set { webThickness = value; }
        }
        double filletDistance;

        public double FilletDistance
        {
            get { return filletDistance; }
            set { filletDistance = value; }
        }


        public override ISection Clone()
        {
            throw new NotImplementedException();
        }
    }
}
