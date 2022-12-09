using Stx.DataServices.Hrm;
using Stx.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stx.JP.Mobile.DataService
{
    public class MJobOrderPreviewDataService : JobOrderPreviewDataService
    {

        public MJobOrderPreviewDataService(IApiHelperDataService apiHelperDataService) : base(apiHelperDataService)
        {
        }
    }
}
