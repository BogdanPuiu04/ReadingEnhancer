using System.Globalization;

namespace ReadingEnhancer.Common.CustomExceptions;

public class ConflictException : Exception
{
    
    public ConflictException() : base()
    {
    }
    public ConflictException(string message) : base(message)
    {
    }
    public ConflictException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}