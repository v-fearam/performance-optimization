param location string = resourceGroup().location
param dataStorageAccountName string = 'stor${uniqueString(resourceGroup().id)}'
param userObjectId string //your user object id

var storageBlobDataContributorRole = subscriptionResourceId(
  'Microsoft.Authorization/roleDefinitions',
  'ba92f5b4-2d11-453d-a403-e96b0029c9fe'
) // Azure Storage Blob Data Contributor

resource dataStorageAccount 'Microsoft.Storage/storageAccounts@2023-01-01' = {
  name: dataStorageAccountName
  location: location
  kind: 'StorageV2'
  sku: {
    name: 'Standard_LRS'   
  }
  properties: {
    allowSharedKeyAccess: false
    supportsHttpsTrafficOnly: true
    minimumTlsVersion: 'TLS1_2'
  }
}

resource dataStorageAccountNameContainer 'Microsoft.Storage/storageAccounts/blobServices/containers@2022-09-01' = {
  name: '${dataStorageAccountName}/default/data'
  dependsOn: [
    dataStorageAccount
  ]
}

resource AzureStorageBlobDataContributorRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(dataStorageAccount.id, userObjectId, storageBlobDataContributorRole)
  scope: dataStorageAccount
  properties: {
    roleDefinitionId: storageBlobDataContributorRole
    principalId: userObjectId
    principalType: 'User'
  }
}
