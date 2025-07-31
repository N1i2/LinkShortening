namespace LinkShortening.Exceptions;

public class InvalidShortCodeFormatException(string shortCode): ModelException($"Short code \"{shortCode}\" is invalid"){}
