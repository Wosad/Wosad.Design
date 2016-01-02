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
 
using Wosad.Common.CalculationLogger.Interfaces; 
using Wosad.Steel.AISC.Interfaces;
using Wosad.Steel.AISC.SteelEntities.Members;
using Wosad.Steel.AISC.SteelEntities.Sections;


namespace  Wosad.Steel.AISC.AISC360_10.HSS.TrussConnections
{
    public class HssTrussConnectionBranch : SteelMemberBase
    {
        public HssTrussConnectionBranch(SteelHssSection Section, double Angle, ICalcLog CalcLog)
            : base(Section as ISteelSection, CalcLog)
        {
            strengthValues = new List<HssConnectionBranchAvailableStrength>();
            this.angle = Angle;
        }

        private double angle;

        public double Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        private string id;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private List<HssConnectionBranchAvailableStrength> strengthValues;

        //public List<HssBranchAvailableStrength> StrengthValues
        //{
        //    get { return strengthValues; }
        //    set { strengthValues = value; }
        //}

        public List<HssConnectionBranchAvailableStrength> GetStrengthValues()
        {
            return strengthValues;
        }

        public void AddStrengthValue(HssConnectionBranchAvailableStrength value)
        {
            strengthValues.Add(value);
        }

        public void AddStrengthValue(double Capacity, string LoadCaseName)
        {
            HssConnectionBranchAvailableStrength str = new HssConnectionBranchAvailableStrength(Capacity, LoadCaseName);
            strengthValues.Add(str);
        }
        
        
    }
}
