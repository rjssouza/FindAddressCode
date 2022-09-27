using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Dto.Api
{
    /// <summary>
    /// Viewmodel to be used on client side
    /// </summary>
    public class PostCodeLocationViewModelDto
    {
        public PostCodeLocationViewModelDto()
        {
            this.PostCodeList = new List<PostCodeLocationDto>();
            DestinationPlaceName = string.Empty;

        }
        public PostCodeLocationViewModelDto(string destinationPlaceName)
            : this()
        {
            DestinationPlaceName = destinationPlaceName;
        }

        /// <summary>
        /// Indicates if need to show loading
        /// </summary>
        public bool Loading { get; set; }
        public string? Postcode { get; set; }

        /// <summary>
        /// Post code location list 
        /// </summary>
        public IEnumerable<PostCodeLocationDto> PostCodeList { get; set; }

        /// <summary>
        /// Destination place name (as defined on appsettings)
        /// </summary>
        public string DestinationPlaceName { get; set; }
    }
}