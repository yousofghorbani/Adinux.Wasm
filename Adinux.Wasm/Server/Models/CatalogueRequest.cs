using Adinux.Wasm.Shared.Enums;

namespace Adinux.Wasm.Server.Models
{
    public class CatalogueRequest
    {
        public int Id { get; set; }
        public int UserFormId { get; set; }

        //Seperated by comma
        public string CataloguesNames { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public UserForm UserForm { get; set; }
        public List<CatalogueRequestSendType> CatalogueRequestSendTypes { get; set; } = new List<CatalogueRequestSendType>();
    }



}
