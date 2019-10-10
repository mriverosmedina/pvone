using GalaSoft.MvvmLight.Command;
using pvone.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace pvone.ViewModels
{
    public class MainViewModel
    {
        public ProductsViewModel Products { get; set; }
        public AddProductViewModel AddProduct { get; set; }

        public MainViewModel()
        {
            Products = new ProductsViewModel();
        }

        public ICommand AddProductCommand
        {
            get
            {
                return new RelayCommand(GoToAddProduct);
            }
        }

        private async void GoToAddProduct()
        {
            AddProduct = new AddProductViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AddProductPage());
        }
    }
}
