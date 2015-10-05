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
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Steel.AISC.Code;
using Wosad.Steel.AISC.Interfaces;
using Wosad.Steel.AISC.SteelEntities.Bolts;

namespace Wosad.Steel.AISC.AISC360_10.Connections.Bolted
{
    public abstract partial class BoltSlipCritical : Bolt, IBoltSlipCritical
    {
        public BoltSlipCritical(double Diameter, BoltThreadType ThreadType,
            SteelDesignFormat DesignFormat, FayingSurface FayingSurface, BoltHoleType HoleType,
            BoltFillers Fillers, int NumberOfSlipPlanes, ICalcLog log, double PretensionMultiplier=1.13)
            : base(Diameter, ThreadType, DesignFormat, log)
        {
            this.fayingSurface = FayingSurface;
            this.holeType = HoleType;
            this.minimumPretension = Math.Round(0.7* Area * NominalTensileStress,0);
            this.fillers = Fillers;
            this.pretensionMultiplier = PretensionMultiplier;
            this.numberOfSlipPlanes = NumberOfSlipPlanes;
        }

        private FayingSurface fayingSurface;

        public FayingSurface FayingSurface
        {
            get { return fayingSurface; }
        }

        private BoltHoleType holeType;

        public BoltHoleType HoleType
        {
            get { return holeType; }
        }

        private double minimumPretension;

        public double MinimumPretension
        {
            get { return minimumPretension; }
        }

        private BoltFillers fillers;

        public BoltFillers Fillers
        {
            get { return fillers; }
            set { Fillers = value; }
        }

        private double pretensionMultiplier;

        public double PretensionMultiplier
        {
            get { return pretensionMultiplier; }
            set { pretensionMultiplier = value; }
        }

        private int numberOfSlipPlanes;

        public int NumberOfSlipPlanes
        {
            get { return numberOfSlipPlanes; }
            set { numberOfSlipPlanes = value; }
        }
        
        
    }
}
