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
using Wosad.Concrete.ACI;
using Wosad.Common.Entities;
using Wosad.Common.Reports;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Concrete.ACI.Entities;
using Wosad.Concrete.ACI.Infrastructure.Entities.Concrete;


namespace Wosad.Concrete.ACI318_11.Materials
{
	public class ConcreteMaterial: ConcreteMaterialBase
	{

		public ConcreteMaterial(double SpecifiedConcreteStrength,
			ConcreteTypeByWeight ConcreteType, double Density, ICalcLog log)
			: base(SpecifiedConcreteStrength, ConcreteType, Density, log)
		{
		}

		public ConcreteMaterial(double SpecifiedConcreteStrength,
		ConcreteTypeByWeight ConcreteType,ICalcLog log)
			: base(SpecifiedConcreteStrength, ConcreteType, 150.0, log)
		{
		}

		public ConcreteMaterial(double SpecifiedConcreteStrength,
			ConcreteTypeByWeight ConcreteType, double Density, double AverageSplittingTensileStrength, ICalcLog log)
			: base(SpecifiedConcreteStrength, ConcreteType,AverageSplittingTensileStrength, Density, log)
		{
			
		}

		public ConcreteMaterial(double SpecifiedConcreteStrength,
		ConcreteTypeByWeight ConcreteType, double Density, TypeOfLightweightConcrete LightWeightConcreteType, ICalcLog log)
			: base(SpecifiedConcreteStrength, ConcreteType, Density, log)
		{
			this.LightWeightConcreteType = LightWeightConcreteType;

		}

		public ConcreteMaterial(double SpecifiedConcreteStrength,
		ConcreteTypeByWeight ConcreteType, TypeOfLightweightConcrete LightWeightConcreteType, ICalcLog log)
			: base(SpecifiedConcreteStrength, ConcreteType, log)
		{
			this.LightWeightConcreteType = LightWeightConcreteType;

		}


		public override double ModulusOfElasticity
		{
			get
			{
				return GetEc();
			}
		}

		public override double ModulusOfRupture
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	
		[ReportElement
			(   new string[] { "Ec" },
				new string[] { "P-8.5.1-1", "P-8.5.1-2" },
				new string[] { "Ec" }
			)]

		private double GetEc()
		{

			double fc = this.SpecifiedCompressiveStrength;
			double sqrt_fc = GetSqrtFc();
			ICalcLogEntry ent = Log.CreateNewEntry();
			ent.AddDependencyValue("lambda", lambda);
			ent.AddDependencyValue("fc", fc);
			ent.Reference = "ACI Section 8.5.1";
			ent.DescriptionReference = "Ec";
			ent.ValueName = "Ec";

			double E;

			if (lambda == 0.0)
			{
				lambda = GetLambda();
			}

			if (this.Density==0.0)
			{
				E = 57000 * lambda* sqrt_fc;
				ent.FormulaID = "P-8.5.1-1";
				ent.VariableValue = E.ToString();

			}
			else
			{
				double wc = this.Density;
				if (wc>=90 && wc<=160)
				{
					ent.AddDependencyValue("wc", wc);
						E= Math.Pow(wc, 1.5) * 33.0 * lambda * sqrt_fc;
					ent.FormulaID = "P-8.5.1-2";
					ent.VariableValue = E.ToString();
				}
				else
				{
					throw new Exception("Concrete density is outside of the normal range. Density between 90 and 160 is expected");
					  
				}
			}
			if (LogModeActive==true)
			{
				AddToLog(ent);  
			}
			return E;
		}

		private double GetSqrtFc()
		{
			double fc = this.SpecifiedCompressiveStrength;
			lambda = GetLambda();
			double sqrt_fc = Math.Sqrt(fc);
			return sqrt_fc;
		}

		private double lambda;

		public override double Lambda
		{
			get 
			{
				if (lambda ==0.0)
				{
					lambda = GetLambda(); 
				}
				return lambda; 
			}
		}


		private TypeOfLightweightConcrete lightWeightType;

		public TypeOfLightweightConcrete LightWeightConcreteType
		{
			get { return lightWeightType; }
			set { lightWeightType = value; }
		}


[ReportElement(
new string[] { "lambda", },
new string[] { "lambda", "P-8.6.1-1" },
new string[] { "lambdaNormalWeight", })]
		   
		private double GetLambda()
		{
			double lambda;
			ICalcLogEntry ent = Log.CreateNewEntry();
			
			ent.ValueName = "lambda";
			ent.Reference = "ACI Section 8.6.1";

			if (this.TypeByWeight == ConcreteTypeByWeight.Normalweight)
			{
				ent.DescriptionReference = "lambdaNormalWeight";
				ent.FormulaID = "lambda";
				lambda = 1.0;
			}
			else
			{
				double fct = AverageSplittingTensileStrength;
				if (fct>0.0)
				{
					//8.6.1
					double sqrt_fc = Math.Sqrt(this.SpecifiedCompressiveStrength);
					lambda=fct/(6.7*sqrt_fc);
					ent.DescriptionReference = "lambdaWithSplittingTensileStrength";
					ent.FormulaID = "P-8.6.1-1";
				}
				else
				{
					lambda = lightWeightType == TypeOfLightweightConcrete.SandLightweightConcrete ? 0.85 : 0.75;
					ent.FormulaID = "lambda";
				}
			}
			if (LogModeActive==true)
			{
				AddToLog(ent); 
			}
			ent.VariableValue = lambda.ToString();
			return lambda;
		}

[ReportElement(
new string[] { "fr", },
new string[] { "9-10"},
new string[] { "fr" })]
		   
		private double GetModulusOfRupture()
		{
			ICalcLogEntry ent = Log.CreateNewEntry();
			ent.ValueName = "fr";
			ent.DescriptionReference = "fr";
			ent.AddDependencyValue("lambda",Lambda); //calculated 1st time
			ent.AddDependencyValue("fc", SpecifiedCompressiveStrength);
			ent.FormulaID = "9-10";
			ent.Reference = "ACI Eq. 9-10";

			double sqrt_fc = Math.Sqrt(this.SpecifiedCompressiveStrength);
			double fr = 7.5 * lambda * sqrt_fc;

			ent.VariableValue = fr.ToString();
			if (LogModeActive==true)
			{
				AddToLog(ent);
			}
			return fr;
		}


	public override double beta1
	{
		get 
		
		{
			double fc = base.SpecifiedCompressiveStrength;
			double _beta1 = 0.85 - 0.05 * ((fc - 4000.0) / 1000.0);
			double beta1_corrected;
			beta1_corrected = _beta1 <= 0.65 ? 0.65 : _beta1;
			beta1_corrected = _beta1 >= 0.85 ? 0.85 : _beta1;
			return beta1_corrected;
		
		}
	}
	private double epsilon_u;

	public double  ConcreteUltimateStrain
	{
		get { return 0.003; }
	}
		

		}


	
}