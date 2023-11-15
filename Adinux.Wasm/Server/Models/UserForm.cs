namespace Adinux.Wasm.Server.Models
{
    public class UserForm
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Company { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string? Comments { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<CatalogueRequest> CatalogueRequests { get; set; } = new List<CatalogueRequest>();
        public List<RepresentationRequest> RepresentationRequests { get; set; } = new List<RepresentationRequest>();
        public List<PriceRequest> PriceRequests { get; set; } = new List<PriceRequest>();
    }

}
