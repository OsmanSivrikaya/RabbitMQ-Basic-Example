using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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

// Mesajları işleme almak için tüketici (consumer) oluşturuluyor.
EventingBasicConsumer consumer = new(channel);
// Belirtilen kuyruktan mesaj almak için tüketici (consumer) başlatılıyor.
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
// Tüketiciye aynı anda bir mesajın işlenmesini belirtmek için QoS (Quality of Service) ayarları yapılıyor.
channel.BasicQos(prefetchCount: 1, prefetchSize: 0, global: false);

// Mesaj alındığında yapılacak işlemi tanımlama
consumer.Received += (sender, e) =>
{
    // Gelen mesajın içeriğini byte dizisinden string'e dönüştürüp konsola yazdırma
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

// Konsoldan bir giriş bekliyoruz, programın kapatılmaması için.
Console.Read();
