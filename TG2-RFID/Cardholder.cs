using System;
using System.Collections;
using System.Collections.Generic;
using Impinj.OctaneSdk;



namespace TG2_RFID
{
    public class Cardholder
    {

        public static int RSSI_HIGHPASSFILTER = -60;
        public static double DOPPLER_FILTER = 0.5;

        /// <summary>
        /// Holds the cardholder's name.
        /// </summary>
        protected string name;

        /// <summary>
        /// Holds the thermal capacity for this cardholder.
        /// </summary>
        protected int thermalCapacity;

        /// <summary>
        /// Holds the tag epc.
        /// </summary>
        protected string tagEPC;

        /// <summary>
        /// Holds whether the cardholder was initialized.
        /// </summary>
        protected bool wasInitialized;

        /// <summary>
        /// Holds the last seen tag as a Tag.
        /// </summary>
        protected Tag lastSeenTag;

        /// <summary>
        /// Holds the first seen time for this cardholder.
        /// </summary>
        protected DateTime firstSeenTime;

        /// <summary>
        /// Holds the last seen time for this cardholder.
        /// </summary>
        protected DateTime lastSeenTime;

        /// <summary>
        /// Holds the last seen rssi power value.
        /// </summary>
        protected double lastSeenRSSI;

        /// <summary>
        /// Holds the current ambient in which this cardholder is according to the default criteria.
        /// </summary>
        protected Ambient currentAmbient;

        /// <summary>
        /// Holds the current ambient in which this cardholder is according to the last RSSI peak time criteria.
        /// </summary>
        protected Ambient ambPeakTimes;

        /// <summary>
        /// Holds the current ambient in which this cardholder is according to the last RSSI peak time and magnitude criteria.
        /// </summary>
        protected Ambient ambPeakTimeAndMag;

        /// <summary>
        /// Holds the current ambient in which this cardholder is according to the last RSSI value criteria.
        /// </summary>
        protected Ambient ambLastRSSI;

        /// <summary>
        /// Holds the current ambient in which this cardholder is according to the last RSSI mean criteria.
        /// </summary>
        protected Ambient ambRSSIMean;

        /// <summary>
        /// Holds the current ambient in which this cardholder is according to the last RSSI median criteria.
        /// </summary>
        protected Ambient ambRSSIMedian;

        /// <summary>
        /// Holds the current ambient in which this cardholder is according to the Doppler Effect criteria.
        /// </summary>
        protected Ambient ambDopplerEffect;

        /// <summary>
        /// Holds the current ambient in which this cardholder is according to the Doppler Effect and RSSI criteria.
        /// </summary>
        protected Ambient ambDopplerAndRSSI;

        /// <summary>
        /// Holds a map of curves per antenna in which this cardholder was seen
        /// holding the history of RSSI power per time.
        /// </summary>
        public Dictionary<Tuple<string, ushort>, Curve> curvesPowerReadingsDictionary;

