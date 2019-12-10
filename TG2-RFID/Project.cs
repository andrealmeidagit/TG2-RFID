using System;
using System.Collections;
using System.Collections.Generic;
using Impinj.OctaneSdk;


namespace TG2_RFID
{
    public class Project
    {
        public static ushort realAmbient = 0;

        /// <summary>
        /// The map of registered people in the project.
        /// </summary>
        protected static volatile Dictionary<string, Cardholder> registeredPeople = new Dictionary<string, Cardholder>();

        /// <summary>
        /// The map of registed ambients in the project.
        /// </summary>
        protected static volatile Dictionary<ushort, Ambient> registerAmbient = new Dictionary<ushort, Ambient>();

        /// <summary>
        /// The map of registed transitions in the project.
        /// </summary>
        protected static volatile Dictionary<Tuple<string, ushort>, Transition> registerTransition = new Dictionary<Tuple<string, ushort>, Transition>();


        /// <summary>
        /// Registers a new cardholder given an tag epc.
        /// </summary>
        /// <param name="valueEPC">tag EPC as an string epc.</param>
        /// <param name="person">Person.</param>
        public static void RegisterNewCardholder(string valueEPC, Cardholder person)
        {
            person.SetCardholderEPC(valueEPC);
            registeredPeople.Add(valueEPC, person);
        }

        /// <summary>
        /// Registers a new ambient given an antenna.
        /// </summary>
        /// <param name="antenna">Antenna.</param>
        /// <param name="ambient">Ambient.</param>
        public static void RegisterNewAmbient(ushort roomNumber, Ambient ambient)
        {
            registerAmbient.Add(roomNumber, ambient);
        }

        /// <summary>
        /// Registers a new ambient given an antenna.
        /// </summary>
        /// <param name="antenna">Antenna.</param>
        /// <param name="transition">Ambient.</param>
        public static void RegisterNewTransition(Tuple<String, ushort> antenna, Transition transition)
        {
            registerTransition.Add(antenna, transition);
        }

        public static void RegisterNewCardholder(string EPC, string name)
        {
            var cardholder = new Cardholder(name);
            Project.registeredPeople.Add(EPC, cardholder);
            foreach (var transition in Project.registerTransition.Values)
            {
                var ant1 = transition.GetAtributes1stAmb().Item2;
                var ant2 = transition.GetAtributes2ndAmb().Item2;
                if (!cardholder.curvesPowerReadingsDictionary.ContainsKey(ant1))
                {
                    cardholder.curvesPowerReadingsDictionary.Add(ant1, new Curve());
                }
                if (!cardholder.curvesPowerReadingsDictionary.ContainsKey(ant2))
                {
                    cardholder.curvesPowerReadingsDictionary.Add(ant2, new Curve());
                }
                if (!cardholder.curvesDoplerFrequencyReadingsDictionary.ContainsKey(ant1))
                {
                    cardholder.curvesDoplerFrequencyReadingsDictionary.Add(ant1, new Curve());
                }
                if (!cardholder.curvesDoplerFrequencyReadingsDictionary.ContainsKey(ant2))
                {
                    cardholder.curvesDoplerFrequencyReadingsDictionary.Add(ant2, new Curve());
                }
            }

        }

        public static void PopulateProjectCardholders()
        {
            Project.registeredPeople.Clear();
            /*Project.registeredPeople.Add("E200 001B 2609 0147 0510 7BBD", new Cardholder("Joao"));
            Project.registeredPeople.Add("E200 001B 2609 0147 0460 7BA5", new Cardholder("Maria"));*/
            Project.RegisterNewCardholder("E200 001B 2609 0147 0520 7BB1", "Jose");
            /*Project.registeredPeople.Add("E200 001B 2609 0147 0450 7B99", new Cardholder("Arlindo"));
            Project.registeredPeople.Add("E200 001B 2609 0147 0380 7B85", new Cardholder("Manoel"));
            Project.registeredPeople.Add("E200 001B 2609 0147 0910 7C5D", new Cardholder("Carla"));
            Project.registeredPeople.Add("E200 001B 2609 0147 0850 7C39", new Cardholder("Julia"));
            Project.registeredPeople.Add("E200 001B 2609 0147 0840 7C31", new Cardholder("Edgar"));
            Project.registeredPeople.Add("E200 001B 2609 0147 0710 7C0D", new Cardholder("Jose"));
            Project.registeredPeople.Add("E200 001B 2609 0147 0660 7BF5", new Cardholder("Arlindo"));
            Project.registeredPeople.Add("E200 001B 2609 0147 0900 7C55", new Cardholder("Getulio"));
            Project.registeredPeople.Add("E200 001B 2609 0147 0390 7B8D", new Cardholder("Laura"));
            Project.registeredPeople.Add("E200 001B 2609 0147 0780 7C25", new Cardholder("Maria Beatriz"));
            Project.registeredPeople.Add("E200 001B 2609 0147 0720 7C01", new Cardholder("Andre"));
            Project.registeredPeople.Add("E200 001B 2609 0147 0600 7BD1", new Cardholder("Anastacia"));
            Project.registeredPeople.Add("E200 001B 2609 0147 1100 7CA5", new Cardholder("Lucas"));
            Project.registeredPeople.Add("E200 001B 2609 0147 0650 7BE9", new Cardholder("Renato"));
            Project.registeredPeople.Add("E200 001B 2609 0147 0790 7C2D", new Cardholder("Jesse"));
            Project.registeredPeople.Add("E200 001B 2609 0147 0590 7BDD", new Cardholder("Artur"));
            Project.registeredPeople.Add("E200 001B 2609 0147 1040 7C81", new Cardholder("Marina"));*/
            //GlobalDataReader1.Cadastro.Add("AD08 3003 4604 3152 2C00 0086", "Tag exemplo impinj");
        }

