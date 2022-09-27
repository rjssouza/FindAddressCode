using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto.Integration;
using Infrastructure.CustomException;
using Integration.Interface;

namespace Integration.HttpIntegration
{
    public class PostCodeIntegration : BaseIntegration, IPostCodeIntegration
    {
        private const string API_ADDRESS = "https://postCodes.io";

        private const string API_NAME = "postCode-api";

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="httpClientFactory">Http client factory</param>
        public PostCodeIntegration(IHttpClientFactory httpClientFactory) 
            : base(httpClientFactory)
        {
        }

        protected override string ApiAddress => API_ADDRESS;

        protected override string Name => API_NAME;

        /// <summary>
        /// Get postcode result from web api
        /// </summary>
        /// <param name="postCode">Postcode parameter</param>
        /// <returns>Postcode result</returns>
        public async Task<PostCodeResultDto?> GetPostCodeResult(string postCode)
        {
            var httpResponseMessage =  await Get($"postCodes/{postCode}");
            var result = await ProcessHttpResponse<PostCodeResultDto>(httpResponseMessage);

            return result;
        }

        protected override async Task ProcessMessageError(HttpResponseMessage responseHttp)
        {
            if (responseHttp.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new ValidationException("PostCode", "Postcode not found");
            
            await ThrowException(responseHttp);
        }
    }
}