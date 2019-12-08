using System;
using System.IO;
using System.Text;
using System.Threading;

namespace TestZone
{
    class Program
    {
        static void Main()
        {
            int i = 0;
            string tempo = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff");
            while (i < 10)
            {
                Console.WriteLine("{0}", tempo);
                Thread.Sleep(1000);
                i++;
            }

            //string strFilePath = @Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"\\log_"+ DateTime.Now.ToString("yyyyMMdd_HH-mm-ss") + ".csv";
            //string strSeperator = ";";
            //StringBuilder sbOutput = new StringBuilder();

            //string[][] inaOutput = new string[][]{
            //                    new string[]{"jose", "2000 251919 2591591", "Ambiente 1", "07/12/2019", 5000.ToString()},
            //                    new string[]{6000.ToString(), 7000.ToString(), 8000.ToString(), 9000.ToString(), 10000.ToString()},
            //                    new string[]{11000.ToString(), 12000.ToString(), 13000.ToString(), 14000.ToString(), 15000.ToString()},
            //                    new string[]{11000.ToString(), 12000.ToString(), 13000.ToString(), 14000.ToString(), 15000.ToString()},
            //                    new string[]{11000.ToString(), 12000.ToString(), 13000.ToString(), 14000.ToString(), 15000.ToString()}
            //};
            //int ilength = inaOutput.GetLength(0);
            //for (int i = 0; i < ilength; i++)
            //    sbOutput.AppendLine(string.Join(strSeperator, inaOutput[i]));


            //Console.Write("{0}", sbOutput);

            //// Create and write the csv file
            //File.WriteAllText(strFilePath, sbOutput.ToString());

            //inaOutput = new string[][]{
            //                    new string[]{"AH MANE", "TA FUNCIONANDO", "Ambiente 1", "07/12/2019", "PORRA!"},
            //                    new string[]{6000.ToString(), 7000.ToString(), 8000.ToString(), 9000.ToString(), 10000.ToString()}
            //};
            //ilength = inaOutput.GetLength(0);
            //sbOutput.Clear();
            //for (int i = 0; i < ilength; i++)
            //    sbOutput.AppendLine(string.Join(strSeperator, inaOutput[i]));
            //;
            //Console.Write("{0}", sbOutput);

            //// To append more lines to the csv file
            //File.AppendAllText(strFilePath, sbOutput.ToString());
        }
    }
}
