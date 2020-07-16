using System;

namespace core.Common
{
    public interface AzureAdEnvConstants
    {
	// Azure AD Tenant ID
	public const string AZURE_AD_TENANT_ID = "AZURE_AD_TENANT_ID";
	// Azure AD Token Endpoint
	public const string AZURE_AD_TOKEN_EP = "AZURE_AD_TOKEN_EP";
	// Azure AD Service Principal Client ID - Application ID in OAuth Client Credentials Flow / OpenID
	// Connect workflow
	public const string AZURE_SP_CLIENT_ID = "AZURE_SP_CLIENT_ID";
	// Azure AD Service Principal Client ID Secret
	public const string AZURE_SP_CLIENT_SECRET = "AZURE_SP_CLIENT_SECRET";
	// Azure AD Service Principal Application ID URI - Target Web Application URI being secured
	// => https://management.azure.com
	public const string AZURE_SP_APP_ID_URI = "AZURE_SP_APP_ID_URI";
	// OAuth Workflow Type - Authorization Code Grant, Client Credentials ....
	public const string OAUTH_GRANT_TYPE = "OAUTH_GRANT_TYPE";
    }
}
