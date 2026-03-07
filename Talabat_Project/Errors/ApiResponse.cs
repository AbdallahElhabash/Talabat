namespace Talabat_Project.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public ApiResponse(int Code,string? Message=null) 
        {
            StatusCode = Code;
            ErrorMessage = Message??GetErrorMessage(Code);
        }  
        public string? GetErrorMessage(int StatusCode)
        {
         
            return StatusCode switch
            {
                400=> "Bad Request",
                401=> "Un Authoreized",
                404=> "Not Found",
                500=> "Internal Server Error",
                _=>null
            };
        }

    }
}
