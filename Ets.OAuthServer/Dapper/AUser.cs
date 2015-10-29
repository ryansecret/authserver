using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ets.OAuthServer.Dapper
{
    public class AUser : IUser<string>
    {
        public string Id
        {
            get; set;
        }
        // 摘要: 
        //     Used to record failures for the purposes of lockout
        public virtual int AccessFailedCount { get; set; }
        //
        // 摘要: 
        //     Email
        public virtual string Email { get; set; }
        //
        // 摘要: 
        //     True if the email is confirmed, default is false
        public virtual bool EmailConfirmed { get; set; }
        //
        // 摘要: 
        //     Is lockout enabled for this user
        public virtual bool LockoutEnabled { get; set; }
        //
        // 摘要: 
        //     DateTime in UTC when lockout ends, any time in the past is considered not
        //     locked out.
        public virtual DateTime? LockoutEndDateUtc { get; set; }
        //
        // 摘要: 
        //     The salted/hashed form of the user password
        public virtual string PasswordHash { get; set; }
        //
        // 摘要: 
        //     PhoneNumber for the user
        public virtual string PhoneNumber { get; set; }
        //
        // 摘要: 
        //     True if the phone number is confirmed, default is false
        public virtual bool PhoneNumberConfirmed { get; set; }
        //
        // 摘要: 
        //     A random value that should change whenever a users credentials have changed
        //     (password changed, login removed)
        public virtual string SecurityStamp { get; set; }
        //
        // 摘要: 
        //     Is two factor enabled for the user
        public virtual bool TwoFactorEnabled { get; set; }
        //
        // 摘要: 
        //     User name
        public virtual string UserName { get; set; }
    }
}