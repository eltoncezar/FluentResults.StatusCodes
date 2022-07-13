namespace FluentResults.StatusCodes.Errors
{
    public abstract class InvalidInputError : Error
    {
        public InvalidInputError(string message) : base(message) { }
    }
}