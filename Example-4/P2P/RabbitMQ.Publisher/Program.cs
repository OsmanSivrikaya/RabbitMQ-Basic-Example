using System;
using System.Text;
using RabbitMQ.Client;

// Bağlantı oluşturma için ConnectionFactory sınıfı kullanılır
ConnectionFactory factory = new();
// RabbitMQ sunucu URI'si belirtilir
factory.Uri = new Uri("amqps://dkqmxsrn:9d2EFpRPz22ELDJmBgIIxmU393NVxkFK@moose.rmq.cloudamqp.com/dkqmxsrn");

// Bağlantı ve kanal oluşturulur
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// Queue oluşturma işlemi
var queueName = "example-p2p-queue";
// QueueDeclare metodu ile queue tanımlanır
channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false);

// Queue'ya mesaj gönderme işlemi
// Mesajlar byte dizisi olarak kabul edildiği için gönderilecek mesaj byte dizisine dönüştürülür
byte[] message = Encoding.UTF8.GetBytes("Merhaba");
// BasicPublish metodu ile mesaj gönderilir
channel.BasicPublish(exchange: string.Empty, routingKey: queueName, body: message);

// Konsolda okuma işlemi, kapatılmaması için bir tuşa basılması beklenir
Console.Read();
