namespace credit_work_app.Models.Exceptions
{
    public class NoCard : BadGatewayException
    {
        public NoCard()
        {
            Code = "No_Card";
        }

        public override string Message => "No Card retrieved from upstream service";
    }
}

