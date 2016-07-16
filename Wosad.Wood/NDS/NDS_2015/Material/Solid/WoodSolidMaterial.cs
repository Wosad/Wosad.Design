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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Wosad.Analytics.Wood.NDS;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Data;
using Wosad.Common.Entities;


namespace Wosad.Wood.NDS.NDS2015.Material
{
    public abstract class WoodSolidMaterial :AnalyticalElement, IWoodSolidMaterial
    {

        public WoodSolidMaterial(string ResourceString, string MaterialParameter1, string MaterialParameter2, ICalcLog CalcLog)
            : base(CalcLog)
        {
            this.ResourceString = ResourceString;
            this.MaterialParameter1 = MaterialParameter1;
            this.MaterialParameter2 = MaterialParameter2;
        }
        public bool ValuesWereCalculated { get; set; }

        protected abstract string GetResource();
        protected abstract void CreateReport();

        public string MaterialParameter1 { get; set; }
        public string MaterialParameter2 { get; set; }
        public string ResourceString { get; set; }
        protected void CalculateValues()
        {
            this.MaterialParameter1 = MaterialParameter1;
            this.MaterialParameter2 = MaterialParameter2;

            if (ValuesWereCalculated == false)
            {
                
                #region Read Table Data

                var Tv11 = new { MaterialParameter1 = "",  MaterialParameter2 = "", Fb = 0.0, Ft = 0.0, Fv = 0.0, FcPerp = 0.0, Fc = 0.0, E = 0.0, Emin = 0.0 }; // sample
                var ResultList = ListFactory.MakeList(Tv11);

                using (StringReader reader = new StringReader(ResourceString))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] Vals = line.Split(',');
                        if (Vals.Count() == 10)
                        {
                            string _MaterialParameter1 = Vals[0];
                            //string _Grade = Vals[1];
                            string _MaterialParameter2 = Vals[2];
                            double _Fb = double.Parse(Vals[3], CultureInfo.InvariantCulture);
                            double _Ft = double.Parse(Vals[4], CultureInfo.InvariantCulture);
                            double _Fv = double.Parse(Vals[5], CultureInfo.InvariantCulture);
                            double _FcPerp = double.Parse(Vals[6], CultureInfo.InvariantCulture);
                            double _Fc = double.Parse(Vals[7], CultureInfo.InvariantCulture);
                            double _E = double.Parse(Vals[8], CultureInfo.InvariantCulture);
                            double _Emin = double.Parse(Vals[9], CultureInfo.InvariantCulture);

                            ResultList.Add(new
                            {
                                MaterialParameter1 = _MaterialParameter1,
                                //Grade = _Grade,
                                MaterialParameter2 = _MaterialParameter2,
                                Fb = _Fb,
                                Ft = _Ft,
                                Fv = _Fv,
                                FcPerp = _FcPerp,
                                Fc = _Fc,
                                E = _E,
                                Emin = _Emin
                            });
                        }
                    }

                }

                #endregion

                var RValues = from v in ResultList
                              where
                                  (v.MaterialParameter1 == MaterialParameter1 &&
                                  v.MaterialParameter2 == MaterialParameter2)
                              select v;
                var foundValues = (RValues.ToList());
                if (foundValues.Count > 0)
                {
                    var ThisMaterialProps = foundValues.FirstOrDefault();
                    this.Bending = ThisMaterialProps.Fb / 1000.0;     //convert to ksi
                    this.TensionParallelToGrain = ThisMaterialProps.Ft / 1000.0;     //convert to ksi
                    this.ShearParallelToGrain = ThisMaterialProps.Fv / 1000.0;     //convert to ksi
                    this.CompresionPerpendicularToGrain = ThisMaterialProps.FcPerp / 1000.0;     //convert to ksi
                    this.CompresionParallelToGrain = ThisMaterialProps.Fc / 1000.0;     //convert to ksi
                    this.ModulusOfElasticity = ThisMaterialProps.E / 1000.0;     //convert to ksi
                    this.ModulusOfElasticityMin = ThisMaterialProps.Emin / 1000.0;     //convert to ksi
                }
                CreateReport();
                ValuesWereCalculated = true;
            }
        }


               double Fb;
        public double Bending
        {
            get
            {
                if (ValuesWereCalculated == false)
                {
                    CalculateValues();
                }
                return Fb;
            }

            set { Fb = value; }

        }

        

        double Fc;
        public double CompresionParallelToGrain
        {
            get
            {
                if (ValuesWereCalculated == false)
                {
                    CalculateValues();
                }
                return Fc;
            }
            set { Fc = value; }
        }

        double FcPerp;
        public double CompresionPerpendicularToGrain
        {
            get
            {
                if (ValuesWereCalculated == false)
                {
                    CalculateValues();
                }
                return FcPerp;
            }
            set { FcPerp = value; }
        }


        double Ft;
        public double TensionParallelToGrain
        {
            get
            {
                if (ValuesWereCalculated == false)
                {
                    CalculateValues();
                }
                return Ft;
            }
            set { Ft = value; }
        }

        double Fv;
        public double ShearParallelToGrain
        {
            get
            {
                if (ValuesWereCalculated == false)
                {
                    CalculateValues();
                }
                return Fv;
            }
            set { Fv = value; }
        }

        double E;
        public double ModulusOfElasticity
        {
            get
            {
                if (ValuesWereCalculated == false)
                {
                    CalculateValues();
                }
                return E;
            }
            set { E = value; }
        }

        double Emin;
        public double ModulusOfElasticityMin
        {
            get
            {
                if (ValuesWereCalculated == false)
                {
                    CalculateValues();
                }
                return Emin;
            }
            set { Emin = value; }
        }
    }
}
