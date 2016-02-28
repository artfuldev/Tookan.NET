using System.Collections.Generic;

namespace Tookan.NET.Core
{
    public interface ITeam
    {
        string TeamName { get; }
        int TeamId { get; }
        List<Agent> Fleets { get; } 
    }
}