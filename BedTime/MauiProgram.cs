using BedTime.Entity;
using BedTime.Entity.Interfaces;
using BedTime.Entity.Repository;
using BedTime.Services;
using BedTime.Services.Interfaces;
using BedTime.ViewModel;
using BedTime.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace BedTime
{
    public static class MauiProgram
    {
        static void AllowMultiLineTruncation()
        {
            static void UpdateMaxLines(Microsoft.Maui.Handlers.LabelHandler handler, ILabel label)
            {
#if ANDROID
      var textView = handler.PlatformView;
      if(    label is Label controlsLabel 
          && textView.Ellipsize == Android.Text.TextUtils.TruncateAt.End )
      {
        textView.SetMaxLines( controlsLabel.MaxLines );
      }
#elif IOS
      var textView = handler.PlatformView;
      if( label is Label controlsLabel
          && textView.LineBreakMode == UILineBreakMode.TailTruncation )
      {
        textView.Lines = controlsLabel.MaxLines;
      }  
#endif
            };

            Label.ControlsLabelMapper.AppendToMapping(
               nameof(Label.LineBreakMode), UpdateMaxLines);

            Label.ControlsLabelMapper.AppendToMapping(
              nameof(Label.MaxLines), UpdateMaxLines);
        }
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Roboto-Regular.ttf", "Roboto");
                    fonts.AddFont("Montserrat-Bold.ttf", "MontserratBold");
                    fonts.AddFont("Montserrat-Medium.ttf", "MontserratMedium");
                    fonts.AddFont("Montserrat-SemiBold.ttf", "MontserratSemiBold");
                    fonts.AddFont("MaterialIcons-Regular.ttf", "GoogleIconFonts");
                });
            
            builder.Services.AddHttpClient();
            builder.Services.AddTransient<IApiRepo, FairyTailsApi>();
            builder.Services.AddTransient<IFairTalesRepository, FairTailsRepository>();
            builder.Services.AddTransient<ILikedStoryRepo, LikedStoryRepo>();
            builder.Services.AddDbContext<DatabaseContext>();
            
            builder.Services.AddSingleton<FairyTailsApi>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddTransient<FavoritesPage>();
            builder.Services.AddTransient<FavoritesPageViewModel>();
            builder.Services.AddTransient<SearchPage>();
            builder.Services.AddTransient<SearchPageViewModel>();
            builder.Services.AddTransient<DetailPage>();
            builder.Services.AddTransient<DetailsPageViewModel>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
