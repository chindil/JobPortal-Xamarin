using Stx.JP.Mobile;
using Stx.JP.Mobile.Controls;
using Stx.JP.Mobile.ViewModels;
using System;
using Xamarin.Forms.Internals;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Model = Stx.Shared.Models.HRM.HrCandidateJobStatDTO;
using Stx.Shared.Interfaces.HRM;
using System.Threading.Tasks;
using System.Collections.Generic;
using Stx.JP.Mobile.Models.Candidate;

namespace Stx.JP.Mobile.ViewModels.Candidate
{
    /// <summary>
    /// ViewModel for Article Job Search page 
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class MySavedDataViewModel : BaseViewModel
    {
        #region Fields

        private ICandidateDataService IBaseDataSvc;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="Job MySavedDataViewModel" /> class.
        /// </summary>
        public MySavedDataViewModel()
        {
            InitializeViewModel();
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that will be executed when the bookmark button is clicked.
        /// </summary>
        public Command BookmarkCommand { get; set; }
        /// <summary>
        /// Gets or sets the command that will be executed when an item is selected.
        /// </summary>
        public Command ListItemSelectedCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when pull to refresh occurred.
        /// </summary>
        public Command RefreshListCommand { get; set; }


        #endregion

        #region Public Properties        
        private ObservableCollection<Model> _SavedJobOrders;
        private ObservableCollection<Model> _AppliedJobOrders;
        private bool _IsRefreshing;

        /// <summary>
        /// Gets or sets the property that has been bound with the list view, which displays the saved jobs.
        /// </summary>
        public ObservableCollection<Model> SavedJobOrders
        {
            get { return this._SavedJobOrders; }

            set
            {
                if (this._SavedJobOrders == value)
                {
                    return;
                }

                this._SavedJobOrders = value;
                this.NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets the property that has been bound with the list view, which displays the applied jobs.
        /// </summary>
        public ObservableCollection<Model> AppliedJobOrders
        {
            get { return this._AppliedJobOrders; }

            set
            {
                if (this._AppliedJobOrders == value)
                {
                    return;
                }

                this._AppliedJobOrders = value;
                this.NotifyPropertyChanged();
            }
        }

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

        #endregion

        #region Methods


        private async void InitializeViewModel()
        {
            try
            {
                this.BookmarkCommand = new Command(this.BookmarkButtonClicked);
                this.ListItemSelectedCommand = new Command(this.ListItemSelected);
                this.RefreshListCommand = new Command(this.RefreshListOccurred);

                await SearchJobs();
            }
            catch (Exception ex)
            {
                var dd = ex.Message;
            }
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
                SavedJobOrders = new ObservableCollection<Model>();
                AppliedJobOrders = new ObservableCollection<Model>();
                
                IBaseDataSvc = DependencyService.Get<ICandidateDataService>();
                var list = await IBaseDataSvc.GetSavedJobs(100);
                SavedJobOrders = new ObservableCollection<Model>(list);
                var listApplied = await IBaseDataSvc.GetAppliedJobs(100);
                AppliedJobOrders = new ObservableCollection<Model>(listApplied);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
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
                    if (article.IsBookmarked ?? false && this.SavedJobOrders.Count > 0)
                    {
                        var isSaved = await IBaseDataSvc.UpdateBookmark(100, article.JobOrderID, (article.IsBookmarked ?? false ? "N" : "Y"));
                        if (isSaved)
                        {
                            article.IsBookmarked = !article.IsBookmarked;
                            this.SavedJobOrders.Remove(article);
                        }
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