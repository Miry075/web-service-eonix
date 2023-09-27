using System;
using miry.async_service.client.Exeptions;
using RabbitMQ.Client;

namespace miry.async_service.client.Helpers
{
	public static class RabbitMQConnectionHelper
	{
        public static IConnection InitilizeRabbitMQConnection(string hostName, string port)
        {
            var factory = new ConnectionFactory() { HostName = hostName, Port = int.Parse(port) };
            try
            {
                return factory.CreateConnection();
            }
            catch
            {
                throw;
            }
        }

        public static void DisposeRabbitMQRessource(IModel channel, IConnection connection)
        {
            if (channel.IsOpen)
            {
                channel.Close();
                connection.Close();
            }
        }

        public static IModel CreateExchange(IConnection connection, string exchangeType, string exchange, EventHandler<ShutdownEventArgs> RabbitMQ_ConnectionShutdown)
        {
            try
            {
                var channel = connection.CreateModel();
                channel.ExchangeDeclare(exchange: exchange, type: exchangeType);
                connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                return channel;
            }
            catch
            {
                throw;
            }
        }

        public static string CreateQueue(IModel channel, string exchange)
        {
            if (!channel.IsOpen)
            {
                throw new RabbitMQChannelException($"There is no channel created on the following connection");
            }
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName, exchange: exchange, routingKey: "");
            return queueName;
        }
    }
}

