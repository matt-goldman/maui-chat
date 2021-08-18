using System;
using MauiChat.Maui.ViewModels;
using Microsoft.Maui.Controls;

namespace MauiChat.Maui
{
	public partial class MainPage : ContentPage
	{
		public MainViewModel ViewModel { get; set; }

		public MainPage()
		{
			//InitializeComponent();
			ViewModel = new MainViewModel();
			BindingContext = ViewModel;
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			await ViewModel.Initialise();
		}
	}
}
