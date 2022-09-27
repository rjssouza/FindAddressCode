using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.CustomException;

namespace Service.Validation.Rules
{
    public abstract class BaseValidation<TEntry> : IDisposable
        where TEntry : class
    {
        private readonly Summary _summary;
        private readonly IServiceProvider _serviceProvider;
        private bool disposedValue;

        public BaseValidation(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _summary = new Summary();
        }

        public virtual void Validade(TEntry? entry)
        {
            if (entry == null)
                throw new NullReferenceException();

            TryValidateFromDataAnnotation(entry);
            
            OnValidated();
        }

        /// <summary>
        /// Try validating dynamically the object schema using annotations
        /// </summary>
        /// <param name="obj">Generic object schema</param>
        private void TryValidateFromDataAnnotation(TEntry obj)
        {
            ICollection<ValidationResult> results = new List<ValidationResult>();
            var context = new ValidationContext(obj, serviceProvider: null, items: null);
            var valid = Validator.TryValidateObject(
                obj, context, results,
                validateAllProperties: true
            );

            if (!valid)
                return;

            foreach (var result in results)
            {
                if (result != null)
                {
                    AddError(result.MemberNames.FirstOrDefault() ?? string.Empty, result.ErrorMessage ?? string.Empty);
                }
            }
        }
        /// <summary>
        /// Add error to summary
        /// </summary>
        /// <param name="subject">Subject field</param>
        /// <param name="message">Error message</param>
        protected void AddError(string subject, string message)
        {
            _summary.AddError(subject, message);
        }

        /// <summary>
        /// Method for throwing validation error 
        /// </summary>
        protected virtual void OnValidated()
        {
            if (_summary.ContainsErrors)
            {
                throw new Infrastructure.CustomException.ValidationException(_summary);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _summary.Dispose();
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