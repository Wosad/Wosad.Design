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

namespace Wosad.Loads.ASCE.ASCE7_10.SeismicLoads
{
    public partial class SeismicLateralForceResistingStructure : AnalyticalElement
    {
        public List<StorySeismicLoad>  CalculateSeismicLoads( double T, double Cs, List<StorySeismicData> storyData)
        {
            double k = GetBuildingPeriodExponent_k(T);
            List<StorySeismicLoad> loads = CalculateSeismicLevelLoads(k, Cs, storyData);
            return loads;
        }

        public List<StorySeismicLoad>  CalculateSeismicLevelLoads( double k, double Cs, List<StorySeismicData> storyData)
        {
            List<StorySeismicLoad> loads = new List<StorySeismicLoad>();

            //Sum(w_i*h_i^k)
            int N = storyData.Count();
            double Sum_w_h_k = 0.0;
            double W = 0.0;

            foreach (var story in storyData)
            {
                double wi = story.SeismicWeight;
                double hi = story.ElevationFromBase;

                Sum_w_h_k = Sum_w_h_k + wi * Math.Pow(hi, k);
                W = W + wi;
            }

            double Vb = this.GetBaseShearVb(Cs, W);

            List<List<string>> ReportTableData = new List<List<string>>();

            foreach (var story in storyData)
            {
                double wx = story.SeismicWeight;
                double hx = story.ElevationFromBase;

                //(12.8-12)
                double Cvx = wx * Math.Pow(hx, k) / Sum_w_h_k;

                //(12.8-11)
                double Fx = Cvx * Vb;

                StorySeismicLoad load = new StorySeismicLoad()
                    {
                        Cvx = Cvx,
                        Fx = Fx,
                        StoryId = story.StoryId,
                        ElevationFromBase = story.ElevationFromBase,
                        Weight = story.SeismicWeight
                    };
                
                loads.Add(load);
                

                List<string> row = new List<string>()
                    {
                        load.StoryId,
                        Math.Round(load.ElevationFromBase,2).ToString(),
                        Math.Round(load.Weight,1).ToString(),
                        Math.Round(load.Cvx,3).ToString(),
                        Math.Round(load.Fx,1).ToString()
                    };
                ReportTableData.Add(row);

            }


            #region Fx
            ICalcLogEntry FxEntry = new CalcLogEntry();
            FxEntry.ValueName = "Fx"; // not used
            FxEntry.Reference = "";
            FxEntry.DescriptionReference = "/Templates/Loads/ASCE7_10/Seismic/SeismicStoryLoads.docx";
            FxEntry.FormulaID = null; //reference to formula from code
            FxEntry.VariableValue = Math.Round(0.0, 3).ToString(); //not used
            FxEntry.TableData = ReportTableData;
            FxEntry.TemplateTableTitle = "Seismic forces at levels";
            #endregion

            this.AddToLog(FxEntry);
            return loads;
        }
    }
}
