using Grpc.Net.Client;

namespace MyClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7251");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                new HelloRequest { Name = "GreeterClient" });


            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
