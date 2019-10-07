using pvone.Common.Models;
using pvone.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace pvone.ViewModels
{
    public class ProductsViewModel : BaseViewMode
    {

        private ApiService apiservice;

        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products
        {
            get { return this.products; }
            set {this.SetValue(ref this.products, value);}
        }

        public ProductsViewModel()
        {
            apiservice = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            var response = await apiservice.GetList<Product>("http://pvoneservice.somee.com", "/api", "/Products");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }

            var list = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(list);
        }
    }

}
