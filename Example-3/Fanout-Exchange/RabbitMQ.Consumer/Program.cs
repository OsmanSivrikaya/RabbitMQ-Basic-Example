using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

// Bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://dkqmxsrn:9d2EFpRPz22ELDJmBgIIxmU393NVxkFK@moose.rmq.cloudamqp.com/dkqmxsrn");

// Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection(); // RabbitMQ'ya bağlantı oluştur
using IModel channel = connection.CreateModel(); // Kanal oluştur

// Exchange tanımlama
channel.ExchangeDeclare(exchange: "fanout-exchange-example", type: ExchangeType.Fanout); // Fanout tipinde bir exchange tanımla

// Kullanıcıdan kuyruk adını al
Console.Write("Kuyruk adını giriniz: ");
string _queueName = Console.ReadLine() ?? ""; // Kullanıcıdan alınan kuyruk adı (varsa)

// Kuyruk tanımlama
channel.QueueDeclare(queue: _queueName, exclusive: false); // Belirtilen kuyruğu tanımla

// Exchange ile kuyruğu bağlama
channel.QueueBind(queue: _queueName, exchange: "fanout-exchange-example", routingKey: string.Empty); // Exchange'e kuyruğu bağla

// Tüketici oluşturma ve mesajları tüketme
EventingBasicConsumer consumer = new(channel); // Tüketici oluştur
channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer); // Kuyruktan mesajları tüketmeye başla
consumer.Received += (sender, e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.Span); // Gelen mesajı UTF-8 formatında oku
    Console.WriteLine(message); // Mesajı ekrana yaz
};

Console.Read(); // Konsolun açık kalmasını sağla
