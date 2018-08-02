using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PilgrimageCommunication.Xamls
{
    public partial class Verification : ContentPage
    {
        public Verification()
        {
            InitializeComponent();
        }

        async void OnConfirmCodeButtonClicked(object sender, EventArgs e)
        {

            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
            Navigation.InsertPageBefore(new PilgrimsDashboard(), this);
            await Navigation.PopAsync();

        }
    }
}
