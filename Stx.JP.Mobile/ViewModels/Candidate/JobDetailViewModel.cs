using Stx.JP.Mobile;
using Stx.JP.Mobile.Controls;
using Stx.JP.Mobile.ViewModels;
using System;
using Xamarin.Forms.Internals;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Model = Stx.Shared.Models.HRM.HrJobOrderPreviewDTO;
using Stx.Shared.Interfaces.HRM;
using System.Threading.Tasks;

namespace Stx.JP.Mobile.ViewModels.Candidate
{
    /// <summary>
    /// ViewModel for Article Job Search page 
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class JobDetailViewModel : BaseViewModel
    {
        #region Fields

        private int JobOrderID;
        private Model _JobOrder;
        private string _BookmarkButtonText = "Save";
        private IJobOrderPreviewDataService IBaseDataSvc;
        private ICandidateDataService ICandidateDataSvc;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="Job SearchsViewModel" /> class.
        /// </summary>
        public JobDetailViewModel(int jobOrderId)
        {
            JobOrderID = jobOrderId;
            InitializeViewModel();
        }

        #endregion

        #region Public Properties        

        /// <summary>
        /// Gets or sets the property that has been bound with the list view, which displays the articles' latest stories items.
        /// </summary>
        public Model JobOrder
        {
            get { return this._JobOrder; }

            set
            {
                if (this._JobOrder == value)
                {
                    return;
                }

                this._JobOrder = value;
                this.NotifyPropertyChanged();
            }
        }    
        
        public string BookmarkButtonText
        {
            get { return this._BookmarkButtonText; }

            set
            {
                if (this._BookmarkButtonText == value)
                {
                    return;
                }

                this._BookmarkButtonText = value;
                this.NotifyPropertyChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that will be executed when the bookmark button is clicked.
        /// </summary>
        public Command SaveButtonCommand { get; set; }
        /// <summary>
        /// Gets or sets the command that will be executed when quick apply button clicked.
        /// </summary>
        public Command QuickApplyButtonCommand { get; set; }
        /// <summary>    
        /// Gets or sets the command that will be executed when apply button clicked.
        /// </summary>
        public Command ApplyButtonCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when report job button clicked.
        /// </summary>
        public Command ReportJobButtonCommand { get; set; }


        #endregion

        #region Methods


        private async void InitializeViewModel()
        {
            IBaseDataSvc = DependencyService.Get<IJobOrderPreviewDataService>();

            this.SaveButtonCommand = new Command(this.BookmarkButtonClicked);
            this.QuickApplyButtonCommand = new Command(this.QuickApplyButtonClicked);
            this.ApplyButtonCommand = new Command(this.ApplyButtonClicked);
            this.ReportJobButtonCommand = new Command(this.ReportJobButtonClicked);

            await GetJobDetails();
        }

        private async Task GetJobDetails()
        {
            try
            {
                JobOrder = await IBaseDataSvc.GetRecordByID(JobOrderID, 100);
                UpdateBookmarkButtonStatus();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        /// <summary>
        /// Invoked when the filter button is clicked.
        /// </summary>
        /// <param name="obj"></param>
        private async void BookmarkButtonClicked(object obj)
        {
            try
            {
                if (JobOrder != null)
                {
                    ICandidateDataSvc = DependencyService.Get<ICandidateDataService>();
                    var isSaved = await ICandidateDataSvc.UpdateBookmark(100, JobOrder.JobOrderID, (JobOrder.IsBookmarked ?? false ? "N" : "Y"));
                    if (isSaved)
                    {
                        JobOrder.IsBookmarked = !JobOrder.IsBookmarked;
                        UpdateBookmarkButtonStatus();
                    }
                }
            }
            catch { }
        }

        private void UpdateBookmarkButtonStatus()
        {
            if (JobOrder.IsBookmarked??false)
            {
                BookmarkButtonText = "Unsave";
            }
            else
            {
                BookmarkButtonText = "Save";
            }
        }

        /// <summary>
        /// Invoked when the QuickApplyButton button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void QuickApplyButtonClicked(object obj)
        {
            if (!(obj is Model article)) return;            
        }

        /// <summary>
        /// Invoked when the Apply/Submit button is clicked.
        /// </summary>
        private async void ApplyButtonClicked(object obj)
        {
            try
            {
                if (JobOrder != null && JobOrder.JobOrderID > 0)
                {
                    var vm = new JobSubmitViewModel(JobOrder);
                    var page = new Views.Candidate.JobSubmitPage(vm);
                    await Application.Current.MainPage.Navigation.PushAsync(page);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");

    }
}

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        private async void ReportJobButtonClicked(object obj)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
            }
        }


        #endregion
    }
}