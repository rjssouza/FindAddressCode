using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.CustomException;

namespace UnitTest.Infrastructure.CustomException
{
    public class ValidationExceptionTest
    {
        [Fact]
        public void CustomException_ValidateMessageDefaultConstructor()
        {
            var summary = new Summary();
            summary.AddError("ERROR_FIELD", "ERROR");
            summary.AddError("ERROR_FIELD_2", "ERROR 2");
            var validationException = new ValidationException(summary);
            var expectedMessage = "ERROR\nERROR 2";

            Assert.Equal(expectedMessage, validationException.Validation.ErrorMessage);
        }

        [Fact]
        public void CustomException_ValidateMessageSimplifiedConstructor()
        {
            var validationException = new ValidationException("ERROR_FIELD", "ERROR");
            var expectedMessage = "ERROR";

            Assert.Equal(expectedMessage, validationException.Validation.ErrorMessage);
        }

        [Fact]
        public void CustomException_ValidateHttpStatusCode()
        {
            var validationException = new ValidationException();
            var expectedResult = System.Net.HttpStatusCode.PreconditionFailed;

            Assert.Equal(expectedResult, validationException.StatusCode);
        }
    }
}