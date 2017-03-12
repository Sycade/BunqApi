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
var session = await new SessionServerEndpoint(bunq).CreateSessionAsync(installation.Token);

// Get all monetary accounts for the User
var accounts = await new MonetaryAccountEndpoint(bunq).ListAsync(session);

// Pay 25 euros from the first to an IBAN number
var paymentId = await new PaymentEndpoint(bunq).CreateAsync(accounts[0].Id,
    accounts[1].Aliases[0], new Amount(Currency.EUR, 25m), "My First Payment", session);
```
### Reuse an existing installation
```csharp
var clientCertificate = new X509Certificate2("your-certificate.pfx", "your-pvk-password");

var serverPublicKey = new ServerPublicKey(File.ReadAllText("the-server-public-key.crt"));
var installationToken = new Token(File.ReadAllText("the-installation-token.txt"));

var useSandbox = true;

var bunq = new BunqApiClient("your-api-key", clientCertificate, serverPublicKey, useSandbox);

var session = await new SessionServerEndpoint(bunq).CreateSessionAsync(installationToken);
// ... Use your session
```
