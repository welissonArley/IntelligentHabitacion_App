using Homuai.App.Model;
using Homuai.App.UseCases.MyFoods.DeleteMyFood;
using Homuai.App.UseCases.MyFoods.RegisterMyFood;
using Homuai.App.UseCases.MyFoods.UpdateMyFood;
using Homuai.App.ValueObjects.Enum;
using Homuai.App.Views.Modal;
using Rg.Plugins.Popup.Extensions;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace Homuai.App.ViewModel.MyFoods
{
    public class AddEditMyFoodsViewModel : BaseViewModel
    {
        #region CheckBox
        public bool IsCheckedUnity
        {
            set { if (value) Model.Type = ProductEnum.Unity; }
            get { return Model.Type == ProductEnum.Unity; }
        }
        public bool IsCheckedBox
        {
            set { if (value) Model.Type = ProductEnum.Box; }
            get { return Model.Type == ProductEnum.Box; }
        }
        public bool IsCheckedPackage
        {
            set { if (value) Model.Type = ProductEnum.Package; }
            get { return Model.Type == ProductEnum.Package; }
        }
        public bool IsCheckedKilogram
        {
            set { if (value) Model.Type = ProductEnum.Kilogram; }
            get { return Model.Type == ProductEnum.Kilogram; }
        }
        #endregion

        public ICommand SelectDueDateTapped { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveAndNewCommand { get; }

        private readonly Lazy<IRegisterMyFoodUseCase> registerUseCase;
        private readonly Lazy<IUpdateMyFoodUseCase> editUseCase;
        private readonly Lazy<IDeleteMyFoodUseCase> deleteUseCase;
        private IRegisterMyFoodUseCase _registerUseCase => registerUseCase.Value;
        private IUpdateMyFoodUseCase _editUseCase => editUseCase.Value;
        private IDeleteMyFoodUseCase _deleteUseCase => deleteUseCase.Value;

        public string Title { get; set; }
        public FoodModel Model { get; set; }

        public Action<FoodModel> CallbackSave { get; set; }
        public Action<FoodModel> CallbackDelete { get; set; }

        public AddEditMyFoodsViewModel(Lazy<IRegisterMyFoodUseCase> registerUseCase,
            Lazy<IUpdateMyFoodUseCase> editUseCase, Lazy<IDeleteMyFoodUseCase> deleteUseCase)
        {
            this.registerUseCase = registerUseCase;
            this.editUseCase = editUseCase;
            this.deleteUseCase = deleteUseCase;

            SelectDueDateTapped = new Command(async () =>
            {
                await ClickSelectDueDate();
            });
            SaveCommand = new Command(async () =>
            {
                await OnSaveItem();
            });
            SaveAndNewCommand = new Command(async () =>
            {
                await OnSaveAndNew();
            });
            DeleteCommand = new Command(async () =>
            {
                await OnDeleteItem();
            });
        }

        private async Task OnSaveItem()
        {
            try
            {
                SendingData();

                FoodModel model = Model;

                if (string.IsNullOrEmpty(Model.Id))
                    model = await _registerUseCase.Execute(Model);
                else
                    await _editUseCase.Execute(Model);

                CallbackSave?.Invoke(model);

                await Sucess();

                await Navigation.PopAsync();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private async Task OnSaveAndNew()
        {
            try
            {
                SendingData();

                var model = await _registerUseCase.Execute(Model);
                CallbackSave?.Invoke(model);

                Model = new FoodModel
                {
                    Quantity = 1.00m
                };
                OnPropertyChanged(new PropertyChangedEventArgs("Model"));

                await Sucess();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private async Task OnDeleteItem()
        {
            try
            {
                SendingData();
                await _deleteUseCase.Execute(Model.Id);
                CallbackDelete(Model);
                await Sucess();
                await Navigation.PopAsync();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
        private async Task ClickSelectDueDate()
        {
            await ShowLoading();
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PushPopupAsync(new Calendar(Model.DueDate ?? DateTime.Today, OnDateSelected, minimumDate: DateTime.Today));
            HideLoading();
        }
        private Task OnDateSelected(DateTime date)
        {
            Model.DueDate = date;
            OnPropertyChanged(new PropertyChangedEventArgs("Model"));
            return Task.CompletedTask;
        }
    }
}
