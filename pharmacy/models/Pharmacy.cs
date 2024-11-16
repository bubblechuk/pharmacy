using System;
using System.Collections.Generic;

namespace pharmacy.models;

public partial class Pharmacy
{
    public int PharmacyId { get; set; }

    public string? Name { get; set; }

    public int? SupplierId { get; set; }

    public string? Adress { get; set; }

    public virtual ICollection<DrugsOnParHand> DrugsOnParHands { get; set; } = new List<DrugsOnParHand>();

    public virtual Supplier? Supplier { get; set; }
}
