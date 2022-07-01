using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Order.Logic.Persistence.Dtos;
using RabbitMQ.Client;
using System.Text;

namespace ordering_server.RabbitMQ
{
    public class RabbitMQProducer : IMessageProducer
    {
        private readonly ConfigurationString _config;
        public RabbitMQProducer(IOptions<ConfigurationString> config)
        {
            _config = config.Value;
        }

        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_config.RabbitMQString)
            };

            // create a connection and open a channel, dispose them when done
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            // ensure that the queue exists before we publish to it
            var queueName = "orders";
            bool durable = false;
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
