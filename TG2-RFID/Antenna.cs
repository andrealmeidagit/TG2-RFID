using System;
using System.Collections;
using System.Collections.Generic;
using Impinj.OctaneSdk;

namespace TG2_RFID
{
    public class Antenna
    {
        /// <summary>
        /// Holds the source reader for this Antenna.
        /// </summary>
        protected ImpinjReader srcReader;

        /// <summary>
        /// Holds the port number of this Antenna.
        /// </summary>
        protected ushort srcAntennaPortNumber;

        /// <summary>
        /// Setter for the source reader for this antenna.
        /// </summary>
        /// <param name="newReader">New reader.</param>
        public void setReader(ImpinjReader newReader)
        {
            srcReader = newReader;
        }

        /// <summary>
        /// Setter for the antenna port number.
        /// </summary>
        /// <param name="newAntennaPortNumber">New antenna port number.</param>
        public void setAntennaPortNumber(ushort newAntennaPortNumber)
        {
            srcAntennaPortNumber = newAntennaPortNumber;
        }

        /// <summary>
        /// Getter for this antenna source reader.
        /// </summary>
        public ImpinjReader getReader()
        {
            return srcReader;
        }

        /// <summary>
        /// Getter for this antenna port number.
        /// </summary>
        public ushort getAntennaPortNumber()
        {
            return srcAntennaPortNumber;
        }

        public Antenna()
        {
            srcReader = null;
            srcAntennaPortNumber = 255;
        }

        /// <summary>
        /// Constructor given the antenna reader and antenna port name.
        /// </summary>
        /// <param name="reader">Reader.</param>
        /// <param name="antennaPortName">Antenna port name.</param>
        public Antenna(ImpinjReader reader, ushort antennaPortName)
        {
            srcReader = reader;
            srcAntennaPortNumber = antennaPortName;
        }
    }
}
