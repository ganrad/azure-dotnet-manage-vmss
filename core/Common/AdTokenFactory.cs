using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using core.Models;

namespace core.Common
{
    public static class AdTokenFactory
    {
	private static readonly HttpClient client = new HttpClient();

        public static async Task<AdTokenResponse> GetToken(Dictionary<string,string> envVars)
        {
	    string adTokenEpUri = 
	      envVars[AzureAdEnvConstants.AZURE_AD_TOKEN_EP] + "/" + 
	      envVars[AzureAdEnvConstants.AZURE_AD_TENANT_ID] + "/oauth2/token";

	    Dictionary<string, string> cred = new Dictionary<string, string>();
	    cred.Add("grant_type",envVars[AzureAdEnvConstants.OAUTH_GRANT_TYPE]);
	    cred.Add("client_id",envVars[AzureAdEnvConstants.AZURE_SP_CLIENT_ID]);
	    cred.Add("client_secret",envVars[AzureAdEnvConstants.AZURE_SP_CLIENT_SECRET]);
	    cred.Add("resource",envVars[AzureAdEnvConstants.AZURE_SP_APP_ID_URI]);

	    HttpResponseMessage response = 
	      await client.PostAsync(new Uri(adTokenEpUri),new FormUrlEncodedContent(cred));
	    HttpStatusCode respCode = response.StatusCode;

	    if ( respCode != HttpStatusCode.OK )
	      throw new HttpRequestException(string.Format("AD Token request failed. Received HTTP status code: {0} ({1})",(int) respCode, respCode));

	    AdTokenResponse token = 
	      await JsonSerializer.DeserializeAsync<AdTokenResponse>(await response.Content.ReadAsStreamAsync());

	    return token;
        }
    }
}
