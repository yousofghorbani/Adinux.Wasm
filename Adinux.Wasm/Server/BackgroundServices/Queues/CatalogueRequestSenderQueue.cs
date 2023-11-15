using Adinux.Wasm.Server.DTOs;
using System.Collections.Concurrent;

namespace Adinux.Wasm.Server.BackgroundServices.Queues
{
    

    public class CatalogueRequestSenderQueue
    {
        private readonly ConcurrentQueue<CatalogueRequestSenderQueueDTO> _catalogueRequests = new ConcurrentQueue<CatalogueRequestSenderQueueDTO>();
        private readonly SemaphoreSlim _messageEnqueuedSignal = new SemaphoreSlim(0);
       
        public async Task EnqueueAsync(CatalogueRequestSenderQueueDTO catalogueRequest)
        {
            try
            {

                if (_catalogueRequests.Count > 2)
                {
                    int counter = 0;
                    await SendMessagesInQueueToDeveloperAsync(counter);
                }

                await AddToQueueAsync(catalogueRequest);
            }
            catch (Exception ex)
            {

            }
        }

        private async Task AddToQueueAsync(CatalogueRequestSenderQueueDTO catalogueRequest)
        {
            try
            {

                _catalogueRequests.Enqueue(catalogueRequest);
                _messageEnqueuedSignal.Release();

            }
            catch (Exception ex)
            {

            }
        }

        public async Task<CatalogueRequestSenderQueueDTO> DequeueAsync(CancellationToken cancellationToken)
        {
            await _messageEnqueuedSignal.WaitAsync(cancellationToken);

            _catalogueRequests.TryDequeue(out CatalogueRequestSenderQueueDTO catalogueRequestSender);

            return catalogueRequestSender;
        }

        private async Task SendMessagesInQueueToDeveloperAsync(int counter)
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }
    }
}
