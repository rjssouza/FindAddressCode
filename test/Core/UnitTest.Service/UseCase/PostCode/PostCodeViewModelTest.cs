using Domain.Dto.Api;
using Domain.Dto.Integration;
using Infrastructure.CustomException;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Service.Interface;
using Service.UseCase;
using UnitTest.Application;

namespace UnitTest.Service;

public class PostCodeViewModelTest : BaseUseCaseTest
{
    public PostCodeViewModelTest(StartupFixture startupFixture)
        : base(startupFixture)
    {
    }

    [Fact]
    public async Task PostCodeViewModel_Sucess()
    {
        var useCase = StartupFixture.GetService<IUseCase<PostCodeLocationViewModelDto>>();
        var result = await useCase.Execute();
        var expected = new PostCodeLocationViewModelDto("London Heathrow Airport");

        Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(result));
    }

 

}