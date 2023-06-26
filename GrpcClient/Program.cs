using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var input = new HelloRequest { Name = "SE1609" };
            //var channel = GrpcChannel.ForAddress("https://localhost:7159");
            //var client = new Greeter.GreeterClient(channel);
            //var reply = await client.SayHelloAsync(input);
            //Console.WriteLine(reply.Message);

            var channel = GrpcChannel.ForAddress("https://localhost:7159");
            var customerClient = new Customer.CustomerClient(channel);
            var clientRequested = new CustomerLookup { UserId = 1 };
            var customer = await customerClient.GetCustomerAsync(clientRequested);
            Console.WriteLine($"{customer.Fname} {customer.Lname}");

            Console.WriteLine();
            Console.WriteLine("List");
            Console.WriteLine();

            using (var call = customerClient.GetListCustomer(new NewCustomerRequest()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var currentCustomer = call.ResponseStream.Current;
                    Console.WriteLine($"{currentCustomer.Fname} {currentCustomer.Lname} : {currentCustomer.Age}");
                }
            }

            Console.ReadLine();
        }
    }
}