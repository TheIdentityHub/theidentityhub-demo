//-----------------------------------------------------------------------
// <copyright file="MainForm.cs" company="U2U Consult NV/SA">
//     Copyright (c) U2U Consult NV/SA. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace U2UConsult.WindowsDemoApp45
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using TheIdentityHub;

    public partial class MainForm : Form
    {
        /// <summary>
        /// The friends.
        /// </summary>
        private List<Friend> friends;

        /// <summary>
        /// The identity service.
        /// </summary>
        private IdentityService identityService = new IdentityService("[Your App Client Id]", new Uri("https://www.theidentityhub.com/[Your Tenant Name]"));

        /// <summary>
        /// The profile.
        /// </summary>
        private Profile profile;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the requireTwoFactorButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void requireTwoFactorButton_Click(object sender, EventArgs e)
        {
            await identityService.RequireTwoFactorAuthenticationAsync();
        }

        /// <summary>
        /// Handles the Click event of the signInButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void signInButton_Click(object sender, EventArgs e)
        {
            if (identityService.IsAuthenticated)
            {
                identityService.SignOut();
                this.signInButton.Text = "Sign In";

                profile = null;
                friends.Clear();
            }
            else
            {
                if (await identityService.TryAuthenticateAsync())
                {
                    profile = await identityService.GetProfileAsync();
                    friends = new List<Friend>(await identityService.GetFriendsAsync());

                    this.friendsListBox.SelectedIndexChanged += (s, ea) =>
                    {
                        if (this.friendsListBox.SelectedItem != null)
                        {
                            this.friendPictureBox.ImageLocation = ((Friend)this.friendsListBox.SelectedItem).SmallPicture.AbsoluteUri;
                        }
                    };

                    this.profilePictureBox.ImageLocation = profile.Picture.AbsoluteUri;
                    this.profileNameLabel.Text = profile.DisplayName;
                    this.friendsListBox.DisplayMember = "DisplayName";
                    this.friendsListBox.ValueMember = "IdentityId";
                    this.friendsListBox.DataSource = friends;

                    this.signInButton.Text = "Sign Out";
                }
            }

            this.profilePictureBox.Visible = identityService.IsAuthenticated;
            this.requireTwoFactorButton.Visible = identityService.IsAuthenticated;
            this.friendPictureBox.Visible = identityService.IsAuthenticated;
            this.friendsLabel.Visible = identityService.IsAuthenticated;
            this.friendsListBox.Visible = identityService.IsAuthenticated;
            this.profileNameLabel.Visible = identityService.IsAuthenticated;
        }
    }
}