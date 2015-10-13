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
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Entities;

namespace Wosad.Analysis
{
    public abstract class Beam : AnalyticalElement, IAnalysisBeam
    {

        double Mx, Vx;
        ForceDataPoint Mmax, Mmin, Vmax;
        bool isCalculated;

        public Beam(double Length, List<LoadBeam> Loads, ICalcLog CalcLog, IBeamCaseFactory BeamCaseFactory)
            : base(CalcLog)
        {
            this.length = Length;
            isCalculated = false;
            this.BeamCaseFactory = BeamCaseFactory;
            this.Loads = Loads;
        }

        private double length;

        public double Length
        {
            get { return length; }
            set { length = value; }
        }


        
        

        public IBeamCaseFactory BeamCaseFactory { get; set; }


        private List<LoadBeam> loads;

        public List<LoadBeam> Loads
        {
            get {
                if (loads==null)
                {
                    loads = new List<LoadBeam>();
                }
                return loads; }
            set { loads = value; }
        }
        
 
        private List<ISingleLoadCaseBeam> loadCases;

        public List<ISingleLoadCaseBeam> LoadCases
        {
            get 
            {
                if (loadCases == null)
                {
                    loadCases = new List<ISingleLoadCaseBeam>();
                }
                return loadCases; 
            }
            set { loadCases = value; }
        }
        

        //public abstract double GetMoment( double X);
        //public abstract double GetShear( double X);
        //public abstract ForceDataPoint GetMomentMaximum();
        //public abstract ForceDataPoint GetMomentMinimum();
        //public abstract ForceDataPoint GetShearMaximumValue();

        public virtual void EvaluateX(double X)
        {
            if (X < 0 || X > this.Length)
            {
                throw new StationOutOfBoundsException(this.Length, X);
            }
        }

        public void CalculateValues(double X)
        {
            //BeamSimpleFactory caseFactory = new BeamSimpleFactory();

            foreach (var load in Loads)
            {
                ISingleLoadCaseBeam bmCase = BeamCaseFactory.GetCase(load, this);
                LoadCases.Add(bmCase);
            }

            if (LoadCases.Count > 0)
            {
                if (LoadCases.Count == 1)
                {
                    ISingleLoadCaseBeam bm = LoadCases[0];
                    //Switch on or off reporting mode to avoid unnecessary data
                    if (ReportX ==false)
                    {
                        LogModeActive = false;
                    }
                    else
                    {
                        LogModeActive = true;
                    }
                    Mx = bm.Moment(X);
                    Vx = bm.Shear(X);
                    //Switch on or off reporting mode to avoid unnecessary data
                    if (ReportMax == false)
                    {
                        LogModeActive = false;
                    }
                    else
                    {
                        LogModeActive = true;
                    }
                    Mmax = bm.MomentMax();
                    Mmin = bm.MomentMin();
                    Vmax = bm.ShearMax();

                    LogModeActive = true;
                }
                else
                {
                    //do iterations here
                    throw new NotImplementedException();

                }
            }
            isCalculated = true;
        }

        protected ForceDataPoint FindForceValueMax(List<double> SpecialPoints,FindValueAtXDelegate evaluateForce)
        {
            //divide beam into equal segments
            //add special points 
            //create sorted list
            //step through points and evaluate maximum
            throw new NotImplementedException();
        }

        private BeamTemplatePathLocator resLoc;

        public BeamTemplatePathLocator ResourceLocator
        {
            get 
            {
                if (resLoc==null)
                {
                    resLoc = new BeamTemplatePathLocator();
                }
                return resLoc; 
            }

        }


        public virtual double GetMoment(double X)
        {
            if (isCalculated == false)
            {
                CalculateValues(X);
            }
            return Mx;
        }

        public virtual double GetShear(double X)
        {
            if (isCalculated == false)
            {
                CalculateValues(X);
            }
            return Vx;
        }

        public virtual ForceDataPoint GetMomentMaximum()
        {
            if (isCalculated == false)
            {
                CalculateValues(Length / 2.0); // if no other X is provided
            }
            return Mmax;
        }

        public virtual ForceDataPoint GetMomentMinimum()
        {
            if (isCalculated == false)
            {
                CalculateValues(0.0); // if no other X is provided
            }
            return Mmin;
        }

        public virtual ForceDataPoint GetShearMaximumValue()
        {
            if (isCalculated == false)
            {
                CalculateValues(0.0); // if no other X is provided
            }
            return Vmax;
        }

        public bool ReportX { get; set; }
        public bool ReportMax { get; set; }


        protected double E;

        public double ModulusOfElasticity
        {
            get { return E; }
            set { E = value; }
        }


        protected double I;

        public double MomentOfInertia
        {
            get { return I; }
            set { I = value; }
        }
        
    }
}
