using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TG2_RFID
{
    public class FileHandler
    {

        protected string filePath;// = @Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\log_" + DateTime.Now.ToString("yyyyMMdd_HH-mm-ss") + ".csv";

        //public FileHandler()
        //{
        //    filePath = @Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\log_" + DateTime.Now.ToString("yyyyMMdd_HH-mm-ss") + ".csv";
        //}

        public void SetFileHandler()
        {
            filePath = @Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\log_" + DateTime.Now.ToString("yyyyMMdd_HH-mm-ss") + ".csv";

        }

        public void WriteToFile(Cardholder person, string epc, string reader, ushort ant)
        {
            var sala = person.GetAmbient().GetName();

            // Set File parameters
            string csvSeperator = ";";
            StringBuilder streamOutput = new StringBuilder();

            string[][] dataOutput = new string[][]{
                                    new string[]{person.GetName(), epc, sala, reader, ant.ToString(), DateTime.Now.ToString("dd/MM/yyyy; HH:mm:ss:ffff")}
                                    };
            int length = dataOutput.GetLength(0);
            for (int i = 0; i < length; i++)
                streamOutput.AppendLine(string.Join(csvSeperator, dataOutput[i]));

            // Appends more lines to the csv file
            File.AppendAllText(filePath, streamOutput.ToString());
        }

        public void CreateFile()
        {
            // Set File parameters
            string csvSeperator = ";";
            StringBuilder streamOutput = new StringBuilder();

            string[][] dataOutput = new string[][]{
                                    new string[]{"Nome", "EPC", "Ambiente", "Leitora", "Antena", "Data", "Horario"}
                                    };
            int length = dataOutput.GetLength(0);
            for (int i = 0; i < length; i++)
                streamOutput.AppendLine(string.Join(csvSeperator, dataOutput[i]));

            // Create and write the csv file
            File.WriteAllText(filePath, streamOutput.ToString());
        }


    }
}
