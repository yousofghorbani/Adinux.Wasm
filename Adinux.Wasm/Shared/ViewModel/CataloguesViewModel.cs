using Adinux.Wasm.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adinux.Wasm.Shared.ViewModel
{
    public class CatalogueViewModel
    {
        public string Category { get; set; }
        public List<CatTypeViewModel> CatTypes { get; set; } = new List<CatTypeViewModel>();
    }

    public class CatTypeViewModel
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Files { get; set; }
        public string Recmonded { get; set; }
        public List<TypeViewModel> Types { get; set; } = new List<TypeViewModel>();

        public bool IsOpen { get; set; }

    }

    public class TypeViewModel
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Files { get; set; }
        public string Recmonded { get; set; }
    }

    public class SelectedCataloguesViewModel
    {
        public string Name { get; set; }
        public string Files { get; set; }
    }

    public class CataloguesRequestViewModel
    {
        public int UserId { get; set; }
        public List<SelectedCataloguesViewModel> SelectedCatalogues { get; set; } = new List<SelectedCataloguesViewModel>();
        public List<SendType> SendTypes { get; set; } = new List<SendType>();

    } 

}
