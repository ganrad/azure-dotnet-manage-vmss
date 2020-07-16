using System;
using System.Collections.Generic;

namespace core.Models
{
  public class RestApiRequest
  {
     public string AzureEndPoint { get; set; }

     public Object Payload;
     public Dictionary<string,string> ReqHeaders;

     public RestApiRequest()
     {
	Payload = "";
	ReqHeaders = new Dictionary<string,string>();
     }
  }
}

