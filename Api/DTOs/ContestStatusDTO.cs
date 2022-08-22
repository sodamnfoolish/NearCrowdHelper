using Api.Entities;

namespace Api.DTOs
{
    public class ContestStatusDTO
    {
        public string status { get; set; }
        public List<Submission> result { get; set; }
    }
}
