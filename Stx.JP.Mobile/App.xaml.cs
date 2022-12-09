using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Stx.DataServices.Hrm;
using Stx.Shared.Interfaces.HRM;
using Stx.Shared.Interfaces;
using Stx.Shared.Services;
using Stx.Shared.Interfaces.CRM;
using Stx.JP.Mobile.DataService;

namespace Stx.JP.Mobile
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        //internal static string baseApiUrl {get;}= "http://jp-api.azurewebsites.net/v1.0/";
        //internal static string baseApiUrl {get;}= "http://localhost:6001/v1.0/";
        internal static string baseApiUrl {get;}= "http://192.168.10.115:6002/v1.0/";
        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzkwNDEzQDMxMzgyZTM0MmUzMFAyT0dZeW5YY2k0SFF5Z2thWVM4dU1ZOUpzcWQ4YVJjVTFTMHpEMVVsWlk9");
            
            InitializeComponent();
            
            //var hclient = new System.Net.Http.HttpClient() { BaseAddress = apiUrl };
            //ApiHelperDataService.Init(hclient);

            //DependencyService.Register<IApiHelperDataService, ApiHelperDataService>();
            DependencyService.Register<IJobOrderPreviewDataService, MJobOrderPreviewDataService>();
            DependencyService.Register<IJobSearchDataService, MJobSearchDataService>();
            DependencyService.Register<IJobSendoutDataService, MJobSendoutDataService>();
            DependencyService.Register<ICandidateDataService, MCandidateDataService>();
            DependencyService.Register<ICandidateProfileDataService, MCandidateProfileDataService>();
            //DependencyService.Register<ICorporatePublicDataService, CorporatePublicDataService>();

            MainPage = new NavigationPage(new Stx.JP.Mobile.MainPage());
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
