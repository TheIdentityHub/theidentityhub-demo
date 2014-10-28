//-----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="U2U Consult NV/SA">
//     Copyright (c) U2U Consult NV/SA. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace U2UConsult.Win8DemoApp.Data
{
    using System;
    using System.Threading.Tasks;
    using TheIdentityHub;

    public sealed class MainViewModel : BindableBase
    {
        private readonly IdentityService identityService;

        private AccountProvider[] accountProviders;
        private Friend[] friends;
        private Profile profile;

        public MainViewModel(string clientId, Uri baseUrl)
        {
            this.identityService = new IdentityService(clientId, baseUrl);
        }

        public AccountProvider[] AccountProviders
        {
            get
            {
                return this.accountProviders;
            }

            private set
            {
                this.SetProperty(ref this.accountProviders, value);
            }
        }

        public Friend[] Friends
        {
            get
            {
                return this.friends;
            }

            private set
            {
                this.SetProperty(ref this.friends, value);
            }
        }

        public IdentityService IdentityService
        {
            get
            {
                return this.identityService;
            }
        }

        public Profile Profile
        {
            get
            {
                return this.profile;
            }

            private set
            {
                this.SetProperty(ref this.profile, value);
            }
        }

        public async Task Refresh()
        {
            var profileTask = this.identityService.GetProfileAsync();

            var friendsTask = this.identityService.GetFriendsAsync();

            var accountProvidersTask = this.identityService.GetAccountProvidersAsync();

            this.Profile = await profileTask;

            this.Friends = await friendsTask;

            this.AccountProviders = await accountProvidersTask;
        }
    }
}