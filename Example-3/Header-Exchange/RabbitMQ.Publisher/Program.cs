using System.Text;
using RabbitMQ.Client;

//Bağlantı oluşturma
ConnectionFactory factory = new();
// RabbitMQ sunucusuna bağlanmak için URI belirtiyoruz
factory.Uri = new Uri("amqps://dkqmxsrn:9d2EFpRPz22ELDJmBgIIxmU393NVxkFK@moose.rmq.cloudamqp.com/dkqmxsrn");

//Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection();
// Bağlantıyı oluşturuyoruz ve kanalı açıyoruz
using IModel channel = connection.CreateModel();

// Exchange'i tanımlama (Headers tipi exchange)
channel.ExchangeDeclare("header-exchange-example", type: ExchangeType.Headers);

// 100 mesaj göndermek için döngü
for (int i = 0; i < 100; i++)
{
    // 200 milisaniye bekle
    await Task.Delay(200);
    // Mesajı UTF8 olarak byte dizisine çevirme
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");
    // Kullanıcıdan header değerini alma
    Console.Write("Lütfen header value'sunu giriniz: ");
    var value = Console.ReadLine();

    // Mesajın header bilgilerini ayarlama
    IBasicProperties basicProperties = channel.CreateBasicProperties();
    basicProperties.Headers = new Dictionary<string, object>{
        ["no"] = value ?? string.Empty
    };

    // Mesajı belirlenen exchange'e ve header bilgisi ile gönderme
    channel.BasicPublish(exchange: "header-exchange-example", routingKey: string.Empty, body: message, basicProperties: basicProperties);
}

// Konsolu açık tutma
Console.Read();
