using System;
using System.Collections.Generic;

namespace core.Common
{
    public static class ParseInputs
    {
        public static Dictionary<string,string> GetEnvVars()
        {
	    Dictionary<string, string> envVars = new Dictionary<string, string>();
	    
	    string adTenantId = Environment.GetEnvironmentVariable(AzureAdEnvConstants.AZURE_AD_TENANT_ID);
	    if ( string.IsNullOrEmpty(adTenantId) )
	       throw new ArgumentNullException($"Env. variable : {AzureAdEnvConstants.AZURE_AD_TENANT_ID} not found!");
	    else
	       envVars.Add(AzureAdEnvConstants.AZURE_AD_TENANT_ID,adTenantId);

	    string adTokenEp = Environment.GetEnvironmentVariable(AzureAdEnvConstants.AZURE_AD_TOKEN_EP);
	    if ( string.IsNullOrEmpty(adTokenEp) )
	       throw new ArgumentNullException($"Env. variable : {AzureAdEnvConstants.AZURE_AD_TOKEN_EP} not found!");
	    else
	       envVars.Add(AzureAdEnvConstants.AZURE_AD_TOKEN_EP,adTokenEp);
	       
	    string clientId = Environment.GetEnvironmentVariable(AzureAdEnvConstants.AZURE_SP_CLIENT_ID);
	    if ( string.IsNullOrEmpty(clientId) )
	       throw new ArgumentNullException($"Env. variable : {AzureAdEnvConstants.AZURE_SP_CLIENT_ID} not found!");
	    else
	       envVars.Add(AzureAdEnvConstants.AZURE_SP_CLIENT_ID,clientId);

	    string clientSecret = Environment.GetEnvironmentVariable(AzureAdEnvConstants.AZURE_SP_CLIENT_SECRET);
	    if ( string.IsNullOrEmpty(clientSecret) )
	       throw new ArgumentNullException($"Env. variable : {AzureAdEnvConstants.AZURE_SP_CLIENT_SECRET} not found!");
	    else
	       envVars.Add(AzureAdEnvConstants.AZURE_SP_CLIENT_SECRET,clientSecret);

	    string appIdUri = Environment.GetEnvironmentVariable(AzureAdEnvConstants.AZURE_SP_APP_ID_URI);
	    if ( string.IsNullOrEmpty(appIdUri) )
	       throw new ArgumentNullException($"Env. variable : {AzureAdEnvConstants.AZURE_SP_APP_ID_URI} not found!");
	    else
	       envVars.Add(AzureAdEnvConstants.AZURE_SP_APP_ID_URI,appIdUri);
	    
	    return(envVars);
        }
    }
}
