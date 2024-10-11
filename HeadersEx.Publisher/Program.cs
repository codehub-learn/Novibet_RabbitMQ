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

channel.ExchangeDeclare("HeadersEx", ExchangeType.Headers);
channel.QueueDeclare("BlackSquares", false, false, false, null);

Dictionary<string, object> bindHeaders = new()
{
    {"x-match", "all" },
    {"shape", "square"},
    {"color", "black" }
};
channel.QueueBind("BlackSquares", "HeadersEx", "", bindHeaders);

string message = "A Black Square has been generated!";
var body = Encoding.UTF8.GetBytes(message);

IBasicProperties props = channel.CreateBasicProperties();
Dictionary<string, object> msgHeaders = new()
{
    {"shape", "square"},
    {"color", "black" }
};
props.Headers = msgHeaders;

channel.BasicPublish("HeadersEx", "", props, body);
Console.WriteLine($"Headers publisher sent {message}");