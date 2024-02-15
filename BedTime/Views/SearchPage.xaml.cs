using BedTime.ViewModel;

namespace BedTime.Views;

public partial class SearchPage : ContentPage
{
	public SearchPage(SearchPageViewModel vm)
	{
		InitializeComponent();
		BindingContext=vm;
	}

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
		var vm = (SearchPageViewModel)BindingContext;
		vm.SearchTextChanged(e.NewTextValue);
    }
}