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
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Interfaces;

namespace Wosad.Analysis
{
    public class BeamLoadFactory : IBeamLoadFactory
    {

       public double L               {get;set;}
       public double P               {get;set;}
       public double M               {get;set;}
       public double w               {get;set;}
       public double LoadDimension_a {get;set;}
       public double LoadDimension_b {get;set;}
       public double LoadDimension_c {get;set;}
       public double P1              {get;set;}
       public double P2              {get;set;}
       public double M1              {get;set;}
       public double M2              {get;set;}
       public IParameterExtractor ParameterExtractor  {get;set;}

        public BeamLoadFactory(IParameterExtractor Extractor)
        {
            this.ParameterExtractor = Extractor;
        }
        public LoadBeam GetLoad(string BeamCaseId)
        {
            LoadBeam Load = null;
            string LoadGroup =  BeamCaseId.Substring(1, 1); //simple, overhang, etc
            string loadingType = BeamCaseId.Substring(2, 1); //A-concentrated, B-distributed etc ...
            string subCase = BeamCaseId.Substring(BeamCaseId.Length - 1, 1); // specific sub-case

                    L = ParameterExtractor.GetParam("L");
                    P = ParameterExtractor.GetParam("P");
                    M = ParameterExtractor.GetParam("M");
                    w = ParameterExtractor.GetParam("w");
                    LoadDimension_a = ParameterExtractor.GetParam("LoadDimension_a");
                    LoadDimension_b = ParameterExtractor.GetParam("LoadDimension_b");
                    LoadDimension_c= L-LoadDimension_a-LoadDimension_b; //e.GetParam("LoadDimension_c");
                    P1 = ParameterExtractor.GetParam("P1");
                    P2 = ParameterExtractor.GetParam("P2");
                    M1 = ParameterExtractor.GetParam("M1");
                    M2 = ParameterExtractor.GetParam("M2");

            Load = GetBeamLoad(loadingType,subCase);

             return Load;
        }

        public virtual LoadBeam GetBeamLoad(string loadingType, string subCase)
        {
        LoadBeam Load = null;

             switch (loadingType)
            {
                case "A": //concentrated loads
                    return GetConcentratedCase(subCase);

                case "B": //distributed loads
                    return GetUniformDistributedCase(subCase);

                case "C": //partially distributed loads
                    return GetPartialDistributedCase(subCase);

                case "D": //Varying loads
                     return GetVaryingCase(subCase);

                case "E": //Moments
                     return GetMomentCase(subCase);

                default:
                    break;
            }
            return Load;
        }

        public virtual LoadBeam GetConcentratedCase(string subCase)
        {
            LoadBeam Load = null;

            switch (subCase)
            {
                case "1":
                    Load = new LoadConcentratedSpecial(P);
                    break;
                case "2":
                    Load = new LoadConcentratedGeneral(P, LoadDimension_a);
                    break;
                case "3":
                    Load = new LoadConcentratedDoubleSymmetrical(P, LoadDimension_a);
                    break;
                case "4":
                    Load = new LoadConcentratedDoubleUnsymmetrical(P1, P2, LoadDimension_a, LoadDimension_b);
                    break;
                case "5":
                    Load = new LoadConcentratedCenterWithEndMoments(P, M1, M2);
                    break;
                default:
                    Load = null;
                    break;
            }
            return Load;
        }
        public virtual LoadBeam GetUniformDistributedCase(string subCase)
        {
            LoadBeam Load = null;
            switch (subCase)
            {
                case "1":
                    Load = new LoadDistributedUniform(w);
                    break;
                case "2":
                    Load = new LoadDistributedUniformWithEndMoments(w, M1, M2);
                    break;
            }
            return Load;
        }
        public virtual LoadBeam GetPartialDistributedCase(string subCase)
        {
            LoadBeam Load = null;
                    Load = new LoadDistributedGeneral(w, LoadDimension_a, LoadDimension_a + LoadDimension_b);
            return Load;
        }
        public virtual LoadBeam GetVaryingCase(string subCase)
        {
            LoadBeam Load = null;
            switch (subCase)
            {
                case "1":
                    Load = new LoadDistributedUniform(w, LoadDistributedSpecialCase.Triangle);
                    break;
                case "2":
                    Load = new LoadDistributedUniform(w, LoadDistributedSpecialCase.DoubleTriangle);
                    break;
            }
            return Load;
        }
        public virtual LoadBeam GetMomentCase(string subCase)
        {
            LoadBeam Load = null;
            switch (subCase)
            {
                case "1":
                    Load = new LoadMomentLeftEnd(M);
                    break;
                case "2":
                    Load = new LoadMomentGeneral(M, LoadDimension_a);
                    break;
                case "3":
                    Load = new LoadMomentBothEnds(M1, M2);
                    break;
                case "4":
                    Load = new LoadMomentRightEnd(M);
                    break;
            }
            return Load;
        }
    }
}
