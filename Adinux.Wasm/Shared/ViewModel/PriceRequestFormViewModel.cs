using System.ComponentModel.DataAnnotations;

namespace Adinux.Wasm.Shared.ViewModel
{
    public class PriceRequestFormViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter the name of the project")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Please enter your project address")]
        public string ProjectAddress { get; set; }

        public string? Desc { get; set; }
    }
}
