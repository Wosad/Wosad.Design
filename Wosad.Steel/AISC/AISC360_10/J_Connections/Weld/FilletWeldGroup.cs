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
using System.Threading.Tasks;
using Wosad.Common.Mathematics;
using Wosad.Steel.AISC.SteelEntities.Welds;

namespace Wosad.Steel.AISC.AISC360_10.Connections
{
    public class FilletWeldGroup : FilletWeldLoadDeformationGroupBase
    {
        public FilletWeldGroup(string WeldPattern, double l_horizontal, double l_vertical, double leg, double ElectrodeStrength  )
        {
            this.l_horizontal= l_horizontal;
            this.l_vertical = l_vertical;
            this.leg = leg;
            this.F_EXX = ElectrodeStrength;
            Nsub = 20;

             switch (WeldPattern)
	        {                                    
                  case  "ParallelVertical"       :  AddParallelVertical      (); break;
                  case  "ParallelHorizontal" :  AddWeldParallelHorizontal(); break;
                  case  "Rectangle"              :  AddRectangle             (); break;
                  case  "C"                      :  AddC                     (); break;
                  case "L"                       :  AddL                     (); break;
                  default: AddParallelVertical(); break;                                            
	        }


        }

        double l_horizontal;
        double l_vertical;
        double leg;
        double F_EXX;
        int Nsub;

        private void AddL()
        {
            Point2D p1_temp = new Point2D(-l_horizontal/2.0,-l_vertical/2.0);
            Point2D p2_temp = new Point2D(-l_horizontal/2.0,l_vertical/2.0);
            Point2D p3_temp = new Point2D(l_horizontal/2.0,l_vertical/2.0);


            List<FilletWeldLine> templines = new List<FilletWeldLine>()
            {
                new FilletWeldLine(p1_temp,p2_temp,leg,F_EXX,Nsub),
                new FilletWeldLine(p2_temp,p3_temp,leg,F_EXX,Nsub)
            };
            FilletWeldLoadDeformationGroupBase tempGroup = new FilletWeldLoadDeformationGroupBase(templines);
            var centroid = tempGroup.GetElasticCentroid();

            Point2D p1 = new Point2D(p1_temp.X - centroid.X, p1_temp.Y - centroid.Y);
            Point2D p2 = new Point2D(p2_temp.X - centroid.X, p2_temp.Y - centroid.Y);
            Point2D p3 = new Point2D(p3_temp.X - centroid.X, p3_temp.Y - centroid.Y);


            List<FilletWeldLine> lines = new List<FilletWeldLine>()
            {
                new FilletWeldLine(p1,p2,leg,F_EXX,Nsub),
                new FilletWeldLine(p2,p3,leg,F_EXX,Nsub)
            };
            Lines = lines;

        }

        private void AddC()
        {
            Point2D p1_temp = new Point2D(0, -l_vertical / 2.0);
            Point2D p2_temp = new Point2D(0, l_vertical / 2.0);
            Point2D p3_temp = new Point2D(l_horizontal, l_vertical / 2.0);
            Point2D p4_temp = new Point2D(l_horizontal, -l_vertical / 2.0);


            List<FilletWeldLine> templines = new List<FilletWeldLine>()
            {
                new FilletWeldLine(p1_temp,p2_temp,leg,F_EXX,Nsub),
                new FilletWeldLine(p2_temp,p3_temp,leg,F_EXX,Nsub),
                new FilletWeldLine(p1_temp,p4_temp,leg,F_EXX,Nsub),
            };
            FilletWeldLoadDeformationGroupBase tempGroup = new FilletWeldLoadDeformationGroupBase(templines);
            var centroid = tempGroup.GetElasticCentroid();

            Point2D p1 = new Point2D(p1_temp.X-centroid.X, p1_temp.Y-centroid.Y);
            Point2D p2 = new Point2D(p2_temp.X-centroid.X, p2_temp.Y-centroid.Y);
            Point2D p3 = new Point2D(p3_temp.X-centroid.X, p3_temp.Y-centroid.Y);
            Point2D p4 = new Point2D(p4_temp.X-centroid.X, p4_temp.Y-centroid.Y);


            List<FilletWeldLine> lines = new List<FilletWeldLine>()
            {
                new FilletWeldLine(p1,p2,leg,F_EXX,Nsub),
                new FilletWeldLine(p2,p3,leg,F_EXX,Nsub),
                new FilletWeldLine(p1,p4,leg,F_EXX,Nsub),
            };
            Lines = lines;
        }
        private void AddRectangle()
        {
            Point2D p1 = new Point2D(-l_horizontal/2.0,-l_vertical/2.0);
            Point2D p2 = new Point2D(-l_horizontal/2.0,l_vertical/2.0);
            Point2D p3 = new Point2D(l_horizontal/2.0,l_vertical/2.0);
            Point2D p4 = new Point2D(l_horizontal/2.0,-l_vertical/2.0);

            List<FilletWeldLine> lines = new List<FilletWeldLine>()
            {
                new FilletWeldLine(p1,p2,leg,F_EXX,Nsub),
                new FilletWeldLine(p2,p3,leg,F_EXX,Nsub),
                new FilletWeldLine(p3,p4,leg,F_EXX,Nsub),
                new FilletWeldLine(p4,p1,leg,F_EXX,Nsub),
            };
            Lines = lines;
        }

        private void AddWeldParallelHorizontal()
        {
            Point2D p1 = new Point2D(-l_horizontal/2.0,-l_vertical/2.0);
            Point2D p2 = new Point2D(-l_horizontal/2.0,l_vertical/2.0);
            Point2D p3 = new Point2D(l_horizontal/2.0,l_vertical/2.0);
            Point2D p4 = new Point2D(l_horizontal/2.0,-l_vertical/2.0);

            List<FilletWeldLine> lines = new List<FilletWeldLine>()
            {
                new FilletWeldLine(p2,p3,leg,F_EXX,Nsub),
                new FilletWeldLine(p4,p1,leg,F_EXX,Nsub),
            };
            Lines = lines;
        }

        private void AddParallelVertical()
        {
            Point2D p1 = new Point2D(-l_horizontal / 2.0, -l_vertical / 2.0);
            Point2D p2 = new Point2D(-l_horizontal / 2.0, l_vertical / 2.0);
            Point2D p3 = new Point2D(l_horizontal / 2.0, l_vertical / 2.0);
            Point2D p4 = new Point2D(l_horizontal / 2.0, -l_vertical / 2.0);

            List<FilletWeldLine> lines = new List<FilletWeldLine>()
            {
                new FilletWeldLine(p1,p2,leg,F_EXX,Nsub),
                new FilletWeldLine(p3,p4,leg,F_EXX,Nsub),
            };
            Lines = lines;
        }

        public double GetInstantaneousCenterCoefficient(double e_x, double AngleOfLoad)
        {

           double P_n= this.FindUltimateEccentricForce(e_x , AngleOfLoad);
            //Bring the coefficient that is in the format of the AISC manual

            //adjust to 1/6 in leg size.
            double ReductionFactor = (1.0/16.0)/this.leg;

            //Divide by length
            double C = P_n*ReductionFactor/l_vertical;
            return C;
        }
    }
}
