using System;
using System.Collections.Generic;
using PilgrimageCommunication.Resx;

using Xamarin.Forms;

namespace PilgrimageCommunication.Xamls
{
    public partial class Registratoin : ContentPage
    {
        

        public Registratoin()
        {
            InitializeComponent();
        }


        async void OnSendVerificationCodeButtonClicked(object sender, EventArgs e)
        {

            App.IsUserLoggedIn = true;
            await Navigation.PushAsync(new Verification());
            //await Navigation.PopAsync();
            /*
            else
            {
                await DisplayAlert(AppResources.FailedLoginMsgTitle, AppResources.FailedLoginTxt, AppResources.FailedLoginMsgBtn);
                passwordEntry.Text = string.Empty;
            }
            */
        }
    }
}
