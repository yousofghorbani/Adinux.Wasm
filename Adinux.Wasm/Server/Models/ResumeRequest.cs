namespace Adinux.Wasm.Server.Models
{
    public class ResumeRequest
    {
        public int Id { get; set; }
        public int UserFormId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public UserForm UserForm { get; set; }
        public List<ResumeRequestSendType> ResumeRequestSendTypes { get; set; } = new List<ResumeRequestSendType>();
    }



}
