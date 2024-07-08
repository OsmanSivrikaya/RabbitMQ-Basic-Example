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
channel.QueueDeclare(queue: "example-1-queue", exclusive: false); // Queue tanımla

// Queue'dan mesaj alma
EventingBasicConsumer consumer = new(channel); // Tüketici oluştur
channel.BasicConsume(queue: "example-1-queue", autoAck: false, consumer); // Queue'dan mesajları tüketmeye başla
consumer.Received += async (sender, e) => {
    // Kuyruğa gelen mesajın işlendiği yerdir!
    // e.Body : Kuyruktaki mesajın verisini bütünsel olarak getirecektir.
    // e.Body.Span veya e.Body.ToArray() : Kuyruktaki mesajın byte verisini getirecektir.
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span)); // Mesajı UTF-8 olarak çöz ve ekrana yaz
    await Task.Delay(3000);

    // delivery tag mesajın uniq kodu 
    // multiple birden fazla mesaja dair onay parametresi gönderir biz false yapıyoruz tek tek mesajları onaylıyoruz
    // onaydan sonra kuyruktaki mesaj silinecektir
    // channel.BasicAck(e.DeliveryTag, multiple: false);
    // BasicNack ile işlenemeyecek mesajı belirtiyoruz ve kuyruktan silinmesini sağlıyoruz
    // requeue ye false verirsek mesaj silinir true verirsek kuyruğa tekrar eklenir
    // channel.BasicNack(e.DeliveryTag, multiple: false, requeue: false);
    // belirli tagli mesajları silebiliriz
    channel.BasicReject(e.DeliveryTag, requeue: false);
};
Console.Read();