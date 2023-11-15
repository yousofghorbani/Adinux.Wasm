using Adinux.Wasm.Shared.Enums;

namespace Adinux.Wasm.Shared.ViewModel
{
    public class RepresentationRequestViewModel
    {
        public int UserId { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public RepresentationServiceType RepresentationServiceType { get; set; }
        public RepresentationPersonType RepresentationPersonType { get; set; }
        

    }

}
