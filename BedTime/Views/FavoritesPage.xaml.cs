using BedTime.ViewModel;

namespace BedTime.Views;

public partial class FavoritesPage : ContentPage
{
	public FavoritesPage(FavoritesPageViewModel vm)
	{
		InitializeComponent();
		BindingContext=vm;
	}
}