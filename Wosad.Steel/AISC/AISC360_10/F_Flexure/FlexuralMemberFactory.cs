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
using Wosad.Common.Entities;
using Wosad.Common.Section.Interfaces;
using Wosad.Steel.AISC.AISC360_10.General.Compactness;
using Wosad.Steel.AISC.Interfaces;
using Wosad.Steel.AISC.SteelEntities.Sections;
using Wosad.Common.Section.Predefined;
 
 

namespace Wosad.Steel.AISC.AISC360_10.Flexure
{

    public class FlexuralMemberFactory
    {
  

        public ISteelBeamFlexure GetBeam(ShapeTypeSteel Shape, MomentAxis MomentAxis, 
            CompactnessClassFlexure UnstiffenedElementCompactness, CompactnessClassFlexure StiffenedElementCompactness,
            ISection Section, ISteelMaterial Material, ICalcLog Log)
        {
            
            ISteelBeamFlexure beam = null;
            switch (Shape)
            {
                case ShapeTypeSteel.IShapeRolled:
                    PredefinedSectionI ISec;
                    ISec = Section as PredefinedSectionI;
                    if (ISec != null)
                    {
                        beam = CreateIBeam(UnstiffenedElementCompactness, StiffenedElementCompactness, ISec, Material, Log);
                    }
                   
                    break;
                case ShapeTypeSteel.IShapeBuiltUp:
                    if (Section is ISectionI)
                    {
                        ISectionI section = Section as ISectionI;
                        beam = CreateIBeam(UnstiffenedElementCompactness, StiffenedElementCompactness, section, Material, Log);
                    }
                    break;
                case ShapeTypeSteel.Channel:
                    break;
                case ShapeTypeSteel.Angle:
                    break;
                case ShapeTypeSteel.TeeRolled:
                    break;
                case ShapeTypeSteel.TeeBuiltUp:
                    break;
                case ShapeTypeSteel.DoubleAngle:
                    break;
                case ShapeTypeSteel.CircularHSS:
                    break;
                case ShapeTypeSteel.RectangularHSS:
                    ISectionTube RhsSec;
                    RhsSec = Section as ISectionTube;
                    if (RhsSec != null)
                    {
                        beam = CreateRhsBeam(UnstiffenedElementCompactness, StiffenedElementCompactness, RhsSec, Material, Log);
                    }
                    break;
                case ShapeTypeSteel.Box:
                    break;
                case ShapeTypeSteel.Rectangular:
                    break;
                case ShapeTypeSteel.Circular:
                    break;
                case ShapeTypeSteel.IShapeAsym:
                    break;
                default:
                    break;
            }
            return beam;
        }

        private ISteelBeamFlexure CreateRhsBeam(CompactnessClassFlexure FlangeCompactness, CompactnessClassFlexure WebCompactness,
            ISectionTube RhsSec, ISteelMaterial Material, ICalcLog Log)
        {
            SteelRhsSection steelSection = new SteelRhsSection(RhsSec, Material);
            ISteelBeamFlexure beam = null;
            beam = new BeamRhs(steelSection, Log);
            return beam;
        }

        private ISteelBeamFlexure CreateIBeam(CompactnessClassFlexure FlangeCompactness,
            CompactnessClassFlexure WebCompactness, ISectionI Section, ISteelMaterial Material, 
            ICalcLog Log)
        {
            SteelSectionI steelSection = new SteelSectionI(Section, Material);
            ISteelBeamFlexure beam = null;
            if (FlangeCompactness== CompactnessClassFlexure.Compact && WebCompactness == CompactnessClassFlexure.Compact)
            {
                //F2
                beam = new BeamIDoublySymmetricCompact(steelSection, Log);
            }
            else if (WebCompactness == CompactnessClassFlexure.Compact && FlangeCompactness!= CompactnessClassFlexure.Compact)
            {
                //F3
                throw new NotImplementedException();
            }

            return beam;
        }

    }
}
