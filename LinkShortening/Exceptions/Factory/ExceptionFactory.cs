namespace LinkShortening.Exceptions.Factory
{
    public class ExceptionFactory
    {
        public static BaseException CreateEmptyUrlException<TContext>()
            where TContext : BaseException
        {
            return typeof(TContext) switch
            {
                Type t when t == typeof(ModelException) => new ModelEmptyUrlException(),
                Type t when t == typeof(ControllerException) => new ControllerEmptyUrlException(),
                _ => throw new InvalidOperationException("Unknown exception context")
            };
        }

        public static BaseException CreateInvalidUrlFormatException<TContext>(string originalUrl)
            where TContext : BaseException
        {
            return typeof(TContext) switch
            {
                Type t when t == typeof(ModelException) => new ModelInvalidUrlFormatException(originalUrl),
                Type t when t == typeof(ControllerException) => new ControllerInvalidUrlFormatException(originalUrl),
                _ => throw new InvalidOperationException("Unknown exception context")
            };
        }
    }
}
