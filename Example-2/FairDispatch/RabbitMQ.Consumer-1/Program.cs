using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

// Bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://dkqmxsrn:9d2EFpRPz22ELDJmBgIIxmU393NVxkFK@moose.rmq.cloudamqp.com/dkqmxsrn");

// Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection(); // Bağlantıyı oluştur
using IModel channel = connection.CreateModel(); // Kanal oluştur

// Queue oluşturma
// kuyruğu kalıcı olarak tanımlamamız gerekiyor
channel.QueueDeclare(queue: "example-1-queue", exclusive: false, durable: true); // Queue tanımla

// Queue'dan mesaj alma
EventingBasicConsumer consumer = new(channel); // Tüketici oluştur
channel.BasicConsume(queue: "example-1-queue", false, consumer); // Queue'dan mesajları tüketmeye başla

// PrefetchSize: Her mesaj için önek yükleme boyutunu belirtir. Genellikle 0 kullanılır, çünkü bu parametre çoğunlukla bayt cinsinden boyutu belirtir ve pek kullanılmaz.
uint prefetchSize = 0;

// PrefetchCount: Bir tüketiciye aynı anda gönderilecek mesaj sayısını belirtir. 
// 1 olarak ayarlanması, tüketiciye aynı anda yalnızca bir mesaj gönderileceği ve o mesaj işlendikten sonra yenisinin gönderileceği anlamına gelir.
ushort prefetchCount = 1;

// Global: false olarak ayarlanmışsa, ayar sadece bu kanal için geçerli olur. 
// true olarak ayarlanmışsa, bu ayar tüm tüketiciler için geçerli olur.
bool global = false;

// Kanalın QoS (Quality of Service) ayarlarını yapılandırma
channel.BasicQos(prefetchSize, prefetchCount, global);

consumer.Received += (sender, e) => {
    // Kuyruğa gelen mesajın işlendiği yerdir!
    // e.Body : Kuyruktaki mesajın verisini bütünsel olarak getirecektir.
    // e.Body.Span veya e.Body.ToArray() : Kuyruktaki mesajın byte verisini getirecektir.
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span)); // Mesajı UTF-8 olarak çöz ve ekrana yaz
    channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
};
Console.Read();