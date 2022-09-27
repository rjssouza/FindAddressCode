using Domain.Dto.Api;
using Domain.Dto.Integration;
using Infrastructure.CustomException;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Service.Interface;
using Service.UseCase;
using UnitTest.Application;

namespace UnitTest.Service;

public class PostCodeUnitTest : BaseUseCaseTest
{
    public PostCodeUnitTest(StartupFixture startupFixture)
        : base(startupFixture)
    {
    }

    [Fact]
    public async Task PostCodeLocation_Sucess()
    {
        StartupFixture.PostCodeResult = () =>
        {
            var postCodeResult = @"{'status':200,'result':{'postcode':'N7 6RS','quality':1,'eastings':530640,'northings':186295,
            'country':'England','nhs_ha':'London','longitude':-0.116805,'latitude':51.560414,'european_electoral_region':'London',
            'primary_care_trust':'Islington','region':'London','lsoa':'Islington 007B','msoa':'Islington 007','incode':'6RS',
            'outcode':'N7','parliamentary_constituency':'Islington North','admin_district':'Islington',
            'parish':'Islington, unparished area','admin_county':null,'admin_ward':'Tollington','ced':null,'ccg':'NHS North Central London','
            nuts':'Haringey and Islington','codes':{'admin_district':'E09000019','admin_county':'E99999999','admin_ward':'E05013712',
            'parish':'E43000209','parliamentary_constituency':'E14000763','ccg':'E38000240','ccg_id':'93C','ced':'E99999999',
            'nuts':'TLI43','lsoa':'E01002731','msoa':'E02000560','lau2':'E09000019'}}}";
            var postCodeDto = JsonConvert.DeserializeObject<PostCodeResultDto>(postCodeResult);

            return postCodeDto;
        };

        var useCase = StartupFixture.GetService<IUseCase<PostCodeLocationDto, PostCodeLocationCommandDto>>();
        var result = await useCase.Execute(new PostCodeLocationCommandDto("N76RS"));
        var expected = new PostCodeLocationDto()
        {
            Code = "N7 6RS",
            DistanceKm = 25.45,
            DistanceMiles = 15.81,
            District = "Islington, England",
            Latitude = 51.560414,
            Longitude = -0.116805,
        };

        Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(result));
    }

    [Fact]
    public async Task PostCodeLocation_ErrorNullResult()
    {
        StartupFixture.PostCodeResult = () =>
        {
            throw new ValidationException("PostCode", "Postcode not found");
        };

        var useCase = StartupFixture.GetService<IUseCase<PostCodeLocationDto, PostCodeLocationCommandDto>>();

        await Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await useCase.Execute(new PostCodeLocationCommandDto("N76RS"));
        });
    }

}