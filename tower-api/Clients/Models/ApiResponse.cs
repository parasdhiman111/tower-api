using System;
using System.Net;

namespace credit_work_app.Clients.Models
{
    public class ApiResponse<TSuccess>
    {
        public HttpStatusCode StatusCode { get; }
        public TSuccess SuccessResponse { get; }
        public string ErrorResponse { get; }

        public ApiResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public ApiResponse(HttpStatusCode statusCode, TSuccess successResponse)
        {
            StatusCode = statusCode;
            SuccessResponse = successResponse;
        }

        public ApiResponse(HttpStatusCode statusCode, string errorResponse)
        {
            StatusCode = statusCode;
            ErrorResponse = errorResponse;
        }
    }
}

