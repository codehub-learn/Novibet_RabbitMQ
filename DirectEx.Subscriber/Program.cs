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
    Console.WriteLine($"{DateTime.Now}: Green1 Subscriber received {message}");
    Thread.Sleep(3000);
};

var consumer2 = new EventingBasicConsumer(channel);

consumer2.Received += (model, ea) =>
{
    byte[] body = ea.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"{DateTime.Now}: Red Subscriber received {message}");
    Thread.Sleep(3000);
};

channel.BasicConsume("Green1", true, consumer);
channel.BasicConsume("Red", true, consumer2);

Console.WriteLine("DirectSubscriber is listening press ENTER to quit...");
Console.ReadLine();