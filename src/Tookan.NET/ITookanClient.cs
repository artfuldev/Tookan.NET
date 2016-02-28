using Tookan.NET.Clients;

namespace Tookan.NET
{
    public interface ITookanClient
    {
        IAgentsClient Agents { get; }
        ITeamsClient Teams { get; }
    }
}