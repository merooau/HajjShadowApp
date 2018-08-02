using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using PilgrimageCommunication.cs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PilgrimageCommunication
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        CultureInfo ci = null;

        const string ResourceId = "PilgrimageCommunication.Resx.AppResources";

        static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(() => new ResourceManager(ResourceId, IntrospectionExtensions.GetTypeInfo(typeof(TranslateExtension)).Assembly));

        public string Text { get; set; }

        public TranslateExtension()
        {
            //ci = new CultureInfo(Application.Current.Properties["currentLanguage"] as string);
            ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();

        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return string.Empty;

            if (ci.TwoLetterISOLanguageName != Application.Current.Properties["currentLanguage"] as string)
            {
                ci = new CultureInfo(Application.Current.Properties["currentLanguage"] as string);
            }

            var translation = ResMgr.Value.GetString(Text, ci);
            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(
                    string.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, ResourceId, ci.Name), "Text");
#else
                translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
            }
            return translation;
        }
    }
}