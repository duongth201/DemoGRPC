using Grpc.Net.Client;
using GrpcServer;
using GrpcWinform;

namespace GrpcWinform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            var input = new HelloRequest { Name = textBox1.Text };
            using var channel = GrpcChannel.ForAddress("https://localhost:7159");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(input);
            textBox2.Text = reply.Message;
        }
    }
}