using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC.AISC360_10.Connections.Bolted;
using Wosad.Steel.AISC.Interfaces;

namespace Wosad.Steel.AISC.AISC360_10.Connections.Bolted
{
    public  class BoltFactory
    {
        string MaterialId;
        public BoltFactory(string MaterialId)
        {
            this.MaterialId = MaterialId;
        }
        public IBoltMaterial GetBoltMaterial()
        {
            IBoltMaterial m =null;
            switch (MaterialId)
            {
                //case "A108": m = new ThreadedBoltMaterial(65.0); break;
                case "A325": m=new BoltGroupAMaterial(); break;
                case "A490": m=new BoltGroupBMaterial(); break;
                case "F1852":m=new BoltGroupAMaterial();  break;
                //case "A36": m = new ThreadedBoltMaterial(58.0); break;
                //case "A193 Grade B7": m = new ThreadedBoltMaterial(100.0); break; //Can use higher value, up to 125 ksi for smaller diameters
                case "A307": m=new BoltA307Material();  break;
                case "A354 Grade BC": m = new BoltGroupAMaterial(); break;  //This is per AISC spec. Design guide 1 gives higher values
                case "A354 Grade BD": m = new BoltGroupBMaterial(); break;  //This is per AISC spec. Design guide 1 gives higher values
                case "A449": m=new BoltGroupAMaterial();break;
                //case "A572": break; //TODO: eliminate this from material selection node
                //case "A588": m = new ThreadedBoltMaterial(70.0); break; // for large diameters (over 4") this is unconservative.
                //case "A687": m = new ThreadedBoltMaterial(150.0); break;  //AISC indicates 150ksi MAX
                case "F1554 Grade 105": m = new ThreadedBoltMaterial(125.0); break;  //Design guide 1 Table 2.2
                case "F1554 Grade 55": m = new ThreadedBoltMaterial(75.0); break;    //Design guide 1 Table 2.2
                case "F1554 Grade 36": m = new ThreadedBoltMaterial(58.0); break;    //Design guide 1 Table 2.2
                //case "A572 Grade 42": m = new ThreadedBoltMaterial(60.0); break;     //AISC Manual Table 2-6
                //case "A572 Grade 50": m = new ThreadedBoltMaterial(65.0); break;     //AISC Manual Table 2-6
                //case "A572 Grade 55": m = new ThreadedBoltMaterial(70.0); break;     //AISC Manual Table 2-6
                //case "A572 Grade 60": m = new ThreadedBoltMaterial(75.0); break;     //AISC Manual Table 2-6
                //case "A572 Grade 65": m = new ThreadedBoltMaterial(80.0); break;     //AISC Manual Table 2-6
                default: throw new Exception("Unrecognized bolt material. Check input");

            }
                return m;
        }

        public IBoltBearing GetBearingBolt(double Diameter, BoltThreadCase ThreadType)
        {
            IBoltMaterial bm = null;
            IBoltBearing bb = null;
            switch (MaterialId)
            {
                case "A325": bb = new BoltBearingGroupA(Diameter, ThreadType); break;
                case "A490": bb = new BoltBearingGroupB(Diameter, ThreadType); break;
                case "F1852": bb = new BoltBearingGroupA(Diameter, ThreadType); break;
                case "A307": bm = new BoltA307Material(); bb=new BoltBearingThreadedGeneral(Diameter, ThreadType,bm); break;
                case "A354 Grade BC": bb = new BoltBearingGroupA(Diameter, ThreadType); break;  
                case "A354 Grade BD": bb = new BoltBearingGroupB(Diameter, ThreadType); break;  
                case "A449": bb = new BoltBearingGroupA(Diameter, ThreadType); break;
                case "F1554 Grade 105": bm = new ThreadedBoltMaterial(125.0); bb = new BoltBearingThreadedGeneral(Diameter, ThreadType, bm); break; 
                case "F1554 Grade 55": bm = new ThreadedBoltMaterial(75.0); bb = new BoltBearingThreadedGeneral(Diameter, ThreadType, bm); break;   
                case "F1554 Grade 36": bm = new ThreadedBoltMaterial(58.0); bb = new BoltBearingThreadedGeneral(Diameter, ThreadType, bm); break;   
                default: throw new Exception("Unrecognized bolt material. Check input");

            }
            return bb;
        }

        public IBoltBearing GetBearingBolt(double Diameter, string ThreadType)
        {
            BoltThreadCase Thread;
            if (ThreadType=="X")
            {
                Thread = BoltThreadCase.Excluded;
            }
            else
            {
                Thread = BoltThreadCase.Included;
            }
            return this.GetBearingBolt(Diameter, Thread);
        }
    }
}
