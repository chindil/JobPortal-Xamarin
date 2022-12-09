using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FilterModel = Stx.Shared.Models.HRM.HrJobOrderPreviewDTO;


namespace Stx.JP.Mobile.Views.Candidate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobSearchPage : ContentPage
    {
        public JobSearchPage()
        {
            InitializeComponent();
        }

        //private async void SearchButton_Clicked(object sender, EventArgs e)
        //{
        //    //FilterModel cont = new FilterModel() { Title = "Name1" };
        //    //var secondPage = new Views.Candidate.JobSearchFilterPage();
        //    //secondPage.FilterSelectionSucceeded += FilterSelectionSucceeded;
        //    //secondPage.BindingContext = cont;
        //    //await Navigation.PushAsync(secondPage);
        //    //if (cont != null)
        //    //{
        //    //}
        //}

        //private void FilterSelectionSucceeded(object sender, EventArgs e)
        //{
        //    if (sender is FilterModel && (sender as FilterModel) != null)
        //    {
        //        var dd = (sender as FilterModel).Title;
        //    }
        //}

        private void ListViewList_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {

        }

        private void LV1_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {

        }
    }
}