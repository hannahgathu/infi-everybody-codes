using System.Globalization;

namespace Search
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //make sure search is not case sensitive
            string searchTerm = args[1].ToLower();
            findMatches(searchTerm);
        }

        public static void findMatches(string searchTerm)
        {
            string[] records = File.ReadAllLines("data/cameras-defb.csv");
            int numberOfRecords = 0;
            for(int i=0; i < records.Length; i++)
            {
                string[] fields = records[i].Split(';');

                //check if record matches
                if (fields[0].ToLower().Contains(searchTerm))
                {
                    Console.WriteLine(formatRecord(fields));
                    numberOfRecords++;
                }
            }
            Console.WriteLine(numberOfRecords.ToString() + " records found.");
        }

        public static string formatRecord(string[]fields)
        {
            string serialNumber = fields[0].Split(" ")[0];
            return serialNumber.Split("-")[2] + " | "+string.Join(" | ", fields);
        }

    }
}