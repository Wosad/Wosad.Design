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
using Wosad.Concrete.ACI;
using Wosad.Common.Entities;
using Wosad.Concrete.ACI.Infrastructure.Entities;
using Wosad.Common.CalculationLogger.Interfaces;

namespace Wosad.Concrete.ACI318_14
{
    public partial class TensionLapSplice : Splice, ISplice

    {
        public TensionLapSplice(
            double rebar1Diameter, double rebar1DevelopmentLength,
            double rebar2Diameter, double rebar2DevelopmentLength,
            TensionLapSpliceClass SpliceClass,
            ICalcLog log):base(log)
        {
            this.spliceClass = SpliceClass;
            this.Rebar1Diameter = rebar1Diameter;
            this.Rebar2Diameter = rebar2Diameter;
            this.Rebar1DevelopmentLength = Rebar1DevelopmentLength;
            this.Rebar2DevelopmentLength = Rebar2DevelopmentLength;
        }

        private TensionLapSpliceClass spliceClass;

        public TensionLapSpliceClass SpliceClass
        {
            get { return spliceClass; }
            set { spliceClass = value; }
        }

        double length;
        public override double Length
        {
            get { return length; }
        }

        public double Rebar1Diameter { get; set; }
        public double Rebar2Diameter { get; set; }

        public double Rebar1DevelopmentLength { get; set; }
        public double Rebar2DevelopmentLength { get; set; }
    }
}
