﻿/*
 * Trabalho de Graduação II - Controle antecipativo de sistemas de conforto térmico usando RFID
 * 
 * Autor: André Abreu Rodrigues de Almeida
 *        Graduando em Engenharia Mecatrônica
 *        Matrícula: 12/0007100
 * Departamento de Engenharia Elétrica
 * Universidade de Brasília - UnB
 * 
 * 
 * Brasília, Agosto a Dezembro de 2019
 * 
 */

/* Aviso: A biblioteca 'Octane SDK' foi desenvolvida e é propriedade da empresa IMPINJ, INC., e está sendo utilizada com base na licença de software aberto descrita a seguir
 * 
 * Para mais informações, visitar o site: https://support.impinj.com/hc/en-us/articles/360000468370-Software-Tools-License-Disclaimer
 */


/*PLEASE READ THE FOLLOWING LICENSE & DISCLAIMER (“AGREEMENT”) CAREFULLY BEFORE USING ANY SOFTWARE TOOLS (AS DEFINED BELOW) MADE AVAILABLE TO YOU (“LICENSEE”) BY IMPINJ, INC. (“IMPINJ”). BY USING THE SOFTWARE TOOLS, YOU ACKNOWLEDGE THAT YOU HAVE READ AND UNDERSTOOD ALL THE TERMS AND CONDITIONS OF THE AGREEMENT, YOU WILL BE CONSENTING TO BE BOUND BY THEM, AND YOU ARE AUTHORIZED TO DO SO. IF YOU DO NOT ACCEPT THESE TERMS AND CONDITIONS, DO NOT USE THE SOFTWARE TOOLS.

1. PURPOSE OF AGREEMENT. From time to time, Impinj technical personnel may make available to Licensee certain software, including code (in source and object form), tools, libraries, configuration files, translations, and related documentation (collectively, “Software Tools”), upon specific request or to assist with a specific deployment. This Agreement sets forth Licensee's limited rights and Impinj's limited obligations with respect to the Software Tools. Licensee acknowledges that Impinj provides the Software Tools free of charge. This Agreement does not grant any rights with respect to Impinj standalone software products (e.g., ItemSense, ItemEncode, SpeedwayConnect) or the firmware on Impinj hardware, all of which are subject to separate license terms.

2. LIMITED LICENSE. Subject to the terms and conditions of this Agreement, Impinj hereby grants to Licensee a limited, royalty-free, worldwide, non-exclusive, perpetual and irrevocable (except as set forth below), non-transferable license, without right of sublicense, to (a) use the Software Tools and (b) only with respect to Software Tools provided in source code form, modify and create derivative works of such Software Tools, in each case, solely for Licensee’s internal development related to the deployment of Impinj products (“Purpose”). The Software Tools may only be used by employees of Licensee that must have access to the Software Tools in connection with the Purpose.

3. TERMINATION. Impinj may immediately terminate this Agreement if Licensee breaches any provision hereof. Upon the termination of this Agreement, Licensee must (a) discontinue all use of the Software Tools, (b) uninstall the Software Tools from its systems, (c) destroy or return to Impinj all copies of the Software Tools and any other materials provided by Impinj, and (d) promptly provide Impinj with written confirmation (including via email) of Licensee’s compliance with these provisions. Sections 4-10 will survive termination of this Agreement.

4. OWNERSHIP. The Software Tools are licensed, not sold, by Impinj to Licensee. Impinj and its suppliers own and retain all right, title, and interest, including all intellectual property rights, in and to the Software Tools. Except for those rights expressly granted in this Agreement, no other rights are granted, either express or implied, to Licensee. Impinj reserves the right to develop, price and sell software products that have features similar to or competitive with Software Tools. Licensee grants Impinj a limited, royalty-free, worldwide, perpetual and irrevocable, transferable, sublicensable, license to Licensee’s derivative works of Software Tools; provided that Licensee has no obligation under this Agreement to deliver to Impinj any such derivative works.

5. CONFIDENTIALITY. In order to protect the trade secrets and proprietary know-how contained in the Software Tools, Licensee will not decompile, disassemble, or reverse engineer, or otherwise attempt to gain access to the source code or algorithms of the Software Tools (unless Impinj provides the Software Tools in source code format). Licensee will maintain the confidentiality of and not disclose to any third party: (a) all non-public information disclosed by Impinj to Licensee under this Agreement and (b) all performance data and all other information obtained through the Software Tools.

6. WARRANTY DISCLAIMER. LICENSEE ACKNOWLEDGES THAT IMPINJ PROVIDES THE SOFTWARE TOOLS FREE OF CHARGE AND ONLY FOR THE PURPOSE. ACCORDINGLY, THE SOFTWARE TOOLS ARE PROVIDED “AS IS” WITHOUT QUALITY CHECK, AND IMPINJ DOES NOT WARRANT THAT THE SOFTWARE TOOLS WILL OPERATE WITHOUT ERROR OR INTERRUPTION OR MEET ANY PERFORMANCE STANDARD OR OTHER EXPECTATION. IMPINJ EXPRESSLY DISCLAIMS ALL WARRANTIES, EXPRESS OR IMPLIED, INCLUDING THE IMPLIED WARRANTIES OF MERCHANTABILITY, NONINFRINGEMENT, QUALITY, ACCURACY, AND FITNESS FOR A PARTICULAR PURPOSE. IMPINJ IS NOT OBLIGATED IN ANY WAY TO PROVIDE SUPPORT OR OTHER MAINTENANCE WITH RESPECT TO THE SOFTWARE TOOLS.

7. LIMITATION OF LIABILITY. THE TOTAL LIABILITY OF IMPINJ ARISING OUT OF OR RELATED TO THE SOFTWARE TOOLS WILL NOT EXCEED THE TOTAL AMOUNT PAID BY LICENSEE TO IMPINJ PURSUANT TO THIS AGREEMENT. IN NO EVENT WILL IMPINJ HAVE LIABILITY FOR ANY INDIRECT, INCIDENTAL, SPECIAL, OR CONSEQUENTIAL DAMAGES, EVEN IF ADVISED OF THE POSSIBILITY OF THESE DAMAGES. THESE LIMITATIONS WILL APPLY NOTWITHSTANDING ANY FAILURE OF ESSENTIAL PURPOSE OF ANY LIMITED REMEDY IN THIS AGREEMENT.

8. THIRD PARTY SOFTWARE. The Software Tools may contain software created by a third party. Licensee’s use of any such third party software is subject to the applicable license terms and this Agreement does not alter those license terms. Licensee may not subject any portion of the Software Tools to an open source license.

9. RESTRICTED USE. Licensee will comply with all applicable laws and regulations to preclude the acquisition by any governmental agency of unlimited rights to technical data, software, and documentation provided with Software Tools, and include the appropriate “Restricted Rights” or “Limited Rights” notices required by the applicable U.S. or foreign government agencies. Licensee will comply in all respects with all U.S. and foreign export and re-export laws and regulations applicable to the technology and documentation provided hereunder.

10. MISCELLANEOUS. This Agreement will be governed by the laws of the State of Washington, U.S.A without reference to conflict of law principles. All disputes arising out of or related to it, will be subject to the exclusive jurisdiction of the state and federal courts located in King County, Washington, and the parties agree and submit to the personal and exclusive jurisdiction and venue of these courts. Licensee will not assign this Agreement, directly or indirectly, by operation of law or otherwise, without the prior written consent of Impinj. This Agreement (and any applicable nondisclosure agreement) is the entire agreement between the parties relating to the Software Tools. No waiver or modification of this Agreement will be valid unless contained in a writing signed by each party.
*/


