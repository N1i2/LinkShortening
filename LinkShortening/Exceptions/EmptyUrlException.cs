namespace LinkShortening.Exceptions;

public class EmptyUrlException() : ModelException("The original URL cannot be empty") { }