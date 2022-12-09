using Stx.DataServices.Hrm;
using Stx.Shared.Interfaces;
using Stx.Shared.Interfaces.HRM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stx.JP.Mobile.DataService
{
    class MJobSearchDataService: JobSearchDataService
    {

        public MJobSearchDataService(IApiHelperDataService apiHelperDataService) : base(apiHelperDataService)
        {
        }
    }
}
