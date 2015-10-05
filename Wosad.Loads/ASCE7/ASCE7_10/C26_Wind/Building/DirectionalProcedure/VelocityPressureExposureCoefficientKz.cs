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
using Wosad.Common.CalculationLogger; 
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.CalculationLogger; 
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Loads.ASCE7.Entities;

namespace Wosad.Loads.ASCE.ASCE7_10.WindLoads.Building.DirectionalProcedure
{
    public partial class BuildingDirectionalProcedureElement : WindBuilding
    {
        public  double GetVelocityPressureExposureCoefficientKz(double z, double zg, double alpha, WindVelocityLocation location)
        {
            double Kz = 0;

            
            #region Kz
            ICalcLogEntry KzEntry = new CalcLogEntry();
            KzEntry.ValueName = "Kz";
            KzEntry.AddDependencyValue("z", Math.Round(z, 3));
            KzEntry.AddDependencyValue("zg", Math.Round(zg, 3));
            KzEntry.AddDependencyValue("alpha", Math.Round(alpha, 3));
            if (location == WindVelocityLocation.Wall)
            {
                KzEntry.AddDependencyValue("locType", "z");
            }
            else
            {
                KzEntry.AddDependencyValue("locType", "h");
            }
            
            KzEntry.Reference = "";
            KzEntry.FormulaID = null; //reference to formula from code

            #endregion
            

            if (z >= 15.0)
            {
                if (z>zg)
                {
                    Kz = 2.01;
                    KzEntry.DescriptionReference = "/Templates/Loads/ASCE7_10/Wind/WindVelocityPressureExposureCoefficientOver_zg.docx";
                
                }
                else
                {
                    Kz = 2.01 * Math.Pow(z / zg, 2.0 / alpha);
                    KzEntry.DescriptionReference = "/Templates/Loads/ASCE7_10/Wind/WindVelocityPressureExposureCoefficientOver15.docx";
                
                }
                
            }
            else
            {
                Kz = 2.01 * Math.Pow(15.0 / zg, 2.0 / alpha);
                KzEntry.DescriptionReference = "/Templates/Loads/ASCE7_10/Wind/WindVelocityPressureExposureCoefficientLess15.docx";
            
            }

            KzEntry.VariableValue = Math.Round(Kz, 3).ToString();
            this.AddToLog(KzEntry);
            return Kz;
        }

        public double GetVelocityPressureExposureCoefficientKz(double z, WindExposureCategory windExposureCategory, WindVelocityLocation location)
        {
            double zg = GetTerrainExposureConstant(TerrainExposureConstant.zg, windExposureCategory);
            double alpha = GetTerrainExposureConstant(TerrainExposureConstant.alpha, windExposureCategory);
            double Kz = GetVelocityPressureExposureCoefficientKz(z, zg, alpha, location);
            return Kz;
        }
    }
}
