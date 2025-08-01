namespace LinkShortening.Exceptions;

public class ModelInvalidUrlFormatException(string originalUrl) : ModelException($"URL \"{originalUrl}\" is invalid") {}
