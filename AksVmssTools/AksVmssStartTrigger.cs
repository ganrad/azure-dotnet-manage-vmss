using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

using Microsoft.Extensions.Logging;

using core.Common;
using core.Models;
using core.Interfaces;
using core.Implementations;

namespace AksVmssTools
{
    public static class AksVmssStartTrigger
    {
        [FunctionName("AksVmssStartTrigger")]
        // public static async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        public static async Task Run([TimerTrigger("%VmssStartTriggerSchedule%")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"***** AksVmssStartTrigger - Start Time: {DateTime.Now}");
	    
	    Dictionary<string, string> inputs = ParseInputs.GetEnvVars();
	    inputs.Add(AzureAdEnvConstants.OAUTH_GRANT_TYPE,"client_credentials");

	    string adTokenEpUri = 
	       inputs[AzureAdEnvConstants.AZURE_AD_TOKEN_EP] + "/" + 
	       inputs[AzureAdEnvConstants.AZURE_AD_TENANT_ID] + "/oauth2/token";
	    log.LogInformation($"***** AksVmssStartTrigger - AD Endpoint URI: {adTokenEpUri}");

	    AdTokenResponse token = await AdTokenFactory.GetToken(inputs);
	    log.LogInformation($"***** AksVmssStartTrigger - Access Token: {token.AccessToken}");

	    String subId = Environment.GetEnvironmentVariable(AzureEnvConstants.AZURE_SUBSCRIPTION_ID);
	    if ( string.IsNullOrEmpty(subId) )
	       throw new ArgumentNullException($"Env. Variable: {AzureEnvConstants.AZURE_SUBSCRIPTION_ID}");

	    String rgName = Environment.GetEnvironmentVariable(AzureEnvConstants.AZURE_RES_GROUP_NAME);
	    if ( string.IsNullOrEmpty(rgName) )
	       throw new ArgumentNullException($"Env. Variable: {AzureEnvConstants.AZURE_RES_GROUP_NAME}");

	    String vmssName = Environment.GetEnvironmentVariable(AzureVmssConstants.AZURE_VMSS_NAME);
	    if ( string.IsNullOrEmpty(vmssName) )
	       throw new ArgumentNullException($"Env. Variable: {AzureVmssConstants.AZURE_VMSS_NAME}");

	    String vmssVersion = Environment.GetEnvironmentVariable(AzureVmssConstants.AZURE_VMSS_API_VER);
	    if ( string.IsNullOrEmpty(vmssVersion) )
	       throw new ArgumentNullException($"Env. Variable: {AzureVmssConstants.AZURE_VMSS_API_VER}");

	    RestApiRequest request = new RestApiRequest();
	    try {
	      /* request.AzureEndPoint = 
	        string.Format(AzureEnvConstants.AZURE_MGMT_API_EP,subId,rgName) +
	        "Microsoft.Compute/virtualMachineScaleSets?api-version=" + vmssVersion; */

	      request.AzureEndPoint = 
	        string.Format(AzureEnvConstants.AZURE_MGMT_API_EP,subId,rgName) +
	        "Microsoft.Compute/virtualMachineScaleSets/" + vmssName +
	        "/start?api-version=" + vmssVersion;

	      log.LogInformation($"***** AksVmssStartTrigger - Azure API URI: {request.AzureEndPoint}");

	      request.ReqHeaders.Add("Authorization",string.Format("Bearer {0}",token.AccessToken));

	      IRestApiOperations vmssApi = new AzVmssApiOperations();
	      // RestApiResponse response = await vmssApi.ExecuteGet(request);
	      RestApiResponse response = await vmssApi.ExecutePost(request);

	      if ( ! string.IsNullOrEmpty(response.ReasonPhrase) )
		 throw new ApiOperationException(string.Format("Azure REST API call failed. Received HTTP status code: {0} ({1}), Response: {2}",(int) response.StatusCode, response.ReasonPhrase, response.Response));

	      log.LogInformation($"***** AksVmssStartTrigger - API Response status: {response.StatusCode}, Response: {response.Response}");
	   }
	   catch ( Exception exp ) // Catch HTTP Exceptions - Server down, connection refused ...
	   {
	      log.LogError($"***** AksVmssStartTrigger - Encountered exception: {exp}");
	   };

           log.LogInformation($"***** AksVmssStartTrigger - End Time: {DateTime.Now}");
        }
    }
}
