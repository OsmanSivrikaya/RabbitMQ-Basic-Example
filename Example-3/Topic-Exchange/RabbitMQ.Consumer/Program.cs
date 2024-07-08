using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

//Bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://dkqmxsrn:9d2EFpRPz22ELDJmBgIIxmU393NVxkFK@moose.rmq.cloudamqp.com/dkqmxsrn");

//Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "topic-exchange-example", type: ExchangeType.Topic);

Console.Write("Dinlenicek topic formatını belirtin: ");
string topic = Console.ReadLine() ?? "";
var queueName = channel.QueueDeclare().QueueName;
channel.QueueBind(queue: queueName, exchange: "topic-exchange-example", routingKey: topic);
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
consumer.Received += (sender, e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};
Console.Read();