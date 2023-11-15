using Adinux.Wasm.Client.Enums;
using Adinux.Wasm.Shared.ViewModel;

namespace Adinux.Wasm.Client.Pages
{
    public partial class ExpoForm
    {
        UserIdentityFormStep currentIdentityFormStep = UserIdentityFormStep.SignUp;
        UserIdentityFormService currentUserIdentityFormService = UserIdentityFormService.ChooseOption;

        UserIdentityFormViewModel currentUserIdentityForm;


        public void UserFormSubmitted(UserIdentityFormViewModel userIdentityForm)
        {
            this.currentUserIdentityForm = userIdentityForm;
            currentIdentityFormStep = UserIdentityFormStep.Services;
        }
    }
}
