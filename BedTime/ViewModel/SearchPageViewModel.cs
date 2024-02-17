using BedTime.Models;
using BedTime.Services.Interfaces;
using BedTime.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BedTime.ViewModel
{
    public partial class SearchPageViewModel: CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        private readonly IApiRepo _apiRepo;

        public SearchPageViewModel(IApiRepo apiRepo)
        {
            _apiRepo = apiRepo;
            SearchedTails = new ObservableRangeCollection<TailModel>();
            GetAllTails();
        }

        [ObservableProperty]
        bool isBusy;

        [ObservableProperty]
        ICommand search;

        [ObservableProperty]
        string testSearch;

        [ObservableProperty]
        ObservableRangeCollection<TailModel> tails;
        [ObservableProperty]
        ObservableRangeCollection<TailModel> searchedTails;


        public void SearchTextChanged(string text)
        {
            if (!text.Equals(string.Empty))
            {
                SearchedTails.Clear();
                SearchedTails.AddRange(Tails.Where(x => x.Title.ToLower().Contains(text.ToLower())));
            }
            
        }
        async void GetAllTails()
        {
            if (InternetAcsses())
            {
                IsBusy = true;
                Tails = new ObservableRangeCollection<TailModel>(await _apiRepo.GetAllTails());
                IsBusy = false;
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Brak połączenia z internetem. W trybie offline możesz prześć do ulubionych", "Ok");
            }
        }
        [RelayCommand]
        public async Task GoToHomePageAsync()
        {
            await Shell.Current.GoToAsync("../..");

        }
        [RelayCommand]
        public async Task GoToFavoritesPageAsync()
        {
            await Shell.Current.GoToAsync(nameof(FavoritesPage));
        }
        [RelayCommand]
        public async Task GoToSearchPageAsync()
        {
            await Shell.Current.GoToAsync(nameof(SearchPage));
        }
        [RelayCommand]
        public async Task GoToDetailPageAsync(TailModel tail)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?", new Dictionary<string, object>
            {
                {"tailId",tail }
            });
        }
        private bool InternetAcsses()
        {
            var current = Connectivity.NetworkAccess;
            return current == NetworkAccess.Internet;
        }
    }
}
