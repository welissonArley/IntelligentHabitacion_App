using Homuai.App.Model;
using Homuai.App.Services;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Templates.Information
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeInformationsRoomTemplate : ContentView
    {
        public IList<RoomModel> Rooms
        {
            get => (IList<RoomModel>)GetValue(RoomsProperty);
            set => SetValue(RoomsProperty, value);
        }

        public ICommand AddRoomCommand
        {
            get => (ICommand)GetValue(AddRoomCommandProperty);
            set => SetValue(AddRoomCommandProperty, value);
        }
        public ICommand RemoveRoomCommand
        {
            get => (ICommand)GetValue(RemoveRoomCommandProperty);
            set => SetValue(RemoveRoomCommandProperty, value);
        }

        public static readonly BindableProperty RoomsProperty = BindableProperty.Create(
                                                        propertyName: "Rooms",
                                                        returnType: typeof(IList<RoomModel>),
                                                        declaringType: typeof(HomeInformationsRoomTemplate),
                                                        defaultValue: new List<RoomModel>(),
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: RoomsChanged);

        public static readonly BindableProperty AddRoomCommandProperty = BindableProperty.Create(
                                                        propertyName: "AddRoomCommand",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(HomeInformationsRoomTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: null);

        public static readonly BindableProperty RemoveRoomCommandProperty = BindableProperty.Create(
                                                        propertyName: "RemoveRoomCommand",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(HomeInformationsRoomTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: null);

        private static void RoomsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            newValue = oldValue != null && newValue is null ? oldValue : newValue;
            if (newValue != null)
            {
                var rooms = (IEnumerable<RoomModel>)newValue;
                var component = (HomeInformationsRoomTemplate)bindable;

                component.Content.Children.Clear();

                var isAdministrator = XLabs.Ioc.Resolver.Resolve<UserPreferences>().IsAdministrator;

                foreach (var room in rooms)
                    component.Content.Children.Add(CreateContentRoom(room.Room, isAdministrator, component));

                if (isAdministrator)
                {
                    component.Content.Children.Add(new Xamarin.Forms.Button
                    {
                        FontSize = 16,
                        BackgroundColor = Color.Transparent,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        TextColor = (Color)Application.Current.Resources["YellowDefault"],
                        Text = ResourceText.TITLE_ADD_ROOM,
                        Command = new Command(() =>
                        {
                            component.AddRoomCommand?.Execute(null);
                        })
                    });
                }
            }
        }

        private static StackLayout CreateContentRoom(string room, bool isAdministrator, HomeInformationsRoomTemplate component)
        {
            var deleteImage = new Image
            {
                HeightRequest = 15,
                WidthRequest = 15,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Source = ImageSource.FromFile("IconDelete"),
                IsVisible = isAdministrator
            };

            var tapGestureRecognizer = new TapGestureRecognizer();

            tapGestureRecognizer.Tapped += (s, e) =>
            {
                component.RemoveRoomCommand?.Execute(room);
            };

            deleteImage.GestureRecognizers.Add(tapGestureRecognizer);

            return new StackLayout
            {
                Margin = new Thickness(0, 20, 0, 0),
                Children =
                {
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label
                            {
                                Text = room,
                                FontSize = 14
                            },
                            deleteImage
                        }
                    },
                    new BoxView
                    {
                        HeightRequest = 1,
                        Opacity = 0.2,
                        Color = (Color)Application.Current.Resources["GrayDefault"]
                    }
                }
            };
        }

        public HomeInformationsRoomTemplate()
        {
            InitializeComponent();
        }
    }
}