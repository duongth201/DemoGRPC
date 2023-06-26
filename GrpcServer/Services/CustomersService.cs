using Grpc.Core;

namespace GrpcServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomer(CustomerLookup request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();
            if (request.UserId == 1)
            {
                output.Fname = "Ja";
                output.Lname = "Ma";
            }
            else if (request.UserId == 2)
            {
                output.Fname = "Baa";
                output.Lname = "Laa";
            }
            else
            {
                output.Fname = "Thomas";
                output.Lname = "Muller";
            }
            return Task.FromResult(output);
        }

        public override async Task GetListCustomer(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    Fname = "abc",
                    Lname = "abd",
                    EmailAddress = "test@fpt.edu.vn",
                    Age = 22,
                    IsAlive = true
                },
                new CustomerModel
                {
                    Fname = "abc2",
                    Lname = "abd2",
                    EmailAddress = "test2@fpt.edu.vn",
                    Age = 22,
                    IsAlive = true,
                }
            };

            foreach (var cust in customers)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(cust);
            }
        }
    }
}
