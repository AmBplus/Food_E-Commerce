namespace Services.Common.Abstract;

public class ViewResult
{
    public bool Status { get; set; }
    public string Message { get; set; }

    public static ViewResult GetViewResultSucceed(string message)
    {
        ViewResult result = new ViewResult();
        result.Message = message;
        result.Status = true;
        return result;
    } 
    public static ViewResult GetViewResultFailed(string message)
    {
        ViewResult result = new ViewResult();
        result.Message = message;
        result.Status = false;
        return result;
    }
}