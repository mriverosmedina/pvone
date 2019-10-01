using System;
using System.Collections.Generic;
using System.Text;

namespace pvone.ViewModels
{
    public class MainViewModel
    {
        public ProductsViewModel Products { get; set; }

        public MainViewModel()
        {
            Products = new ProductsViewModel();
        }
    }
}
