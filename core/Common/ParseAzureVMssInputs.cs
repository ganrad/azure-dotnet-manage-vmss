using System;
using System.Collections.Generic;

using core.Interfaces;

namespace core.Common
{
    public class ParseAzureVMssInputs : IParseInputs
    {
        public void GetEnvVars(ref Dictionary<string, string> envVars)
        {
	    string vmssName = Environment.GetEnvironmentVariable(AzureVMssConstants.AZURE_VMSS_NAME);
	    if ( string.IsNullOrEmpty(vmssName) )
	       throw new ArgumentNullException($"Env. variable : {AzureVMssConstants.AZURE_VMSS_NAME}");
	    else
	       envVars.Add(AzureVMssConstants.AZURE_VMSS_NAME,vmssName);

	    string vmssVersion = Environment.GetEnvironmentVariable(AzureVMssConstants.AZURE_VMSS_API_VER);
	    if ( string.IsNullOrEmpty(vmssVersion) )
	       throw new ArgumentNullException($"Env. variable : {AzureVMssConstants.AZURE_VMSS_API_VER}");
	    else
	       envVars.Add(AzureVMssConstants.AZURE_VMSS_API_VER,vmssVersion);

	    string vmssAction = Environment.GetEnvironmentVariable(AzureVMssConstants.AZURE_VMSS_ACTION);
	    if ( string.IsNullOrEmpty(vmssAction) )
	       throw new ArgumentNullException($"Env. variable : {AzureVMssConstants.AZURE_VMSS_ACTION}");
	    else
	       envVars.Add(AzureVMssConstants.AZURE_VMSS_ACTION,vmssAction);

	    return;
        }
    }
}
