using FinTrack.Client.Pages;
using FinTrack.Client.Pages.Profile;
using FinTrack.Client.Services.Implementation;
using FinTrack.Client.Services.Interfaces;
using FinTrack.Client.ViewModels;
using Microsoft.Extensions.Logging;

namespace FinTrack.Client
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IBudgetService, BudgetService>();
            builder.Services.AddTransient<ICurrencyService, CurrencyService>();

            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<LoginPage>( s => new LoginPage()
            {
                BindingContext = s.GetRequiredService<LoginViewModel>()
            });

            builder.Services.AddSingleton<RegistrationViewModel>();
            builder.Services.AddSingleton<RegistrationPage>(s => new RegistrationPage()
            {
                BindingContext = s.GetRequiredService<RegistrationViewModel>()
            });

            builder.Services.AddSingleton<BudgetViewModel>();
            builder.Services.AddSingleton<BudgetView>(s => new BudgetView()
            {
                BindingContext = s.GetRequiredService<BudgetViewModel>()
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        //private static void AddView<TView,TViewModel>(this IServiceCollection services)
        //    where TView : ContentPage, new()
        //{
        //    services.AddSingleton<TView>(serviceProvider => new TView()
        //    {
        //        BindingContext = serviceProvider.GetRequiredService<TViewModel>()
        //    })
        //}
    }
}
