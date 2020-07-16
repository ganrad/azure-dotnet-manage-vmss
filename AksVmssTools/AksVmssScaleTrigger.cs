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
    public static class AksVmssScaleTrigger
    {
        [FunctionName("AksVmssScaleTrigger")]
        // public static async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        public static async Task Run([TimerTrigger("%VmssTriggerSchedule%")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"***** AksVmssScaleTrigger - Start Time: {DateTime.Now}");
	    
	    Dictionary<string, string> envVars = new Dictionary<string, string>();
	    new ParseAzureADInputs().GetEnvVars(ref envVars);
	    envVars.Add(AzureAdEnvConstants.OAUTH_GRANT_TYPE,"client_credentials");

	    string adTokenEpUri = 
	       envVars[AzureAdEnvConstants.AZURE_AD_TOKEN_EP] + "/" + 
	       envVars[AzureAdEnvConstants.AZURE_AD_TENANT_ID] + "/oauth2/token";
	    log.LogInformation($"***** AksVmssScaleTrigger - AD Endpoint URI: {adTokenEpUri}");

	    AdTokenResponse token = await AdTokenFactory.GetToken(envVars);
	    log.LogInformation($"***** AksVmssScaleTrigger - Access Token: {token.AccessToken}");

	    new ParseAzureInputs().GetEnvVars(ref envVars);
	    new ParseAzureVMssInputs().GetEnvVars(ref envVars);

	    RestApiRequest request = new RestApiRequest();
	    try {
	      /* request.AzureEndPoint = 
	        string.Format(AzureEnvConstants.AZURE_MGMT_API_EP,subId,rgName) +
	        "Microsoft.Compute/virtualMachineScaleSets?api-version=" + vmssVersion; */

	      request.AzureEndPoint = 
	        string.Format(
		  AzureEnvConstants.AZURE_MGMT_API_EP,
		  envVars[AzureEnvConstants.AZURE_SUBSCRIPTION_ID],
		  envVars[AzureEnvConstants.AZURE_RES_GROUP_NAME]) +
	        "Microsoft.Compute/virtualMachineScaleSets/" + 
		envVars[AzureVMssConstants.AZURE_VMSS_NAME] +
		string.Format(
	          "/{0}?api-version={1}", 
		  envVars[AzureVMssConstants.AZURE_VMSS_ACTION],
		  envVars[AzureVMssConstants.AZURE_VMSS_API_VER]);

	      log.LogInformation($"***** AksVmssScaleTrigger - Azure API URI: {request.AzureEndPoint}");

	      request.ReqHeaders.Add("Authorization",string.Format("Bearer {0}",token.AccessToken));

	      IRestApiOperations vmssApi = new AzVmssApiOperations();
	      // RestApiResponse response = await vmssApi.ExecuteGet(request);
	      RestApiResponse response = await vmssApi.ExecutePost(request);

	      if ( ! string.IsNullOrEmpty(response.ReasonPhrase) )
		 throw new ApiOperationException(string.Format("Azure REST API call failed. Received HTTP status code: {0} ({1}), Response: {2}",(int) response.StatusCode, response.ReasonPhrase, response.Response));

	      log.LogInformation($"***** AksVmssScaleTrigger - API Response status: {response.StatusCode}, Response: {response.Response}");
	   }
	   catch ( Exception exp ) // Catch HTTP Exceptions - Server down, connection refused ...
	   {
	      log.LogError($"***** AksVmssScaleTrigger - Encountered exception: {exp}");
	   };

           log.LogInformation($"***** AksVmssScaleTrigger - End Time: {DateTime.Now}");
        }
    }
}
