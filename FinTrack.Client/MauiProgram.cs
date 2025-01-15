using FinTrack.Client.Pages;
using FinTrack.Client.Pages.Profile;

using FinTrack.Client.Services;
using FinTrack.Client.ViewModels;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace FinTrack.Client;

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
        builder.Services.AddSingleton( s => new LoginPage()
        {
            BindingContext = s.GetRequiredService<LoginViewModel>()
        });

        builder.Services.AddSingleton<RegistrationViewModel>();
        builder.Services.AddSingleton(s => new RegistrationPage()
        {
            BindingContext = s.GetRequiredService<RegistrationViewModel>()
        });

        builder.Services.AddSingleton<BudgetListViewModel>();
        builder.Services.AddSingleton(s => new BudgetsListView()
        {
            BindingContext = s.GetRequiredService<BudgetListViewModel>()
        });


        builder.Services.AddSingleton<BudgetViewModel>();
        builder.Services.AddSingleton(s => new BudgetView()
        {
            BindingContext = s.GetRequiredService<BudgetViewModel>()
        });

        builder.Services.AddSingleton<BudgetCreateViewModel>();
        builder.Services.AddSingleton(s => new CreateBudgetView()
        {
            BindingContext = s.GetRequiredService<BudgetCreateViewModel>()
        });

        builder.Services.AddSingleton<AddCategoryViewModel>();
        builder.Services.AddSingleton(s => new AddCategoryView()
        {
            BindingContext = s.GetRequiredService<AddCategoryViewModel>()
        });

        builder.Services.AddSingleton<AddExpenseViewModel>();
        builder.Services.AddSingleton(s => new AddExpenseView()
        {
            BindingContext = s.GetRequiredService<AddExpenseViewModel>()
        });

        builder.Services.AddSingleton<AddIncomeViewModel>();
        builder.Services.AddSingleton(s => new AddIncomeView()
        {
            BindingContext = s.GetRequiredService<AddIncomeViewModel>()
        });

        builder.Services.AddSingleton<ExpensesViewModel>();
        builder.Services.AddSingleton(s => new ExpensesView()
        {
            BindingContext = s.GetRequiredService<ExpensesViewModel>()
        });

        builder.Services.AddSingleton<IncomesViewModel>();
        builder.Services.AddSingleton(s => new IncomesView()
        {
            BindingContext = s.GetRequiredService<IncomesViewModel>()
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
