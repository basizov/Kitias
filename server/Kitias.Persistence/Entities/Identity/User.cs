using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Kitias.Persistence.Entities.Identity
{
    /// <summary>
    /// User entity
    /// </summary>
	public class User : IdentityUser<Guid>
    {
        /// <summary>
        /// Collection of user claims
        /// </summary>
        public virtual ICollection<UserClaim> Claims { get; set; }
        /// <summary>
        /// Collection of user logins
        /// </summary>
        public virtual ICollection<UserLogin> Logins { get; set; }
        /// <summary>
        /// Collection of user tokens
        /// </summary>
        public virtual ICollection<UserToken> Tokens { get; set; }
        /// <summary>
        /// Collection of user roles
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
