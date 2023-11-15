using Adinux.Wasm.Client.Helper;
using Adinux.Wasm.Client.Repository;
using Adinux.Wasm.Client.Services;
using Adinux.Wasm.Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;

namespace Adinux.Wasm.Client.Components
{
    public partial class IdentityForm : IDisposable
    {
        [Inject]
        public HttpRepository _repo { get; set; }
        [Inject]
        public ToastService _toastService { get; set; }


        [Parameter]
        public EventCallback<UserIdentityFormViewModel> OnFormSubmitted { get; set; }

        public UserIdentityFormViewModel UserIdentityForm { get; set; } = new UserIdentityFormViewModel();

        bool _isBusy = false;
        private EditContext _editContext;

        protected override void OnInitialized()
        {
            _editContext = new EditContext(UserIdentityForm);
            _editContext.SetFieldCssClassProvider(new ValidationFieldClassProvider("inputValidate", "inputValidate errorValid errorerrorValid error"));
            //_editContext.OnFieldChanged += HandleFieldChanged;
        }

        public async Task HandleFormSubmit()
        {
            if (!_editContext.Validate() || _isBusy) return;

            _isBusy = true;
            var res = await _repo.GetOrCreateUserForm(UserIdentityForm);
            if (res.IsSuccesfull)
            {
                UserIdentityForm = new UserIdentityFormViewModel();
                await OnFormSubmitted.InvokeAsync(res.Result);
            }
            else
            {
                _isBusy = false;
                _toastService.ShowToast("An error has occurred", Enums.ToastLevel.Error);
                Console.WriteLine(res.Errors);
            }

        }


        //private void HandleFieldChanged(object sender, FieldChangedEventArgs e)
        //{

        //    _editContext.Validate();
        //    StateHasChanged();
        //}

        public void Dispose()
        {
            //_editContext.OnFieldChanged -= HandleFieldChanged;
        }


    }
}
