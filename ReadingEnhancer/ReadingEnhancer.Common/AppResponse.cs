namespace ReadingEnhancer.Common;

public class AppResponse<Payload>
{
    public bool IsSuccessful { get; set; }
    public List<string> Errors { get; set; }
    public Payload Data { get; set; }

    public static AppResponse<Payload> Fail(List<string> errorMessage)
    {
        return new AppResponse<Payload> {IsSuccessful = false, Errors = errorMessage};
    }

    public static AppResponse<Payload> Success(Payload payload)
    {
        return new AppResponse<Payload> {IsSuccessful = true, Data = payload};
    }
}