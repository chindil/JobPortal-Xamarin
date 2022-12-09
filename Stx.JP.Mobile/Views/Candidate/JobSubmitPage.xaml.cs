using Stx.JP.Mobile.ViewModels.Candidate;
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
    public partial class JobSubmitPage : ContentPage
    {
        public JobSubmitPage(JobSubmitViewModel jobSubmitViewModel )
        {
            InitializeComponent();
            this.BindingContext = jobSubmitViewModel;
        }

        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}