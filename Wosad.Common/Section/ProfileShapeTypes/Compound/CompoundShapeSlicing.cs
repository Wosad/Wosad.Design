using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Mathematics;
using Wosad.Common.Section.Interfaces;

namespace Wosad.Common.Section
{
    public abstract partial class CompoundShape : ISliceableSection, ISection 
    {
        public double GetSlicePlaneLocation(double Atar)
        {
            double SlicePlaneCoordinate = 0.0;
            double Sum_hi = 0;  //summation of height of previous rectangles
            double Sum_Ai = 0; //summation of areas of previous rectangles
            var sortedRectanglesX = RectanglesXAxis.OrderByDescending(r => r.InsertionPoint.Y).ToList();

            foreach (var r in sortedRectanglesX)
            {
                double bn = r.b;
                double hn = r.h;
                double hn_actual = r.h_a; //actual height used for fillet areas
                double yn = 0;
                
                double An = bn * hn;

                //distance from top of the rectangle to the PNA
                //this number is meaningful only for one rectangle
                double h_n_tilda = (Atar - Sum_Ai) / bn;
                double Y_n_tilda = 0;

                //check if this rectangle is the one where
                //slice plane is located
                if (h_n_tilda > 0 && h_n_tilda <= hn)
                {
                    //this condition is met only for one rectangle
                    Y_n_tilda = Sum_hi + h_n_tilda;
                    SlicePlaneCoordinate = Y_n_tilda - this.YMin; //PNA coordinate is meeasured from top
                }
                Sum_Ai += An;
                Sum_hi += hn_actual;
            }

            return SlicePlaneCoordinate;
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
            double Atar = 0; //target area
            double YPlane = 0;
            if (Area > this.A)
            {
                throw new Exception("Section analysis failed. Area of section sub-part exceeds the total area of cross section.");
            }
            else
            {
                if (sliceType == SLiceType.Top) // slicing happens from top 
                {
                    Atar = Area;
                }
                else
                {
                    Atar = this.A - Area;
                }
                YPlane = GetSlicePlaneLocation(Atar);
            }

            return getSliceAtCoordinate(YPlane, sliceType);
        }

        private IMoveableSection getSliceAtCoordinate(double YPlane, SLiceType sliceType)
        {

            ArbitraryCompoundShape newShape = new ArbitraryCompoundShape(null, null);
            if (sliceType == SLiceType.Top)
            {
                var sortedRectanglesX = RectanglesXAxis.OrderByDescending(r => r.InsertionPoint.Y).ToList();
                foreach (var r in sortedRectanglesX)
                {
                    if (r.Ymax > YPlane && r.Ymin >= YPlane)
                    {
                        newShape.rectanglesXAxis.Add(r);
                    }
                    else if (r.Ymax >= YPlane && r.Ymin <= YPlane)
                    {
                        double thisRectHeight = r.Ymax - YPlane;
                        newShape.rectanglesXAxis.Add(new CompoundShapePart(r.b, thisRectHeight, new Point2D(0, r.Ymax - thisRectHeight / 2)));
                    }
                    else
                    {
                        //do nothing since this rectangle does not belong here
                    }

                }
            }
            else
            {
                var sortedRectanglesX = RectanglesXAxis.OrderBy(r => r.InsertionPoint.Y).ToList();
                foreach (var r in sortedRectanglesX)
                {
                    if (r.Ymax < YPlane && r.Ymin <= YPlane)
                    {
                        newShape.rectanglesXAxis.Add(r);
                    }
                    else if (r.Ymax >= YPlane && r.Ymin <= YPlane)
                    {
                        double thisRectHeight =  YPlane -r.Ymin;
                        newShape.rectanglesXAxis.Add(new CompoundShapePart(r.b, thisRectHeight, new Point2D(0, r.Ymin + thisRectHeight / 2)));
                    }
                    else
                    {
                        //do nothing since this rectangle does not belong here
                    }

                }
            }

            return newShape;

            
        }


        public IMoveableSection GetTopSliceSection(double PlaneOffset, SlicingPlaneOffsetType OffsetType)
        {
            double YPlane = GetYPlane(PlaneOffset, OffsetType);
            return getSliceAtCoordinate(YPlane, SLiceType.Top);
        }

        public IMoveableSection GetBottomSliceSection(double PlaneOffset, SlicingPlaneOffsetType OffsetType)
        {
            double YPlane = GetYPlane(PlaneOffset, OffsetType);
            return getSliceAtCoordinate(YPlane, SLiceType.Bottom);
        }

        private double GetYPlane(double PlaneOffset, SlicingPlaneOffsetType OffsetType)
        {
            double YPlane = 0;

            switch (OffsetType)
            {
                case SlicingPlaneOffsetType.Top:
                    YPlane = YMax - PlaneOffset;
                    break;
                case SlicingPlaneOffsetType.Centroid:
                    YPlane = this.Centroid.Y + PlaneOffset;
                    break;
                case SlicingPlaneOffsetType.Bottom:
                    YPlane = YMin - PlaneOffset;
                    break;
                default:
                    break;
            }

            return YPlane;
        }

        ///// <summary>
        ///// Gets a slice of the section for further analysis.
        ///// </summary>
        ///// <param name="PlaneOffset">Datum (top fiber) offset.</param>
        ///// <param name="OffsetType">Indicates which part of section after slicing to return</param>
        ///// <returns></returns>
        //public IMoveableSection GetTopSliceSection(double PlaneOffset, SlicingPlaneOffsetType OffsetType)
        //{


        //    double currentCentroidY = this.YMax;       //offset from datum (top)
        //    double currentMinY = this.YMax;       //offset from datum (top)
        //    double currentMaxY = this.YMax;       //offset from datum (top)

