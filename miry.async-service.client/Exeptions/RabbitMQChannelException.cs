using System;
namespace miry.async_service.client.Exeptions
{
	public class RabbitMQChannelException: Exception
    {
		public RabbitMQChannelException(string message):base (message)
		{
		}
	}
}

