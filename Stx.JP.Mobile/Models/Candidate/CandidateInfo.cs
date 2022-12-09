using Stx.JP.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stx.JP.Mobile.Models.Candidate
{
	public class CandidateInfo : BaseViewModel
	{
		private string _FirstName;
		private string _LastName;
		private string _CurrentJobTitle;
		private string _Email;
		private string _Mobile;
		private string _LatestResumes;
		private string _CoverLetter1;
		private string _CoverLetter2;
		private string _ExpectedSalary;

		public string FirstName
		{
			get { return this._FirstName; }
			set
			{
				this._FirstName = value;
				NotifyPropertyChanged();
			}
		}
		public string LastName
		{
			get { return this._LastName; }
			set
			{
				this._LastName = value;
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
		public string Email
		{
			get { return this._Email; }
			set
			{
				this._Email = value;
				NotifyPropertyChanged();
			}
		}
		public string Mobile
		{
			get { return this._Mobile; }
			set
			{
				this._Mobile = value;
				NotifyPropertyChanged();
			}
		}
		public string LatestResumes
		{
			get { return this._LatestResumes; }
			set
			{
				this._LatestResumes = value;
				NotifyPropertyChanged();
			}
		}
		public string CoverLetter1
		{
			get { return this._CoverLetter1; }
			set
			{
				this._CoverLetter1 = value;
				NotifyPropertyChanged();
			}
		}
		public string CoverLetter2
		{
			get { return this._CoverLetter2; }
			set
			{
				this._CoverLetter2 = value;
				NotifyPropertyChanged();
			}
		}
		public string ExpectedSalary
		{
			get { return this._ExpectedSalary; }
			set
			{
				this._ExpectedSalary = value;
				NotifyPropertyChanged();
			}
		}

	}
}
