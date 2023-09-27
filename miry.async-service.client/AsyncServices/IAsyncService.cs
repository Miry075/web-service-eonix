using System;
namespace miry.async_service.client.AsyncServices
{
	public interface IAsyncService<TPublishedData>
    {
        void PublishData(TPublishedData publishedData);

        void SendMessage(string message);
    }
}

