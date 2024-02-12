using System;

namespace Turnit.GenericStore.Domain.Entity
{
    
public class ProductAvailability
{
    public virtual Guid Id { get; set; }

    public virtual Product Product { get; set; }

    public virtual Store Store { get; set; }

    public virtual int Availability { get; set; }
}
}