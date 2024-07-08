using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

// Bağlantı oluşturma
ConnectionFactory factory = new();
// RabbitMQ sunucusuna bağlanmak için URI belirtiyoruz
factory.Uri = new Uri("amqps://dkqmxsrn:9d2EFpRPz22ELDJmBgIIxmU393NVxkFK@moose.rmq.cloudamqp.com/dkqmxsrn");

// Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection();
// Bağlantıyı oluşturuyoruz ve kanalı açıyoruz
using IModel channel = connection.CreateModel();

// Exchange'i tanımlama (Headers tipi exchange)
channel.ExchangeDeclare("header-exchange-example", type: ExchangeType.Headers);

// Kullanıcıdan header value'sunu alma
Console.Write("Lütfen header value'sunu giriniz: ");
var value = Console.ReadLine();

// Kuyruk oluşturma ve bind etme
var queueName = channel.QueueDeclare().QueueName;
// Oluşturulan kuyruğu belirlenen exchange'e ve header bilgisi ile bağlama
channel.QueueBind(queue: queueName, exchange: "header-exchange-example", routingKey: string.Empty, new Dictionary<string, object> { ["no"] = value ?? string.Empty });

// Queue'dan mesaj alma
EventingBasicConsumer consumer = new(channel);
// Kuyruğu tüketiciye bağlama (mesajları alma)
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
consumer.Received += (sender, e) =>
{
    // Kuyruğa gelen mesajın işlendiği yer
    // e.Body: Kuyruktaki mesajın verisini bütünsel olarak getirir.
    // e.Body.Span veya e.Body.ToArray(): Kuyruktaki mesajın byte verisini getirir.
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

// Konsolu açık tutma
Console.Read();
