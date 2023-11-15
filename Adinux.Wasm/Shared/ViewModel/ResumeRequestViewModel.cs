using Adinux.Wasm.Shared.Enums;

namespace Adinux.Wasm.Shared.ViewModel
{
    public class ResumeRequestViewModel
    {
        public int UserId { get; set; }
        public List<SendType> SendTypes { get; set; } = new List<SendType>();
    }

}
