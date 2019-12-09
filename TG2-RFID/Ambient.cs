using System;
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
        /// Holds the total thermal load. 
        /// A value that is proportional to the number of people in the room.
        /// </summary>  
        protected int totalThermalLoad;

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
        public void SetAntenna(Antenna antenna)
        {
            localAntenna = antenna;
        }

        /// <summary>
        /// Getter for the total thermal capacity in this room.
        /// </summary>
        public int GetTotalThermalLoad()
        {
            return totalThermalLoad;
        }

        /// <summary>
        /// Getter for the total number of people inside of the ambient.
        /// </summary>
        public int GetNumberOfCarholdersInside()
        {
            return cardholders.Count;
        }

        /// <summary>
        /// Adds a cardholder for this ambient.
        /// </summary>
        /// <param name="cardholder">Cardholder.</param>
        public void AddCardholder(Cardholder cardholder)
        {
            try
            {
                cardholders.Add(cardholder.GetTagEPC(), cardholder);
            } 
            catch(Exception e)
            {
                // Handle .NET errors.
                Console.WriteLine("Exception : {0}", e.Message);
            }
        }

        /// <summary>
        /// Removes a cardholder for this ambient.
        /// If the cardholder is not in this ambient nothing happens.        
        /// </summary>
        /// <param name="cardholder">Cardholder.</param>
        public void RemoveCardholder(Cardholder cardholder)
        {
            if (cardholders.ContainsKey(cardholder.GetTagEPC()))
            {
                cardholders.Remove(cardholder.GetTagEPC());
            }
        }

        /// <summary>
        /// Getter ambient name       
        /// </summary>
        public string GetName()
        {
            return name;
        }
    }
}
