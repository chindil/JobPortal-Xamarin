using Stx.DataServices.Hrm;
using Stx.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stx.JP.Mobile.DataService
{
    class MJobSendoutDataService : JobSendoutDataService
    {
        public MJobSendoutDataService(IApiHelperDataService apiHelperDataService) : base(apiHelperDataService)

        {
        }
    }
}
