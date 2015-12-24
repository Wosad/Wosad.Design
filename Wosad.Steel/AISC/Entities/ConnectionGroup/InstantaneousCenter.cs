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
using Wosad.Common.Interfaces;
using Wosad.Common.Mathematics;

namespace Wosad.Steel.AISC.SteelEntities
{
    public abstract partial class ConnectionGroup
    {

        public ConnectionGroup()
        {

        }
        public ConnectionGroup(List<ILocationArrayElement> Elements)
        {
            this.Elements = Elements;
        }
        /// <summary>
        /// Finds bolt group ultimate strength coefficient C
        /// using instantaneous center of rotation method
        /// </summary>
        /// <param name="Eccentricity"></param>
        /// <param name="AngleOfLoad"></param>
        /// <returns></returns>
        public double FindUltimateStrengthCoefficient(double Eccentricity , double AngleOfLoad)
        {
            bool iterationYieldedResult = false;
            int MaxNumberOfIterations = 1999;
            //iteration variables
            double P=0.0;
            double M=0.0; 
            double J=0.0;
            double Ry=0.0;
            double Rx=0.0;
            double Pprev = 0;
            double Mapfunc =0.0;

            double Rot = AngleOfLoad.ToRadians();
            double Ec = Eccentricity;
            int N = Elements.Count();
            
            //Initial assumption of bolt center
            Point2D Center = new Point2D(0, 0);
            int iterationCount = 0;
            while (iterationYieldedResult == false)
            {
               //double LiMax = FindLargestElementDistanceFromCenter(Center);
               ILocationArrayElement controllingElement = FindUltimateDeformationElement(Center); 

                //Iterate through all bolts and using force-displacement 
                //relationship find the force in each bolt
                //then find the contribution of this bolt to overall
                //Force and Moment of the group.
                M  =0;
                Ry =0;
                Rx =0;
                J = 0;

                      foreach (var el in elements)
	                    {
                            // adjust coordinate
                            double xi = el.Location.X - Center.X;  //delta X
                            double yi = el.Location.Y - Center.Y;  //delta Y
                            double ri =Math.Sqrt(xi*xi + yi*yi); //radial distance from center to this element
                            double Rn_i = GetElementForce(el, Center, controllingElement, AngleOfLoad);
                            //Contribution of this bolt to the overall response of the group
                            M = M + (Rn_i) * ri ;                      //Moment
                            Vector2d ForceUnitVector = GetElementForceUnitVector(el, Center);
                            Ry = Ry + Rn_i * ForceUnitVector.Y;              //Vertical Force
                            Rx = Rx + Rn_i * ForceUnitVector.X;              //Horizontal Force

                            J = J + Math.Pow(ri , 2);
                        }
                            //Define a force vector due to unit force
                            Vector2d UnitForce = new Vector2d(Math.Sin(Rot), Math.Cos(Rot));
                            Vector2d PositionVector = new Vector2d(Ec - Center.X, -Center.Y);
                            double d = UnitForce.FindDistance(PositionVector);
                            P = M / d;   //resolving the internal forces into a resultant at the given moment arm
                
                        double Py = P * Math.Cos(Rot);
                        double Px = P * Math.Sin(Rot);

                        double Fxx = Px - Rx;       //Horizontal Unbalanced Force
                        double Fyy = Py - Ry;       //Vertical Unbalanced Force

                        double Pp = Math.Sqrt(Fyy*Fyy+ Fxx*Fxx); //Unbalanced force resultant

                bool CovergedCriteria1 = Math.Abs(Fyy) <= 0.0001 && Math.Abs(Fxx) <= 0.0001 ? true : false; //reached convergence tolerance
                bool CovergedCriteria2 = (iterationCount > MaxNumberOfIterations)? true :false;   
                bool CovergedCriteria3 = iterationCount > 200 && Pp > Pprev ? true : false;  
                
                if (CovergedCriteria1 == true)
	                {
		            break;
	                }
                    else
	                {
                        if (CovergedCriteria2==true)
	                    {
		                     throw new Exception("Failed to find solution for bolt group coefficient. Maximum number of iterations in bolt group has been exceeded.");
	                    }
                        else
	                    {
                            if (CovergedCriteria3 == true)
                            {
                                break; 
                            }
                            
                            //throw new Exception(String.Format("Failed to find solution for bolt group coefficient.The resultant force increment increased after iteration {0}.", iterationCount ));
	                    }
	                }

                //if the solution hasn't converged update values for next iteration
       
                Pprev = Pp  ;                                         
                Mapfunc = J / (N * M);                        //Mapping function
                Center.X = Center.X - Fyy * Mapfunc;          //I.C. location on x-axis from bolt group C.G.
                Center.Y = Center.Y + Fxx * Mapfunc;          //I.C. location on y-axis from bolt group C.G.

           iterationCount++;
            }
            #region Final run through elelents (for debug only)
            foreach (var el in elements)
            {
                ILocationArrayElement controllingElement = FindUltimateDeformationElement(Center);
                double xi = el.Location.X + Center.X;
                double yi = el.Location.Y + Center.Y;
                double ri = Math.Sqrt(xi * xi + yi * yi);
                double iRn = GetElementForce(el, Center, controllingElement, AngleOfLoad);
            } 
            #endregion
            return P;
        }

        protected abstract ILocationArrayElement FindUltimateDeformationElement(Point2D Center);




        protected abstract List<ILocationArrayElement> GetICElements();
        


        /// <summary>
        /// Gets individual  element force based on the response of the entire group
        /// </summary>
        /// <param name="element"> Bolt or weld element point</param>
        /// <param name="Center">Instantaneous Center of rotation</param>
        /// <param name="ControllingElement">Element in group the having the governing deformation.</param>
        /// <param name="angle">Angle of force application, measured from Y-axis</param>
        /// <returns></returns>
        protected abstract double GetElementForce(ILocationArrayElement element, Point2D Center,
            ILocationArrayElement ControllingElement, double angle);


        private List<ILocationArrayElement> elements;

        /// <summary>
        /// Elements in the group (bolts or weld segments)
        /// </summary>
        public List<ILocationArrayElement> Elements
        {
            get
            {
                if (elements == null)
                {
                    elements = GetICElements();
                }
                return elements;
            }
            set { elements = value; ElasticPropertiesWerCalculated = false; }
        }


        /// <summary>
        /// Returns a unit vector of the resultant force in the element. Which is perpendicular to
        /// the ray from center of rotation to the element.
        /// </summary>
        /// <param name="e">Element</param>
        /// <param name="c">Center of rotation</param>
        /// <returns></returns>
        protected Vector2d GetElementForceUnitVector(ILocationArrayElement e, Point2D c)
        {
            double BX = e.Location.X - c.X;
            double BY = e.Location.Y - c.Y;

            //Normal vector:
            // cross product definition
            //AXB=(AyBz -AzBy)i -(AxBz -AzBx)j +(AxBy -AyBx)k
            //assuming AZ = 1, AX =AY =0;

            Vector2d perpVector= new Vector2d(-BY, BX);
            return perpVector.GetUnit();
        }

    }
}
