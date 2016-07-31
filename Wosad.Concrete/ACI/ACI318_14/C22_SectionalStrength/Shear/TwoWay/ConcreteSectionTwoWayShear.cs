using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Concrete.ACI.ACI318_14.C22_SectionalStrength.Shear.TwoWay
{
    public class ConcreteSectionTwoWayShear
    {
        /// <summary>
        /// Two-way shear section
        /// </summary>
        /// <param name="Material">Concrete material</param>
        /// <param name="Segments">Shear perimeter segments</param>
        /// <param name="d">Effective slab section</param>
        /// <param name="b_1">Column dimension</param>
        /// <param name="b_2">Column dimension</param>
        /// <param name="AtColumnFace">Identifies if the section is adjacent to column face (typical) or away from column face (as is the case with shear studs away from the face)</param>
        /// <param name="ColumnType">Identifies if the column is located at the interior, slab edge, or slab corner</param>
        public ConcreteSectionTwoWayShear(IConcreteMaterial Material, List<PerimeterLineSegment> Segments, double d,
            double b_1, double b_2, bool AtColumnFace, PunchingPerimeterColumnType ColumnType)
        {
            this.Material    =Material      ;
            this.Segments    =Segments      ;
            this.d           =d             ;
            this.b_1         =b_1           ;
            this.b_2         =b_2           ;
            this.AtColumnFace=AtColumnFace  ;
            this.ColumnType = ColumnType    ;
        }
        /// <summary>
        /// Indicates if this a section at column interface
        /// </summary>
        public bool AtColumnFace { get; set; }

        IConcreteMaterial Material { get; set; }
        public double d { get; set; }

        public List<PerimeterLineSegment> Segments { get; set; }

        public PunchingPerimeterColumnType ColumnType { get; set; }


        /// <summary>
        /// Column dimension
        /// </summary>
        public double b_1 { get; set; }
        public double b_2 { get; set; }

        /// <summary>
        /// Table 22.6.5.2
        /// </summary>
        /// <returns></returns>
        public double GetTwoWayStrengthForUnreinforcedConcrete()
        {
            double f_c = Material.SpecifiedCompressiveStrength;

            double beta = GetBeta();
            double alpha_s = Get_alpha_s();
            double b_o = Get_b_o();

            double v_c1 =4.0*Math.Sqrt(f_c);
            double v_c2=(2.0+((4.0) / (beta)))*Math.Sqrt(f_c);
            double v_c3=(((alpha_s*d) / (b_o))+2.0)*Math.Sqrt(f_c);

            List<double> vc_s = new List<double>()
            {
                v_c1,
                v_c2,
                v_c3
            };

            double phi = 0.75;
            return phi* vc_s.Min();
        }

        private double Get_b_o()
        {
            double b_o = Segments.Sum(s => s.Length);
            return b_o;
        }

        /// <summary>
        /// Section 22.6.5.3
        /// </summary>
        /// <returns></returns>
        private double Get_alpha_s()
        {
            switch (ColumnType)
            {
                case PunchingPerimeterColumnType.Interior:
                    return 40.0;
                    break;
                case PunchingPerimeterColumnType.Edge:
                    return 30.0;
                    break;
                case PunchingPerimeterColumnType.Corner:
                    return 20.0;
                    break;
                default:
                    return 40.0;
                    break;
            }
        }

        /// <summary>
        /// Footnote Table 22.6.5.2
        /// </summary>
        /// <returns></returns>
        private double GetBeta()
        {
           
            if (b_1>b_2)
            {
                return b_1 / b_2;
            }
            else
            {
                return b_2 / b_1;
            }
        }


    }
}
