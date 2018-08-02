using Xamarin.Forms;
using System.Reflection;
using Xamarin.Forms.Xaml;
using PilgrimageCommunication.Xamls;
using PilgrimageCommunication.cs;
using Plugin.Connectivity;
using System.Globalization;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PilgrimageCommunication
{
    public partial class App : Application
    {
        public static string CurrAppLanguage { get; set; }
        public static bool IsUserLoggedIn { get; set; }
        public static bool IsConnected { get; set; }

        public App()
        {
            InitializeComponent();

            System.Diagnostics.Debug.WriteLine("====== resource debug info =========");
            var assembly = typeof(App).GetTypeInfo().Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
                System.Diagnostics.Debug.WriteLine("found resource: " + res);
            System.Diagnostics.Debug.WriteLine("====================================");

            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resx.AppResources.Culture = ci; // set the RESX for resource localization
            DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods

            Application.Current.Properties["currentLanguage"] = ci.ToString();

            CurrAppLanguage = ci.ToString();

            IsConnected = DoIHaveInternet();

            if (!IsConnected)
            {
                MainPage = new NoInternetConnection();
            }
            else
            {
                
                    if (!IsUserLoggedIn)
                    {
                        MainPage = new NavigationPage(new Registratoin());
                    }
                    else
                    {
                    MainPage = new NavigationPage(new PilgrimsDashboard());
                    }

            }

            //MainPage = new PilgrimageCommunicationPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            IsConnected = DoIHaveInternet();

            if (!IsConnected)
            {
                MainPage = new NoInternetConnection();
            }
            else
            {
                 if (!IsUserLoggedIn)
                    {
                    MainPage = new NavigationPage(new Registratoin());
                    }
                    else
                    {
                    MainPage = new NavigationPage(new PilgrimsDashboard());
                    }
            }
        }

        public void ReloadApp(string language)
        {
            Resx.AppResources.Culture = new CultureInfo(language); // set the RESX for resource localization
            DependencyService.Get<ILocalize>().SetLocale(new CultureInfo(language)); // set the Thread for locale-aware methods
            Application.Current.Properties["currentLanguage"] = language;
            CurrAppLanguage = language;

            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new Registratoin());
            }
            else
            {
                MainPage = new NavigationPage(new PilgrimsDashboard());
            }
        }

        public bool DoIHaveInternet()
        {
            return CrossConnectivity.Current.IsConnected;
        }
    }
}