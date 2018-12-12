using System;

namespace WebWallet.Models.Contracts
{
    public interface IEntity
    {
        string Id { get; set; }
        string Name { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }
    }
}