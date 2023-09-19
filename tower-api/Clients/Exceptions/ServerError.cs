using credit_work_app.Models.Exceptions;

namespace credit_work_app.Clients.Exceptions
{
    public class ServerError : BadGatewayException
    {
        public ServerError(string message) : base(message)
        {
            Code = "Upstream_ServerError";
        }
    }
}

