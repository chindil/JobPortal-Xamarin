using Stx.JP.Mobile.ViewModels.Profile;
using Syncfusion.SfRating.XForms;
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
    public partial class SkillEditPage : ContentPage
    {
        public SkillEditPage(ResumeCompEditViewModel resumeCompViewmodel)
        {
            InitializeComponent();
            this.BindingContext = resumeCompViewmodel;            
        }


        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}