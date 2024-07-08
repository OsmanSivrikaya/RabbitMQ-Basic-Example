using System.Text;
using RabbitMQ.Client;

//Bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://dkqmxsrn:9d2EFpRPz22ELDJmBgIIxmU393NVxkFK@moose.rmq.cloudamqp.com/dkqmxsrn");

//Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue oluşturma
channel.QueueDeclare(queue:"example-1-queue", exclusive: false);

//queue'ya mesaj gönderme

// RabbitMQ kuyruğa atacağı mesajşaro byte türünden kabul etmektedir. Haliyle mesajları byte dönüştürmemiz gerekecektir.
byte[] message = Encoding.UTF8.GetBytes("Merhaba");
channel.BasicPublish(exchange: "", routingKey: "example-1-queue", body: message);

Console.Read();