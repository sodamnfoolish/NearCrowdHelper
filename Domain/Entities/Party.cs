using System.Collections.Generic;

namespace Domain.Entities
{
    public class Party
    {
        public int contestId { get; set; }
        public List<Member> members { get; set; }
        public string participantType { get; set; }
        public bool ghost { get; set; }
        public int room { get; set; }
        public int startTimeSeconds { get; set; }
    }
}
