namespace Graphql.Business.Common
{
    public class BadRequestError
    {
        public BadRequestError(string message, string code)
        {
            Message = message;
            Code = code;
        }

        public string Message { get; }

        public string Code { get; }
    }
}
