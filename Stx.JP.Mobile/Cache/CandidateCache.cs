using Stx.JP.Mobile.Models.Candidate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stx.JP.Mobile.Cache
{
    public class CandidateCache
    {
        internal static CandidateInfo CandidateSummary { get; private set; } = new CandidateInfo();
        internal static int CandidateID { get; set; } = 100;

        public void Clear()
        {

        }

        public static async Task Refresh()
        {
            CandidateSummary.FirstName = "ddddd";
            CandidateSummary.LastName = "dilhan";
            CandidateSummary.CurrentJobTitle = "ddddddddddddddfdad";
            CandidateSummary.Email = "ddd@ggg.com";
            CandidateSummary.Mobile = "658954225";
            CandidateSummary.LatestResumes = "my_resume.pdf";
            CandidateSummary.CoverLetter1 = "Cover letter 1 fasd fasdfsadf asdhfsajdhf askjfdsfsdfasd";
            CandidateSummary.CoverLetter2 = "";
            CandidateSummary.ExpectedSalary = "5000";
        }

    }
}
