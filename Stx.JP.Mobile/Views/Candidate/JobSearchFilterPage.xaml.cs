using Stx.JP.Mobile.Models.Candidate;
using Stx.JP.Mobile.ViewModels.Candidate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Model = Stx.JP.Mobile.Models.Candidate.JobSearchFilter;


namespace Stx.JP.Mobile.Views.Candidate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobSearchFilterPage : ContentPage
    {

        public JobSearchFilterPage(JobSearchFilterViewModel jobSearchFilter)
        {
            InitializeComponent();
            this.BindingContext = jobSearchFilter;
        }

        private void BtnCancel_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        //protected override void OnSizeAllocated(double width, double height)
        //{
        //    base.OnSizeAllocated(width, height);

        //    if (width > 0 && pageWidth != width)
        //    {
        //        var size = Application.Current.MainPage.Width / listView.ItemSize;
        //        gridLayout.SpanCount = (int)size;
        //        listView.LayoutManager = gridLayout;
        //    }
        //}
    }
}