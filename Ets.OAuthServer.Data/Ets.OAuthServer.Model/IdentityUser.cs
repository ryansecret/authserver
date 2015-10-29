﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ets.OAuthServer.Model
{
    public class IdentityUser<TKey> where TKey : IEquatable<TKey>
    {
        public IdentityUser() { }

        public IdentityUser(string userName) : this()
        {
            UserName = userName;
        }

        public virtual TKey Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string NormalizedUserName { get; set; }

        /// <summary>
        ///     Email
        /// </summary>
        public virtual string Email { get; set; }

        public virtual string NormalizedEmail { get; set; }

        /// <summary>
        ///     True if the email is confirmed, default is false
        /// </summary>
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>
        ///     The salted/hashed form of the user password
        /// </summary>
        public virtual string PasswordHash { get; set; }

        /// <summary>
        /// A random value that should change whenever a users credentials change (password changed, login removed)
        /// </summary>
        public virtual string SecurityStamp { get; set; }

        /// <summary>
        /// A random value that should change whenever a user is persisted to the store
        /// </summary>
        public virtual string ConcurrencyStamp { get; set; }
        //= Guid.NewGuid().ToString();

        /// <summary>
        ///     PhoneNumber for the user
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        ///     True if the phone number is confirmed, default is false
        /// </summary>
        public virtual bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        ///     Is two factor enabled for the user
        /// </summary>
        public virtual bool TwoFactorEnabled { get; set; }

        /// <summary>
        ///     DateTime in UTC when lockout ends, any time in the past is considered not locked out.
        /// </summary>
        public virtual DateTimeOffset? LockoutEnd { get; set; }

        /// <summary>
        ///     Is lockout enabled for this user
        /// </summary>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        ///     Used to record failures for the purposes of lockout
        /// </summary>
        public virtual int AccessFailedCount { get; set; }

        /// <summary>
        ///     Navigation property for users in the role
        /// </summary>
        private ICollection<IdentityUserRole<TKey>> _roles;
        public virtual ICollection<IdentityUserRole<TKey>> Roles
        {
            get { return this._roles; }
            set{this._roles = new List<IdentityUserRole<TKey>>();}
        } 
        //= new List<IdentityUserRole<TKey>>();

        /// <summary>
        ///     Navigation property for users claims
        /// </summary>
        private ICollection<IdentityUserClaim<TKey>> _claims;
        public virtual ICollection<IdentityUserClaim<TKey>> Claims
        {
            get { return this._claims; }
            set { this._claims = new List<IdentityUserClaim<TKey>>(); }
        } 
        //= new List<IdentityUserClaim<TKey>>();

        /// <summary>
        ///     Navigation property for users logins
        /// </summary>
        private ICollection<IdentityUserLogin<TKey>> _logins;
        public virtual ICollection<IdentityUserLogin<TKey>> Logins
        {
            get { return _logins; }
            set { this._logins= new List<IdentityUserLogin<TKey>>();}
        }
        //= new List<IdentityUserLogin<TKey>>();

        /// <summary>
        /// Returns a friendly name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return UserName;
        }
    }
}
