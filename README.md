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
var installation = await new InstallationEndpoint(bunq).CreateAsync();
bunq.SetServerPublicKey(installation.ServerPublicKey);

var deviceServer = await new DeviceServerEndpoint(bunq).CreateAsync("My First DeviceServer", installation.Token);
var sessionServer = await new SessionServerEndpoint(bunq).CreateAsync(installation.Token);

// Get all monetary accounts for the User
var accounts = await new MonetaryAccountEndpoint(bunq).ListAsync(sessionServer.User, sessionServer.Token);

// Pay 25 euros from the first to the second bank account in the user account
var paymentId = await new PaymentEndpoint(bunq).CreateAsync(sessionServer.User, accounts[0].Id,
    accounts[1].Aliases[0], new Amount(Currency.EUR, 25m), "My First Payment", sessionServer.Token);
```
### Reuse an existing installation
```csharp
var clientCertificate = new X509Certificate2("your-certificate.pfx", "your-pvk-password");

var serverPublicKey = new ServerPublicKey(File.ReadAllText("the-server-public-key.crt"));
var installationToken = new Token(File.ReadAllText("the-installation-token.txt"));

var useSandbox = true;

var bunq = new BunqApiClient("your-api-key", clientCertificate, useSandbox);
bunq.SetServerPublicKey(serverPublicKey);

var sessionServer = await new SessionServerEndpoint(bunq).CreateAsync(installationToken);
// ... use your active session
```
