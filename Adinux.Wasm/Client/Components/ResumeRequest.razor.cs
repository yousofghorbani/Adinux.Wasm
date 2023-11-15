using Adinux.Wasm.Client.Repository;
using Adinux.Wasm.Client.Services;
using Adinux.Wasm.Shared.Enums;
using Adinux.Wasm.Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Adinux.Wasm.Client.Components
{
    public partial class ResumeRequest
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
        List<SendType> selectedSendTypes = new List<SendType>();

        public void HandleSendType(SendType sendType)
        {
            bool isExist = selectedSendTypes.Contains(sendType);
            if (isExist) selectedSendTypes.Remove(sendType);
            else selectedSendTypes.Add(sendType);
        }

        public async Task HandleRequestClicked()
        {
            var res = await _repo.CreateResumeRequest(new ResumeRequestViewModel
            {
                UserId = CurrentUserIdentityForm.Id.Value,
                SendTypes = selectedSendTypes

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
