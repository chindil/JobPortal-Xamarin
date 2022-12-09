using Stx.JP.Mobile.ViewModels.Profile;
using Stx.Shared.Interfaces.HRM;
using Stx.Shared.Models;
using Stx.Shared.Models.HRM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stx.JP.Mobile.Views.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExperienceEditPage : ContentPage
    {
        public ExperienceEditPage(ResumeCompEditViewModel resumeCompViewmodel)
        {
            InitializeComponent();
            this.BindingContext = resumeCompViewmodel;
            //BaseEntry = baseEntry;
            //this.BindingContext = this;
            //InitializeViewModel();
        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
       
        //private async void BtnApply_Clicked(object sender, EventArgs e)
        //{
        //    //try
        //    //{
        //    //    await SaveRecord();
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
        //    //}
        //}
   


    }
}