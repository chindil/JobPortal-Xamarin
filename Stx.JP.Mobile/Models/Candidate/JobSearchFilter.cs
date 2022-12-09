using Stx.JP.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Stx.Shared;
using Stx.Shared.Extensions.Common;

namespace Stx.JP.Mobile.Models.Candidate
{
    public class JobSearchFilter: BaseViewModel
    {
		private string _Keyword;
		private string _Location;
		private List<short> _EmploymentTypes;
		private string _Industry;
		private string _Specialty;
		private decimal _SalaryFrom;
		private decimal _SalaryTo;

		public JobSearchFilter DeepCopy()
		{
			JobSearchFilter copy = new JobSearchFilter();
			copy.Keyword = this.Keyword;
			copy.Location = this.Location;
			copy.Industry = this.Industry;
			copy.Specialty = this.Specialty;
			copy.SalaryFrom = this.SalaryFrom;
			copy.SalaryTo = this.SalaryTo;
			return copy;
		}

		private bool _IsFilterApplied;
		public bool IsFilterApplied
		{
			get
			{
				//CheckIsFilterApplied(); 
				return _IsFilterApplied;
			}
			set
			{
				//this._IsFilterApplied = value;		
				NotifyPropertyChanged();						
			}
		}

		public void CheckIsFilterApplied()
		{
			try
			{
				if (string.Empty.AllIsNullOrWhiteSpace(_Keyword, _Location, _Industry, _Specialty) && _SalaryFrom <= 0 && _SalaryTo <= 0 && EmploymentTypes.Count == 0)
					_IsFilterApplied = false;
				else
					_IsFilterApplied = true;
			}
			catch
			{
				_IsFilterApplied = false;
			}

			NotifyPropertyChanged("IsFilterApplied");
		}

		public string Keyword
		{
			get { return this._Keyword; }
			set
			{
				this._Keyword = value;
				CheckIsFilterApplied();
				NotifyPropertyChanged();
			}
		}
		public string Location
		{
			get { return this._Location; }
			set
			{
				this._Location = value;
				CheckIsFilterApplied();
				NotifyPropertyChanged();
			}
		}	
		public List<short> EmploymentTypes
		{
			get { return this._EmploymentTypes; }
			set
			{
				this._EmploymentTypes = value;
				CheckIsFilterApplied();
				NotifyPropertyChanged();
			}
		}
		public string Industry
		{
			get { return this._Industry; }
			set
			{
				this._Industry = value;
				CheckIsFilterApplied();
				NotifyPropertyChanged();
			}
		}
		public string Specialty
		{
			get { return this._Specialty; }
			set
			{
				this._Specialty = value;
				CheckIsFilterApplied();
				NotifyPropertyChanged();
			}
		}
		public decimal SalaryFrom
		{
			get { return this._SalaryFrom; }
			set
			{
				this._SalaryFrom = value;
				CheckIsFilterApplied();
				NotifyPropertyChanged();
			}
		}
		public decimal SalaryTo
		{
			get { return this._SalaryTo; }
			set
			{
				this._SalaryTo = value;
				CheckIsFilterApplied();
				NotifyPropertyChanged();
			}
		}

	}
}
