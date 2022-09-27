using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto.Api;
using Domain.Dto.Integration;
using Infrastructure.CustomException;
using Newtonsoft.Json;
using Service.Interface;
using Service.Validation.Interface;

namespace UnitTest.ServiceValidation.Rules
{
    public class PostCodeResultIntegrationTest : BaseUseCaseTest
    {
        public PostCodeResultIntegrationTest(StartupFixture startupFixture)
            : base(startupFixture)
        {
        }

        [Fact]
        public void PostCodeLocation_LatitudeMustBeInformed()
        {
            var postCodeResult = @"{'status':200,'result':{'postcode':'N7 6RS','quality':1,'eastings':530640,'northings':186295,
            'country':'England','nhs_ha':'London','longitude':0,'latitude':null,'european_electoral_region':'London',
            'primary_care_trust':'Islington','region':'London','lsoa':'Islington 007B','msoa':'Islington 007','incode':'6RS',
            'outcode':'N7','parliamentary_constituency':'Islington North','admin_district':'Islington',
            'parish':'Islington, unparished area','admin_county':null,'admin_ward':'Tollington','ced':null,'ccg':'NHS North Central London','
            nuts':'Haringey and Islington','codes':{'admin_district':'E09000019','admin_county':'E99999999','admin_ward':'E05013712',
            'parish':'E43000209','parliamentary_constituency':'E14000763','ccg':'E38000240','ccg_id':'93C','ced':'E99999999',
            'nuts':'TLI43','lsoa':'E01002731','msoa':'E02000560','lau2':'E09000019'}}}";
            var postCodeDto = JsonConvert.DeserializeObject<PostCodeResultDto>(postCodeResult);

            var useCase = StartupFixture.GetService<IUseCaseValidation<PostCodeResultDto>>();

            Assert.Throws<ValidationException>(() =>
            {
                useCase.Validade(postCodeDto);
            });
        }

        [Fact]
        public void PostCodeLocation_LongitudeMustBeInformed()
        {
            var postCodeResult = @"{'status':200,'result':{'postcode':'N7 6RS','quality':1,'eastings':530640,'northings':186295,
            'country':'England','nhs_ha':'London','longitude':null,'latitude':0,'european_electoral_region':'London',
            'primary_care_trust':'Islington','region':'London','lsoa':'Islington 007B','msoa':'Islington 007','incode':'6RS',
            'outcode':'N7','parliamentary_constituency':'Islington North','admin_district':'Islington',
            'parish':'Islington, unparished area','admin_county':null,'admin_ward':'Tollington','ced':null,'ccg':'NHS North Central London','
            nuts':'Haringey and Islington','codes':{'admin_district':'E09000019','admin_county':'E99999999','admin_ward':'E05013712',
            'parish':'E43000209','parliamentary_constituency':'E14000763','ccg':'E38000240','ccg_id':'93C','ced':'E99999999',
            'nuts':'TLI43','lsoa':'E01002731','msoa':'E02000560','lau2':'E09000019'}}}";
            var postCodeDto = JsonConvert.DeserializeObject<PostCodeResultDto>(postCodeResult);

            var useCase = StartupFixture.GetService<IUseCaseValidation<PostCodeResultDto>>();

            Assert.Throws<ValidationException>(() =>
            {
                useCase.Validade(postCodeDto);
            });
        }

        [Fact]
        public void PostCodeLocation_StatusMustBeSuccess()
        {
            var postCodeResult = @"{'status':412,'result': null}";
            var postCodeDto = JsonConvert.DeserializeObject<PostCodeResultDto>(postCodeResult);

            var useCase = StartupFixture.GetService<IUseCaseValidation<PostCodeResultDto>>();

            Assert.Throws<ValidationException>(() =>
            {
                useCase.Validade(postCodeDto);
            });
        }
    }
}