using GalaSoft.MvvmLight.Command;
using pvone.Common.Models;
using pvone.Helpers;
using pvone.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
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
            set { this.SetValue(ref this.products, value); }
        }


        private bool isRefreshing;
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public ProductsViewModel()
        {
            apiservice = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            this.IsRefreshing = true;
            Response cnn = await this.apiservice.CheckConnection();

            if (!cnn.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, cnn.Message, Languages.Accept);
                return;
            }
            string url = Application.Current.Resources["UrlAPI"].ToString();
            string prefix = Application.Current.Resources["UrlPrefix"].ToString();
            string controller = Application.Current.Resources["UrlControllers"].ToString();
            Response response = await apiservice.GetList<Product>(url, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            List<Product> list = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(list);
            this.IsRefreshing = false;
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadProducts);
            }
        }
    }

}
