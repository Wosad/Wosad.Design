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
using Wosad.Concrete.ACI.Infrastructure.Entities;
using Wosad.Concrete.ACI;
using Wosad.Common.Entities;
using Wosad.Common.Reports; using Wosad.Common.CalculationLogger.Interfaces; using Wosad.Common.CalculationLogger;
using p = Wosad.Concrete.ACI318_11.TensionDevelopmentParagraphs;
using f = Wosad.Concrete.ACI318_11.TensionDevelopmentFormulas;
using v = Wosad.Concrete.ACI318_11.TensionDevelopmentValues;
using d = Wosad.Concrete.ACI318_11.TensionDevelopmentDescriptions;
using dv = Wosad.Concrete.ACI318_11.DevelopmentValues;
using gv = Wosad.Concrete.ACI318_11.GeneralValues;
using gd = Wosad.Concrete.ACI318_11.GeneralDescriptions;
using gf = Wosad.Concrete.ACI318_11.GeneralFormulas;

namespace Wosad.Concrete.ACI318_11
{
    public partial class DevelopmentTension:Development
    {

[ReportElement(
new string[] { v.ksi_t, },
new string[] { f.ksi_t},
new string[] { d.ksi_t_Bottom, d.ksi_t_Top})]
     
         
        internal double GetKsi_t()
        {
            //12.2.4
            //(a) Where horizontal reinforcement is placed such
            //that more than 12 in. of fresh concrete is cast below
            //the development length or splice, ?t = 1.3. For other
            //situations, ?t = 1.0.
             
    ICalcLogEntry ent = Log.CreateNewEntry();
    double ksi_t;
    
    if (isTopRebar==true)
	{
		 ksi_t=1.3;
        ent.DescriptionReference = d.ksi_t_Top;
	}
    else
	{
        ksi_t=1.0;
        ent.DescriptionReference = d.ksi_t_Bottom;
	}

            
            ent.ValueName = v.ksi_t;
            ent.Reference = "ACI Section 12.2.4 (a)";
            
            ent.FormulaID = f.ksi_t;
            ent.VariableValue = ksi_t.ToString();

            AddToLog(ent);

            return ksi_t;
        }

[ReportElement(
new string[] { v.ksi_e },
new string[] { f.ksi_e },
new string[] { d.ksi_e_CoatedLargeSpacing, d.ksi_e_CoatedSmallSpacing,d.ksi_e_Uncoated})]
           
        
        internal double GetKsi_e()
        {
            //(b) For epoxy-coated bars or wires with cover less
            //than 3db, or clear spacing less than 6db, ?e = 1.5.
            //For all other epoxy-coated bars or wires, ?e = 1.2.
            //For uncoated and zinc-coated (galvanized) reinforcement,
            //?e = 1.0.
            
            double sc = clearCover;

            double ksi_e;


            ICalcLogEntry ent = Log.CreateNewEntry();
            ent.ValueName = v.ksi_e;
            ent.Reference = "ACI Section 12.2.4 (b)";
            ent.FormulaID = v.ksi_e;


            if (Rebar.IsEpoxyCoated == false)
            {

                ksi_e = 1.0;
                ent.DescriptionReference = d.ksi_e_Uncoated;


            }
            else
            {
                if (db<=0 || clearCover<=0)
                {
                    throw new Exception("bar diameter or clear cover cannot be <= 0. Please check input");
                }

                ent.AddDependencyValue(gv.db, db);
                ent.AddDependencyValue(gv.cc, clearCover);
                if (clearCover < 3.0 * db || clearSpacing < 6.0 * db)
                {
                    ent.DescriptionReference = d.ksi_e_CoatedSmallSpacing;
                    ksi_e = 1.5;
                }
                else
                {
                    ent.DescriptionReference = d.ksi_e_CoatedLargeSpacing;
                    ksi_e = 1.2;
                }
            }

            ent.VariableValue = ksi_e.ToString();
            AddToLog(ent);

            return ksi_e;
        }


[ReportElement(
new string[] { v.ksi_s, },
new string[] { v.ksi_s},
new string[] { d.ksi_s_LargeDiameter, d.ksi_s_SmallDiameter})]
           
        internal double GetKsi_s()
        {
            //(c) For No. 6 and smaller bars and deformed wires,
            //?s = 0.8. For No. 7 and larger bars, ?s = 1.0.

            
            ICalcLogEntry ent = Log.CreateNewEntry();
            ent.ValueName = v.ksi_s;
            

            if (db<=0.0)
            {
                throw new Exception("Bar diamater cannot be <=0. Please check input");
            }

            ent.AddDependencyValue(gv.db, db);
            ent.Reference = "ACI Section 12.2.4 (c)";
            

            double ksi_s;

            if (Rebar.Diameter<7/8)
            {
                ent.DescriptionReference = d.ksi_s_SmallDiameter;
                ksi_s = 0.8;
            }
            else
            {
                ent.DescriptionReference = d.ksi_s_LargeDiameter;
                ksi_s = 1.0;
            }

            ent.FormulaID = f.ksi_s;
            ent.VariableValue = ksi_s.ToString();

            AddToLog(ent);

            return ksi_s;
        }


[ReportElement(
new string[] { v.ksi_t_ksi_e_Product },
new string[] { f.ksi_t_ksi_e_Product},
new string[] { d.ksi_t_ksi_e_Product })]

internal double Getksi_tAndKsi_eProduct(double ksi_t, double ksi_e)
{
    double ksi_tAndKsi_eProduct = ksi_t * ksi_e;
    //However, the product ?t?e need not be greater than 1.7.

    if (ksi_tAndKsi_eProduct > 1.7)
    {

        ICalcLogEntry ent1 = Log.CreateNewEntry();
        ent1.ValueName = v.ksi_t_ksi_e_Product;
        ent1.AddDependencyValue(v.ksi_t, ksi_t);
        ent1.AddDependencyValue(v.ksi_e, ksi_e);
        ent1.Reference = "ACI Section 12.2.4 (b)";
        ent1.DescriptionReference = d.ksi_t_ksi_e_Product;
        ent1.FormulaID = f.ksi_t_ksi_e_Product;
        ent1.VariableValue = ksi_tAndKsi_eProduct.ToString();
        AddToLog(ent1);
        ksi_tAndKsi_eProduct = 1.7;

    }
    return ksi_tAndKsi_eProduct;
}


    }
}
