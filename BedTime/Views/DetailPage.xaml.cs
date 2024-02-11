using BedTime.ViewModel;

namespace BedTime.Views;

public partial class DetailPage : ContentPage
{
	public DetailPage(DetailsPageViewModel vm)
	{
		InitializeComponent();
		BindingContext=vm;
	}
}