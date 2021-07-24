using Homuai.Exception;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Homuai.App.ViewModel.Home.Informations
{
    public class InsertRoomViewModel : BaseViewModel
    {
        public IList<string> RoomsSaved { get; set; }
        public string Room { get; set; }
        public ICommand SelectRoomExempleCommand { get; private set; }
        public ICommand CallbackSelectRoomCommand { get; set; }
        public ICommand SelectedRoomCommand { get; private set; }

        public InsertRoomViewModel()
        {
            SelectRoomExempleCommand = new Command((text) =>
            {
                SelectedRoomExemple(text.ToString());
            });

            SelectedRoomCommand = new Command(async () =>
            {
                await RoomSelected();
            });
        }

        private void SelectedRoomExemple(string text)
        {
            Room = text;
            OnPropertyChanged(new PropertyChangedEventArgs("Room"));
        }

        private async Task RoomSelected()
        {
            if (string.IsNullOrWhiteSpace(Room))
                await Exception(ResourceTextException.ROOM_EMPTY);
            else if (RoomsSaved.Any(c => c.Equals(Room)))
                await Exception(ResourceTextException.ROOM_NAME_EXIST);
            else
            {
                CallbackSelectRoomCommand.Execute(Room);
                await Navigation.PopAsync();
            }
        }
    }
}
