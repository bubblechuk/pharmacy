using System;
using System.Collections.Generic;

namespace pharmacy.models;

public partial class DrugsOnSupHand
{
    public int DrugId { get; set; }

    public string DrugName { get; set; } = null!;

    public int? SupplierId { get; set; }

    public int? Prior { get; set; }

    public int? Filling { get; set; }

    public int? Amount { get; set; }

    public virtual Supplier? Supplier { get; set; }
}