using System;
using Impinj.OctaneSdk;
using System.Collections;
using System.Collections.Generic;

namespace TG2_RFID
{
    class GlobalDataReader1
    {
        public static int RSSILowPassFilter = -55;
        public static volatile int reader_1_count = 0;
        public static volatile int reader_2_count = 0;
        public static volatile int reader_3_count = 0;
        public static volatile Hashtable SalaReuniao = new Hashtable();
        public static volatile Hashtable CorredorMesas = new Hashtable();
        public static volatile Hashtable Cadastro = new Hashtable();
    }
    class Program
    {
        // Create a collection to hold all the ImpinjReader instances.
        static List<ImpinjReader> readers = new List<ImpinjReader>();

        static void Main(/*string[] args*/)
        {
            try
            {
                // Connect to the reader.
                // Pass in a reader hostname or IP address as a 
                // command line argument when running the example
                /*if (args.Length != 2)
                {
                    Console.WriteLine("Error: No hostname specified.  Pass in two reader hostnames as command line arguments when running the Sdk Example.");
                    return;
                }
                string hostname1 = args[0];
                string hostname2 = args[1];*/
 //               string hostname1 = "speedwayr-10-9f-bb.local";
                string hostname2 = "speedwayr-10-9f-c8.local";
 //               string hostname3 = "speedwayr-10-9f-3f.local";
                // Create two reader instances and add them to the List of readers.
//                readers.Add(new ImpinjReader(hostname1, "Reader #1"));
                readers.Add(new ImpinjReader(hostname2, "Reader #2"));
//                readers.Add(new ImpinjReader(hostname3, "Reader #3"));

                // Loop through the List of readers to configure and start them.
                foreach (ImpinjReader reader in readers)
                {
                    // Connect to the reader
                    reader.Connect();

                    // Get the default settings
                    // We'll use these as a starting point
                    // and then modify the settings we're 
                    // interested in.
                    Settings settings = reader.QueryDefaultSettings();

                    settings.Report.IncludeAntennaPortNumber = true;
                    settings.Report.IncludeFirstSeenTime = true;
                    settings.Report.IncludeLastSeenTime = true;
                    settings.Report.IncludeSeenCount = true;
                    settings.Report.IncludeDopplerFrequency = true;
                    settings.Report.IncludePeakRssi = true;

                    //Settings de antena
                    settings.Antennas.DisableAll();
                    settings.Antennas.GetAntenna(1).IsEnabled = true;
                    settings.Antennas.GetAntenna(2).IsEnabled = true;
                    // Set all the antennas to the max transmit power and receive sensitivity
                    settings.Antennas.TxPowerMax = true;
                    settings.Antennas.RxSensitivityMax = true;
                    // Or set all antennas to a specific value in dBm
                    //settings.Antennas.TxPowerInDbm = 28.0;
                    //settings.Antennas.RxSensitivityInDbm = -70.0;
                    // Or set each antenna individually
                    //settings.Antennas.GetAntenna(1).MaxTxPower = true;
                    //settings.Antennas.GetAntenna(1).MaxRxSensitivity = true;
                    //settings.Antennas.GetAntenna(2).TxPowerInDbm = 30.0;
                    //settings.Antennas.GetAntenna(2).RxSensitivityInDbm = -70.0;
                    // ...

                    // Send a tag report every time the reader stops (period is over).
                    settings.Report.Mode = ReportMode.Individual;// BatchAfterStop;

                    // Reading tags for 1 seconds every 0.25 second
                    //settings.AutoStart.Mode = AutoStartMode.Periodic;
                    //settings.AutoStart.PeriodInMs = 500;
                    //settings.AutoStop.Mode = AutoStopMode.Duration;
                    //settings.AutoStop.DurationInMs = 500;

                    settings.ReaderMode = ReaderMode.AutoSetDenseReader;
                    settings.TagPopulationEstimate = 20;


                    // Apply the newly modified settings.
                    reader.ApplySettings(settings);

                    RealizaCadastro();

                    // Assign the TagsReported event handler.
                    // This specifies which method to call
                    // when tags reports are available.
                    reader.TagsReported += Captura_tags;

                    // Start reading.
                    reader.Start();
                }

                // Wait for the user to press enter.
                Console.WriteLine("Press enter to exit.");
                Console.ReadKey();

                // Stop all the readers and disconnect from them.
                foreach (ImpinjReader reader in readers)
                {
                    try
                    {
                        reader.Stop();
                        reader.Disconnect();
                    }
                    catch (OctaneSdkException) { }
                }
            }
            catch (OctaneSdkException e)
            {
                // Handle Octane SDK errors.
                Console.WriteLine("Octane SDK exception: {0}", e.Message);
            }
            catch (Exception e)
            {
                // Handle other .NET errors.
                Console.WriteLine("Exception : {0}", e.Message);
            }
        }

