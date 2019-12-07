using System;
using System.IO;
using System.Text;

namespace TestZone
{
    class Program
    {
        static void Main(string[] args)
        {
            string strFilePath = @"C:\testfile.csv";
            string strSeperator = ";";
            StringBuilder sbOutput = new StringBuilder();

            int[][] inaOutput = new int[][]{
                                new int[]{1000, 2000, 3000, 4000, 5000},
                                new int[]{6000, 7000, 8000, 9000, 10000},
                                new int[]{11000, 12000, 13000, 14000, 15000}
            };
            int ilength = inaOutput.GetLength(0);
            for (int i = 0; i < ilength; i++)
                sbOutput.AppendLine(string.Join(strSeperator, inaOutput[i]));

            // Create and write the csv file
            File.WriteAllText(strFilePath, sbOutput.ToString());

            // To append more lines to the csv file
            File.AppendAllText(strFilePath, sbOutput.ToString());
        }
    }
}
