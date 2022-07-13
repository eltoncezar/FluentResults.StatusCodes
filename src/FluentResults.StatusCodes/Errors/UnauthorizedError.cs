namespace FluentResults.StatusCodes.Errors
{
    public abstract class UnauthorizedError : Error
    {
        public UnauthorizedError(string message) : base(message) { }
    }
}