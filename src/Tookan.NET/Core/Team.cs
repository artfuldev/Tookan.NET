using System.Collections.Generic;

namespace Tookan.NET.Core
{
    public class Team : ITeam
    {
        private List<Agent> _fleets;
        public string TeamName { get; set; }
        public int TeamId { get; set; }

        public List<Agent> Fleets
        {
            get { return _fleets ?? (_fleets = new List<Agent>()); }
            set { _fleets = value; }
        }
    }
}