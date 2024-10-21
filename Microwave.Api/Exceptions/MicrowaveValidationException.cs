namespace Microwave.Api.Exceptions;

public class MicrowaveValidationException : Exception
{
    public MicrowaveValidationException(string message) : base(message)
    {
    }
}
