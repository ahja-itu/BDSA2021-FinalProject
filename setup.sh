#!/bin/bash

# Server
export serverApiClientId="a691e80e-219a-45d1-81fe-166463fa31ea"
export tenantId="16c9f4ae-4aca-4168-ac6f-7ec3c25e39b6"
export tenantDomain="ITUBDSABridgeTheGap.onmicrosoft.com"
export appIdUri="api://a691e80e-219a-45d1-81fe-166463fa31ea/API.Access"
export scope="API.Access"

## Client
export redirectUri="https =//localhost =8001/authentication/login-callback"
export clientAppClientId="1e14277d-cee5-4acf-8a1b-abdc783fd755"

dotnet new blazorwasm --force --hosted \
    --auth SingleOrg \
    --api-client-id "$serverApiClientId" \
    --app-id-uri "$serverApiClientId" \
    --client-id "$clientAppClientId" \
    --default-scope "$scope" \
    --domain "$tenantDomain" \
    --output "WebService.Core" \
    --tenant-id "$tenantId"
