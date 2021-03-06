﻿#region Copyright
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
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Exceptions;
using Wosad.Common.Section.Interfaces;
using Wosad.Steel.AISC.AISC360v10.B_General;
using Wosad.Steel.AISC.AISC360v10.General.Compactness;
using Wosad.Steel.AISC.Interfaces;

namespace Wosad.Steel.AISC.AISC360v10.Flexure
{
    public class SinglySymmetricIBeam
    {
        public SinglySymmetricIBeam(ISteelSection section, bool IsRolledMember, FlexuralCompressionFiberPosition compressionFiberPosition, ICalcLog CalcLog)
        {
            this.section = section;
            this.IsRolledMember = IsRolledMember;
            this.CalcLog = CalcLog;
            this.compressionFiberPosition = compressionFiberPosition;
        }



        FlexuralCompressionFiberPosition compressionFiberPosition;
        ISteelSection section;
        bool IsRolledMember;
        ICalcLog CalcLog;


        public ISteelBeamFlexure GetBeamCase()
        {
            ISteelBeamFlexure beam = null;
            IShapeCompactness compactness = new ShapeCompactness.IShapeMember(section, IsRolledMember, compressionFiberPosition);

            CompactnessClassFlexure flangeCompactness = compactness.GetFlangeCompactnessFlexure();
            CompactnessClassFlexure webCompactness = compactness.GetWebCompactnessFlexure();



                if (webCompactness == CompactnessClassFlexure.Compact || webCompactness == CompactnessClassFlexure.Noncompact)
                {
                    if (flangeCompactness == CompactnessClassFlexure.Compact)
                    {
                        return new BeamINoncompactWebCompactFlange(section, IsRolledMember, CalcLog);
                    }
                    else
                    {
                        return new BeamINoncompactWeb(section, IsRolledMember, CalcLog);
                    }
                }

                else 
                {
                    if (flangeCompactness == CompactnessClassFlexure.Compact)
                    {
                        return new BeamISlenderWebCompactFlange(section, IsRolledMember, CalcLog);
                    }
                    else
                    {
                        return new BeamISlenderWeb(section, IsRolledMember, CalcLog);
                    }

                }

            
            return beam;

        }



        private IShapeCompactness compactness;

        public IShapeCompactness Compactness
        {
            get
            {
                if (compactness == null)
                {
                    compactness = GetCompactness();
                }
                return compactness;
            }
        }

        private IShapeCompactness GetCompactness()
        {
            ISectionI Isec = section as ISectionI;
            if (Isec != null)
            {
                compactness = new ShapeCompactness.IShapeMember(section, IsRolledMember, compressionFiberPosition);
            }
            else
            {
                throw new ShapeTypeNotSupportedException(" flexural calculation of I-beam");
            }
            return compactness;
        }
	

    }
}
