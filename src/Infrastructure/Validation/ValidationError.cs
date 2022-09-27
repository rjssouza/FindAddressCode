using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    /// <summary>
    /// Objeto que representa o erro de validação
    /// </summary>
    public class ValidationError
    {
        private readonly List<string> _errors;
        private readonly string _subject;

        /// <summary>
        /// Indicates if there's any errors
        /// </summary>
        public bool ContainsErrors
        {
            get
            {
                return _errors.Count > 0;
            }
        }

        /// <summary>
        /// Error list
        /// </summary>
        public List<string> Errors
        {
            get
            {
                return _errors;
            }
        }

        /// <summary>
        /// Validation subject
        /// </summary>
        public string Subject
        {
            get
            {
                return _subject;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="subject">Assunto</param>
        /// <param name="message">Mensagem de erro</param>
        public ValidationError(string subject, string message)
        {
            _errors = new List<string>() { message };
            _subject = subject;
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

            List<string> errorsList = Errors
                .Select((message) =>
                {
                    return $"{errorIndicator}{message}";
                }).ToList();

            string errorMessage = string.Join(_errorSeparator, errorsList);

            return errorMessage;
        }
    }
}