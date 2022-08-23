namespace ApplicationServices.Models.Common;

public class ApiSuccessResult<T> : ApiResult<T>
{
    public ApiSuccessResult(T resultObj)
    {
        IsSuccessed = true;
        ResultObj = resultObj;
    }

    public ApiSuccessResult()
    {
        IsSuccessed = true;
    }

    public ApiSuccessResult(string _message)
    {
        IsSuccessed = true;
        Message = _message;
    }
}