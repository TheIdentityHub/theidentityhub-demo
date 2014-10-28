//-----------------------------------------------------------------------
// <copyright file="MainPage.cs" company="U2U Consult NV/SA">
//     Copyright (c) U2U Consult NV/SA. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace U2UConsult.Win8DemoApp
{
    using System;
    using TheIdentityHub;
    using U2UConsult.Win8DemoApp.Data;
    using Windows.UI.Popups;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// The client identifier.
        /// </summary>
        private const string clientId = "4824062250562640119";

        /// <summary>
        /// The base URL.
        /// </summary>
        private static readonly Uri baseUrl = new Uri("https://theidentityhubtest.cloudapp.net/demo2/");

        /// <summary>
        /// The view model.
        /// </summary>
        private readonly MainViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

            this.DataContext = this.viewModel = new MainViewModel(clientId, baseUrl);
        }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <value>
        /// The view model.
        /// </value>
        public MainViewModel ViewModel
        {
            get
            {
                return this.viewModel;
            }
        }

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually the most relevant property to examine is Parameter.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (this.ViewModel.IdentityService.IsAuthenticated)
            {
                await this.ViewModel.Refresh();
            }
        }

        /// <summary>
        /// When the add account button is pushed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void AddAccount(object sender, RoutedEventArgs e)
        {
            var accountProvider = (sender as Control).Tag as AccountProvider;

            if (await this.ViewModel.IdentityService.AddAccountAsync(accountProvider))
            {
                await this.ViewModel.Refresh();
            }
        }

        /// <summary>
        /// When the refresh is pushed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void Refresh(object sender, RoutedEventArgs e)
        {
            this.progress.IsEnabled = true;

            try
            {
                await this.ViewModel.Refresh();
            }
            finally
            {
                this.progress.IsEnabled = false;
            }
        }

        /// <summary>
        /// When the Require TwoFactor Authentication button is pushed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void RequireTwoFactorAuthentication(object sender, RoutedEventArgs e)
        {
            var result = await this.ViewModel.IdentityService.RequireTwoFactorAuthenticationAsync();

            if (result)
            {
                await new MessageDialog("The user was authenticated using two-factor authentication.").ShowAsync();
            }
            else
            {
                await new MessageDialog("The user was not authenticated using two-factor authentication.").ShowAsync();
            }
        }

        /// <summary>
        /// When the sign in button is pushed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void SignIn(object sender, RoutedEventArgs e)
        {
            if (await this.ViewModel.IdentityService.TryAuthenticateAsync())
            {
                await this.ViewModel.Refresh();
            }
            else
            {
                await new MessageDialog("There was an error trying to sign you in.").ShowAsync();
            }
        }

        /// <summary>
        /// When the sign out button is pushed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SignOut(object sender, RoutedEventArgs e)
        {
            this.progress.IsEnabled = true;

            try
            {
                this.ViewModel.IdentityService.SignOut();
            }
            finally
            {
                this.progress.IsEnabled = false;
            }
        }
    }
}