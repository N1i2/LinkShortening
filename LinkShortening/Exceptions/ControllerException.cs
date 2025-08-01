namespace LinkShortening.Exceptions
{
    public abstract class ControllerException(string message): BaseException($"Controller exception: {message}") {}
}
