using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TG2_RFID
{

    public class Curve
    {
        /// <summary>
        /// Holds the curve's name.
        /// </summary>
        protected string curveName;

        /// <summary>
        /// Holds the curve's data separated as X, Y point.
        /// </summary>
        protected SortedList<double, double> curveData;

        /// <summary>
        /// Holds the maximum amount of points in this curve.
        /// </summary>
        static protected int maxNumberOfPoints = 12;

        static protected int averageFilterWindow = 3;

        protected double maximumPointX;
        protected double maximumPointY;

        protected double minimumPointX;
        protected double minimumPointY;

        public static void PopulateCurveTest(Curve curve)
        {
            //curve.AddPoint(0, .5);
            //curve.AddPoint(0.1, 0.1);
            //curve.AddPoint(0.2, 0.2);
            //curve.AddPoint(0.25, 0.25);
            //curve.AddPoint(0.4, 0.4);
            //curve.AddPoint(0.5, 0.5);
            //curve.AddPoint(0.6, 0.6);
            //curve.AddPoint(0.9, 0.9);
            //curve.AddPoint(0.99, .95);
            //curve.AddPoint(.33, .33);
            //curve.AddPoint(.7, .8);
            //curve.AddPoint(.75, .8);
            //curve.AddPoint(.8, .8);
            //curve.AddPoint(1.3, .6);
            //curve.AddPoint(1.4, .4);
            //curve.AddPoint(1.5, .3);
            //curve.AddPoint(1.6, .2);
            //curve.AddPoint(1.7, .1);
            //curve.AddPoint(1.8, -.1);
            //curve.AddPoint(1.9, -.1);
            //curve.AddPoint(2.1, -.2);
            //curve.AddPoint(2.15, -.3);
            //curve.AddPoint(2.2, -.4);
            //curve.AddPoint(2.225, -.5);
            //curve.AddPoint(2.25, -.6);
            //curve.AddPoint(2.275, -.7);
            //curve.AddPoint(2.3, -.8);
            //curve.AddPoint(2.4, -.9);
            //curve.AddPoint(2.5, -.95);
            //curve.AddPoint(2.7, -.95);
            //////////////////////////////////////
            /// 
            curve.AddPointWithAvgFilter(-3.14159265358979, 1.39635420686754);
            curve.AddPointWithAvgFilter(-3.07812613533545, 1.89995829146925);
            curve.AddPointWithAvgFilter(-3.01465961708111, 1.48854286693781);
            curve.AddPointWithAvgFilter(-2.95119309882678, 1.0860811022767);
            curve.AddPointWithAvgFilter(-2.88772658057244, 0.920492037729703);
            curve.AddPointWithAvgFilter(-2.8242600623181, 1.3838628891516);
            curve.AddPointWithAvgFilter(-2.76079354406376, 0.933474681122092);
            curve.AddPointWithAvgFilter(-2.69732702580942, 0.35450756315884);
            curve.AddPointWithAvgFilter(-2.63386050755508, 0.0534229643943726);
            curve.AddPointWithAvgFilter(-2.57039398930074, 0.838723714241578);
            curve.AddPointWithAvgFilter(-2.5069274710464, 0.217962442212655);
            curve.AddPointWithAvgFilter(-2.44346095279206, 0.410322568792965);
            curve.AddPointWithAvgFilter(-2.37999443453772, 0.33462340878903);
            curve.AddPointWithAvgFilter(-2.31652791628338, -0.674969032252129);
            curve.AddPointWithAvgFilter(-2.25306139802904, -0.804442376513671);
            curve.AddPointWithAvgFilter(-2.1895948797747, -0.997707724013973);
            curve.AddPointWithAvgFilter(-2.12612836152037, -1.0413203658205);
            curve.AddPointWithAvgFilter(-2.06266184326603, -1.25870522517678);
            curve.AddPointWithAvgFilter(-1.99919532501169, -1.35944318507287);
            curve.AddPointWithAvgFilter(-1.93572880675735, -1.42452521176546);
            curve.AddPointWithAvgFilter(-1.87226228850301, -1.30958887273105);
            curve.AddPointWithAvgFilter(-1.80879577024867, -1.15584663542153);
            curve.AddPointWithAvgFilter(-1.74532925199433, -1.67184080123879);
            curve.AddPointWithAvgFilter(-1.68186273373999, -1.65681005379094);
            curve.AddPointWithAvgFilter(-1.61839621548565, -1.64392135469107);
            curve.AddPointWithAvgFilter(-1.55492969723131, -1.37223601492581);
            curve.AddPointWithAvgFilter(-1.49146317897697, -1.92358823367669);
            curve.AddPointWithAvgFilter(-1.42799666072263, -1.19516369166389);
            curve.AddPointWithAvgFilter(-1.36453014246829, -1.36492208827684);
            curve.AddPointWithAvgFilter(-1.30106362421395, -1.08783537085571);
            curve.AddPointWithAvgFilter(-1.23759710595962, -1.26508751441717);
            curve.AddPointWithAvgFilter(-1.17413058770528, -0.625362236401926);
            curve.AddPointWithAvgFilter(-1.11066406945094, -0.56684144603304);
            curve.AddPointWithAvgFilter(-1.0471975511966, -0.856734378351539);
            curve.AddPointWithAvgFilter(-0.983731032942258, -0.711052078905264);
            curve.AddPointWithAvgFilter(-0.920264514687919, -0.282403717588411);
            curve.AddPointWithAvgFilter(-0.85679799643358, -0.645852139012745);
            curve.AddPointWithAvgFilter(-0.79333147817924, 0.186706066961859);
            curve.AddPointWithAvgFilter(-0.729864959924901, -0.466745932262059);
            curve.AddPointWithAvgFilter(-0.666398441670562, 0.176330072978658);
            curve.AddPointWithAvgFilter(-0.602931923416223, 0.0677179747872264);
            curve.AddPointWithAvgFilter(-0.539465405161884, 0.613412607652294);
            curve.AddPointWithAvgFilter(-0.475998886907544, 0.502621087466802);
            curve.AddPointWithAvgFilter(-0.412532368653205, 0.716935659109958);
            curve.AddPointWithAvgFilter(-0.349065850398866, 0.470792234094891);
            curve.AddPointWithAvgFilter(-0.285599332144526, 1.09232955297835);
            curve.AddPointWithAvgFilter(-0.222132813890187, 1.41011741790095);
            curve.AddPointWithAvgFilter(-0.158666295635848, 1.33875190579228);
            curve.AddPointWithAvgFilter(-0.095199777381509, 1.00794417153841);
            curve.AddPointWithAvgFilter(-0.03173325912717, 1.91460655432265);
            curve.AddPointWithAvgFilter(0.03173325912717, 1.7816862048228);
            curve.AddPointWithAvgFilter(0.095199777381509, 1.37592051147737);
            curve.AddPointWithAvgFilter(0.158666295635848, 1.37860742057852);
            curve.AddPointWithAvgFilter(0.222132813890187, 1.2872818200005);
            curve.AddPointWithAvgFilter(0.285599332144526, 1.98959707400003);
            curve.AddPointWithAvgFilter(0.349065850398866, 1.18834528115876);
            curve.AddPointWithAvgFilter(0.412532368653205, 1.99317242166135);
            curve.AddPointWithAvgFilter(0.475998886907544, 1.60381272234308);
            curve.AddPointWithAvgFilter(0.539465405161884, 1.80561808342577);
            curve.AddPointWithAvgFilter(0.602931923416223, 1.72570961909859);
            curve.AddPointWithAvgFilter(0.666398441670562, 1.6386525947751);
            curve.AddPointWithAvgFilter(0.729864959924901, 1.75586085557358);
            curve.AddPointWithAvgFilter(0.79333147817924, 1.49256828391793);
            curve.AddPointWithAvgFilter(0.85679799643358, 1.12734384227661);
            curve.AddPointWithAvgFilter(0.920264514687919, 0.719248621295077);
            curve.AddPointWithAvgFilter(0.983731032942258, 0.65975286277186);
            curve.AddPointWithAvgFilter(1.0471975511966, 0.591019662713436);
            curve.AddPointWithAvgFilter(1.11066406945094, 1.28613295844699);
            curve.AddPointWithAvgFilter(1.17413058770528, 1.12065310853309);
            curve.AddPointWithAvgFilter(1.23759710595962, 1.00736033247674);
            curve.AddPointWithAvgFilter(1.30106362421395, 0.624858768074646);
            curve.AddPointWithAvgFilter(1.36453014246829, 0.0810994893070038);
            curve.AddPointWithAvgFilter(1.42799666072263, 0.0881309104803278);
            curve.AddPointWithAvgFilter(1.49146317897697, 0.0115093591309446);
            curve.AddPointWithAvgFilter(1.55492969723131, 0.472983281472946);
            curve.AddPointWithAvgFilter(1.61839621548565, 0.171282500319029);
            curve.AddPointWithAvgFilter(1.68186273373999, 0.789575346431925);
            curve.AddPointWithAvgFilter(1.74532925199433, 0.0455145603400934);
            curve.AddPointWithAvgFilter(1.80879577024867, 0.414848567793334);
            curve.AddPointWithAvgFilter(1.87226228850301, 0.976707108463735);
            curve.AddPointWithAvgFilter(1.93572880675735, 0.409173496221809);
            curve.AddPointWithAvgFilter(1.99919532501169, 0.461345089757619);
            curve.AddPointWithAvgFilter(2.06266184326603, 1.2881157063007);
            curve.AddPointWithAvgFilter(2.12612836152037, 1.15891840436669);
            curve.AddPointWithAvgFilter(2.1895948797747, 0.953258370681676);
            curve.AddPointWithAvgFilter(2.25306139802904, 0.879173622417986);
            curve.AddPointWithAvgFilter(2.31652791628338, 1.48330907109987);
            curve.AddPointWithAvgFilter(2.37999443453772, 1.21653245695899);
            curve.AddPointWithAvgFilter(2.44346095279206, 1.7273615984108);
            curve.AddPointWithAvgFilter(2.5069274710464, 1.8059154068297);
            curve.AddPointWithAvgFilter(2.57039398930074, 1.91125743207817);
            curve.AddPointWithAvgFilter(2.63386050755508, 1.89979940409336);
            curve.AddPointWithAvgFilter(2.69732702580942, 1.21360887389898);
            curve.AddPointWithAvgFilter(2.76079354406376, 1.47324949905646);
            curve.AddPointWithAvgFilter(2.8242600623181, 2.09487483004092);
            curve.AddPointWithAvgFilter(2.88772658057244, 2.11566891751715);
            curve.AddPointWithAvgFilter(2.95119309882678, 1.34972184058067);
            curve.AddPointWithAvgFilter(3.01465961708111, 1.31136365213583);
            curve.AddPointWithAvgFilter(3.07812613533545, 1.68187849713421);
            curve.AddPointWithAvgFilter(3.14159265358979, 1.15568185651978);
        }

        /// <summary>
        /// Adds a point for the curve.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public void AddPoint(double x, double y)
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                curveData.Add(x, y);
                if (maximumPointY < y)
                {
                    maximumPointY = y;
                    maximumPointX = x;
                }
                if (minimumPointY > y)
                {
                    minimumPointY = y;
                    minimumPointX = x;
                }
                while (curveData.Count >= maxNumberOfPoints)
                {
                    var removedX = curveData.Keys[0];
                    curveData.Remove(curveData.Keys[0]);
                    if (Double.Equals(removedX, minimumPointX))
                    {
                        var XY = CalculateCurveMinPoint();
                        minimumPointX = XY.Item1;
                        minimumPointY = XY.Item2;
                    }
                    if (Double.Equals(removedX, maximumPointX))
                    {
                        var XY = CalculateCurveMaxPoint();
                        maximumPointX = XY.Item1;
                        maximumPointY = XY.Item2;
                    }
                }
            } 
            catch(Exception e)
            {
                // Handle .NET errors.
                Console.WriteLine("Exception : {0}", e.Message);
            }
            //watch.Stop();
            //var elapsedMs = watch.ElapsedMilliseconds;
            //Console.WriteLine("AddPointTIme in ms : {0}", elapsedMs);
        }

        public void AddPointWithAvgFilter(double x, double y)
        {
            if (curveData.Count >= averageFilterWindow)
            {
                var avg = y + curveData.Values[curveData.Count - 1] + curveData.Values[curveData.Count - (averageFilterWindow - 1)];
                avg /= averageFilterWindow;
                AddPoint(x, avg);
            } 
            else 
            {
                AddPoint(x, y);
            }
        }

        /// <summary>
        /// Prints the curve in console.
        /// </summary>
        public void PrintCurveInConsole()
        {
            const char BLANK = ' ';
            const char DOT = '.';
            const char X = 'x';
            const int cMaxLineChars = 79;
            const int cHalf = cMaxLineChars / 2;
            char[] LINE = new char[cMaxLineChars];

            for (int i = 0; i < LINE.Length; i++)
            {
                LINE[i] = DOT;
            }
            Console.WriteLine(LINE);
            for (int i = 0; i < LINE.Length; i++)
            {
                LINE[i] = BLANK;
            }

            int loc;
            //var maxY = getCurveMaxAbsY();
            var maxY = 2.2;
            LINE[cHalf] = DOT; 
            foreach (var pair in curveData)
            {
                loc = (int)Math.Round(cMaxLineChars * (pair.Value + maxY) / (2 * maxY));
                //loc = (int)Math.Round(cMaxLineChars * (pair.Value + maxY) / (2 * maxY));
                if (loc == LINE.Length)
                {
                    LINE[loc - 1] = X;
                } else
                {
                    LINE[loc] = X;
                }
                Console.WriteLine(LINE);
                for (int i = 0; i < LINE.Length; i++)
                {
                    LINE[i] = BLANK;
                }
                LINE[cHalf] = DOT;
            }
        }

        public void PrintCurveLastValue()
        {
            Console.WriteLine(curveData[curveData.Keys[0]]);
        }

        public double GetCurveLastValue()
        {
            if (curveData.Count != 0)
            {
                var x = curveData.Keys[curveData.Keys.Count - 1];
                return curveData[x];
            }
            else
            {
                return 0;
            }
            
        }

        /*!
         * Writes info about cardholder: name, EPC, Ambient and time-entered-ambient
         */
        public void WriteDataToFile()
        {

        }

        /// <summary>
        /// Getter the minimum value for x coordinate.
        /// </summary>
        /// <returns>The curve minimum x.</returns>
        public double GetCurveMinX()
        {
            return curveData.Keys[0];
        }

        /// <summary>
        /// Getter for the index's value for Y
        /// </summary>
        public double GetCurveIndexY(int i)
        {
            if (curveData.Values.Count() > i)
                return curveData.Values[i];
            else
                return 0;
        }

        /// <summary>
        /// Getter for the index's value for X
        /// </summary>
        public double GetCurveIndexX(int i)
        {
            if (curveData.Values.Count() > i)
                return curveData.Keys[i];
            else
                return 0;
        }

        /// <summary>
        /// Getter the maximum value for x coordinate.
        /// </summary>
        public double GetCurveMaxX()
        {
            return curveData.Keys[curveData.Count - 1];
        }

        /// <summary>
        /// Getter the curve coordinate for the y maximum value.
        /// </summary>
        public Tuple<double, double> CalculateCurveMaxPoint()
        {
            maximumPointX = Double.NegativeInfinity; 
            maximumPointY = Double.NegativeInfinity;
            if (curveData.Count != 0)
            {
                foreach (var pair in curveData)
                {
                    if (pair.Value > maximumPointY)
                    {
                        maximumPointX = pair.Key;
                        maximumPointY = pair.Value;
                    }
                }
            }

            return Tuple.Create<double, double>(maximumPointX, maximumPointY);
        }

        public Tuple<double, double> GetCurveMaxPoint()
        {
            return Tuple.Create<double, double>(maximumPointX, maximumPointY);
        }

        /// <summary>
        /// Getter the curve coordinate for the y minimum value.
        /// </summary>
        public Tuple<double, double> CalculateCurveMinPoint()
        {
            minimumPointX = Double.PositiveInfinity;
            minimumPointY = Double.PositiveInfinity;
            foreach (var pair in curveData)
            {
                if (pair.Value < minimumPointY)
                {
                    minimumPointX = pair.Key;
                    minimumPointY = pair.Value;
                }
            }
            return Tuple.Create<double, double>(minimumPointX, minimumPointY);
        }

        public Tuple<double, double> GetCurveMinPoint()
        {
            return Tuple.Create<double, double>(minimumPointX, minimumPointY);
        }

        /// <summary>
        /// Getter the curve median y coordinates.
        /// </summary>
        public double GetMedianY()
        {
            double medianY = 0;
            if (curveData.Count > 2)
            {
                var values = curveData.Values;
                List<double> list = new List<double>(values);
                list.Sort();
                if (list.Count % 2 != 0)
                {
                    medianY = list[list.Count / 2];
                }
                else
                {
                    medianY = list[1 + list.Count / 2];
                }
            }
            return medianY;
        }

        /// <summary>
        /// Getter the curve mean y coordinates.
        /// </summary>
        public double CalculateMeanY()
        {
            double sumY = 0;
            double medianY = 0;
            foreach (var pair in curveData)
            {
                sumY += pair.Value;
            }
            if (curveData.Count != 0)
            {
                medianY = sumY / curveData.Count;
            }
            return medianY;
        }

        /// <summary>
        /// Getter for the crossing treshold between positive and negative values.
        /// </summary>
        public Tuple<double, double> CalculateCrossingPoint()
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            double crossingPointX = Double.NaN;
            double crossingPointY = Double.NaN;
            bool isFirstSignPositive = false;
            for(int i = curveData.Count - 1; i >= 0; i--)
            {
                var x = curveData.Keys[i];
                var y = curveData[x];
                if (i == curveData.Count - 1)
                {
                    isFirstSignPositive = y > 0 ? true : false;
                    continue;
                }
                //if ((y < 0 && isFirstSignPositive) || (y > 0 && !isFirstSignPositive))
                if ((y > 0 && !isFirstSignPositive))
                {
                    if (i < curveData.Count - 2)
                    {
                        x += curveData.Keys[i + 1];
                        x *= 0.5;
                        y += curveData[curveData.Keys[i + 1]];
                        y *= 0.5;
                    }
                    crossingPointY = x;
                    crossingPointX = y;
                    break;
                }
            }
            //watch.Stop();
            //var elapsedMs = watch.ElapsedMilliseconds;
            //Console.WriteLine("CrossingPoint in ms : {0}", elapsedMs);
            return Tuple.Create(crossingPointX, crossingPointY);
        }

        /// <summary>
        /// Constructor for a curve.
        /// </summary>
        public Curve()
        {
            curveData = new SortedList<double, double>();
            maximumPointX = Double.NegativeInfinity;
            maximumPointY = Double.NegativeInfinity;
            minimumPointX = Double.PositiveInfinity;
            minimumPointY = Double.PositiveInfinity;
        }

        public int GetSize()
        {
            return curveData.Count;
        }

        public IList<Tuple<double, double>> CalculatePeaks()
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            IList<double> values = curveData.Values;
            int rangeOfPeaks = (int)(Math.Round(.3 * values.Count));

            List<Tuple<double, double>> peaks = new List<Tuple<double, double>>();

            double current;
            IEnumerable<double> range;

            int checksOnEachSide = rangeOfPeaks / 2;
            if (values.Count == 0)
            {
                return peaks;
            }
            else if (values.Count < checksOnEachSide)
            {
                peaks.Add(Tuple.Create(maximumPointX, maximumPointY));
            }
            else
            {
                for (int i = 0; i < values.Count; i++)
                {
                    current = values[i];
                    range = values;

                    if (i > checksOnEachSide)
                    {
                        range = range.Skip(i - checksOnEachSide);
                    }

                    range = range.Take(rangeOfPeaks);
                    if ((range.Count() > 0) && (current == range.Max()))
                    {
                        peaks.Add(Tuple.Create(curveData.Keys[i], curveData.Values[i]));
                    }
                }
            }
            //watch.Stop();
            //var elapsedMs = watch.ElapsedMilliseconds;
            //Console.WriteLine("PeaksPoint in ms : {0}", elapsedMs);
            return peaks;
        }

        public bool CompareCurveMaximums(Curve otherCurve)
        {
            return GetCurveMaxPoint().Item2 > otherCurve.GetCurveMaxPoint().Item2;
        }

        public bool CompareCurveLastValues(Curve otherCurve)
        {
            return GetCurveLastValue() > otherCurve.GetCurveLastValue();
        }

        public bool CompareCurveMeans(Curve otherCurve)
        {
            return CalculateMeanY() >= otherCurve.CalculateMeanY();
        }

        public bool CompareCurveMedians(Curve otherCurve)
        {
            return GetMedianY() >= otherCurve.GetMedianY();
        }

        public bool CompareCurveLastPeak(Curve otherCurve)
        {
            var peakListLast = CalculatePeaks();
            var peakListOther = otherCurve.CalculatePeaks();

            return (peakListLast.Count > 0 && peakListOther.Count > 0 && (peakListLast[peakListLast.Count - 1].Item1 > peakListOther[peakListOther.Count - 1].Item1))
                    || (peakListLast.Count == 0 || peakListOther.Count == 0 && CompareCurveMaximums(otherCurve));
        }
    }
}

