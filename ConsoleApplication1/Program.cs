using Sycade.BunqApi;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static async Task MainAsync()
        {
            var clientCert = new X509Certificate2("testcert.pfx", "test");

            //var bunq = new BunqApiClient("eab171d23621d7203636b615d6c547a4404aadd9b99fde9545765b5ad786b909", "700c880a08edc6bc2690402b8effe75ce57527f37fe1645a7323f10355b2d538", clientCert, true);
            var bunq = new BunqApiClient("eab171d23621d7203636b615d6c547a4404aadd9b99fde9545765b5ad786b909", new X509Certificate2("testcert2.pfx", "test"), true);

            var installation = await bunq.CreateInstallationAsync();
            var deviceServer = await bunq.CreateDeviceServerAsync("my test deviceserver 2");
            var sessionServer = await bunq.CreateSessionServerAsync();

        }

        static void Main(string[] args)
        {
            MainAsync().Wait();
        }
    }
}
