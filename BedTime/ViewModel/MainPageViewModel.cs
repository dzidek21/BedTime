using BedTime.Entity.Interfaces;
using BedTime.Models;
using BedTime.Services.Interfaces;
using BedTime.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BedTime.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly IFairTalesRepository _fairTalesRepository;
        private readonly IApiRepo _apiRepo;
        public MainPageViewModel(IFairTalesRepository fairTalesRepository, IApiRepo apiRepo)
        {
            
            FairTails=new ObservableCollection<TailModel>();
            MostLiked=new ObservableCollection<TailModel>();
            _fairTalesRepository = fairTalesRepository;
            _apiRepo = apiRepo;
            //var t=Task.Run(() =>CheckNewStory());
            CheckNewStory();
            
        }

        [ObservableProperty]
        ObservableCollection<TailModel> fairTails;

        [ObservableProperty]
        ObservableCollection<TailModel> mostLiked;

        public static List<TailModel> FairTailsSearchRsult=new List<TailModel>();

        [ObservableProperty]
        ObservableCollection<TailModel>? fairTailsHot;
        [ObservableProperty]
        char searchModel;
        [ObservableProperty]
        bool isBusy;
        

        async void CheckNewStory()
        {
            if (InternetAcsses())
            {
                IsBusy = true;
                FairTails=new ObservableCollection<TailModel>( await _apiRepo.GetAllTails());
                MostLiked = new ObservableCollection<TailModel>(FairTails.OrderByDescending(x => x.LikeCount));
                IsBusy = false;
                if(FairTailsSearchRsult.Count>0)
                {
                    FairTailsSearchRsult.Clear();
                }
                SortAndAddToHot();
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Brak połączenia z internetem. W trybie offline możesz prześć do ulubionych", "Ok");
                //Przeniesc do ulubionych

            }
            
        }
        // This method checks for internet access by checking the current network access status.
        // It returns true if there is internet access, otherwise false.
        bool InternetAcsses()
        {
            var current = Connectivity.NetworkAccess;
            return current == NetworkAccess.Internet;
        }
        public void SortAndAddToHot()
        {
            FairTailsHot=new ObservableCollection<TailModel>();
            var sortedTails = FairTails.OrderBy(tail => Guid.NewGuid()).ToList();
            foreach (var tail in sortedTails)
            {
                FairTailsHot.Add(tail);
            }
        }
        [RelayCommand]
        public async Task GoToHomePageAsync()
        {
            await Shell.Current.GoToAsync(nameof(MainPage));
            
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
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?",new Dictionary<string, object>
            {
                {"tailId",tail }
            });
        }   
    }
}
