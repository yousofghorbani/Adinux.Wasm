using Adinux.Wasm.Shared.Enums;

namespace Adinux.Wasm.Server.Models
{
    public class RepresentationRequest
    {
        public int Id { get; set; }
        public int UserFormId { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public RepresentationServiceType RepresentationServiceType { get; set; }
        public RepresentationPersonType RepresentationPersonType { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public UserForm UserForm { get; set; }
    } 



}
