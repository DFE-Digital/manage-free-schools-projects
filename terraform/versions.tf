terraform {
  required_version = ">= 1.5.4"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = ">= 3.67.0"
    }
    azapi = {
      source  = "azure/azapi"
      version = ">= 1.8.0"
    }
    azuread = {
      source  = "hashicorp/azuread"
      version = ">= 2.41.0"
    }
  }
}
