using Adinux.Wasm.Server.DTOs;
using Adinux.Wasm.Server.Repository;
using Adinux.Wasm.Server.Services;
using Adinux.Wasm.Shared.Enums;
using Adinux.Wasm.Shared.ViewModel;

namespace Adinux.Wasm.Server.BackgroundServices.Queues
{
    internal class CatalogueRequestSenderDequeuer: IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CatalogueRequestSenderQueue _updatesQueue;
        private readonly CancellationTokenSource _stopTokenSource;
        private readonly WTelegramService _wTelegramService;
        MailSenderService _mailSenderService;

        private Task _dequeueMessagesTask;


        public CatalogueRequestSenderDequeuer(
            IServiceProvider serviceProvider,
            CatalogueRequestSenderQueue queuer,
            MailSenderService mailSenderService
           /* ,WTelegramService wT*/)
        {
            _serviceProvider = serviceProvider;
            _updatesQueue = queuer;
            _stopTokenSource = new CancellationTokenSource();
            _mailSenderService = mailSenderService;
           // _wTelegramService = wT;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _dequeueMessagesTask = Task.Run(DequeueMessagesAsync);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _stopTokenSource.Cancel();

            return Task.WhenAny(_dequeueMessagesTask, Task.Delay(Timeout.Infinite, cancellationToken));
        }

        // <summary>
        // پروسه Queue
        // </summary>
        // <returns></returns>
        private async Task DequeueMessagesAsync()
        {
            try // شاید رو شررط حلقه خطا بخوره واسه همین اینجا خم ترای گذاشتم
            {
                while (!_stopTokenSource.IsCancellationRequested)
                {
                    CatalogueRequestSenderQueueDTO catalogueRequest = null;
                    try
                    {
                        catalogueRequest = await _updatesQueue.DequeueAsync(_stopTokenSource.Token);
                        if (catalogueRequest != null)
                        {
                            if (!_stopTokenSource.IsCancellationRequested)
                            {
                                if (catalogueRequest.CatalogueRequestSendTypes.Any(c => c.SendType == SendType.Email))
                                {
                                   var result = await _mailSenderService.SendCataloguesAsync(catalogueRequest.Email,catalogueRequest.FullName, catalogueRequest.SelectedCatalogues);
                                    await UpdateEntityAsync(catalogueRequest, result,SendType.Email);
                                }

                                if (catalogueRequest.CatalogueRequestSendTypes.Any(c => c.SendType == SendType.Telegram))
                                {
                                    var result = await _wTelegramService.SendCataloguesAsync(catalogueRequest.Mobile, catalogueRequest.FullName, catalogueRequest.SelectedCatalogues);
                                    await UpdateEntityAsync(catalogueRequest, result,SendType.Telegram);

                                }
                                //await  Task.Delay(2000);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        using (IServiceScope serviceScope = _serviceProvider.CreateScope())
                        {

                            //TelegramLoggerService _telegramLogger =
                            //   serviceScope.ServiceProvider.GetRequiredService<TelegramLoggerService>();
                            //await _telegramLogger.Error(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (IServiceScope serviceScope = _serviceProvider.CreateScope())
                {

                    //TelegramLoggerService _telegramLogger =
                    //   serviceScope.ServiceProvider.GetRequiredService<TelegramLoggerService>();
                    //await _telegramLogger.Error(ex);
                }
            }
        }

        private async Task UpdateEntityAsync(CatalogueRequestSenderQueueDTO catalogueRequest, APIResponseViewModel result,SendType sendType)
        {
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            DataRepository _repo = serviceScope.ServiceProvider.GetRequiredService<DataRepository>();
            var catalogueRequestSendType = catalogueRequest.CatalogueRequestSendTypes.Where(c => c.SendType == sendType).First();
            catalogueRequestSendType.IsSuccessful = result.IsSuccesfull;
            if (!result.IsSuccesfull)
                catalogueRequestSendType.Error = result.Errors[0];
            await _repo.UpdateCatalogueRequestSendTypeAsync(catalogueRequestSendType);

        }

    }
}
