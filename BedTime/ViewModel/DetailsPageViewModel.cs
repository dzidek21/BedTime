using BedTime.Entity.Interfaces;
using BedTime.Models;
using BedTime.Services.Interfaces;
using BedTime.Views;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BedTime.ViewModel
{
    [QueryProperty("Tail", "tailId")]
    public partial class DetailsPageViewModel : ObservableObject
    {
        protected readonly IApiRepo _apiRepo;
        protected readonly IFairTalesRepository _fairTalesRepository;
        protected readonly ILikedStoryRepo _likedStoryRepo;
        public DetailsPageViewModel(IApiRepo apiRepo, IFairTalesRepository fairTalesRepository, ILikedStoryRepo likedStoryRepo)
        {
            _apiRepo = apiRepo;
            _fairTalesRepository = fairTalesRepository;
            _likedStoryRepo = likedStoryRepo;
            CheckLikes();
            CheckFavorites();
        }

        [ObservableProperty]
        TailModel? tail;

        [ObservableProperty]
        string? iconFontFavorites;

        [ObservableProperty]
        string? iconFonteLike;

        public async void CheckLikes()
        {
            while (Tail == null)
            {
                await Task.Delay(1000);
            }
            var tailLike = await _likedStoryRepo.GetLikedStory(Tail.Id);
            // rest of the method body
            if (tailLike.StoryId == 0)
            {
                IconFonteLike = IconFont.Thumb_up_off_alt;
            }
            else 
            {
                IconFonteLike = IconFont.Thumb_up;
            }
        }
        public async void CheckFavorites()
        {
            while (Tail == null)
            {
                await Task.Delay(1000);
            }
            var favorites= await _fairTalesRepository.GetTailById(Tail.Id);
            if (favorites.Story!=null)
            {
                IconFontFavorites = IconFont.Favorite;
            }
            else
            {
                IconFontFavorites = IconFont.Favorite_outline;
            }
        }

        [RelayCommand]
        public async Task AddLike()
        {
            //dodaj polubienie
            //dodaj do bazy danych(moze zrobic nowa baze danych tylko z Id z bajki)
            //potem sprawdz czy jest w bazie danych do ponownego polubienia

            if (Tail!=null)
            {
                var foundStories = await _likedStoryRepo.GetLikedStory(Tail.Id);
                if (foundStories.StoryId == 0)
                {
                    var newLikedStory = new LikedStory() { IsLiked = true, StoryId = Tail.Id };
                    await _likedStoryRepo.AddLikedStory(newLikedStory);
                    await _apiRepo.AddLike(Tail.Id);
                    IconFonteLike = IconFont.Thumb_up;
                    await Shell.Current.DisplaySnackbar($"Polubiłeś {Tail.Title}");
                }
                else
                {
                    await _likedStoryRepo.DeleteLikedStory(foundStories.Id);
                    await _apiRepo.RemoveLike(Tail.Id);
                    IconFonteLike = IconFont.Thumb_up_off_alt;
                    await Shell.Current.DisplaySnackbar($"Nie lubisz już {Tail.Title}");
                } 
            }

        }

        [RelayCommand]
        public async Task AddFavorites()
        {
            if (Tail!=null)
            {
                var isFavorite=await _fairTalesRepository.AddToFavorites(Tail);
                if (isFavorite)
                {
                    IconFontFavorites = IconFont.Favorite;
                    await Shell.Current.DisplaySnackbar($"Dodano {Tail.Title} do ulubionych");
                }
                else
                {
                    IconFontFavorites = IconFont.Favorite_outline;
                    await Shell.Current.DisplaySnackbar($"Usunięto {Tail.Title} z ulubionych");
                } 
            }
        }

        [RelayCommand]
        public async Task GoToHomePageAsync()
        {
            await Shell.Current.GoToAsync("..");

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
    }
}
