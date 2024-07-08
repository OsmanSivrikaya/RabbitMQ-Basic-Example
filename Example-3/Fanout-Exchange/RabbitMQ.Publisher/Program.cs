using System;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

// Bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://dkqmxsrn:9d2EFpRPz22ELDJmBgIIxmU393NVxkFK@moose.rmq.cloudamqp.com/dkqmxsrn");

// Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection(); // Bağlantıyı oluştur
using IModel channel = connection.CreateModel(); // Kanal oluştur

// Exchange tanımlama
channel.ExchangeDeclare(exchange: "fanout-exchange-example", type: ExchangeType.Fanout); // Fanout tipinde bir exchange tanımla

// 100 adet mesaj gönderme döngüsü
for (int i = 0; i < 100; i++)
{
    await Task.Delay(200); // Her mesaj arasında 200 milisaniye bekle
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}"); // Mesajı UTF-8 formatında byte dizisine dönüştür
    channel.BasicPublish(exchange: "fanout-exchange-example", routingKey: string.Empty, body: message); // Exchange'e mesajı gönder
}

Console.Read(); // Konsolun açık kalmasını sağla
