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
using System.Threading.Tasks;
using Wosad.Common.Mathematics;
using Wosad.Common.Section.Interfaces;

namespace Wosad.Common.Section
{
    /// <summary>
    /// Shape comprised of full-width rectangles,
    /// providing default implementation of ISliceableSection
    /// for calculation of location of PNA (Plastic Neutral Axis)
    /// and Plastic section Modulus Zx
    /// </summary>
    public abstract partial class CompoundShape : ISliceableSection, ISection //SectionBaseClass, 
    {
        bool basicPropertiesCalculated;


        /// <summary>
        /// Abstract method to get a list of rectangles for plastic section modulus analysis.
        /// These rectangles are used for plastic analysis with respect to X-axis.
        /// Correct definition of rectangles must be such that each rectangle 
        /// occupies FULL WIDTH of section.
        /// </summary>
        /// <returns>List of CompoundShapePart rectangles for this shape </returns>
        public abstract List<CompoundShapePart> GetCompoundRectangleXAxisList();

        private  List<CompoundShapePart> rectanglesXAxis;

        public  List<CompoundShapePart> RectanglesXAxis
        {
            get {
                if (rectanglesXAxis == null)
                {
                    rectanglesXAxis = GetCompoundRectangleXAxisList();
                }
                return rectanglesXAxis; }
            set { rectanglesXAxis = value; }
        }

        /// <summary>
        /// Abstract method to get a list of rectangles for plastic section modulus analysis.
        /// These rectangles are used for plastic analysis with respect to Y-axis.
        /// Correct definition of rectangles must be such that each rectangle 
        /// occupies FULL HEIGHT of section.
        /// </summary>
        /// <returns>List of CompoundShapePart rectangles for this shape </returns>
        public abstract List<CompoundShapePart> GetCompoundRectangleYAxisList();

        private List<CompoundShapePart> rectanglesYAxis;

        public List<CompoundShapePart> RectanglesYAxis
        {
            get
            {
                if (rectanglesYAxis == null)
                {
                    rectanglesYAxis = GetCompoundRectangleYAxisList();
                }
                return rectanglesYAxis;
            }
            set { rectanglesYAxis = value; }
        }

        private Point2D centroid;

        public Point2D Centroid
        {
            get {
                if (basicPropertiesCalculated == false)
                {
                    CalculateBasicProperties();
                }
                
                return centroid; }
            set { centroid = value; }
        }
        
        public CompoundShape(): this(null)
        {
           
        }

        public CompoundShape(string Name)
        {
            basicPropertiesCalculated = false;
            momentsOfInertiaCalculated = false;
            areaCalculated = false;
            torsionConstantCalculated   =false;
            warpingConstantCalculated   =false;
            elasticPropertiesCalculated = false;
            plasticPropertiesCalculated = false;
            this.Name = Name;

        }


        /// <summary>
        /// Gets a slice of the section for further analysis.
        /// </summary>
        /// <param name="PlaneOffset">Datum (top fiber) offset.</param>
        /// <param name="OffsetType">Indicates which part of section after slicing to return</param>
        /// <returns></returns>
        public IMoveableSection GetTopSliceSection(double PlaneOffset, SlicingPlaneOffsetType OffsetType)
        {

            
            double currentCentroidY=this.YMax;       //offset from datum (top)
            double currentMinY     =this.YMax;       //offset from datum (top)
            double currentMaxY     =this.YMax;       //offset from datum (top)

            double slicePlaneY = GetSlicingPlaneY(PlaneOffset,OffsetType);

            List<CompoundShapePart> newRectangles = new List<CompoundShapePart>(); 

            for (int i = 0; i < RectanglesXAxis.Count(); i++)
            {
                currentMinY = currentMaxY;
                currentMaxY = currentMaxY + RectanglesXAxis[i].h;
                currentCentroidY = (currentMinY + currentMaxY) / 2.0;

                if (currentMinY >= slicePlaneY)
                {
                    newRectangles.Add(RectanglesXAxis[i]);
                }

                else
	            {
                    if (currentMinY <= slicePlaneY && currentMaxY >= slicePlaneY)
                    {
                        newRectangles.Add(
                            new CompoundShapePart(
                                RectanglesXAxis[i].b, currentMaxY-slicePlaneY, 
                                new Point2D(RectanglesXAxis[i].InsertionPoint.X, currentCentroidY)));
                    }
	            }

            }

            ArbitraryCompoundShape shape = new ArbitraryCompoundShape(newRectangles, null);
            return shape;
        }



