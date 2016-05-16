using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC.Entities.Enums.FloorVibrations;

namespace Wosad.Steel.AISC.Entities.FloorVibrations
{
    public class FloorVibrationBeamGirderPanel
    {

        public double GetEffectiveWeight(double q_s, double D_s, double L_floor, double B_floor,
            double L_j, double S_joist, double D_j,  double Delta_j,
            double L_g, double D_g, double Delta_g,
            double L_jAdjacent =0, 
            BeamFloorLocationType JoistLocationType = BeamFloorLocationType.Inner,
            BeamFloorLocationType GirderLocationType = BeamFloorLocationType.Inner, 
            BeamGirderPlacement ConnectionType = BeamGirderPlacement.ConnectionToWeb,
            AdjacentSpanWeightIncreaseType AdjacentSpanWeightIncreaseType = AdjacentSpanWeightIncreaseType.None)
        {

            double L_jAverage = (L_j + L_jAdjacent) / 2.0;
            throw new NotImplementedException();
            double B_j = GetEffectiveJoistWidth_B_j(D_s, D_j, JoistLocationType, L_floor);
            double B_g = GetEffectiveGirderWidth_B_g(L_g, B_floor, L_j, D_j, D_g, GirderLocationType, ConnectionType);


            double w_j=q_s;
            double w_g= q_s;

            //Joist mode weight
            double W_j=((w_j) / (S_joist))*B_j*L_j;

            double JoistWeightIncrease = 1.0;
            switch (AdjacentSpanWeightIncreaseType)
            {
                case AdjacentSpanWeightIncreaseType.HotRolledBeamOverTheColumn:
                    JoistWeightIncrease = 1.5;
                    break;
                case AdjacentSpanWeightIncreaseType.JoistWithExtendedBottomChord:
                    JoistWeightIncrease = 1.3;
                    break;
                case AdjacentSpanWeightIncreaseType.None:
                    JoistWeightIncrease = 1.0;
                    break;
            }

            W_j = W_j * JoistWeightIncrease;

            double W_g = (((w_g) / (L_jAverage))) * B_g * L_g;

            double W=((Delta_j) / (Delta_j+Delta_g))*W_j+((Delta_g) / (Delta_j+Delta_g))*W_g;

            return W;
        }

        /// <summary>
        /// Returns B_j: effecive panel width for joist mode
        /// </summary>
        /// <param name="D_s">Distributed slab moment of inertia</param>
        /// <param name="D_j">Distributed joist moment of inertia</param>
        /// <param name="beamType"> Beam type (inner or edge)</param>
        /// <param name="L_floor"> Length of floor for panel (use pebble rule to determine L_floor)</param>
        /// <returns></returns>
        double GetEffectiveJoistWidth_B_j(double D_s, double D_j,
            BeamFloorLocationType beamType, double L_floor )
        {
 
            double C_j = 2.0;

            if (beamType== BeamFloorLocationType.AtFreeEdge)
            {
                C_j = 1.0;
            }

            double B_j = C_j * Math.Pow((((D_s) / (D_j))), 1/4.0);

            if (B_j > 2.0/3.0*L_floor)
            {
                return L_floor;
            }
            else
            {
                return B_j;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="I_j">Joist moment of inertia</param>
        /// <param name="S_j">Joist spacing</param>
        /// <returns></returns>
        public double GetDistributedJoistMomentOfInertia(double I_j, double S_j)
        {
            double D_j = I_j / S_j;
            return D_j;
        }


        public double GetDistributedGirderMomentOfInertia(double I_g, double L_jAverage)
        {
            double D_g = I_g / L_jAverage;
            return D_g;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d_e">Slab effective thickness</param>
        /// <param name="n"> Modular ratio (include dynamic increase)</param>
        /// <returns></returns>
        public double GetDistributedSlabMomentOfInertia(double n, double d_e)
        {
            double D_s = (((12.0) / (n))) * (((Math.Pow(d_e, 3)) / (12.0)));
            return D_s;
        }

        double GetEffectiveGirderWidth_B_g(double L_g, double B_floor, double L_joist,
            double D_j, double D_g, BeamFloorLocationType beamType, 
            BeamGirderPlacement ConnectionType)
        {
            double C_g = 1.8;
            if (ConnectionType == BeamGirderPlacement.PlacementAtTopFlange)
            {
                C_g = 1.8;
            }

            double B_g = C_g * Math.Pow((((D_j) / (D_g))),1/ 4.0) * L_g;
            if (beamType == BeamFloorLocationType.Inner)
            {
                return Math.Min(B_g, 2.0 / 3.0 * B_floor);
            }
            else
            {
                return Math.Min(B_g, 2.0 / 3.0 * L_joist);
            }
          }

        public double GetAccelerationLimit(string FloorVibrationOccupancyId)
        {
            throw new NotImplementedException();
        }

        public double GetAccelerationRatio(double f_n, double W, double beta, string FloorVibrationOccupancyId )
        {
            double P_o = GetP_oFromOccupancyId(FloorVibrationOccupancyId);
            double apTo_g = P_o * Math.Exp((-0.35 * f_n) / (beta * W));
            return apTo_g;
        }

        private double GetP_oFromOccupancyId(string FloorVibrationOccupancyId)
        {
            throw new NotImplementedException();
        }

        public double GetCombinedModeFundamentalFrequency(double Delta_j, double Delta_g)
        {
            double g =386.0;
           double f_n=0.18*Math.Sqrt(((g) / (Delta_j+Delta_g)));
            return f_n;
        }

        public double GetFloorModalDampingRatio(string FloorVibrationOccupancyId)
        {
            throw new NotImplementedException();
        }
    }
}
