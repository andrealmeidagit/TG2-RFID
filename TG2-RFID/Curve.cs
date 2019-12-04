using System;
using System.Collections;
using System.Collections.Generic;

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
        protected int maxNumberOfPoints;

        public static void PopulateCurveTest(Curve curve)
        {
            curve.AddPoint(0, 0);
            curve.AddPoint(0.1, 0.1);
            curve.AddPoint(0.2, 0.2);
            curve.AddPoint(0.25, 0.25);
            curve.AddPoint(0.4, 0.4);
            curve.AddPoint(0.5, 0.5);
            curve.AddPoint(0.6, 0.6);
            curve.AddPoint(0.9, 0.9);
            curve.AddPoint(0.99, .95);
            curve.AddPoint(.33, .33);
            curve.AddPoint(.7, .8);
            curve.AddPoint(.75, .8);
            curve.AddPoint(.8, .8);
            curve.AddPoint(1.3, .6);
            curve.AddPoint(1.4, .4);
            curve.AddPoint(1.5, .3);
            curve.AddPoint(1.6, .2);
            curve.AddPoint(1.7, .1);
            curve.AddPoint(1.8, -.1);
            curve.AddPoint(1.9, -.1);
            curve.AddPoint(2.1, -.2);
            curve.AddPoint(2.15, -.3);
            curve.AddPoint(2.2, -.4);
            curve.AddPoint(2.225, -.5);
            curve.AddPoint(2.25, -.6);
            curve.AddPoint(2.275, -.7);
            curve.AddPoint(2.3, -.8);
            curve.AddPoint(2.4, -.9);
            curve.AddPoint(2.5, -.95);
            curve.AddPoint(2.7, -.95);
        }

        /// <summary>
        /// Adds a point for the curve.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public void AddPoint(double x, double y)
        {
            try
            {
                curveData.Add(x, y);
                if (curveData.Count == maxNumberOfPoints)
                {
                    curveData.Remove(curveData.Keys[0]);
                }
            } 
            catch(Exception e)
            {
                // Handle .NET errors.
                Console.WriteLine("Exception : {0}", e.Message);
            }
        }

        public void AddPointWithAvgFilter(double x, double y)
        {
            if (curveData.Count >= 3)
            {
                var avg = y + curveData.Values[curveData.Count - 1] + curveData.Values[curveData.Count - 2];
                avg /= 3;
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
            var maxY = 100;
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

        /*!
         * TODO
         */
        public void WriteCurveToFile()
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
        /// Getter the maximum value for x coordinate.
        /// </summary>
        public double GetCurveMaxX()
        {
            return curveData.Keys[curveData.Count - 1];
        }

        /// <summary>
        /// Getter the curve maximum absolute value for y coordinate.
        /// </summary>
        public double GetCurveMaxAbsY()
        {
            double maxY = -1e12;
            if (curveData.Count != 0)
            {
                foreach (var pair in curveData)
                {
                    if (Math.Abs(pair.Value) > maxY)
                    {
                        maxY = Math.Abs(pair.Value);
                    }
                }
            }
            return maxY;
        }

        /// <summary>
        /// Getter the curve coordinate for the y maximum value.
        /// </summary>
        public Tuple<double, double> GetCurveMaxPoint()
        {
            double maxX = -1e12, maxY = -1e12;
            if (curveData.Count != 0)
            {
                foreach (var pair in curveData)
                {
                    if (pair.Value > maxY)
                    {
                        maxX = pair.Key;
                        maxY = pair.Value;
                    }
                }
            }

            return Tuple.Create<double, double>(maxX, maxY);
        }

        /// <summary>
        /// Getter the curve coordinate for the y minimum value.
        /// </summary>
        public Tuple<double, double> GetCurveMinPoint()
        {
            double minY = 1e12;
            double minX = 1e12;
            foreach (var pair in curveData)
            {
                if (pair.Value < minY)
                {
                    minX = pair.Key;
                    minY = pair.Value;
                }
            }
            return Tuple.Create<double, double>(minX, minY);
        }

        /// <summary>
        /// Getter the curve median y coordinates.
        /// </summary>
        public double GetMedianY()
        {
            double medianY = 0;
            if (curveData.Count != 0)
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
        public double GetMeanY()
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
        public double GetPosNegTransition()
        {
            SortedList<double, double> positiveData = new SortedList<double, double>();
            SortedList<double, double> negativeData = new SortedList<double, double>();
            foreach (var pair in curveData)
            {
                if (pair.Value > 0)
                {
                    positiveData.Add(pair.Key, pair.Value);
                }
                else
                {
                    negativeData.Add(pair.Key, pair.Value);
                }

                //positives median
                double sumPosX = 0;
                double medianPosX = 0;
                foreach (var pairPos in positiveData)
                {
                    sumPosX += pairPos.Key;
                }
                if (positiveData.Count != 0)
                {
                    medianPosX = sumPosX / positiveData.Count;
                }

                //negatives median
                double sumNegX = 0;
                double medianNegX = 0;
                foreach (var pairNeg in negativeData)
                {
                    sumNegX += pairNeg.Key;
                }
                if (negativeData.Count != 0)
                {
                    medianNegX = sumNegX / negativeData.Count;
                }

                //to be continued


            }

        }

        /// <summary>
        /// Constructor for a curve.
        /// </summary>
        public Curve()
        {
            maxNumberOfPoints = 100;
            curveData = new SortedList<double, double>();
        }

        public int GetSize()
        {
            return curveData.Count;
        }
    }
}