        public IMoveableSection GetBottomSliceSection(double PlaneOffset, SlicingPlaneOffsetType OffsetType)
        {


            double currentCentroidY = this.YMax;     //offset from datum (top)
            double currentMinY = this.YMax;       //offset from datum (top)
            double currentMaxY = this.YMax;       //offset from datum (top)

            double slicePlaneY = GetSlicingPlaneY(PlaneOffset, OffsetType);

            List<CompoundShapePart> newRectangles = new List<CompoundShapePart>();

            for (int i = 0; i < RectanglesXAxis.Count(); i++)
            {
                currentMinY = currentMaxY;
                currentMaxY = currentMaxY + RectanglesXAxis[i].h;
                currentCentroidY = (currentMinY + currentMaxY) / 2.0;

                if (currentMaxY <= slicePlaneY)
                {
                    newRectangles.Add(RectanglesXAxis[i]);
                }

                else
                {
                    if (currentMinY <= slicePlaneY && currentMaxY >= slicePlaneY)
                    {
                        newRectangles.Add(
                            new CompoundShapePart(
                                RectanglesXAxis[i].b, slicePlaneY - currentMinY,
                                new Point2D(RectanglesXAxis[i].InsertionPoint.X, currentCentroidY)));
                    }
                }

            }

            ArbitraryCompoundShape shape = new ArbitraryCompoundShape(newRectangles, null);
            return shape;

        }

        /// <summary>
        /// Returns a top slice of section having specified area.
        /// </summary>
        /// <param name="Area"></param>
        /// <returns></returns>
        public IMoveableSection GetTopSliceOfArea(double Area)
        {
            return getSliceOfArea(Area, SLiceType.Top);
 
        }

        /// <summary>
        /// Returns a top slice of section having specified area.
        /// </summary>
        /// <param name="Area"></param>
        /// <returns></returns>
        public IMoveableSection GetBottomSliceOfArea(double Area)
        {
            return getSliceOfArea(Area, SLiceType.Bottom);
        }

        //variables used to store iteration data for finding a slice of a given area;
        double targetArea;
        IMoveableSection cutSection;

        /// <summary>
        /// Iterates the section until a top or bottom slice (as specified) 
        /// is of requested area.
        /// </summary>
        /// <param name="Area"></param>
        /// <param name="sliceType"></param>
        /// <returns></returns>
        private IMoveableSection getSliceOfArea(double Area, SLiceType sliceType)
        {
            double ConvergenceTolerance = this.A * 0.0001;
            double targetAreaDelta = 0.0;
            double AxisLocationDistanceMin = 0.0;
            double AxisLocationDistanceMax = this.YMax - this.YMin;

            //Iterate until the slice area is as required
            targetArea = Area;
            if (sliceType == SLiceType.Bottom)
            {

                double SliceAxisOffsetFromTop = RootFinding.Brent(new FunctionOfOneVariable(BottomAreaDeltaCalculationFunction),
                    AxisLocationDistanceMin, AxisLocationDistanceMax,
                    ConvergenceTolerance, targetAreaDelta);
            }
            else
            {
                double SliceAxisOffsetFromTop = RootFinding.Brent(new FunctionOfOneVariable(TopAreaDeltaCalculationFunction),
                    AxisLocationDistanceMin, AxisLocationDistanceMax,
                    ConvergenceTolerance, targetAreaDelta);
            }
            return cutSection; //the section was stored during the iteration
        }

        /// <summary>
        /// Calculates Y coordinate of slicing plane
        /// </summary>
        /// <param name="OffsetType"> The point from which the offset is measured.</param>
        /// <returns></returns>
        private double GetSlicingPlaneY(double PlaneOffset, SlicingPlaneOffsetType OffsetType)
        {
            double slicePlaneY = this.YMax;
            switch (OffsetType)
            {
                case SlicingPlaneOffsetType.Top:
                    slicePlaneY = this.YMax - PlaneOffset;
                    break;
                case SlicingPlaneOffsetType.Centroid:
                    slicePlaneY = this.Centroid.Y - PlaneOffset;
                    break;
                case SlicingPlaneOffsetType.Bottom:
                    slicePlaneY = this.YMin - PlaneOffset;
                    break;
            }
            return slicePlaneY;
        }

