using System;
using System.Collections.Generic;

using core.Interfaces;

namespace core.Common
{
    public class ParseAzureInputs : IParseInputs
    {
        public void GetEnvVars(ref Dictionary<string, string> envVars)
        {
	    string subId = Environment.GetEnvironmentVariable(AzureEnvConstants.AZURE_SUBSCRIPTION_ID);
	    if ( string.IsNullOrEmpty(subId) )
	       throw new ArgumentNullException($"Env. variable : {AzureEnvConstants.AZURE_SUBSCRIPTION_ID}");
	    else
	       envVars.Add(AzureEnvConstants.AZURE_SUBSCRIPTION_ID,subId);

	    string rgName = Environment.GetEnvironmentVariable(AzureEnvConstants.AZURE_RES_GROUP_NAME);
	    if ( string.IsNullOrEmpty(rgName) )
	       throw new ArgumentNullException($"Env. variable : {AzureEnvConstants.AZURE_RES_GROUP_NAME}");
	    else
	       envVars.Add(AzureEnvConstants.AZURE_RES_GROUP_NAME,rgName);

	    return;
        }
    }
}
