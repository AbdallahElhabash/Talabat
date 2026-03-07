namespace Talabat_Project.Errors
{
    public class ApiInternalServerErrorResponse:ApiResponse
    {
        public string? Details { get; set; }
        public ApiInternalServerErrorResponse(string? details=null) : base(500)
        {
            Details = details;
        }
    }
}
