namespace PimVIII.MauiCreator.Views;
using PimVIII.MauiCreator.ViewModels;

public partial class AddPlaylistPage : ContentPage
{
	public AddPlaylistPage(AddPlaylistViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}