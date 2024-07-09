using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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

// Queue oluşturma ve exchange'e bağlama
var queueName = channel.QueueDeclare().QueueName; // Geçici bir kuyruk oluşturma, adı sunucu tarafından belirlenir
channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: string.Empty); // Kuyruğu exchange'e bağlama

// Queue'dan mesaj alma
EventingBasicConsumer consumer = new(channel); // Tüketici (consumer) oluşturma
// Mesajları kuyruktan almaya başlama
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

// Mesaj alındığında yapılacak işlemi tanımlama
consumer.Received += (sender, e) => {
    // Mesajın içeriğini byte dizisinden string'e dönüştürme ve yazdırma
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

// Konsolu açık tutmak için
Console.ReadLine();
