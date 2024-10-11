using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new ConnectionFactory()
{
    UserName = "guest",
    Password = "guest",
    HostName = "192.168.1.90",
};

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare("DirectEx", ExchangeType.Direct);

channel.QueueDeclare("Green1", false, false, false, null);
channel.QueueBind("Green1", "DirectEx", "green");

channel.QueueDeclare("Green2", false, false, false, null);
channel.QueueBind("Green2", "DirectEx", "green");

channel.QueueDeclare("Red", false, false, false, null);
channel.QueueBind("Red", "DirectEx", "red");

string message = "Hello world from green";
byte[] body = Encoding.UTF8.GetBytes(message);
channel.BasicPublish("DirectEx", "green", null, body);

string message2 = "Hello world from red";
byte[] body2 = Encoding.UTF8.GetBytes(message2);
channel.BasicPublish("DirectEx", "red", null, body2);