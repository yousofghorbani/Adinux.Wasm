using DNTPersianUtils.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adinux.Wasm.Shared.ViewModel
{
    public class UserIdentityFormViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        public string FirstName { get; set; }

       // [Required(ErrorMessage = "لطفا نام خانوادگی خود را وارد کنید")]
        public string? LastName { get; set; }

        public string? Company { get; set; }

        [Required(ErrorMessage = "please enter your Email")]
        [EmailAddress(ErrorMessage = "Please enter your email correctly")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا موبایل خود را وارد کنید")]
        //[ValidIranianMobileNumber(ErrorMessage ="لطفا موبایل خود را به صورت صحیح وارد کنید")]//TODO
        public string Mobile { get; set; }
    }
}
