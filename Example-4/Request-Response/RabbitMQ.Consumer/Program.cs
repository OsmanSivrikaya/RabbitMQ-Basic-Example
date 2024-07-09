using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

//Bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://dkqmxsrn:9d2EFpRPz22ELDJmBgIIxmU393NVxkFK@moose.rmq.cloudamqp.com/dkqmxsrn");

//Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue oluşturma
var requestQueueName = "example-request-response-queue";
channel.QueueDeclare(queue: requestQueueName, durable: false, exclusive: false, autoDelete: false);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: requestQueueName, autoAck: true, consumer: consumer);

consumer.Received += (sender, e) => {
    var message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
    byte[] responseMessage = Encoding.UTF8.GetBytes($"İşlem tamamlandı: {message}");
    IBasicProperties properties = channel.CreateBasicProperties();
    properties.CorrelationId = e.BasicProperties.CorrelationId;
    channel.BasicPublish(exchange: string.Empty, routingKey: e.BasicProperties.ReplyTo, basicProperties: properties, body: responseMessage);
};

Console.Read();