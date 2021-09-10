using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace learnMeRMQ.Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory 
            { 
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            
            channel.QueueDeclare("demo queue", true, false, false, null);

            var message = new { Name = "Producer", Message = "Hello" };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            channel.BasicPublish("", "demo queue", true, null, body);
        }
    }
}
