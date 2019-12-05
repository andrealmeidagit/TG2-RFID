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

        /// <summary>
        /// Setter for transition
        /// </summary>
        public Transition (Ambient amb1, String reader1, ushort ant1, Ambient amb2, String reader2, ushort ant2)
        {
            antenna1 = Tuple.Create<String, ushort>(reader1, ant1);
            antenna2 = Tuple.Create<String, ushort>(reader2, ant2);
            ambient1 = amb1;
            ambient2 = amb2;
        }

        /// <summary>
        /// Gets the second antenna, given the first one
        /// </summary>
        public Tuple<string, ushort> GetOtherAntenna(Tuple <string, ushort> givenAntenna)
        {
            if (antenna1.Item1 == givenAntenna.Item1 && antenna1.Item2 == givenAntenna.Item2)
            {
                return antenna2;
            }
            else
            {
                return antenna1;
            }
        }


        /// <summary>
        /// Getter for Ambient related to antenna
        /// </summary>
        public Ambient GetAmb4GivenAntenna(Tuple <string, ushort> ant)
        {
            if (ant.Item1 == antenna1.Item1 && ant.Item2 == antenna1.Item2)
            {
                return ambient1;
            } else if (ant.Item1 == antenna2.Item1 && ant.Item2 == antenna2.Item2)
            {
                return ambient2;
            } else
            {
                throw new Exception ();
            }
        }

    }
}