        //    double slicePlaneY = GetSlicingPlaneY(PlaneOffset, OffsetType);

        //    List<CompoundShapePart> newRectangles = new List<CompoundShapePart>();

        //    for (int i = 0; i < RectanglesXAxis.Count(); i++)
        //    {
        //        currentMinY = currentMaxY;
        //        currentMaxY = currentMaxY + RectanglesXAxis[i].h;
        //        currentCentroidY = (currentMinY + currentMaxY) / 2.0;

        //        if (currentMinY >= slicePlaneY)
        //        {
        //            newRectangles.Add(RectanglesXAxis[i]);
        //        }

        //        else
        //        {
        //            if (currentMinY <= slicePlaneY && currentMaxY >= slicePlaneY)
        //            {
        //                newRectangles.Add(
        //                    new CompoundShapePart(
        //                        RectanglesXAxis[i].b, currentMaxY - slicePlaneY,
        //                        new Point2D(RectanglesXAxis[i].InsertionPoint.X, currentCentroidY)));
        //            }
        //        }

        //    }

        //    ArbitraryCompoundShape shape = new ArbitraryCompoundShape(newRectangles, null);
        //    return shape;
        //}

        #region Obsolete

        ///// <summary>
        ///// Iterates the section until a top or bottom slice (as specified) 
        ///// is of requested area.
        ///// </summary>
        ///// <param name="Area"></param>
        ///// <param name="sliceType"></param>
        ///// <returns></returns>
        //private IMoveableSection getSliceOfArea(double Area, SLiceType sliceType)
        //{
        //    double ConvergenceTolerance = this.A * 0.0001;
        //    double targetAreaDelta = 0.0;
        //    double AxisLocationDistanceMin = 0.0;
        //    double AxisLocationDistanceMax = this.YMax - this.YMin;

        //    //Iterate until the slice area is as required
        //    targetArea = Area;
        //    if (sliceType == SLiceType.Bottom)
        //    {

        //        double SliceAxisOffsetFromTop = RootFinding.Brent(new FunctionOfOneVariable(BottomAreaDeltaCalculationFunction),
        //            AxisLocationDistanceMin, AxisLocationDistanceMax,
        //            ConvergenceTolerance, targetAreaDelta);
        //    }
        //    else
        //    {
        //        double SliceAxisOffsetFromTop = RootFinding.Brent(new FunctionOfOneVariable(TopAreaDeltaCalculationFunction),
        //            AxisLocationDistanceMin, AxisLocationDistanceMax,
        //            ConvergenceTolerance, targetAreaDelta);
        //    }
        //    return cutSection; //the section was stored during the iteration
        //}

        //// <summary>
        //// Calculates Y coordinate of slicing plane
        //// </summary>
        //// <param name="OffsetType"> The point from which the offset is measured.</param>
        //// <returns></returns>
        ////private double GetSlicingPlaneY(double PlaneOffset, SlicingPlaneOffsetType OffsetType)
        ////{
        ////    double slicePlaneY = this.YMax;
        ////    switch (OffsetType)
        ////    {
        ////        case SlicingPlaneOffsetType.Top:
        ////            slicePlaneY = this.YMax - PlaneOffset;
        ////            break;
        ////        case SlicingPlaneOffsetType.Centroid:
        ////            slicePlaneY = this.Centroid.Y - PlaneOffset;
        ////            break;
        ////        case SlicingPlaneOffsetType.Bottom:
        ////            slicePlaneY = this.YMin - PlaneOffset;
        ////            break;
        ////    }
        ////    return slicePlaneY;
        ////}

        //private double TopAreaDeltaCalculationFunction(double SliceAxisY)
        //{
        //    cutSection = this.GetTopSliceSection(SliceAxisY, SlicingPlaneOffsetType.Top);
        //    double SliceArea = cutSection.A;
        //    return targetArea - SliceArea;
        //}

        //private double BottomAreaDeltaCalculationFunction(double SliceAxisY)
        //{
        //    cutSection = this.GetBottomSliceSection(SliceAxisY, SlicingPlaneOffsetType.Top);
        //    double SliceArea = cutSection.A;
        //    return targetArea - SliceArea;
        //}

        #endregion

        //public IMoveableSection GetBottomSliceSection(double PlaneOffset, SlicingPlaneOffsetType OffsetType)
        //{


        //    double currentCentroidY = this.YMax;     //offset from datum (top)
        //    double currentMinY = this.YMax;       //offset from datum (top)
        //    double currentMaxY = this.YMax;       //offset from datum (top)

        //    double slicePlaneY = GetSlicingPlaneY(PlaneOffset, OffsetType);

        //    List<CompoundShapePart> newRectangles = new List<CompoundShapePart>();

        //    for (int i = 0; i < RectanglesXAxis.Count(); i++)
        //    {
        //        currentMinY = currentMaxY;
        //        currentMaxY = currentMaxY + RectanglesXAxis[i].h;
        //        currentCentroidY = (currentMinY + currentMaxY) / 2.0;

        //        if (currentMaxY <= slicePlaneY)
        //        {
        //            newRectangles.Add(RectanglesXAxis[i]);
        //        }

        //        else
        //        {
        //            if (currentMinY <= slicePlaneY && currentMaxY >= slicePlaneY)
        //            {
        //                newRectangles.Add(
        //                    new CompoundShapePart(
        //                        RectanglesXAxis[i].b, slicePlaneY - currentMinY,
        //                        new Point2D(RectanglesXAxis[i].InsertionPoint.X, currentCentroidY)));
        //            }
        //        }

        //    }

        //    ArbitraryCompoundShape shape = new ArbitraryCompoundShape(newRectangles, null);
        //    return shape;

        //}



    }
}
