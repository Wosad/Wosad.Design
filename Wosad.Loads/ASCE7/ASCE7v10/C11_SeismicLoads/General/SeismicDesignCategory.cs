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
using Wosad.Common.CalculationLogger; 
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Entities;
using Wosad.Common.CalculationLogger; 
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Loads.ASCE7.Entities;
using data = Wosad.Loads.ASCE7.Entities;

namespace Wosad.Loads.ASCE.ASCE7_10.SeismicLoads
{
    public partial class General : AnalyticalElement
    {
        public data.SeismicDesignCategory GetSeismicDesignCategory(BuildingRiskCategory RiskCategory,double SDS, double SD1, double S1 )
        {
            data.SeismicDesignCategory SDC, CategoryS1, CategorySDS, CategorySD1;
            CategorySDS = Get02secSeismicDesignCategory(SDS, RiskCategory);
            CategorySD1 = Get1secSeismicDesignCategory(SD1, RiskCategory);


            if (S1 >= 0.75)
            {
                if (RiskCategory == BuildingRiskCategory.IV)
                {
                    CategoryS1 = data.SeismicDesignCategory.F;
                }
                else
                {
                    CategoryS1 = data.SeismicDesignCategory.E;
                }

                //High-seismic design category
                #region S1 Governs High-Seismic
                ICalcLogEntry S1Entry = new CalcLogEntry();
                S1Entry.ValueName = "CategoryS1";
                S1Entry.AddDependencyValue("S1", Math.Round(S1, 4));
                S1Entry.AddDependencyValue("RiskCategory", RiskCategory.ToString());
                S1Entry.Reference = "Seismic design rategory";
                S1Entry.DescriptionReference = "/Templates/Loads/ASCE7_10/Seismic/SeismicSDC_S1Governs.docx";
                S1Entry.FormulaID = "Table 11.6-1"; //reference to formula from code
                S1Entry.VariableValue = CategoryS1.ToString(); 
                #endregion

                SDC = CategoryS1;
                this.AddToLog(S1Entry);
            }
            else
            {
                if ((int)CategorySDS >= (int)CategorySD1)
                {
                    SDC = CategorySDS;
                    
                    #region SDSGoverns
                    ICalcLogEntry SDSGovernsEntry = new CalcLogEntry();
                    SDSGovernsEntry.ValueName = "CategorySDS";
                    //SDSGovernsEntry.AddDependencyValue("CategorySDS", CategorySDS.ToString());
                    //SDSGovernsEntry.AddDependencyValue("CategorySD1", CategorySD1.ToString());
                    SDSGovernsEntry.Reference = "Seismic Design Category";
                    SDSGovernsEntry.DescriptionReference = "/Templates/Loads/ASCE7_10/Seismic/SeismicSDC_SDSGoverns.docx";
                    SDSGovernsEntry.FormulaID = null; //reference to formula from code
                    SDSGovernsEntry.VariableValue = SDC.ToString();
                    #endregion
                    this.AddToLog(SDSGovernsEntry);
                    

                }
                else
                {
                    SDC = CategorySD1;
                    #region SD1Governs
                    ICalcLogEntry SD1GovernsEntry = new CalcLogEntry();
                    SD1GovernsEntry.ValueName = "CategorySD1";
                    //SD1GovernsEntry.AddDependencyValue("CategorySD1", CategorySD1.ToString());
                    SD1GovernsEntry.Reference = "Seismic Design Category";
                    SD1GovernsEntry.DescriptionReference = "/Templates/Loads/ASCE7_10/Seismic/SeismicSDC_SD1Governs.docx";
                    SD1GovernsEntry.FormulaID = null; //reference to formula from code
                    SD1GovernsEntry.VariableValue = SDC.ToString();
                    #endregion
                    this.AddToLog(SD1GovernsEntry);
                }

            }
            return SDC;
        }
    }
}
