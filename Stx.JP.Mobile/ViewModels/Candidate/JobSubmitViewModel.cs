using Stx.JP.Mobile;
using Stx.JP.Mobile.Controls;
using Stx.JP.Mobile.ViewModels;
using System;
using Xamarin.Forms.Internals;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Model = Stx.Shared.Models.HRM.HrJobSendout;
using Stx.Shared.Interfaces.HRM;
using System.Threading.Tasks;
using Stx.JP.Mobile.Cache;
using Stx.JP.Mobile.Models.Candidate;
using Stx.JP.Mobile.Views.Common;
using Stx.Shared.Models.HRM;

namespace Stx.JP.Mobile.ViewModels.Candidate
{
    /// <summary>
    /// ViewModel for Article Job Search page 
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class JobSubmitViewModel : BaseViewModel
    {
        #region Fields

        private HrJobOrderPreviewDTO JobOrder;
        private IJobSendoutDataService IBaseDataSvc;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="Job SearchsViewModel" /> class.
        /// </summary>
        public JobSubmitViewModel(HrJobOrderPreviewDTO jobOrder)
        {
            JobOrder = jobOrder;
            InitializeViewModel();
        }

        #endregion

        #region Public Properties  

        private string _JobTitle;
        public string JobTitle
        {
            get { return this._JobTitle; }
            set
            {
                this._JobTitle = value;
                NotifyPropertyChanged();
            }
        }

        private CandidateInfo _CandidateInfo = new CandidateInfo();
        public CandidateInfo CandidateInfo
        {
            get { return this._CandidateInfo; }

            set
            {
                if (this._CandidateInfo == value)
                {
                    return;
                }

                this._CandidateInfo = value;
                this.NotifyPropertyChanged();
            }
        }
        #endregion

        #region Command

        /// <summary>    
        /// Gets or sets the command that will be executed when apply button clicked.
        /// </summary>
        public Command ApplyButtonCommand { get; set; }

        #endregion

        #region Methods


        private async void InitializeViewModel()
        {
            JobTitle = JobOrder.Title;
            await CandidateCache.Refresh();
            CandidateInfo = CandidateCache.CandidateSummary;
            //IBaseDataSvc = DependencyService.Get<IJobOrderPreviewDataService>();
            this.ApplyButtonCommand = new Command(this.ApplyButtonClicked);
        }
           

        /// <summary>
        /// Invoked when the apply button is clicked.
        /// </summary>
        private async void ApplyButtonClicked(object obj)
        {
            try
            {
                IBaseDataSvc = DependencyService.Get<IJobSendoutDataService>();

                Model jso = new Model();
                jso.ID = 0;
                jso.CandidateID = CandidateCache.CandidateID;
                jso.JobOrderID = JobOrder.JobOrderID;
                jso.Sender = "M";
                var issuccess = await IBaseDataSvc.Submit(jso);
                if(!issuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "The application has not been submitted. Please try again later.", "Ok");
                    return;
                }

                //Get Sent-Ack Page
                var page = new AcknowledgementPage(AcknowledgementPage.AckType.JobSent, JobOrder.JobOrderID, JobOrder.Title);
                await Application.Current.MainPage.Navigation.PushAsync(page);

                bool isCurrentPageClosed = false;
                try
                {
                    int pagesToRemove = 2;
                    for (var i = 1; i <= pagesToRemove; i++)
                    {
                        Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[Application.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                        isCurrentPageClosed = true;
                    }
                    //await Application.Current.MainPage.Navigation.PopAsync();
                }
                catch
                {
                    //await Application.Current.MainPage.Navigation.PopAsync(true);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        #endregion
    }
}