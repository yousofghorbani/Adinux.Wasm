using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adinux.Wasm.Shared.ViewModel
{
    public class APIResponseViewModel
    {
        public bool IsSuccesfull { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    public class APIResponseViewModel<T>:APIResponseViewModel
    {
        public T Result { get; set; }
    }

}
