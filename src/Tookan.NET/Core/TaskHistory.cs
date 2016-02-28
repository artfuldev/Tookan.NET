namespace Tookan.NET.Core
{
    public class TaskHistory
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int FleetId { get; set; }
        public string FleetName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string CreationDateTime { get; set; }
    }
}