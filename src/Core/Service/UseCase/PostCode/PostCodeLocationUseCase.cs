using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto.Api;
using Domain.Dto.Config;
using GeoCoordinatePortable;
using Infrastructure.Enums;
using Infrastructure.Helper;
using Integration.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Interface;

namespace Service.UseCase.PostCode
{
    public class PostCodeLocationUseCase : BaseUseCase, IUseCase<PostCodeLocationDto, PostCodeLocationCommandDto>
    {
        private readonly IPostCodeIntegration _postCodeIntegration;
        private readonly DestinationConfigDto? _destinationConfigDto;

        public PostCodeLocationUseCase(IServiceProvider serviceProvider, IPostCodeIntegration postCodeIntegration)
            : base(serviceProvider)
        {
            _postCodeIntegration = postCodeIntegration;
            _destinationConfigDto = this.Configuration?.GetSection(Constants.DESTINATION).Get<DestinationConfigDto?>();
        }

        public async Task<PostCodeLocationDto> Execute(PostCodeLocationCommandDto entry)
        {
            if (_destinationConfigDto == null)
                throw new NullReferenceException("Destination should be configured");

            var postCodeLocationResultDto = await _postCodeIntegration.GetPostCodeResult(entry.Postcode);
            this.Validate(postCodeLocationResultDto);

            var postCodeLocationDto = this.Map<PostCodeLocationDto>(postCodeLocationResultDto);
            postCodeLocationDto.SetDistance(_destinationConfigDto.GeoCoordinate);

            return postCodeLocationDto;
        }
    }
}