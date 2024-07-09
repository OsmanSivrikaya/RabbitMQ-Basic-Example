using System.Text;
using RabbitMQ.Client;

// System.Text ve RabbitMQ.Client namespace'lerini kullanarak gerekli kütüphaneleri ekledik.

// Bağlantı oluşturma için Factory oluşturuluyor.
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://dkqmxsrn:9d2EFpRPz22ELDJmBgIIxmU393NVxkFK@moose.rmq.cloudamqp.com/dkqmxsrn");
// RabbitMQ sunucusuna URI üzerinden bağlanıyor.

// Bağlantıyı ve kanalı oluşturuyoruz.
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// Kuyruk adı tanımlanıyor.
var queueName = "example-work-queue";
// Kuyruğu tanımlıyoruz: adı, dayanıklılık (durable), özel (exclusive), otomatik silme (autoDelete)
channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false);

// 0'dan 99'a kadar döngü oluşturarak mesaj gönderiyoruz.
for (int i = 0; i < 100; i++)
{
    // Her bir mesaj için 200 milisaniye bekleme yapıyoruz.
    await Task.Delay(200);
    // Mesajımızı UTF-8 formatında byte dizisine dönüştürüyoruz.
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");
    // Mesajı belirtilen kuyruğa gönderiyoruz.
    channel.BasicPublish(exchange: string.Empty, routingKey: queueName, body: message);
}

// Konsoldan bir giriş bekliyoruz, programın kapatılmaması için.
Console.Read();
