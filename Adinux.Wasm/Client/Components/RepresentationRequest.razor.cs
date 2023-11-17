using Adinux.Wasm.Client.Repository;
using Adinux.Wasm.Client.Services;
using Adinux.Wasm.Shared.Enums;
using Adinux.Wasm.Shared.ViewModel;
using DNTPersianUtils.Core.IranCities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Adinux.Wasm.Client.Components
{
    public partial class RepresentationRequest
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

        string selectedpProvince;
        string selectedpCity = " ";
        RepresentationServiceType selectedRepresentationServiceType = RepresentationServiceType.Sell;
        RepresentationPersonType selectedRepresentationPersonType = RepresentationPersonType.RealCharacter;

        public async Task HandleRequestClicked()
        {
            var res = await _repo.CreateRepresentationRequest(new RepresentationRequestViewModel
            {
                UserId = CurrentUserIdentityForm.Id.Value,
                Province = selectedpProvince,
                City = selectedpCity,
                RepresentationPersonType = selectedRepresentationPersonType,
                RepresentationServiceType = selectedRepresentationServiceType

            });
            if (res.IsSuccesfull)
            {
                _toastService.ShowToast("Your request has been registered", Enums.ToastLevel.Success);
            }
            else
                _toastService.ShowToast(res.Errors[0], Enums.ToastLevel.Error);
        }
    }
}
