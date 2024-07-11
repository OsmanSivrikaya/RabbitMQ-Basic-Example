using MassTransit; // MassTransit kütüphanesini içe aktarır
using RabbitMQ.ESB.MassTransit.RequestResponsePattern.Shared.RequestResponseMessage; // RequestMessage ve ResponseMessage sınıflarını içe aktarır

namespace RabbitMQ.ESB.MassTransit.RequestResponsePattern.Consumer.Consumers
{
    public class RequestMessageConsumer : IConsumer<RequestMessage>
    {
        // IConsumer<T> arayüzünden gelen Consume metodu
        public async Task Consume(ConsumeContext<RequestMessage> context)
        {
            // Gelen mesajın metnini konsola yazdırır
            Console.WriteLine(context.Message.Text);

            // Gelen isteğe cevap olarak yeni bir ResponseMessage oluşturur ve gönderir
            await context.RespondAsync<ResponseMessage>(new() { Text = $"{context.Message.MessageNo}. response to request" });
        }
    }
}
