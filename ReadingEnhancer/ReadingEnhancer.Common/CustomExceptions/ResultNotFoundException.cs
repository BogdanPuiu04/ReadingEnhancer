using System.Globalization;

namespace ReadingEnhancer.Common.CustomExceptions;

public class ResultNotFoundException : Exception
{
    public ResultNotFoundException() : base()
    {
    }

    public ResultNotFoundException(string message) : base(message)
    {
    }

    public ResultNotFoundException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}