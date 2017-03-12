# Sycade.BunqApi
A .NET bunq API client written in C# by Sycade.

## Requirements
To use the .NET bunq API client, you need:
- an API key (created in the Bunq Android (Sandbox) or iOS app;
- a PKCS #12 (.pfx) file with exportable private key (can be self-signed) for API request signing.

## Example
### Create a new Installation and execute a payment

```csharp
var clientCertificate = new X509Certificate2("your-certificate.pfx", "your-pvk-password");
var useSandbox = true;

var bunq = new BunqApiClient("your-api-key", clientCertificate, useSandbox);

// Link your API key to this IP address
var installation = await bunq.Installation.CreateAsync();
bunq.SetServerPublicKey(installation.ServerPublicKey);

var deviceServer = await bunq.DeviceServer.CreateAsync("My First DeviceServer", installation.Token);
var session = await bunq.SessionServer.CreateSessionAsync(installation.Token);

// Get all bank accounts for the User
var accounts = await bunq.MonetaryAccountBank.ListAsync(session);

// Pay 25 euros from the first account to the second
var paymentId = await accounts[0].CreatePaymentAsync(new Amount(Currency.EUR, 25m), accounts[1], "My First Payment", session);
```
### Reuse an existing installation
```csharp
var clientCertificate = new X509Certificate2("your-certificate.pfx", "your-pvk-password");

var serverPublicKey = new ServerPublicKey(File.ReadAllText("the-server-public-key.crt"));
var installationToken = new Token(File.ReadAllText("the-installation-token.txt"));

var useSandbox = true;

var bunq = new BunqApiClient("your-api-key", clientCertificate, serverPublicKey, useSandbox);

var session = await bunq.SessionServer.CreateSessionAsync(installationToken);
// ... Use your session
```
