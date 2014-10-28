//-----------------------------------------------------------------------
// <copyright file="IndexModel.cs" company="U2U Consult NV/SA">
//     Copyright (c) U2U Consult NV/SA. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace U2UConsult.AspNetMvcDemo.Models
{
    using System.Collections.Generic;
    using TheIdentityHub;

    public class IndexModel
    {
        /// <summary>
        /// Gets or sets the account providers.
        /// </summary>
        /// <value>
        /// The account providers.
        /// </value>
        public AccountProvider[] AccountProviders { get; set; }

        /// <summary>
        /// Gets or sets the friends.
        /// </summary>
        /// <value>
        /// The friends.
        /// </value>
        public IEnumerable<Friend> Friends { get; set; }

        /// <summary>
        /// Gets or sets the profile.
        /// </summary>
        /// <value>
        /// The profile.
        /// </value>
        public Profile Profile { get; set; }
    }
}