        private double TopAreaDeltaCalculationFunction(double SliceAxisY)
        {
            cutSection = this.GetTopSliceSection(SliceAxisY, SlicingPlaneOffsetType.Top);
            double SliceArea = cutSection.A;
            return targetArea - SliceArea;
        }

        private double BottomAreaDeltaCalculationFunction(double SliceAxisY)
        {
            cutSection = this.GetBottomSliceSection(SliceAxisY, SlicingPlaneOffsetType.Top);
            double SliceArea = cutSection.A;
            return targetArea - SliceArea;
        }

        Point2D plasticCentroid;
        public Point2D PlasticCentroidCoordinate
        {
            get
            {
                if (plasticPropertiesCalculated == false)
                {
                    CalculatePlasticProperties();
                }
                return plasticCentroid;
            }
        }

        public Point2D GetElasticCentroidCoordinate()
        {
            if (basicPropertiesCalculated == false)
            {
                CalculateBasicProperties();
            }
            return Centroid;
        }

        double _YMax;
        public double YMax
        {
            get {
                if (basicPropertiesCalculated == false)
                {
                    CalculateBasicProperties();
                }
                return _YMax;
            }
        }

        double _YMin;
        public double YMin
        {
            get {
                if (basicPropertiesCalculated == false)
                {
                    CalculateBasicProperties();
                }
                return _YMin;
            }
        }

        double _XMax;
        public double XMax
        {
            get {
                if (basicPropertiesCalculated == false)
                {
                    CalculateBasicProperties();
                }
                return _XMax;
            }
        }

        double _XMin;
        public double XMin
        {
            get {
                if (basicPropertiesCalculated == false)
                {
                    CalculateBasicProperties();
                }
                return _XMin;
            }
        }

        private void CalculateBasicProperties()
        {
            CalculateMinAndMaxCoordinates();
            CalculateCentroid();
            basicPropertiesCalculated = true;
        }

        private void CalculateCentroid()
        {

            double sumOfAreasX=0;
            double sumOfAreasY = 0;
            double sumOfAreaTimesY=0;
            double sumOfAreaTimesX=0;

            foreach (var r in RectanglesXAxis)
            {

                double thisArea = r.b * r.h;
                sumOfAreasX += thisArea;
                sumOfAreaTimesY +=thisArea*r.Centroid.Y;
                
            }
            double cY = sumOfAreaTimesY / sumOfAreasX;

            sumOfAreasY = 0;
            foreach (var r in RectanglesYAxis)
            {
                double thisArea = r.b * r.h;
                sumOfAreasY += thisArea;
                //we still use Y because the shape is symmetrical
                sumOfAreaTimesX += thisArea * r.Centroid.Y;

            }
            double cX = sumOfAreaTimesX / sumOfAreasY;

            Centroid = new Point2D(cX, cY);

        }

        private void CalculateMinAndMaxCoordinates()
        {
            double MinXtemp = double.PositiveInfinity;
            double MaxXtemp = double.NegativeInfinity;
            double MinYtemp = double.PositiveInfinity;
            double MaxYtemp = double.NegativeInfinity;

            foreach (var r in RectanglesXAxis)
            {
                //this rectangle properties
                double thisMinX = r.Centroid.X-r.b/2.0;
                double thisMaxX = r.Centroid.X+r.b/2.0;
                double thisMinY = r.Centroid.Y-r.h/2.0;
                double thisMaxY = r.Centroid.Y+r.h/2.0;

                MinXtemp = thisMinX < MinXtemp ? thisMinX : MinXtemp;
                MaxXtemp = thisMaxX > MaxXtemp ? thisMaxX : MaxXtemp;
                MinYtemp = thisMinY < MinYtemp ? thisMinY : MinYtemp;
                MaxYtemp = thisMaxY > MaxYtemp ? thisMaxY : MaxYtemp;
            }
            this._XMin = MinXtemp;
            this._XMax = MaxXtemp;
            this._YMin = MinYtemp;
            this._YMax = MaxYtemp;
        }

        enum SLiceType
        {
            Top,
            Bottom
        }
    }

}
