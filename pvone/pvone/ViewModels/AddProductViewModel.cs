using GalaSoft.MvvmLight.Command;
using pvone.Common.Models;
using pvone.Helpers;
using pvone.Service;
using System.Windows.Input;
using Xamarin.Forms;


namespace pvone.ViewModels
{
    public class AddProductViewModel : BaseViewMode
    {
        #region Atributos
        private ApiService apiservice;
        private bool isRunnig;
        private bool isEnabled;
        #endregion

        #region Propiedades
        public string Description { get; set; }

        public string Price { get; set; }

        public string Remarks { get; set; }

        public bool IsRunning
        {
            get { return this.isRunnig; }
            set { this.SetValue(ref this.isRunnig, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }
        #endregion

        #region Constructores
        public AddProductViewModel()
        {
            this.apiservice = new ApiService();
            this.IsEnabled = true;
        }
        #endregion

        #region Comandos
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.Description))
            {
                await Application.Current.MainPage.DisplayAlert(
                     Languages.Error,
                     Languages.DescriptionError,
                     Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Price))
            {
                await Application.Current.MainPage.DisplayAlert(
                     Languages.Error,
                     Languages.PriceError,
                     Languages.Accept);
                return;
            }

            decimal price = decimal.Parse(this.Price);
            if (price < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                     Languages.Error,
                     Languages.PriceError,
                     Languages.Accept);
                return;
            }
            //
            this.IsRunning = true;
            this.IsEnabled = false;

            Response cnn = await this.apiservice.CheckConnection();
            if (!cnn.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, cnn.Message, Languages.Accept);
                return;
            }
            Product product = new Product
            {
                Description = this.Description,
                Price = price,
                Remarks = this.Remarks,
            };
            string url = Application.Current.Resources["UrlAPI"].ToString();
            string prefix = Application.Current.Resources["UrlPrefix"].ToString();
            string controller = Application.Current.Resources["UrlControllers"].ToString();
            Response response = await apiservice.Post(url, prefix, controller, product);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            this.IsRunning = false;
            this.IsEnabled = true;
            await Application.Current.MainPage.Navigation.PopAsync();


        }
        #endregion

    }
}
