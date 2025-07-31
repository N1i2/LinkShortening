namespace LinkShortening.Exceptions;

public abstract class ModelException(string message) : BaseException($"Model exception: {message}") { }
