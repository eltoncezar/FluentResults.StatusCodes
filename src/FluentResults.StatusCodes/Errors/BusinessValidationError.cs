namespace FluentResults.StatusCodes.Errors
{
    public abstract class BusinessValidationError : Error
    {
        public BusinessValidationError(string message) : base(message) { }
    }
}