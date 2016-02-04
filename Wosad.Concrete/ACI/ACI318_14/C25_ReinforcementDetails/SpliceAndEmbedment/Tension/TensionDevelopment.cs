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
using Wosad.Concrete.ACI.Infrastructure.Entities.Rebar;
using Wosad.Common.CalculationLogger.Interfaces;

namespace Wosad.Concrete.ACI318_14
{
    public partial class DevelopmentTension:Development
    {

        public DevelopmentTension(
            IConcreteMaterial Concrete, 
            Rebar Rebar, 
            double clearSpacing, 
            double ClearCover,
            bool IsTopRebar, 
            double ExcessFlexureReinforcementRatio, 
            bool CheckMinimumLength,  
            ICalcLog log)
            : base (Concrete, Rebar, ExcessFlexureReinforcementRatio,log)
        {
            this.isTopRebar = IsTopRebar;
            this.ClearCover = ClearCover;
            this.clearSpacing = clearSpacing;
            this.CheckMinimumLength = CheckMinimumLength;
            db = Rebar.Diameter;
        }

        


        //        //Tension case (in flexure)
        //    public DevelopmentTension(
        //        IConcreteMaterial Concrete, 
        //        Rebar Rebar, 
        //        bool IsTopRebar, 
        //        double clearSpacing, 
        //        double ClearCover,
        //        bool MinimumShearReinforcementProvided, 
        //        bool CheckMinimumLength, 
        //        ICalcLog log,
        //        double ExcessFlexureReinforcementRatio = 1.0)
        //    :this(Concrete,Rebar,clearSpacing,ClearCover,IsTopRebar,ExcessFlexureReinforcementRatio,CheckMinimumLength, log)
        //{
            
        //    this.minimumShearReinforcementProvided = MinimumShearReinforcementProvided;
        //    useRefinedFormula = false;
        //}



        ////Tension case (in flexure) for refined formula
        //public DevelopmentTension(
        //    IConcreteMaterial Concrete, 
        //    Rebar Rebar,
        //    bool IsTopRebar,
        //    double clearSpacing, 
        //    double ClearCover, 
        //    double TransverseRebarArea, 
        //    double TransverseRebarSpacing,
        //    int NumberOfBarsBeingDeveloped, 
        //    bool CheckMinimumLength,
        //    ICalcLog log, double 
        //    ExcessFlexureReinforcementRatio = 1.0)
        //    : this(Concrete, Rebar, clearSpacing, ClearCover, IsTopRebar,ExcessFlexureReinforcementRatio,CheckMinimumLength, log)
        //{

        //    this.transverseRebarArea = TransverseRebarArea;
        //    this.transverseRebarSpacing = TransverseRebarSpacing;
        //    this.numberOfBarsAlongSplittingPlane = NumberOfBarsBeingDeveloped;
        //    useRefinedFormula = true;
        //}

        double db;

        //private double transverseRebarArea;

        //public double TransverseRebarArea
        //{
        //    get { return transverseRebarArea; }
        //    set { transverseRebarArea = value; }
        //}

        //private double transverseRebarSpacing;

        //public double TransverseRebarSpacing
        //{
        //    get { return transverseRebarSpacing; }
        //    set { transverseRebarSpacing = value; }
        //}

        //private bool minimumShearReinforcementProvided;

        //public bool MinimumShearReinforcementProvided
        //{
        //    get { return minimumShearReinforcementProvided; }
        //    set { minimumShearReinforcementProvided = value; }
        //}

        private double clearSpacing;

        public double ClearSpacing
        {
            get { return clearSpacing; }
            set { clearSpacing = value; }
        }

        private double clearCover;

        public double ClearCover
        {
            get { return clearCover; }
            set { clearCover = value; }
        }



        private bool isTopRebar;

        public bool IsTopRebar
        {
            get { return isTopRebar; }
            set { isTopRebar = value; }
        }

        private bool meetsSpacingCritera;

        public bool MeetsSpacingCritera
        {
            get { return meetsSpacingCritera; }
            set { meetsSpacingCritera = value; }
        }

        //bool useRefinedFormula;
        ////distinguishes between 12.2.2 (simplified) and 
        ////12.2.3 (refined) formula

        //public double ksi_t { get; set; }
        //public double ksi_e { get; set; }
        //public double ksi_s { get; set; }

        //public double cb { get; set; }
        //public double Ktr { get; set; }

        //public double ConfinementTerm { get; set; }

        public bool CheckMinimumLength { get; set; }

    }
}