        /// <summary>
        /// Checks whether the seen tag is registered.
        /// </summary>
        /// <returns><c>true</c>, if tag registered was ised, <c>false</c> otherwise.</returns>
        /// <param name="tag">Tag.</param>
        public static bool IsTagRegistered(Tag tag)
        {
            return registeredPeople.ContainsKey(tag.Epc.ToString()); ;
        }

        /// <summary>
        /// Gets cardholder from tag
        /// </summary>
        public static void ReadingCardholderTag(Tag tag, String senderName)
        {
            registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder cardholder);

            cardholder.ReadingCardholderTag(tag, senderName);
        }

        public static void ProcessDataGiveTransition(Transition transition, Tuple<string, ushort> antennaPersonAt, Cardholder person, Tag tag, string senderName)
        {
            var otherAntenna = transition.GetOtherAntenna(antennaPersonAt);
            var powerCurveLastAntenna = person.GetPowerCurve(antennaPersonAt);
            var powerCurveOtherAntenna = person.GetPowerCurve(otherAntenna);
            var dopplerCurveLastAntenna = person.GetDopplerEffectCurve(antennaPersonAt);
            var dopplerCurveOtherAntenna = person.GetDopplerEffectCurve(otherAntenna);

            /*
             * Compare Peaks Time
             */

            /*
             * Compare Peaks Time and value
             */

            /*
             * Compare last value RSSI
             */

            /*
             * Compare mean / median
             */

            /*
             * Compare Doppler transition point
             */

            // Compare powerCurve peaks
            var peakListLast = powerCurveLastAntenna.CalculatePeaks();
            var peakListOther = powerCurveOtherAntenna.CalculatePeaks();
            var maxLastAntenna = powerCurveLastAntenna.GetCurveMaxPoint();
            var maxOtherAntenna = powerCurveOtherAntenna.GetCurveMaxPoint();


            /*
             * Compare RSSI Peaks Time
             */
            if (powerCurveLastAntenna.CompareCurveLastPeak(powerCurveOtherAntenna))
            {
                // sets ambient to cardholder
                registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder cardholder);
                cardholder.SetAmbient(transition.GetAmb4GivenAntenna(antennaPersonAt), 0);
            }
            else
            {
                // sets ambient to cardholder
                registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder cardholder);
                cardholder.SetAmbient(transition.GetAmb4GivenAntenna(otherAntenna), 0);
            }

            /*
             * Compare Peaks Time and value
             */
            //if ((peakListLast.Count > 0 && peakListOther.Count > 0 && (peakListLast[peakListLast.Count - 1].Item2 > peakListOther[peakListOther.Count - 1].Item2))
            //    || (peakListLast.Count == 0 || peakListOther.Count == 0 && maxLastAntenna.Item1 > maxOtherAntenna.Item1 && maxLastAntenna.Item2 > maxOtherAntenna.Item2))
            //{
            //    // sets ambient to cardholder
            //    registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder cardholder);
            //    cardholder.SetAmbient(transition.GetAmb4GivenAntenna(antennaPersonAt),1);
            //}
            //else
            //{
            //    // sets ambient to cardholder
            //    registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder cardholder);
            //    cardholder.SetAmbient(transition.GetAmb4GivenAntenna(otherAntenna),1);
            //}

            /*
			 * Compare last value RSSI
			 */
            if (powerCurveLastAntenna.CompareCurveLastPeak(powerCurveOtherAntenna))
            {
                //sets ambient to cardholder
                registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder cardholder);
                cardholder.SetAmbient(transition.GetAmb4GivenAntenna(antennaPersonAt), 2);
            }
            else
            {
                //sets ambient to cardholder
                registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder cardholder);
                cardholder.SetAmbient(transition.GetAmb4GivenAntenna(otherAntenna), 2);
            }

            /*
			 * Compare mean
			 */
            if (powerCurveLastAntenna.CompareCurveMeans(powerCurveOtherAntenna))
            {
                //sets ambient to cardholder
                registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder cardholder);
                cardholder.SetAmbient(transition.GetAmb4GivenAntenna(antennaPersonAt), 3);
            }
            else
            {
                //sets ambient to cardholder
                registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder cardholder);
                cardholder.SetAmbient(transition.GetAmb4GivenAntenna(otherAntenna), 3);
            }

            /*
			 * Compare meadian
			 */
            if (powerCurveLastAntenna.CompareCurveMedians(powerCurveOtherAntenna))
            {
                //sets ambient to cardholder
                registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder cardholder);
                cardholder.SetAmbient(transition.GetAmb4GivenAntenna(antennaPersonAt), 4);
            }
            else
            {
                //sets ambient to cardholder
                registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder cardholder);
                cardholder.SetAmbient(transition.GetAmb4GivenAntenna(otherAntenna), 4);
            }

            /*
			 * Compare Doppler transition point
			 */
            if (!Double.IsNaN(dopplerCurveLastAntenna.CalculateCrossingPoint().Item1) &&
                !Double.IsNaN(dopplerCurveOtherAntenna.CalculateCrossingPoint().Item1) &&
                dopplerCurveLastAntenna.CalculateCrossingPoint().Item1 > dopplerCurveOtherAntenna.CalculateCrossingPoint().Item1)
            {
                //sets ambient to cardholder
                registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder cardholder);
                cardholder.SetAmbient(transition.GetAmb4GivenAntenna(antennaPersonAt), 5);
            }
            else if (!Double.IsNaN(dopplerCurveLastAntenna.CalculateCrossingPoint().Item1) &&
              !Double.IsNaN(dopplerCurveOtherAntenna.CalculateCrossingPoint().Item1) &&
              dopplerCurveLastAntenna.CalculateCrossingPoint().Item1 < dopplerCurveOtherAntenna.CalculateCrossingPoint().Item1)
            {
                //sets ambient to cardholder
                registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder cardholder);
                cardholder.SetAmbient(transition.GetAmb4GivenAntenna(otherAntenna), 5);
            }
            else
            {
                //if ((peakListLast.Count > 0 && peakListOther.Count > 0 && (peakListLast[peakListLast.Count - 1].Item1 > peakListOther[peakListOther.Count - 1].Item1))
                //|| (peakListLast.Count == 0 || peakListOther.Count == 0 && maxLastAntenna.Item1 > maxOtherAntenna.Item1 && maxLastAntenna.Item2 > maxOtherAntenna.Item2))
                //{
                //    // sets ambient to cardholder
                //    registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder cardholder);
                //    cardholder.SetAmbient(transition.GetAmb4GivenAntenna(antennaPersonAt), 5);
                //}
                //else
                //{
                //    // sets ambient to cardholder
                //    registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder cardholder);
                //    cardholder.SetAmbient(transition.GetAmb4GivenAntenna(otherAntenna), 5);
                //}
            }


            /*
			 * Compare Doppler transition point and RSSI peaks
			 */

            if ((peakListLast.Count > 0 && peakListOther.Count > 0 && (peakListLast[peakListLast.Count - 1].Item1 > peakListOther[peakListOther.Count - 1].Item1) &&
                (!Double.IsNaN(dopplerCurveLastAntenna.CalculateCrossingPoint().Item1) &&
                !Double.IsNaN(dopplerCurveOtherAntenna.CalculateCrossingPoint().Item1) &&
                dopplerCurveLastAntenna.CalculateCrossingPoint().Item1 > dopplerCurveOtherAntenna.CalculateCrossingPoint().Item1)))
            //|| (peakListLast.Count == 0 || peakListOther.Count == 0 && maxLastAntenna.Item1 > maxOtherAntenna.Item1 && maxLastAntenna.Item2 > maxOtherAntenna.Item2))
            {
                // sets ambient to cardholder
                registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder cardholder);
                cardholder.SetAmbient(transition.GetAmb4GivenAntenna(antennaPersonAt), 6);
            }
            else if ((peakListLast.Count > 0 && peakListOther.Count > 0 && (peakListLast[peakListLast.Count - 1].Item1 <= peakListOther[peakListOther.Count - 1].Item1) &&
                (!Double.IsNaN(dopplerCurveLastAntenna.CalculateCrossingPoint().Item1) &&
                !Double.IsNaN(dopplerCurveOtherAntenna.CalculateCrossingPoint().Item1) &&
                dopplerCurveLastAntenna.CalculateCrossingPoint().Item1 <= dopplerCurveOtherAntenna.CalculateCrossingPoint().Item1)))
            {
                // sets ambient to cardholder
                registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder cardholder);
                cardholder.SetAmbient(transition.GetAmb4GivenAntenna(otherAntenna), 6);
            }







            //if (powerCurveLastAntenna.GetSize() > 4 ||
            //    powerCurveLastAntenna.GetSize() > 4)
            //{
            //    int TESTANDO = 0;
            //}

            //if ((peakListLast.Count > 0 && peakListOther.Count > 0 && (peakListLast[peakListLast.Count - 1].Item1 > peakListOther[peakListOther.Count - 1].Item1))
            //    || (peakListLast.Count == 0 || peakListOther.Count == 0 && maxLastAntenna.Item1 > maxOtherAntenna.Item1))
            //if (maxLastAntenna.Item1 > maxOtherAntenna.Item1)
            //if (powerCurveLastAntenna.CalculateMeanY() > powerCurveOtherAntenna.CalculateMeanY())
            //if (powerCurveLastAntenna.GetCurveLastValue() > powerCurveOtherAntenna.GetCurveLastValue())
            //{
            //    sets ambient to cardholder
            //    registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder cardholder);
            //    cardholder.SetCurrAmbient(transition.GetAmb4GivenAntenna(antennaPersonAt));
            //}
            //else
            //{
            //    sets ambient to cardholder
            //    registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder cardholder);
            //    cardholder.SetCurrAmbient(transition.GetAmb4GivenAntenna(otherAntenna));
            //}

            person.SetCurrAmbient(Project.GetAmbientInstance(Project.realAmbient));
        }

        // TODO
        // Aqui vamos processar a curva já populada!
        // Processa o cardholder data
        public static void ProcessCardholderData(Tag tag, string senderName)
        {
            //get curves
            registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder person);
            var antennaPersonAt = Tuple.Create<string, ushort>(senderName, tag.AntennaPortNumber);
            var transition = Project.GetTransitionInstance(antennaPersonAt);

            var ambient = transition.GetAmb4GivenAntenna(antennaPersonAt);

            Tuple.Create<string, ushort>("Reader #1", 2);
            Tuple.Create<string, ushort>("Reader #2", 1);
            Tuple.Create<string, ushort>("Reader #2", 2);
            Tuple.Create<string, ushort>("Reader #3", 1);
            Tuple.Create<string, ushort>("Reader #3", 2);

            if (ambient.GetName() == ("Area_Externa(0)") && 
                transition != Project.GetTransitionInstance(Tuple.Create<string, ushort>("Reader #1", 1)))
            {
                return;
            } 
            else if (ambient.GetName() == ("Sala_Reuniao(2)") &&
                transition != Project.GetTransitionInstance(Tuple.Create<string, ushort>("Reader #2", 1)))
            {
                return;
            }
            else if (ambient.GetName() == ("Corredor_Baias(3)") &&
                transition != Project.GetTransitionInstance(Tuple.Create<string, ushort>("Reader #3", 2)))
            {
                return;
            }
            //else if (ambient.GetName() == ("Sala_Principal(1)") 



            ProcessDataGiveTransition(transition, antennaPersonAt, person, tag, senderName);
        }

        /// <summary>
        /// Returns Ambient type instance from dictionary according to key given
        /// </summary>
        public static Ambient GetAmbientInstance (ushort ambientKey)
        {
            registerAmbient.TryGetValue(ambientKey, out Ambient ambientInstance);
            return ambientInstance;
        }

        /// <summary>
        /// Returns Transition type instance from dictionary according to key given
        /// </summary>
        public static Transition GetTransitionInstance(Tuple<string, ushort> antennaID)
        {
            registerTransition.TryGetValue(antennaID, out Transition transitionInstance);
            return transitionInstance;
        }

        /// <summary>
        /// Getter people
        /// </summary>
        /// 
        public static Cardholder GetCardholder(string tag)
        {
            registeredPeople.TryGetValue(tag, out Cardholder cardholderObj);
            return cardholderObj;
        }






    }
}
