using System;
using System.Collections;
using System.Collections.Generic;
using Impinj.OctaneSdk;

namespace TG2_RFID
{
    public class Cardholder
    {
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
        /// Holds the current ambient in which this cardholder is.
        /// </summary>
        protected Ambient currentAmbient;

         /// <summary>
         /// Holds a map of curves per antenna in which this cardholder was seen
         /// holding the history of RSSI power per time.
         /// </summary>
        public Dictionary<Tuple<String, ushort>, Curve> curvesPowerReadingsDictionary;

        /// <summary>
        /// Holds a map of curves per antenna in which this cardholder was seen
        /// holding the history of doppler frequency per time.
        /// </summary>
        public Dictionary<Tuple<String, ushort>, Curve> curvesDoplerFrequencyReadingsDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TG2_RFID.Cardholder"/> class.
        /// </summary>
        public Cardholder()
        {
            name = "DefaultName";
            thermalCapacity = 1000;
            wasInitialized = false;
            curvesPowerReadingsDictionary = new Dictionary<Tuple<String, ushort>, Curve>();
            curvesDoplerFrequencyReadingsDictionary = new Dictionary<Tuple<String, ushort>, Curve>();
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
            curvesPowerReadingsDictionary = new Dictionary<Tuple<String, ushort>, Curve>();
            curvesDoplerFrequencyReadingsDictionary = new Dictionary<Tuple<String, ushort>, Curve>();
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
            curvesPowerReadingsDictionary = new Dictionary<Tuple<String, ushort>, Curve>();
            curvesDoplerFrequencyReadingsDictionary = new Dictionary<Tuple<String, ushort>, Curve>();
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
            curvesPowerReadingsDictionary = new Dictionary<Tuple<String, ushort>, Curve>();
            curvesDoplerFrequencyReadingsDictionary = new Dictionary<Tuple<String, ushort>, Curve>();
        }

        /// <summary>
        /// Getter for the tag EPC.
        /// </summary>
        public string getCardholderEPC()
        {
            return tagEPC;
        }

        /// <summary>
        /// Setter for the tag EPC.
        /// </summary>
        /// <param name="newTagEPC">New tag epc.</param>
        public void setCardholderEPC(string newTagEPC)
        {
            tagEPC = newTagEPC;
        }

        /// <summary>
        /// Getter for a RSSI power curve given an antenna.
        /// </summary>
        /// <returns>The power curve.</returns>
        /// <param name="antenna">Antenna.</param>
        public Curve getPowerCurve(Tuple<String, ushort> antenna)
        {
            Curve retCurve = new Curve();
            curvesPowerReadingsDictionary.TryGetValue(antenna, out retCurve);
            return retCurve;
        }

        /// <summary>
        /// Getter for the dopple effect curve given an antenna.
        /// </summary>
        /// <returns>The doppler effect curve.</returns>
        /// <param name="antenna">Antenna.</param>
        public Curve getDopplerEffectCurve(Tuple<String, ushort> antenna)
        {
            Curve retCurve = new Curve();
            curvesDoplerFrequencyReadingsDictionary.TryGetValue(antenna, out retCurve);
            return retCurve;
        }

        /// <summary>
        /// Getter for the tag EPC.
        /// </summary>
        /// <returns>The tag epc.</returns>
        public string getTagEPC()
        {
            return tagEPC;
        }

        /// <summary>
        /// TODO
        /// TODO Reset buffer at 4am due to overflow problem.
        /// </summary>
        public void readingCardholderTag(Tag tag, String senderName)
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
            double readingTime = (double)(diffTime.TotalMilliseconds);//(double)(lastSeenTime.Date.Millisecond) - (double)(firstSeenTime.Date.Millisecond);

            Antenna seenAntenna = new Antenna();

            var tupleAntenna = Tuple.Create<String, ushort>(senderName, tag.AntennaPortNumber);

            if (!curvesPowerReadingsDictionary.ContainsKey(tupleAntenna))
            {
                curvesPowerReadingsDictionary.Add(tupleAntenna, new Curve());
            }
            Curve powerCurve;
            try
            {
                powerCurve = curvesPowerReadingsDictionary[tupleAntenna];
                powerCurve.addPointWithAvgFilter(readingTime, tag.PeakRssiInDbm);
                //Console.WriteLine("SIZE PWRCURVE {0} TIME {1}", powerCurve.getSize(), readingTime);
                if (tupleAntenna.Item2 == 1)
                {
                    powerCurve.printCurveInConsole();
                    //powerCurve.printCurveLastValue;
                }
            }
            catch(Exception e)
            {
            }


            if (!curvesDoplerFrequencyReadingsDictionary.ContainsKey(tupleAntenna))
            {
                curvesDoplerFrequencyReadingsDictionary.Add(tupleAntenna, new Curve());
            }
            Curve dopplerCurve;
            try
            {
                dopplerCurve = curvesDoplerFrequencyReadingsDictionary[tupleAntenna];
                dopplerCurve.addPointWithAvgFilter(readingTime, tag.RfDopplerFrequency);
                //Console.WriteLine("SIZE DOPCURVE {0} TIME {1}", dopplerCurve.getSize(), readingTime);
                if (tupleAntenna.Item2 == 1)
                {
                    //dopplerCurve.printCurveInConsole();
                    //dopplerCurve.printCurveLastValue();
                    //Console.WriteLine("DOPVAL {0} TIME {1}", tag.RfDopplerFrequency, readingTime);
                }
            }
            catch (Exception e)
            {
            }



            //Console.WriteLine("I am dead!");
            //Console.WriteLine("Antena: {0}, EPC: {1}, RSSI: {2}", tag.AntennaPortNumber, tag.Epc.ToString(), tag.PeakRssiInDbm);


            //tag.AntennaPortNumber;
            //tag.RfDopplerFrequency
            //curvesPowerReadingsDictionary.Add


        }
    }
}
