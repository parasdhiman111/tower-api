using credit_work_app.Models.Exceptions;
using System.Net;

namespace credit_work_app.Clients.Exceptions
{
    public class WrongDataFormat : WebClientException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.OK;

        public WrongDataFormat(Exception innerException)
            : base("Unable to deserialise.", innerException)
        { }
    }
}
