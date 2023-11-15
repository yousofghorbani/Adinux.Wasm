using System.ComponentModel;

namespace Adinux.Wasm.Client.Enums
{
    public enum UserIdentityFormService
    {
        [Description("Please select an option")]
        ChooseOption,
        [Description("Representation request")]
        RepresentationRequest,
        [Description("Catalog request")]
        CatalogueRequest,
        [Description("Price request")]
        PriceRequest, 
        [Description("Request resume / portfolio")]
        ResumeRequest,

    }
}
