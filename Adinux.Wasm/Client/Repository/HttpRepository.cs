using Adinux.Wasm.Shared.ViewModel;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Adinux.Wasm.Client.Repository
{
    public class HttpRepository
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        public HttpRepository(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = false, };
           
        }
        public async Task<APIResponseViewModel<UserIdentityFormViewModel>> GetOrCreateUserForm(UserIdentityFormViewModel userIdentityForm)
        {
           
            var postResult = await _client.PostAsJsonAsync("/UserIdentityForm", userIdentityForm);
            return await postResult.Content.ReadFromJsonAsync<APIResponseViewModel<UserIdentityFormViewModel>>();
        }
        public async Task<APIResponseViewModel<List<CatalogueViewModel>>> GetCatalogues()
        {
            try
            {
           
                var catalogues = await _client.GetFromJsonAsync<List<CatalogueViewModel>>("/GetCatalogues");
                return new APIResponseViewModel<List<CatalogueViewModel>>()
                {
                    IsSuccesfull = true,
                    Result = catalogues
                };
            }
            catch (Exception ex)
            {

                return new APIResponseViewModel<List<CatalogueViewModel>>()
                {
                    IsSuccesfull = false,
                    Errors = new List<string>() { "An error has occurred" }
                };
                    Console.WriteLine(ex.ToString());
            }  
        }

        public async Task<APIResponseViewModel<string>> CreateCataloguesRequest(CataloguesRequestViewModel cataloguesRequestViewModel)
        {

            var postResult = await _client.PostAsJsonAsync("/CataloguesRequest", cataloguesRequestViewModel);
            return await postResult.Content.ReadFromJsonAsync<APIResponseViewModel<string>>();
        }
        public async Task<APIResponseViewModel<string>> CreateResumeRequest(ResumeRequestViewModel resumeRequestViewModel)
        {

            var postResult = await _client.PostAsJsonAsync("/ResumeRequest", resumeRequestViewModel);
            return await postResult.Content.ReadFromJsonAsync<APIResponseViewModel<string>>();
        }
        public async Task<APIResponseViewModel<string>> CreateRepresentationRequest(RepresentationRequestViewModel representationRequestView)
        {

            var postResult = await _client.PostAsJsonAsync("/RepresentationRequest", representationRequestView);
            return await postResult.Content.ReadFromJsonAsync<APIResponseViewModel<string>>();
        }
        public async Task<APIResponseViewModel<string>> CreatePriceRequest(PriceRequestFormViewModel priceRequestFormViewModel)
        {

            var postResult = await _client.PostAsJsonAsync("/PriceRequest", priceRequestFormViewModel);
            return await postResult.Content.ReadFromJsonAsync<APIResponseViewModel<string>>();
        }

    }
}
