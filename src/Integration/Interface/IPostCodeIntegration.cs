using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto.Integration;

namespace Integration.Interface
{
    public interface IPostCodeIntegration : IDisposable
    {
        /// <summary>
        /// Get postcode result from web api
        /// </summary>
        /// <param name="postCode">Postcode parameter</param>
        /// <returns>Postcode result</returns>
        Task<PostCodeResultDto?> GetPostCodeResult(string postCode);
    }
}