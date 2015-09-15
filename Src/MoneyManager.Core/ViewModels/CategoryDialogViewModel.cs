using Cirrious.MvvmCross.ViewModels;
using MoneyManager.Core.ViewModels;
using MoneyManager.Foundation;
using MoneyManager.Foundation.Model;
using MoneyManager.Foundation.OperationContracts;
using MoneyManager.Localization;

namespace MoneyManager.Windows.Dialogs
{
    public class CategoryDialogViewModel: BaseViewModel
    {
        private readonly IRepository<Category> categoryRepository;
        private readonly IDialogService dialogService;
        private readonly CategoryListViewModel categoryListView;

        public CategoryDialogViewModel(IRepository<Category> categoryRepository, IDialogService dialogService, CategoryListViewModel categoryListView)
        {
            this.categoryRepository = categoryRepository;
            this.dialogService = dialogService;
            this.categoryListView = categoryListView;
        }

        public Category Selected { get; set; }

        public bool IsEdit { get; set; }

        public IMvxCommand LoadedCommand => new MvxCommand(Loaded);

        private void Loaded()
        {
            if (!IsEdit)
            {
                Selected = new Category();

                if (!string.IsNullOrEmpty(categoryListView.SearchText))
                {
                    Selected.Name = categoryListView.SearchText;
                }
            }
        }

        public IMvxCommand DoneCommand => new MvxCommand(Done);

        private async void Done()
        {
            if (string.IsNullOrEmpty(Selected.Name))
            {
                await dialogService.ShowMessage(Strings.MandatoryFieldEmptryTitle, Strings.NameRequiredMessage);
                return;
            }


            categoryRepository.Save(Selected);
            categoryListView.SearchText = string.Empty;
            categoryListView.Search();
        }
    }
}