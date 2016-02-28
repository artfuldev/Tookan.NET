namespace Tookan.NET.Core
{
    public interface IAgent
    {
        string Email { get; }
        int FleetId { get; }
        string FleetImage { get; }
        string FleetStatusColor { get; }
        string FleetThumbImage { get; }
        int IsAvailable { get; }
        string LastUpdatedLocationTime { get; }
        string Latitude { get; }
        string Longitude { get; }
        int PendingTasks { get; }
        string Phone { get; }
        int RegistrationStatus { get; }
        TaskStatus Status { get; }
        string Username { get; }
    }
}