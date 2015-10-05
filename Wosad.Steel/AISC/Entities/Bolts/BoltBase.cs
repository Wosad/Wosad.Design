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
using System.Text; using Wosad.Common.Entities; using Wosad.Common.Section.Interfaces; using Wosad.Steel.AISC.Interfaces;
using Wosad.Common.CalculationLogger.Interfaces; 
using Wosad.Steel.AISC.Interfaces;
using Wosad.Common.Entities;
using Wosad.Steel.AISC.Code;
using Wosad.Steel.AISC.Interfaces;

namespace Wosad.Steel.AISC.SteelEntities.Bolts
{
    public abstract class BoltBase : AnalyticalElementForceBased, IDesignElement, IBolt
    {
        public BoltBase(double Diameter, BoltThreadType ThreadType, SteelDesignFormat DesignFormat,
            ICalcLog log)
             : base(log)
        {
            this.DesignFormat = DesignFormat;
            this.diameter = Diameter;
            this.threadType = ThreadType;
        }

        public abstract double NominalShearStress { get; }

        public abstract double NominalTensileStress { get; }

        private double diameter;

        public double Diameter
        {
            get { return diameter; }
            set { diameter = value; }
        }
        
        double area;
        public double Area
        {
            get 
            {
                area = Math.PI * Math.Pow(diameter, 2) / 4.0; 
                return area;
            }
        }

        private BoltThreadType threadType;

        public BoltThreadType ThreadType
        {
            get { return threadType; }
            set { threadType = value; }
        }


        public SteelDesignFormat DesignFormat { get; set; }

        public abstract double GetShearCapacity();


        public abstract double GetTensileCapacity();


       
    }
}
