using System;
using System.Net;

namespace ModelService
{
    public class ResponseStatusInfoModel
    {
        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
