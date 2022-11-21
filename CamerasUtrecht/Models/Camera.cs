using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace CamerasUtrecht.Models
{
    public class CameraRecord
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
