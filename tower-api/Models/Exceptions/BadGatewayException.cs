using System;
namespace credit_work_app.Models.Exceptions
{
    [Serializable]
    public abstract class BadGatewayException : WebClientException
    {
        protected BadGatewayException()
        { }

        protected BadGatewayException(string message)
            : base(message)
        { }
    }
}

