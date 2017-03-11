# BunqApi
.NET Bunq API client by Sycade

## Requirements
To use the .NET Bunq API client, you need:
- an API key (created in the Bunq Android (Sandbox)/iOS app;
- a client certificate (can be self-signed);

## Example

```csharp
var clientCertificate = new X509Certificate2("your-certificate.pfx", "your-pvk-password");
var useSandbox = true;

var bunq = new BunqApiClient("your-api-key", clientCertificate, useSandbox);

// Link your API key to this IP address
var installation = await bunq.CreateInstallationAsync();
var deviceServer = await bunq.CreateDeviceServerAsync("My First DeviceServer");
var sessionServer = await bunq.CreateSessionServerAsync();

// Get all monetary accounts of type Bank from the current User
var accounts = await bunq.ListMonetaryAccountBanksAsync();

// Pay 25 euros from the first to the second bank account in your user account
var paymentId = await bunq.CreatePaymentAsync(accounts[0], accounts[1].Aliases[0], new Amount(Currency.EUR, 25m), "My First Payment");
```
