namespace PimVIII.MauiCreator.Views;
using PimVIII.MauiCreator.ViewModels;

public partial class PlaylistDetailsPage : ContentPage
{
	public PlaylistDetailsPage(PlaylistDetailsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}