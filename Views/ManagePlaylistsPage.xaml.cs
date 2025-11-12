namespace PimVIII.MauiCreator.Views;
using PimVIII.MauiCreator.ViewModels;

public partial class ManagePlaylistsPage : ContentPage
{
	public ManagePlaylistsPage(ManagePlaylistsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}