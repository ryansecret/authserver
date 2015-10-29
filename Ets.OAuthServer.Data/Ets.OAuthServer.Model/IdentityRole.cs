using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ets.OAuthServer.Model
{
    public class IdentityRole<TKey> where TKey : IEquatable<TKey>
    {
        public IdentityRole() { }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="roleName"></param>
        public IdentityRole(string roleName) : this()
        {
            Name = roleName;
        }

        /// <summary>
        ///     Navigation property for users in the role
        /// </summary>
        private ICollection<IdentityUserRole<TKey>> _users;
        public virtual ICollection<IdentityUserRole<TKey>> Users {
            get { return _users; }
            set { this._users=new List<IdentityUserRole<TKey>>(); }
        } 
        //= new List<IdentityUserRole<TKey>>();

        /// <summary>
        ///     Navigation property for claims in the role
        /// </summary>
        private ICollection<IdentityRoleClaim<TKey>> _claims;
        public virtual ICollection<IdentityRoleClaim<TKey>> Claims {
            get { return _claims; }
            set {this._claims=  new List<IdentityRoleClaim<TKey>>();}
        }
        //= new List<IdentityRoleClaim<TKey>>();

        /// <summary>
        ///     Role id
        /// </summary>
        public virtual TKey Id { get; set; }

        /// <summary>
        ///     Role name
        /// </summary>
        public virtual string Name { get; set; }
        public virtual string NormalizedName { get; set; }

        /// <summary>
        /// A random value that should change whenever a role is persisted to the store
        /// </summary>
        private string _concurrencyStamp;
        public virtual string ConcurrencyStamp
        {
            get { return _concurrencyStamp; }
            set { _concurrencyStamp = Guid.NewGuid().ToString(); }
        } 

        /// <summary>
        /// Returns a friendly name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
