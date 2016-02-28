namespace Tookan.NET.Core
{
    public class Agent : IAgent
    {
        public int FleetId { get; set; }
        public string FleetThumbImage { get; set; }
        public string FleetImage { get; set; }
        public TaskStatus Status { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RegistrationStatus { get; set; }
        public string Latitude { get; set; }
        public int IsAvailable { get; set; }
        public string Longitude { get; set; }
        public string LastUpdatedLocationTime { get; set; }
        public string FleetStatusColor { get; set; }
        public int PendingTasks { get; set; }
    }
}