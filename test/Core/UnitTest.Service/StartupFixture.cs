using System.Diagnostics;
using Domain.Dto.Integration;
using Domain.Mapper;
using Infrastructure.Extension;
using Integration.HttpIntegration;
using Integration.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Service.Interface;
using Service.UseCase;
using Service.Validation.Interface;
using Service.Validation.Rules;

namespace UnitTest.Application
{
    public class StartupFixture : IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        public IServiceProvider ServiceProvider => _serviceProvider;
        public Func<PostCodeResultDto?>? PostCodeResult { get; set; }

        private bool disposedValue;

        public StartupFixture()
        {
            var serviceCollection = new ServiceCollection();
            var config = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.Development.json")
                .AddEnvironmentVariables()
                .Build();
            serviceCollection.AddSingleton<IConfiguration>((serviceProvider) =>
            {
                return config;
            });

            serviceCollection.RegisterAllTypes(typeof(IUseCase<,>), new[] { typeof(BaseUseCase).Assembly });
            serviceCollection.RegisterAllTypes(typeof(IUseCase<>), new[] { typeof(BaseUseCase).Assembly });
            serviceCollection.RegisterAllTypes(typeof(IUseCaseValidation<>), new[] { typeof(BaseValidation<>).Assembly });
            serviceCollection.AddLogging(configure => configure.AddConsole());

            var postCodeIntegration = new Mock<IPostCodeIntegration>();
            postCodeIntegration.Setup(callTo => callTo.GetPostCodeResult(It.IsAny<string>()))
                                .ReturnsAsync(() => PostCodeResult?.Invoke());
            serviceCollection.AddScoped<IPostCodeIntegration>((serviceProvider) =>
            {
                return postCodeIntegration.Object;
            });
            serviceCollection.AddHttpClient();
            serviceCollection.AddAutoMapper(typeof(PostCodeProfile).Assembly);

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public T GetService<T>()
        {
            var result = ServiceProvider.GetService<T>();
            if (result == null)
                throw new NullReferenceException();

            return result;

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // _producerAdapterTest.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}