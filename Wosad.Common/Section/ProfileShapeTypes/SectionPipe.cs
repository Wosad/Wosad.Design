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
    /// Generic pipe shape with geometric parameters provided in a constructor.
    /// </summary>
    public class SectionPipe: SectionBase,  ISectionHollow, ISectionPipe
    {
        public double Diameter { get; set; }
        public double WallThickness { get; set; }

        private double designWallThickness;

        public double DesignWallThickness
        {
            get { return designWallThickness; }
            set { designWallThickness = value; }
        }
        


        public SectionPipe(string Name, double Diameter, double WallThickness): this(Name, Diameter,WallThickness,WallThickness)
        {
        }

        public SectionPipe(string Name, double Diameter, double twallNominal, double DesignWallThickness)
            : base(Name)
        {
            this.Diameter = Diameter;
            this.WallThickness = twallNominal;
            this.designWallThickness = DesignWallThickness;
            CalculateProperties();

        }

        /// <summary>
        /// Overrides default fields for properties
        /// </summary>
        private void CalculateProperties()
        {
 
            double R = Diameter/2.0;
            double d = Diameter;
            double t = DesignWallThickness;

            double R_i = (Diameter-2*DesignWallThickness)/2;
            h = Diameter;
            b = Diameter;
            A =Math.PI * (R * R - R_i*R_i);
            I_x = ((Math.PI) / (4)) * (Math.Pow(R, 4) - Math.Pow(R_i, 4));
            I_y = I_x;
            Z_x = ((4) / (3))*(((Math.Pow(R, 4)-Math.Pow(R_i, 3)*R) / (Math.Pow(R, 4)-Math.Pow(R_i, 4)))) ;
            Z_y = Z_x;
            C_w = 0.0;
            J = ((Math.PI) / (32)) * (Math.Pow(d, 4) - Math.Pow((d - 2 * t), 4));
            x_Bar = R;
            x_p_Bar = R;
            y_Bar = R;
            y_p_Bar = R;
        }

    


        public ISection GetWeakAxisClone()
        {
            string cloneName = this.Name + "_clone";
            return new SectionPipe (cloneName, Diameter,WallThickness);
        }

        public override ISection Clone()
        {
            return new SectionPipe(Name, Diameter, WallThickness);
        }
    }
}
