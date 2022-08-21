using Api.Entities;

namespace Api.DTOs
{
    public class ContestHacksDTO
    {
        public string status { get; set; }
        public List<Hack> result { get; set; }
    }
}
