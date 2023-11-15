using Adinux.Wasm.Server.Models;
using Adinux.Wasm.Shared.ViewModel;

namespace Adinux.Wasm.Server.DTOs
{
    public class CatalogueRequestSenderQueueDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

        public List<SelectedCataloguesViewModel> SelectedCatalogues { get; set; } = new List<SelectedCataloguesViewModel>();
        public List<CatalogueRequestSendType> CatalogueRequestSendTypes { get; set; } = new List<CatalogueRequestSendType>();

        public string FullName => FirstName + " " + LastName;
    }
}
