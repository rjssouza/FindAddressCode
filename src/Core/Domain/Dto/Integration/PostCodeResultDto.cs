using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Domain.Dto.Integration
{
    public class PostCodeResultDto
    {
        public PostCodeResultDto()
        {
            Result = new ResultDto();
        }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("result")]
        public ResultDto Result { get; set; }
    }
}