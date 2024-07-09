using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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

// Queue'dan mesaj alma işlemi
// EventingBasicConsumer sınıfı ile tüketici oluşturulur
EventingBasicConsumer consumer = new(channel);
// BasicConsume metodu ile mesajları almaya başlar
channel.BasicConsume(queue: queueName, autoAck: false, consumer);
// Received event'i ile gelen mesajları işler
consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};
