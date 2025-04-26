using MiniE_Commerce.DAL.Entities.Enums;

namespace MiniE_Commerce.Errors
{
    public class ApiException : ApiResponse
    {
        public string Details { get; }
        public ApiException(CodeStatus errorNumber, string message = null, string details = null)
            : base(errorNumber, message)
        {
            Details = details;
        }

    }
}
