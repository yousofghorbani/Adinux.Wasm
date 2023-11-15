using Adinux.Wasm.Client.Enums;
using Adinux.Wasm.Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace Adinux.Wasm.Client.Pages
{
    public partial class Index
    {
        UserIdentityFormStep currentIdentityFormStep = UserIdentityFormStep.SignUp;
        UserIdentityFormViewModel currentUserIdentityForm;

        public void UserFormSubmitted(UserIdentityFormViewModel userIdentityForm)
        {
            this.currentUserIdentityForm = userIdentityForm;
            currentIdentityFormStep = UserIdentityFormStep.Services;
        }
    }
}
