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
    /// Predefined  rectangular HSS section is used for rectangular or square hollow sections (RHS) having known properties 
    /// from catalog (such as AISC shapes)
    /// </summary>
    public class PredefinedSectionRHS : SectionPredefinedBase, ISectionTube
    {
        public PredefinedSectionRHS(ISection section)
            : base(section)
        {

        }
        public PredefinedSectionRHS(double Width, double Height, double CornerRadiusOutside, ISection section)
            : base(section)
        {
            this.width=Width;
            this.height=Height;
            this.cornerRadiusOutside=CornerRadiusOutside;
        }

        double width;

        public double Width
        {
            get { return width; }
        }
        double height;

        public double Height
        {
            get { return height; }
        }


        public ISection GetWeakAxisClone()
        {
            throw new NotImplementedException();
        }

        public override ISection Clone()
        {
            throw new NotImplementedException();
        }



 


        private double designWallThickness;

        public double DesignWallThickness
        {
            get { return designWallThickness; }
        }

        double cornerRadiusOutside;

        double CornerRadiusOutside
        {
            get
            {
                return cornerRadiusOutside;
            }
            set
            {
                cornerRadiusOutside = value;
            }
        }


        double ISectionTube.CornerRadiusOutside
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
