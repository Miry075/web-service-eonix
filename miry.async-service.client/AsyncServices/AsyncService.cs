using System;
using System.Text;
using System.Text.Json;
using miry.async_service.client.Exeptions;
using miry.async_service.client.Helpers;
using RabbitMQ.Client;

namespace miry.async_service.client.AsyncServices
{
	public class AsyncService<TPublishedData> : IAsyncService<TPublishedData> where TPublishedData : class
	{

        protected readonly string _hostName;
        protected readonly string _port;
        protected readonly string _exchange;
        protected IConnection _connection;
        protected IModel _channel;
        protected string _queueName;

        public AsyncService(string hostName, string port, string exchange)
		{
            _hostName = hostName;
            _port = port;
            _exchange = exchange;
            _connection = RabbitMQConnectionHelper.InitilizeRabbitMQConnection(hostName, port);
        }

        public void PublishData(TPublishedData dataToBublish)
        {
            var message = JsonSerializer.Serialize(dataToBublish);
            SendMessage(message);
        }

        public void SendMessage(string message)
        {
            try
            {
                if (_connection.IsOpen)
                {
                    var body = Encoding.UTF8.GetBytes(message);
                    if (_channel != null && _channel.IsOpen)
                    {
                        _channel.BasicPublish(exchange: _exchange, routingKey: "", basicProperties: null, body: body);
                    }
                    else
                    {
                        throw new RabbitMQChannelException($"There is no channel created on the following connection {_connection.ToString} ");
                    }
                }
                else
                {
                    throw new RabbitMQConnectionException($"Connection on {_hostName}:{_port} has been closed ");
                }
            }
            catch
            {
                throw;
            }
        }

        public void Dispose()
        {
            RabbitMQConnectionHelper.DisposeRabbitMQRessource(_channel, _connection);
        }

        public void CreateExchange(string exchangeType)
        {
            _channel = RabbitMQConnectionHelper.CreateExchange(_connection, exchangeType, _exchange, RabbitMQ_ConnectionShutdown);
        }

        public void CreateQueue()
        {
            _queueName = RabbitMQConnectionHelper.CreateQueue(_channel, _exchange);
        }

        protected static void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("Connection has been closed");
        }
    }
}

