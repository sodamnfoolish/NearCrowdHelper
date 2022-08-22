namespace Domain.Entities
{
    public class Submission
    {
        public int id { get; set; }
        public int? contestId { get; set; }
        public int creationTimeSeconds { get; set; }
        public int relativeTimeSeconds { get; set; }
        public Problem problem { get; set; }
        public Party author { get; set; }
        public string programmingLanguage { get; set; }
        public string? verdict { get; set; }
        public string testset { get; set; }
        public int passedTestCount { get; set; }
        public int timeConsumedMillis { get; set; }
        public float? memoryConsumedBytes { get; set; }
    }
}
