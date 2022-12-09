using Stx.DataServices.Hrm;
using Stx.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stx.JP.Mobile.DataService
{
    class MCandidateProfileDataService : CandidateProfileDataService
    {
        private readonly IApiHelperDataService _apiHelperDataService;

        public MCandidateProfileDataService(IApiHelperDataService apiHelperDataService) : base(apiHelperDataService)
        {
        }
    }
}
