using Sycade.BunqApi;
using Sycade.BunqApi.Model;
using System.IO;
using System.Security.Cryptography;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static async void CreateDeviceServer()
        {
var rsaKeyPair = new RSACng(2048); // Generate a key pair or load one
var useSandbox = true;

var bunq = new BunqApiClient(rsaKeyPair, useSandbox);

// Link your API key to this IP address
var installation = await bunq.Installations.CreateAsync(rsaKeyPair);
bunq.SetServerPublicKey(installation.ServerPublicKey);

var deviceServer = await bunq.DeviceServers.CreateAsync("your-api-key", "My First DeviceServer", installation.Token);
var session = await bunq.Sessions.CreateAsync("your-api-key", installation.Token);

// Get all bank accounts for the User
var accounts = await bunq.MonetaryAccountBanks.GetAllAsync(session);

// Pay 25 euros from the first account to the second
var paymentId = await accounts[0].CreatePaymentAsync(accounts[1], new Amount(Currency.EUR, 25m), "My First Payment", session);
        }

        static async void ReloadDeviceServer()
        {
var rsaKeyPair = new RSACng(CngKey.Import(new byte[0], CngKeyBlobFormat.GenericPrivateBlob)); // Load your private key here
var serverPublicKey = new RSACng(CngKey.Import(new byte[0], CngKeyBlobFormat.GenericPublicBlob)); // Load the server public key

var installationToken = new Token(File.ReadAllText("the-installation-token.txt")); // Load your installation token

var useSandbox = true;

var bunq = new BunqApiClient(rsaKeyPair, serverPublicKey, useSandbox);

var session = await bunq.Sessions.CreateAsync("your-api-key", installationToken);
// ... Use your session
        }
    }
}
