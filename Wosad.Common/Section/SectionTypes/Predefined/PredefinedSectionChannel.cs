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
           double d,
           double FlangeCentroidDistance,
           double FlangeClearDistance,
           double FlangeThickness,
           double FlangeWidth,
           double WebThickness,
           double FilletDistance,
           ISection section): base(section)
        {
            this._h                  =d                   ;
            this.flangeCentroidDistance  =FlangeCentroidDistance  ;
            this.flangeClearDistance     =FlangeClearDistance     ;
            this.flangeThickness      =FlangeThickness    ;
            this._b_f       =FlangeWidth       ;
            this._t_w            =WebThickness            ;
            this._k       =FilletDistance       ;
        }

        double _h;

        public double d
        {
            get { return _h; }
        }
        double flangeCentroidDistance;

        public double h_o
        {
            get { return flangeCentroidDistance; }
        }
        double flangeClearDistance;

        public double FlangeClearDistance
        {
            get { return flangeClearDistance; }
        }
        double flangeThickness;

        public double t_f
        {
            get { return flangeThickness; }
        }
        double _b_f;

        public double b_f
        {
            get { return _b_f; }
        }
        double _t_w;

        public double t_w
        {
            get { return _t_w; }
            set { _t_w = value; }
        }
        double _k;

        public double k
        {
            get { return _k; }
            set { _k = value; }
        }


        public override ISection Clone()
        {
            throw new NotImplementedException();
        }
    }
}
