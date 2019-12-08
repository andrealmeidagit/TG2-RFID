using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TG2_RFID
{
    public class FileHandler
    {

        protected string filePath;

        public void SetFileHandler()
        {
            filePath = @Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\log_" + DateTime.Now.ToString("yyyyMMdd_HH-mm-ss") + ".csv";

        }

        public void WriteToFile(Cardholder person, string epc, string reader, ushort ant)
        {
            var sala = person.GetCurAmbient().GetName();

            // Set File parameters
            string csvSeperator = ";";
            StringBuilder streamOutput = new StringBuilder();

            var antenna = Tuple.Create<string,ushort> (reader,ant);
            var strRSSIcurve = person.GetPowerCurve(antenna).GetCurveIndexY(0).ToString() + ";" +
                               person.GetPowerCurve(antenna).GetCurveIndexY(1).ToString() + ";" +
                               person.GetPowerCurve(antenna).GetCurveIndexY(2).ToString() + ";" +
                               person.GetPowerCurve(antenna).GetCurveIndexY(3).ToString() + ";" +
                               person.GetPowerCurve(antenna).GetCurveIndexY(4).ToString() + ";" +
                               person.GetPowerCurve(antenna).GetCurveIndexY(5).ToString() + ";" +
                               person.GetPowerCurve(antenna).GetCurveIndexY(6).ToString() + ";" +
                               person.GetPowerCurve(antenna).GetCurveIndexY(7).ToString() + ";" +
                               person.GetPowerCurve(antenna).GetCurveIndexY(8).ToString() + ";" +
                               person.GetPowerCurve(antenna).GetCurveIndexY(9).ToString() + ";" +
                               person.GetPowerCurve(antenna).GetCurveIndexY(10).ToString() + ";"
                               ;
            var strDopplercurve = person.GetDopplerEffectCurve(antenna).GetCurveIndexY(0).ToString() + ";" +
                                  person.GetDopplerEffectCurve(antenna).GetCurveIndexY(1).ToString() + ";" +
                                  person.GetDopplerEffectCurve(antenna).GetCurveIndexY(2).ToString() + ";" +
                                  person.GetDopplerEffectCurve(antenna).GetCurveIndexY(3).ToString() + ";" +
                                  person.GetDopplerEffectCurve(antenna).GetCurveIndexY(4).ToString() + ";" +
                                  person.GetDopplerEffectCurve(antenna).GetCurveIndexY(5).ToString() + ";" +
                                  person.GetDopplerEffectCurve(antenna).GetCurveIndexY(6).ToString() + ";" +
                                  person.GetDopplerEffectCurve(antenna).GetCurveIndexY(7).ToString() + ";" +
                                  person.GetDopplerEffectCurve(antenna).GetCurveIndexY(8).ToString() + ";" +
                                  person.GetDopplerEffectCurve(antenna).GetCurveIndexY(9).ToString() + ";" +
                                  person.GetDopplerEffectCurve(antenna).GetCurveIndexY(10).ToString()
                                  ;

            string[][] dataOutput = new string[][]{
                                    new string[]{person.GetName(), epc, sala, reader, ant.ToString(), DateTime.Now.ToString("dd/MM/yyyy; HH:mm:ss:ffff"), ";", strRSSIcurve, strDopplercurve }
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
                                    new string[]{"Nome", "EPC", "Ambiente", "Leitora", "Antena", "Data", "Horario", ";", "Curva RSSI", ";;;;;;;;",";" , "Curva Doppler"}
                                    };
            int length = dataOutput.GetLength(0);
            for (int i = 0; i < length; i++)
                streamOutput.AppendLine(string.Join(csvSeperator, dataOutput[i]));

            // Create and write the csv file
            File.WriteAllText(filePath, streamOutput.ToString());
        }


    }
}
