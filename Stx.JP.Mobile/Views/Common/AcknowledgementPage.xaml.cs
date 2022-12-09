using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stx.JP.Mobile.Views.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AcknowledgementPage : ContentPage
    {
        public enum AckType
        {
            Error,
            JobSent,
        }

        public AcknowledgementPage(AckType ackType, object paramID, object parmSubject)
        {
            InitializeComponent();
        }
    }
}