# Sycade.BunqApi
A .NET bunq API client written in C# by Sycade.

## Requirements
To use the .NET bunq API client, you just need an API key (created in the Bunq Android (Sandbox) or iOS app!

## Installation
Get the package from [nuget](https://www.nuget.org/packages/Sycade.BunqApi/) or clone the repository and build it yourself.

## Example
### Create a new Installation and execute a payment

```csharp
var rsaKeyPair = new RSACng(2048); // Generate a key pair or load one
var useSandbox = true;

var bunq = new BunqApiClient(rsaKeyPair, useSandbox);

// Link your API key to this IP address and create a device server
var installation = await bunq.Installations.CreateAsync(rsaKeyPair);
var deviceServer = await bunq.DeviceServers.CreateAsync("your-api-key", "My First DeviceServer", installation.Token);

// Open a session
var session = await bunq.Sessions.CreateAsync("your-api-key", installation.Token);

// Get all bank accounts for the User
var accounts = await bunq.MonetaryAccountBanks.GetAllAsync(session);

// Pay 25 euros from the first account to the second
var paymentId = await accounts[0].CreatePaymentAsync(accounts[1], new Amount(Currency.EUR, 25m), "My First Payment", session);
```
### Reuse an existing installation
```csharp
var rsaKeyPair = new RSACng(CngKey.Import(new byte[0], CngKeyBlobFormat.GenericPrivateBlob)); // Load your private key here
var serverPublicKey = new RSACng(CngKey.Import(new byte[0], CngKeyBlobFormat.GenericPublicBlob)); // Load the server public key

var installationToken = File.ReadAllText("the-installation-token.txt"); // Load your installation token

var useSandbox = true;

var bunq = new BunqApiClient(rsaKeyPair, serverPublicKey, useSandbox);

var session = await bunq.Sessions.CreateAsync("your-api-key", installationToken);
// ... Use your session
```
