using FinTrack.Client.Pages;
using FinTrack.Client.Pages.Profile;

using FinTrack.Client.Services;
using FinTrack.Client.ViewModels;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using SkiaSharp.Views.Maui.Controls.Hosting;
namespace FinTrack.Client
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseSkiaSharp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddServices();

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

            builder.Services.AddSingleton<BudgetListViewModel>();
            builder.Services.AddSingleton<BudgetsListView>(s => new BudgetsListView()
            {
                BindingContext = s.GetRequiredService<BudgetListViewModel>()
            });


            builder.Services.AddSingleton<BudgetViewModel>();
            builder.Services.AddSingleton<BudgetView>(s => new BudgetView()
            {
                BindingContext = s.GetRequiredService<BudgetViewModel>()
            });

            //builder.Services.AddTransientPopup<CreateBudgetPopup, CreateBudgetViewModel>();

            builder.Services.AddSingleton<BudgetCreateViewModel>();
            builder.Services.AddSingleton<CreateBudgetView>(s => new CreateBudgetView()
            {
                BindingContext = s.GetRequiredService<BudgetCreateViewModel>()
            });

            builder.Services.AddSingleton<AddCategoryViewModel>();
            builder.Services.AddSingleton<AddCategoryView>(s => new AddCategoryView()
            {
                BindingContext = s.GetRequiredService<AddCategoryViewModel>()
            });



            builder.Services.AddSingleton<ExpensesViewModel>();
            builder.Services.AddSingleton<ExpensesView>(s => new ExpensesView()
            {
                BindingContext = s.GetRequiredService<ExpensesViewModel>()
            });

            builder.Services.AddSingleton<IncomesViewModel>();
            builder.Services.AddSingleton<IncomesView>(s => new IncomesView()
            {
                BindingContext = s.GetRequiredService<IncomesViewModel>()
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
