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

- Azure Kubernetes Service Clusters
- Visual Studio 2019, or [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) for Linux, macOS, or Windows

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

1. 

## Project code of conduct

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
