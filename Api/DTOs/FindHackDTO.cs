﻿using System.ComponentModel;

namespace Api.DTOs
{
    public class FindHackDTO
    {
        [DefaultValue("123D")]
        public string problem { get; set; }
        public string input { get; set; }
        [DefaultValue(null)]
        public string? output { get; set; } = null;
    }
}
