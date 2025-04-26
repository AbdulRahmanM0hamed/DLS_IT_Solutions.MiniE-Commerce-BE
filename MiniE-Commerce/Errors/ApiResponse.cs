using MiniE_Commerce.DAL.Entities.Enums;

namespace MiniE_Commerce.Errors
{
    public class ApiResponse
    {
        public CodeStatus CodeStatus { get; set; }
        public string Message { get; set; }
        public ApiResponse(CodeStatus errorNumber, string message = null)
        {
            CodeStatus = errorNumber;
            Message = message ?? Enum.GetName(typeof(CodeStatus), errorNumber)!;
        }
    }
}
