using System;

namespace core.Common
{
    public interface AzureEnvConstants
    {
	// Azure Subscription ID
	public const string AZURE_SUBSCRIPTION_ID = "AZURE_SUBSCRIPTION_ID";
	// Azure Resource Group Name
	public const string AZURE_RES_GROUP_NAME = "AZURE_RES_GROUP_NAME";
	// Azure Management API end-point
	public const string AZURE_MGMT_API_EP = "https://management.azure.com/subscriptions/{0}/resourceGroups/{1}/providers/";
    }
}
