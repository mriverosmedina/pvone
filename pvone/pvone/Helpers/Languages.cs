using pvone.Interfaces;
using pvone.Resources;
using Xamarin.Forms;

namespace pvone.Helpers
{
    public class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Error => Resource.Error;
        public static string Accept => Resource.Accept;
        public static string NoInternet => Resource.NoInternet;
        public static string Products => Resource.Products;
        public static string TurnOnInternet => Resource.TurnOnInternet;
        public static string AddProducto => Resource.AddProduct;
        public static string Description => Resource.Description;
        public static string DescriptionPlaceholder => Resource.DescriptionPlaceholder;
        public static string Price => Resource.Price;
        public static string PricePlaceholder => Resource.PricePlaceholder;
        public static string Remarks => Resource.Remarks;
        public static string Save => Resource.Save;
        public static string ChangeImage => Resource.ChangeImage;
    }
}
