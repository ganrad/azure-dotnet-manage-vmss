using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using core.Models;
using core.Interfaces;

namespace core.Implementations
{
    public class AzVmssApiOperations : IRestApiOperations
    {
	public async Task<RestApiResponse> ExecuteGet(RestApiRequest apiRequest)
	{
	   HttpClient client = new HttpClient();

	   foreach (var entry in apiRequest.ReqHeaders)
	      client.DefaultRequestHeaders.Add(entry.Key,entry.Value);

	   HttpRequestMessage reqMsg = 
	     new HttpRequestMessage(HttpMethod.Get,new Uri(apiRequest.AzureEndPoint));
	   HttpResponseMessage response = await client.SendAsync(reqMsg);

	   RestApiResponse apiResponse = new RestApiResponse();
	   apiResponse.StatusCode = response.StatusCode;

	   if ( ! response.IsSuccessStatusCode )
	      apiResponse.ReasonPhrase = response.ReasonPhrase;

	   apiResponse.Response = await response.Content.ReadAsStringAsync();
	   apiResponse.ContentHeaders = response.Content.Headers;
	   apiResponse.ResponseHeaders = response.Headers;

	   // return (T) Convert.ChangeType(await response.Content.ReadAsStringAsync(), typeof(T));
	   return apiResponse;
	}

        public async Task<RestApiResponse> ExecutePost(RestApiRequest apiRequest)
        {
	   HttpClient client = new HttpClient();

	   foreach (var entry in apiRequest.ReqHeaders)
	      client.DefaultRequestHeaders.Add(entry.Key,entry.Value);

	   HttpRequestMessage reqMsg = 
	     new HttpRequestMessage(HttpMethod.Post,new Uri(apiRequest.AzureEndPoint));
	   HttpResponseMessage response = await client.SendAsync(reqMsg);

	   RestApiResponse apiResponse = new RestApiResponse();
	   apiResponse.StatusCode = response.StatusCode;

	   if ( ! response.IsSuccessStatusCode )
	      apiResponse.ReasonPhrase = response.ReasonPhrase;

	   apiResponse.Response = await response.Content.ReadAsStringAsync();
	   apiResponse.ContentHeaders = response.Content.Headers;
	   apiResponse.ResponseHeaders = response.Headers;

	   return apiResponse;
        }
    }
}
