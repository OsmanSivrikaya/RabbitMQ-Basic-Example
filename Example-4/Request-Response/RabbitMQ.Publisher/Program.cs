using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

// Bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://dkqmxsrn:9d2EFpRPz22ELDJmBgIIxmU393NVxkFK@moose.rmq.cloudamqp.com/dkqmxsrn");

// Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// Queue oluşturma
var requestQueueName = "example-request-response-queue";
channel.QueueDeclare(queue: requestQueueName, durable: false, exclusive: false, autoDelete: false);

// Reply queue adını ve correlation ID'yi belirleme
var replyQueueName = channel.QueueDeclare().QueueName;
var correlationId = Guid.NewGuid().ToString();

#region Request Mesajını Oluşturma ve Gönderme 
// Request mesajı için temel özellikleri belirleme
IBasicProperties properties = channel.CreateBasicProperties();
properties.CorrelationId = correlationId;
properties.ReplyTo = replyQueueName;

// 100 adet mesaj gönderme
for (int i = 0; i < 100; i++)
{
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");
    channel.BasicPublish(exchange: string.Empty, routingKey: requestQueueName, body: message, basicProperties: properties);
}
#endregion

#region Response Kuyruğunu Dinleme
// Response kuyruğunu dinlemek için consumer oluşturma
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: replyQueueName, autoAck: true, consumer: consumer);

// Response mesajını alma ve correlation ID'yi kontrol etme
consumer.Received += (sender, e) =>
{
    if (e.BasicProperties.CorrelationId == correlationId)
    {
        Console.WriteLine($"Response: {Encoding.UTF8.GetString(e.Body.Span)}");
    }
};
#endregion

// Programın kapanmasını engelleme
Console.Read();
