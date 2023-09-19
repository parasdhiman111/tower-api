using System;
namespace credit_work_app.Models.Exceptions
{
    public class ApiError : Clients.Interfaces.IWebClientError
    {
        public string Code { get; private set; }
        public string Message { get; private set; }


        public ApiError(Exception ex)
        {
            CreateCodedApiError((BadGatewayException)ex);
        }

        private void CreateCodedApiError(BadGatewayException ex)
        {
            Code = ex.Code;
        }
    }
}

