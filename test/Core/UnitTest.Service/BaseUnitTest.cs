using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto.Integration;
using UnitTest.Application;

namespace UnitTest.Service
{
    public abstract class BaseUseCaseTest : IClassFixture<StartupFixture>
    {
        private readonly StartupFixture _startupFixture;

        protected StartupFixture StartupFixture => _startupFixture;
        
        public BaseUseCaseTest(StartupFixture startupFixture)
        {
            _startupFixture = startupFixture;
        }

    }
}