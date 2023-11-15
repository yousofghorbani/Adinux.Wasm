using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adinux.Wasm.Shared.Enums
{
    public enum SendType
    {
        [Description("Whats app")]
        Whatsapp,
        [Description("Telegram")]
        Telegram,
        [Description("Email")]
        Email
    }


}
