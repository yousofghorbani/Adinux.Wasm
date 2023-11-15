using Adinux.Wasm.Shared.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Adinux.Wasm.Server.Repository;
using Adinux.Wasm.Server.Services;
using Adinux.Wasm.Server.BackgroundServices.Queues;

namespace Adinux.Wasm.Server.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly DataRepository _repo;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly MailSenderService _mailSender;
        private CatalogueRequestSenderQueue _catalogueSenderQueue;

        public DataController(DataRepository dataRepository, IWebHostEnvironment hostEnvironment, CatalogueRequestSenderQueue catalogueSenderQueue)
        {
            _repo = dataRepository;
            _hostEnvironment = hostEnvironment;
            _catalogueSenderQueue = catalogueSenderQueue;
        }


        [Route("/UserIdentityForm")]
        [HttpPost]
        public async Task<IActionResult> UserIdentityForm(UserIdentityFormViewModel userIdentityForm)
        {
            if (userIdentityForm == null)
                return BadRequest();
            return Ok(await _repo.GetOrCreateUserForIdentityFormAsync(userIdentityForm));
        }

        [Route("/GetCatalogues")]
        [HttpGet]
        public async Task<IActionResult> GetCatalogues()
        {
            var rootPath = _hostEnvironment.WebRootPath; //get the root path

            var fullPath = Path.Combine(rootPath, "catalogues/catalogues.json"); //combine the root path with that of our json file inside mydata directory

            var jsonData = System.IO.File.ReadAllText(fullPath);
            return Ok(jsonData);
        }

        [Route("/CataloguesRequest")]
        [HttpPost]
        public async Task<IActionResult> CataloguesRequest(CataloguesRequestViewModel cataloguesRequestViewModel)
        {
            var result = await _repo.AddCataloguesRequestAsync(cataloguesRequestViewModel);
            if (result.IsSuccesfull)
            {
               await _catalogueSenderQueue.EnqueueAsync(result.Result);
            }
            return Ok(new APIResponseViewModel { IsSuccesfull = result.IsSuccesfull,Errors = result.Errors});
        }

        [Route("/RepresentationRequest")]
        [HttpPost]
        public async Task<IActionResult> RepresentationRequest(RepresentationRequestViewModel representationRequestViewModel)
        {
            var result = await _repo.AddRepresentationRequestAsync(representationRequestViewModel);
            
            return Ok(new APIResponseViewModel { IsSuccesfull = result.IsSuccesfull,Errors = result.Errors});
        } 
        [Route("/PriceRequest")]
        [HttpPost]
        public async Task<IActionResult> PriceRequest(PriceRequestFormViewModel priceRequestFormViewModel)
        {
            var result = await _repo.AddPriceRequestAsync(priceRequestFormViewModel);
            
            return Ok(new APIResponseViewModel { IsSuccesfull = result.IsSuccesfull,Errors = result.Errors});
        }

        [Route("/ResumeRequest")]
        [HttpPost]
        public async Task<IActionResult> ResumeRequest(ResumeRequestViewModel resumeRequestViewModel)
        {
            var result = await _repo.AddResumeRequestAsync(resumeRequestViewModel);
            
            return Ok(new APIResponseViewModel { IsSuccesfull = result.IsSuccesfull,Errors = result.Errors});
        }
    }
}
