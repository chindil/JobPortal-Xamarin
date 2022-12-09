using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Model = Stx.Shared.Models.HRM.HrCandidate;
//using ModelExpr = Stx.Shared.Models.HRM.HrCandidateExperience;
//using ModelEduc = Stx.Shared.Models.HRM.HrCandidateEducation;
//using ModelCert = Stx.Shared.Models.HRM.HrCandidateCertificate;
//using ModelSkill = Stx.Shared.Models.HRM.HrCandidateSkill;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Stx.Shared.Interfaces.HRM;
using Stx.Shared.Models.HRM;

namespace Stx.JP.Mobile.ViewModels.Profile
{
    [Preserve(AllMembers = true)]
    class ResumeViewModel : BaseViewModel
    {
        private Model _BaseEntry;
        private int CurrentCandidateID = 100;

        #region Command
        /// <summary>
        /// Gets or sets the command that will be executed when the edit button clicked.
        /// </summary>
        public Command EditSectionButtonCommand { get; set; }
        /// <summary>
        /// Gets or sets the command that will be executed when add new vutton clicked.
        /// </summary>
        public Command AddNewSectionButtonCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when filter button clicked.
        /// </summary>
        public Command FilterButtonCommand { get; set; }

        #endregion

        #region Properties
        public Model BaseEntry
        {
            get { return this._BaseEntry; }
            set
            {
                if (this._BaseEntry == value)
                {
                    return;
                }

                this._BaseEntry = value;
                this.NotifyPropertyChanged();
            }
        }
        
        #endregion

        #region Constructor

        public ResumeViewModel()
        {
            InitializeViewModel();
        }

        #endregion

        private async void InitializeViewModel()
        {
            try
            {
                this.EditSectionButtonCommand = new Command(this.EditSectionButtonClicked);
                this.AddNewSectionButtonCommand = new Command(this.AddNewSectionButtonClicked);
                //this.FilterButtonCommand = new Command(this.AddNewSectionButtonClicked);

                await LoadCandidateData();
            }
            catch (Exception ex)
            {
                var dd = ex.Message;
            }
        }

        private async Task LoadCandidateData()
        {
            try
            {
                BaseEntry = new Model();
                var IBaseDataSvc = DependencyService.Get<ICandidateDataService>();
                BaseEntry = await IBaseDataSvc.GetRecordByCD("USER@EXAMPLE.COM");
                //BaseEntry.Experiences
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private void AddNewSectionButtonClicked(object obj)
        {
        
            if(obj is string section)
            {
                if (section == "EXPR")
                    EditSectionButtonClicked(new HrCandidateExperience() { CandidateID = CurrentCandidateID });
                else if (section == "EDUC")
                    EditSectionButtonClicked(new HrCandidateEducation() { CandidateID = CurrentCandidateID });
                else if (section == "CERT")
                    EditSectionButtonClicked(new HrCandidateCertificate() { CandidateID = CurrentCandidateID });
            }
        }

        private async void EditSectionButtonClicked(object obj)
        {
            try
            {
                if (obj == null) return;

                if (obj is string parm && parm == "SKILL")
                {
                    var dataSk = new ObservableCollection<HrCandidateSkill>();
                    BaseEntry.Skills.ForEach(x => dataSk.Add(x.DeepCopy()));
                    var viewmodel = new ResumeCompEditViewModel(dataSk);
                    viewmodel.EditChangesSubmissionSucceeded += SectionEditChangesSubmissionSucceeded;
                    var page = new Views.Profile.SkillEditPage(viewmodel);
                    await Application.Current.MainPage.Navigation.PushAsync(page);
                }  
                else if (obj is string parm2 && parm2 == "PROF")
                {
                    var viewmodel = new ResumeCompEditViewModel(BaseEntry);
                    viewmodel.EditChangesSubmissionSucceeded += SectionEditChangesSubmissionSucceeded;
                    var page = new Views.Profile.ProfileEditPage(viewmodel);
                    await Application.Current.MainPage.Navigation.PushAsync(page);
                }
                else
                {
                    if (obj is HrCandidateExperience dataEx)
                    {
                        var viewmodel = new ResumeCompEditViewModel(dataEx.DeepCopy());
                        viewmodel.EditChangesSubmissionSucceeded += SectionEditChangesSubmissionSucceeded;
                        var page = new Views.Profile.ExperienceEditPage(viewmodel);
                        await Application.Current.MainPage.Navigation.PushAsync(page);
                    }
                    else if (obj is HrCandidateEducation dataEd)
                    {
                        var viewmodel = new ResumeCompEditViewModel(dataEd.DeepCopy());
                        viewmodel.EditChangesSubmissionSucceeded += SectionEditChangesSubmissionSucceeded;
                        var page = new Views.Profile.EducationEditPage(viewmodel);
                        await Application.Current.MainPage.Navigation.PushAsync(page);
                    }
                    else if (obj is HrCandidateCertificate dataCe)
                    {
                        var viewmodel = new ResumeCompEditViewModel(dataCe.DeepCopy());
                        viewmodel.EditChangesSubmissionSucceeded += SectionEditChangesSubmissionSucceeded;
                        var page = new Views.Profile.CertificateEditPage(viewmodel);
                        await Application.Current.MainPage.Navigation.PushAsync(page);
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }
        private async void SectionEditChangesSubmissionSucceeded(object sender, EventArgs e)
        {
            if (sender == null) return;
            if(sender is bool isSuccuss && isSuccuss)
            {
                await LoadCandidateData();
            }
            else if (sender is HrCandidateExperience || sender is HrCandidateEducation|| sender is HrCandidateCertificate || sender is ObservableCollection<HrCandidateSkill>)
            {
                await LoadCandidateData();
            }
        }

    }
}
