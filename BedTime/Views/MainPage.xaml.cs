using BedTime.ViewModel;

namespace BedTime
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            BindingContext=vm;
        }
    }
}
