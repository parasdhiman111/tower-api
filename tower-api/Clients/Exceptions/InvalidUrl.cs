using credit_work_app.Models.Exceptions;
using System.Net;

namespace credit_work_app.Clients.Exceptions
{
    public class InvalidUrl : WebClientException
    {
        public string Url { get; }
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;


        public InvalidUrl(string url = null)
        {
            Url = url;
        }

        public InvalidUrl(string url, string message = null)
            : base(message)
        {
            Url = url;
        }
    }
}

