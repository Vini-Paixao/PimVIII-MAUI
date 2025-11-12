using PimVIII.MauiCreator.ViewModels;
namespace PimVIII.MauiCreator.Views;

public partial class AnalyticsPage : ContentPage
{
    public AnalyticsPage(AnalyticsViewModel viewModel) // <-- Modifique
    {
        InitializeComponent();
        BindingContext = viewModel; // <-- Adicione
    }
}