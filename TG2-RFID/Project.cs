﻿using System;
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
        protected static volatile Dictionary<Antenna, Ambient> registerAmbient = new Dictionary<Antenna, Ambient>();

        /// <summary>
        /// Registers a new cardholder given an tag epc.
        /// </summary>
        /// <param name="valueEPC">tag EPC as an string epc.</param>
        /// <param name="person">Person.</param>
        public static void RegisterNewCardholder(string valueEPC, Cardholder person)
        {
            person.setCardholderEPC(valueEPC);
            registeredPeople.Add(valueEPC, person);
        }

        /// <summary>
        /// Registers a new ambient given an antenna.
        /// </summary>
        /// <param name="antenna">Antenna.</param>
        /// <param name="ambient">Ambient.</param>
        public static void RegisterNewAmbient(Antenna antenna, Ambient ambient)
        {
            ambient.setAntenna(antenna);
            registerAmbient.Add(antenna, ambient);
        }

        public static void PopulateProjectData()
        {
            Project.registeredPeople.Clear();
            Project.registeredPeople.Add("E200 001B 2609 0147 0510 7BBD", new Cardholder("Joao"));
            Project.registeredPeople.Add("E200 001B 2609 0147 0460 7BA5", new Cardholder("Maria"));
            Project.registeredPeople.Add("E200 001B 2609 0147 0520 7BB1", new Cardholder("Jose"));
            Project.registeredPeople.Add("E200 001B 2609 0147 0450 7B99", new Cardholder("Arlindo"));
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
            Project.registeredPeople.Add("E200 001B 2609 0147 1040 7C81", new Cardholder("Marina"));
            //GlobalDataReader1.Cadastro.Add("AD08 3003 4604 3152 2C00 0086", "Tag exemplo impinj");
        }

        /// <summary>
        /// Checks whether the seen tag is registered.
        /// </summary>
        /// <returns><c>true</c>, if tag registered was ised, <c>false</c> otherwise.</returns>
        /// <param name="tag">Tag.</param>
        public static bool isTagRegistered(Tag tag)
        {
            return registeredPeople.ContainsKey(tag.Epc.ToString()); ;
        }

        /// <summary>
        /// TODO
        /// </summary>
        public static void readingCardholderTag()
        {

        }
    }
}