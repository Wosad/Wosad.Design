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
    /// Predefined circular HSS (pipe) is used for circular hollow sections (CHS) having known properties 
    /// from catalog (such as AISC shapes)
    /// </summary>
    public class PredefinedSectionCHS : SectionPredefinedBase, ISectionPipe
    {
        public PredefinedSectionCHS(ISection section)
            : base(section)
        {

        }
        public PredefinedSectionCHS(
        double Diameter,
        double WallThickness,ISection section): base(section)
        {
            this.diameter = Diameter;
            this.wallThickness=WallThickness;
            this.designWallThickness = t_des;
        }
        double diameter;
        public double D
        {
            get { return diameter; }
        }

        double wallThickness;
        public double t
        {
            get { return wallThickness; }
        }

        double designWallThickness;
        public double t_des
        {
            get { return designWallThickness; }
        }

        public ISection GetWeakAxisClone()
        {
            throw new NotImplementedException();
        }

        public override ISection Clone()
        {
            throw new NotImplementedException();
        }
    }
}
