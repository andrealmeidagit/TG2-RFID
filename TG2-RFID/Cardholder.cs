﻿using System;
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
        public Dictionary<Antenna, Curve> curvesPowerReadingsDictionary;

        /// <summary>
        /// Holds a map of curves per antenna in which this cardholder was seen
        /// holding the history of doppler frequency per time.
        /// </summary>
        public Dictionary<Antenna, Curve> curvesDoplerFrequencyReadingsDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TG2_RFID.Cardholder"/> class.
        /// </summary>
        public Cardholder()
        {
            name = "DefaultName";
            thermalCapacity = 1000;
            wasInitialized = false;
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
        public Curve getPowerCurve(Antenna antenna)
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
        public Curve getDopplerEffectCurve(Antenna antenna)
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
        /// </summary>
        public void readingCardholderTag(Tag tag)
        {
            lastSeenTag = tag;
            lastSeenTime = tag.LastSeenTime.LocalDateTime.Date;
            lastSeenRSSI = tag.PeakRssiInDbm;

            if (!wasInitialized)
            {
                firstSeenTime = tag.FirstSeenTime.LocalDateTime.Date;
                wasInitialized = true;
            }

            double readingTime = (double)(lastSeenTime.Date.Millisecond) - (double)(firstSeenTime.Date.Millisecond);
            //tag.AntennaPortNumber;
            //tag.RfDopplerFrequency
            //curvesPowerReadingsDictionary.Add


        }
    }
}