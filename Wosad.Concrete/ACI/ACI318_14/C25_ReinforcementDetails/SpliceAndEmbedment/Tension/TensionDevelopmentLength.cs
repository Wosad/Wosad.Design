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
using Wosad.Common.Reports; 
using Wosad.Common.CalculationLogger.Interfaces; 
using Wosad.Common.CalculationLogger;


namespace Wosad.Concrete.ACI318_14
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

           
        public double GetTensionDevelopmentLength(double transverseRebarArea, double transverseRebarSpacing, double NumberOfSplicedBars)
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


            double ConfinementTerm = GetConfinementTerm(cb, Ktr);
            ld = 3.0 / 40.0 * (fy / (lambda * sqrt_fc)) * (ksi_tAndKsi_eProduct * ksi_s / (ConfinementTerm)) * db;


            ld = CheckExcessReinforcement(ld, true,false);
            
    
            if (this.CheckMinimumLength == true)
            {

                ld = GetMinimumLength(ld);
            }


            Length = ld;
            return ld;
        }


        public double GetTensionDevelopmentLength(bool minimumShearReinforcementProvided)
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


            if (clearSpacing >= db ||
                clearCover >= db ||
                minimumShearReinforcementProvided == true)
            {
                if (db < 7 / 8)
                {
                    //Formula A
                    ld = (fy * ksi_tAndKsi_eProduct / (25 * lambda * sqrt_fc)) * db;
                }
                else
                {
                    //Formula B
                    ld = (fy * ksi_tAndKsi_eProduct / (20 * lambda * sqrt_fc)) * db;
                }
            }
            else
            {
                if (db < 7 / 8)
                {
                    //Formula C
                    ld = (3.0 * fy * ksi_tAndKsi_eProduct / (50 * lambda * sqrt_fc)) * db;
                }
                else
                {
                    //Formula D
                    ld = (3.0 * fy * ksi_tAndKsi_eProduct / (40 * lambda * sqrt_fc)) * db;
                }
            }



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

                ld = 12.0;

            }
            return ld;
        }
        

    }
}