        /// <summary>
        /// Holds a map of curves per antenna in which this cardholder was seen
        /// holding the history of doppler frequency per time.
        /// </summary>
        public Dictionary<Tuple<string, ushort>, Curve> curvesDoplerFrequencyReadingsDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TG2_RFID.Cardholder"/> class.
        /// </summary>
        public Cardholder()
        {
            name = "DefaultName";
            thermalCapacity = 1000;
            wasInitialized = false;
            curvesPowerReadingsDictionary = new Dictionary<Tuple<string, ushort>, Curve>();
            curvesDoplerFrequencyReadingsDictionary = new Dictionary<Tuple<string, ushort>, Curve>();
            ambDopplerAndRSSI = (Project.GetAmbientInstance(0));
            ambDopplerEffect = (Project.GetAmbientInstance(0));
            ambLastRSSI = (Project.GetAmbientInstance(0));
            ambPeakTimeAndMag = (Project.GetAmbientInstance(0));
            ambPeakTimes = (Project.GetAmbientInstance(0));
            ambRSSIMean = (Project.GetAmbientInstance(0));
            ambRSSIMedian = (Project.GetAmbientInstance(0));
            currentAmbient = (Project.GetAmbientInstance(0));

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TG2_RFID.Cardholder"/> class.
        /// </summary>
        /// <param name="personName">Person name.</param>
        public Cardholder(string personName)
        {
            name = personName;
            thermalCapacity = 1000;
            wasInitialized = false;
            curvesPowerReadingsDictionary = new Dictionary<Tuple<string, ushort>, Curve>();
            curvesDoplerFrequencyReadingsDictionary = new Dictionary<Tuple<string, ushort>, Curve>();
            ambDopplerAndRSSI = (Project.GetAmbientInstance(0));
            ambDopplerEffect = (Project.GetAmbientInstance(0));
            ambLastRSSI = (Project.GetAmbientInstance(0));
            ambPeakTimeAndMag = (Project.GetAmbientInstance(0));
            ambPeakTimes = (Project.GetAmbientInstance(0));
            ambRSSIMean = (Project.GetAmbientInstance(0));
            ambRSSIMedian = (Project.GetAmbientInstance(0));
            currentAmbient = (Project.GetAmbientInstance(0));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TG2_RFID.Cardholder"/> class.
        /// </summary>
        /// <param name="personName">Person name.</param>
        /// <param name="personThermalCapacity">Person thermal capacity.</param>
        public Cardholder(string personName, int personThermalCapacity)
        {
            name = personName;
            thermalCapacity = personThermalCapacity;
            wasInitialized = false;
            curvesPowerReadingsDictionary = new Dictionary<Tuple<string, ushort>, Curve>();
            curvesDoplerFrequencyReadingsDictionary = new Dictionary<Tuple<string, ushort>, Curve>();
            ambDopplerAndRSSI = (Project.GetAmbientInstance(0));
            ambDopplerEffect = (Project.GetAmbientInstance(0));
            ambLastRSSI = (Project.GetAmbientInstance(0));
            ambPeakTimeAndMag = (Project.GetAmbientInstance(0));
            ambPeakTimes = (Project.GetAmbientInstance(0));
            ambRSSIMean = (Project.GetAmbientInstance(0));
            ambRSSIMedian = (Project.GetAmbientInstance(0));
            currentAmbient = (Project.GetAmbientInstance(0));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TG2_RFID.Cardholder"/> class.
        /// </summary>
        /// <param name="personName">Person name.</param>
        /// <param name="personTagEPC">Person tag epc.</param>
        public Cardholder(string personName, string personTagEPC)
        {
            name = personName;
            thermalCapacity = 1000;
            tagEPC = personTagEPC;
            wasInitialized = false;
            curvesPowerReadingsDictionary = new Dictionary<Tuple<string, ushort>, Curve>();
            curvesDoplerFrequencyReadingsDictionary = new Dictionary<Tuple<string, ushort>, Curve>();
        }

        /// <summary>
        /// Getter for the tag EPC.
        /// </summary>
        public string GetCardholderEPC()
        {
            return tagEPC;
        }

        /// <summary>
        /// Setter for the tag EPC.
        /// </summary>
        /// <param name="newTagEPC">New tag epc.</param>
        public void SetCardholderEPC(string newTagEPC)
        {
            tagEPC = newTagEPC;
        }

        /// <summary>
        /// Getter for a RSSI power curve given an antenna.
        /// </summary>
        /// <returns>The power curve.</returns>
        /// <param name="antenna">Antenna.</param>
        public Curve GetPowerCurve(Tuple<string, ushort> antenna)
        {
            if (!curvesPowerReadingsDictionary.ContainsKey(antenna))
            {
                curvesPowerReadingsDictionary.Add(antenna, new Curve());
            }
            curvesPowerReadingsDictionary.TryGetValue(antenna, out Curve retCurve);
            return retCurve;
        }

        /// <summary>
        /// Getter for the dopple effect curve given an antenna.
        /// </summary>
        /// <returns>The doppler effect curve.</returns>
        /// <param name="antenna">Antenna.</param>
        public Curve GetDopplerEffectCurve(Tuple<string, ushort> antenna)
        {
            if (!curvesDoplerFrequencyReadingsDictionary.ContainsKey(antenna))
            {
                curvesDoplerFrequencyReadingsDictionary.Add(antenna, new Curve());
            }
            curvesDoplerFrequencyReadingsDictionary.TryGetValue(antenna, out Curve retCurve);
            return retCurve;
        }

        /// <summary>
        /// Getter for the tag EPC.
        /// </summary>
        /// <returns>The tag epc.</returns>
        public string GetTagEPC()
        {
            return tagEPC;
        }

        /// <summary>
        /// Getter for the person's name.
        /// </summary>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// TODO
        /// TODO Reset buffer at 4am due to overflow problem.
        /// </summary>
        public void ReadingCardholderTag(Tag tag, string senderName)
        {
            lastSeenTag = tag;
            lastSeenTime = DateTime.Now;
            lastSeenRSSI = tag.PeakRssiInDbm;


            if (!wasInitialized)
            {
                firstSeenTime = lastSeenTime;
                wasInitialized = true;
            }

            var diffTime = lastSeenTime - firstSeenTime;
            double readingTime = (double)(diffTime.TotalMilliseconds);

            var tupleAntenna = Tuple.Create<string, ushort>(senderName, tag.AntennaPortNumber);

            if (!curvesPowerReadingsDictionary.ContainsKey(tupleAntenna))
            {
                curvesPowerReadingsDictionary.Add(tupleAntenna, new Curve());
            }
            var powerCurve = curvesPowerReadingsDictionary[tupleAntenna];
            if (tag.PeakRssiInDbm > RSSI_HIGHPASSFILTER)
            {
                powerCurve.AddPointWithAvgFilter(readingTime, tag.PeakRssiInDbm);
            }
            // foreach (var antenna in curvesPowerReadingsDictionary.Keys)
            //{
            //    cnt++;
            //    var powerCurve = curvesPowerReadingsDictionary[antenna];
            //    if (tupleAntenna.Equals(antenna))
            //    {
            //        powerCurve.AddPointWithAvgFilter(readingTime, tag.PeakRssiInDbm);
            //    }
            //    else
            //    {
            //        powerCurve.AddPointWithAvgFilter(readingTime, 0.0);
            //    }
            //}


            if (!curvesDoplerFrequencyReadingsDictionary.ContainsKey(tupleAntenna))
            {
                curvesDoplerFrequencyReadingsDictionary.Add(tupleAntenna, new Curve());
            }
            try
            {
                var dopplerCurve = curvesDoplerFrequencyReadingsDictionary[tupleAntenna];
                if (Math.Abs(tag.RfDopplerFrequency) > DOPPLER_FILTER)
                {
                    dopplerCurve.AddPoint(readingTime, tag.RfDopplerFrequency);
                }
                else
                {
                    //dopplerCurve.AddPoint(readingTime, dopplerCurve.GetCurveLastValue());
                }
                    
            }
            catch (Exception e)
            {
                // Handle .NET errors.
                Console.WriteLine("Exception : {0}", e.Message);
            }
        }

        /// <summary>
        /// Setter Ambiente
        /// </summary>
        public void SetCurrAmbient(Ambient currAmb)
        {
            if (currAmb == null)
            {
                return;
            }
            currentAmbient = currAmb;
        }

        /// <summary>
        /// Getter Ambiente
        /// </summary>
        public Ambient GetCurAmbient()
        {
            return currentAmbient;
        }

        /// <summary>
        /// Setter Ambiente
        /// TODO
        /// </summary>
        public void SetAmbient(Ambient ambientGuess, int i)
        {
            switch (i)
            {
                case 0:
                    ambPeakTimes = ambientGuess;
                    break;
                case 1:
                    ambPeakTimeAndMag = ambientGuess;
                    break;
                case 2:
                    ambLastRSSI = ambientGuess;
                    break;
                case 3:
                    ambRSSIMean = ambientGuess;
                    break;
                case 4:
                    ambRSSIMedian = ambientGuess;
                    break;
                case 5:
                    ambDopplerEffect = ambientGuess;
                    break;
                case 6:
                    ambDopplerAndRSSI = ambientGuess;
                    break;
            }
        }

        /// <summary>
        /// Getter Ambiente
        /// TODO
        /// </summary>
        public Ambient GetAmbient(int i)
        {
            switch (i)
            {
                case 0:
                    return ambPeakTimes;
                case 1:
                    return ambPeakTimeAndMag;
                case 2:
                    return ambLastRSSI;
                case 3:
                    return ambRSSIMean;
                case 4:
                    return ambRSSIMedian;
                case 5:
                    return ambDopplerEffect;
                case 6:
                    return ambDopplerAndRSSI;
                default:
                    return currentAmbient;
            }
        }



    }
}
