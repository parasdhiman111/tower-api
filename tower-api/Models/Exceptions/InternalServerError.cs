using System;
namespace credit_work_app.Models.Exceptions
{
    public class InternalServerError : BadGatewayException
    {
        public InternalServerError(string message) : base(message)
        {
            Code = "Upstream_ServerError";
        }
    }
}

