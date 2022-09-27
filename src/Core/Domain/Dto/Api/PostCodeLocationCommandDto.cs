using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto.Api;

namespace Domain.Dto.Api
{
    /// <summary>
    /// Post code location command
    /// </summary>
    public class PostCodeLocationCommandDto
    {
        public PostCodeLocationCommandDto(string postCode)
        {
            Postcode = postCode;
        }

        /// <summary>
        /// Postcode sent by client
        /// </summary>
        public string Postcode { get; set; }    
    }
}