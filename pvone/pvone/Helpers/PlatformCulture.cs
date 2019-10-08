using System;

namespace pvone.Helpers
{
    public class PlatformCulture
    {

        public PlatformCulture(string platformCulturestring)
        {
            if (string.IsNullOrEmpty(platformCulturestring))
            {
                throw new ArgumentException("Expected culture identifier", "platformCulturestring");
            }

            PlatformString = platformCulturestring.Replace("_", "-");
            var dashIndex = PlatformString.IndexOf("-", StringComparison.Ordinal);

            if (dashIndex > 0)
            {
                var parts = PlatformString.Split('-');
                LanguageCode = parts[0];
                LocaleCode = parts[1];
            }
            else
            {
                LanguageCode = PlatformString;
                LocaleCode = string.Empty;
            }
        }

        public string PlatformString { get; private set; }
        public string LanguageCode { get; private set; }
        public string LocaleCode { get; private set; }

        public override string ToString()
        {
            return PlatformString;
        }
    }
}
