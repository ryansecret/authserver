using Dapper.Extensions;

namespace AspnetIdentity.Dapper
{
    [Table("AspNetUserRoles")]
    public partial class IdentityUserRole<TKey,TRoleKey>
    {
        //public IdentityUserRole()
        //{
        //    Id = Guid.NewGuid().ToString();
        //}
        //public string Id { get; set; }
        public TKey UserId { get; set; }
        public TRoleKey RoleId { get; set; }
    }
}
