namespace RabbitMQ.ESB.MassTransit.RequestResponsePattern.Shared.RequestResponseMessage
{
    public record RequestMessage
    {
        public int MessageNo { get; set; }
        public string Text { get; set; }
    }
}