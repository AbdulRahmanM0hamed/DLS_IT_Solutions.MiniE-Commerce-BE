using MiniE_Commerce.DAL.Entities.Enums;
namespace MiniE_Commerce.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErrorResponse() : base(errorNumber: CodeStatus.BadRequest)
        {
        }
    }
}
