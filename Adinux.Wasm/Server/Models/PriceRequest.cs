namespace Adinux.Wasm.Server.Models
{
    public class PriceRequest
    {
        public int Id { get; set; }
        public int UserFormId { get; set; }

        public string ProjectName { get; set; }

        public string ProjectAddress { get; set; }

        public string? Desc { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public UserForm UserForm { get; set; }
    }



}
