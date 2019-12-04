using System;
using System.Collections.Generic;
using System.Text;

namespace TG2_RFID
{
    public class Transition
    {
        /// <summary>
        /// Holds the ambients the transition connects
        /// </summary> 
        protected Ambient ambient1, ambient2;

        /// <summary>
        /// Holds the antennas that make the transition
        /// </summary> 
        protected Tuple<String, ushort> antenna1, antenna2;

        /// <summary>
        /// Setter for the Ambients of the transition.       
        /// </summary>
        public void SetAmbients2Transition(Ambient amb1, Ambient amb2)
        {
            ambient1 = amb1;
            ambient2 = amb2;
        }

        /// <summary>
        /// Setter for the Antennas of the transition.       
        /// </summary>
        public void SetAntennas2Transition(String firstReader, ushort firstAntenna, String secndReader, ushort secndAntenna)
        {
            Tuple.Create<String, ushort>(firstReader, firstAntenna);
            Tuple.Create<String, ushort>(secndReader, secndAntenna);
        }

        /// <summary>
        /// Getter for first Ambient and Antenna
        /// </summary>
        public Tuple<Ambient, Tuple<String, ushort>> GetAtributes1stAmb()
        {
            return Tuple.Create<Ambient, Tuple<String, ushort>>(ambient1, antenna1);
        }

        /// <summary>
        /// Getter for second Ambient and Antenna
        /// </summary>
        public Tuple<Ambient, Tuple<String, ushort>> GetAtributes2ndAmb()
        {
            return Tuple.Create<Ambient, Tuple<String, ushort>>(ambient2, antenna2);
        }

        public Transition (Ambient amb1, String reader1, ushort ant1, Ambient amb2, String reader2, ushort ant2)
        {
            Tuple.Create<String, ushort>(reader1, ant1);
            Tuple.Create<String, ushort>(reader2, ant2);
            ambient1 = amb1;
            ambient2 = amb2;
        }

        public Tuple<string, ushort> GetOtherAntenna(Tuple <string, ushort> givenAntenna)
        {
            if (antenna1 == givenAntenna)
            {
                return antenna2;
            }
            else
            {
                return antenna1;
            }
        }
    }
}
