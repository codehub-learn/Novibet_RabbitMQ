using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new ConnectionFactory()
{
    UserName = "guest",
    Password = "guest",
    HostName = "192.168.1.90",
};

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    byte[] body = ea.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"{DateTime.Now}: TopicEx - All Sales Subscriber received {message}");
    Thread.Sleep(3000);
};

channel.BasicConsume("AllSales", true, consumer);

Console.WriteLine("FanoutEx is listening press ENTER to quit...");
Console.ReadLine();