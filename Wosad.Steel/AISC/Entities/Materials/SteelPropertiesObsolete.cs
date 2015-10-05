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



namespace Wosad.Steel.AISC.SteelEntities.Materials
{
    //public class SteelProperties1
    //{
    //    public static double GetYieldStress(SteelMaterialGradePlate PlateMaterial, double PlateThickness)
    //    {
    //        double Fy = 0.0;

    //        switch (PlateMaterial)
    //        {
    //            case SteelMaterialGradePlate.A36:
    //                if (PlateThickness<=8)
    //                {
    //                    return 36.0;
    //                }
    //                else
    //                {
    //                    return 32.0;
    //                }
    //            case SteelMaterialGradePlate.A529Grade50:
    //                return 50.0;
                    
    //            case SteelMaterialGradePlate.A529Grade55:
    //                return 55.0;
                    
    //            case SteelMaterialGradePlate.A572Grade42:
    //                return 42.0;
                    
    //            case SteelMaterialGradePlate.A572Grade50:
    //                return 50.0;
                    
    //            case SteelMaterialGradePlate.A572Grade55:
    //                return 55.0;
                    
    //            case SteelMaterialGradePlate.A572Grade60:
    //                return 60.0;
                    
    //            case SteelMaterialGradePlate.A572Grade65:
    //                return 65.0;
                    
    //            case SteelMaterialGradePlate.A242:
    //                if (PlateThickness<=5.0)
    //                {
                        
    //                    if (PlateThickness>1.5)
    //                        {
    //                        return 42.0;
    //                        }
    //                    else
    //                        {
    //                            if (PlateThickness<=0.75)
    //                        {
    //                             return 50.0;
    //                        }
    //                    else
    //                        {
    //                            return 46.0;
    //                        }
    //                        }
    //                }
    //                else
    //                {
    //                    throw new Exception("ASTM A242 material not available in thickness over 5 in");
    //                }
                    
    //            case SteelMaterialGradePlate.A588:
    //                if (PlateThickness<=8.0)
    //                    {
    //                        if (PlateThickness>5.0)
    //                        {
    //                            return 42.0;
    //                        }
    //                        else
    //                        {
    //                            if (PlateThickness<=4.0)
    //                            {
    //                                return 50.0;
    //                            }
    //                            else
    //                            {
    //                                return 46.0;
    //                            }
    //                        }
		 
    //                    }
    //                else
    //                    {
    //                        throw new Exception("ASTM A588 material not available in thickness over 8 in");
    //                    }
                    
    //            case SteelMaterialGradePlate.A514:
    //                if (PlateThickness<=6.0)
    //                {
    //                    if (PlateThickness<=2.5)
    //                    {
    //                        return 52.0;
    //                    }
    //                    else
    //                    {
    //                        return 51.0;
    //                    }
    //                }
    //                else
    //                {
    //                    throw new Exception("ASTM A514 material not available in thickness over 6 in");
    //                }
                    
    //            case SteelMaterialGradePlate.A852:

    //                if (PlateThickness<=4.0)
    //                {
    //                    return 53.0;
    //                }
    //                else
    //                {
    //                    throw new Exception("ASTM A852 material not available in thickness over 4 in");
    //                }
                    

    //        }

    //        return Fy;
    //    }

    //    public static double GetYieldStress(SteelMaterialGradeShapes sm, double p, ISection sec)
    //    {
    //        double Fy = 0.0;

    //        switch (sm)
    //        {
    //            case SteelMaterialGradeShapes.A36:
    //                if ( !(sec is ISectionTube) || !(sec is ISectionPipe))
    //                {
    //                    return 36.0;
    //                }
    //                else
    //                {
    //                    throw new Exception("Unsupported shapetype for ASTM A36 material");
    //                }
    //            case SteelMaterialGradeShapes.A53GradeB:
    //                if (sec is ISectionPipe)
    //                {
    //                   return 35.0; 
    //                }
    //                else
    //                {
    //                    throw new Exception("Unsupported shapetype for ASTM A53 material");
    //                }
    //            case SteelMaterialGradeShapes.A500GradeB:
    //                if (sec is ISectionPipe)
    //                {
    //                    return 42.0;
    //                }
    //                else
    //                {
    //                    if (sec is ISectionTube)
    //                    {
    //                        return 46.0;
    //                    }
    //                    else
    //                    {
    //                        throw new Exception("Unsupported shapetype for ASTM A500 material");
    //                    }
    //                }

    //            case SteelMaterialGradeShapes.A500GradeC:
    //                if (sec is ISectionPipe)
    //                {
    //                    return 46.0;
    //                }
    //                else
    //                {
    //                    if (sec is ISectionTube)
    //                    {
    //                        return 50.0;
    //                    }
    //                    else
    //                    {
    //                        throw new Exception("Unsupported shapetype for ASTM A500 material");
    //                    }
    //                }
    //            case SteelMaterialGradeShapes.A501:
    //                if (sec is ISectionTube || sec is ISectionPipe)
    //                {
    //                    return 36.0;
    //                }
    //                else
    //                {
    //                    throw new Exception("Unsupported shapetype for ASTM A501 material");
    //                }

    //            case SteelMaterialGradeShapes.A529Grade50:
    //                return 50.0;

    //            case SteelMaterialGradeShapes.A529Grade55:
    //                return 55.0;

