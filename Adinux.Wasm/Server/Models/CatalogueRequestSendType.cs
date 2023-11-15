using Adinux.Wasm.Shared.Enums;

namespace Adinux.Wasm.Server.Models
{
    public class CatalogueRequestSendType
    {
        public int Id { get; set; }
        public int CatalogueRequestId { get; set; }
        public SendType SendType { get; set; }
        public bool? IsSuccessful { get; set; }
        public string? Error { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public CatalogueRequest CatalogueRequest { get; set; }

    } 

}
