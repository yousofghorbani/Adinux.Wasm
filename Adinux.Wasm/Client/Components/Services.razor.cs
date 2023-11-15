using Adinux.Wasm.Client.Enums;
using Adinux.Wasm.Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace Adinux.Wasm.Client.Components
{
    public partial class Services
    {
        //[CascadingParameter]
        //public UserIdentityFormViewModel CurrentUserIdentityForm { get; set; }
        [Parameter]
        public EventCallback<bool> BackToSignUp { get; set; }
        [Parameter]
        public EventCallback<UserIdentityFormService> CurrentUserIdentityFormService { get; set; }
    }
}
