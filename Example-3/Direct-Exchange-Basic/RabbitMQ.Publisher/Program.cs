using System.Text;
using RabbitMQ.Client;

// Bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://dkqmxsrn:9d2EFpRPz22ELDJmBgIIxmU393NVxkFK@moose.rmq.cloudamqp.com/dkqmxsrn");

// Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection(); // Bağlantıyı oluştur
using IModel channel = connection.CreateModel(); // Kanal oluştur

// Exchange tanımlama
channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct); // Direct tipinde bir exchange tanımla

// Mesaj gönderme döngüsü
while (true)
{
    Console.Write("Mesaj: "); // Kullanıcıdan mesaj almak için konsola yaz
    string message = Console.ReadLine(); // Kullanıcıdan mesajı al
    byte[] byteMessage = Encoding.UTF8.GetBytes(message); // Mesajı byte dizisine dönüştür

    // Mesajı yayınlama
    channel.BasicPublish(exchange: "direct-exchange-example", routingKey: "direct-queue-example", body: byteMessage); // Mesajı exchange'e gönder
}

Console.Read(); // Konsolun açık kalmasını sağla
