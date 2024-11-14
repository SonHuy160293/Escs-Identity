using Core.Application.Exceptions;

namespace Identity.Application.Exceptions
{
    internal class NotFoundException : BusinessException
    {
        public NotFoundException(string errorMessage) : base(errorMessage)
        {
        }

        public NotFoundException(string errorCode, string errorMessage) : base(errorCode, errorMessage)
        {
        }
    }
}