        /* private static void OnTagsReported(ImpinjReader sender, TagReport report)
         {
             // This event handler is called asynchronously 
             // when tag reports are available.
             // Loop through each tag in the report 
             // and print the data.
             foreach (Tag tag in report)
             {
                 if (tag.Epc.ToString() == "AD08 3003 4604 3152 2C00 0086" /* "AD12 2100 16E2 2BFF 6A00 0009")
                 {
                     // The Tag report contains an EPC with Spaces so we need to remove those spaces.
                     string trimmedEPC = tag.Epc.ToString().Replace(" ", string.Empty);
                     // Calculates the elapsed time between first and last seen times
                     TimeSpan deltaT = new TimeSpan(tag.LastSeenTime.LocalDateTime.Ticks - tag.FirstSeenTime.LocalDateTime.Ticks);
                     Console.WriteLine("Name: {0}, Address: {1}, EPC: {2}, TAGSC: {3}, TotalSeconds: {4}, RSSI: {5}",
                       sender.Name, sender.Address, /*tag.AntennaPortNumber, trimmedEPC, /*tag.FirstSeenTime.ToString(),
                                    tag.LastSeenTime.ToString(), tag.TagSeenCount, deltaT.TotalSeconds, tag.PeakRssiInDbm );
                                    /*(sender.Name == "Reader #1" ? GlobalDataReader1.reader_1_count++ : (sender.Name == "Reader #2" ? GlobalDataReader1.reader_2_count++ : (sender.Name == "Reader #3" ? GlobalDataReader1.reader_3_count++ : -1))));
                 }
             }
             //Console.WriteLine("\n");
         }*/
        private static void Captura_tags(ImpinjReader sender, TagReport report)
        {
            foreach (Tag tag in report)
            {
                /* if (tag.Epc.ToString() == "E200 001B 2609 0147 0510 7BBD" ||
                     tag.Epc.ToString() == "E200 001B 2609 0147 0460 7BA5" ||
                     tag.Epc.ToString() == "E200 001B 2609 0147 0520 7BB1" ||
                     tag.Epc.ToString() == "E200 001B 2609 0147 0450 7B99" ||
                     tag.Epc.ToString() == "E200 001B 2609 0147 0380 7B85" ||
                     tag.Epc.ToString() == "E200 001B 2609 0147 0910 7C5D" ||
                     tag.Epc.ToString() == "E200 001B 2609 0147 0850 7C39" ||
                     tag.Epc.ToString() == "E200 001B 2609 0147 0840 7C31" ||
                     tag.Epc.ToString() == "E200 001B 2609 0147 0710 7C0D" ||
                     tag.Epc.ToString() == "E200 001B 2609 0147 0660 7BF5" ||
                     tag.Epc.ToString() == "E200 001B 2609 0147 0900 7C55" ||
                     tag.Epc.ToString() == "E200 001B 2609 0147 0390 7B8D" ||
                     tag.Epc.ToString() == "E200 001B 2609 0147 0780 7C25" ||
                     tag.Epc.ToString() == "E200 001B 2609 0147 0720 7C01" ||
                     tag.Epc.ToString() == "E200 001B 2609 0147 0600 7BD1" ||
                     tag.Epc.ToString() == "E200 001B 2609 0147 1100 7CA5" ||
                     tag.Epc.ToString() == "E200 001B 2609 0147 0650 7BE9" ||
                     tag.Epc.ToString() == "E200 001B 2609 0147 0790 7C2D" ||
                     tag.Epc.ToString() == "E200 001B 2609 0147 0590 7BDD" ||
                     tag.Epc.ToString() == "E200 001B 2609 0147 1040 7C81")*/
                if (GlobalDataReader1.Cadastro.ContainsKey(tag.Epc.ToString()))
                {
                    string trimmedEPC = tag.Epc.ToString().Replace(" ", string.Empty);
                    TimeSpan deltaT = new TimeSpan(tag.LastSeenTime.LocalDateTime.Ticks - tag.FirstSeenTime.LocalDateTime.Ticks);

                    Console.WriteLine("Antena: {0}, EPC: {1}, TAGSC: {2}, RSSI: {3}, LastSeen: {4}", tag.AntennaPortNumber, trimmedEPC, tag.TagSeenCount, tag.PeakRssiInDbm, tag.LastSeenTime);

                    if (GlobalDataReader1.SalaReuniao.ContainsKey(tag.Epc.ToString()))
                    {
                        SegundaLeituraSalaReuniao(sender.Name, tag.Epc.ToString(), tag.PeakRssiInDbm);
                    }
                    else if (GlobalDataReader1.CorredorMesas.ContainsKey(tag.Epc.ToString()))
                    {
                        SegundaLeituraCorredorMesas(sender.Name, tag.Epc.ToString(), tag.PeakRssiInDbm);
                    }
                    else
                    {
                        PrimeiraLeitura(sender.Name, tag.Epc.ToString(), tag.PeakRssiInDbm);
                    }
                }
            }
            //       Console.WriteLine("\n");
            /*DateTime now = DateTime.Now;
            //Console.WriteLine("Em Brasília, {0}", DateTime.Now.ToString("F")/*now.ToString("F"));
            Console.WriteLine("Today's date: {0}", now.Date);
            //WriteLine("Today is {0} day of {1}", now.Day, months[now.Month - 1]);
            Console.WriteLine("Today is {0} day of {1}", now.DayOfYear, now.Year);
            Console.WriteLine("Today's time: {0}", now.TimeOfDay);
            Console.WriteLine("Hour: {0}", now.Hour);
            Console.WriteLine("Minute: {0}", now.Minute);
            Console.WriteLine("Second: {0}", now.Second);
            Console.WriteLine("Millisecond: {0}", now.Millisecond);
            Console.WriteLine("The day of week: {0}", now.DayOfWeek);
            Console.WriteLine("Kind: {0}\n\n", now.Kind);*/
        }

