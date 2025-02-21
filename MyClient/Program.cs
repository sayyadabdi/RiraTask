using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using System.Text.Json;

namespace MyClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7251");
            var client = new MyService.MyServiceClient(channel);

            var models = new List<MyModel>
            {
                await CreateAsync(client),
                await CreateAsync(client),
            };

            foreach (var m in (await GetAsync(client)).Models)
            {
                Console.WriteLine(JsonSerializer.Serialize(m));

                await EditAsync(client, m);

                await DeleteAsync(client, m);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static async Task<MyModel> CreateAsync(MyService.MyServiceClient client)
        {
            var random = new Random();

            var result = await client.CreateAsync(new MyModel()
            {
                Id = random.Next(0, 1000),
                FirstName = $"اسم {random.Next(0, 9)}",
                LastName = $"فامیل {random.Next(0, 9)}",
                NationalCode = random.NextInt64(),
                BirthDate = Timestamp.FromDateTimeOffset(DateTime.UtcNow.ToUniversalTime())
            });

            return result;
        }

        private static async Task<GetResultModel> GetAsync(MyService.MyServiceClient client)
        {
            return await client.GetAsync(new PagingModel { PageNumber = 1, PageSize = 10 });
        }

        private static async Task EditAsync(MyService.MyServiceClient client, MyModel m)
        {
            await client.EditAsync(m);
        }

        private static async Task DeleteAsync(MyService.MyServiceClient client, MyModel m)
        {
            await client.DeleteAsync(new Int32Model() { Number = m.Id });
        }
    }
}
