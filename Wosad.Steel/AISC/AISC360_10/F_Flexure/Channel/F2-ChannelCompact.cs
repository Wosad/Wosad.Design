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
 
 
using Wosad.Steel.AISC.AISC360_10.General.Compactness;
using Wosad.Common.CalculationLogger.Interfaces; 
using Wosad.Steel.AISC.Interfaces;
using Wosad.Common.CalculationLogger;
using Wosad.Steel.AISC.Exceptions;
using Wosad.Steel.AISC.Code;


namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public abstract partial class FlexuralMemberChannelBase : BeamIDoublySymmetricCompact
    {
        public FlexuralMemberChannelBase(ISteelSection section, bool IsRolledMember,
            double UnbracedLength, double EffectiveLengthFactor, SteelDesignFormat DesignFormat, ICalcLog CalcLog)
            : base( section, IsRolledMember, UnbracedLength, EffectiveLengthFactor, DesignFormat, CalcLog)
        {
            SectionChannel = null;
            ISectionChannel s = Section as ISectionChannel;
            this.isRolledMember = IsRolledMember;

            if (s == null)
            {
                throw new SectionWrongTypeException(typeof(ISectionI));
            }
            else
            {
                SectionChannel = s;
            }
        }


        ISectionChannel SectionChannel;


        private bool isRolledMember;

        public override bool IsRolledMember
        {
            get { return isRolledMember; }
            set { isRolledMember = value; }
        }


        protected override double GetHeight()
        {
            return SectionChannel.Height;
        }

        protected override double GetBfTop()
        {
            return SectionChannel.FlangeWidth;
        }
        protected override double Get_tfTop()
        {
            return SectionChannel.FlangeThickness;
        }

        protected override double GetBfBottom()
        {
            return SectionChannel.FlangeWidth;
        }
        protected override double Get_tfBottom()
        {
            return SectionChannel.FlangeThickness;
        }

        //protected override double GetkBottom()
        //{
        //    return SectionChannel.FilletDistance;
        //}
        //protected override double GetkTop()
        //{
        //    return SectionChannel.FilletDistance;
        //}

        protected override double Gettw()
        {
            return SectionChannel.WebThickness;
        }


        protected override double GetFlangeCentroidDistanceho()
        {
            return SectionChannel.FlangeCentroidDistance;
        }



       public override double Get_c()
       {
           
           double Iy = SectionChannel.MomentOfInertiaY;
           double ho = SectionChannel.FlangeCentroidDistance;
           double Cw = SectionChannel.WarpingConstant;
           double c=ho/2.0*Math.Sqrt(Iy/Cw);

           
           #region c
           ICalcLogEntry cEntry = new CalcLogEntry();
           cEntry.ValueName = "c";
           cEntry.AddDependencyValue("Iy", Math.Round(Iy, 3));
           cEntry.AddDependencyValue("ho", Math.Round(ho, 3));
           cEntry.AddDependencyValue("Cw", Math.Round(Cw, 3));
           cEntry.Reference = "";
           cEntry.DescriptionReference = "/Templates/Steel/AISC360_10/Flexure/F2_c_Channel.docx";
           cEntry.FormulaID = null; //reference to formula from code
           cEntry.VariableValue = Math.Round(c, 3).ToString();
           #endregion
           this.AddToLog(cEntry);

           return c;
       }


    }
}
