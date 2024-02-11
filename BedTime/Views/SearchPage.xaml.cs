using BedTime.ViewModel;

namespace BedTime.Views;

public partial class SearchPage : ContentPage
{
	public SearchPage(SearchPageViewModel vm)
	{
		InitializeComponent();
		BindingContext=vm;
	}
}