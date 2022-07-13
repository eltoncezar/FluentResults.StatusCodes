namespace FluentResults.StatusCodes.Errors
{
    public abstract class NotFoundError : Error
    {
        public NotFoundError(string message) : base(message) { }
    }
}