using System;
using System.Text;
using RabbitMQ.Client;

// Bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://dkqmxsrn:9d2EFpRPz22ELDJmBgIIxmU393NVxkFK@moose.rmq.cloudamqp.com/dkqmxsrn");

// Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection(); // RabbitMQ'ya bağlantı oluştur
using IModel channel = connection.CreateModel(); // Kanal oluştur

// Exchange tanımlama
channel.ExchangeDeclare(exchange: "topic-exchange-example", type: ExchangeType.Topic); // Topic tipinde bir exchange tanımla

// 100 adet mesaj gönderme döngüsü
for (int i = 0; i < 100; i++)
{
    await Task.Delay(200); // Her mesaj arasında 200 milisaniye bekle
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}"); // Mesajı UTF-8 formatında byte dizisine dönüştür
    
    // Kullanıcıdan topic routing key'i al
    Console.Write("Mesjaın gönderileceği topic formatını belirtiniz: ");
    string topicRoutingKey = Console.ReadLine() ?? ""; // Kullanıcıdan alınan topic routing key'i (varsa)
    
    // Mesajı ilgili topic routing key'i ile gönder
    channel.BasicPublish(exchange: "topic-exchange-example", routingKey: topicRoutingKey, body: message); // Exchange'e mesajı gönder
}

Console.Read(); // Konsolun açık kalmasını sağla