        private static void SegundaLeituraSalaReuniao(string name, string EPC, double PeakRssi)
        {
            if (name == "Reader #1")
            {
                return;
            }
            else if (name == "Reader #3")
            {
               // GlobalDataReader1.

            }
            else if (name == "Reader #2")
            {
                GlobalDataReader1.SalaReuniao.Remove(EPC);
            }
            else
            {
                Console.Error.Write("[ERRO!] Leitora não cadastrada!");
                Environment.Exit(1);
            }

        }

        private static void SegundaLeituraCorredorMesas(string name, string EPC, double PeakRssi)
        {

        }

        private static void PrimeiraLeitura(string name, string EPC, double PeakRssi)
        {

            if (name == "Reader #2")
            {
                return;
            }
            else if (name == "Reader #1")
            {
                GlobalDataReader1.SalaReuniao.Add(EPC, PeakRssi);
                //       Console.WriteLine("tag count number READER#1 = {0}\n\n", GlobalDataReader1.SalaReuniao.Count);
                ICollection key = GlobalDataReader1.SalaReuniao.Keys;

               /* foreach (Int32 k in key)
                {
                    Console.WriteLine(k + ": " + GlobalDataReader1.SalaReuniao[k]);
                }
                Console.WriteLine("\n\n");*/
                return;
            }
            else if (name == "Reader #3")
            {
                GlobalDataReader1.CorredorMesas.Add(EPC, PeakRssi);
                //       Console.WriteLine("tag count number READER#3 = {0}\n\n", GlobalDataReader1.CorredorMesas.Count);
                ICollection key = GlobalDataReader1.CorredorMesas.Keys;

                /*         foreach (Int32 k in key)
                         {
                             Console.WriteLine(k + ": " + GlobalDataReader1.CorredorMesas[k]);
                         }
                         Console.WriteLine("\n\n");      */
                return;
            }
            else
            {
                Console.Error.Write("[ERRO!] Leitora não cadastrada!");
                Environment.Exit(1);
            }
        }

