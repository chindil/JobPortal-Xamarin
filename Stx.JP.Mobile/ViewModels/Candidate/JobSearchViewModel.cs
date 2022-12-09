using Stx.JP.Mobile;
using Stx.JP.Mobile.Controls;
using Stx.JP.Mobile.ViewModels;
using System;
using Xamarin.Forms.Internals;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Model = Stx.Shared.Models.HRM.HrJobOrderSearch;
using Stx.Shared.Interfaces.HRM;
using System.Threading.Tasks;
using System.Collections.Generic;
using Stx.JP.Mobile.Models.Candidate;
using Stx.Shared.Models.Parm;

namespace Stx.JP.Mobile.ViewModels.Candidate
{
    /// <summary>
    /// ViewModel for Article Job Search page 
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class JobSearchViewModel : BaseViewModel
    {
        #region Fields

        private IJobSearchDataService IBaseDataSvc;
        private ICandidateDataService ICandidateDataSvc;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="Job SearchsViewModel" /> class.
        /// </summary>
        public JobSearchViewModel()
        {
            InitializeViewModel();
        }

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();

        //    await InitializeViewModel();
        //}

        //public static async Task<JobSearchViewModel> Create()
        //{
        //    //var mClass = new JobSearchViewModel();
        //    //await mClass.InitializeViewModel();
        //    //return mClass;
        //}

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that will be executed when the bookmark button is clicked.
        /// </summary>
        public Command BookmarkCommand { get; set; }
        /// <summary>
        /// Gets or sets the command that will be executed when the bookmark button is clicked.
        /// </summary>
        public Command ShareButtonCommand { get; set; }
        /// <summary>
        /// Gets or sets the command that will be executed when an item is selected.
        /// </summary>
        public Command ListItemSelectedCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when filter button clicked.
        /// </summary>
        public Command FilterButtonCommand { get; set; }
        /// <summary>
        /// Gets or sets the command that will be executed when pull to refresh occurred.
        /// </summary>
        public Command RefreshListCommand { get; set; }


        #endregion

        #region Public Properties        
        private ObservableCollection<Model> _JobOrders;

        /// <summary>
        /// Gets or sets the property that has been bound with the list view, which displays the articles' latest stories items.
        /// </summary>
        public ObservableCollection<Model> JobOrders
        {
            get { return this._JobOrders; }

            set
            {
                if (this._JobOrders == value)
                {
                    return;
                }

                this._JobOrders = value;
                this.NotifyPropertyChanged();
            }
        }

        private JobSearchFilter _JobFilters = new JobSearchFilter();
        public JobSearchFilter JobFilters
        {
            get { return this._JobFilters; }

            set
            {
                if (this._JobFilters == value)
                {
                    return;
                }

                this._JobFilters = value;
                this.NotifyPropertyChanged();
            }
        }    

        private bool _IsRefreshing;
        public bool IsRefreshing
        {
            get { return this._IsRefreshing; }

            set
            {
                if (this._IsRefreshing == value)
                {
                    return;
                }

                this._IsRefreshing = value;
                this.NotifyPropertyChanged();
            }
        }

        //private bool _IsFilterApplied=false;
        //public bool IsFilterApplied
        //{
        //    get { return this._IsFilterApplied; }

        //    set
        //    {
        //        if (this._IsFilterApplied == value)
        //        {
        //            return;
        //        }

        //        this._IsFilterApplied = value;
        //        this.NotifyPropertyChanged();
        //    }
        //}
        #endregion

        #region Methods


        private async void InitializeViewModel()
        {
            try
            {
                IBaseDataSvc = DependencyService.Get<IJobSearchDataService>();
                ICandidateDataSvc = DependencyService.Get<ICandidateDataService>();

                this.FilterButtonCommand = new Command(this.FilterButtonClicked);
                this.BookmarkCommand = new Command(this.BookmarkButtonClicked);
                this.ShareButtonCommand = new Command(this.ShareButtonClicked);
                this.ListItemSelectedCommand = new Command(this.ListItemSelected);
                this.RefreshListCommand = new Command(this.RefreshListOccurred);

                await SearchJobs();
            }
            catch (Exception ex) 
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private void ShareButtonClicked(object obj)
        {
            
        }

        //pullToRefresh.Refreshing += PullToRefresh_Refreshing;
        private async void RefreshListOccurred(object sender)
        {
            try
            {
                IsRefreshing = true;
                await Task.Delay(1000);

                await SearchJobs();
            }
            catch { }
            IsRefreshing = false;
        }

        private async Task SearchJobs()
        {
            try
            {
                JobOrders = new ObservableCollection<Model>();
                HrJobSearchParmDTO parm = new HrJobSearchParmDTO()
                {
                    CandidateID = Cache.CandidateCache.CandidateID,
                    Keyword = JobFilters.Keyword,
                    JobIndustries = new List<string> { JobFilters.Industry },
                    Location = JobFilters.Location,
                    EmploymentTypes = JobFilters.EmploymentTypes,
                    SalaryFrom = JobFilters.SalaryFrom,
                    SalaryTo = JobFilters.SalaryTo,
                    Specialty = JobFilters.Specialty
                };
                var list = await IBaseDataSvc.Search(parm);
                //var list = await IBaseDataSvc.Search(JobFilters.Keyword, JobFilters.Location, JobFilters.Industry, 100);
                JobOrders = new ObservableCollection<Model>(list);
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
        private async void FilterButtonClicked(object obj)
        {
            try
            {
                var vm = new JobSearchFilterViewModel(JobFilters.DeepCopy());
                vm.FilterSelectionSucceeded += FilterSelectionSucceeded;
                var secondPage = new Views.Candidate.JobSearchFilterPage(vm);
                await Application.Current.MainPage.Navigation.PushAsync(secondPage);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private async void FilterSelectionSucceeded(object sender, EventArgs e)
        {
            if (sender is JobSearchFilter jsf && jsf != null) // && jsf != JobFilters)
            {
                JobFilters = jsf;
                JobFilters.CheckIsFilterApplied();
                await SearchJobs();
            }
        }


        /// <summary>
        /// Invoked when the bookmark button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private async void BookmarkButtonClicked(object obj)
        {            
            if (obj is Model article)
            {
                try
                {
                    var isSaved= await ICandidateDataSvc.UpdateBookmark(100, article.JobOrderID, (article.IsBookmarked ?? false ? "N" : "Y"));
                    if (isSaved)
                    {
                        article.IsBookmarked = !article.IsBookmarked; 
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        private async void ListItemSelected(object obj)
        {
            try
            {
                if (obj is Syncfusion.ListView.XForms.ItemTappedEventArgs && ((Syncfusion.ListView.XForms.ItemTappedEventArgs)obj).ItemData is Model model)
                {
                    var vm = new JobDetailViewModel(model.JobOrderID);
                    var page = new Views.Candidate.JobDetailPage(vm);
                    await Application.Current.MainPage.Navigation.PushAsync(page);
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