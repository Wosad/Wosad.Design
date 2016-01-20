using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Entities;

namespace Wosad.Steel.AISC.AISC360_10.Composite
{
    public partial class HeadedAnchor : AnalyticalElement
    {

      public double GetPlacementFactorR_p(HeadedAnchorDeckCondition HeadedAnchorDeckCondition,HeadedAnchorWeldCase HeadedAnchorWeldCase,double e_mid_ht)
        {
            double R_p;

            if (HeadedAnchorWeldCase == AISC.HeadedAnchorWeldCase.WeldedDirectly)
            {
                //(1a) steel headed stud anchors welded directly to the steel shape;
                R_p = 0.75;
            }
            else
            {
                if (HeadedAnchorDeckCondition == AISC.HeadedAnchorDeckCondition.Parallel)
                {
                    //(1c) steel headed stud anchors welded through steel deck, or steel sheet
                    //used as girder filler material, and embedded in a composite slab with
                    //the deck oriented parallel to the beam
                    R_p = 0.75;
                }
                else if (HeadedAnchorDeckCondition == AISC.HeadedAnchorDeckCondition.Perpendicular)
                {
                    if (e_mid_ht>=2)
                    {
                            //(1b) steel headed stud anchors welded in a composite slab with the deck
                            //oriented perpendicular to the beam and emid-ht ≥ 2 in.; 
                        R_p = 0.75;
                    }
                    else
                    {
                        //(3) for steel headed stud anchors welded in a composite slab with deck
                        //oriented perpendicular to the beam and emid-ht < 2 in
                        R_p = 0.6;
                    }
                }
                else //No decking
                {
                    R_p = 0.75;
                }
            }
            return R_p;
        }
    }
}
