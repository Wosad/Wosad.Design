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



namespace Wosad.Steel.AISC.AISC360_10.General.Compactness
{
    public abstract class StiffenedElementCompactness : CompactnessBase
    {
        private double width;

        public virtual double Width
        {
            get { return width; }
            set { width = value; }
        }

        private double thickness;

        public virtual double Thickness
        {
            get { return thickness; }
            set { thickness = value; }
        }

        public override double GetLambda()
        {
            return width / thickness;
        }


        public StiffenedElementCompactness(ISteelMaterial Material): base(Material)
        {

        }
        public StiffenedElementCompactness(ISteelMaterial Material, double ClearDepth, double Thickness)
            :base(Material)
        {
            this.Width = ClearDepth;
            this.Thickness = Thickness;
        }

    }
}