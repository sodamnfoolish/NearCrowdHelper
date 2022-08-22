namespace Domain.Entities
{
    public class Hack
    {
        public int id { get; set; }
        public int creationTimeSeconds { get; set; }
        public Party hacker { get; set; } 
        public Party defender { get; set; }
        public string verdict { get; set; }
        public Problem problem { get; set; }
        public JudgeProtocol judgeProtocol { get; set; }
    }
}
