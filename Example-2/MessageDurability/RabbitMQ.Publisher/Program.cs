﻿using System.Text;
using RabbitMQ.Client;

// Bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://dkqmxsrn:9d2EFpRPz22ELDJmBgIIxmU393NVxkFK@moose.rmq.cloudamqp.com/dkqmxsrn");

//Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// Queue oluşturma
// queue'yi kalıcı hale getirmek için durable true yapıyoruz
channel.QueueDeclare(queue: "example-1-queue", exclusive: false, durable: true);

// queue'ya mesaj gönderme

// mesajları kalıcı hale getirmek için oluşturulur ve publish içerisinde parametre olarak eklenir
IBasicProperties properties = channel.CreateBasicProperties();
properties.Persistent = true;

// RabbitMQ kuyruğa atacağı mesajşaro byte türünden kabul etmektedir. Haliyle mesajları byte dönüştürmemiz gerekecektir.
for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes("Merhaba " + i);
    channel.BasicPublish(exchange: "", routingKey: "example-1-queue", body: message, basicProperties: properties);
}

Console.Read();