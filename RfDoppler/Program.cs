////////////////////////////////////////////////////////////////////////////////
//
//    RF Doppler
//
////////////////////////////////////////////////////////////////////////////////

using System;
using Impinj.OctaneSdk;

namespace OctaneSdkExamples
{
    class Program
    {
        // Create an instance of the ImpinjReader class.
        static ImpinjReader reader = new ImpinjReader();

        static void Main(/*string[] args*/)
        {
            try
            {
                // Connect to the reader.
                // Pass in a reader hostname or IP address as a 
                // command line argument when running the example
                /*if (args.Length != 1)
                {
                    Console.WriteLine("Error: No hostname specified.  Pass in the reader hostname as a command line argument when running the Sdk Example.");
                    return;
                }*/
                string hostname = "speedwayr-10-9f-c8.local";//args[0];
                reader.Connect(hostname);

                // Get the default settings
                // We'll use these as a starting point
                // and then modify the settings we're 
                // interested in.
                Settings settings = reader.QueryDefaultSettings();

                // Tell the reader to include the
                // RF doppler frequency in all tag reports. 
                settings.Report.IncludeDopplerFrequency = true;
                settings.Report.IncludePeakRssi = true;
                settings.Report.IncludePhaseAngle = true;

                // Use antenna #1
                settings.Antennas.DisableAll();
                settings.Antennas.GetAntenna(1).IsEnabled = true;

                // ReaderMode must be set to DenseReaderM8.
                settings.ReaderMode = ReaderMode.DenseReaderM8;

                // Apply the newly modified settings.
                reader.ApplySettings(settings);

                // Assign the TagsReported event handler.
                // This specifies which method to call
                // when tags reports are available.
                reader.TagsReported += OnTagsReported;

                // Start reading.
                reader.Start();

                // Wait for the user to press enter.
                Console.WriteLine("Press enter to exit.");
                Console.ReadLine();

                // Stop reading.
                reader.Stop();

                // Disconnect from the reader.
                reader.Disconnect();
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

        static void OnTagsReported(ImpinjReader sender, TagReport report)
        {
            // This event handler is called asynchronously 
            // when tag reports are available.
            // Loop through each tag in the report 
            // and print the data.
            foreach (Tag tag in report)
            {
                if (tag.Epc.ToString() == "E200 001B 2609 0147 0510 7BBD")
                {
                    // Console.WriteLine("RSSI = {0} - - - Fase = {1}", tag.PeakRssiInDbm.ToString("0.00"), tag.PhaseAngleInRadians.ToString("0.00")) ;
                    if (tag.PeakRssiInDbm > -90)
                    {
                        if (Math.Abs(tag.RfDopplerFrequency) > 1.5)
                        {
                            if (tag.RfDopplerFrequency > 0)
                            {
                                Console.WriteLine("APROXIMANDO!!! -> Doppler Frequency (Hz) : {0} -- RSSI (dB) {1}",
                                    tag.RfDopplerFrequency.ToString("0.00"), tag.PeakRssiInDbm.ToString("0.00"));
                            }
                            else
                            {
                                Console.WriteLine("A F A S T A N D O!!! -> Doppler Frequency (Hz) : {0} - RSSI (dB): {1}",
                                    tag.RfDopplerFrequency.ToString("0.00"), tag.PeakRssiInDbm.ToString("0.00"));
                            }
                        }
                    }
                }
            }
        }
    }
}
