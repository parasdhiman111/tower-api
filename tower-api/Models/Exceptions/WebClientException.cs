using System;
using System.Net;

namespace credit_work_app.Models.Exceptions
{
    [Serializable]
    public abstract class WebClientException : Exception
    {
        public virtual HttpStatusCode StatusCode { get; }
        public string Code { get; set; }
        public new string Data { get; set; }


        protected WebClientException()
        { }

        protected WebClientException(string message)
            : base(message)
        { }

        protected WebClientException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        protected WebClientException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        protected WebClientException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

