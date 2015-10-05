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
using Wosad.Concrete.ACI;
using Wosad.Common.Entities;
using Wosad.Concrete.ACI.Infrastructure.Entities;
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
    public partial class DevelopmentTension : Development
    {


           
        //internal double GetTensionDevelopmentLength(double fy, double sqrt_fc, double fc, double lambda,
        //double ksi_t, double ksi_e, double ksi_s, double cb, double Ktr)
        //{

    private void GetDevelopmentLengthParameters(ref double lambda, ref double fc, ref double sqrt_fc, ref double fy, ref double db, 
                                                    ref double ksi_t, ref double ksi_e, ref double ksi_s, ref double ksi_tAndKsi_eProduct)
    {
     fy = Rebar.Material.YieldStress;
     fc = Concrete.SpecifiedCompressiveStrength;
     sqrt_fc = GetSqrt_fc();
     lambda = Concrete.Lambda;
     lambda = CheckLambda(lambda);
     db = Rebar.Diameter;

     //Get ksi Factors and lambda factor

     ksi_t = GetKsi_t();
     ksi_e = GetKsi_e();
     ksi_s = GetKsi_s();

     if (ksi_t == 0.0 || ksi_e == 0.0 || ksi_s == 0.0)
     {
         throw new Exception("Failed to calculate at least one ksi-factor. Please check input");
     }



     //double ksi_tAndKsi_eProduct = ksi_t * ksi_e;

     //if (ksi_tAndKsi_eProduct > 1.7)
     //{

     //    ICalcLogEntry ent1 = Log.CreateNewEntry();
     //    ent1.ValueName = "ksi_t*ksi_e";
     //    ent1.AddDependencyValue("ksi_tAndKsi_eProduct", ksi_tAndKsi_eProduct);
     //    ent1.Reference = "ACI Section 12.2.4 (b)";
     //    ent1.DescriptionReference = "ksi_t*ksi_e";
     //    ent1.FormulaID = "P-12.2.4-2";
     //    ent1.VariableValue = ksi_tAndKsi_eProduct.ToString();
     //    AddToLog(ent1);
     //    ksi_tAndKsi_eProduct = 1.7;

     //}

      ksi_tAndKsi_eProduct = Getksi_tAndKsi_eProduct(ksi_t, ksi_e);
   

}
   [ReportElement(
new string[] { gv.fy,gv.fc, gv.db,v.ksi_t, v.ksi_e,v.ksi_s,v.cb,v.Ktr  },
new string[] { f._12_1 },
new string[] { d.ldGeneral })]
           
        internal double GetTensionDevelopmentLength(double transverseRebarArea, double transverseRebarSpacing, double NumberOfSplicedBars)
        {

            double ld;
            double lambda =0; 
            double fc=0; 
            double sqrt_fc=0; 
            double fy=0;
            double db=0;
            double ksi_t=0; 
            double ksi_e=0;
            double ksi_s=0;
            double ksi_tAndKsi_eProduct=0;

            GetDevelopmentLengthParameters(ref  lambda, ref  fc, ref  sqrt_fc, ref  fy, ref  db, ref ksi_t,ref ksi_e,ref ksi_s, ref ksi_tAndKsi_eProduct);


            double cb = GetCb();
            double Ktr = GetKtr(transverseRebarArea,transverseRebarSpacing,NumberOfSplicedBars);

            ICalcLogEntry ent2 = Log.CreateNewEntry();
            ent2.ValueName = v.ld;
            ent2.AddDependencyValue(gv.fy, fy);
            ent2.AddDependencyValue(gv.fc, fc);
            ent2.AddDependencyValue(gv.db, db);
            ent2.AddDependencyValue(v.ksi_t_ksi_e_Product, ksi_tAndKsi_eProduct);
            ent2.DescriptionReference = d.ldGeneral;
            ent2.AddDependencyValue(v.ksi_s, ksi_s);
            ent2.AddDependencyValue(v.cb, cb);
            ent2.AddDependencyValue(v.Ktr, Ktr);
            ent2.Reference = "ACI Eq. 12-1";
            ent2.FormulaID = f._12_1;

            double ConfinementTerm = GetConfinementTerm(cb, Ktr);
            ld = 3.0 / 40.0 * (fy / (lambda * sqrt_fc)) * (ksi_tAndKsi_eProduct * ksi_s / (ConfinementTerm)) * db;

            ent2.VariableValue = ld.ToString();
            AddToLog(ent2);

            ld = CheckExcessReinforcement(ld, true,false);
            
    
            if (this.CheckMinimumLength == true)
            {

                ld = GetMinimumLength(ld);
            }


            Length = ld;
            return ld;
        }

        [ReportElement(
        new string[] { v.ld, v.ksi_t_ksi_e_Product },
        new string[] { "P-12.2.4-2", "12-1", "P-12.2.2-1", "P-12.2.2-2", "P-12.2.2-3", "P-12.2.2-4", "P-12.2.1-1" },
        new string[] { "ksi_t*ksi_e", "ldTension", "ldTensionMinimum" })]
        internal double GetTensionDevelopmentLength(bool minimumShearReinforcementProvided)
        {
            double ld;

            double lambda = 0;
            double fc = 0;
            double sqrt_fc = 0;
            double fy = 0;
            double db = 0;
            double ksi_t = 0;
            double ksi_e = 0;
            double ksi_s = 0;
            double ksi_tAndKsi_eProduct = 0;

            GetDevelopmentLengthParameters(ref  lambda, ref  fc, ref  sqrt_fc, ref  fy, ref  db, ref ksi_t, ref ksi_e, ref ksi_s, ref ksi_tAndKsi_eProduct);


            ICalcLogEntry ent2 = Log.CreateNewEntry();
            ent2.ValueName = "ld";
            ent2.AddDependencyValue("fy", fy);
            ent2.AddDependencyValue("fc", fc);
            ent2.AddDependencyValue("ksi_t", ksi_t);
            ent2.AddDependencyValue("ksi_e", ksi_e);

            ent2.AddDependencyValue("db", db);

            ent2.DescriptionReference = "ldTension";
            //use simplified formula here
            ent2.Reference = "ACI Section. 12.2.2";

            if (clearSpacing >= db ||
                clearCover >= db ||
                minimumShearReinforcementProvided == true)
            {
                if (db < 7 / 8)
                {
                    //Formula A
                    ent2.FormulaID = "P-12.2.2-1";
                    ld = (fy * ksi_tAndKsi_eProduct / (25 * lambda * sqrt_fc)) * db;
                }
                else
                {
                    //Formula B
                    ent2.FormulaID = "P-12.2.2-2";
                    ld = (fy * ksi_tAndKsi_eProduct / (20 * lambda * sqrt_fc)) * db;
                }
            }
            else
            {
                if (db < 7 / 8)
                {
                    //Formula C
                    ent2.FormulaID = "P-12.2.2-3";
                    ld = (3.0 * fy * ksi_tAndKsi_eProduct / (50 * lambda * sqrt_fc)) * db;
                }
                else
                {
                    //Formula D
                    ent2.FormulaID = "P-12.2.2-4";
                    ld = (3.0 * fy * ksi_tAndKsi_eProduct / (40 * lambda * sqrt_fc)) * db;
                }
            }

            ent2.VariableValue = ld.ToString();
            AddToLog(ent2);


            ld = CheckExcessReinforcement(ld, true, false);


            if (this.CheckMinimumLength == true)
            {

                ld = GetMinimumLength(ld);
            }


            return ld;
        }

        
        internal double GetMinimumLength(double ld)
        {
            if (ld < 12.0)
            {

                ICalcLogEntry ent3 = Log.CreateNewEntry();
                ent3.ValueName = "ld";
                ent3.AddDependencyValue("ld", ld);
                ent3.Reference = "ACI Section 12.2.1";
                ent3.DescriptionReference = "ldTensionMinimum";
                ent3.FormulaID = "P-12.2.1-1";
                ld = 12.0;
                ent3.VariableValue = ld.ToString();
                AddToLog(ent3);
            }
            return ld;
        }
        

    }
}
