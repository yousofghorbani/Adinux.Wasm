using Adinux.Wasm.Shared.Enums;

namespace Adinux.Wasm.Server.Models
{
    public class ResumeRequestSendType
    {
        public int Id { get; set; }
        public int ResumeRequestId { get; set; }
        public SendType SendType { get; set; }
        public bool? IsSuccessful { get; set; }
        public string? Error { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ResumeRequest ResumeRequest { get; set; }

    }

}
