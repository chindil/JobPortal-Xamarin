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
    public partial class JobDetailPage : ContentPage
    {
        //public JobDetailPage()
        //{
        //    InitializeComponent();
        //}
        public JobDetailPage(JobDetailViewModel jobDetailViewModel)
        {
            InitializeComponent();
            this.BindingContext = jobDetailViewModel;
        }

        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}