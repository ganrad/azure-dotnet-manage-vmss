using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace core.Models
{
  public class RestApiResponse
  {
     public HttpStatusCode StatusCode { get; set; }
     public string ReasonPhrase { get; set; }
     public string Response { get; set; }
     public HttpContentHeaders ContentHeaders { get; set; }
     public HttpResponseHeaders ResponseHeaders { get; set; }

     // public Dictionary<string,string> ResHeaders;
  }
}

