using System;
using Dapper.Extensions;

namespace AspnetIdentity.Dapper
{
    [Table("AspNetUserLogins")]
    public partial class IdentityUserClaim<TKey>
    {
        public IdentityUserClaim()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public TKey UserId { get; set; }
        public string ClaimValue { get; set; }
        public string ClaimType { get; set; }
    }
}
