using Microsoft.AspNetCore.Identity;
using System;
using WebWallet.Models.Contracts;

namespace WebWallet.Models.Entities
{
    public class User : IdentityUser<string>, IEntity
    {
        public string Name
        {
            get { return this.UserName; }
            set { this.UserName = value; }
        }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}