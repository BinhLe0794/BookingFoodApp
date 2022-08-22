namespace AdminApp.Models.Common;
public class ApiException<T> : ApiResult<T>
{
   public ApiException(Exception e)
   {
      IsSuccessed = false;
      Message     = e.InnerException != null ? e.InnerException.Message : e.Message;
   }
}