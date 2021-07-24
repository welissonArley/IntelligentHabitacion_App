using Homuai.App.Model;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Templates.Information
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyFriendsComponent : ContentView
    {
        public FriendModel Friend
        {
            get => (FriendModel)GetValue(FriendProperty);
            set => SetValue(FriendProperty, value);
        }

        public static readonly BindableProperty FriendProperty = BindableProperty.Create(
                                                        propertyName: "Friend",
                                                        returnType: typeof(FriendModel),
                                                        declaringType: typeof(MyFriendsComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: FriendChanged);

        public static readonly BindableProperty TappedMakePhonecallCommandProperty = BindableProperty.Create(propertyName: "TappedMakePhonecall",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(MyFriendsComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        public static readonly BindableProperty TappedItemCommandProperty = BindableProperty.Create(propertyName: "TappedItem",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(MyFriendsComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        public ICommand TappedMakePhonecallCommand
        {
            get => (ICommand)GetValue(TappedMakePhonecallCommandProperty);
            set => SetValue(TappedMakePhonecallCommandProperty, value);
        }

        public ICommand TappedItemCommand
        {
            get => (ICommand)GetValue(TappedItemCommandProperty);
            set => SetValue(TappedItemCommandProperty, value);
        }

        private static void FriendChanged(BindableObject bindable, object oldValue, object newValue)
        {
            newValue = oldValue != null && newValue is null ? oldValue : newValue;
            if (newValue != null)
            {
                var friendModel = (FriendModel)newValue;
                var component = (MyFriendsComponent)bindable;
                component.LabelFriendsName.Text = friendModel.Name;
                component.LabelShortName.Text = new Services.ShortNameConverter().Converter(friendModel.Name);
                component.BackgroundShortName.Fill = new SolidColorBrush(Color.FromHex(friendModel.ProfileColor)); ;
                component.BackgroundCall.Fill = new SolidColorBrush(Color.FromHex(friendModel.ProfileColor));
                component.LabelJoinedOn.Text = string.Format(ResourceText.TITLE_JOINED_IN, friendModel.JoinedOn.ToString(ResourceText.FORMAT_DATE));
            }
        }

        public MyFriendsComponent()
        {
            InitializeComponent();
        }

        private void MakePhoneCall_Tapped(object sender, System.EventArgs e)
        {
            TappedMakePhonecallCommand?.Execute(Friend);
        }

        private void Item_Tapped(object sender, System.EventArgs e)
        {
            TappedItemCommand?.Execute(this);
        }

        public void Refresh()
        {
            LabelFriendsName.Text = Friend.Name;
            LabelShortName.Text = new Services.ShortNameConverter().Converter(Friend.Name);
            BackgroundShortName.Fill = new SolidColorBrush(Color.FromHex(Friend.ProfileColor));
            BackgroundCall.Fill = new SolidColorBrush(Color.FromHex(Friend.ProfileColor));
            LabelJoinedOn.Text = string.Format(ResourceText.TITLE_JOINED_IN, Friend.JoinedOn.ToString(ResourceText.FORMAT_DATE));
        }
    }
}