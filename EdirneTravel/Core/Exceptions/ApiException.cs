using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EdirneTravel.Core.Exceptions
{
    public class ApiException : System.Exception
    {
        public virtual HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;
        public object[] Args { get; init; }

        public ApiException(params object[] args) : base("Beklenmeyen bir hata oluştu.")
        {
            Args = args;
        }

        public ApiException(string message, params object[] args) : base(message)
        {
            Args = args;
        }

        public ApiException(string message, System.Exception inner, params object[] args) : base(message, inner)
        {
            Args = args;
        }
    }
}
