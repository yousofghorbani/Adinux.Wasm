using Adinux.Wasm.Client.Helper;
using Adinux.Wasm.Client.Repository;
using Adinux.Wasm.Client.Services;
using Adinux.Wasm.Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace Adinux.Wasm.Client.Components
{
    public partial class PriceRequest
    {
        [Parameter]
        public EventCallback<bool> BackToSignUp { get; set; }
        [CascadingParameter]
        public UserIdentityFormViewModel CurrentUserIdentityForm { get; set; }

        [Inject]
        public HttpRepository _repo { get; set; }

        [Inject]
        public ToastService _toastService { get; set; }

        [Inject]
        public IJSRuntime _jSRuntime { get; set; }
        private EditContext _editContext;
        bool _isBusy = false;
        public PriceRequestFormViewModel PriceRequestForm { get; set; } = new PriceRequestFormViewModel();


        protected override void OnInitialized()
        {
            _editContext = new EditContext(PriceRequestForm);
            _editContext.SetFieldCssClassProvider(new ValidationFieldClassProvider("inputValidate", "inputValidate errorValid errorerrorValid error"));
            //_editContext.OnFieldChanged += HandleFieldChanged;
        }

        public async Task HandleFormSubmit()
        {
            if (!_editContext.Validate() || _isBusy) return;

            _isBusy = true;
            PriceRequestForm.UserId = CurrentUserIdentityForm.Id.Value;
            var res = await _repo.CreatePriceRequest(PriceRequestForm);
            if (res.IsSuccesfull)
            {
                _toastService.ShowToast("Your request has been registered", Enums.ToastLevel.Success);
                PriceRequestForm = new PriceRequestFormViewModel();
            }
            else
            {
                _isBusy = false;
                _toastService.ShowToast("An error has occurred", Enums.ToastLevel.Error);
                Console.WriteLine(res.Errors);
            }

        }
    }
}
