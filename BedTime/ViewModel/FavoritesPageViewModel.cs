using BedTime.Entity.Interfaces;
using BedTime.Models;
using BedTime.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BedTime.ViewModel
{
    public partial class FavoritesPageViewModel:ObservableObject
    {
        protected readonly IFairTalesRepository _fairTalesRepository;
        public FavoritesPageViewModel(IFairTalesRepository fairTalesRepository)
        {
            Favorites = new ObservableCollection<TailModel>();
            _fairTalesRepository = fairTalesRepository;
            LoadFavorites();
        }
        [ObservableProperty]
        ObservableCollection<TailModel>? favorites;

        [ObservableProperty]
        bool isBusy;

        async void LoadFavorites()
        {
            IsBusy = true;
            Favorites = new ObservableCollection<TailModel>(await _fairTalesRepository.GetAllFavorites());
            IsBusy = false;
            if (Favorites==null)
            {
                await Shell.Current.DisplayAlert("Error", "Brak ulubionych", "Ok");
            }
        }
        [RelayCommand]
        public async Task GoToHomePageAsync()
        {
            await Shell.Current.GoToAsync("../../");

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
    }
}
