namespace PimVIII.MauiCreator.Views;
using PimVIII.MauiCreator.ViewModels;

public partial class AddConteudoPage : ContentPage
{
    public AddConteudoPage(AddConteudoViewModel viewModel)
    {
        InitializeComponent();

        // Define o "cérebro" (ViewModel) da página
        BindingContext = viewModel;
    }
}