    //            case SteelMaterialGradeShapes.A572Grade42:
    //                if (!(sec is ISectionTube) || !(sec is ISectionPipe))
    //                {
    //                    return 42.0;
    //                }
    //                else
    //                {
    //                    throw new Exception("Unsupported shapetype for ASTM A572 material");
    //                }

    //            case SteelMaterialGradeShapes.A572Grade50:
    //                if (!(sec is ISectionTube) || !(sec is ISectionPipe))
    //                {
    //                    return 50.0;
    //                }
    //                else
    //                {
    //                    throw new Exception("Unsupported shapetype for ASTM A572 material");
    //                }
    //            case SteelMaterialGradeShapes.A572Grade55:
    //                if (!(sec is ISectionTube) || !(sec is ISectionPipe))
    //                {
    //                    return 55.0;
    //                }
    //                else
    //                {
    //                    throw new Exception("Unsupported shapetype for ASTM A572 material");
    //                }
    //            case SteelMaterialGradeShapes.A572Grade60:
    //                if (!(sec is ISectionTube) || !(sec is ISectionPipe))
    //                {
    //                    return 60.0;
    //                }
    //                else
    //                {
    //                    throw new Exception("Unsupported shapetype for ASTM A572 material");
    //                }
    //            case SteelMaterialGradeShapes.A572Grade65:
    //                if (!(sec is ISectionTube) || !(sec is ISectionPipe))
    //                {
    //                    return 65.0;
    //                }
    //                else
    //                {
    //                    throw new Exception("Unsupported shapetype for ASTM A572 material");
    //                }
    //            case SteelMaterialGradeShapes.A618GradeIAndII:
    //                if (sec is ISectionTube|| sec is ISectionPipe)
    //                {
    //                    return 50.0;
    //                }
    //                else
    //                {
    //                    throw new Exception("Unsupported shapetype for ASTM A618 material");
    //                }
    //            case SteelMaterialGradeShapes.A618GradeIII:
    //                if (sec is ISectionTube || sec is ISectionPipe)
    //                {
    //                    return 50.0;
    //                }
    //                else
    //                {
    //                    throw new Exception("Unsupported shapetype for ASTM A618 material");
    //                }
    //            case SteelMaterialGradeShapes.A913Grade50:
    //                if (!(sec is ISectionTube) || !(sec is ISectionPipe))
    //                {
    //                    return 50.0;
    //                }
    //                else
    //                {
    //                    throw new Exception("Unsupported shapetype for ASTM A913 material");
    //                }
    //            case SteelMaterialGradeShapes.A913Grade60:
    //                if (!(sec is ISectionTube) || !(sec is ISectionPipe))
    //                {
    //                    return 60.0;
    //                }
    //                else
    //                {
    //                    throw new Exception("Unsupported shapetype for ASTM A913 material");
    //                }
    //            case SteelMaterialGradeShapes.A913Grade65:
    //                if (!(sec is ISectionTube) || !(sec is ISectionPipe))
    //                {
    //                    return 65.0;
    //                }
    //                else
    //                {
    //                    throw new Exception("Unsupported shapetype for ASTM A913 material");
    //                }
    //            case SteelMaterialGradeShapes.A913Grade70:
    //                if (!(sec is ISectionTube) || !(sec is ISectionPipe))
    //                {
    //                    return 70.0;
    //                }
    //                else
    //                {
    //                    throw new Exception("Unsupported shapetype for ASTM A913 material");
    //                }
    //            case SteelMaterialGradeShapes.A992Grade50:
    //                if (!(sec is ISectionTube) || !(sec is ISectionPipe))
    //                {
    //                    return 50.0;
    //                }
    //                else
    //                {
    //                    throw new Exception("Unsupported shapetype for ASTM A992 material");
    //                }
    //            case SteelMaterialGradeShapes.A992Grade65:
    //                if (!(sec is ISectionTube) || !(sec is ISectionPipe))
    //                {
    //                    return 65.0;
    //                }
    //                else
    //                {
    //                    throw new Exception("Unsupported shapetype for ASTM A992 material");
    //                }
    //            case SteelMaterialGradeShapes.A242Grade42:
    //                if (sec is ISectionI )
    //                {
    //                    return 42.0;
    //                }
    //                else
    //                {
    //                    throw new Exception("Unsupported shapetype for ASTM A242 material");
    //                }
    //            case SteelMaterialGradeShapes.A242Grade46:
    //                if (sec is ISectionI || sec is ISectionAngle)
    //                {
    //                    return 46.0;
    //                }
    //                else
    //                {
    //                    throw new Exception("Unsupported shapetype for ASTM A242 material");
    //                }
    //            case SteelMaterialGradeShapes.A242Grade50:
    //                if (!(sec is ISectionTube) || !(sec is ISectionPipe))
    //                {
    //                    return 50.0;
    //                }
    //                else
    //                {
    //                    throw new Exception("Unsupported shapetype for ASTM A242 material");
    //                }
    //            case SteelMaterialGradeShapes.A588:
    //                if (!(sec is ISectionTube) || !(sec is ISectionPipe))
    //                {
    //                    return 50.0;
    //                }
    //                else
    //                {
    //                    throw new Exception("Unsupported shapetype for ASTM A588 material");
    //                }
    //            case SteelMaterialGradeShapes.A847:
    //                if (sec is ISectionTube || sec is ISectionPipe)
    //                {
    //                    return 50.0;
    //                }
    //                else
    //                {
    //                    throw new Exception("Unsupported shapetype for ASTM A847 material");
    //                }
    //        }

    //        return Fy;
    //    }
    //}
}
