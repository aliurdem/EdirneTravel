using EdirneTravel.Core.Exceptions;
using System.Net;

namespace EdirneTravel.Core.Exception
{
    public class ResourceNotFoundException : ApiException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public ResourceNotFoundException(params object[] args) : base("Kayıt bulunamadı.", args)
        {
        }

        public ResourceNotFoundException(string message, params object[] args) : base(message, args)
        {
        }

        public ResourceNotFoundException(string message, System.Exception inner, params object[] args) : base(message, inner, args)
        {
        }
    }
}
