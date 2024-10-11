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

channel.ExchangeDeclare("FanoutEx", ExchangeType.Fanout, false, false); // Name, Type, Durable, AutoDelete

channel.QueueDeclare("FanoutEx_Q1", false, false, false, null);
channel.QueueBind("FanoutEx_Q1", "FanoutEx", "");

channel.QueueDeclare("FanoutEx_Q2", false, false, false, null);
channel.QueueBind("FanoutEx_Q2", "FanoutEx", "");

channel.QueueDeclare("FanoutEx_Q3", false, false, false, null);
channel.QueueBind("FanoutEx_Q3", "FanoutEx", "");

string message = "Hello World Again";
byte[] body = Encoding.UTF8.GetBytes(message);
channel.BasicPublish("FanoutEx", "", null, body);

string message2 = "Hello World Again2";
byte[] body2 = Encoding.UTF8.GetBytes(message2);
channel.BasicPublish("FanoutEx", "", null, body2);

Console.WriteLine($"FanoutEx Punlisher sent {message}");