﻿//using System.Collections.Generic;
//using NUnit.Framework;
//using Wosad.Analytics.ACI318_14.Tests.Prestressed;
//using Wosad.Concrete.ACI;
//using Wosad.Concrete.ACI.Infrastructure.Entities.Rebar;
//using Wosad.Concrete.ACI318_14;
//using Wosad.Common.Mathematics;



//namespace Wosad.Analytics.ACI318_14.Tests.Prestressed
//{
//    [TestFixture]
//    public class PrestressedGeneralTestsBase : PrestressedBeamTestBase
//    {

//        protected PrestressedConcreteSection GetGeneralPrestressedConcreteBeam(double fc,
//         double fci, double RebarCentroidYCoordinate, params RebarPrestressed[] rebarInput)
//        {
//            IPrestressedConcreteSection Section = null;

//            List<RebarPoint> LongitudinalBars = new List<RebarPoint>();
//            foreach (var bar in rebarInput)
//            {
//                RebarPoint point = new RebarPoint(bar, new RebarCoordinate() { X = 0, Y = RebarCentroidYCoordinate });
//                LongitudinalBars.Add(point);
//            }

//            PrestressedConcreteSection beam = new PrestressedConcreteSection
//                (Section, LongitudinalBars,
//              CrossSectionLocationType.EndOfSImplySupported, MemberClass.U, Log);
//            return beam;
//        }



//        protected IPrestressedConcreteSection GetGeneralPrestressedSection(double fc, double fci)
//        {
//            IPrestressedConcreteMaterial mat = GetPrestressedConcreteMaterial(fc, fci);
            
//            PrestressedConcreteSectionGeneral section = new PrestressedConcreteSectionGeneral(mat, null, null, 0,0);
//            return section;

//        }
//    }
//}
