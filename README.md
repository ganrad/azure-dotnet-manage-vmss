---
Type: Sample
Languages:
- CSharp
Products:
- Azure Virtual Machine Scale Sets, Azure Kubernetes Service
Problem Definition: "Customers running AKS clusters in non-production regions often want to cut down infrastructure costs by decommissioning resources when they are not being used by anyone.  A common use case is shutting down VM machine scale sets associated with Azure Kubernetes Service during a period of inactivity such as a weekend.  This project attempts to provide a simple solution to address this requirement."
UrlFragment: azure-dotnet-manage-vmss
---

# Lower Azure Kubernetes Service costs by deallocating / reallocating VMSS resources as needed

At any given time, an AKS Cluster usually has multiple node pools running.  Node pool Virtual Machines are usually deployed on **Azure Virtual Machine Scale Sets**.  Also, customers typically provision multiple AKS clusters to isolate applications deployed in different environments such as dev-test, pre-prod and so forth.  Azure PaaS services deployed in non-prod (or non critical) regions usually consume compute resources even when the deployed applications are not actively serving any end user requests.  A common scenario is when the AKS clusters are allowed to run during weekends when there is little to no user activity.

This project provides a simple automated solution that allows customers to deallocate and restart Virtual Machine Scale Sets on a pre-defined schedule.  By shutting down the VM's associated with VMSS during long periods of inactivity, customers can easily lower their Azure infrastructure costs.

## Prerequisites

- Azure CLI (2.7+) installed on your workstation/VM
- Docker or Moby container engine deployed on your workstation/VM (This project uses Docker engine)
- Azure Kubernetes Service Cluster (AKS)
- Kubernetes CLI installed on your workstation/VM
- Azure Container Registry (ACR) or access to Docker Hub
- Visual Studio 2019, or [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) for Linux, macOS, or Windows installed on your workstation/VM

## Resources

- [Docker documentation](https://docs.docker.com/)
- [Azure Kubernetes Service documentation](https://docs.microsoft.com/en-us/azure/aks/)
- [Azure Virtual Machine Scale Sets documentation](https://docs.microsoft.com/en-us/azure/virtual-machine-scale-sets/)
- [Azure Functions documentation](https://docs.microsoft.com/en-us/azure/azure-functions/)

## Deploy Solution on Azure
This solution can be deployed on any one of the following platforms.
- Run as a standalone container on a Linux/Windows VM (Container engine required)
- Azure Functions
- Azure Container Instances
- Azure Kubernetes Service
- Azure App Service

Steps for deploying this solution on an AKS cluster is detailed below.

1. Fork/Clone this GitHub repository to your local machine

2. Build the container image and push the image to ACR

   Refer to the command snippet below.

   ```
   # (Optional) Build the application.  Make sure there are no compilation errors or warnings.
   $ dotnet build
   #
   # Run the container build
   # Substitute correct values for the following:
   #   acr-name : Name of your ACR instance
   $ docker build -t <acr-name>.azurecr.io/recycle-vmss:latest
   #
   # List the container images on your workstation/VM. The image built in the preceding step should be listed.
   $ docker images
   #
   # Before you can login to ACR, you should be logged into Azure via Azure CLI.
   # Login to the ACR instance. Substitute the correct value for 'acr-name' in the command below.
   $ az acr login -n <acr-name>
   #
   # Push the container image into ACR.  Substitute correct value for 'acr-name'.
   $ docker push <acr-name>.azurecr.io/recycle-vmss:latest
   #
   # Verify the image was pushed into your ACR instance via Azure Portal or by running the CLI command below.
   $ az acr repository list -n <acr-name> -o table
   #
   ```

3. Update Kubernetes deployment manifests

   There are two Kubernetes deployment manifests (Yaml files) provided in the `./k8sresources` directory. See below.
   - `deploy-start.yaml` : Use this manifest file to deploy an Azure Function Pod.  This Pod will be used to start the VMSS for a given Kubernetes cluster.
   - `deploy-stop.yaml` : Use this manifest file to deploy an Azure Function Pod.  This Pod will be used to stop (deallocate) the VMSS for a given Kubernetes cluster.

   The **VMSS Scale Trigger** Azure Function uses Azure Resource Manager APIs to deallocate and start VMSS.

   Refer to the table below to set correct values for the required environment variables. These environment variables are required for the Function to invoke Azure Resource Provider API's for VMSS.

   Environment Variable Name | Value | Description
   ------------------------- | ----- | -----------
   AzureWebJobsStorage | Required | Azure Storage Account connection string. Azure Functions runtime uses this storage account to store/manipulate WebJobs logs. 
   WEBSITE_TIME_ZONE | Required | Set this value based on your time zone. The list of TZ strings can be found [here](https://en.wikipedia.org/wiki/List_of_tz_database_time_zones).
   FUNCTIONS_WORKER_RUNTIME | dotnet | Runtime for this Azure Function is **dotnet**. Leave this value as is.
   AZURE_AD_TENANT_ID | Required | Specify the Azure AD Tenant ID.  You can use Azure Portal or CLI to retrieve this value.
   AZURE_AD_TOKEN_EP | https://login.microsoftonline.com | Azure AD Token end-point.  Leave this value as is.
   AZURE_SP_CLIENT_ID | Required | This solution uses OAuth Client Credentials Flow to authenticate a *Service Principal* against Azure AD.  An Azure Service Principal ID with appropriate permissions to access the target VMSS resource is required.  Refer to [Azure AD documentation](https://docs.microsoft.com/en-us/azure/active-directory/develop/howto-create-service-principal-portal) to create a Service Principal and assign it relevant permissions. 
   AZURE_SP_CLIENT_SECRET | Required | The secret associated with the Azure AD Service Principal.
   AZURE_SP_APP_ID_URI | https://management.azure.com | This is the Azure Resource Manager API end-point.  Leave this value as is.
   AZURE_SUBSCRIPTION_ID | Required | Specify the Azure Subscription ID where the AKS cluster VMSS resource is deployed.
   AZURE_RES_GROUP_NAME | Required | Specify the Resource Group in which the VMSS resource is deployed.  This is usually the AKS cluster **node** resource group.
   AZURE_VMSS_NAME | Required | Specify the name of the VMSS.
   AZURE_VMSS_API_VER | 2019-12-01 | This is the Azure resource provider API version. You can leave this value as is.
   AZURE_VMSS_ACTION | Required | Specify the **action** to be performed on the VMSS.  Supported values are - start (to start the VMSS), deallocate (to stop and deallocate the VM's associated with the VMSS).
   VmssTriggerSchedule | Required | An NCRONTAB expression which specifies the Function execution schedule. Refer to the [Function documentation](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-timer?tabs=csharp#ncrontab-expressions) for details and correct syntax.

## Project code of conduct

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
