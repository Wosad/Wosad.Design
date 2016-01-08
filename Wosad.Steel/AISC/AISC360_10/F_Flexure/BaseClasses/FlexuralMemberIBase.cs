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
using Wosad.Common.Section.Interfaces;
using Wosad.Steel.AISC.Exceptions;



namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public abstract partial class FlexuralMemberIBase : FlexuralMember
    {
        public FlexuralMemberIBase(ISteelSection section, bool IsRolledMember,
            double UnbracedLength, double EffectiveLengthFactor, ICalcLog CalcLog)
            : base(section, UnbracedLength, EffectiveLengthFactor,  CalcLog)
        {
            SectionI = null;
            ISectionI s = Section.Shape as ISectionI;
            this.isRolledMember = IsRolledMember;

            if (s == null)
            {
                throw new SectionWrongTypeException(typeof(ISectionI));
            }
            else
            {
                SectionI = s;
            }
        }

          ISectionI SectionI;
         

        private bool isRolledMember;

        public virtual bool IsRolledMember
        {
            get { return isRolledMember; }
            set { isRolledMember = value; }
        }
        

        protected virtual double GetHeight()
        {
            return SectionI.d;
        }

        protected virtual double GetBfTop()
        {
            return SectionI.b_fTop;
        }
        protected virtual double Get_tfTop()
        {
            return SectionI.t_fTop;
        }

        protected virtual double GetBfBottom()
        {
            return SectionI.b_fBot;
        }
        protected virtual double Get_tfBottom()
        {
            return SectionI.t_fBot;
        }

        //protected virtual double GetkBottom()
        //{
        //    return SectionI.k;
        //}
        //protected virtual double GetkTop()
        //{
        //    return SectionI.k;
        //}

        protected virtual double Gettw()
        {
            return SectionI.t_w;
        }


        protected virtual double GetFlangeCentroidDistanceho()
        {
            return SectionI.h_o;
        }




    }
}
