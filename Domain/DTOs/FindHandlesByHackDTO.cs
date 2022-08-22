using System.ComponentModel;

namespace Domain.DTOs
{
    public class FindHandlesByHackDTO
    {
        [DefaultValue("123D")]
        public string problem { get; set; }
        public string input { get; set; }
        [DefaultValue(null)]
        public string? output { get; set; } = null;
    }
}
