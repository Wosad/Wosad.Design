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
using Wosad.Common.Entities; 
using Wosad.Common.Section.Interfaces; 
using Wosad.Steel.AISC.Interfaces;
using Wosad.Common.CalculationLogger.Interfaces; 
using Wosad.Common.CalculationLogger;

using Wosad.Steel.AISC.Exceptions;
 using Wosad.Common.CalculationLogger;

namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public partial class BeamIDoublySymmetricCompact : BeamIDoublySymmetricBase
    {

        public virtual double Get_c()
        {
            double c = 1.0;

            
            #region c
            ICalcLogEntry cEntry = new CalcLogEntry();
            cEntry.ValueName = "c";
            cEntry.Reference = "";
            cEntry.DescriptionReference = "/Templates/Steel/AISC360_10/Flexure/F2_c_Ishape.docx";
            cEntry.FormulaID = null; //reference to formula from code
            cEntry.VariableValue = Math.Round(c, 3).ToString();
            #endregion
            this.AddToLog(cEntry);
            return c;
        }


        public virtual double Get_ho()
        {
            double ho;

            ISectionI section = this.Section.Shape as ISectionI;
            if (section != null)
            {
                //ho = section.h_o;
                ho = section.d - (section.t_fTop / 2.0 + section.t_fBot / 2.0);
                #region ho
                ICalcLogEntry hoEntry = new CalcLogEntry();
                hoEntry.ValueName = "ho";
                hoEntry.AddDependencyValue("d", Math.Round(section.d, 3));
                hoEntry.AddDependencyValue("tfc", Math.Round(section.t_fTop, 3));
                hoEntry.AddDependencyValue("tf", Math.Round(section.b_fBot, 3));
                hoEntry.Reference = "";
                hoEntry.DescriptionReference = "/Templates/Steel/AISC360_10/Flexure/ho_IShape.docx";
                hoEntry.FormulaID = null; //reference to formula from code
                hoEntry.VariableValue = Math.Round(ho, 3).ToString();
                #endregion
                this.AddToLog(hoEntry);
            }
            else
            {
                throw new SectionWrongTypeException(typeof(ISectionI));
            }
            return ho;
        }

        //Lateral Torsional Buckling F2.2
        public double GetFlexuralTorsionalBucklingMomentCapacity(double Cb)
        {

            double Lp = GetLp(ry, E, Fy); //(F2-5)

            double rts = Getrts(Iy, Cw, Sx);

            double Lr = GetLr(rts, E, Fy, Sx, J, c, ho);  // (F2-6)


            LateralTorsionalBucklingType BucklingType = GetLateralTorsionalBucklingType(Lb, Lp, Lr);
            double Mp;
            double Mn = 0.0;


            switch (BucklingType)
            {

                case LateralTorsionalBucklingType.NotApplicable:
                    Mn = double.PositiveInfinity;
                    break;
                case LateralTorsionalBucklingType.Inelastic:

                    Mp = GetYieldingMomentCapacity();
                    Mn = Cb * (Mp - (0.7 * Fy * Sx) * ((Lb - Lp) / (Lr - Lp))); //(F2-2)
                    Mn = Mn > Mp ? Mp : Mn;
                    break;
                case LateralTorsionalBucklingType.Elastic:
                    double Fcr = GetFcr(Cb, E, Lb, rts, J, c, Sx, ho);
                    Mn = Fcr * Sx; //(F2-3)
                    Mp = GetYieldingMomentCapacity();
                    Mn = Mn > Mp ? Mp : Mn;
                    break;
            }


            double M =  Mn * 0.9;
            return M;

        }

        public double GetLp(double ry, double E, double Fy)
        {
            double Lp = 1.76 * ry * Math.Sqrt(E / Fy); //(F2-5)

            
            #region Lp
            ICalcLogEntry LpEntry = new CalcLogEntry();
            LpEntry.ValueName = "Lp";
            LpEntry.AddDependencyValue("E", Math.Round(E, 3));
            LpEntry.AddDependencyValue("Fy", Math.Round(Fy, 3));
            LpEntry.AddDependencyValue("ry", Math.Round(ry, 3));
            LpEntry.Reference = "";
            LpEntry.DescriptionReference = "/Templates/Steel/AISC360_10/Flexure/F2_Lp.docx";
            LpEntry.FormulaID = null; //reference to formula from code
            LpEntry.VariableValue = Math.Round(Lp, 3).ToString();
            #endregion
            this.AddToLog(LpEntry);
            return Lp;
        }

        public double GetLr(double rts, double E, double Fy, double Sx, double J, double c, double ho)
        {
            double Lr = 1.95 * rts * E / (0.7 * Fy) * Math.Sqrt((J * c / (Sx * ho)) + Math.Sqrt(Math.Pow(J * c / (Sx * ho), 2.0) + 6.76 * Math.Pow(0.7 * Fy / E, 2.0)));  // (F2-6)
            
            #region Lr
            ICalcLogEntry LrEntry = new CalcLogEntry();
            LrEntry.ValueName = "Lr";
            LrEntry.AddDependencyValue("Fy", Math.Round(Fy, 3));
            LrEntry.AddDependencyValue("E", Math.Round(E, 3));
            LrEntry.AddDependencyValue("J", Math.Round(J, 3));
            LrEntry.AddDependencyValue("c", Math.Round(c, 3));
            LrEntry.AddDependencyValue("ho", Math.Round(ho, 3));
            LrEntry.AddDependencyValue("Sx", Math.Round(Sx, 3));
            LrEntry.AddDependencyValue("rts", Math.Round(rts, 3));
            LrEntry.Reference = "";
            LrEntry.DescriptionReference = "/Templates/Steel/AISC360_10/Flexure/F2_Lr.docx";
            LrEntry.FormulaID = null; //reference to formula from code
            LrEntry.VariableValue = Math.Round(Lr, 3).ToString();
            #endregion
            this.AddToLog(LrEntry);
            return Lr;
        }

        public double Getrts(double Iy, double Cw, double Sx)
        {
            double rts = Math.Sqrt(Math.Sqrt(Iy * Cw) / Sx);
            
            #region rts
            ICalcLogEntry rtsEntry = new CalcLogEntry();
            rtsEntry.ValueName = "rts";
            rtsEntry.AddDependencyValue("Iy", Math.Round(Iy, 3));
            rtsEntry.AddDependencyValue("Cw", Math.Round(Cw, 3));
            rtsEntry.AddDependencyValue("Sx", Math.Round(Sx, 3));
            rtsEntry.Reference = "";
            rtsEntry.DescriptionReference = "/Templates/Steel/AISC360_10/Flexure/F2_rts.docx";
            rtsEntry.FormulaID = null; //reference to formula from code
            rtsEntry.VariableValue = Math.Round(rts, 3).ToString();
            #endregion
            this.AddToLog(rtsEntry);
            return rts;
        }

        public double GetFcr(double Cb, double E, double Lb, double rts, double J, double c, double Sx, double ho)
        {
            double Fcr;
            double pi2 = Math.Pow(Math.PI, 2);
            Fcr = Cb * pi2 * E / (Math.Pow(Lb / rts, 2)) * Math.Sqrt(1.0 + 0.078 * J * c / (Sx * ho) * Math.Pow(Lb / rts, 2)); //(F2-4)

            return Fcr;
        }


    }
}
