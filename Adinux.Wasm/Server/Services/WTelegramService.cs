using Adinux.Wasm.Shared.ViewModel;
using Microsoft.Extensions.Options;
using TL;

namespace Adinux.Wasm.Server.Services
{
    public class WTelegramService : BackgroundService
    {
        public Task<string> ConfigNeeded() => _configNeeded.Task;
        private TaskCompletionSource<string> _configNeeded = new();
        private readonly ManualResetEventSlim _configRequest = new();
        private readonly IWebHostEnvironment _hostEnvironment;
        private string _configValue;

        private readonly WTelegram.Client _client;
        public User User { get; private set; }

        public WTelegramService(IWebHostEnvironment hostEnvironment)
        {
            _client = new WTelegram.Client(Config);
            _hostEnvironment = hostEnvironment;
        }


        public void ReplyConfig(string value)
        {
            _configValue = value;
            _configNeeded = new();
            _configRequest.Set();
        }

        public async Task<APIResponseViewModel> SendCataloguesAsync(string to, string name, List<SelectedCataloguesViewModel> selectedCatalogues)
        {
            try
            {
                var contacts = await _client.Contacts_ImportContacts(new[] { new InputPhoneContact { phone = to } });
                if (contacts.imported.Length > 0)
                {
                    var user = contacts.users[contacts.imported[0].user_id];
                    var text = $"Hello <u>dear <b>{HtmlText.Escape(name)}</b></u>\n" +
            "Enjoy this <code>userbot</code> written with";
                    var entities = _client.HtmlToEntities(ref text);
                    var sent = await _client.SendMessageAsync(user, text, entities: entities);
                   
                    var rootPath = _hostEnvironment.WebRootPath; //get the root path
                    foreach (var item in selectedCatalogues)
                    {
                        if (!string.IsNullOrEmpty(item.Files))
                        {

                            foreach (var file in item.Files.Split(","))
                            {
                                var fullPath = Path.Combine(rootPath, "catalogues/", file); //combine the root path with that of our json file inside mydata directory
                                var inputFile = await _client.UploadFileAsync(fullPath);
                                await _client.SendMediaAsync(user, null, inputFile);
                            }
                        }
                    }


                    return new APIResponseViewModel
                    {
                        IsSuccesfull = true
                    };
                }
               return new APIResponseViewModel { Errors = new List<string> { "Something Happend" } };

            }
            catch (Exception ex)
            {
                return new APIResponseViewModel { Errors = new List<string> { ex.ToString() } };
            }
        }


        private string Config(string what)
        {
            switch (what)
            {
                case "verification_code":
                case "password":
                    _configNeeded.SetResult(what);
                    _configRequest.Wait();
                    _configRequest.Reset();
                    return _configValue;
                case "api_id": return "9214983";
                case "api_hash": return "07ac6dd4601428b0b51a0d79b0a58dee";
                case "phone_number": return "+989103275992";
                case "first_name": return "John";      // if sign-up is required
                case "last_name": return "Doe";        // if sign-up is required
                default: return null;                  // let WTelegramClient decide the default config
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                User = await _client.LoginUserIfNeeded();
            }
            catch (Exception ex)
            {
                _configNeeded.SetException(ex);
                //throw;
            }
            _configNeeded.SetResult(null); // login complete
        }
    }
}
