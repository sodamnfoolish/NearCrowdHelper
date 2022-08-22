using System.Collections.Generic;

namespace Domain.Entities
{
    public class Problem
    {
        public int contestId { get; set; }
        public string index { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public float points { get; set; }
        public int rating { get; set; }
        public List<string> tags { get; set; }
    }
}
