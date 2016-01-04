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
    /// Generic solid round shape with geometric parameters provided in a constructor.
    /// </summary>
    public  class SectionRound: SectionBase, ISectionRound
    {
        private double diameter;

        public double Diameter
        {
            get { return diameter; }

        }
        


        public SectionRound(string Name, double Diameter): base(Name)
        {
            this.diameter = Diameter;
            CalculateProperties();
        }


        /// <summary>
        /// Overrides default fields for properties
        /// </summary>
        private void CalculateProperties()
        {

            double R = Diameter / 2.0;
            double D = Diameter;
            //height and width in the base class 
            _H = Diameter;
            _B = Diameter;
            _A = ((Math.PI * Math.Pow(D, 2)) / (4));
            _I_x = ((Math.PI * Math.Pow(D, 4)) / (64));
            _I_y = _I_x;
            _Z_x = ((Math.Pow(D, 3)) / (6));
            _Z_y = _Z_x;
            _x_Bar = R;
            _x_pBar = R;
            _y_Bar = R;
            _y_pBar = R;
            _C_w = 0; //to be confirmed
            double I_p = ((Math.PI * Math.Pow(D, 4)) / (32));
            _J = I_p;

        }

        public ISection GetWeakAxisClone()
        {
            string cloneName = this.Name + "_clone";
            return new SectionRound(cloneName, Diameter);
        }

        private double _A;

        public override double A
        {
            get
            {
                _A = GetArea();
                return _A;
            }

        }

        double GetArea()
        {
            double pi = Math.PI;
            double A = pi / 4.0 * (Math.Pow(Diameter, 2));
            return A;
        }

        public override ISection Clone()
        {
            return new SectionRound(Name, Diameter);
        }
    }
}
