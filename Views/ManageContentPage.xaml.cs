using PimVIII.MauiCreator.ViewModels;

namespace PimVIII.MauiCreator.Views;

public partial class ManageContentPage : ContentPage
{
    // Construtor modificado para Injeção de Dependência
    public ManageContentPage(ManageContentViewModel viewModel)
    {
        InitializeComponent();

        // 1. Define o "cérebro" (ViewModel) da página
        BindingContext = viewModel;
    }
}