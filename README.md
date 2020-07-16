---
page_type: sample
languages:
- CSharp
products:
- Azure Virtual Machine Scale Sets, Azure Kubernetes Service
Problem Definition: "Customers running AKS clusters in non-production regions often want to cut down infrastructure costs by decommissioning resources when they are not being used by anyone.  A common use case is shutting down VM machine scale sets associated with Azure Kubernetes Service during a period of inactivity such as a weekend.  This project attempts to provide a simple solution to address this requirement.
urlFragment: azure-dotnet-manage-vmss
---

# Deallocate and Start Azure Virtual Machine Scale Set on a pre-defined schedule

A basic .NET application that introduces Batch features such as pools, nodes, jobs, tasks, and interaction with Storage. Each task writes a small text file to standard output.

For details and explanation, see the accompanying article [Run your first Batch job with the .NET API](https://docs.microsoft.com/azure/batch/quick-run-dotnet).

## Prerequisites

- Azure Batch account and linked general-purpose Azure Storage account
- Visual Studio 2019, or [.NET Core 2.1](https://www.microsoft.com/net/download/dotnet-core/2.1) for Linux, macOS, or Windows

## Resources

- [Azure Batch documentation](https://docs.microsoft.com/azure/batch/)
- [Azure Batch code samples repo](https://github.com/Azure-Samples/azure-batch-samples)

## Project code of conduct

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
