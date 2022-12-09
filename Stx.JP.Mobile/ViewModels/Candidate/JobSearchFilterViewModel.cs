using Stx.JP.Mobile.Models.Candidate;
using Stx.Shared.Models.HRM;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Stx.JP.Mobile.ViewModels.Candidate
{
    public class JobSearchFilterViewModel: BaseViewModel
    {
        public event EventHandler FilterSelectionSucceeded;

        /// <summary>
        /// Gets or sets the command that will be executed when the bookmark button is clicked.
        /// </summary>
        public Command ApplyFiltersButtonCommand { get; set; }
        public Command ResetFilterButtonCommand { get; set; }
        public Command JobTypeSelectionChangedCommand { get; set; }

        public JobSearchFilterViewModel(JobSearchFilter model)
        {
            ApplyFiltersButtonCommand = new Command(this.ApplyFilterButtonClicked);
            ResetFilterButtonCommand = new Command(this.ResetFilterButtonClicked);
            JobTypeSelectionChangedCommand = new Command(this.JobTypeSelectionChanged);
            JobFilters = model;
            Stx.DataServices.Hrm.StaticData.EmploymentType.EmploymentTypes().ForEach(x=> EmploymentTypes.Add(x));
        }

        private void JobTypeSelectionChanged(object obj)
        {
            //if the event not working, use selectedItems prop.
        }

        private JobSearchFilter _JobFilters;
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


        private ObservableCollection<HrEmploymentType> _EmploymentTypes = new ObservableCollection<HrEmploymentType>();
        public ObservableCollection<HrEmploymentType> EmploymentTypes
        {
            get { return this._EmploymentTypes; }

            set
            {
                if (this._EmploymentTypes == value)
                {
                    return;
                }

                this._EmploymentTypes = value;
                this.NotifyPropertyChanged();
            }
        }

        private ObservableCollection<HrEmploymentType> _SelectedEmploymentTypes = new ObservableCollection<HrEmploymentType>();
        public ObservableCollection<HrEmploymentType> SelectedEmploymentTypes
        {
            get { return this._SelectedEmploymentTypes; }

            set
            {
                if (this._SelectedEmploymentTypes == value)
                {
                    return;
                }

                this._SelectedEmploymentTypes = value;
                this.NotifyPropertyChanged();
            }
        }

        private async void ApplyFilterButtonClicked(object sender)
        {
            JobFilters.EmploymentTypes = SelectedEmploymentTypes.Select(x => x.ID).ToList() ?? new List<short>();
            await Application.Current.MainPage.Navigation.PopAsync();
            FilterSelectionSucceeded?.Invoke(JobFilters, EventArgs.Empty);
        }
        private void ResetFilterButtonClicked(object sender)
        {
            JobFilters = new JobSearchFilter();
            EmploymentTypes = new ObservableCollection<HrEmploymentType>(EmploymentTypes);
            SelectedEmploymentTypes.Clear();
        }



    }
}
