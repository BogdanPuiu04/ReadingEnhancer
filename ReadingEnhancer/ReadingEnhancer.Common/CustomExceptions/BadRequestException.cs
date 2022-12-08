using System.Globalization;

namespace ReadingEnhancer.Common.CustomExceptions;

public class BadRequestException : Exception
{
    public BadRequestException() : base()
    {
    }
    public BadRequestException(string message) : base(message)
    {
    }
    public BadRequestException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}