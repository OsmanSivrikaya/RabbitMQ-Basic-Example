using System.Text;
using RabbitMQ.Client;

// Bağlantı oluşturma
ConnectionFactory factory = new();
// RabbitMQ sunucusuna bağlanmak için URI belirleme
factory.Uri = new Uri("amqps://dkqmxsrn:9d2EFpRPz22ELDJmBgIIxmU393NVxkFK@moose.rmq.cloudamqp.com/dkqmxsrn");

// Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection();
// Kanal oluşturma
using IModel channel = connection.CreateModel();

// Değişim (exchange) adını belirleme
var exchangeName = "example-pub-sub-exchange";
// Değişimi (exchange) tanımlama, türü 'fanout' olarak belirlenir
channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);

// Mesaj gönderme

for (int i = 0; i < 100; i++)
{
    // RabbitMQ mesajları byte türünde kabul eder, bu nedenle mesajı byte dizisine dönüştürme
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");
    // Mesajı belirlenen değişime (exchange) gönderme, routing key boş bırakılır çünkü 'fanout' türünde tüm kuyruklara gönderilir
    channel.BasicPublish(exchange: exchangeName, routingKey: string.Empty, body: message);
}

// Konsolu açık tutmak için
Console.Read();
