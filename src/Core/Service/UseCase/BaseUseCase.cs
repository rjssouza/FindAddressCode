using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Service.Validation.Interface;

namespace Service.UseCase
{
    /// <summary>
    /// Base use case class
    /// </summary>
    /// <typeparam name="TResult">Result type</typeparam>
    /// <typeparam name="TCommand">Command entry type</typeparam>
    public abstract class BaseUseCase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper? _mapper;
        private readonly IConfiguration? _configuration;
        private readonly ILogger<BaseUseCase>? _logger;

        private bool disposedValue;

        protected IConfiguration? Configuration => _configuration ;
        
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="serviceProvider">Service provider</param>
        public BaseUseCase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = serviceProvider.GetService<ILogger<BaseUseCase>>();
            _mapper = serviceProvider.GetService<IMapper>();
            _configuration = serviceProvider.GetService<IConfiguration>();
        }

        /// <summary>
        /// Auxiliary method to map from generic object using automapper
        /// </summary>
        /// <param name="entryValue">Generic entry value</param>
        /// <typeparam name="T">Generic parameter</typeparam>
        /// <returns>Result of the generic type</returns>
        protected T Map<T>(object? entryValue)
            where T : class
        {
            var result = _mapper?.Map<T>(entryValue);
            if(result == null)
                throw new NullReferenceException("Mapper not configured for this object");
            
            return result;
        }

        /// <summary>
        /// Auxiliary method to call the correspondent validation from validation schema
        /// </summary>
        /// <param name="validationObject">Validation object</param>
        protected void Validate<TEntry>(TEntry? validationObject)
                where TEntry : class
        {
            using var serviceValidation = _serviceProvider.GetService<IUseCaseValidation<TEntry>>();
            serviceValidation?.Validade(validationObject);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _logger?.LogInformation("User case disposed");
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