        private static void RealizaCadastro()
        {
            GlobalDataReader1.Cadastro.Add("E200 001B 2609 0147 0510 7BBD", "Joao");
            GlobalDataReader1.Cadastro.Add("E200 001B 2609 0147 0460 7BA5", "Maria");
            GlobalDataReader1.Cadastro.Add("E200 001B 2609 0147 0520 7BB1", "Jose");
            GlobalDataReader1.Cadastro.Add("E200 001B 2609 0147 0450 7B99", "Arlindo");
            GlobalDataReader1.Cadastro.Add("E200 001B 2609 0147 0380 7B85", "Manoel");
            GlobalDataReader1.Cadastro.Add("E200 001B 2609 0147 0910 7C5D", "Carla");
            GlobalDataReader1.Cadastro.Add("E200 001B 2609 0147 0850 7C39", "Julia");
            GlobalDataReader1.Cadastro.Add("E200 001B 2609 0147 0840 7C31", "Edgar");
            GlobalDataReader1.Cadastro.Add("E200 001B 2609 0147 0710 7C0D", "Jose");
            GlobalDataReader1.Cadastro.Add("E200 001B 2609 0147 0660 7BF5", "Arlindo");
            GlobalDataReader1.Cadastro.Add("E200 001B 2609 0147 0900 7C55", "Getulio");
            GlobalDataReader1.Cadastro.Add("E200 001B 2609 0147 0390 7B8D", "Laura");
            GlobalDataReader1.Cadastro.Add("E200 001B 2609 0147 0780 7C25", "Maria Beatriz");
            GlobalDataReader1.Cadastro.Add("E200 001B 2609 0147 0720 7C01", "Andre");
            GlobalDataReader1.Cadastro.Add("E200 001B 2609 0147 0600 7BD1", "Anastacia");
            GlobalDataReader1.Cadastro.Add("E200 001B 2609 0147 1100 7CA5", "Lucas");
            GlobalDataReader1.Cadastro.Add("E200 001B 2609 0147 0650 7BE9", "Renato");
            GlobalDataReader1.Cadastro.Add("E200 001B 2609 0147 0790 7C2D", "Jesse");
            GlobalDataReader1.Cadastro.Add("E200 001B 2609 0147 0590 7BDD", "Artur");
            GlobalDataReader1.Cadastro.Add("E200 001B 2609 0147 1040 7C81", "Marina");
            //GlobalDataReader1.Cadastro.Add("AD08 3003 4604 3152 2C00 0086", "Tag exemplo impinj");
        }

    }
}