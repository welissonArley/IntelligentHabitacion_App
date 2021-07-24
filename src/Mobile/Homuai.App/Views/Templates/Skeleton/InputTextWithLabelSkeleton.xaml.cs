using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Templates.Skeleton
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputTextWithLabelSkeleton : ContentView
    {
        public bool TopMargin { get; set; }
        public string LabelTitle { set { Label.Text = value; } get { return Label.Text; } }

        public static readonly BindableProperty TopMarginProperty = BindableProperty.Create(
                                                        propertyName: "TopMargin",
                                                        returnType: typeof(bool),
                                                        declaringType: typeof(InputTextWithLabelSkeleton),
                                                        defaultValue: false,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: TopMarginPropertyChanged);

        private static void TopMarginPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if ((bool)newValue)
                ((InputTextWithLabelSkeleton)bindable).component.Margin = new Thickness(0, 15, 0, 0);
        }

        public InputTextWithLabelSkeleton()
        {
            InitializeComponent();
        }
    }
}