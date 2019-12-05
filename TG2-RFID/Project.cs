using System;
using System.Collections;
using System.Collections.Generic;
using Impinj.OctaneSdk;

namespace TG2_RFID
{
    public class Project
    {
        /// <summary>
        /// The RSSI low pass filter value.
        /// </summary>
        public static volatile int RSSILowPassFilter = -55;

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
        protected static volatile Dictionary<Tuple<String, ushort>, Transition> registerTransition = new Dictionary<Tuple<String, ushort>, Transition>();


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

        public static void PopulateProjectData()
        {
            Project.registeredPeople.Clear();
            /*Project.registeredPeople.Add("E200 001B 2609 0147 0510 7BBD", new Cardholder("Joao"));
            Project.registeredPeople.Add("E200 001B 2609 0147 0460 7BA5", new Cardholder("Maria"));*/
            Project.registeredPeople.Add("E200 001B 2609 0147 0520 7BB1", new Cardholder("Jose"));
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
        /// TODO
        /// </summary>
        public static void ReadingCardholderTag(Tag tag, String senderName)
        {
            registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder cardholder);

            cardholder.ReadingCardholderTag(tag, senderName);
        }


        // TODO
        // Aqui vamos processar a curva já populada!
        // Processa o cardholder data
        public static void ProcessCardholderData(Tag tag, string senderName)
        {
            //get curves
            registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder person);
            Tuple<string,ushort> antennaPersonAt = Tuple.Create<string, ushort>(senderName, tag.AntennaPortNumber);
            Transition transition = Project.GetTransitionInstance(antennaPersonAt);
            Tuple<string,ushort> otherAntenna = transition.GetOtherAntenna(antennaPersonAt);
            Curve powerCurveLastAntenna = person.GetPowerCurve(antennaPersonAt);
            Curve powerCurveOtherAntenna = person.GetPowerCurve(otherAntenna);
            Curve dopplerCurveLastAntenna = person.GetDopplerEffectCurve(antennaPersonAt);
            Curve dopplerCurveOtherAntenna = person.GetDopplerEffectCurve(otherAntenna);

            //compare powerCurve peaks
            Tuple<double, double> peakLastAntenna = powerCurveLastAntenna.GetCurveMaxPoint();
            Tuple<double, double> peakOtherAntenna = powerCurveOtherAntenna.GetCurveMaxPoint();
            //if (peakLastAntenna.Item1 > peakOtherAntenna.Item1)
            if (powerCurveLastAntenna.GetCurveLastValue() > powerCurveOtherAntenna.GetCurveLastValue())
            {
                //sets ambient to cardholder
                registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder cardholder);
                cardholder.SetCurrAmbient(transition.GetAmb4GivenAntenna(antennaPersonAt));
            }
            else
            {
                //sets ambient to cardholder
                registeredPeople.TryGetValue(tag.Epc.ToString(), out Cardholder cardholder);
                cardholder.SetCurrAmbient(transition.GetAmb4GivenAntenna(otherAntenna));
            }
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
