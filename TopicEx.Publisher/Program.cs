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

channel.ExchangeDeclare("TopicEx", ExchangeType.Topic);

channel.QueueDeclare("BikeSales", false, false, false, null);
channel.QueueBind("BikeSales", "TopicEx", "CRM.Sales.Bikes");

channel.QueueDeclare("ClothingSales", false, false, false, null);
channel.QueueBind("ClothingSales", "TopicEx", "CRM.Sales.Clothing");

channel.QueueDeclare("AllSales", false, false, false, null);
channel.QueueBind("AllSales", "TopicEx", "CRM.Sales.*");

channel.QueueDeclare("AllCRM", false, false, false, null);
channel.QueueBind("AllCRM", "TopicEx", "CRM.#");

string message = "A Clothing sale happened!";
var body = Encoding.UTF8.GetBytes(message);
channel.BasicPublish("TopicEx", "CRM.Sales.Clothing", null, body);
Console.WriteLine("Bike sales message sent");
