//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Wosad.Common.Section.General
//{
//    //this class stores information about a slice of section that is 
//    //used by  ISliceableSection interface
//    // this allows the use of simplified approach to when ISliceableSection functionality
//    //is required but the section cannot be easily described analytically

//    public class SectionSliceRectangular: ISectionSlice
//    {
//        public SectionSliceRectangular(double Width, double MinY, double MaxY)
//        {
//            this.Width = Width;
//            this.MinY = MinY;
//            this.MaxY = MaxY;
//        }
//        public double MinY { get; set; }
//        public double MaxY { get; set; }
//        public double Width { get; set; }

//        public double GetCentroidY()
//        {
//            return (MaxY - MinY) / 2.0;
//        }

//        public double GetArea()
//        {
//            return (MaxY - MinY) * Width;
//        }
//    }
//}
