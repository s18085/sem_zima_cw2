using System;
using System.IO;

namespace cw2
{
    class MainClass
    {
        static readonly string errorLogPath = Directory.GetCurrentDirectory() + "/log.txt";
        public static void Main(string[] args)
        {
            var csvFilePath = Directory.GetCurrentDirectory() + "/dane.csv"; ;
            var xmlFilePath = Directory.GetCurrentDirectory() + "/result.xml";
            var serialType = "xml";

            using (var lg = new StreamWriter(errorLogPath))
            {
                try
                {
                    if (args.Length == 3)
                    {
                        csvFilePath = args[0];
                        xmlFilePath = args[1];
                        serialType = args[2];
                    }
                    if (File.Exists(csvFilePath))
                    {
                        using (var reader = new StreamReader(File.OpenRead(csvFilePath)))
                        {
                            string line = null;
                            var lineNo = 0;
                            while ((line = reader.ReadLine()) != null)
                            {
                                lineNo++;
                                try
                                {
                                    Student.SaveStudentToExtension(line, lineNo);
                                }
                                catch (Exception e)
                                {
                                    lg.WriteLine(e.Message);
                                }
                            }
                            using (var writer = new StreamWriter(File.Create(xmlFilePath)))
                            {
                                Student.SerializeToFile(writer, serialType);
                            }
                            
                        }
                    }
                    else
                    {
                        throw new FileNotFoundException($"Plik {csvFilePath} nie istnieje");
                    }
                }
                catch (Exception e)
                {
                    throw e;
                    //lg.Write(e.Message);
                }
            }
        }
    } 
}
