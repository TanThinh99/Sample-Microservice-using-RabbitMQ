using BackOffice.Persistence.Dtos;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace BackOffice.RabbitMQ
{
    public class RabbitMQProducer : IMessageProducer
    {
        private readonly ConfigurationString _config;
        public RabbitMQProducer(IOptions<ConfigurationString> config) 
        {
            _config = config.Value;
        }
        public void SendMessage<T> (T message)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_config.RabbitMQString)
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var queueName = "back-orders";
            var durable = false;
            bool exclusive = false;
            bool autoDelete = true;

            channel.QueueDeclare(queueName, durable, exclusive, autoDelete, null);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            var exchangeName = "";
            var routingKey = queueName;

            channel.BasicPublish(exchangeName, routingKey, null, body);
        }
    }
}
