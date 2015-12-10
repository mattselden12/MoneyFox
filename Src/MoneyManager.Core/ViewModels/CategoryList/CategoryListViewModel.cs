﻿using System.Collections.ObjectModel;
using System.Linq;
using Cirrious.MvvmCross.ViewModels;
using MoneyManager.Foundation.Interfaces;
using MoneyManager.Foundation.Model;
using MoneyManager.Localization;
using PropertyChanged;

namespace MoneyManager.Core.ViewModels.CategoryList
{
    [ImplementPropertyChanged]
    public abstract class CategoryListViewModel : BaseViewModel
    {
        protected readonly IRepository<Category> CategoryRepository;
        protected readonly IDialogService DialogService;

        private string searchText;

        /// <summary>
        ///     Baseclass for the categorylist usercontrol
        /// </summary>
        /// <param name="categoryRepository">An instance of <see cref="IRepository{T}" /> of type category.</param>
        /// <param name="dialogService">An instance of <see cref="IDialogService" /></param>
        protected CategoryListViewModel(IRepository<Category> categoryRepository,
            IDialogService dialogService)
        {
            CategoryRepository = categoryRepository;
            DialogService = dialogService;

            Categories = categoryRepository.Data;
        }

        /// <summary>
        ///     Deletes the passed Category after show a confirmation dialog.
        /// </summary>
        public MvxCommand<Category> DeleteCategoryCommand => new MvxCommand<Category>(DeleteCategory);

        public ObservableCollection<Category> Categories { get; set; }

        public abstract Category SelectedCategory { get; set; }

        /// <summary>
        ///     Text to search for. Will perform the search when the text changes.
        /// </summary>
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                Search();
            }
        }

        /// <summary>
        ///     Performs a search with the text in the searchtext property
        /// </summary>
        public void Search()
        {
            if (SearchText != string.Empty)
            {
                Categories = new ObservableCollection<Category>
                    (CategoryRepository.Data.Where(
                        x => x.Name != null && x.Name.ToLower().Contains(searchText.ToLower()))
                        .ToList());
            }
            else
            {
                Categories = CategoryRepository.Data;
            }
        }

        private async void DeleteCategory(Category categoryToDelete)
        {
            if (await DialogService.ShowConfirmMessage(Strings.DeleteTitle, Strings.DeleteCategoryConfirmationMessage))
            {
                CategoryRepository.Delete(categoryToDelete);
            }
        }
    }
}