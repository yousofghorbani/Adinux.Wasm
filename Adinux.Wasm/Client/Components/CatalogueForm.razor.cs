using Adinux.Wasm.Client.Repository;
using Adinux.Wasm.Client.Services;
using Adinux.Wasm.Shared.Enums;
using Adinux.Wasm.Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Linq;

namespace Adinux.Wasm.Client.Components
{
    public partial class CatalogueForm
    {
        [CascadingParameter]
        public UserIdentityFormViewModel CurrentUserIdentityForm { get; set; }

        [Inject]
        public HttpRepository _repo { get; set; }

        [Inject]
        public ToastService _toastService { get; set; }

        [Inject]
        public IJSRuntime _jSRuntime { get; set; }


        List<CatalogueViewModel> catalogues = new List<CatalogueViewModel>();
        string selectedcCatalogueTab;
        List<SelectedCataloguesViewModel> selectedCatalogues = new List<SelectedCataloguesViewModel>();
        List<SendType> selectedSendTypes = new List<SendType>();
        bool showSharingOptions;

        protected override async Task OnInitializedAsync()
        {
            var res = await _repo.GetCatalogues();
            if (res.IsSuccesfull)
            {
                catalogues = res.Result;
                selectedcCatalogueTab = catalogues[0].Category;
            }
            else
                _toastService.ShowToast(res.Errors[0], Enums.ToastLevel.Error);
        }

      
        public void HandleOnCheckboxChange(CatTypeViewModel catType)
        {
            if(selectedCatalogues.Any(c => c.Name == catType.Name)) selectedCatalogues.Remove(selectedCatalogues.Find(c => c.Name == catType.Name));
            else
            {
                selectedCatalogues.Add(new SelectedCataloguesViewModel { Name = catType.Name,Files = catType.Files });
                foreach (var type in catType.Types)
                {
                    if(!selectedCatalogues.Any(s => s.Name == type.Name))
                     selectedCatalogues.Add(new SelectedCataloguesViewModel { Name = type.Name, Files = type.Files });
                }
            }
            _jSRuntime.InvokeVoidAsync("setindeterminateCheckboxes");
        }
        public void HandleOnCheckboxChange(TypeViewModel type)
        {
            if (selectedCatalogues.Any(c => c.Name == type.Name)) selectedCatalogues.Remove(selectedCatalogues.Find(c => c.Name == type.Name));
            else selectedCatalogues.Add(new SelectedCataloguesViewModel { Name = type.Name, Files = type.Files });

            _jSRuntime.InvokeVoidAsync("setindeterminateCheckboxes");
        }

        public void HandleSendType(SendType sendType)
        {
          bool isExist = selectedSendTypes.Contains(sendType);
            if (isExist) selectedSendTypes.Remove(sendType);
            else selectedSendTypes.Add(sendType);
        }

        public async Task HandleCataloguesRequestClicked()
        {
            var res = await _repo.CreateCataloguesRequest(new CataloguesRequestViewModel
            {
                UserId = CurrentUserIdentityForm.Id.Value,
                SelectedCatalogues = selectedCatalogues,
                SendTypes = selectedSendTypes

            });
            if (res.IsSuccesfull)
            {
                _toastService.ShowToast("Requested catalogs will be sent to you", Enums.ToastLevel.Success);
            }
            else
                _toastService.ShowToast(res.Errors[0], Enums.ToastLevel.Error);
        }

    }
}
