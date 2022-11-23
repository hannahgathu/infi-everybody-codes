namespace CamerasUtrecht.Models
{
    public class CameraViewModel
    {
        public List<CameraRecord> AllRecords { get; set; }
        public List<CameraRecord> DivByThreeAndFive { get; set; }
        public List<CameraRecord> DivByThree { get; set; }
        public List<CameraRecord> DivByFive { get; set; }
        public List<CameraRecord> DivByNone { get; set; }

    }
}
