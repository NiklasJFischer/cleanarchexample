using ChatAPI.Domain.Enums;

namespace ChatAPI.Application.Core
{
    public class ServiceResult<TResult>
    {
        public TResult? Result { get; set; }
        public StatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public ServiceResult(TResult result)
        {
            Result = result;
            StatusCode = StatusCode.Success;
            Message = string.Empty;
        }

        public ServiceResult(StatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
