namespace LinkShortening.Exceptions;

public class ControllerInvalidUrlFormatException(string originalUrl) : ControllerException($"URL \"{originalUrl}\" is invalid") {}
