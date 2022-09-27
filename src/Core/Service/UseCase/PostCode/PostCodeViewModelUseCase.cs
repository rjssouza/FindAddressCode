using Domain.Dto.Api;
using Domain.Dto.Config;
using Infrastructure.Helper;
using Microsoft.Extensions.Configuration;
using Service.Interface;

namespace Service.UseCase.PostCode
{
    public class PostCodeViewModelUseCase : BaseUseCase, IUseCase<PostCodeLocationViewModelDto>
    {
        private readonly DestinationConfigDto? _destinationConfigDto;

        public PostCodeViewModelUseCase(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            _destinationConfigDto = this.Configuration?.GetSection(Constants.DESTINATION).Get<DestinationConfigDto?>();
        }

        public Task<PostCodeLocationViewModelDto> Execute()
        {
            return Task.FromResult<PostCodeLocationViewModelDto>(new PostCodeLocationViewModelDto(_destinationConfigDto?.Name ?? string.Empty));
        }
    }
}