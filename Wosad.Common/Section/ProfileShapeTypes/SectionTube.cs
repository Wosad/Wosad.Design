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


namespace Wosad.Common.Section.SectionTypes
{
    /// <summary>
    /// Generic rectangular tube shape with geometric parameters provided in a constructor.
    /// </summary>
    public class SectionTube : SectionBox , ISectionTube
    {


        public SectionTube(string Name, double Depth, double Width, double Thickness, double DesignWallThickness,
            double CornerRadiusOutside = -1)
            : base(Name, Depth, Width, DesignWallThickness, DesignWallThickness)
        {
            this.Height = Depth;
            this.Width = Width;
            this.WallThickness = Thickness;
            this.t = DesignWallThickness;
            if (CornerRadiusOutside == -1)
            {
                this.r_c = 1.5 * Thickness;
            }
            else
            {
                this.r_c = CornerRadiusOutside;
            }

        }
        #region Properties specific to HSS tube


        private double r_c;

        public double CornerRadiusOutside
        {
            get { return r_c; }
            set { r_c = value; }
        }

        //private const double CornerRadiusOutside = 1.5;

        private double h;

        public double Height
        {
            get { return h; }
            set { h = value; }
        }

        private double b;

        public double Width
        {
            get { return b; }
            set { b = value; }
        }

        private double t_nom;

        public double WallThickness
        {
            get { return t_nom; }
            set { t_nom = value; }
        }


        private double t;

        public double DesignWallThickness
        {
            get { return t; }
            set { t = value; }
        }
        
        #endregion

    }
}
