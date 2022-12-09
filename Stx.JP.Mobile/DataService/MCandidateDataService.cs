using Stx.DataServices.Hrm;
using Stx.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stx.JP.Mobile.DataService
{
    class MCandidateDataService : CandidateDataService
    {
        private readonly IApiHelperDataService _apiHelperDataService;

        public MCandidateDataService(IApiHelperDataService apiHelperDataService) : base(apiHelperDataService)
        {
        }
    }
}
