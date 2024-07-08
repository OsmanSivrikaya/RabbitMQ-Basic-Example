using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

// Bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://dkqmxsrn:9d2EFpRPz22ELDJmBgIIxmU393NVxkFK@moose.rmq.cloudamqp.com/dkqmxsrn");

// Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection(); // Bağlantıyı oluştur
using IModel channel = connection.CreateModel(); // Kanal oluştur

// 1. Adım: Publisher'daki exchange ile birebir aynı isim ve type'a sahip bir exchange tanımlanmalıdır.
channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

// 2. Adım: Publisher tarafından routing key'de bulunan değeri kuyruğa gönderilen mesajları kendi oluşturduğumuz kuyruğa yönlendirerek tüketmemiz gerekmektedir.
// Bunun için öncelikle bir kuyruk oluşturulmalıdır.
var queueName = channel.QueueDeclare().QueueName; // Geçici bir kuyruk oluştur ve kuyruk adını al

// 3. Adım: Kuyruğu exchange'e bağlama
channel.QueueBind(queue: queueName, exchange: "direct-exchange-example", routingKey: "direct-queue-example");

// Tüketici oluşturma ve mesajları tüketme
EventingBasicConsumer consumer = new(channel); // Tüketici oluştur
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer); // Mesajları tüketmeye başla
consumer.Received += (sender, e) => {
    var message = Encoding.UTF8.GetString(e.Body.Span); // Mesajı UTF-8 olarak çöz
    Console.WriteLine(message); // Mesajı ekrana yaz
};

Console.Read(); // Konsolun açık kalmasını sağla

// 1. Adım: Publisher'daki exchange ile birebir aynı isim ve type'a sahip bir exchange tanımlanmalıdır.
// 2. Adım: Publisher tarafından routing key'de bulunan değeri kuyruğa gönderilen mesajları kendi oluşturduğumuz kuyruğa yönlendirerek tüketmemiz gerekmektedir.
// Bunun için öncelikle bir kuyruk oluşturulmalıdır.
