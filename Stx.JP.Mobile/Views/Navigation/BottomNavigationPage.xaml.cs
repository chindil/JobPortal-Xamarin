using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Stx.JP.Mobile.Views.Navigation
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BottomNavigationPage : TabbedPage
    {
        public BottomNavigationPage()
        {
            InitializeComponent();
        }
    }
}