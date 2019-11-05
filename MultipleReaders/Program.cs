////////////////////////////////////////////////////////////////////////////////
//
//    Multiple Readers
//
////////////////////////////////////////////////////////////////////////////////

using System;
using Impinj.OctaneSdk;
using System.Collections;
using System.Collections.Generic;

namespace OctaneSdkExamples
{
    class GlobalDataReader1
    {
        public static int RSSILowPassFilter = -55;
        public static volatile int reader_1_count = 0;
        public static volatile int reader_2_count = 0;
        public static volatile int reader_3_count = 0;
        public static volatile Hashtable SalaReuniao = new Hashtable();
        public static volatile Hashtable CorredorMesas = new Hashtable();
    }
    class Program
    {
        // Create a collection to hold all the ImpinjReader instances.
        static List<ImpinjReader> readers = new List<ImpinjReader>();

        static void Main(string[] args)
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
                string hostname1 = "speedwayr-10-9f-bb.local";
                string hostname2 = "speedwayr-10-9f-c8.local";
                string hostname3 = "speedwayr-10-9f-3f.local";
                // Create two reader instances and add them to the List of readers.
                readers.Add(new ImpinjReader(hostname1, "Reader #1"));
                readers.Add(new ImpinjReader(hostname2, "Reader #2"));
                readers.Add(new ImpinjReader(hostname3, "Reader #3"));

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

                    // Send a tag report every time the reader stops (period is over).
                    settings.Report.Mode = ReportMode.BatchAfterStop;

                    // Reading tags for 1 seconds every 0.25 second
                    settings.AutoStart.Mode = AutoStartMode.Periodic;
                    settings.AutoStart.PeriodInMs = 500;
                    settings.AutoStop.Mode = AutoStopMode.Duration;
                    settings.AutoStop.DurationInMs = 500;


                    // Apply the newly modified settings.
                    reader.ApplySettings(settings);

                    // Assign the TagsReported event handler.
                    // This specifies which method to call
                    // when tags reports are available.
                    reader.TagsReported += captura_tags;

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
        private static void captura_tags(ImpinjReader sender, TagReport report)
        {
            foreach (Tag tag in report)
            {
                if (tag.Epc.ToString() == /*"AD08 3003 4604 3152 2C00 0086"*/"E200 001B 2609 0147 0510 7BBD" ||
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
                    tag.Epc.ToString() == "E200 001B 2609 0147 1040 7C81")
                    {
                    if (tag.PeakRssiInDbm > GlobalDataReader1.RSSILowPassFilter)
                    {
                        string trimmedEPC = tag.Epc.ToString().Replace(" ", string.Empty);
                        TimeSpan deltaT = new TimeSpan(tag.LastSeenTime.LocalDateTime.Ticks - tag.FirstSeenTime.LocalDateTime.Ticks);

      //                  Console.WriteLine("Name: {0}, EPC: {1}, TAGSC: {2}, RSSI: {3}", sender.Name, trimmedEPC, tag.TagSeenCount, tag.PeakRssiInDbm);

                        if (GlobalDataReader1.SalaReuniao.ContainsValue(trimmedEPC))
                        {
                            SegundaLeituraSalaReuniao(sender.Name, trimmedEPC, tag.PeakRssiInDbm);
                        }else if (GlobalDataReader1.CorredorMesas.ContainsValue(trimmedEPC))
                        {
                            SegundaLeituraCorredorMesas(sender.Name, trimmedEPC, tag.PeakRssiInDbm);
                        }else
                        {
                            PrimeiraLeitura(sender.Name, trimmedEPC, tag.PeakRssiInDbm);
                        }

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
        private static void SegundaLeituraSalaReuniao(string name, string trimmedEPC, double PeakRssi)
        {
            if (name == "Reader #1")
            {

            } else if (name == "Reader #1")
            {

            } else
            {

            }

        }
        private static void SegundaLeituraCorredorMesas(string name, string trimmedEPC, double PeakRssi)
        {

        }
        private static void PrimeiraLeitura (string name, string trimmedEPC, double PeakRssi)
        {

            if (name == "Reader #2")
            {
                return;
            } else if (name == "Reader #1")
            {
                GlobalDataReader1.SalaReuniao.Add(GlobalDataReader1.SalaReuniao.Count + 1, trimmedEPC);
         //       Console.WriteLine("tag count number READER#1 = {0}\n\n", GlobalDataReader1.SalaReuniao.Count);
                ICollection key = GlobalDataReader1.SalaReuniao.Keys;

                foreach (Int32 k in key)
                {
                    Console.WriteLine(k + ": " + GlobalDataReader1.SalaReuniao[k]);
                }
                Console.WriteLine("\n\n");
                return;
            }
            else if (name == "Reader #3")
            {
                GlobalDataReader1.CorredorMesas.Add(GlobalDataReader1.CorredorMesas.Count + 1, trimmedEPC);
         //       Console.WriteLine("tag count number READER#3 = {0}\n\n", GlobalDataReader1.CorredorMesas.Count);
                ICollection key = GlobalDataReader1.CorredorMesas.Keys;

       /*         foreach (Int32 k in key)
                {
                    Console.WriteLine(k + ": " + GlobalDataReader1.CorredorMesas[k]);
                }
                Console.WriteLine("\n\n");      */
                return;
            }




            }

    }
}
