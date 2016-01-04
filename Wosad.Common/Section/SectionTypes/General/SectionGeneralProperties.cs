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
using Wosad.Common.Section.Interfaces;

namespace Wosad.Common.Section.General
{
    public class SectionGeneralProperties: ISection
    {

        double _Area;
        double _MomentOfInertiaX;
        double _MomentOfInertiaY;
        double _MomentOfInertiaTorsional;
        double _SectionModulusXTop;
        double _SectionModulusXBot;
        double _SectionModulusYLeft;
        double _SectionModulusYRight;
        double _PlasticSectionModulusX;
        double _PlasticSectionModulusY;
        double _RadiusOfGyrationX;
        double _RadiusOfGyrationY;
        double _CentroidXtoLeftEdge;
        double _CentroidYtoBottomEdge;
        double _PlasticCentroidXtoLeftEdge;
        double _PlasticCentroidYtoBottomEdge;
        double _WarpingConstant;
        double _TorsionalConstant;

        public SectionGeneralProperties(string Name, double Area, double MomentOfInertiaX, double MomentOfInertiaY, double MomentOfInertiaTorsional, double SectionModulusXTop, double SectionModulusXBot, double SectionModulusYLeft, double SectionModulusYRight, double PlasticSectionModulusX, double PlasticSectionModulusY, double RadiusOfGyrationX, double RadiusOfGyrationY, double CentroidXtoLeftEdge, double CentroidYtoBottomEdge, double PlasticCentroidXtoLeftEdge, double PlasticCentroidYtoBottomEdge, double WarpingConstant, double TorsionalConstant)
        {
            //this.Name = Name;
            this._Area = Area;
            this._MomentOfInertiaX = MomentOfInertiaX;
            this._MomentOfInertiaY = MomentOfInertiaY;
            this._MomentOfInertiaTorsional = MomentOfInertiaTorsional;
            this._SectionModulusXTop = SectionModulusXTop;
            this._SectionModulusXBot = SectionModulusXBot;
            this._SectionModulusYLeft = SectionModulusYLeft;
            this._SectionModulusYRight = SectionModulusYRight;
            this._PlasticSectionModulusX = PlasticSectionModulusX;
            this._PlasticSectionModulusY = PlasticSectionModulusY;
            this._RadiusOfGyrationX = RadiusOfGyrationX;
            this._RadiusOfGyrationY = RadiusOfGyrationY;
            this._CentroidXtoLeftEdge = CentroidXtoLeftEdge;
            this._CentroidYtoBottomEdge = CentroidYtoBottomEdge;
            this._PlasticCentroidXtoLeftEdge = PlasticCentroidXtoLeftEdge;
            this._PlasticCentroidYtoBottomEdge = PlasticCentroidYtoBottomEdge;
            this._WarpingConstant = WarpingConstant;
            this._TorsionalConstant = TorsionalConstant;
        }
        string name;
        public string Name
        {
            get { return name; }
        }

        public double A
        {
            get { return _Area; }
        }

        public double I_x
        {
            get { return _MomentOfInertiaX; }
        }

        public double I_y
        {
            get { return _MomentOfInertiaY; }
        }

        public double MomentOfInertiaTorsional
        {
            get { return _MomentOfInertiaTorsional; }
        }

        public double S_xTop
        {
            get { return _SectionModulusXTop; }
        }

        public double S_xBot
        {
            get { return _SectionModulusXBot; }
        }

        public double S_yLeft
        {
            get {return _SectionModulusYLeft;  }
        }

        public double S_yRight
        {
            get { return _SectionModulusYRight; }
        }

        public double Z_x
        {
            get { return _PlasticSectionModulusX; }
        }

        public double Z_y
        {
            get { return _PlasticSectionModulusY; }
        }

        public double r_x
        {
            get { return _RadiusOfGyrationX; }
        }

        public double r_y
        {
            get { return _RadiusOfGyrationY; }
        }

        public double x_Bar
        {
            get { return _CentroidXtoLeftEdge; }
        }

        public double y_Bar
        {
            get { return _CentroidYtoBottomEdge; }
        }

        public double x_pBar
        {
            get { return _PlasticCentroidXtoLeftEdge; }
        }

        public double y_pBar
        {
            get {return _PlasticCentroidYtoBottomEdge;  }
        }

        public double C_w
        {
            get { return _WarpingConstant; }
        }

        public double J
        {
            get { return _TorsionalConstant; }
        }

        public ISection Clone()
        {
            throw new NotImplementedException();
        }
    }
}
