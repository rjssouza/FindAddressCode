namespace Infrastructure.CustomException
{
    /// <summary>
    /// Exception to be thrown after validation rules
    /// </summary>
    public class ValidationException : ApiException
    {
        private readonly Summary _validation;

        public Summary Validation
        {
            get
            {
                return _validation;
            }
        }

        /// <summary>
        /// Construtor exceção de validação
        /// </summary>
        public ValidationException()
            : base(System.Net.HttpStatusCode.PreconditionFailed)
        {
            _validation = new Summary();
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="validation">Validation summary</param>
        public ValidationException(Summary validation)
            : base(System.Net.HttpStatusCode.PreconditionFailed)
        {
            _validation = validation;
        }

        /// <summary>
        /// Alternative validation exception constructor
        /// </summary>
        /// <param name="subject">Validation subject</param>
        /// <param name="message">Validation message</param>
        public ValidationException(string subject, string message)
            : base(System.Net.HttpStatusCode.PreconditionFailed)
        {
            _validation = new Summary();
            _validation.AddError(subject, message);
        }
    }
}