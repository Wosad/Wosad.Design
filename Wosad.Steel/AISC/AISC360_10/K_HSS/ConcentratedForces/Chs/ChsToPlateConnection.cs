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
//using Wosad.Analytics.Section;
 
 
using Wosad.Common.CalculationLogger.Interfaces; 
using Wosad.Steel.AISC.Interfaces;
using Wosad.Steel.AISC.SteelEntities.Sections;
using Wosad.Steel.AISC.Exceptions;


namespace  Wosad.Steel.AISC360_10.HSS.ConcentratedForces
{
    public abstract partial class ChsToPlateConnection :HssToPlateConnection //: IHssConcentratedForceConnection
    {

        private SteelPlateSection plate;

        public SteelPlateSection Plate
        {
            get { return plate; }
            set { plate = value; }
        }

        private SteelChsSection hss;

        public SteelChsSection Hss
        {
            get { return hss; }
            set { hss = value; }
        }


        public ChsToPlateConnection(SteelChsSection Hss, SteelPlateSection Plate,  ICalcLog CalcLog)
            :base(CalcLog)
        {
            this.hss = Hss;
            this.plate = Plate;
        }

        protected void GetTypicalParameters(ref double Fy, ref double t, ref double Bp, ref double D, ref double tp )
        {
            Fy = Hss.Material.YieldStress;
            t = Hss.Section.DesignWallThickness;
            Bp = Plate.Section.Height;
            tp = Plate.Section.Width;

            ISectionPipe pipe = Hss.Section as ISectionPipe;
            if (pipe != null)
            {
                throw new SectionWrongTypeException(typeof(ISectionPipe));
            }
            D = pipe.Diameter;

        }
    }
}
