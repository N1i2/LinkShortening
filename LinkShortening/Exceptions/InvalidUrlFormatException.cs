namespace LinkShortening.Exceptions;

public class InvalidUrlFormatException(string originalUrl) : ModelException($"URL \"{originalUrl}\" is invalid") {}
