using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Stx.JP.Mobile.Views.Candidate.Templates
{
    /// <summary>
    /// Article list template.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobSearchListTemplate : Grid
    {
        /// <summary>
        /// Bindable property to set the parent bindingcontext.
        /// </summary>
        public static BindableProperty ParentBindingContextProperty =
         BindableProperty.Create(nameof(ParentBindingContext), typeof(object), typeof(JobSearchListTemplate), null);

        /// <summary>
        /// Gets or sets the parent bindingcontext.
        /// </summary>
        public object ParentBindingContext
        {
            get { return GetValue(ParentBindingContextProperty); }
            set { SetValue(ParentBindingContextProperty, value); }
        }

        /// <summary>
        /// Bindable property to set the parent element.
        /// </summary>
        public static BindableProperty ParentElementProperty =
         BindableProperty.Create(nameof(ParentElement), typeof(object), typeof(JobSearchListTemplate), null);

        /// <summary>
        /// Gets or sets the Parent element.
        /// </summary>
        public object ParentElement
        {
            get { return GetValue(ParentElementProperty); }
            set { SetValue(ParentElementProperty, value); }
        }

        /// <summary>
        /// Bindable property to set the child element.
        /// </summary>
        public static BindableProperty ChildElementProperty =
         BindableProperty.Create(nameof(ChildElement), typeof(object), typeof(JobSearchListTemplate), null);

        /// <summary>
        /// Gets or sets the child element.
        /// </summary>
        public object ChildElement
        {
            get { return GetValue(ChildElementProperty); }
            set { SetValue(ChildElementProperty, value); }
        }

        /// <summary>
        /// Bindable property to set the job list type.
        /// </summary>
        public static BindableProperty IsShowBookmarkProperty =
         BindableProperty.Create(nameof(IsShowBookmark), typeof(bool), typeof(JobSearchListTemplate), true);

        /// <summary>
        /// Gets or sets the is show bookmarks element.
        /// </summary>
        public bool IsShowBookmark
        {
            get { return (bool)GetValue(IsShowBookmarkProperty); }
            set { SetValue(IsShowBookmarkProperty, value); }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="JobSearchListTemplate"/> class.
        /// </summary>
		public JobSearchListTemplate()
        {
            InitializeComponent();
        }
    }
}