using System;

namespace BruteParse
{
    using System.IO;

    using CsvHelper;

    using PostcodeParser.UK;

    class Program
    {
        static void Main(string[] args)
        {
            const string outfilename = "c:\\temp\\outfile.csv";
            if (File.Exists(outfilename))
            {
                File.Delete(outfilename);
            }

            Console.WriteLine($"Started at {DateTime.Now}");
            int counter = 0;
            using (var filestream = File.OpenText("c:\\temp\\ukpostcodes.csv"))
            using (var csvReader = new CsvParser(filestream))
            using (var outstream = File.CreateText(outfilename))
            using (var csvWriter = new CsvWriter(outstream))
            {
                var row = csvReader.Read(); // Header row
                while (true)
                {
                    row = csvReader.Read();
                    if (row == null) break;
                    counter++;
                    if (counter % 1000 == 0)
                    {
                        Console.CursorLeft = 0;
                        Console.Write(counter.ToString("N0"));
                    }
                    var postcode = row[1];
                    try
                    {
                        var destructured = Postcode.Destructure(postcode);
                        csvWriter.WriteField(destructured.Postcode);
                        csvWriter.WriteField(destructured.Outward.Area);
                        csvWriter.WriteField(destructured.Outward.District);
                        csvWriter.WriteField(destructured.Inward.Sector);
                        csvWriter.WriteField(destructured.Inward.Unit);
                        csvWriter.NextRecord();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    outstream.Flush();
                }
            }
            Console.WriteLine("");
            Console.WriteLine("--- Done ---");
            Console.ReadKey();
        }
    }
}