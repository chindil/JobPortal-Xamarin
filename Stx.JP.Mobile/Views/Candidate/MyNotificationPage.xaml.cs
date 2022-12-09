using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stx.JP.Mobile.Views.Candidate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyNotificationPage : ContentPage
    {
        public MyNotificationPage()
        {
            InitializeComponent();
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopAsync();

        }
    }
}