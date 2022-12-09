using Stx.JP.Mobile.Cache;
using Stx.Shared.Interfaces.HRM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Stx.JP.Mobile.ViewModels.Profile
{
    [Preserve(AllMembers = true)]
    class ProfileMenuViewModel : BaseViewModel
    {

        #region Command
        /// <summary>
        /// Gets or sets the command that will be executed when a menu clicked.
        /// </summary>
        public Command SettingsMenuClickCommand { get; set; }
        /// <summary>
        /// Gets or sets the command that will be executed when edit profile button clicked.
        /// </summary>
        public Command EditProfileButtonClickCommand { get; set; }
        /// <summary>
        /// Gets or sets the command that will be executed when update menu item is selected.
        /// </summary>
        public Command UpdateResumeMenuCommand { get; set; }

        #endregion

        #region Properties
        private string _ProfileName;
        private string _CurrentJobTitle;
        private string _City;
        private string _CountryName;

        public string ProfileName
        {
            get { return this._ProfileName; }
            set
            {
                this._ProfileName = value;
                NotifyPropertyChanged();
            }
        }
        public string CurrentJobTitle
        {
            get { return this._CurrentJobTitle; }
            set
            {
                this._CurrentJobTitle = value;
                NotifyPropertyChanged();
            }
        }
        public string CountryName
        {
            get { return this._CountryName; }
            set
            {
                this._CountryName = value;
                NotifyPropertyChanged();
            }
        }
        public string City
        {
            get { return this._City; }
            set
            {
                this._City = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Constructor

        public ProfileMenuViewModel()
        {
            InitializeViewModel();
        }
        #endregion

        #region Methods
        private async void InitializeViewModel()
        { 
            this.SettingsMenuClickCommand = new Command(this.SettingsMenuClicked);
            this.UpdateResumeMenuCommand = new Command(this.UpdateResumeMenuClicked);
            this.EditProfileButtonClickCommand = new Command(this.EditProfileButtonClicked);
            await GetProfileInfo();
        }

        private async Task GetProfileInfo()
        {
            try
            {
                var IBaseDataSvc = DependencyService.Get<ICandidateProfileDataService>();
                var candt = await IBaseDataSvc.GetRecordByID(CandidateCache.CandidateID);
                ProfileName = $"{candt.FirstName} {candt.LastName}";
                CurrentJobTitle = candt.CurrentJobTitle;
                CountryName = candt.CountryName;
                City = candt.City;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }


        private async void EditProfileButtonClicked(object obj)
        {
            try
            {
                var viewmodel = new ResumeCompEditViewModel(CandidateCache.CandidateID);
                viewmodel.EditChangesSubmissionSucceeded += SectionEditChangesSubmissionSucceeded;
                var page = new Views.Profile.ProfileEditPage(viewmodel);
                await Application.Current.MainPage.Navigation.PushAsync(page);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private async void SectionEditChangesSubmissionSucceeded(object sender, EventArgs e)
        {
            if (sender == null) return;
            if (sender is bool isSuccuss && isSuccuss)
            {
                await GetProfileInfo();
            }
        }

        /// <summary>
        /// Invoked when the text size option is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private async void SettingsMenuClicked(object obj)
        {
            if(obj is string menu)
            await OpenMenuPage(menu);
        }
        private async Task OpenMenuPage(string menu)
        {
            try
            {
                if (menu == "NOTIF")
                {
                    var page = new Views.Candidate.MyNotificationPage();
                    await Application.Current.MainPage.Navigation.PushAsync(page);
                }
                else if (menu == "ACTIV")
                {
                }
                else if (menu == "PREFE")
                {
                    var page = new Views.Settings.PreferencePage();
                    await Application.Current.MainPage.Navigation.PushAsync(page);
                }
                else if (menu == "HELP")
                {
                    var page = new Views.Settings.SupportPage();
                    await Application.Current.MainPage.Navigation.PushAsync(page);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        /// <summary>
        /// Invoked when the update resume menu clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private async void UpdateResumeMenuClicked(object obj)
        {
            var page = new Views.Profile.ResumePage();
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion


    }
}
