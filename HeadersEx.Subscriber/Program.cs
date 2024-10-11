using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new()
{ UserName = "guest", Password = "guest", HostName = "192.168.1.90" };
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();
channel.ExchangeDeclare("CS1225_HeadersEx", ExchangeType.Headers);


var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"HeadersEx.Subscriber received '{message}' from 	BlackSquares Queue");
};
channel.BasicConsume("BlackSquares", true, consumer);

Console.WriteLine("Listening for messages. Press ENTER anytime to quit...");
Console.ReadLine();
