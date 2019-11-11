﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace TG2_RFID
{
    public class Ambient
    {
        /// <summary>
        /// Holds the ambient name.
        /// </summary>     
        protected string name;

        /// <summary>
        /// Holds the total thermal capacity necessary to increase/decrease the heat in this room. 
        /// A value that is proportional to the number of people in the room.
        /// </summary>  
        protected int totalThermalCapacity;

        /// <summary>
        /// Holds a map of people inside of room using their tag's EPC as key.
        /// </summary>
        protected Dictionary<string, Cardholder> cardholders;

        /// <summary>
        /// Holds the local antenna for this ambient.
        /// </summary>
        protected Antenna localAntenna;

        /// <summary>
        /// Constructor given the ambient name.
        /// </summary>
        public Ambient(string ambientName)
        {
            name = ambientName;
            cardholders = new Dictionary<string, Cardholder>();
        }

        /// <summary>
        /// Setter for the ambient antenna.       
        /// </summary>   
        public void setAntenna(Antenna antenna)
        {
            localAntenna = antenna;
        }

        /// <summary>
        /// Getter for the total thermal capacity in this room.
        /// </summary>
        public int getTotalThermalCapacity()
        {
            return totalThermalCapacity;
        }

        /// <summary>
        /// Getter for the total number of people inside of the ambient.
        /// </summary>
        public int getNumberOfCarholdersInside()
        {
            return cardholders.Count;
        }

        /// <summary>
        /// Adds a cardholder for this ambient.
        /// </summary>
        /// <param name="cardholder">Cardholder.</param>
        public void addCardholder(Cardholder cardholder)
        {
            try
            {
                cardholders.Add(cardholder.getTagEPC(), cardholder);
            } catch(Exception e) { }

        }

        /// <summary>
        /// Removes a cardholder for this ambient.
        /// If the cardholder is not in this ambient nothing happens.        
        /// </summary>
        /// <param name="cardholder">Cardholder.</param>
        public void removeCardholder(Cardholder cardholder)
        {
            if (cardholders.ContainsKey(cardholder.getTagEPC()))
            {
                cardholders.Remove(cardholder.getTagEPC());
            }
        }

    }
}
