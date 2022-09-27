using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Summary : IDisposable
    {
        private readonly Dictionary<string, ValidationError> errors;
        private bool disposedValue;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Summary()
        {
            errors = new Dictionary<string, ValidationError>();
        }

        /// <summary>
        /// Indicates if there's any errors
        /// </summary>
        public bool ContainsErrors
        {
            get
            {
                return errors.Count > 0;
            }
        }

        /// <summary>
        /// Error message
        /// </summary>
        public string? ErrorMessage
        {
            get
            {
                return GetErrorMessage();
            }
        }

        /// <summary>
        /// Error dictionary
        /// </summary>
        public Dictionary<string, ValidationError> Errors
        {
            get
            {
                return errors;
            }
        }

        /// <summary>
        /// Add Validation object to dictionary
        /// </summary>
        /// <param name="subject">Subject error</param>
        /// <param name="message">Error message</param>
        public void AddError(string subject, string message)
        {
            if (!errors.ContainsKey(subject))
            {
                ValidationError validationError = new(subject, message);

                errors.Add(subject, validationError);

                return;
            }

            errors[subject].Errors.Add(message);
        }

        /// <summary>
        /// Add Validation object to dictionary
        /// </summary>
        /// <param name="validationError">Validation error</param>
        public void AddValidationError(ValidationError validationError)
        {
            if (!errors.ContainsKey(validationError.Subject))
            {
                errors.Add(validationError.Subject, validationError);

                return;
            }

            errors[validationError.Subject].Errors.AddRange(validationError.Errors);
        }

        /// <summary>
        /// Get error messages
        /// </summary>
        /// <param name="errorIndicator">Char that indicates the error punctuation</param>
        /// <param name="errorSeparator">It puts a separator char between errors</param>
        /// <returns>Formatted message</returns>
        public string? GetErrorMessage(string? errorIndicator = null, string? errorSeparator = null)
        {
            if (!ContainsErrors)
            {
                return null;
            }

            string _errorSeparator = errorSeparator ?? Environment.NewLine;

            List<string?> errorList = Errors
                .Select((dictError) =>
                {
                    string? errorsFromSubject = dictError.Value.GetErrorMessage(errorIndicator, _errorSeparator);

                    return errorsFromSubject;
                }).ToList();

            string errorMessage = string.Join(_errorSeparator, errorList);

            return errorMessage;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    errors.Clear();
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