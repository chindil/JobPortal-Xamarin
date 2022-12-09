using Stx.Shared;
using Stx.Shared.Interfaces.HRM;
using Stx.Shared.Models;
using Stx.Shared.Models.HRM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Stx.JP.Mobile.ViewModels.Profile
{
    public class ResumeCompEditViewModel: BaseViewModel
    {
        public event EventHandler EditChangesSubmissionSucceeded;

        //public HrCandidateExperience BaseEntry { get; set; } = new HrCandidateExperience();
        ICandidateDataService IBaseDataSvc;
        private object _BaseEntry = new object();
        private ObservableCollection<CountryMin> _Countries = new ObservableCollection<CountryMin>();
        private ObservableCollection<HrCandidateSkill> _Skills;
        private ObservableCollection<HrCandidateLanguage> _Langs;

        private string DocType = "";
        private int CandidateID = 0;

        public object BaseEntry
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
        private string _PageHeader = "";
        public string PageHeader
        {
            get { return this._PageHeader; }

            set
            {
                if (this._PageHeader == value)
                {
                    return;
                }

                this._PageHeader = value;
                this.NotifyPropertyChanged();
            }
        }
        public ObservableCollection<CountryMin> Countries
        {
            get { return this._Countries; }

            set
            {
                if (this._Countries == value)
                {
                    return;
                }

                this._Countries = value;
                this.NotifyPropertyChanged();
            }
        } 
        public ObservableCollection<HrCandidateSkill> Skills
        {
            get { return this._Skills; }

            set
            {
                if (this._Skills == value)
                {
                    return;
                }

                this._Skills = value;
                this.NotifyPropertyChanged();
            }
        }

        public ObservableCollection<HrCandidateLanguage> Langs
        {
            get { return this._Langs; }

            set
            {
                if (this._Langs == value)
                {
                    return;
                }

                this._Langs = value;
                this.NotifyPropertyChanged();
            }
        }


        public Command ApplyButtonCommand { get; set; }
        public Command AddNewRecordButtonCommand { get; set; }
        public Command RemoveRecordButtonCommand { get; set; }


        public ResumeCompEditViewModel(object baseEntry)
        {
            BaseEntry = baseEntry;
            IBaseDataSvc = DependencyService.Get<ICandidateDataService>();
            InitializeViewModel();
        }

        private async void InitializeViewModel()
        {
            if (BaseEntry is int candidateId && candidateId > 0) // Edit Basic Profile
            {
                DocType = "PROF";
                PageHeader = "";
                BaseEntry = await IBaseDataSvc.GetRecordByID(candidateId);
            }
            else if (BaseEntry is ObservableCollection<HrCandidateSkill> skills)
            {
                CandidateID = skills.FirstOrDefault().CandidateID;
                DocType = "SKIL";
                Skills = new ObservableCollection<HrCandidateSkill>(skills);
                BaseEntry = null;
                PageHeader = "Update Skills";
            }
            else if (BaseEntry is HrCandidateCertificate baseentryCert)
            {
                DocType = "CERT";
                PageHeader = $"{(baseentryCert.ID > 0 ? "Update" : "Add")} Certification";
            }
            else if (BaseEntry is HrCandidateEducation baseentryEdu)
            {
                DocType = "EDUC";
                PageHeader = $"{(baseentryEdu.ID > 0 ? "Update" : "Add")} Education";
            }
            else if (BaseEntry is HrCandidateExperience baseentryExp)
            {
                DocType = "EXPE";
                PageHeader = $"{(baseentryExp.ID > 0 ? "Update" : "Add")} Experience";
            }

            ApplyButtonCommand = new Command(this.ApplyButtonClicked);
            AddNewRecordButtonCommand = new Command(this.AddNewRecordButtonClicked);
            RemoveRecordButtonCommand = new Command(this.RemoveRecordButtonClicked);
            Countries = new ObservableCollection<CountryMin>(Stx.Shared.Services.StaticDataService.Country.Countries());
        }

        private void AddNewRecordButtonClicked(object obj)
        {
            try
            {
                if (DocType == "SKIL")
                {
                    Skills.Add(new HrCandidateSkill() { ID = 0, CandidateID = CandidateID, Proficiency = 1, SkillName ="" } );
                }
            }
            catch { }
        }

        private async void RemoveRecordButtonClicked(object obj)
        {
            try
            {
                if (DocType == "SKIL" && obj is HrCandidateSkill candidateSkill)
                {
                    Skills.Remove(candidateSkill);
                }
                else 
                {
                    var dr = await Application.Current.MainPage.DisplayAlert("Delete?", "Do you want to delete this record?", "Yes", "No");
                    if (!dr) return;

                    if (BaseEntry is HrCandidateExperience baseentryExp)
                    {
                        var reslt = await IBaseDataSvc.DeleteResumeComponent(DocType, baseentryExp.CandidateID, baseentryExp.ID);
                        await Application.Current.MainPage.DisplayAlert("Delete", "The record has been deleted.", "Ok");
                        await Application.Current.MainPage.Navigation.PopAsync();
                        EditChangesSubmissionSucceeded?.Invoke(reslt, EventArgs.Empty);
                    }
                    else if (BaseEntry is HrCandidateEducation baseentryEdu)
                    {
                        var reslt = await IBaseDataSvc.DeleteResumeComponent(DocType, baseentryEdu.CandidateID, baseentryEdu.ID);
                        await Application.Current.MainPage.DisplayAlert("Delete", "The record has been deleted.", "Ok");
                        await Application.Current.MainPage.Navigation.PopAsync();
                        EditChangesSubmissionSucceeded?.Invoke(reslt, EventArgs.Empty);
                    }
                    else if (BaseEntry is HrCandidateCertificate baseentryCert)
                    {
                        var reslt = await IBaseDataSvc.DeleteResumeComponent(DocType, baseentryCert.CandidateID, baseentryCert.ID);
                        await Application.Current.MainPage.DisplayAlert("Delete", "The record has been deleted.", "Ok");
                        await Application.Current.MainPage.Navigation.PopAsync();
                        EditChangesSubmissionSucceeded?.Invoke(reslt, EventArgs.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private async void ApplyButtonClicked(object obj)
        {
            try
            {
                if(BaseEntry is HrCandidateExperience baseentryExp)
                {
                    var reslt = await IBaseDataSvc.UpdateExperiences(baseentryExp);
                    await Application.Current.MainPage.Navigation.PopAsync();      
                    EditChangesSubmissionSucceeded?.Invoke(reslt, EventArgs.Empty);
                }
                else if (BaseEntry is HrCandidateEducation baseentryEdu)
                {
                    var reslt = await IBaseDataSvc.UpdateEducations(baseentryEdu);
                    await Application.Current.MainPage.Navigation.PopAsync();
                    EditChangesSubmissionSucceeded?.Invoke(reslt, EventArgs.Empty);
                }
                else if (BaseEntry is HrCandidateCertificate baseentryCert)
                {
                    var reslt = await IBaseDataSvc.UpdateCertificates(baseentryCert);
                    await Application.Current.MainPage.Navigation.PopAsync();
                    EditChangesSubmissionSucceeded?.Invoke(reslt, EventArgs.Empty);
                }  
                else if (DocType == "PROF")
                {
                    //var reslt = await IBaseDataSvc.UpdateRecord(BaseEntry);
                    await Application.Current.MainPage.Navigation.PopAsync();
                    EditChangesSubmissionSucceeded?.Invoke(true, EventArgs.Empty);
                }
                else if (DocType == "SKIL")
                {
                    var reslt = await IBaseDataSvc.UpdateSkills(Skills.ToList());
                    await Application.Current.MainPage.Navigation.PopAsync();
                    EditChangesSubmissionSucceeded?.Invoke(true, EventArgs.Empty);
                }
                else if (DocType == "LANG")
                {
                    var reslt = await IBaseDataSvc.UpdateLanguages(Langs.ToList());
                    await Application.Current.MainPage.Navigation.PopAsync();
                    EditChangesSubmissionSucceeded?.Invoke(true, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }
    }
}
