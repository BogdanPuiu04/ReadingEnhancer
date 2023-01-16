using System.Globalization;

namespace ReadingEnhancer.Common.CustomExceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base()
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}