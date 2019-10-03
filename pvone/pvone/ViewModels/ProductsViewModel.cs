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
            get => products;
            set => SetValue(ref products, value);
        }
        public ProductsViewModel()
        {
            apiservice = new ApiService();
            LoadProducts();
        }

        private async void LoadProducts()
        {
            Response response = await apiservice.GetList<Product>("http://localhost:51083", "/api", "/products");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }

            List<Product> list = (List<Product>)response.Result;
            Products = new ObservableCollection<Product>(list);
        }
    }

}
